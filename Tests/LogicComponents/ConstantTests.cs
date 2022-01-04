using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace UseYourBrainLogicLib.Logic_Components.Tests
{
    [TestClass()]
    public class ConstantTests
    {
        [TestMethod()]
        public void ConstantTest()
        {
            Constant a = new Constant('0');
            Assert.IsNotNull(a);
            Assert.AreEqual('0', a.Name);
            Assert.AreEqual(SymbolType.primitive, a.Type);
            Assert.AreEqual(false, a.Value);

            a = new Constant('1');
            Assert.IsNotNull(a);
            Assert.AreEqual('1', a.Name);
            Assert.AreEqual(SymbolType.primitive, a.Type);
            Assert.AreEqual(true, a.Value);


            for (char c = (char)0; c < 256; c++)
            {
                if (c == '0' || c == '1')
                    continue;

                Assert.ThrowsException<Exception>(() => new Constant(c));
            }
        }

        [TestMethod()]
        public void GetTruthValueDictTest()
        {
            Dictionary<char, bool> dict = new Dictionary<char, bool>();

            Constant a = new Constant('1');

            dict['x'] = false;
            Assert.IsTrue(a.GetTruthValue(dict));

            dict['x'] = true;
            Assert.IsTrue(a.GetTruthValue(dict));

            dict.Remove('x');
            Assert.IsTrue(a.GetTruthValue(dict));

            // False
            a = new Constant('0');

            dict['x'] = false;
            Assert.IsFalse(a.GetTruthValue(dict));

            dict['x'] = true;
            Assert.IsFalse(a.GetTruthValue(dict));

            dict.Remove('x');
            Assert.IsFalse(a.GetTruthValue(dict));
        }

        [TestMethod()]
        public void GetTruthValueArrayTest()
        {
            bool[] truthValues = new bool[130];

            Constant a = new Constant('1');

            truthValues['x'] = false;
            Assert.IsTrue(a.GetTruthValue(truthValues));

            truthValues['x'] = true;
            Assert.IsTrue(a.GetTruthValue(truthValues));

            // False
            a = new Constant('0');

            truthValues['x'] = false;
            Assert.IsFalse(a.GetTruthValue(truthValues));

            truthValues['x'] = true;
            Assert.IsFalse(a.GetTruthValue(truthValues));
        }

        [TestMethod()]
        public void toNandTest()
        {
            Constant p = new Constant('0');
            Assert.AreEqual(p, p.toNand());

            p = new Constant('1');
            Assert.AreEqual(p, p.toNand());
        }

        [TestMethod()]
        public void ToStringTest()
        {
            Constant p = new Constant('0');
            Assert.AreEqual("0", p.ToString());

            p = new Constant('1');
            Assert.AreEqual("1", p.ToString());
        }
    }
}