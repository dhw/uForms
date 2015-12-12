using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace uForms.Editor.Control
{
    public class UFRoot : UFControl
    {
        public UFRoot(Vector2 size)
        {
            this.rect = new Rect(Vector2.zero, size);
        }
        
        public void DrawTree()
        {
            this.childList.ForEach(child => child.DrawDesign());
        }

        public override void WriteCode(CodeBuilder builder)
        {
            this.childList.ForEach(child =>
            {
                child.WriteCode(builder);
                builder.WriteLine("this.Controls.Add(this." + child.Name + ");");
            });
        }

        public override void WriteDefinitionCode(CodeBuilder builder)
        {
        }
    }
}
