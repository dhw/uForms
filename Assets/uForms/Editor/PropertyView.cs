using UnityEditor;
using UnityEngine;

namespace uForms
{
    public class PropertyView : SingletonWindow<PropertyView>
    {
        void OnGUI()
        {
            if(UFSelection.ActiveControl != null)
            {
                UFSelection.ActiveControl.DrawProperty();
            }
        }
    }
}
