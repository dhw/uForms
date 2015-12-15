using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.IO;
using UnityEditor;
using System.Text;

namespace uForms
{
    /// <summary></summary>
    public class UFProject
    {
        private static UFProject current = new UFProject();

        public static UFProject Current { get { return current; } }

        public string Namespace = "Form1";

        public string ClassName = "Form1";

        public List<UFControl> Controls = new List<UFControl>();

        public static void CreateNewProject()
        {
            current = new UFProject();
            UFCanvas canvas1 = new UFCanvas();
            canvas1.Text = "canvas1";
            canvas1.Name = "canvas1";
            current.Controls.Add(canvas1);
        }

        public static void ImportXml(string xmlPath)
        {
            var assembly = Assembly.GetAssembly(typeof(UFControl));

            var list = assembly.GetTypes()
                .Where(t => t.BaseType == typeof(UFControl))
                .ToList();
            list.Add(typeof(UFControl));
            list = list.Distinct().ToList();

            var attributes = new XmlAttributes();
            list.ForEach(t => attributes.XmlArrayItems.Add(new XmlArrayItemAttribute(t)));

            var attrOverride = new XmlAttributeOverrides();
            attrOverride.Add(typeof(UFControl), "childList", attributes);
            attrOverride.Add(typeof(UFProject), "Controls", attributes);

            current = UFUtility.ImportXml<UFProject>(xmlPath, attrOverride: attrOverride);
            current.Controls.ForEach(child => child.RefleshHierarchy());
        }

        public static void ImportCode(Type t)
        {
            if(t.BaseType != typeof(UFWindow))
            {
                throw new Exception("Specified type is not derived from 'UFWindow'! - " + t.Name);
            }

            current = new UFProject();
            current.Namespace = t.Namespace;
            current.ClassName = t.Name;

            var window = EditorWindow.CreateInstance(t) as UFWindow;
            current.Controls.AddRange(window.Controls);
            current.Controls.ForEach(child => child.RefleshHierarchy());
            EditorWindow.DestroyImmediate(window);
        }

        public static void ExportXml(string xmlPath)
        {
            List<Type> list = new List<Type>();
            current.Controls.ForEach(child => child.ForTree(node =>
            {
                list.Add(node.GetType());
            }));
            list.Add(typeof(UFControl));
            list = list.Distinct().ToList();

            var attributes = new XmlAttributes();
            list.ForEach(t => attributes.XmlArrayItems.Add(new XmlArrayItemAttribute(t)));

            var attrOverride = new XmlAttributeOverrides();
            attrOverride.Add(typeof(UFControl), "childList", attributes);
            attrOverride.Add(typeof(UFProject), "Controls", attributes);

            UFUtility.ExportXml(xmlPath, current, attrOverride: attrOverride);
        }

        public static void ExportCode(string codePath)
        {
            string designerCodePath = codePath.Replace(".cs",".Designer.cs");

            // export the designer code always.
            {
                #region export the designer code

                CodeBuilder cb = new CodeBuilder();
                cb.WriteLine("using uForms;");
                cb.WriteLine("using UnityEngine;");
                cb.WriteLine("");
                cb.WriteLine("namespace " + current.Namespace);
                cb.WriteLine("{");
                cb.Indent++;
                cb.WriteLine("partial class " + current.ClassName);
                cb.WriteLine("{");
                cb.Indent++;
                cb.WriteLine("#region Auto generated code from uForms.");
                cb.WriteLine("");
                cb.WriteLine("private void InitializeComponent()");
                cb.WriteLine("{");
                cb.Indent++;
                current.Controls.ForEach(child =>
                {
                    cb.WriteLine("// " + child.Name);
                    child.WriteCode(cb);
                    cb.WriteLine("this.Controls.Add(this." + child.Name + ");");
                });
                cb.Indent--;
                cb.WriteLine("}");
                cb.WriteLine("");
                cb.WriteLine("#endregion");
                cb.WriteLine("");
                current.Controls.ForEach(child => child.ForTree(node => node.WriteDefinitionCode(cb)));
                cb.Indent--;
                cb.WriteLine("}");
                cb.Indent--;
                cb.WriteLine("}");
                File.WriteAllText(designerCodePath, cb.GetCode(), new UTF8Encoding(true));

                #endregion
            }

            // export the main code if it doesn't exist.
            if(!File.Exists(codePath))
            {
                #region export the main code

                CodeBuilder cb = new CodeBuilder();
                cb.WriteLine("using uForms;");
                cb.WriteLine("using UnityEngine;");
                cb.WriteLine("using UnityEditor;");
                cb.WriteLine("");
                cb.WriteLine("namespace " + current.Namespace);
                cb.WriteLine("{");
                cb.Indent++;
                cb.WriteLine("public partial class " + current.ClassName + " : UFWindow");
                cb.WriteLine("{");
                cb.Indent++;
                cb.WriteLine("[MenuItem(\"Tools/" + current.ClassName + "\")]");
                cb.WriteLine("public static void OpenWindow()");
                cb.WriteLine("{");
                cb.Indent++;
                cb.WriteLine("GetWindow<" + current.ClassName + ">();");
                cb.Indent--;
                cb.WriteLine("}");
                cb.WriteLine("");
                cb.WriteLine("void Awake()");
                cb.WriteLine("{");
                cb.Indent++;
                cb.WriteLine("InitializeComponent();");
                cb.Indent--;
                cb.WriteLine("}");
                cb.Indent--;
                cb.WriteLine("}");
                cb.Indent--;
                cb.WriteLine("}");
                File.WriteAllText(codePath, cb.GetCode(), new UTF8Encoding(true));

                #endregion
            }

            if(codePath.Contains("Assets/"))
            {
                codePath = codePath.Substring(codePath.IndexOf("Assets/"));
                AssetDatabase.ImportAsset(codePath);
            }
            if(designerCodePath.Contains("Assets/"))
            {
                designerCodePath = designerCodePath.Substring(designerCodePath.IndexOf("Assets/"));
                AssetDatabase.ImportAsset(designerCodePath);
            }
        }
    }
}