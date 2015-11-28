using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace uForms.Editor.View
{
    public class DesignerView : EditorWindow
    {
        public static DesignerView OpenWindow()
        {
            return GetWindow<DesignerView>("Designer");
        }
        
        void OnGUI()
        {
            UFStudio.project.rootWindow.DrawDesign();
        }
    }
}