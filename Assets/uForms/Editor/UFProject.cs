using UnityEngine;
using uForms.Editor.Control;

namespace uForms
{
    /// <summary></summary>
    public class UFProject
    {
        public string nameSpace = "sample";
        public string className = "SampleEditor";
        public UFRoot root = new UFRoot(new Vector2(400,400));
    }
}