using UnityEngine;
using UnityEditor;
using System.IO;

namespace uForms
{
    public class UFStudio : SingletonWindow<UFStudio>
    {
        private const string TempXmlPath = "Temp/UFStudio/~temp.xml";
        
        public static UFProject project { get; private set; }

        [MenuItem("Window/uForms Studio")]
        public static void OpenStudio()
        {
            OpenWindowIfNotExists();
            DesignerView.OpenWindowIfNotExists();
            OutlineView.OpenWindowIfNotExists();
            PropertyView.OpenWindowIfNotExists();
        }

        public static void RepaintAll()
        {
            Instance.Repaint();
            OutlineView.Instance.Repaint();
            DesignerView.Instance.Repaint();
            PropertyView.Instance.Repaint();
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            UFSelection.OnSelectionChange += (control) =>
            {
                RepaintAll();
            };

            if(File.Exists(TempXmlPath))
            {
                project = UFProject.CreateFromXml(TempXmlPath);
            }
            else
            {
                project = new UFProject();
                project.className = "HidakaEditor";
                project.nameSpace = "hidaka";

                UFCanvas canvas1 = new UFCanvas();
                canvas1.Text = "canvas1";
                canvas1.Name = "canvas1";
                project.Controls.Add(canvas1);
                UFButton button1 = new UFButton(new Vector2(5, 60));
                button1.Text = "テストA";
                button1.Name = "button1";
                canvas1.Add(button1);
                UFLabel label1 = new UFLabel(new Vector2(5, 20));
                label1.Text = "テストB";
                label1.Name = "label1";
                canvas1.Add(label1);
            }
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            project.ExportXml(TempXmlPath);
            UFSelection.OnSelectionChange = null;
            UFSelection.ActiveControl = null;
        }

        void OnDestroy()
        {
            OutlineView.CloseIfExists();
            DesignerView.CloseIfExists();
            PropertyView.CloseIfExists();
        }

        void OnGUI()
        {
            if(GUILayout.Button("Export Code"))
            {
                string path = EditorUtility.SaveFilePanel("Choice the save path", "Assets/Editor", project.className, "cs");
                if(!string.IsNullOrEmpty(path) && path.Contains("/Editor/"))
                {
                    project.ExportCode(path);
                    Debug.Log(path);
                }
            }

            if(GUILayout.Button("Import Code"))
            {
                UFSelector.OpenWindow((t) =>
                {
                    project = UFProject.CreateFromType(t);
                    UFSelection.ActiveControl = null;
                    RepaintAll();
                });
            }
        }
    }
}