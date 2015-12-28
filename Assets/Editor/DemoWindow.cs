using uForms;
using UnityEngine;
using UnityEditor;

namespace Demo
{
    public partial class DemoWindow : UFWindow
    {
        [MenuItem("Tools/DemoWindow")]
        public static void OpenWindow()
        {
            GetWindow<DemoWindow>("DemoWindow");
        }
        
        void Awake()
        {
            InitializeComponent();

            this.button.OnClick += () =>
            {
                this.objectField.Target = GameObject.CreatePrimitive(PrimitiveType.Cube);
                this.toggle.Checked = true;
            };

            this.floatSlider.OnValueChanged += (value) =>
            {
                if(this.objectField.Target != null)
                {
                    var go = this.objectField.Target as GameObject;
                    go.transform.localScale = Vector3.one * value;
                }
            };

            this.text.OnTextChanged += (text) =>
            {
                this.label.Text = text;
            };

            this.toggle.OnCheckStateChanged += (check) =>
            {
                if(this.objectField.Target != null)
                {
                    var go = this.objectField.Target as GameObject;
                    go.SetActive(check);
                }
                this.toggle.Text = (check ? "active" : "inactive");
            };
        }
    }
}
