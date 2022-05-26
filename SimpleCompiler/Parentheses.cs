using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleCompiler
{
    class Parentheses : Symbol
    {
        public Parentheses(char name, int line, int position)
        {
            Line = line;
            Position = position;
            Name = name;
        }
        public override bool Equals(object obj)
        {
            if (obj is Parentheses)
            {
                return base.Equals(obj);
            }
            return false;
        }


    }
}
