using UnityEditor;
using UnityEngine;

namespace uForms
{
    public class UFTextField : UFControl
    {
        public override string DefaultName { get { return "text"; } }
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

        public override void WriteNativeCodeByRect(CodeBuilder builder)
        {
            builder.WriteLine(string.Format("EditorGUI.TextField(new Rect({0}f, {1}f, {2}f, {3}f), {4});",
                this.DrawRect.x, this.DrawRect.y, this.DrawRect.width, this.DrawRect.height,
                "\"" + this.Text + "\""));
        }
    }
}
