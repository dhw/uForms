using UnityEditor;
using System.Collections.Generic;

namespace uForms
{
    /// <summary></summary>
    public class UFWindow : EditorWindow
    {
        private List<UFControl> controls = new List<UFControl>();

        public List<UFControl> Controls
        {
            get
            {
                return this.controls;
            }
        }

        /// <summary>ウィンドウの描画処理</summary>
        private void OnGUI()
        {
            if(this.controls == null) { return; }

            PreOnGUI();

            this.controls.ForEach(control => control.Draw());

            PostOnGUI();
        }

        protected virtual void PreOnGUI()
        {

        }

        protected virtual void PostOnGUI()
        {

        }
    }
}