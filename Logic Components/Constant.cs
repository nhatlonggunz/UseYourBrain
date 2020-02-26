using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseYourBrain.Logic_Components
{
    public class Constant : Symbol
    {
        public char Name { get; set; }
        public bool Value { get; private set; }

        //public protected SymbolType Type { get => type; }

        public Constant(char value)
        {
            if(value != '1' || value != '0')
            Name = value;
            nChild = 0;
            type = SymbolType.primitive;

            Value = (value == '1');
        }

        public override bool GetTruthValue(Dictionary<char, bool> dictTruthValue)
        {
            return Value;
        }
    }
}
