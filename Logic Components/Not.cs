using System;
using System.Collections.Generic;
using System.Linq;

namespace UseYourBrainLogicLib.Logic_Components
{
    public class Not : Symbol
    {
        public Symbol Subformula { get => this.Childs[0]; }

        public Not()
        {
            name = '~';

            Childs = new List<Symbol>();
            nChild = 1;
            type = SymbolType.operational;

            Variable tmp = new Variable('#');

            Childs.Add(tmp);
        }

        public Not(Symbol operand)
        {
            if (operand == null)
                throw new ArgumentNullException();

            name = '~';

            Childs = new List<Symbol>();
            nChild = 1;
            type = SymbolType.operational;

            Childs.Add(operand);
        }

        public override void Operate(IEnumerable<Symbol> operands)
        {
            if (operands.Count() != 1 ||
                operands.ElementAt(0) == null)
                throw new ArgumentNullException();
            Operate(operands.ElementAt(0));
        }

        public void Operate(Symbol First)
        {
            if (First == null)
                throw new ArgumentNullException();

            Childs[0] = First;
        }

        public override Symbol toNand()
        {
            // ~A = A % A
            Symbol operand = this.Childs[0].toNand();
            return new Nand(operand, operand);
        }

        public override bool GetTruthValue(Dictionary<char, bool> dictTruthValue)
        {
            return !Childs[0].GetTruthValue(dictTruthValue);
        }

        public override bool GetTruthValue(bool[] dictTruthValue)
        {
            return !Childs[0].GetTruthValue(dictTruthValue);
        }

        public override string ToString()
        {
            return "~" + Childs[0].ToString();
        }
    }
}
