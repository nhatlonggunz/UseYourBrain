using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace UseYourBrainLogicLib.Logic_Components.Tests
{
    [TestClass()]
    public class ImplicationTests
    {
        [TestMethod()]
        public void ImplicationConstructorTest()
        {
            Implication a = new Implication();

            Assert.IsNotNull(a.Childs);
            Assert.AreEqual('>', a.Name);
            Assert.AreEqual(2, a.nOperand);
            Assert.AreEqual(SymbolType.operational, a.Type);
        }

        [TestMethod()]
        public void ImplicationConstructorTest1()
        {
            Variable p = new Variable('p');
            Variable q = new Variable('q');

            Implication a = new Implication(p, q);

            Assert.IsNotNull(a.Childs);
            Assert.AreEqual('>', a.Name);
            Assert.AreEqual(2, a.nOperand);
            Assert.AreEqual(SymbolType.operational, a.Type);

            Assert.AreEqual(p, a.Childs[0]);
            Assert.AreEqual(q, a.Childs[1]);

            //
            Assert.ThrowsException<ArgumentNullException>(() => new Implication(null, null));
        }

        [TestMethod()]
        public void OperateListTest()
        {
            List<Variable> vars = new List<Variable>()
            {
                new Variable('p'),
                new Variable('q')
            };

            Implication a = new Implication();

            a.Operate(vars);
            Assert.AreEqual(vars[0], a.Childs[0]);
            Assert.AreEqual(vars[1], a.Childs[1]);
        }

        [TestMethod()]
        public void OperateTwoTest()
        {
            Variable p = new Variable('p');
            Variable q = new Variable('q');

            Implication a = new Implication();

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

            Implication implication = new Implication(A, B);
            Symbol nand = implication.toNand();

            Assert.AreEqual("(A % (B % B))", nand.ToString());
        }

        [TestMethod()]
        public void GetTruthValueDictTest()
        {
            Variable p = new Variable('p');
            Variable q = new Variable('q');
            Implication a = new Implication(p, q);

            Dictionary<char, bool> dict = new Dictionary<char, bool>();

            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    dict['p'] = i == 1;
                    dict['q'] = j == 1;

                    Assert.AreEqual(((i ^ 1) | j) == 1, a.GetTruthValue(dict));
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
            Implication a = new Implication(p, q);

            bool[] dict = new bool[130];

            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    dict['p'] = i == 1;
                    dict['q'] = j == 1;

                    Assert.AreEqual(((i ^ 1) | j) == 1, a.GetTruthValue(dict));
                }
            }
        }

        [TestMethod()]
        public void ToStringTest()
        {
            Variable p = new Variable('p');
            Variable q = new Variable('q');
            Implication a = new Implication(p, q);

            Assert.AreEqual("(p > q)", a.ToString());

            Not Left = new Not(p);
            Or Right = new Or(p, q);
            a.Operate(Left, Right);

            Assert.AreEqual("(~p > (p | q))", a.ToString());

            Left = new Not(new Nand(p, q));
            a.Operate(Left, Right);

            Assert.AreEqual("(~(p % q) > (p | q))", a.ToString());
        }
    }
}