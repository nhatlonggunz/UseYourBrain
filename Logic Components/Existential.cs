namespace UseYourBrainLogicLib.Logic_Components
{
    public class Existential : Quantifier
    {

        public Existential(string boundVariables) : base(boundVariables)
        {
            name = '!';
        }
    }
}
