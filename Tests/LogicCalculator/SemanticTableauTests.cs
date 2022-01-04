using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using UseYourBrainLogicLib.Logic_Components;

namespace UseYourBrainLogicLib.LogicCalculator.Tests
{
    [TestClass()]
    public class SemanticTableauTests
    {
        [TestMethod()]
        public void SemanticTableauTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void BuildTest()
        {
            AbstractionSyntaxTree ast =
                new AbstractionSyntaxTree("|(&(a, ~(a)), &(b, ~(b)))");

            SemanticTableau tableau = new SemanticTableau(ast.Root);
            Assert.AreEqual(-1, tableau.State);

            tableau.Build();
            Assert.AreEqual(0, tableau.State);

            // test 02
            ast =
                new AbstractionSyntaxTree("|(|(a, ~(a)), |(b, ~(b)))");
            tableau = new SemanticTableau(ast.Root);
            Assert.AreEqual(-1, tableau.State);

            tableau.Build();
            Assert.AreEqual(1, tableau.State);

            // test 03
            ast =
                new AbstractionSyntaxTree(">(=(>(a,b), |(~(a), b)), =(|(~(x),y), >(x, y)))");
            tableau = new SemanticTableau(ast.Root);
            Assert.AreEqual(-1, tableau.State);

            tableau.Build();
            Assert.AreEqual(1, tableau.State);

            // test 04
            ast =
                new AbstractionSyntaxTree(">(!x.(@y.(P(x,y))),@q.(!p.(P(p,q))))");
            tableau = new SemanticTableau(ast.Root);
            Assert.AreEqual(-1, tableau.State);

            tableau.Build();
            Assert.AreEqual(1, tableau.State);

            // test 05
            ast =
                new AbstractionSyntaxTree("|(@x.(@y.(@z.(@x.(P(x,y,z))))), !x.(!y.(!z.(!x.(P(x,y,z))))))");
            tableau = new SemanticTableau(ast.Root);
            Assert.AreEqual(-1, tableau.State);

            tableau.Build();
            Assert.AreEqual(0, tableau.State);

            // test 06
            ast =
                new AbstractionSyntaxTree("&(|(~(@x.(&(F(x), G(x)))), &(@x.(F(x)), @x.(G(x)))), |(@x.(&(F(x), G(x))), ~(&(@x.(F(x)), @x.(G(x))))))");
            tableau = new SemanticTableau(ast.Root);
            Assert.AreEqual(-1, tableau.State);

            tableau.Build();
            Assert.AreEqual(1, tableau.State);
        }

        [TestMethod()]
        public void BuildUtilTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ExpandAllAndsTest()
        {
            Variable A = new Variable('a');
            Variable B = new Variable('b');

            // test 01
            Symbol t1_A = new And(A, B);
            Symbol t1_B = new And(B, A);

            HashSet<Symbol> t1_InputSet = new HashSet<Symbol>()
            {
                t1_A, t1_A, t1_A, t1_B, t1_B
            };

            var t1_expected = new HashSet<Symbol>()
            {
                A, B
            };

            var t1_res = SemanticTableau.ExpandAllAnds(new TableauNode(t1_InputSet), true, false);

            Assert.AreEqual(t1_expected.Count(), t1_res.Count());

            foreach (Symbol s in t1_expected)
            {
                Assert.IsTrue(t1_res.Contains(s));
            }
        }

        [TestMethod()]
        public void CheckContradictionTest()
        {
            Variable A = new Variable('a');
            Variable B = new Variable('b');

            // test 01
            Symbol t1_A = new And(A, B);
            Symbol t1_B = new And(A, B);

            HashSet<Symbol> t1_InputSet = new HashSet<Symbol>()
            {
                t1_A, t1_A, t1_A, t1_B, t1_B
            };
            bool t1_contradiction =
                SemanticTableau.CheckContradiction(t1_InputSet);
            Assert.IsFalse(t1_contradiction);

            // test 02
            Symbol t2_A = new And(A, B);
            HashSet<Symbol> t2_InputSet = new HashSet<Symbol>()
            {
                t2_A, A, B, new Not(A)
            };
            bool t2_contradiction =
                SemanticTableau.CheckContradiction(t2_InputSet);
            Assert.IsTrue(t2_contradiction);
        }

