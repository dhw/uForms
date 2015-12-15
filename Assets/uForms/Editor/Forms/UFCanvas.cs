using UnityEngine;

namespace uForms
{
    /// <summary></summary>
    public class UFCanvas : UFControl
    {
        public static readonly Rect DefaultRect = new Rect(0,0,100,100);

        public static readonly Vector2 DefaultSize = new Vector2(100, 100);

        public UFCanvas()
        {
            this.DrawRect = DefaultRect;
        }

        public UFCanvas(UFControl parent)
        {
            this.DrawRect = new Rect(parent.DrawRect.position, DefaultSize);
        }

        public override void Draw()
        {
            if(IsHidden) { return; }
            GUI.BeginGroup(this.DrawRect);
            this.childList.ForEach(child => child.DrawByRect());
            GUI.EndGroup();
        }

        public override void DrawDesign()
        {
            if(IsHidden) { return; }
            GUI.Label(this.DrawRect, "", "GroupBox");
            GUI.BeginGroup(this.DrawRect);
            this.childList.ForEach(child => child.DrawDesignByRect());
            GUI.EndGroup();
        }

        public override void WriteCode(CodeBuilder builder)
        {
            builder.WriteLine("this." + this.Name + " = new UFCanvas();");
            base.WriteCode(builder);
            this.childList.ForEach(child =>
            {
                child.WriteCode(builder);
                builder.WriteLine("this." + this.Name + ".Add(this." + child.Name + ");");
            });
        }

        public override void WriteDefinitionCode(CodeBuilder builder)
        {
            builder.WriteLine("private UFCanvas " + this.Name + ";");
        }
    }
}