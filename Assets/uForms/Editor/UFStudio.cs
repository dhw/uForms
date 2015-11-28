using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using uForms.Editor.View;
using uForms.Editor.Control;

namespace uForms.Editor
{
    /// <summary></summary>
    public class UFStudio
    {
        private static DesignerView designer = null;

        private static OutlineView outline = null;

        private static PropertyView property = null;

        public static UFProject project { get; private set; }
        
        [MenuItem("Window/uForms Studio")]
        public static void OpenStudio()
        {
            designer = DesignerView.OpenWindow();
            outline = OutlineView.OpenWindow();
            property = PropertyView.OpenWindow();

            UFSelection.OnSelectionChange += (ctrl) =>
            {
                property.Repaint();
                designer.Repaint();
                outline.Repaint();
            };

            var pRect = property.position;
            pRect.x = pRect.y = 100;
            property.position = pRect;

            var oRect = outline.position;
            oRect.x = pRect.x + pRect.width;
            oRect.y = pRect.y;
            outline.position = oRect;

            var dRect = designer.position;
            dRect.x = oRect.x + oRect.width;
            dRect.y = oRect.y;
            designer.position = dRect;

            // テストプロジェクト
            project = new UFProject();

            UFStackPanel sp = new UFStackPanel();
            sp.direction = StackDirection.Horizontal;
            UFLabel label = new UFLabel();
            label.Text = "テストラベル";
            UFButton button = new UFButton();
            button.Text = "テスト";
            sp.Name = "StackPanel";
            label.Name = "Label";
            button.Name = "Button";
            sp.AddChild(label);
            sp.AddChild(button);
            project.rootWindow.Name = "Window";
            project.rootWindow.AddChild(sp);
        }
    }
}