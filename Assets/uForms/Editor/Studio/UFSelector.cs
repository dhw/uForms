using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System;
using System.Reflection;

namespace uForms
{
    public class UFSelector : EditorWindow
    {
        List<Type> typeList = new List<Type>();

        System.Action<Type> OnSelected = null;

        public static void OpenWindow(Action<Type> onSelected = null)
        {
            var window = GetWindow<UFSelector>();
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
    }
}