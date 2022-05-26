using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleCompiler
{
    class Operator : Symbol
    {
        public Operator(char name, int line, int position)
        {
            Line = line;
            Position = position;
            Name = name;
        }
        public override bool Equals(object obj)
        {
            if (obj is Operator)
            {
                return base.Equals(obj);
            }
            return false;
        }

    }
}
