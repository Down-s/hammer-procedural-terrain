using System;
using System.Text;
using System.Collections.Generic;

namespace Volvo
{
    public class VMFWriter
    {
        private StringBuilder Data = new StringBuilder();
        private int Scope = 0;
        public VMFWriter()
        {

        }

        private void IndentScope()
        {
            if (Scope == 0) return;
            Data.Append('\t', Scope);
        }

        public void OpenScope(string name)
        {
            Data.AppendFormat("{0}\n", name);
            this.IndentScope();
            Data.Append("{\n");
            Scope++;
            this.IndentScope();
        }

        public void CloseScope()
        {
            Scope--;
            Data.Remove(Data.Length - 1, 1);
            Data.Append("}\n");
            this.IndentScope();
        }

        public void Insert<T>(string key, T value)
        {
            if (value == null) return;
            Data.AppendFormat("\"{0}\" \"{1}\"", key, value.ToString());
            Data.Append('\n');
            this.IndentScope();
        }
        
        public void Append(string str)
        {
            Data.Append(str);
        }

        public string GetText()
        {
            return Data.ToString();
        }
    }
}