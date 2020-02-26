using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseYourBrain.Logic_Components
{
    /***
     * A "Cheat" class for simplifying the lexing and parsing process
     */

    /// <summary>
    /// OpenParathesis Symbol
    /// </summary>
    class OpenParathesis : Symbol
    {

        public override bool GetTruthValue(Dictionary<char, bool> dictTruthValue)
        {
            return false;
        }
    }
}
