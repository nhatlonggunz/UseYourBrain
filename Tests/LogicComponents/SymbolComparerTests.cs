using Microsoft.VisualStudio.TestTools.UnitTesting;
using UseYourBrainLogicLib.LogicCalculator;

namespace UseYourBrainLogicLib.Logic_Components.Tests
{
    [TestClass()]
    public class SymbolComparerTests
    {
        [TestMethod()]
        public void EqualsTest()
        {
            SymbolComparer sc = new SymbolComparer();

            AbstractionSyntaxTree a = new AbstractionSyntaxTree(">(a,b)");
            AbstractionSyntaxTree b = new AbstractionSyntaxTree(">(a,b)");

            Assert.IsTrue(sc.Equals(a.Root, b.Root));

            a = new AbstractionSyntaxTree("&(&(a,b), c)");
            b = new AbstractionSyntaxTree("&(&(a,b), c)");
            Assert.IsTrue(sc.Equals(a.Root, b.Root));

            a = new AbstractionSyntaxTree(">(a,b)");
            b = new AbstractionSyntaxTree("|(~(a), b)");
            Assert.IsFalse(sc.Equals(a.Root, b.Root));
        }

        [TestMethod()]
        public void GetHashCodeTest()
        {
            SymbolComparer sc = new SymbolComparer();
            Variable a = new Variable('a');
            Variable b = new Variable('a');

            Assert.AreEqual(sc.GetHashCode(a), sc.GetHashCode(b));

            a = new Variable('a');
            b = new Variable('b');
            Assert.AreNotEqual(sc.GetHashCode(a), sc.GetHashCode(b));
        }
    }
}