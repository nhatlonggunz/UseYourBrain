using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UseYourBrainLogicLib.LogicCalculator.Tests
{
    [TestClass()]
    public class CalculatorTests
    {
        [TestMethod()]
        public void ConvertToFullDNFClauseTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ConvertToFullDNFTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ConvertToDNFClauseTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ConvertToDNFTest()
        {
            AbstractionSyntaxTree ast;
            TruthTable tb;
            string dnfClause;

            // test 01: tautology
            ast = new AbstractionSyntaxTree("|(|(z, z), |(~(z), ~(z)))");
            tb = ast.GenerateTruthTable();
            dnfClause = new Calculator().ConvertToDNF(tb);

            Assert.AreEqual("|(a, ~(a))", dnfClause);

            // test 02: contradiction
            ast = new AbstractionSyntaxTree("&(z, ~(z))");
            tb = ast.GenerateTruthTable();
            dnfClause = new Calculator().ConvertToDNF(tb);

            Assert.AreEqual("&(a, ~(a))", dnfClause);
        }
    }
}