using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleCompiler
{
    class Statement : Keyword
    {
        public Statement(string name, int line, int position)
        {
            Line = line;
            Position = position;
            Name = name;
        }
        public override bool Equals(object obj)
        {
            if (obj is Statement)
            {
                return base.Equals(obj);
            }
            return false;
        }

    }
}
