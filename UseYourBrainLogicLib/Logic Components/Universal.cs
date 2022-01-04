namespace UseYourBrainLogicLib.Logic_Components
{
    public class Universal : Quantifier
    {

        public Universal(string boundVariables) : base(boundVariables)
        {
            name = '@';
        }
    }
}
