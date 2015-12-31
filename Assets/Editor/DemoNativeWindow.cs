using UnityEngine;
using UnityEditor;

namespace DemoNative
{
    public class DemoNativeWindow : EditorWindow
    {
        public class DrawRects
        {
            public static readonly Rect canvas1 = new Rect(16f, 20f, 250f, 250f);
            public static readonly Rect label = new Rect(36f, 35f, 112.5f, 16f);
            public static readonly Rect button = new Rect(32f, 63f, 178.5f, 42.5f);
            public static readonly Rect text = new Rect(36f, 121f, 80f, 16f);
            public static readonly Rect canvas2 = new Rect(280f, 22f, 250f, 250f);
            public static readonly Rect objectField = new Rect(34f, 40f, 184.5f, 16f);
            public static readonly Rect floatSlider = new Rect(31f, 97f, 177f, 16f);
            public static readonly Rect image = new Rect(27f, 134f, 100f, 100f);
            public static readonly Rect toggle = new Rect(32f, 15f, 100f, 16f);
        }
        
        public class DrawContents
        {
            public static readonly GUIContent label = new GUIContent("オブジェクト生成");
            public static readonly GUIContent button = new GUIContent("Cube");
            public static readonly GUIContent image = new GUIContent(AssetDatabase.LoadAssetAtPath<Texture>(AssetDatabase.GUIDToAssetPath("e6f532133ac63bb49a9b65446503128b")));
            public static readonly GUIContent toggle = new GUIContent("active");
        }
        
        #region Constants
        
        public const float floatSliderMin = 0.1f;
        public const float floatSliderMax = 3f;
        
        #endregion Constants
        
        #region Variables
        
        private string textText = "text";
        private UnityEngine.Object objectFieldObject = null;
        private float floatSliderValue = 1f;
        private bool toggleValue = true;
        
        #endregion Variables
        
        [MenuItem("Tools/DemoNativeWindow")]
        public static void OpenWindow()
        {
            GetWindow<DemoNativeWindow>("DemoNativeWindow");
        }
        
        void OnGUI()
        {
            GUI.BeginGroup(DrawRects.canvas1);
            {
                GUI.Label(DrawRects.label, DrawContents.label);
                if(GUI.Button(DrawRects.button, DrawContents.button))
                {
                
                }
                this.textText = EditorGUI.TextField(DrawRects.text, this.textText);
            }
            GUI.EndGroup();
            GUI.BeginGroup(DrawRects.canvas2);
            {
                this.objectFieldObject = EditorGUI.ObjectField(DrawRects.objectField, this.objectFieldObject, typeof(UnityEngine.Object), true);
                this.floatSliderValue = EditorGUI.Slider(DrawRects.floatSlider, this.floatSliderValue, floatSliderMin, floatSliderMax);
                GUI.Label(DrawRects.image, DrawContents.image);
                this.toggleValue = GUI.Toggle(DrawRects.toggle, this.toggleValue, DrawContents.toggle);
            }
            GUI.EndGroup();
        }
    }
}
