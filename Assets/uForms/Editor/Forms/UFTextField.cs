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
        
        public override void WriteNativeVariableDefinitionCode(CodeBuilder builder)
        {
            builder.WriteLine(string.Format("private string {0}Text = \"{1}\";", this.Name, this.Text));
        }

        public override void WriteNativeCodeByRect(CodeBuilder builder)
        {
            builder.WriteLine(string.Format("this.{0}Text = EditorGUI.TextField(DrawRects.{1}, this.{2}Text);",
                this.Name, this.Name, this.Name));
        }
    }
}
