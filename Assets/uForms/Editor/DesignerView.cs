using UnityEditor;

namespace uForms.Editor.View
{
    public class DesignerView : EditorWindow
    {
        public static DesignerView OpenWindow()
        {
            return GetWindow<DesignerView>("Designer");
        }

        void Awake()
        {
            this.wantsMouseMove = true;
        }

        void OnGUI()
        {
            UFStudio.project.root.DrawTree();

            if(UFSelection.ActiveControl != null)
            {
                BeginWindows();
                UFSelection.ActiveControl.DrawGuide();
                EndWindows();
            }

        }
    }
}