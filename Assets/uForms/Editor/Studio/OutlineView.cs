using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace uForms
{
    public class OutlineView : SingletonWindow<OutlineView>
    {
        List<UFControl> drawList = null;

        GenericMenu menu = new GenericMenu();

        void Awake()
        {
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            menu = new GenericMenu();
            menu.AddItem(new GUIContent("Delete"), false, OnMenuDelete);
            
            var assembly = Assembly.GetAssembly(typeof(UFControl));

            var list = assembly.GetTypes()
                .Where(t => t.BaseType == typeof(UFControl))
                .ToList();

            list.ForEach(t => menu.AddItem(new GUIContent("Add/" + t.Name.Replace("UF", "")), false, OnMenuAdd, t.FullName));
        }

        private void OnMenuDelete()
        {
            if(UFSelection.ActiveControl != null)
            {
                if(UFSelection.ActiveControl.HasParent)
                {
                    UFSelection.ActiveControl.RemoveFromTree();
                }
                else
                {
                    UFProject.Current.Controls.Remove(UFSelection.ActiveControl);
                }
                UFSelection.ActiveControl = null;
            }
        }

        private void OnMenuAdd(object type)
        {
            string typeString = (string)type;
            
            var current = UFSelection.ActiveControl;
            if(current == null)
            {
                if(typeString == "UFCanvas")
                {
                    var canvas = new UFCanvas();
                    canvas.Name = "canvas";
                    canvas.Text = "canvas";
                    UFProject.Current.Controls.Add(canvas);
                }

                return;
            }
            else
            {
                var assembly = Assembly.GetAssembly(typeof(UFControl));
                Type childType = assembly.GetType(typeString);
                if(childType != null)
                {
                    UFControl child = Activator.CreateInstance(childType) as UFControl;
                    child.Name = child.DefaultName;
                    child.Text = child.DefaultName;
                    current.Add(child);
                    UFSelection.ActiveControl = child;
                }
            }
        }
        
        void OnGUI()
        {
            if(Event.current.type == EventType.ContextClick)
            {
                this.menu.ShowAsContext();
            }

            this.drawList = new List<UFControl>();
            UFProject.Current.Controls.ForEach(child => child.GetOutlineDrawListInternal(this.drawList));

            BeginWindows();
            {
                bool anyControlSelected = false;
                for(int i = 0; i < this.drawList.Count; ++i)
                {
                    Rect rect = new Rect(0, (i * 16), this.position.width, 16);

                    string style = (UFSelection.ActiveControl == this.drawList[i]) ? "LODSliderRangeSelected" : "LODSliderText";
                    
                    GUI.Window(i, rect, WindowCallback, "", style);
                    if(Event.current.type == EventType.MouseUp && rect.Contains(Event.current.mousePosition))
                    {
                        UFSelection.ActiveControl = this.drawList[i];
                        anyControlSelected = true;
                    }
                }
                if(Event.current.type == EventType.MouseUp && !anyControlSelected)
                {
                    UFSelection.ActiveControl = null;
                }
            }
            EndWindows();
        }

        void WindowCallback(int id)
        {
            UFControl current = this.drawList[id];
            if(UFSelection.ActiveControl != current)
            {
                GUILayout.Space(-1);
            }

            GUILayout.BeginHorizontal();

            if(UFSelection.ActiveControl != current)
            {
                GUILayout.Space(-3);
            }

            GUILayout.Space(current.Nest * 10);

            if(current.HasChild)
            {
                string text = (current.Foldout ? "\u25BC" : "\u25BA");

                if(GUILayout.Button(text, EditorStyles.label, GUILayout.Width(13)))
                {
                    current.Foldout = !current.Foldout;
                }
            }
            else
            {
                GUILayout.Label(" ", GUILayout.Width(13));
            }

            GUILayout.Label(current.Name);

            GUIContent visibleContent = (current.IsHidden ? UFContent.Minus : UFContent.VisibleSwitch);
            if(GUILayout.Button(visibleContent, EditorStyles.label, GUILayout.Width(20)))
            {
                current.IsHidden = !current.IsHidden;
            }

            GUILayout.EndHorizontal();

            if(GUI.changed)
            {
                UFStudio.RepaintAll();
            }
        }
    }
}