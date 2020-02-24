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
        public abstract List<Symbol> Childs { get; set; }

        protected SymbolType type;

        public SymbolType Type { get => type; }

        public abstract bool GetTruthValue();
    }
}
