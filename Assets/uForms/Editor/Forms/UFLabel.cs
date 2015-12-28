using UnityEngine;

namespace uForms
{
    public class UFLabel : UFControl
    {
        public override string DefaultName { get { return "label"; } }
        public override Vector2 DefaultSize { get { return new Vector2(80, 16); } }
        
        public override void Draw()
        {
            GUILayout.Label(this.Text);
        }

        public override void DrawByRect()
        {
            GUI.Label(this.DrawRect, this.Text);
        }
    }
}
