using UnityEditor;
using UnityEngine;

namespace uForms.Editor.Control
{
    public class UFButton : UFControl
    {
        public Vector2 DefaultOffset = new Vector2(10,10);
        public Vector2 DefaultSize = new Vector2(80,16);

        public override void DrawDesign()
        {
            GUILayout.Button(this.Text);
        }

        public override void DrawByRect()
        {
            if(IsHidden) { return; }
            GUI.Label(this.rect, this.Text, "button");
        }

        public UFButton(UFControl parent)
        {
            this.rect = new Rect(parent.DrawRect.position + DefaultOffset, DefaultSize);
        }
    }
}