        [TestMethod()]
        /**
         * A ^ B -> A,B
         * ~~A -> A
         * ~(A v B) = ~A ^ ~B
         * ~(A => B) = A ^ ~B
         */
        public void ExpandToAndTest()
        {
            Symbol s;
            Variable A = new Variable('a');
            Variable B = new Variable('b');
            List<Symbol> res;
            List<Symbol> expected;

            // test 01
            s = new And(A, B);
            res = SemanticTableau.ExpandToAnd(s);
            expected = new List<Symbol>() { A, B };

            Assert.AreEqual(expected.Count(), res.Count());
            for (int i = 0; i < expected.Count(); i++)
                Assert.AreEqual(expected[i].ToString(), res[i].ToString());

            // test 02: ~~A -> A
            s = new Not(new Not(A));
            res = SemanticTableau.ExpandToAnd(s);
            expected = new List<Symbol>() { A };

            Assert.AreEqual(expected.Count(), res.Count());
            for (int i = 0; i < expected.Count(); i++)
                Assert.AreEqual(expected[i].ToString(), res[i].ToString());

            // test 03: ~(A v B) = ~A ^ ~B
            s = new Not(new Or(A, B));
            res = SemanticTableau.ExpandToAnd(s);
            expected = new List<Symbol>() {
                new Not(A),
                new Not(B)
            };

            Assert.AreEqual(expected.Count(), res.Count());
            for (int i = 0; i < expected.Count(); i++)
                Assert.AreEqual(expected[i].ToString(), res[i].ToString());

            // test 04: ~(A => B) = A ^ ~B
            s = new Not(new Implication(A, B));
            res = SemanticTableau.ExpandToAnd(s);
            expected = new List<Symbol>() {
                A,
                new Not(B)
            };

            Assert.AreEqual(expected.Count(), res.Count());
            for (int i = 0; i < expected.Count(); i++)
                Assert.AreEqual(expected[i].ToString(), res[i].ToString());
        }

        [TestMethod()]
        /**
         * The result of the expansion is an Or,
         * which split the current node into several branches
         * 
         * A v B -> A v B (2 branches)
         * ~(A ^ B) = ~A v ~B
         * ~(A <=> B) = (A ^ ~B) v (~A ^ B)
         * 
         * A => B = ~A v B
         * A <=> B = (A ^ B) v (~A ^ ~B)
         * 
         */
        public void ExpandToOrTest()
        {
            Symbol s;
            Variable A = new Variable('a');
            Variable B = new Variable('b');
            List<Symbol> res;
            List<Symbol> expected;

            // test 01
            s = new Or(A, B);
            res = SemanticTableau.ExpandToOr(s);
            expected = new List<Symbol>() { A, B };

            Assert.AreEqual(expected.Count(), res.Count());
            for (int i = 0; i < expected.Count(); i++)
                Assert.AreEqual(expected[i], res[i]);

            // test 01
            s = new Or(A, B);
            res = SemanticTableau.ExpandToOr(s);
            expected = new List<Symbol>() { A, B };

            Assert.AreEqual(expected.Count(), res.Count());
            for (int i = 0; i < expected.Count(); i++)
                Assert.AreEqual(expected[i], res[i]);

            // test 02
            s = new Not(new And(A, B));
            res = SemanticTableau.ExpandToOr(s);
            expected = new List<Symbol>() { new Not(A), new Not(B) };

            Assert.AreEqual(expected.Count(), res.Count());
            for (int i = 0; i < expected.Count(); i++)
                Assert.AreEqual(expected[i].ToString(), res[i].ToString());

            // test 03: ~(A <=> B) = (A ^ ~B) v (~A ^ B)
            s = new Not(new BiImplication(A, B));
            res = SemanticTableau.ExpandToOr(s);
            expected = new List<Symbol>() {
                new And(A, new Not(B)),
                new And(new Not(A), B)
            };

            Assert.AreEqual(expected.Count(), res.Count());
            for (int i = 0; i < expected.Count(); i++)
                Assert.AreEqual(expected[i].ToString(), res[i].ToString());

            // test 04: A => B = ~A v B
            s = new Implication(A, B);
            res = SemanticTableau.ExpandToOr(s);

            expected = new List<Symbol>() {
                new Not(A),
                B
            };

            Assert.AreEqual(expected.Count(), res.Count());
            for (int i = 0; i < expected.Count(); i++)
                Assert.AreEqual(expected[i].ToString(), res[i].ToString());

            // test 05: A <=> B = (A ^ B) v (~A ^ ~B)
            s = new BiImplication(A, B);
            res = SemanticTableau.ExpandToOr(s);

            expected = new List<Symbol>() {
                new And(A,B),
                new And(new Not(A), new Not(B))
            };

            Assert.AreEqual(expected.Count(), res.Count());
            for (int i = 0; i < expected.Count(); i++)
                Assert.AreEqual(expected[i].ToString(), res[i].ToString());
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
    }
}