using System.Xml.Serialization;
using UnityEditor;
using UnityEngine;

namespace uForms
{
    public class UFObjectField : UFControl
    {
        public override string DefaultName { get { return "objectField"; } }
        public override Vector2 DefaultSize { get { return new Vector2(100, 16); } }
        public override GUIStyle DesignGUIStyle { get { return EditorStyles.objectField; } }

        [XmlIgnore]
        public System.Action<UnityEngine.Object> OnTargetChanged = null;

        private UnityEngine.Object target = null;
        private bool allowSceneObject = true;

        [XmlIgnore]
        public UnityEngine.Object Target
        {
            get
            {
                return this.target;
            }
            set
            {
                if(this.target != value)
                {
                    this.target = value;
                    if(OnTargetChanged != null)
                    {
                        this.OnTargetChanged(this.target);
                    }
                }
            }
        }

        public bool AllowSceneObject
        {
            get
            {
                return this.allowSceneObject;
            }
            set
            {
                this.allowSceneObject = value;
            }
        }

        public override void Draw()
        {
            this.Target = EditorGUILayout.ObjectField(this.Target, typeof(UnityEngine.Object), this.allowSceneObject);
        }

        public override void DrawByRect()
        {
            this.Target = EditorGUI.ObjectField(this.DrawRect, this.target, typeof(UnityEngine.Object), this.allowSceneObject);
        }
        
        public override void WriteCodeAdditional(CodeBuilder builder)
        {
            builder.WriteLine("this." + this.Name + ".AllowSceneObject = " + (this.allowSceneObject ? "true" : "false") + ";");
        }

        protected override void DrawPropertyOriginal()
        {
            DrawPropertyItem("AllowSceneObject",
                () => this.allowSceneObject = EditorGUILayout.Toggle(this.allowSceneObject));
        }

        public override void WriteNativeVariableDefinitionCode(CodeBuilder builder)
        {
            builder.WriteLine(string.Format("private UnityEngine.Object {0}Object = null;", this.Name));
        }

        public override void WriteNativeCodeByRect(CodeBuilder builder)
        {
            builder.WriteLine(string.Format("this.{0}Object = EditorGUI.ObjectField(DrawRects.{1}, this.{2}Object, typeof(UnityEngine.Object), {3});",
                this.Name, this.Name, this.Name, (this.allowSceneObject ? "true" : "false")));
        }
    }
}
