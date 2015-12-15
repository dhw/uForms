using UnityEngine;

namespace uForms
{
    /// <summary></summary>
    public class UFCanvas : UFControl
    {
        public static readonly Rect DefaultRect = new Rect(0,0,400,400);

        public UFCanvas()
        {
            this.DrawRect = DefaultRect;
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
                builder.WriteLine("// " + child.Name);
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