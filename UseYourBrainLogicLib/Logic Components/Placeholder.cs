﻿using System.Collections.Generic;

namespace UseYourBrainLogicLib.Logic_Components
{
    /***
     * A "Cheat" class for simplifying the lexing and parsing process
     */

    /// <summary>
    /// OpenParathesis Symbol
    /// </summary>
    public class Placeholder : Symbol
    {
        public Placeholder(char name)
        {
            this.name = name;
        }

        public override bool GetTruthValue(Dictionary<char, bool> dictTruthValue)
        {
            return false;
        }
        public override bool GetTruthValue(bool[] dictTruthValue)
        {
            return false;
        }

        public override Symbol toNand()
        {
            return this;
        }
    }
}
