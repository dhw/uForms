using System.Text;

namespace uForms
{
    public class CodeBuilder
    {
        private int indent = 0;

        private StringBuilder builder = new StringBuilder();

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

        public void WriteLine(string format, params object[] args)
        {
            WriteLine(string.Format(format, args));
        }

        public void WriteLine(string line)
        {
            this.AddIndent();
            this.builder.Append(line + "\r\n");
        }

        public string GetCode()
        {
            return this.builder.ToString();
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