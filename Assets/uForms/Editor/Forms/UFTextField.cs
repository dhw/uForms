using UnityEditor;
using UnityEngine;

namespace uForms
{
    public class UFTextField : UFControl
    {
        public static readonly Vector2 DefaultSize = new Vector2(80, 16);
        
        public override void Draw()
        {
            this.Text = EditorGUILayout.TextField(this.Text);
        }

        public override void DrawByRect()
        {
            this.Text = EditorGUI.TextField(this.DrawRect, this.Text);
        }

        public override void DrawDesign()
        {
            GUILayout.Label(this.text, EditorStyles.textField);
        }

        public override void DrawDesignByRect()
        {
            GUI.Label(this.DrawRect, this.text, EditorStyles.textField);
        }

        public UFTextField()
        {
            this.DrawRect = new Rect(Vector2.zero, DefaultSize);
        }
        
        public override void WriteCode(CodeBuilder builder)
        {
            builder.WriteLine("this." + this.Name + " = new UFTextField();");
            base.WriteCode(builder);
        }

        public override void WriteDefinitionCode(CodeBuilder builder)
        {
            builder.WriteLine("private UFTextField " + this.Name + ";");
        }
    }
}
