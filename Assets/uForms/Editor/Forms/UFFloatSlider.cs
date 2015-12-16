using System.Xml.Serialization;
using UnityEditor;
using UnityEngine;

namespace uForms
{
    public class UFFloatSlider : UFControl
    {
        public static readonly Vector2 DefaultSize = new Vector2(100, 16);

        [XmlIgnore]
        public System.Action<float> OnValueChanged = null;

        private float value = 1.0f;

        private float minValue = float.MinValue;
        private float maxValue = float.MaxValue;

        public float MinValue
        {
            get
            {
                return this.minValue;
            }
            set
            {
                this.minValue = value;
            }
        }

        public float MaxValue
        {
            get
            {
                return this.maxValue;
            }
            set
            {
                this.maxValue = value;
            }
        }

        public float Value
        {
            get
            {
                return this.value;
            }
            set
            {
                if(this.value != value)
                {
                    this.value = Mathf.Clamp(value, this.minValue, this.maxValue);
                    if(OnValueChanged != null)
                    {
                        OnValueChanged(this.value);
                    }
                }
            }
        }

        public override void Draw()
        {

        }

        public override void DrawByRect()
        {
            this.Value = EditorGUI.Slider(this.DrawRect, this.Value, this.minValue, this.maxValue);
        }

        public override void DrawDesign()
        {
        }

        public override void DrawDesignByRect()
        {
            if(IsHidden) { return; }
            Rect slider = this.DrawRect;
            slider.width -= 55;
            Rect box = this.DrawRect;
            box.x = box.x + slider.width + 5;
            box.width = 50;
            GUI.Label(slider, "", "PreSlider");
            GUI.Label(slider, "", "PreSliderThumb");
            GUI.Label(box, "", EditorStyles.textField);
        }

        public UFFloatSlider()
        {
            this.DrawRect = new Rect(Vector2.zero, DefaultSize);
        }

        public override void WriteCode(CodeBuilder builder)
        {
            builder.WriteLine("this." + this.Name + " = new UFFloatSlider();");
            base.WriteCode(builder);
            builder.WriteLine("this." + this.Name + ".Value = " + this.value.ToString() + "f;");
            builder.WriteLine("this." + this.Name + ".MaxValue = " + this.maxValue.ToString() + "f;");
            builder.WriteLine("this." + this.Name + ".MinValue = " + this.minValue.ToString() + "f;");
        }

        public override void WriteDefinitionCode(CodeBuilder builder)
        {
            builder.WriteLine("private UFFloatSlider " + this.Name + ";");
        }

        protected override void DrawPropertyOriginal()
        {
            DrawPropertyItem("MinValue", () => this.MinValue = EditorGUILayout.FloatField(this.MinValue));
            DrawPropertyItem("MaxValue", () => this.MaxValue = EditorGUILayout.FloatField(this.MaxValue));
            DrawPropertyItem("Value", () => this.Value = EditorGUILayout.FloatField(this.Value));
        }

    }
}
