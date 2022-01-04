using Microsoft.VisualStudio.TestTools.UnitTesting;

using UseYourBrainLogicLib.LogicCalculator;

namespace System.Tests
{
    [TestClass()]
    public class ObjectExtensionsTests
    {
        [TestMethod()]
        public void TestDeepCopy()
        {
            AbstractionSyntaxTree ast = new AbstractionSyntaxTree(">(>(P,&(Q,R)),&(>(P,Q),>(Q,>(P,R))))");
            var tmp = ObjectExtensions.Copy(ast);
            tmp.Root = null;

            Assert.IsFalse(ast.Root is null);
        }
    }
}