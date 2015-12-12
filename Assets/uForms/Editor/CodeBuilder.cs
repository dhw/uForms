using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Serialization;
using UnityEditor;
using UnityEngine;

namespace uForms
{
    /// <summary></summary>
    public class CodeBuilder
    {
        int indent = 0;
        StringBuilder builder = new StringBuilder();

        public int Indent
        {
            get
            {
                return this.indent;
            }
            set
            {
                this.indent = value;
            }
        }

        [MenuItem("Tools/Test")]
        public static void Test()
        {
            UFProject project = new UFProject();
            project.className = "HidakaEditor";
            project.nameSpace = "hidaka";

            UFCanvas canvas1 = new UFCanvas();
            canvas1.Text = "canvas1";
            canvas1.Name = "canvas1";
            project.root.Add(canvas1);
            UFButton button1 = new UFButton(new Vector2(5, 60));
            button1.Text = "テストA";
            button1.Name = "button1";
            canvas1.Add(button1);
            UFLabel label1 = new UFLabel(new Vector2(5, 20));
            label1.Text = "テストC";
            label1.Name = "label1";
            canvas1.Add(label1);

            Build(project);
        }

        [MenuItem("Tools/Test2")]
        public static void Test2()
        {
            UFProject project = new UFProject();
            project.className = "HidakaEditor";
            project.nameSpace = "hidaka";

            UFCanvas canvas1 = new UFCanvas();
            canvas1.Text = "canvas1";
            canvas1.Name = "canvas1";
            project.root.Add(canvas1);
            UFButton button1 = new UFButton(new Vector2(5, 60));
            button1.Text = "テストA";
            button1.Name = "button1";
            canvas1.Add(button1);
            UFLabel label1 = new UFLabel(new Vector2(5, 20));
            label1.Text = "テストC";
            label1.Name = "label1";
            canvas1.Add(label1);

            var attrOverride = new XmlAttributeOverrides(); //属性上書き情報

            var attributes = new XmlAttributes();       //属性のリスト

            //リフレクションを使用してClassList.Childrenの既存属性[XmlArrayItem]を列挙して追加
            var info = typeof(UFControl).GetMember("childList")[0];
            var originalAttrs = Attribute.GetCustomAttributes(info, typeof(XmlArrayItemAttribute));
            foreach(var attr in originalAttrs)
            {
                attributes.XmlArrayItems.Add(attr as XmlArrayItemAttribute);
            }

            var ase = Assembly.GetAssembly(typeof(UFControl));
            var list = ase.GetTypes()
                .Where(t => t.BaseType == typeof(UFControl))
                .ToList();

            list.Clear();
            project.root.ForTree(node =>
            {
                list.Add(node.GetType());
            });
            
            list.Add(typeof(UFControl));
            list = list.Distinct().ToList();

            list.ForEach(t => attributes.XmlArrayItems.Add(new XmlArrayItemAttribute(t)));
            attrOverride.Add(typeof(UFControl), "childList", attributes);
            
            UFUtility.ExportXml("Data/HidakaEditor.xml", project, attrOverride:attrOverride);
        }

        public static void Build(UFProject project)
        {
            {
                CodeBuilder cb = new CodeBuilder();
                cb.WriteLine("using uForms;");
                cb.WriteLine("using UnityEngine;");
                cb.WriteLine("");
                cb.WriteLine("namespace " + project.nameSpace);
                cb.WriteLine("{");
                cb.Indent++;
                cb.WriteLine("partial class " + project.className);
                cb.WriteLine("{");
                cb.Indent++;
                cb.WriteLine("#region Auto generated code from uForms.");
                cb.WriteLine("");
                cb.WriteLine("private void InitializeComponent()");
                cb.WriteLine("{");
                cb.Indent++;
                project.root.WriteCode(cb);
                cb.Indent--;
                cb.WriteLine("}");
                cb.WriteLine("");
                cb.WriteLine("#endregion");
                cb.WriteLine("");
                project.root.ForTree(node => node.WriteDefinitionCode(cb));
                cb.Indent--;
                cb.WriteLine("}");
                cb.Indent--;
                cb.WriteLine("}");
                File.WriteAllText("Assets/Editor/" + project.className + ".Designer.cs", cb.builder.ToString());
                AssetDatabase.ImportAsset("Assets/Editor/" + project.className + ".Designer.cs");
            }
            {
                CodeBuilder cb = new CodeBuilder();
                cb.WriteLine("using uForms;");
                cb.WriteLine("using UnityEngine;");
                cb.WriteLine("using UnityEditor;");
                cb.WriteLine("");
                cb.WriteLine("namespace " + project.nameSpace);
                cb.WriteLine("{");
                cb.Indent++;
                cb.WriteLine("public partial class " + project.className + " : UFWindow");
                cb.WriteLine("{");
                cb.Indent++;
                cb.WriteLine("[MenuItem(\"Tools/" + project.className + "\")]");
                cb.WriteLine("public static void OpenWindow()");
                cb.WriteLine("{");
                cb.Indent++;
                cb.WriteLine("GetWindow<" + project.className + ">();");
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
                File.WriteAllText("Assets/Editor/" + project.className + ".cs", cb.builder.ToString());
                AssetDatabase.ImportAsset("Assets/Editor/" + project.className + ".cs");
            }
        }

        public void WriteLine(string line)
        {
            this.AddIndent();
            this.builder.Append(line + "\r\n");

        }

        private void AddIndent()
        {
            for(int i = 0; i < this.indent; ++i)
            {
                this.builder.Append("    ");
            }
        }
    }
}