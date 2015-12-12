using UnityEngine;

namespace uForms
{
    public class UFButton : UFControl
    {
        public System.Action OnClick = null;

        public Vector2 DefaultOffset = new Vector2(10,10);
        public Vector2 DefaultSize = new Vector2(80,16);

        public override void Draw()
        {
            if(GUILayout.Button(this.text))
            {
                if(this.OnClick != null)
                {
                    this.OnClick();
                }
            }
        }

        public override void DrawByRect()
        {
            if(GUI.Button(this.rect, this.text))
            {
                if(this.OnClick != null)
                {
                    this.OnClick();
                }
            }
        }

        public override void DrawDesign()
        {
            GUILayout.Button(this.text);
        }

        public override void DrawDesignByRect()
        {
            if(IsHidden) { return; }
            GUI.Label(this.rect, this.text, "button");
        }

        public UFButton(Vector2 position)
        {
            this.rect = new Rect(position, DefaultSize);
        }

        public UFButton(UFControl parent)
        {
            this.rect = new Rect(parent.DrawRect.position + DefaultOffset, DefaultSize);
        }

        public override void WriteCode(CodeBuilder builder)
        {
            builder.WriteLine("this." + this.Name + " = new UFButton(new Vector2(" + this.rect.x.ToString() + ", " + this.rect.y.ToString() + "));");
            base.WriteCode(builder);
        }

        public override void WriteDefinitionCode(CodeBuilder builder)
        {
            builder.WriteLine("private UFButton " + this.Name + ";");
        }
    }
}
