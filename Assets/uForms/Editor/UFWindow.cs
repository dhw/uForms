using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace uForms.Editor.Control
{
    public class UFWindow : UFControl
    {
        public UFWindow(Vector2 size)
        {
            this.rect = new Rect(Vector2.zero, size);
        }

        public override void DrawDesign()
        {
            this.childList.ForEach(child => child.DrawDesign());
        }
    }
}
