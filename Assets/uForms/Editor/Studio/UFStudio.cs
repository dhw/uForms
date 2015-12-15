using UnityEngine;
using UnityEditor;
using System.IO;

namespace uForms
{
    public class UFStudio : SingletonWindow<UFStudio>
    {
        private const string TempXmlPath = "Temp/UFStudio/~temp.xml";

        public class UIOP
        {
            public static GUILayoutOption[] Button = {GUILayout.Width(100), GUILayout.Height(100) };
        }

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
                UFProject.ImportXml(TempXmlPath);
            }
            else
            {
                UFProject.CreateNewProject();
                UFProject.Current.ClassName = "HidakaEditor";
                UFProject.Current.Namespace = "hidaka";

                UFCanvas canvas1 = new UFCanvas();
                canvas1.Text = "canvas1";
                canvas1.Name = "canvas1";
                UFProject.Current.Controls.Add(canvas1);
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

            UFProject.ExportXml(TempXmlPath);
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
            GUILayout.BeginHorizontal();
            {
                if(GUILayout.Button("New Form", UIOP.Button))
                {

                }

                if(GUILayout.Button("Import Code", UIOP.Button))
                {
                    UFSelector.OpenWindow((t) =>
                    {
                        UFProject.ImportCode(t);
                        UFSelection.ActiveControl = null;
                        RepaintAll();
                    });
                }

                if(GUILayout.Button("Export Code", UIOP.Button))
                {
                    string path = EditorUtility.SaveFilePanel("Choice the save path", "Assets/Editor", UFProject.Current.ClassName, "cs");
                    if(!string.IsNullOrEmpty(path) && path.Contains("/Editor/"))
                    {
                        UFProject.ExportCode(path);
                        Debug.Log(path);
                    }
                }

                if(GUILayout.Button("Import Xml", UIOP.Button))
                {

                }

                if(GUILayout.Button("Export Xml", UIOP.Button))
                {

                }
            }
        }
    }
}