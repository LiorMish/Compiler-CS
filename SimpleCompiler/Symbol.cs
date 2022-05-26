using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleCompiler
{
    class Symbol : Token
    {
        public char Name { get; set; }
        public override bool Equals(object obj)
        {
            if (obj is Symbol)
            {
                Symbol t = (Symbol)obj;
                if (Name == t.Name)
                    return base.Equals(t);
            }
            return false;
        }
        public override string ToString()
        {
            return Name + "";
        }

    }
}
