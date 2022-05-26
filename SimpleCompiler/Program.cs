using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCompiler
{
    class Program
    {
        static bool Test1()
        {
            Compiler c = new Compiler();
            List<string> lCodeLines = c.ReadFile(@"C:\Users\liorm\Desktop\BGU\הנדסת מערכות מידע\שנה ב\סמסטר א\מבנה מערכות מחשב\Assignment 3\CodeFiles\GCD.Jack");
            List<Token> lTokens = c.Tokenize(lCodeLines);
            Token t = lTokens[0];
            if (t.ToString() != "function" || !(t is Statement))
                return false;
            t = lTokens[20];
            if (t.ToString() != "=" || t.Line != 3 || t.Position != 11)
                return false;
            t = lTokens[50];
            if (t.ToString() != "}" || !(t is Parentheses) || t.Line != 7 || t.Position != 1)
                return false;
            return true;
        }
        static bool Test4()
        {
            try
            {
                Compiler c = new Compiler();
                List<string> lCodeLines = c.ReadFile(@"C:\Users\liorm\Desktop\BGU\הנדסת מערכות מידע\שנה ב\סמסטר א\מבנה מערכות מחשב\Assignment 3\CodeFiles\GCDErr.Jack");
                List<Token> lTokens = c.Tokenize(lCodeLines);
            }
            catch (SyntaxErrorException e)
            {
                return true;
            }
            return false;
        }

        static bool Test2()
        {
            Compiler c = new Compiler();
            List<string> lCodeLines = c.ReadFile(@"C:\Users\liorm\Desktop\BGU\הנדסת מערכות מידע\שנה ב\סמסטר א\מבנה מערכות מחשב\Assignment 3\CodeFiles\Fib.Jack");
            List<Token> lTokens = c.Tokenize(lCodeLines);
            Token t = lTokens[0];
            if (t.ToString() != "function" || !(t is Statement))
                return false;
            t = lTokens[20];
            if (t.ToString() != "2" || !(t is Number) || t.Line != 5 || t.Position != 8)
                return false;
            t = lTokens[50];
            if (t.ToString() != "}" || !(t is Parentheses) || t.Line != 12 || t.Position != 0)
                return false;
            return true;
        }

        static bool Test3()
        {
            Compiler c = new Compiler();
            List<string> lCodeLines = c.ReadFile(@"C:\Users\liorm\Desktop\BGU\הנדסת מערכות מידע\שנה ב\סמסטר א\מבנה מערכות מחשב\Assignment 3\CodeFiles\BinarySearch.Jack");
            List<Token> lTokens = c.Tokenize(lCodeLines);
            Token t = lTokens[0];
            if (t.ToString() != "function" || !(t is Statement))
                return false;
            t = lTokens[20];
            if (t.ToString() != "end" || !(t is Identifier) || t.Line != 2 || t.Position != 9)
                return false;
            t = lTokens[50];
            if (t.ToString() != "+" || !(t is Operator) || t.Line != 9 || t.Position != 22)
                return false;
            return true;
        }

        static bool Test5()
        {
            Compiler c = new Compiler();
            List<string> lCodeLines = c.ReadFile(@"C:\Users\liorm\Desktop\BGU\הנדסת מערכות מידע\שנה ב\סמסטר א\מבנה מערכות מחשב\Assignment 3\CodeFiles\return.Jack");
            List<Token> lTokens = c.Tokenize(lCodeLines);
            Token t = lTokens[0];
            if (t.ToString() != "function" || !(t is Statement))
                return false;
            t = lTokens[20];
            if (t.ToString() != "end" || !(t is Identifier) || t.Line != 2 || t.Position != 9)
                return false;
            t = lTokens[50];
            if (t.ToString() != "+" || !(t is Operator) || t.Line != 9 || t.Position != 22)
                return false;
            return true;
        }



        static void Main(string[] args)
        {
           //Test5();
            /*Test1();
            Test2();
            Test3();
            Test4();*/


        }

    }
}
