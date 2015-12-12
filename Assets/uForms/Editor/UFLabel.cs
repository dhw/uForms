using UnityEngine;

namespace uForms
{
    public class UFLabel : UFControl
    {
        public static readonly Vector2 DefaultSize = new Vector2(80, 16);
        
        public override void Draw()
        {
            GUILayout.Label(this.text);
        }

        public override void DrawByRect()
        {
            GUI.Label(this.rect, this.text);
        }

        public override void DrawDesign()
        {
            GUILayout.Label(this.text);
        }

        public override void DrawDesignByRect()
        {
            GUI.Label(this.rect, this.text);
        }

        public UFLabel(Vector2 position)
        {
            this.rect = new Rect(position, DefaultSize);
        }

        public override void WriteCode(CodeBuilder builder)
        {
            builder.WriteLine("this." + this.Name + " = new UFLabel(new Vector2(" + this.rect.x.ToString() + ", " + this.rect.y.ToString() + "));");
            base.WriteCode(builder);
        }

        public override void WriteDefinitionCode(CodeBuilder builder)
        {
            builder.WriteLine("private UFLabel " + this.Name + ";");
        }
    }
}
