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
        public string nameSpace = "sample";
        public string className = "SampleEditor";
        public List<UFControl> Controls = new List<UFControl>();
        //public UFRoot root = new UFRoot(new Vector2(400,400));

        public void ExportCode(string codePath)
        {
            string designerCodePath = codePath.Replace(".cs",".Designer.cs");

            // export the designer code always.
            {
                #region export the designer code

                CodeBuilder cb = new CodeBuilder();
                cb.WriteLine("using uForms;");
                cb.WriteLine("using UnityEngine;");
                cb.WriteLine("");
                cb.WriteLine("namespace " + this.nameSpace);
                cb.WriteLine("{");
                cb.Indent++;
                cb.WriteLine("partial class " + this.className);
                cb.WriteLine("{");
                cb.Indent++;
                cb.WriteLine("#region Auto generated code from uForms.");
                cb.WriteLine("");
                cb.WriteLine("private void InitializeComponent()");
                cb.WriteLine("{");
                cb.Indent++;
                this.Controls.ForEach(child =>
                {
                    child.WriteCode(cb);
                    cb.WriteLine("this.Controls.Add(this." + child.Name + ");");
                });
                cb.Indent--;
                cb.WriteLine("}");
                cb.WriteLine("");
                cb.WriteLine("#endregion");
                cb.WriteLine("");
                this.Controls.ForEach(child => child.ForTree(node => node.WriteDefinitionCode(cb)));
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
                cb.WriteLine("namespace " + this.nameSpace);
                cb.WriteLine("{");
                cb.Indent++;
                cb.WriteLine("public partial class " + this.className + " : UFWindow");
                cb.WriteLine("{");
                cb.Indent++;
                cb.WriteLine("[MenuItem(\"Tools/" + this.className + "\")]");
                cb.WriteLine("public static void OpenWindow()");
                cb.WriteLine("{");
                cb.Indent++;
                cb.WriteLine("GetWindow<" + this.className + ">();");
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

            AssetDatabase.ImportAsset(codePath);
            AssetDatabase.ImportAsset(designerCodePath);
        }

        public void ExportXml(string xmlPath)
        {
            var attrOverride = new XmlAttributeOverrides(); //属性上書き情報

            var attributes = new XmlAttributes();       //属性のリスト

            //リフレクションを使用してClassList.Childrenの既存属性[XmlArrayItem]を列挙して追加
            //var info = typeof(UFControl).GetMember("childList")[0];
            //var originalAttrs = Attribute.GetCustomAttributes(info, typeof(XmlArrayItemAttribute));
            //foreach(var attr in originalAttrs)
            //{
            //    attributes.XmlArrayItems.Add(attr as XmlArrayItemAttribute);
            //}

            var ase = Assembly.GetAssembly(typeof(UFControl));
            var list = ase.GetTypes()
                .Where(t => t.BaseType == typeof(UFControl))
                .ToList();

            list.Clear();
            this.Controls.ForEach(child => child.ForTree(node =>
            {
                list.Add(node.GetType());
            }));

            list.Add(typeof(UFControl));
            list = list.Distinct().ToList();

            list.ForEach(t => attributes.XmlArrayItems.Add(new XmlArrayItemAttribute(t)));
            attrOverride.Add(typeof(UFControl), "childList", attributes);
            attrOverride.Add(typeof(UFProject), "Controls", attributes);

            UFUtility.ExportXml(xmlPath, this, attrOverride: attrOverride);
        }

        public static UFProject CreateFromXml(string xmlPath)
        {
            var attributes = new XmlAttributes();       //属性のリスト
            var attrOverride = new XmlAttributeOverrides(); //属性上書き情報
            var ase = Assembly.GetAssembly(typeof(UFControl));
            var list = ase.GetTypes()
                .Where(t => t.BaseType == typeof(UFControl))
                .ToList();
            list.Add(typeof(UFControl));
            list = list.Distinct().ToList();

            list.ForEach(t => attributes.XmlArrayItems.Add(new XmlArrayItemAttribute(t)));
            attrOverride.Add(typeof(UFControl), "childList", attributes);
            attrOverride.Add(typeof(UFProject), "Controls", attributes);
            UFProject project = UFUtility.ImportXml<UFProject>(xmlPath, attrOverride:attrOverride);

            project.Controls.ForEach(child => child.RefleshHierarchy());

            return project;
        }
    }
}