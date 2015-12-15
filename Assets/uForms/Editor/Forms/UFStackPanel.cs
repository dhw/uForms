using UnityEditor;
using UnityEngine;

namespace uForms
{
    public enum Orientation
    {
        Vertical,
        Horizontal
    }

    public class UFStackPanel : UFControl
    {
        public Orientation direction = Orientation.Vertical;

        public UFStackPanel()
        {
        }

        public UFStackPanel(UFControl parent, Orientation direction = Orientation.Vertical)
        {
            if(direction == Orientation.Vertical)
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
            if(direction == Orientation.Horizontal)
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
                this.direction = (Orientation)EditorGUILayout.EnumPopup(this.direction);
            });
        }
    }
}
