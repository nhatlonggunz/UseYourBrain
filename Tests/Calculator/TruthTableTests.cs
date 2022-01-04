using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace UseYourBrainLogicLib.LogicCalculator.Tests
{
    [TestClass()]
    public class TruthTableTests
    {
        [TestMethod()]
        public void GetHashTest()
        {
            // test 01
            AbstractionSyntaxTree ast = new AbstractionSyntaxTree("=(a,b)");
            TruthTable tb = new TruthTable(ast);

            Assert.AreEqual("9", tb.GetHash());

            // test 02
            ast = new AbstractionSyntaxTree("=(>(a,b),b)");
            tb = new TruthTable(ast);

            Assert.AreEqual("E", tb.GetHash());


            // test 03
            ast = new AbstractionSyntaxTree("|(a,&(b,%(c,>(d,=(e,~(f))))))");
            tb = new TruthTable(ast);

            Assert.AreEqual("FFFFFFFF90FF0000", tb.GetHash());
        }

        [TestMethod()]
        public void TruthTableTest()
        {
            AbstractionSyntaxTree ast = new AbstractionSyntaxTree(">(=(&(p,q),a),b)");
            TruthTable tb = new TruthTable(ast);

            Assert.AreEqual(false, tb.isCalulated);
            Assert.AreEqual(ast, tb.AST);
        }

        [TestMethod()]
        public void CalculateTest()
        {
            // test 01
            AbstractionSyntaxTree ast = new AbstractionSyntaxTree("=(a,b)");
            TruthTable tb = new TruthTable(ast);
            tb.Calculate();

            Assert.AreEqual(true, tb.isCalulated);
            Assert.AreEqual("1001", tb.Result);

            // test 02
            ast = new AbstractionSyntaxTree("=(>(a,b),b)");
            tb = new TruthTable(ast);
            tb.Calculate();

            Assert.AreEqual(true, tb.isCalulated);
            Assert.AreEqual("0111", tb.Result);
        }

        [TestMethod()]
        public void GenerateHeadersTest()
        {
            string expression = "&(&(&(&(&(&(&(&(&(&(&(a,b),c),d),e),f),g),h),i),j),k),l)";
            AbstractionSyntaxTree ast = new AbstractionSyntaxTree(expression);
            TruthTable tb = new TruthTable(ast);

            string[] headers = { "a", "b", "c", "d", "e", "f",
                                 "g", "h", "i", "j", "k", "l", ast.ToString() };
            string[] res = tb.GenerateHeaders();

            for (int i = 0; i < headers.Length; i++)
            {
                Assert.AreEqual(headers[i], res[i]);
            }

            // Assert.AreEqual(headers, res);
        }

        [TestMethod()]
        public void GenerateRowsTest()
        {
            // Test 01
            string expression = "=(a,b)";
            AbstractionSyntaxTree ast = new AbstractionSyntaxTree(expression);
            TruthTable tb = new TruthTable(ast);


            List<string> rows = new List<string>()
            {
                "00", "01", "10", "11"
            };
            string rowsResult = "1001";

            // Rows without result
            List<string> rowsReturned = tb.GenerateRows(false);

            for (int i = 0; i < rows.Count; i++)
            {
                Assert.AreEqual(rows[i], rowsReturned[i]);
            }

            // Rows With Result
            for (int i = 0; i < rows.Count; i++)
            {
                rows[i] = rows[i] + rowsResult[i];
            }

            rowsReturned = tb.GenerateRows(true);

            for (int i = 0; i < rows.Count; i++)
            {
                Assert.AreEqual(rows[i], rowsReturned[i]);
            }

            // End test 01
        }

        [TestMethod()]
        public void GenerateDataGridViewDataTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GenerateSimplifiedRowsTest()
        {
            // Test 01
            string expression = "|(|(a,~(a)), &(%(=(>(~(c),d),e),f), g))";
            AbstractionSyntaxTree ast = new AbstractionSyntaxTree(expression);
            TruthTable tb = new TruthTable(ast);


            List<string> rows = new List<string>() { "******1" };


            // Rows without result
            List<string> rowsReturned = tb.GenerateSimplifiedRows();

            for (int i = 0; i < rows.Count; i++)
            {
                Assert.AreEqual(rows[i], rowsReturned[i]);
            }

            // Test 02
            expression = "&(&(a,~(a)), &(%(=(>(~(c),d),e),f), g))";
            ast = new AbstractionSyntaxTree(expression);
            tb = new TruthTable(ast);

            rows = new List<string>() { "******0" };

            // Rows without result
            rowsReturned = tb.GenerateSimplifiedRows();

            for (int i = 0; i < rows.Count; i++)
            {
                Assert.AreEqual(rows[i], rowsReturned[i]);
            }
        }
    }
}