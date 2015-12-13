using UnityEditor;
using UnityEngine;

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
            if(Event.current.type == EventType.MouseDown)
            {
                CheckSelection();
            }

            UFStudio.project.root.DrawTree();

            if(UFSelection.ActiveControl != null)
            {
                BeginWindows();
                UFSelection.ActiveControl.DrawGuide();
                EndWindows();
            }
        }

        void CheckSelection()
        {
            Vector2 click = Event.current.mousePosition;
            bool selected = false;

            UFStudio.project.root.ForTreeFromChild(child =>
            {
                if(selected) { return; }
                if(UFSelection.ActiveControl == child)
                {
                    if(child.DrawRectWithGuide.Contains(click))
                    {
                        selected = true;
                    }
                }
                if(child.DrawRect.Contains(click))
                {
                    UFSelection.ActiveControl = child;
                    selected = true;
                }
            });
        }
    }
}