using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace uForms
{
    public class OutlineView : EditorWindow
    {
        public static OutlineView OpenWindow()
        {
            var window = GetWindow<OutlineView>("Outline");
            window.minSize = new Vector2(300, 200);
            return window;
        }

        List<UFControl> drawList = null;

        GenericMenu menu = new GenericMenu();

        void Awake()
        {
            menu.AddItem(new GUIContent("Delete"), false, OnMenuDelete);
            menu.AddItem(new GUIContent("Add/Button"), false, OnMenuAdd, "Button");
            menu.AddItem(new GUIContent("Add/Label"), false, OnMenuAdd, "Label");
        }

        private void OnMenuDelete()
        {
            if(UFSelection.ActiveControl != null)
            {
                Debug.Log("delete : " + UFSelection.ActiveControl.Name);
                UFSelection.ActiveControl.RemoveFromTree();
                UFSelection.ActiveControl = null;
            }
        }

        private void OnMenuAdd(object type)
        {
            var current = UFSelection.ActiveControl;
            if(current == null) { return; }
            string typeString = (string)type;
            switch(typeString)
            {
                case "Button":
                    var button = new UFButton(current);
                    button.Name = "HogeButton";
                    button.Text = "Hoge";
                    current.Add(button);
                    break;
                case "Label":
                    var label = new UFLabel(Vector2.zero);
                    label.Name = "HogeButton";
                    label.Text = "Hoge";
                    current.Add(label);
                    break;
            }
        }

        void OnEnable()
        {
            UFStudio.Initialize();
        }
        
        void OnGUI()
        {
            if(Event.current.type == EventType.ContextClick)
            {
                this.menu.ShowAsContext();
            }

            this.drawList = new List<UFControl>();
            UFStudio.project.Controls.ForEach(child => child.GetOutlineDrawListInternal(this.drawList));

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

            GUIContent lockContent = (current.IsLocked ? UFContent.LockSwitch : UFContent.Minus);
            if(GUILayout.Button(lockContent, EditorStyles.label, GUILayout.Width(20)))
            {
                current.IsLocked = !current.IsLocked;
            }

            GUILayout.EndHorizontal();
            //GUI.DragWindow();
            if(GUI.changed)
            {
                UFStudio.RepaintAll();
            }
        }
    }
}