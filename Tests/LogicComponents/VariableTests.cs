using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace UseYourBrainLogicLib.Logic_Components.Tests
{
    [TestClass()]
    public class VariableTests
    {
        [TestMethod()]
        public void VariableTest()
        {
            Variable a = new Variable('x');
            Assert.IsNotNull(a);

            Assert.ThrowsException<Exception>(() => new Variable('!'));
        }

        [TestMethod()]
        public void GetTruthValueDictTest()
        {
            Dictionary<char, bool> dict = new Dictionary<char, bool>();

            Variable a = new Variable('x');

            dict['x'] = false;
            Assert.IsFalse(a.GetTruthValue(dict));

            dict['x'] = true;
            Assert.IsTrue(a.GetTruthValue(dict));

            dict.Remove('x');
            Assert.ThrowsException<KeyNotFoundException>(
                () => a.GetTruthValue(dict));
        }

        [TestMethod()]
        public void GetTruthValueArrayTest()
        {
            bool[] truthValues = new bool[130];
            Variable a = new Variable('x');

            truthValues['x'] = false;
            Assert.IsFalse(a.GetTruthValue(truthValues));

            truthValues['x'] = true;
            Assert.IsTrue(a.GetTruthValue(truthValues));
        }

        [TestMethod()]
        public void toNandTest()
        {
            Variable p = new Variable('p');

            Assert.AreEqual(p, p.toNand());
        }

        [TestMethod()]
        public void ToStringTest()
        {
            for (char c = 'A'; c <= 'Z'; c++)
            {
                Variable p = new Variable(c);
                Assert.AreEqual(c.ToString(), p.ToString());
            }
            for (char c = 'a'; c <= 'z'; c++)
            {
                Variable p = new Variable(c);
                Assert.AreEqual(c.ToString(), p.ToString());
            }
        }
    }
}