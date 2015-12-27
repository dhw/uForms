using UnityEngine;

namespace uForms
{
    public class UFLabel : UFControl
    {
        public override Vector2 DefaultSize { get { return new Vector2(80, 16); } }

        public override void Draw()
        {
            GUILayout.Label(this.text);
        }

        public override void DrawByRect()
        {
            GUI.Label(this.DrawRect, this.text);
        }

        public override void DrawDesign()
        {
            GUILayout.Label(this.text);
        }

        public override void DrawDesignByRect()
        {
            GUI.Label(this.DrawRect, this.text);
        }

        public UFLabel()
        {

        }

        public override void WriteCode(CodeBuilder builder)
        {
            builder.WriteLine("this." + this.Name + " = new UFLabel();");
            base.WriteCode(builder);
        }

        public override void WriteDefinitionCode(CodeBuilder builder)
        {
            builder.WriteLine("private UFLabel " + this.Name + ";");
        }
    }
}
