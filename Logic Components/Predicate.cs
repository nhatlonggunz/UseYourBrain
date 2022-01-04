using System;
using System.Collections.Generic;
using System.Linq;

namespace UseYourBrainLogicLib.Logic_Components
{
    class Predicate : Symbol
    {
        public override string FullName => this.ToString();

        public Predicate(char name)
        {
            this.name = name;

            type = SymbolType.predicate;
            nChild = -1;
        }


        public override bool GetTruthValue(Dictionary<char, bool> dictTruthValue)
        {
            try
            {
                bool value = dictTruthValue[Name];
                return value;
            }
            catch (KeyNotFoundException ex)
            {
                throw ex;
            }
        }

        public override bool GetTruthValue(bool[] dictTruthValue)
        {
            bool value = dictTruthValue[Name];
            return value;
        }

        public override void Operate(IEnumerable<Symbol> operands)
        {
            if (nChild != -1 && operands.Count() != nChild)
                throw new ArgumentException("Wrong number of object variables");

            if (operands == null)
                throw new ArgumentNullException();

            Childs = new List<Symbol>();

            for (int i = 0; i < operands.Count(); i++)
            {
                if (operands.ElementAt(i) == null)
                    throw new ArgumentNullException();
                Childs.Add(operands.ElementAt(i));
            }
        }

        public override Symbol toNand()
        {
            return this;
        }

        public override string ToString()
        {
            if (this.nOperand == 0)
                return name.ToString();

            string content = name + "(";

            foreach (Symbol s in this.Childs)
                content += s + ",";
            content = content.Remove(content.Length - 1);
            content += ")";

            return content;
        }
    }
}
