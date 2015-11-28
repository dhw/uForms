using SyntaxTree.VisualStudio.Unity.Bridge;
using System;
using System.Linq;
using System.Xml.Linq;
using UnityEditor;

namespace Editor
{
    /// <summary>Hook VSTU's generation.</summary>
    [InitializeOnLoad]
    public static class VSTUHook
    {
        static VSTUHook()
        {
            ProjectFilesGenerator.ProjectFileGeneration += Hook;
        }

        readonly static string[] ExcludeTargets =
        {
            "Boo.Lang",
            "UnityEditor.BB10.Extensions",
            "UnityEditor.LinuxStandalone.Extensions",
            "UnityEditor.Metro.Extensions",
            "UnityEditor.OSXStandalone.Extensions",
            "UnityEditor.SamsungTV.Extensions",
            "UnityEditor.Tizen.Extensions",
            "UnityEditor.WebGL.Extensions",
            "UnityEditor.WP8.Extensions",
            "UnityScript.Lang",
        };
        
        static string Hook(string filename, string content)
        {
            var document = XDocument.Parse(content);
            
            document.Root
                .Descendants()
                .Where(x => x.Name.LocalName == "Reference")
                .Where(x => ExcludeTargets.Any(target => target == (string)x.Attribute("Include")))
                .Remove();

            return document.Declaration + Environment.NewLine + document.Root;
        }
    }
}