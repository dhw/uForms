using UnityEditor;

namespace uForms.Editor.View
{
    public class PropertyView : EditorWindow
    {
        public static PropertyView OpenWindow()
        {
            return GetWindow<PropertyView>("Property");
        }
        
        void OnGUI()
        {
            if(UFSelection.ActiveControl != null)
            {
                UFSelection.ActiveControl.DrawProperty();
            }
        }
    }
}
