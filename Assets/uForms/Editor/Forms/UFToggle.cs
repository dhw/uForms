using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using UnityEditor;
using UnityEngine;

namespace uForms
{
    public enum CheckState
    {
        Unchecked,
        Checked,
    }

    public class UFToggle : UFControl
    {
        public override string DefaultName { get { return "toggle"; } }
        public override Vector2 DefaultSize { get { return new Vector2(100, 16); } }
        public override GUIStyle DesignGUIStyle { get { return EditorStyles.toggle; } }

        [XmlIgnore]
        public System.Action<bool> OnCheckStateChanged = null;

        private CheckState checkState = CheckState.Unchecked;
        
        public bool Checked
        {
            get
            {
                return this.checkState == CheckState.Checked;
            }
            set
            {
                if(value && this.checkState == CheckState.Unchecked)
                {
                    this.checkState = CheckState.Checked;
                    if(OnCheckStateChanged != null)
                    {
                        this.OnCheckStateChanged(value);
                    }
                }
                else if(!value && this.checkState == CheckState.Checked)
                {
                    this.checkState = CheckState.Unchecked;
                    if(OnCheckStateChanged != null)
                    {
                        this.OnCheckStateChanged(value);
                    }
                }
            }
        }

        public override void Draw()
        {
            this.Checked = GUILayout.Toggle(this.Checked, this.Text);
        }

        public override void DrawByRect()
        {
            this.Checked = GUI.Toggle(this.DrawRect, this.Checked, this.Text);
        }

        public override void WriteCodeAdditional(CodeBuilder builder)
        {
            builder.WriteLine("this." + this.Name + ".Checked = " + (this.Checked ? "true" : "false") + ";");
        }

        protected override void DrawPropertyOriginal()
        {
            DrawPropertyItem("Checked",
                () => this.Checked = EditorGUILayout.Toggle(this.Checked));
        }

        public override void WriteNativeContentDefinitionCode(CodeBuilder builder)
        {
            builder.WriteLine("public static readonly GUIContent {0} = new GUIContent(\"{1}\");",
                this.Name, this.Text);
        }

        public override void WriteNativeVariableDefinitionCode(CodeBuilder builder)
        {
            builder.WriteLine("private bool {0}Value = {1};", this.Name, (this.Checked ? "true" : "false"));
        }

        public override void WriteNativeCodeByRect(CodeBuilder builder)
        {
            builder.WriteLine("this.{0}Value = GUI.Toggle(DrawRects.{1}, this.{2}Value, DrawContents.{3});",
                this.Name, this.Name, this.Name, this.Name);
        }
    }
}
