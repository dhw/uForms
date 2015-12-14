using UnityEditor;
using UnityEngine;

namespace uForms
{
    public class DesignerView : SingletonWindow<DesignerView>
    {        
        void Awake()
        {
            this.wantsMouseMove = true;
        }

        void OnGUI()
        {
            if(UFStudio.project == null) { return; }

            if(Event.current.type == EventType.MouseDown)
            {
                CheckSelection();
            }

            UFStudio.project.Controls.ForEach(child => child.DrawDesign());

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

            UFStudio.project.Controls.ForEach(child => child.ForTreeFromChild(node =>
            {
                if(selected) { return; }
                if(UFSelection.ActiveControl == node)
                {
                    if(node.DrawRectWithGuide.Contains(click - node.parentPosition))
                    {
                        selected = true;
                    }
                }
                if(node.DrawRect.Contains(click - node.parentPosition))
                {
                    UFSelection.ActiveControl = node;
                    selected = true;
                }
            }));
        }
    }
}