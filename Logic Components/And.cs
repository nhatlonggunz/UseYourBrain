using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseYourBrain.Logic_Components
{
    public class And : Symbol
    {
        //public override SymbolType Type { get => type; }

        public override List<Symbol> Childs { get; set; }

        public And()
        {
            nChild = 2;
            type = SymbolType.operational;

            Variable tmp = new Variable('#');
            
            Childs.Add(tmp);
            Childs.Add(tmp);
        }

        public And(Symbol left, Symbol right)
        {
            nChild = 2;
            type = SymbolType.operational;

            Childs.Add(left);
            Childs.Add(right);
        }

        public override bool GetTruthValue()
        {
            return Childs[0].GetTruthValue() && Childs[1].GetTruthValue();
        }
    }
}
