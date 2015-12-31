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

        public override void WriteNativeContentDefinitionCode(CodeBuilder builder)
        {
            builder.WriteLine("public static readonly GUIContent {0} = new GUIContent(\"{1}\");",
                this.Name, this.Text);
        }

        public override void WriteNativeCodeByRect(CodeBuilder builder)
        {
            builder.WriteLine("GUI.Label(DrawRects.{0}, DrawContents.{1});", this.Name, this.Name);
        }
    }
}
