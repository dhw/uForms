using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace uForms.Editor.Control
{
    /// <summary></summary>
    public class UFControl
    {
        private GUIContent name = new GUIContent("");
        private GUIContent text = new GUIContent("");
        
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

        public string Text
        {
            get
            {
                return this.text.text;
            }
            set
            {
                this.text.text = value;
            }
        }

        private bool isHidden = false;
        private bool isLocked = false;

        private bool isEnabled = true;
        private bool foldout = true;

        private UFControl parent = null;
        protected List<UFControl> childList = new List<UFControl>();

        public int TreeCount
        {
            get
            {
                int count = 1;
                ForTree(node => count += node.childList.Count);
                return count;
            }
        }

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

        public bool IsHidden
        {
            get
            {
                return this.isHidden;
            }
            set
            {
                this.isHidden = value;
            }
        }
        
        public bool IsLocked
        {
            get
            {
                return this.isLocked;
            }
            set
            {
                this.isLocked = value;
            }
        }

        public bool Foldout
        {
            get
            {
                return this.foldout;
            }
            set
            {
                this.foldout = value;
            }
        }

        public bool HasChild
        {
            get
            {
                return this.childList.Count > 0;
            }
        }

        public int Nest
        {
            get
            {
                if(this.parent != null)
                {
                    return this.parent.Nest + 1;
                }
                return 0;
            }
        }

        public void AddChild(UFControl child)
        {
            child.parent = this;
            this.childList.Add(child);
        }

        public List<UFControl> GetOutlineDrawList()
        {
            List<UFControl> list = new List<UFControl>(this.TreeCount);
            GetOutlineDrawListInternal(list);
            return list;
        }

        private void GetOutlineDrawListInternal(List<UFControl> list)
        {
            list.Add(this);
            if(this.foldout)
            {
                this.childList.ForEach(child => child.GetOutlineDrawListInternal(list));
            }
        }

        public void ForTree(System.Action<UFControl> action)
        {
            if(action != null)
            {
                action(this);
                this.childList.ForEach(child => child.ForTree(action));
            }
        }
        
        public virtual void DrawDesign()
        {
        }

        Vector2 propertyViewScroll = Vector2.zero;

        public void DrawProperty()
        {
            this.propertyViewScroll = GUILayout.BeginScrollView(this.propertyViewScroll);
            {
                DrawPropertyItem("Name", () => Name = EditorGUILayout.TextField(Name));
                DrawPropertyItem("Text", () => Text = EditorGUILayout.TextField(Text));
                DrawPropertyItem("Enabled", () => IsEnabled = EditorGUILayout.Toggle(IsEnabled));
                this.DrawPropertyOriginal();
            }
            GUILayout.EndScrollView();

        }

        protected virtual void DrawPropertyOriginal()
        {

        }

        protected void DrawPropertyItem(string label, System.Action drawAction)
        {
            GUILayout.BeginHorizontal(EditorStyles.textArea);
            {
                GUILayout.Label(label, GUILayout.Width(100));
                drawAction();
            }
            GUILayout.EndHorizontal();
            GUILayout.Space(-3);
        }
    }
}