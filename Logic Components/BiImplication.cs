using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseYourBrain.Logic_Components
{
    public class BiImplication : Symbol
    {

        public BiImplication()
        {
            Childs = new List<Symbol>();
            nChild = 2;
            type = SymbolType.operational;

            Variable tmp = new Variable('#');
            
            Childs.Add(tmp);
            Childs.Add(tmp);
        }

        public BiImplication(Symbol left, Symbol right)
        {
            Childs = new List<Symbol>();
            nChild = 2;
            type = SymbolType.operational;

            Childs.Add(left);
            Childs.Add(right);
        }

        public override void Operate(IEnumerable<Symbol> operands)
        {
            Operate(operands.ElementAt(0), operands.ElementAt(1));
        }

        public void Operate(Symbol First, Symbol Second)
        {
            Childs[0] = First;
            Childs[1] = Second;
        }

        public override bool GetTruthValue(Dictionary<char, bool> dictTruthValue)
        {
            return Childs[0].GetTruthValue(dictTruthValue) && Childs[1].GetTruthValue(dictTruthValue);
        }
    }
}
