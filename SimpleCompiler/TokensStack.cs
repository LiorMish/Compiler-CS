using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCompiler
{
    public class TokensStack
    {
        public int Count { get { return m_sTokens.Count; } }
        public Token LastPush { get; private set; }
        public Token LastPop { get; private set; }
        private Stack<Token> m_sTokens;

        public TokensStack()
        {
            m_sTokens = new Stack<Token>();
        }
        public TokensStack(List<Token> lTokens)
        {
            m_sTokens = new Stack<Token>();
            for (int i = lTokens.Count - 1; i >= 0; i--)
                Push(lTokens[i]);
        }
        public void Push(Token t)
        {
            m_sTokens.Push(t);
            LastPush = t;
        }
        public Token Pop()
        {
            Token t = m_sTokens.Pop();
            LastPop = t;
            return t;
        }
        public Token Peek()
        {
            return m_sTokens.Peek();
        }
        public Token Peek(int cItems)
        {
            Stack<Token> aux = new Stack<Token>();
            for (int i = 0; i < cItems; i++)
                aux.Push(m_sTokens.Pop());
            Token t = m_sTokens.Peek();
            while (aux.Count > 0)
                m_sTokens.Push(aux.Pop());
            return t;
        }
    }
}
