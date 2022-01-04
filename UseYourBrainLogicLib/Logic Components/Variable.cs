using System;
using System.Collections.Generic;

namespace UseYourBrainLogicLib.Logic_Components
{
    public class Variable : Symbol
    {
        public bool Bounded { get; set; }
        //public protected SymbolType Type { get => type; }

        public Variable(char name)
            : base()
        {
            // # is a placeholder
            if (!(name >= 'A' && name <= 'Z') && !(name >= 'a' && name <= 'z') && name != '#')
                throw new Exception("Invalid variable name");

            this.name = name;
            nChild = 0;
            Bounded = false;
            type = SymbolType.primitive;
        }

        public override bool GetTruthValue(Dictionary<char, bool> dictTruthValue)
        {
            try
            {
                bool value = dictTruthValue[Name];
                return value;
            }
            catch (KeyNotFoundException ex)
            {
                throw ex;
            }
        }

        public override bool GetTruthValue(bool[] dictTruthValue)
        {
            bool value = dictTruthValue[Name];
            return value;
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
