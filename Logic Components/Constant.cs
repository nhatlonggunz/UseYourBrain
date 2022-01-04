using System;
using System.Collections.Generic;

namespace UseYourBrainLogicLib.Logic_Components
{
    public class Constant : Symbol
    {
        public bool Value { get; private set; }

        //public protected SymbolType Type { get => type; }

        public Constant(char value)
        {
            if (value != '1' && value != '0')
                throw new Exception("Constant can only be 0 or 1");

            this.name = value;
            nChild = 0;
            type = SymbolType.primitive;

            Value = (value == '1');
        }

        public override bool GetTruthValue(Dictionary<char, bool> dictTruthValue)
        {
            return Value;
        }

        public override bool GetTruthValue(bool[] dictTruthValue)
        {
            return Value;
        }

        public override Symbol toNand()
        {
            return this;
        }

        public override string ToString()
        {
            return this.name.ToString();
        }
    }
}
