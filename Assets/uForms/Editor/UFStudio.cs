using UnityEngine;
using UnityEditor;
using uForms.Editor.View;
using uForms.Editor.Control;

namespace uForms
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
                Repaint();
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

            UFStackPanel sp = new UFStackPanel(project.root);
            sp.direction = StackDirection.Horizontal;
            UFLabel label = new UFLabel(Vector2.zero);
            label.Text = "テストラベル";
            UFButton button = new UFButton(sp);
            button.Text = "テスト";
            sp.Name = "StackPanel";
            label.Name = "Label";
            button.Name = "Button";
            sp.Add(label);
            sp.Add(button);
            project.root.Name = "Window";
            //project.rootWindow.AddChild(sp);

            UFCanvas canvas = new UFCanvas(project.root);
            canvas.Name = "Canvas1";
            project.root.Add(canvas);
            UFButton button2 = new UFButton(canvas);
            button2.Text = "テストA";
            button2.Name = "Button1";
            canvas.Add(button2);
            UFButton button3 = new UFButton(canvas);
            button3.Text = "テストB";
            button3.Name = "Button2";
            canvas.Add(button3);
        }

        public static void Repaint()
        {
            property.Repaint();
            designer.Repaint();
            outline.Repaint();
        }
    }
}