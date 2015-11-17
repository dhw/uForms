using System.Collections.Generic;
using UnityEngine;

namespace uForms.Editor.Control
{
    /// <summary></summary>
    public class UFControl
    {
        public static readonly GUIContent Hidden = new GUIContent("-");
        public static readonly GUIContent NotHidden= new GUIContent("○");
        public static readonly GUIContent NotLocked = new GUIContent("-");
        public static readonly GUIContent Locked= new GUIContent("○");

        private List<UFControl> childList = new List<UFControl>();

        private GUIContent name = new GUIContent("");

        public string Name
        {
            get
            {
                return this.name.text;
            }
            set
            {
                this.name.text = value;
            }
        }

        private bool isHidden = false;
        private bool isLocked = false;

        private bool isEnabled = true;

        public bool IsEnabled
        {
            get
            {
                return this.isEnabled;
            }
            set
            {
                this.isEnabled = value;
            }
        }

        public void AddChild(UFControl child)
        {
            this.childList.Add(child);
        }

        public void DrawOutlineTree(int nest = 0)
        {
            GUILayout.BeginHorizontal();
            {
                GUILayout.Space(16 * nest);
                GUILayout.Label(this.name, GUILayout.Width(200 - nest * 16));
                if(GUILayout.Button(this.isHidden ? Hidden : NotHidden, GUILayout.Width(16)))
                {
                    this.isHidden = !this.isHidden;
                }

                if(GUILayout.Button(this.isLocked ? Locked : NotLocked, GUILayout.Width(16)))
                {
                    this.isLocked = !this.isLocked;
                }
            }
            GUILayout.EndHorizontal();

            childList.ForEach(child => child.DrawOutlineTree(nest + 1));
        }

    }
}