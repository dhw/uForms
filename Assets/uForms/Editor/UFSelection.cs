using System.Collections.Generic;
using uForms.Editor.Control;

namespace uForms
{
    /// <summary></summary>
    public class UFSelection
    {
        public static System.Action<UFControl> OnSelectionChange = null;

        private static UFControl activeControl = null;

        public static UFControl ActiveControl
        {
            get
            {
                return activeControl;
            }
            set
            {
                if(activeControl != value)
                {
                    activeControl = value;
                    if(OnSelectionChange != null)
                    {
                        OnSelectionChange(activeControl);
                    }
                }
            }
        }
    }
}