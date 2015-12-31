using UnityEngine;

namespace uForms
{
    /// <summary></summary>
    public class UFCanvas : UFControl
    {
        public override string DefaultName { get { return "canvas"; } }
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

        public override void WriteNativeCodeByRect(CodeBuilder builder)
        {
            builder.WriteLine("GUI.BeginGroup(DrawRects.{0});", this.Name);
            builder.WriteLine("{");
            ++builder.Indent;
            this.childList.ForEach(child => child.WriteNativeCodeByRect(builder));
            --builder.Indent;
            builder.WriteLine("}");
            builder.WriteLine("GUI.EndGroup();");
        }
    }
}