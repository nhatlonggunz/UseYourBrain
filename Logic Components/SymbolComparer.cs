using System.Collections.Generic;

namespace UseYourBrainLogicLib.Logic_Components
{
    /**
     * This comparer only provides a loose comparision for Semantic Tableaux.
     * In ST, we care more about the form/presentation of the expression 
     * rather than the truth value. 
     * 
     * 
     */
    public class SymbolComparer : IEqualityComparer<Symbol>
    {
        public bool Equals(Symbol A, Symbol B)
        {
            return A.ToString().Equals(B.ToString());
        }

        public int GetHashCode(Symbol A)
        {
            return A.ToString().GetHashCode();
        }
    }
}
