using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace uForms
{
    /// <summary></summary>
    public class UFControl
    {
        protected GUIContent name = new GUIContent("");
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

        protected UFControl parent = null;
        protected List<UFControl> childList = new List<UFControl>();
        protected Rect rect = new Rect(0,0,100,100);

        public Rect DrawRect { get { return this.rect; } }

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

            int guideNum = 8;
            int margin = 3;
            int size = (margin * 2) + 1;

            float sx = this.rect.x;
            float cx = this.rect.center.x;
            float ex = this.rect.x + this.rect.width;
            float sy = this.rect.y;
            float cy = this.rect.center.y;
            float ey = this.rect.y + this.rect.height;
            
            Rect[] guides = new Rect[guideNum];
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
            guides[0].x = sx - size;
            guides[0].y = sy - size;
            guides[1].x = cx - margin;
            guides[1].y = sy - size;
            guides[2].x = ex;
            guides[2].y = sy - size;
            guides[3].x = ex;
            guides[3].y = cy - margin;
            guides[4].x = ex;
            guides[4].y = ey;
            guides[5].x = cx - margin;
            guides[5].y = ey;
            guides[6].x = sx - size;
            guides[6].y = ey;
            guides[7].x = sx - size;
            guides[7].y = cy - margin;
            guides[0].width = size;
            guides[0].height = size;
            guides[1].width = size;
            guides[1].height = size;
            guides[2].width = size;
            guides[2].height = size;
            guides[3].width = size;
            guides[3].height = size;
            guides[4].width = size;
            guides[4].height = size;
            guides[5].width = size;
            guides[5].height = size;
            guides[6].width = size;
            guides[6].height = size;
            guides[7].width = size;
            guides[7].height = size;

            for(int i = 0; i < guideNum; ++i)
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