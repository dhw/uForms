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

        public override void WriteNativeCodeByRect(CodeBuilder builder)
        {
            builder.WriteLine(string.Format("GUI.Label(new Rect({0}f, {1}f, {2}f, {3}f), {4});",
                this.DrawRect.x, this.DrawRect.y, this.DrawRect.width, this.DrawRect.height,
                "\"" + this.Text + "\""));
        }
    }
}
