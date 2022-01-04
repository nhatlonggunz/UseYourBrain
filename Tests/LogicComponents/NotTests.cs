using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace UseYourBrainLogicLib.Logic_Components.Tests
{
    [TestClass()]
    public class NotTests
    {
        [TestMethod()]
        public void NotConstructorTest()
        {
            Not a = new Not();

            Assert.IsNotNull(a.Childs);
            Assert.AreEqual('~', a.Name);
            Assert.AreEqual(1, a.nOperand);
            Assert.AreEqual(SymbolType.operational, a.Type);
        }

        [TestMethod()]
        public void NotConstructorTest1()
        {
            Variable p = new Variable('p');

            Not a = new Not(p);

            Assert.IsNotNull(a.Childs);
            Assert.AreEqual('~', a.Name);
            Assert.AreEqual(1, a.nOperand);
            Assert.AreEqual(SymbolType.operational, a.Type);

            Assert.AreEqual(p, a.Childs[0]);

            // 

            Assert.ThrowsException<ArgumentNullException>(() => new Not(null));
        }

        [TestMethod()]
        public void OperateListTest()
        {
            List<Variable> vars = new List<Variable>()
            {
                new Variable('p')
            };

            Not a = new Not();

            a.Operate(vars);
            Assert.AreEqual(vars[0], a.Childs[0]);
        }

        [TestMethod()]
        public void OperateTwoTest()
        {
            Variable p = new Variable('p');

            Not a = new Not();
            a.Operate(p);

            Assert.AreEqual(p, a.Childs[0]);

            // operate null
            Assert.ThrowsException<ArgumentNullException>(
                () => a.Operate(null));
        }

        [TestMethod()]
        public void toNandTest()
        {
            Variable A = new Variable('A');

            Not not = new Not(A);
            Symbol nand = not.toNand();

            Assert.AreEqual("(A % A)", nand.ToString());
        }

        [TestMethod()]
        public void GetTruthValueDictTest()
        {
            Variable p = new Variable('p');
            Not a = new Not(p);

            Dictionary<char, bool> dict = new Dictionary<char, bool>();

            for (int i = 0; i < 2; i++)
            {
                dict['p'] = i == 1;

                Assert.AreEqual(i == 0, a.GetTruthValue(dict));
            }

            dict.Remove('p');
            Assert.ThrowsException<KeyNotFoundException>(
                () => a.GetTruthValue(dict));
        }

        [TestMethod()]
        public void GetTruthValueArrayTest()
        {
            Variable p = new Variable('p');
            Not a = new Not(p);

            Dictionary<char, bool> dict = new Dictionary<char, bool>();

            for (int i = 0; i < 2; i++)
            {
                dict['p'] = i == 1;

                Assert.AreEqual(i == 0, a.GetTruthValue(dict));
            }
        }

        [TestMethod()]
        public void ToStringTest()
        {
            Variable p = new Variable('p');
            Variable q = new Variable('q');
            And a = new And(p, q);

            Assert.AreEqual("(p & q)", a.ToString());

            Not Left = new Not(p);
            Or Right = new Or(p, q);
            a.Operate(Left, Right);

            Assert.AreEqual("(~p & (p | q))", a.ToString());

            Left = new Not(new Nand(p, q));
            a.Operate(Left, Right);

            Assert.AreEqual("(~(p % q) & (p | q))", a.ToString());
        }
    }
}