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

        public override void WriteNativeCodeByRect(CodeBuilder builder)
        {
            builder.WriteLine(string.Format("if(GUI.Button(new Rect({0}f, {1}f, {2}f, {3}f), {4}))",
                this.DrawRect.x, this.DrawRect.y, this.DrawRect.width, this.DrawRect.height, "\"" + this.Text +"\""));
            builder.WriteLine("{");
            builder.WriteLine("");
            builder.WriteLine("}");
        }
    }
}
