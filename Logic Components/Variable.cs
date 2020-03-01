using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseYourBrain.Logic_Components
{
    public class Variable : Symbol
    {
        public bool bounded { get; set; }
        //public protected SymbolType Type { get => type; }

        public Variable(char name)
            :base()
        {
            this.name = name;
            nChild = 0;
            bounded = false;
            type = SymbolType.primitive;
        }

        public override bool GetTruthValue(Dictionary<char, bool> dictTruthValue)
        {
            try
            {
                bool value = dictTruthValue[Name];
                return value;
            }
            catch(KeyNotFoundException)
            {
                Console.WriteLine("Variable {0} is not found", Name);
                return false;
            }
        }

        public override bool GetTruthValue(bool[] dictTruthValue)
        {
            try
            {
                bool value = dictTruthValue[Name - 'A'];
                return value;
            }
            catch (Exception)
            {
                Console.WriteLine("Variable {0} is not found", Name);
                return false;
            }
        }

        public override string ToString()
        {
            return this.name.ToString();
        }
    }
}
