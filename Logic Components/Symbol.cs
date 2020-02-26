using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseYourBrain.Logic_Components
{
    public abstract class Symbol
    {
        protected int nChild;
        public int nOperand { get => nChild; }
        public List<Symbol> Childs { get; protected set; }

        protected SymbolType type;

        public SymbolType Type { get => type; }

        public virtual void Operate(IEnumerable<Symbol> operands)
        {

        }

        public abstract bool GetTruthValue(Dictionary<char, bool> dictTruthValue);

        //public virtual to
    }
}
