﻿using System.Collections.Generic;
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
            menu.AddItem(new GUIContent("Add/Canvas"), false, OnMenuAdd, "Canvas");
            menu.AddItem(new GUIContent("Add/Button"), false, OnMenuAdd, "Button");
            menu.AddItem(new GUIContent("Add/Label"), false, OnMenuAdd, "Label");
            menu.AddItem(new GUIContent("Add/ObjectField"), false, OnMenuAdd, "ObjectField");
            menu.AddItem(new GUIContent("Add/FloatSlider"), false, OnMenuAdd, "FloatSlider");
            menu.AddItem(new GUIContent("Add/Image"), false, OnMenuAdd, "Image");
            menu.AddItem(new GUIContent("Add/TextField"), false, OnMenuAdd, "TextField");
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
                if(typeString == "Canvas")
                {
                    var canvas = new UFCanvas();
                    canvas.Name = "canvas";
                    canvas.Text = "canvas";
                    UFProject.Current.Controls.Add(canvas);
                }

                return;
            }
            switch(typeString)
            {
                case "Canvas":
                    var canvas = new UFCanvas();
                    canvas.Name = "canvas";
                    canvas.Text = "canvas";
                    current.Add(canvas);
                    UFSelection.ActiveControl = canvas;
                    break;
                case "Button":
                    var button = new UFButton();
                    button.Name = "button";
                    button.Text = "button";
                    current.Add(button);
                    UFSelection.ActiveControl = button;
                    break;
                case "Label":
                    var label = new UFLabel();
                    label.Name = "label";
                    label.Text = "label";
                    current.Add(label);
                    UFSelection.ActiveControl = label;
                    break;
                case "ObjectField":
                    var objectField= new UFObjectField();
                    objectField.Name = "objectField";
                    objectField.Text = "objectField";
                    current.Add(objectField);
                    UFSelection.ActiveControl = objectField;
                    break;
                case "FloatSlider":
                    var floatSlider= new UFFloatSlider();
                    floatSlider.Name = "floatSlider";
                    floatSlider.Text = "floatSlider";
                    current.Add(floatSlider);
                    UFSelection.ActiveControl = floatSlider;
                    break;
                case "Image":
                    var image= new UFImage();
                    image.Name = "image";
                    image.Text = "image";
                    current.Add(image);
                    UFSelection.ActiveControl = image;
                    break;
                case "TextField":
                    var text = new UFTextField();
                    text.Name = "text";
                    text.Text = "text";
                    current.Add(text);
                    UFSelection.ActiveControl = text;
                    break;
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