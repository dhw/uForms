using UnityEngine;
using UnityEditor;
using System.IO;

namespace uForms
{
    /// <summary></summary>
    public class UFStudio : EditorWindow
    {
        private const string TempXmlPath = "Data/UFStudio/~temp.xml";

        private static DesignerView designer = null;
        private static OutlineView outline = null;
        private static PropertyView property = null;
        private static UFStudio studio = null;
        public static UFProject project { get; private set; }

        [MenuItem("Window/uForms Studio")]
        public static void OpenStudio()
        {
            studio = GetWindow<UFStudio>("uForms Studio");
            designer = DesignerView.OpenWindow();
            outline = OutlineView.OpenWindow();
            property = PropertyView.OpenWindow();
        }

        void OnEnable()
        {
            studio = this;
            designer = DesignerView.OpenWindow();
            outline = OutlineView.OpenWindow();
            property = PropertyView.OpenWindow();

            UFSelection.OnSelectionChange += (control) =>
            {
                RepaintAll();
            };

            Initialize();
        }

        void OnDisable()
        {
            project.ExportXml(TempXmlPath);
            UFSelection.OnSelectionChange = null;
            UFSelection.ActiveControl = null;
            studio = null;
            designer = null;
            outline = null;
            property = null;
        }

        void OnDestroy()
        {
            if(designer != null)
            {
                designer.Close();
                designer = null;
            }
            if(outline != null)
            {
                outline.Close();
                outline = null;
            }
            if(property != null)
            {
                property.Close();
                property = null;
            }
        }

        public static void Initialize()
        {
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

        public static void RepaintAll()
        {
            property.Repaint();
            designer.Repaint();
            outline.Repaint();
            studio.Repaint();
        }

        void OnGUI()
        {
            if(GUILayout.Button("Export Code"))
            {
                string path = EditorUtility.SaveFilePanel("コードの出力先を選択", "Assets/Editor", project.className, "cs");
                if(!string.IsNullOrEmpty(path) && path.Contains("/Editor/"))
                {
                    project.ExportCode(path);
                    Debug.Log(path);
                }
            }
        }
    }
}