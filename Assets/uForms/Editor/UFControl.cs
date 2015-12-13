using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEditor;
using UnityEngine;

namespace uForms
{
    /// <summary></summary>
    public class UFControl
    {
        const int GuideNum = 8;
        const int GuideMargin = 3;
        const int GuideSize = (GuideMargin * 2) + 1;

        [XmlIgnore]
        protected GUIContent name = new GUIContent("");

        [XmlIgnore]
        protected GUIContent text = new GUIContent("");

        public UFControl()
        {

        }

        public UFControl(Rect rect)
        {
            this.rect = rect;
        }

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

        [XmlIgnore]
        protected UFControl parent = null;

        public List<UFControl> childList = new List<UFControl>();
        [XmlIgnore]
        protected Rect rect = new Rect(0,0,100,100);
        
        public Rect DrawRect { get { return this.rect; } }

        public Rect DrawRectWithGuide
        {
            get
            {
                return new Rect(this.rect.x - GuideSize, this.rect.y - GuideSize, this.rect.width + (GuideSize * 2), this.rect.height + (GuideSize * 2));
            }
        }

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

        public void Add(UFControl child)
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
        public void ForTreeFromChild(System.Action<UFControl> action)
        {
            if(action != null)
            {
                for(int i = this.childList.Count - 1; i >= 0; --i)
                {
                    this.childList[i].ForTreeFromChild(action);
                }
                action(this);
            }
        }

        public virtual void Draw()
        {

        }

        public virtual void DrawByRect()
        {

        }

        public virtual void DrawDesign()
        {
        }

        public virtual void DrawDesignByRect()
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
            if(GUI.changed)
            {
                UFStudio.Repaint();
            }
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

        public void DrawGuide()
        {
            Rect prev = this.rect;
            this.rect = GUI.Window(-1, this.rect, DrawGuideRect, "", "grey_border");


            float sx = this.rect.x;
            float cx = this.rect.center.x;
            float ex = this.rect.x + this.rect.width;
            float sy = this.rect.y;
            float cy = this.rect.center.y;
            float ey = this.rect.y + this.rect.height;
            
            Rect[] guides = new Rect[GuideNum];
            /*
            guides[0].x = sx - margin;
            guides[0].y = sy - margin;
            guides[1].x = cx - margin;
            guides[1].y = sy - margin;
            guides[2].x = ex - margin;
            guides[2].y = sy - margin;
            guides[3].x = ex - margin;
            guides[3].y = cy - margin;
            guides[4].x = ex - margin;
            guides[4].y = ey - margin;
            guides[5].x = cx - margin;
            guides[5].y = ey - margin;
            guides[6].x = sx - margin;
            guides[6].y = ey - margin;
            guides[7].x = sx - margin;
            guides[7].y = cy - margin;
            */
            guides[0].x = sx - GuideSize;
            guides[0].y = sy - GuideSize;
            guides[1].x = cx - GuideMargin;
            guides[1].y = sy - GuideSize;
            guides[2].x = ex;
            guides[2].y = sy - GuideSize;
            guides[3].x = ex;
            guides[3].y = cy - GuideMargin;
            guides[4].x = ex;
            guides[4].y = ey;
            guides[5].x = cx - GuideMargin;
            guides[5].y = ey;
            guides[6].x = sx - GuideSize;
            guides[6].y = ey;
            guides[7].x = sx - GuideSize;
            guides[7].y = cy - GuideMargin;
            guides[0].width = GuideSize;
            guides[0].height = GuideSize;
            guides[1].width = GuideSize;
            guides[1].height = GuideSize;
            guides[2].width = GuideSize;
            guides[2].height = GuideSize;
            guides[3].width = GuideSize;
            guides[3].height = GuideSize;
            guides[4].width = GuideSize;
            guides[4].height = GuideSize;
            guides[5].width = GuideSize;
            guides[5].height = GuideSize;
            guides[6].width = GuideSize;
            guides[6].height = GuideSize;
            guides[7].width = GuideSize;
            guides[7].height = GuideSize;

            for(int i = 0; i < GuideNum; ++i)
            {
                Rect guide = guides[i];
                Rect result = GUI.Window(i, guide, DrawGuideRect, "", "LODSliderRangeSelected");
                if(guide.center.x != result.center.x && guide.center.y != result.center.y)
                {
                    float rx = result.center.x;
                    float ry = result.center.y;
                    switch(i)
                    {
                        case 0:
                            this.rect.width += sx - rx;
                            this.rect.height += sy - ry;
                            this.rect.x = rx;
                            this.rect.y = ry;
                            break;
                        case 1:
                            this.rect.height += sy - ry;
                            this.rect.y = ry;
                            break;
                        case 2:
                            this.rect.width += rx - ex;
                            this.rect.height += sy - ry;
                            this.rect.y = ry;
                            break;
                        case 3:
                            this.rect.width += rx - ex;
                            break;
                        case 4:
                            this.rect.width += rx - ex;
                            this.rect.height += ry - ey;
                            break;
                        case 5:
                            this.rect.height += ry - ey;
                            break;
                        case 6:
                            this.rect.width += sx - rx;
                            this.rect.height += ry - ey;
                            this.rect.x = rx;
                            break;
                        case 7:
                            this.rect.width += sx - rx;
                            this.rect.x = rx;
                            break;
                    }
                }
            }
            Vector2 delta = this.rect.position - prev.position;

            this.childList.ForEach(child => child.ForTree(node => node.Move(delta)));
        }

        private void DrawGuideRect(int id)
        {
            GUI.DragWindow();
        }

        private void Move(Vector2 delta)
        {
            this.rect.position += delta;
        }

        public virtual void WriteCode(CodeBuilder builder)
        {
            builder.WriteLine("this." + this.Name + ".Text = \"" + this.Text + "\";");
            builder.WriteLine("this." + this.Name + ".Name = \"" + this.Name + "\";");
            builder.WriteLine("this." + this.Name + ".IsEnabled = " + (this.IsEnabled ? "true" : "false") + ";");
            builder.WriteLine("this." + this.Name + ".IsHidden = " + (this.IsHidden ? "true" : "false") + ";");
        }
        
        public virtual void WriteDefinitionCode(CodeBuilder builder)
        {
            throw new System.Exception("Please override code!");
        }
    }
}