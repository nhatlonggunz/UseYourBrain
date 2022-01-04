using System;
using System.Collections.Generic;
using System.Linq;

namespace UseYourBrainLogicLib.Logic_Components
{
    public class Quantifier : Symbol
    {

        public bool[] ListBoundVariables { get; set; }
        public string BoundVariables
        {
            get
            {
                string content = "";
                for (int i = 'a'; i <= 'z'; i++)
                    if (ListBoundVariables[i])
                        content += (char)i;
                return content;
            }
        }

        public override string FullName => name + BoundVariables;

        public Quantifier(string boundVariables)
        {
            ListBoundVariables = new bool[130];

            foreach (char c in boundVariables)
            {
                if (c < 'a' || c > 'z')
                    throw new ArgumentException("Object variable must be a lowercase letter");
                ListBoundVariables[c] = true;
            }

            nChild = 1;
            Childs = new List<Symbol>();
            Childs.Add(new Variable('#'));

            type = SymbolType.quantifier;
        }

        public override bool GetTruthValue(Dictionary<char, bool> dictTruthValue)
        {
            return false;
        }

        public override bool GetTruthValue(bool[] dictTruthValue)
        {
            return false;
        }

        public override void Operate(IEnumerable<Symbol> operands)
        {
            if (operands.Count() != 1 || operands.ElementAt(0) == null)
                throw new ArgumentNullException();

            Operate(operands.ElementAt(0));
        }

        public void Operate(Symbol First)
        {
            Childs[0] = First ?? throw new ArgumentNullException();
        }

        public override Symbol toNand()
        {
            Quantifier quan = new Quantifier(this.BoundVariables);
            quan.Name = this.Name;
            quan.Operate(this.Childs[0].toNand());

            return quan;
        }

        public override string ToString()
        {
            string content = Name.ToString() + BoundVariables + ".";
            content += $"({Childs[0]})";

            return content;
        }
    }
}
