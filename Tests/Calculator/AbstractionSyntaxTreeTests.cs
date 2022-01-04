using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

using UseYourBrainLogicLib.Logic_Components;

namespace UseYourBrainLogicLib.LogicCalculator.Tests
{
    [TestClass()]
    public class AbstractionSyntaxTreeTests
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        [TestMethod()]
        public void AbstractionSyntaxTreeTest()
        {
            AbstractionSyntaxTree ast = new AbstractionSyntaxTree();
            Assert.IsNotNull(ast.ListVariable);
        }

        /// <summary>
        /// Constructor that takes Expression
        /// </summary>
        [TestMethod()]
        public void AbstractionSyntaxTreeTest1()
        {
            string expression = "%(%(%(p,p), %(q,q)), %(p,q))";
            AbstractionSyntaxTree ast = new AbstractionSyntaxTree(expression);
            Assert.IsNotNull(ast.ListVariable);
            Assert.AreEqual("(((p % p) % (q % q)) % (p % q))", ast.ToString());
        }

        /// <summary>
        /// Constructor that takes a Symbol root
        /// </summary>
        [TestMethod()]
        public void AbstractionSyntaxTreeTest2()
        {
            Variable p = new Variable('p');
            Variable q = new Variable('q');
            And root = new And(p, q);

            Not Left = new Not(new Nand(p, q));
            Or Right = new Or(p, q);

            root.Operate(Left, Right);

            // (~(p % q) & (p | q))"

            AbstractionSyntaxTree ast = new AbstractionSyntaxTree(root);
            Assert.AreEqual(root.ToString(), ast.Root.ToString());
        }

        /*
         * Need to add test that have And and Constant and upper case variable
         * that throws no exception.
         * 
         * **/
        [TestMethod()]
        public void BuildTest()
        {
            Variable p = new Variable('p');
            Variable q = new Variable('q');
            And root = new And(p, q);

            Not Left = new Not(new Nand(p, q));
            Or Right = new Or(p, q);

            root.Operate(Left, Right);

            // (~(p % q) & (p | q))"

            // perform
            AbstractionSyntaxTree ast = new AbstractionSyntaxTree("&(~(%(p,q)), |(p,q))");
            ast.Build();
            Assert.AreEqual(root.ToString(), ast.Root.ToString());
            Assert.IsTrue(ast.IsProposition);
            Assert.IsFalse(ast.HasQuantifier);

            // Test 02
            Assert.ThrowsException<ArgumentException>(
                () => ast = new AbstractionSyntaxTree("~(a,a)"));

            // Test 03
            Assert.ThrowsException<ArgumentException>(
                () => ast = new AbstractionSyntaxTree("|(a)"));

            // Test 04
            Assert.ThrowsException<ArgumentException>(
                () => ast = new AbstractionSyntaxTree("|(a,a))"));

            // Test 05
            Assert.ThrowsException<ArgumentException>(
                () => ast = new AbstractionSyntaxTree("|(aa,a))"));

            // Test 06
            Assert.ThrowsException<ArgumentException>(
                () => ast = new AbstractionSyntaxTree("&(a,a) &(B, C)"));

            // Test 07
            Assert.ThrowsException<ArgumentException>(
                () => ast = new AbstractionSyntaxTree("&(a,a) &(B, C)"));

            // Test 08
            Assert.ThrowsException<ArgumentException>(
                () => ast = new AbstractionSyntaxTree("&(a,a) #(B, C)"));

            // Test 09
            Assert.ThrowsException<ArgumentException>(
                () => ast = new AbstractionSyntaxTree("#(a,b)"));

            // Test 09
            Assert.ThrowsException<ArgumentException>(
                () => ast = new AbstractionSyntaxTree("&)a,b("));

            // Test 11
            Assert.ThrowsException<ArgumentException>(
                () => ast = new AbstractionSyntaxTree("&(b,,,,,,,, ,,,, ,, , ,, a)"));

            // test 12
            string expression = "!x.(@y.(&(P(x, x), Q(y, x))))";
            ast = new AbstractionSyntaxTree(expression);
            ast.Build();
            Assert.AreEqual("!x.(@y.((P(x,x) & Q(y,x))))", ast.ToString());
            Assert.IsFalse(ast.IsProposition);
            Assert.IsTrue(ast.HasQuantifier);

            // test 13
            expression = "!x.(@y.(&(P(x, x, y, x), Q(y, x))))";
            ast = new AbstractionSyntaxTree(expression);
            ast.Build();
            Assert.AreEqual("!x.(@y.((P(x,x,y,x) & Q(y,x))))", ast.ToString());
            Assert.IsFalse(ast.IsProposition);
            Assert.IsTrue(ast.HasQuantifier);
        }

