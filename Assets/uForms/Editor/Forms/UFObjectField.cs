using System.Xml.Serialization;
using UnityEditor;
using UnityEngine;

namespace uForms
{
    public class UFObjectField : UFControl
    {
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
    }
}
