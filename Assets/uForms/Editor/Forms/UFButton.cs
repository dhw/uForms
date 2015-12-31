using System;
using System.Xml.Serialization;
using UnityEngine;

namespace uForms
{
    public class UFButton : UFControl
    {
        public override string DefaultName { get { return "button"; } }
        public override Vector2 DefaultSize { get { return new Vector2(80, 16); } }
        public override GUIStyle DesignGUIStyle { get { return new GUIStyle("button"); } }

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
            if(GUI.Button(this.DrawRect, this.Text))
            {
                if(this.OnClick != null)
                {
                    this.OnClick();
                }
            }
        }

        public override void WriteNativeContentDefinitionCode(CodeBuilder builder)
        {
            builder.WriteLine("public static readonly GUIContent {0} = new GUIContent(\"{1}\");",
                this.Name, this.Text);
        }

        public override void WriteNativeCodeByRect(CodeBuilder builder)
        {
            builder.WriteLine("if(GUI.Button(DrawRects.{0}, DrawContents.{1}))", this.Name, this.Name);
            builder.WriteLine("{");
            builder.WriteLine("");
            builder.WriteLine("}");
        }
    }
}
