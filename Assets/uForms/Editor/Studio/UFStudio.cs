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
            public static GUILayoutOption[] Button = {GUILayout.Width(100), GUILayout.Height(60) };
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
                    UFProject.CreateNewProject();
                    RepaintAll();
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
                    string path = EditorUtility.SaveFilePanel("Select the path to export", "Assets/Editor", UFProject.Current.ClassName, "cs");
                    if(!string.IsNullOrEmpty(path) && path.Contains("/Editor/"))
                    {
                        UFProject.ExportCode(path);
                    }
                }

                if(GUILayout.Button("Import Xml", UIOP.Button))
                {
                    string path = EditorUtility.OpenFilePanel("Select the path to import", "", "xml");
                    if(!string.IsNullOrEmpty(path) && File.Exists(path))
                    {
                        UFProject.ImportXml(path);
                        RepaintAll();
                    }
                }

                if(GUILayout.Button("Export Xml", UIOP.Button))
                {
                    string path = EditorUtility.SaveFilePanel("Select the path to export", "", UFProject.Current.ClassName, "xml");
                    if(!string.IsNullOrEmpty(path))
                    {
                        UFProject.ExportXml(path);
                    }
                }
            }
        }
    }
}