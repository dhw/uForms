using UnityEditor;
using UnityEngine;

namespace uForms
{
    public class UFContent
    {
        public class Tex
        {
            public static readonly Texture2D Minus = LoadTexture("icons/Toolbar Minus.png");
            public static readonly Texture2D Eye = LoadTexture("icons/ClothInspector.ViewValue.png");
            public static readonly Texture2D Lock = LoadTexture("icons/InspectorLock.png");
            public static readonly Texture2D TriangleRight = LoadTexture("IN foldout");
            public static readonly Texture2D TriangleDown = LoadTexture("IN foldout on");

            public static Texture2D LoadTexture(string internalPath)
            {
                return EditorGUIUtility.Load(internalPath) as Texture2D;
            }

            public static Texture2D FindTexture(string findName)
            {
                return EditorGUIUtility.FindTexture(findName);
            }
        }

        public static readonly GUIContent TreeOpen = new GUIContent(Tex.TriangleDown);
        public static readonly GUIContent TreeClose = new GUIContent(Tex.TriangleRight);
        public static readonly GUIContent VisibleSwitch = new GUIContent(Tex.Eye);
        public static readonly GUIContent LockSwitch = new GUIContent(Tex.Lock);
        public static readonly GUIContent Minus = new GUIContent(Tex.Minus);
    }
}
