using System.Xml.Serialization;
using UnityEngine;

namespace uForms
{
    public class UFButton : UFControl
    {
        public override Vector2 DefaultSize { get { return new Vector2(80, 16); } }

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
    }
}
