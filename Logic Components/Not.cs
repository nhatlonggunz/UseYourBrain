using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseYourBrain.Logic_Components
{
    public class Not : Symbol
    {

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
            name = '~';

            Childs = new List<Symbol>();
            nChild = 1;
            type = SymbolType.operational;

            Childs.Add(operand);
        }

        public override void Operate(IEnumerable<Symbol> operands)
        {
            Operate(operands.ElementAt(0));
        }

        public void Operate(Symbol First)
        {
            Childs[0] = First;
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
