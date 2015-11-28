using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace uForms.Editor.Control
{
    public enum StackDirection
    {
        Vertical,
        Horizontal
    }

    public class UFStackPanel : UFControl
    {
        public StackDirection direction = StackDirection.Vertical;

        public override void DrawDesign()
        {
            if(direction == StackDirection.Horizontal)
            {
                GUILayout.BeginHorizontal();
                {
                    this.childList.ForEach(child => child.DrawDesign());
                }
                GUILayout.EndHorizontal();
            }
            else
            {
                GUILayout.BeginVertical();
                {
                    this.childList.ForEach(child => child.DrawDesign());
                }
                GUILayout.EndHorizontal();
            }
        }

        protected override void DrawPropertyOriginal()
        {
            DrawPropertyItem("StackDirection", () =>
            {
                this.direction = (StackDirection)EditorGUILayout.EnumPopup(this.direction);
            });
        }
    }
}
