using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCompiler
{
    public class VarDeclaration : JackProgramElement
    {

        public enum VarTypeEnum { Int, Array, Char, Boolean, Invalid };
        public VarTypeEnum Type { get; private set; }
        public string Name { get; private set; }


        public VarDeclaration()
        {

        }

        public VarDeclaration(string sType, string sName)
        {
            Type = GetVarType(sType);
            Name = sName;
        }

        public VarDeclaration(Token tType, Token tName)
        {
            SetTypeAndName(tType, tName);
        }

        private void SetTypeAndName(Token tType, Token tName)
        {
            if (!(tType is VarType))
                throw new SyntaxErrorException("Expected var type, received " + tType, tType);
            Type = GetVarType(tType);
            if (Type == VarTypeEnum.Invalid)
                throw new SyntaxErrorException("Expected var type, received " + tType, tType);
            if (!(tName is Identifier))
                throw new SyntaxErrorException("Expected var name, received " + tName, tName);

            Name = ((Identifier)tName).Name;
        }

        public static VarTypeEnum GetVarType(Token t)
        {
            if (t is VarType)
                return GetVarType(((VarType)t).Name);
            return VarTypeEnum.Invalid;
        }

        public static VarTypeEnum GetVarType(string sName)
        {
            if (sName == "int")
                return VarTypeEnum.Int;
            if (sName == "char")
                return VarTypeEnum.Char;
            if (sName == "array")
                return VarTypeEnum.Array;
            if (sName == "boolean")
                return VarTypeEnum.Boolean;
            return VarTypeEnum.Invalid;
        }

        public override void Parse(TokensStack sTokens)
        {
            //parsing code from assignment 3.2 here
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return "var " + Type + " " + Name + ";";
        }

        public override bool Equals(object obj)
        {
            if(obj is VarDeclaration)
            {
                VarDeclaration var = (VarDeclaration)obj;
                return var.Type == Type && var.Name == Name;
            }
            return false;
        }
        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }
    }
}