        [TestMethod()]
        public void EvaluateDictTest()
        {
            Dictionary<char, bool> truthValues = new Dictionary<char, bool>();

            // test 01
            string expression = "%(%(%(p,p), %(q,q)), %(p,q))";
            AbstractionSyntaxTree ast = new AbstractionSyntaxTree(expression);
            ast.Build();

            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    truthValues['p'] = i == 1;
                    truthValues['q'] = j == 1;

                    bool val = ast.Evaluate(truthValues);
                    Assert.AreEqual(i == j, val);
                }
            }
        }

        [TestMethod()]
        public void EvaluateArrayTest()
        {
            bool[] truthValues = new bool[130];

            // test 01
            string expression = "%(%(%(p,p), %(q,q)), %(p,q))";
            AbstractionSyntaxTree ast = new AbstractionSyntaxTree(expression);
            ast.Build();

            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    truthValues['p'] = i == 1;
                    truthValues['q'] = j == 1;

                    bool val = ast.Evaluate(truthValues);
                    Assert.AreEqual(i == j, val);
                }
            }
        }

        [TestMethod()]
        public void GenerateGraphTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GenerateGraphUtilTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ToNandTest()
        {
            // test 01
            string expression = "&(~(%(p,q)), |(p, q))";
            AbstractionSyntaxTree ast = new AbstractionSyntaxTree(expression);
            ast.Build();

            Symbol s = ast.ToNand();
            Assert.AreEqual("((((p % q) % (p % q)) % ((p % p) % (q % q))) % (((p % q) % (p % q)) % ((p % p) % (q % q))))", ast.ToNand().ToString());
            
            TruthTable tb = new TruthTable(ast);
            string oriHash = tb.GetHash();
            AbstractionSyntaxTree nandTree = ObjectExtensions.Copy(ast);
            nandTree.Root = nandTree.Root.toNand();
            TruthTable nandTB = new TruthTable(nandTree);
            string nandHash = nandTB.GetHash();

            // test 02
            expression = "=(p,q)";
            ast = new AbstractionSyntaxTree(expression);
            ast.Build();

            s = ast.ToNand();
            Assert.AreEqual("(((p % p) % (q % q)) % (p % q))", ast.ToNand().ToString());

            tb = new TruthTable(ast);
            oriHash = tb.GetHash();
            nandTree = ObjectExtensions.Copy(ast);
            nandTree.Root = nandTree.Root.toNand();
            nandTB = new TruthTable(nandTree);
            nandHash = nandTB.GetHash();

            // test 03
            expression = ">(p, q)";
            ast = new AbstractionSyntaxTree(expression);
            ast.Build();

            s = ast.ToNand();
            Assert.AreEqual("(p % (q % q))", ast.ToNand().ToString());

            tb = new TruthTable(ast);
            oriHash = tb.GetHash();
            nandTree = ObjectExtensions.Copy(ast);
            nandTree.Root = nandTree.Root.toNand();
            nandTB = new TruthTable(nandTree);
            nandHash = nandTB.GetHash();

            // test 04
            expression = "!x.(@y.(&(P(x, x), Q(y, x))))";
            ast = new AbstractionSyntaxTree(expression);
            ast.Build();

            s = ast.ToNand();
            Assert.AreEqual("!x.(@y.(((P(x,x) % Q(y,x)) % (P(x,x) % Q(y,x)))))", ast.ToNand().ToString());

            tb = new TruthTable(ast);
            oriHash = tb.GetHash();
            nandTree = ObjectExtensions.Copy(ast);
            nandTree.Root = nandTree.Root.toNand();
            nandTB = new TruthTable(nandTree);
            nandHash = nandTB.GetHash();

            // test 05
            expression = "!x.(@y.(&(P(x, x, y, x), Q(y, x))))";
            ast = new AbstractionSyntaxTree(expression);
            ast.Build();

            s = ast.ToNand();
            Assert.AreEqual("!x.(@y.(((P(x,x,y,x) % Q(y,x)) % (P(x,x,y,x) % Q(y,x)))))", ast.ToNand().ToString());

            tb = new TruthTable(ast);
            oriHash = tb.GetHash();
            nandTree = ObjectExtensions.Copy(ast);
            nandTree.Root = nandTree.Root.toNand();
            nandTB = new TruthTable(nandTree);
            nandHash = nandTB.GetHash();
        }

        [TestMethod()]
        public void ToStringTest()
        {
            // test 01
            string expression = "%(p, %(q,q))";
            AbstractionSyntaxTree ast = new AbstractionSyntaxTree(expression);
            ast.Build();

            Symbol s = ast.ToNand();
            Assert.AreEqual("(p % (q % q))", ast.ToNand().ToString());

            // test 02
            expression = "%(%(%(p,p), %(q,q)), %(p,q))";
            ast = new AbstractionSyntaxTree(expression);
            ast.Build();

            s = ast.ToNand();
            Assert.AreEqual("(((p % p) % (q % q)) % (p % q))", ast.ToNand().ToString());

            // test 02
            ast = new AbstractionSyntaxTree();
            Assert.ThrowsException<ArgumentNullException>(
                () => ast.ToString());
        }

        [TestMethod()]
        public void IsPropositionTest()
        {
            string expression;
            AbstractionSyntaxTree ast;

            // test 01
            expression = "!x.(@y.(&(P(x, x), Q(y, x))))";
            ast = new AbstractionSyntaxTree(expression);
            ast.Build();
            Assert.IsFalse(ast.IsProposition);

            // test 02
            expression = "!x.(@y.(&(P(x, x, y, x), Q(y, x))))";
            ast = new AbstractionSyntaxTree(expression);
            ast.Build();
            Assert.IsFalse(ast.IsProposition);

            // test 02
            expression = "|(a,&(b,%(c,>(d,=(e,~(f))))))";
            ast = new AbstractionSyntaxTree(expression);
            ast.Build();
            Assert.IsTrue(ast.IsProposition);
        }
    }
}