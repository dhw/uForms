using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Reflection;

namespace uForms
{
    public class UFSelector : EditorWindow
    {
        List<Type> typeList = new List<Type>();

        Vector2 scrollPosition = Vector2.zero;

        System.Action<Type> OnSelected = null;

        public static void OpenWindow(Action<Type> onSelected = null)
        {
            var window = GetWindow<UFSelector>("UFSelector");
            window.OnSelected = onSelected;
        }

        void Awake()
        {
            this.typeList.AddRange(
                Assembly.GetAssembly(typeof(UFWindow))
                .GetTypes()
                .Where(t => t.BaseType == typeof(UFWindow)));
        }

        void OnGUI()
        {
            this.scrollPosition = GUILayout.BeginScrollView(this.scrollPosition);
            {
                this.typeList.ForEach(t =>
                {
                    if(GUILayout.Button(t.Name))
                    {
                        if(OnSelected != null)
                        {
                            this.OnSelected(t);
                            this.Close();
                        }
                    }
                });
            }
            GUILayout.EndScrollView();
        }
    }
}