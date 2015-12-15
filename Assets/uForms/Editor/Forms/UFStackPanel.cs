using UnityEditor;
using UnityEngine;

namespace uForms
{
    public enum StackDirection
    {
        Vertical,
        Horizontal
    }

    public class UFStackPanel : UFControl
    {
        public StackDirection direction = StackDirection.Vertical;

        public UFStackPanel()
        {
        }

        public UFStackPanel(UFControl parent, StackDirection direction = StackDirection.Vertical)
        {
            if(direction == StackDirection.Vertical)
            {
                this.DrawRect = new Rect(parent.DrawRect.x, parent.DrawRect.y, parent.DrawRect.width, 100);
            }
            else
            {
                this.DrawRect = new Rect(parent.DrawRect.x, parent.DrawRect.y, 100, parent.DrawRect.height);
            }
        }

        public override void DrawDesign()
        {
            if(direction == StackDirection.Horizontal)
            {
                GUILayout.BeginHorizontal();
                {
                    this.childList.ForEach(child => child.DrawDesign());
                }
                GUILayout.EndHorizontal();
            }
            else
            {
                GUILayout.BeginVertical();
                {
                    this.childList.ForEach(child => child.DrawDesign());
                }
                GUILayout.EndHorizontal();
            }
        }

        protected override void DrawPropertyOriginal()
        {
            DrawPropertyItem("StackDirection", () =>
            {
                this.direction = (StackDirection)EditorGUILayout.EnumPopup(this.direction);
            });
        }
    }
}
