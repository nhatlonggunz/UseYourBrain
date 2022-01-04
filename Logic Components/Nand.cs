using System;
using System.Collections.Generic;
using System.Linq;

namespace UseYourBrainLogicLib.Logic_Components
{
    public class Nand : Symbol
    {
        public Nand()
        {
            name = '%';

            Childs = new List<Symbol>();
            nChild = 2;
            type = SymbolType.operational;

            Variable tmp = new Variable('#');

            Childs.Add(tmp);
            Childs.Add(tmp);
        }

        public Nand(Symbol left, Symbol right)
        {
            if (left == null || right == null)
                throw new ArgumentNullException();

            name = '%';

            Childs = new List<Symbol>();
            nChild = 2;
            type = SymbolType.operational;

            Childs.Add(left);
            Childs.Add(right);
        }

        public override void Operate(IEnumerable<Symbol> operands)
        {
            if (operands.Count() != 2 ||
                operands.ElementAt(0) == null ||
                operands.ElementAt(1) == null)
                throw new ArgumentNullException();

            Operate(operands.ElementAt(0), operands.ElementAt(1));
        }

        public void Operate(Symbol First, Symbol Second)
        {
            if (First == null || Second == null)
                throw new ArgumentNullException();

            Childs[0] = First;
            Childs[1] = Second;
        }

        public override bool GetTruthValue(Dictionary<char, bool> dictTruthValue)
        {
            return !(Childs[0].GetTruthValue(dictTruthValue) &&
                     Childs[1].GetTruthValue(dictTruthValue));
        }

        public override bool GetTruthValue(bool[] dictTruthValue)
        {
            return !(Childs[0].GetTruthValue(dictTruthValue) &&
                     Childs[1].GetTruthValue(dictTruthValue));
        }

        public override Symbol toNand()
        {
            return this;
        }

        public override string ToString()
        {
            return "(" + Childs[0].ToString() + " % " + Childs[1].ToString() + ")";
        }
    }
}
