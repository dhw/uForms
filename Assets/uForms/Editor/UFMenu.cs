using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using uForms.Editor.View;

namespace uForms.Editor
{
    /// <summary></summary>
    public class uFormsMenu
    {
        [MenuItem("Window/uForms")]
        public static void Show()
        {
            OutlineView.OpenWindow();
            PropertyView.OpenWindow();
            DesignerView.OpenWindow();
        }
    }
}