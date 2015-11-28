using UnityEngine;

namespace uForms.Editor.Control
{
    public class UFButton : UFControl
    {
        public override void DrawDesign()
        {
            GUILayout.Button(this.Text);
        }
    }
}
