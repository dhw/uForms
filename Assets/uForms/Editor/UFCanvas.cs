using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using uForms.Editor.Control;

namespace uForms.Editor
{
    /// <summary></summary>
    public class UFCanvas : UFControl
    {
        public static readonly Rect DefaultRect = new Rect(0,0,100,100);

        public static readonly Vector2 DefaultSize = new Vector2(100, 100);

        public UFCanvas(UFControl parent)
        {
            this.rect = new Rect(parent.DrawRect.position, DefaultSize);
        }

        public override void DrawDesign()
        {
            if(IsHidden) { return; }
            GUI.Label(this.rect, "", "GroupBox");
            this.childList.ForEach(child => child.DrawByRect());
        }
    }
}