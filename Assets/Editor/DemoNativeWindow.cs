using UnityEngine;
using UnityEditor;

namespace DemoNative
{
    public class DemoNativeWindow : EditorWindow
    {
        [MenuItem("Tools/DemoNativeWindow")]
        public static void OpenWindow()
        {
            GetWindow<DemoNativeWindow>("DemoNativeWindow");
        }
        
        void OnGUI()
        {
            GUI.BeginGroup(new Rect(16f, 20f, 250f, 250f));
            {
                GUI.Label(new Rect(36f, 35f, 112.5f, 16f), "オブジェクト生成");
                if(GUI.Button(new Rect(32f, 63f, 178.5f, 42.5f), "Cube"))
                {
                
                }
                EditorGUI.TextField(new Rect(36f, 121f, 80f, 16f), "text");
            }
            GUI.EndGroup();
            GUI.BeginGroup(new Rect(280f, 22f, 250f, 250f));
            {
                EditorGUI.ObjectField(new Rect(34f, 40f, 184.5f, 16f), null, typeof(UnityEngine.Object), true);
                EditorGUI.Slider(new Rect(31f, 97f, 177f, 16f), 1f, 0.1f, 3f);
                GUI.Label(new Rect(27f, 134f, 100f, 100f), AssetDatabase.LoadAssetAtPath<Texture>(AssetDatabase.GUIDToAssetPath("e6f532133ac63bb49a9b65446503128b")));
                GUI.Toggle(new Rect(32f, 15f, 100f, 16f), true, "active");
            }
            GUI.EndGroup();
        }
    }
}
