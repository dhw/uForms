using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
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