using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace uForms
{
    public class DesignerView : EditorWindow
    {
        public static void OpenWindow()
        {
            GetWindow<DesignerView>("Designer");
        }
        
        void OnGUI()
        { 
        
        }
    }
}