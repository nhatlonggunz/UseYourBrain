using System;

using UseYourBrainLogicLib.Logic_Components;

namespace UseYourBrainLogicLib.LogicCalculator
{
    public class Factory
    {
        public static Type[] Types = new Type[]
        {
            typeof(And), typeof(Or), typeof(Not), typeof(Nand),typeof(Variable),
            typeof(BiImplication), typeof(Implication), typeof(Constant)
        };

        // Generate random input
        // currently proposition only
        //public static Symbol GenerateInput(int maxDepth, double stopChance, int curDepth = 0)
        //{
        //    //int randInd = Random.
        //    //Symbol cur = (Symbol)Activator.CreateInstance()
        //    //if(curDepth == maxDepth - 1 && )
        //}
    }
}
