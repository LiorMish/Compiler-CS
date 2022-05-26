using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SimpleCompiler
{
    class Compiler
    {


        public Compiler()
        {
        }

        //reads a file into a list of strings, each string represents one line of code
        public List<string> ReadFile(string sFileName)
        {
            StreamReader sr = new StreamReader(sFileName);
            List<string> lCodeLines = new List<string>();
            while (!sr.EndOfStream)
            {
                lCodeLines.Add(sr.ReadLine());
            }
            sr.Close();
            return lCodeLines;
        }



        //Computes the next token in the string s, from the begining of s until a delimiter has been reached. 
        //Returns the string without the token.
        private string Next(string s, char[] aDelimiters, out string sToken, out int cChars)
        {
            cChars = 1;
            sToken = s[0] + "";
            if (aDelimiters.Contains(s[0]))
                return s.Substring(1);
            int i = 0;
            for (i = 1; i < s.Length; i++)
            {
                if (aDelimiters.Contains(s[i]))
                    return s.Substring(i);
                else
                    sToken += s[i];
                cChars++;
            }
            return null;
        }

        //Splits a string into a list of tokens, separated by delimiters
        private List<string> Split(string s, char[] aDelimiters)
        {
            List<string> lTokens = new List<string>();
            while (s.Length > 0)
            {
                string sToken = "";
                int i = 0;
                for (i = 0; i < s.Length; i++)
                {
                    if (aDelimiters.Contains(s[i]))
                    {
                        if (sToken.Length > 0)
                            lTokens.Add(sToken);
                        lTokens.Add(s[i] + "");
                        break;
                    }
                    else
                        sToken += s[i];
                }
                if (i == s.Length)
                {
                    lTokens.Add(sToken);
                    s = "";
                }
                else
                    s = s.Substring(i + 1);
            }
            return lTokens;
        }

        //This is the main method for the Tokenizing assignment. 
        //Takes a list of code lines, and returns a list of tokens.
        //For each token you must identify its type, and instantiate the correct subclass accordingly.
        //You need to identify the token position in the file (line, index within the line).
        //You also need to identify errors, in this assignement - illegal identifier names.
        public List<Token> Tokenize(List<string> lCodeLines)
        {
            List<Token> lTokens = new List<Token>();
            //your code here
            char[] delimiters = {' ', '\t', '\n', ',', ';' , '(', ')', '[', ']', '{', '}', '*', '+', '-', '/', '<', '>', '&', '=', '|', '!'};
            int positionCounter = 0;
            int lineCounter = 0;

            for (int i = 0; i < lCodeLines.Count; i++)
            {
                string line = lCodeLines[i];
                if ((line.Length >= 2 && line[0] == '/' && line[1] == '/') || emptyLine(line)) // this is a comment
                {
                    lineCounter++;
                    continue;
                }
                
                if (line.Contains("//"))
                {
                    int index = findCommentIndex(line);
                    line = line.Substring(0, index);
                }

                List<string> tokens = Split(line, delimiters);
                positionCounter = 0;

                for (int j=0; j< tokens.Count; j++)
                {
                    
                    if (tokens[j] != " " && tokens[j] != "\t")
                    {
                       

                        Token token;
                        if (Token.Statements.Contains(tokens[j]))
                        {
                            token = new Statement(tokens[j], lineCounter, positionCounter);
                            lTokens.Add(token);
                            positionCounter+=tokens[j].Length;
                            continue;
                        }
                        else if (Token.VarTypes.Contains(tokens[j]))
                        {
                            token = new VarType(tokens[j], lineCounter, positionCounter);
                            lTokens.Add(token);
                            positionCounter += tokens[j].Length;
                            continue;
                        }
                        else if (Token.Constants.Contains(tokens[j]))
                        {
                            token = new Constant(tokens[j], lineCounter, positionCounter);
                            lTokens.Add(token);
                            positionCounter += tokens[j].Length;
                            continue;
                        }
                        else if (Token.Operators.Contains(tokens[j][0]))
                        {
                            token = new Operator(tokens[j][0], lineCounter, positionCounter);
                            lTokens.Add(token);
                            positionCounter += tokens[j].Length;
                            continue;
                        }
                        else if (Token.Parentheses.Contains(tokens[j][0]))
                        {
                            token = new Parentheses(tokens[j][0], lineCounter, positionCounter);
                            lTokens.Add(token);
                            positionCounter += tokens[j].Length;
                            continue;
                        }
                        else if (Token.Separators.Contains(tokens[j][0]))
                        {
                            token = new Separator(tokens[j][0], lineCounter, positionCounter);
                            lTokens.Add(token);
                            positionCounter += tokens[j].Length;
                            continue;
                        }
                        else
                        {
                            
                            bool isNumeric = int.TryParse(tokens[j], out int num);
                            if (isNumeric)
                            {
                                token = new Number(tokens[j], lineCounter, positionCounter);
                                lTokens.Add(token);
                                positionCounter += tokens[j].Length;
                                continue;
                            }
                            else
                            {
                                token = new Identifier(tokens[j], lineCounter, positionCounter);

                                if (tokens[j][0] >= '0' && tokens[j][0] <= '9')
                                    throw new SyntaxErrorException("Identifier can't start with a number", token);
                                if (unknownCharExist(tokens[j]))
                                    throw new SyntaxErrorException("unknown symbol found", token);

                                lTokens.Add(token);

                                positionCounter += tokens[j].Length;
                                continue;
                            }
                        }
                    }
                    else positionCounter += 1;
                    
                }
                lineCounter++;

            }
                
            
            return lTokens;
        }

        private bool emptyLine(string line)
        {
            if (line == "")
                return true;
            for (int i = 0; i < line.Length; i++)
            {
                if (line[i] != ' ' && line[i] != '\t' && line[i] != '\n')
                        return false;
            }

        return true;
        }

        private int findCommentIndex(string v)
        {
            for (int i = 0; i < v.Length-1; i++)
            {
                if (v[i] == '/' && v[i+1] == '/')
                {
                    return i;
                }

            }
            return -1;
        }

        private bool unknownCharExist(string s)
        {
            char[] unknownChars = new char[] {'#', ':', '"'};
            int i = 0;
            for (; i < s.Length; i++)
                if (unknownChars.Contains(s[i]))
                    return true;
            return false;
        }
    }
}

