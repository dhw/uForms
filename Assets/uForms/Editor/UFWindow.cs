using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace uForms.Editor.Control
{
    public class UFWindow : UFControl
    {
        public override void DrawDesign()
        {
            this.childList.ForEach(child => child.DrawDesign());
        }
    }
}
