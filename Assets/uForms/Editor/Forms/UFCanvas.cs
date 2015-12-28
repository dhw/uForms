using UnityEngine;

namespace uForms
{
    /// <summary></summary>
    public class UFCanvas : UFControl
    {
        public override Vector2 DefaultSize { get { return new Vector2(250, 250); } }

        public override void Draw()
        {
            if(IsHidden) { return; }
            GUI.BeginGroup(this.DrawRect);
            this.childList.ForEach(child => child.DrawByRect());
            GUI.EndGroup();
        }

        public override void DrawByRect()
        {
            Draw();
        }

        public override void DrawDesign()
        {
            if(IsHidden) { return; }
            GUI.Label(this.DrawRect, "", "GroupBox");
            GUI.BeginGroup(this.DrawRect);
            this.childList.ForEach(child => child.DrawDesignByRect());
            GUI.EndGroup();
        }

        public override void DrawDesignByRect()
        {
            DrawDesign();
        }

        public override void WriteCodeAdditional(CodeBuilder builder)
        {
            this.childList.ForEach(child =>
            {
                builder.WriteLine("// " + child.Name);
                child.WriteCode(builder);
                builder.WriteLine("this." + this.Name + ".Add(this." + child.Name + ");");
            });
        }
    }
}