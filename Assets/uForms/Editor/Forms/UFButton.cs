using System.Xml.Serialization;
using UnityEngine;

namespace uForms
{
    public class UFButton : UFControl
    {
        public static readonly Vector2 DefaultOffset = new Vector2(10,10);
        public static readonly Vector2 DefaultSize = new Vector2(80,16);
        
        [XmlIgnore]
        public System.Action OnClick = null;

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
            if(GUI.Button(this.DrawRect, this.text))
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
            GUI.Label(this.DrawRect, this.text, "button");
        }

        public UFButton() { }

        public UFButton(Vector2 position)
        {
            this.DrawRect = new Rect(position, DefaultSize);
        }

        public UFButton(UFControl parent)
        {
            this.DrawRect = new Rect(parent.DrawRect.position + DefaultOffset, DefaultSize);
        }

        public override void WriteCode(CodeBuilder builder)
        {
            builder.WriteLine("this." + this.Name + " = new UFButton();");
            base.WriteCode(builder);
        }

        public override void WriteDefinitionCode(CodeBuilder builder)
        {
            builder.WriteLine("private UFButton " + this.Name + ";");
        }
    }
}
