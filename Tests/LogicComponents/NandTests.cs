using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace UseYourBrainLogicLib.Logic_Components.Tests
{
    [TestClass()]
    public class NandTests
    {
        [TestMethod()]
        public void NandConstructorTest()
        {
            Nand a = new Nand();

            Assert.IsNotNull(a.Childs);
            Assert.AreEqual('%', a.Name);
            Assert.AreEqual(2, a.nOperand);
            Assert.AreEqual(SymbolType.operational, a.Type);
        }

        [TestMethod()]
        public void NandConstructorTest1()
        {
            Variable p = new Variable('p');
            Variable q = new Variable('q');

            Nand a = new Nand(p, q);

            Assert.IsNotNull(a.Childs);
            Assert.AreEqual('%', a.Name);
            Assert.AreEqual(2, a.nOperand);
            Assert.AreEqual(SymbolType.operational, a.Type);

            Assert.AreEqual(p, a.Childs[0]);
            Assert.AreEqual(q, a.Childs[1]);

            //
            Assert.ThrowsException<ArgumentNullException>(() => new Nand(null, null));
        }

        [TestMethod()]
        public void OperateListTest()
        {
            List<Variable> vars = new List<Variable>()
            {
                new Variable('p'),
                new Variable('q')
            };

            Nand a = new Nand();

            a.Operate(vars);
            Assert.AreEqual(vars[0], a.Childs[0]);
            Assert.AreEqual(vars[1], a.Childs[1]);
        }

        [TestMethod()]
        public void OperateTwoTest()
        {
            Variable p = new Variable('p');
            Variable q = new Variable('q');

            Nand a = new Nand();

            a.Operate(p, q);
            Assert.AreEqual(p, a.Childs[0]);
            Assert.AreEqual(q, a.Childs[1]);

            Assert.ThrowsException<ArgumentNullException>(() => a.Operate(null, null));
        }

        [TestMethod()]
        public void toNandTest()
        {
            Variable A = new Variable('A');
            Variable B = new Variable('B');

            Nand nand = new Nand(A, B);
            nand = (Nand)nand.toNand();

            Assert.AreEqual("(A % B)", nand.ToString());
        }

        [TestMethod()]
        public void GetTruthValueDictTest()
        {
            Variable p = new Variable('p');
            Variable q = new Variable('q');
            Nand a = new Nand(p, q);

            Dictionary<char, bool> dict = new Dictionary<char, bool>();

            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    dict['p'] = i == 1;
                    dict['q'] = j == 1;

                    Assert.AreEqual((i & j) == 0, a.GetTruthValue(dict));
                }
            }

            dict.Remove('p');
            Assert.ThrowsException<KeyNotFoundException>(
                () => a.GetTruthValue(dict));
        }

        [TestMethod()]
        public void GetTruthValueArrayTest()
        {
            Variable p = new Variable('p');
            Variable q = new Variable('q');
            Nand a = new Nand(p, q);

            bool[] dict = new bool[130];

            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    dict['p'] = i == 1;
                    dict['q'] = j == 1;

                    Assert.AreEqual((i & j) == 0, a.GetTruthValue(dict));
                }
            }
        }

        [TestMethod()]
        public void ToStringTest()
        {
            Variable p = new Variable('p');
            Variable q = new Variable('q');
            Nand a = new Nand(p, q);

            Assert.AreEqual("(p % q)", a.ToString());

            Not Left = new Not(p);
            Or Right = new Or(p, q);
            a.Operate(Left, Right);

            Assert.AreEqual("(~p % (p | q))", a.ToString());

            Left = new Not(new Nand(p, q));
            a.Operate(Left, Right);

            Assert.AreEqual("(~(p % q) % (p | q))", a.ToString());
        }
    }
}