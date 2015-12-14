using System.Xml.Serialization;
using UnityEditor;
using UnityEngine;

namespace uForms
{
    public class UFObjectField : UFControl
    {
        public static readonly Vector2 DefaultSize = new Vector2(100, 16);

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

        }

        public override void DrawByRect()
        {
            this.Target = EditorGUI.ObjectField(this.DrawRect, this.target, typeof(UnityEngine.Object), this.allowSceneObject);
        }

        public override void DrawDesign()
        {
        }

        public override void DrawDesignByRect()
        {
            if(IsHidden) { return; }
            GUI.Label(this.DrawRect, "", EditorStyles.objectField);
        }

        public UFObjectField()
        {
            this.DrawRect = new Rect(Vector2.zero, DefaultSize);
        }

        public override void WriteCode(CodeBuilder builder)
        {
            builder.WriteLine("this." + this.Name + " = new UFObjectField();");
            base.WriteCode(builder);
            builder.WriteLine("this." + this.Name + ".AllowSceneObject = " + (this.allowSceneObject ? "true" : "false") + ";");
        }

        public override void WriteDefinitionCode(CodeBuilder builder)
        {
            builder.WriteLine("private UFObjectField " + this.Name + ";");
        }

        protected override void DrawPropertyOriginal()
        {
            DrawPropertyItem("AllowSceneObject", 
                () => this.allowSceneObject = EditorGUILayout.Toggle(this.allowSceneObject));
        }
    }
}
