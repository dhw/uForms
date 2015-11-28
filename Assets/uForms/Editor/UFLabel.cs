using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace uForms.Editor.Control
{
    public class UFLabel : UFControl
    {
        public override void DrawDesign()
        {
            GUILayout.Label(this.Text);
        }
    }
}
