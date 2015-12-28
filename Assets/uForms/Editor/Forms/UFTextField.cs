using UnityEditor;
using UnityEngine;

namespace uForms
{
    public class UFTextField : UFControl
    {
        public override Vector2 DefaultSize { get { return new Vector2(80, 16); } }
        public override GUIStyle DesignGUIStyle { get { return EditorStyles.textField; } }

        public override void Draw()
        {
            this.Text = EditorGUILayout.TextField(this.Text);
        }

        public override void DrawByRect()
        {
            this.Text = EditorGUI.TextField(this.DrawRect, this.Text);
        }
    }
}
