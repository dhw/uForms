using UnityEditor;
using System.Collections.Generic;

namespace uForms
{
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