using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseYourBrain.Logic_Components
{
    public class Variable : Symbol
    {
        public char Name { get; set; }
        public override List<Symbol> Childs { get; set; }
        public bool bounded { get; set; }
        //public protected SymbolType Type { get => type; }

        private bool truthValue;

        public Variable(char name)
        {
            Name = name;
            nChild = 0;
            bounded = false;
            type = SymbolType.primitive;
        }

        public override bool GetTruthValue()
        {
            return truthValue;
        }
    }
}
