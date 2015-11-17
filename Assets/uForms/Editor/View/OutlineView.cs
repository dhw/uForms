using uForms.Editor.Control;
using UnityEditor;

namespace uForms.Editor.View
{
    public class OutlineView : EditorWindow
    {
        public static void OpenWindow()
        {
            GetWindow<OutlineView>("Outline");
        }

        UFControl root = new UFControl();

        void Awake()
        {
            root.Name = "Window";
            var sp = new UFStackPanel() {Name = "StackPanel" };
            var label = new UFLabel() {Name = "Label" };
            var button = new UFButton() {Name = "Button" };
            root.AddChild(sp);
            sp.AddChild(label);
            sp.AddChild(button);

        }
        
        void OnGUI()
        {

            root.DrawOutlineTree();
        }
    }
}