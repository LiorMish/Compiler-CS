using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleCompiler
{
    class VarType : Keyword
    {
        public VarType(string name, int line, int position)
        {
            Line = line;
            Position = position;
            Name = name;
        }
        public override bool Equals(object obj)
        {
            if (obj is VarType)
            {
                return base.Equals(obj);
            }
            return false;
        }


    }
}
