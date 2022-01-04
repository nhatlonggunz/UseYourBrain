using System;
using System.Collections.Generic;

namespace UseYourBrainLogicLib.Logic_Components
{
    public abstract class Symbol
    {
        protected char name;
        public char Name { get => name; set { name = value; } }
        public virtual string FullName { get => name.ToString(); }

        public int nChild { get; set; }
        public List<Symbol> Childs { get; protected set; }
        public int nOperand
        {
            get => Childs is null ? -1 : Childs.Count;
        }

        public bool[] GammaApplied { get; set; }

        protected SymbolType type;

        public SymbolType Type { get => type; }

        public Symbol()
        {
            Childs = new List<Symbol>();
            GammaApplied = new bool[130];
        }

        public virtual void Operate(IEnumerable<Symbol> operands)
        {
            throw new NotImplementedException();
        }

        public abstract Symbol toNand();

        public Symbol ChangeVariableName(char FromChar, char ToChar, bool bounded = false)
        {
            Symbol result = ObjectExtensions.Copy<Symbol>(this);

            ChangeVariableNameUtil(result, new bool[130], FromChar, ToChar, bounded);

            return result;
        }

        public void ChangeVariableNameUtil(Symbol u, bool[] MapBoundVariable, char FromChar, char ToChar, bool bounded)
        {
            if (u is Quantifier)
            {
                foreach (char c in ((Quantifier)u).BoundVariables)
                    MapBoundVariable[c] = true;
            }
            if (u is Variable)
            {
                if (MapBoundVariable[u.Name] != bounded)
                    return;

                if (u.Name == FromChar)
                    u.Name = ToChar;
            }

            foreach (var child in u.Childs)
                ChangeVariableNameUtil(child, MapBoundVariable, FromChar, ToChar, bounded);
        }

        public abstract bool GetTruthValue(Dictionary<char, bool> dictTruthValue);
        public abstract bool GetTruthValue(bool[] dictTruthValue);
    }
}
