using System;
using System.Collections.Generic;
using System.Linq;
using UseYourBrainLogicLib.Logic_Components;

namespace UseYourBrainLogicLib.LogicCalculator
{
    /**
     * In this truth table, I value space over performace.
     * Therefore, I will not store the rows of the TruthTable,
     * but instead generate them when needed.
     * 
     * 
     * Time complexity: O(2^|var|)
     * Time complexity for generating simplified table:
     * O(2^|var|*log(2^|var|))
     * 
     * Space complexity: O(2^|var| * |var|)
     * 
     * When |var| <= 20, it does not matter 
     * 
     * However, when |var| > 20, save the generated rows result
     * will hugely reduce the time generating simplified table
     */
    public class TruthTable
    {
        /// <summary>
        /// Concatenation of the truth table's result 
        /// assuming the rows are sorted in lexicographical order.
        /// result[0] is the first row's result.
        /// </summary>
        public string Result { get; private set; }

        /// <summary>
        /// The result is calculated or not
        /// </summary>
        public bool isCalulated { get; private set; }
        public AbstractionSyntaxTree AST { get; set; }

        public string GetHash()
        {
            if (!isCalulated)
                Calculate();

            // have to reverse bcz row 1 is currently at Result[0]
            // row 1 should be the LSB in binary form
            char[] charArr = Result.ToCharArray();
            Array.Reverse(charArr);

            string strHex = Convert.ToInt64(new string(charArr), 2).ToString("X");

            return strHex;
        }

        public TruthTable(AbstractionSyntaxTree ast)
        {
            this.AST = ast;
            isCalulated = false;
        }

        /// <summary>
        /// Calculate the result of the truth table
        /// </summary>
        public void Calculate()
        {
            if (isCalulated)
                return;

            List<char> vars = new List<char>(AST.ListVariable);
            vars.Reverse();

            bool[] truthValues = new bool[130];

            for (int mask = 0; mask < (1 << vars.Count); mask++)
            {
                for (int pos = 0; pos < vars.Count; pos++)
                {
                    truthValues[vars[pos]] = (((mask >> pos) & 1) == 1);
                }
                string currentRowResult = (AST.Evaluate(truthValues) ? "1" : "0");
                Result += currentRowResult.ToString();
            }

            isCalulated = true;
        }

        /// <summary>
        /// Generate header for DataGridView
        /// </summary>
        /// <returns>string[] headers</returns>
        public string[] GenerateHeaders()
        {
            string[] headers = new string[AST.ListVariable.Count + 1];

            for (int i = 0; i < AST.ListVariable.Count; i++)
            {
                headers[i] = AST.ListVariable[i].ToString();
            }

            headers[headers.Length - 1] = AST.ToString();

            return headers;
        }

        /// <summary>
        /// Generate all truth table rows in DataGridView format
        /// </summary>
        /// <param name="withHeader">To include header row also.</param>
        /// <returns>List of string[nVar + 1], DataGridView format row</returns>
        public List<string[]> GenerateDataGridViewData(List<string> listRows, bool withHeader)
        {
            // List to return
            List<string[]> rows = new List<string[]>();

            // Add header
            if (withHeader)
            {
                string[] headers = GenerateHeaders();
                rows.Add(headers);
            }

            foreach (string row in listRows)
            {
                string[] DGVRow = new string[row.Length];

                for (int i = 0; i < row.Length; i++)
                    DGVRow[i] = row[i].ToString();

                rows.Add(DGVRow);
            }

            return rows;
        }



        #region Generate Rows

        /// <summary>
        /// Generate a list of all rows in truth table 
        /// in the form of a binary string.
        /// </summary>
        /// <param name="withResult">Whether to add the result column (last one) or not</param>
        /// <returns>A list of binary string in lexicographical order.</returns>
        public List<string> GenerateRows(bool withResult)
        {
            if (withResult && !isCalulated)
                this.Calculate();

            List<string> listRows = new List<string>();

            List<char> vars = new List<char>(AST.ListVariable);

            // loop through all posible binary string
            for (int mask = 0; mask < (1 << vars.Count); mask++)
            {
                string curRow = "";

                for (int pos = 0; pos < vars.Count; pos++)
                {
                    bool value = (((mask >> pos) & 1) == 1);
                    curRow += (value == true ? "1" : "0");
                }

                // reverse the cur row
                // bcz if not, it would be 00 10 01 11
                // but we want             00 01 10 11
                char[] charArr = curRow.ToCharArray();
                Array.Reverse(charArr);
                curRow = new string(charArr);

                if (withResult)
                    curRow += Result[mask];

                listRows.Add(curRow);
            }

            return listRows;
        }

        /// <summary>
        /// Generate the simplified version of the truth table
        /// </summary>
        /// <returns></returns>
        public List<string> GenerateSimplifiedRows()
        {
            HashSet<string> setRows = new HashSet<string>(GenerateRows(true));

            // number of iteration, there are maximum |variable| stars
            int nIter = AST.ListVariable.Count;

            while (nIter-- > 0)
            {
                HashSet<string> simplified = new HashSet<string>();

                // convert to list for easier iteration
                List<string> tmpSetRows = setRows.ToList();

                // to Mark the row that is considered
                bool[] considered = new bool[setRows.Count];

                // Loop O(N^2 * |Length|) to find pairs that differ by strictly 1 character
                for (int i = 0; i < tmpSetRows.Count; i++)
                {
                    string a = tmpSetRows[i]; // i row
                    bool hasDuplicate = false;

                    for (int j = i + 1; j < tmpSetRows.Count; j++)
                    {
                        string b = tmpSetRows[j]; // j row
                        int diff = -1; // the index of the different character

                        for (int k = 0; k < a.Length; k++) // loop O(|length|) characters
                        {
                            if (a[k] != b[k]) // find the different character
                            {
                                // this character is not the first different one
                                if (diff != -1)
                                {
                                    diff = -1;
                                    break;
                                }
                                // else, diff stores the index
                                diff = k;
                            }
                        }

                        // There is 1 different character
                        if (diff != -1)
                        {
                            hasDuplicate = true;
                            considered[j] = true;

                            // Add the row with the different char replaced by "*"
                            // to the simplified list
                            char[] tmp = a.ToCharArray(); // Have to do this bcz string is immutable =='
                            tmp[diff] = '*';
                            simplified.Add(new string(tmp));
                        }
                    }

                    // this row stands alone :((( ;_; 
                    if (!hasDuplicate && !considered[i])
                        simplified.Add(a);
                }

                // Replace the old simplified list (setRows) by the new one, and
                // remove duplicates using HashSet, proved to be more efficient than .Distinct()
                setRows = new HashSet<string>(simplified);
            }

            return setRows.ToList();
        }

        #endregion

        #region Generate DNF

        public Symbol ToFullDNF()
        {
            string[] headers = GenerateHeaders();
            List<string> rows = GenerateRows(true);
            rows = rows.Where(row => row.Last() == '1').ToList();

            // Tautology
            if (Result.Contains("0") == false)
            {
                return new Constant('1');
            }
            // Contradiction
            if (Result.Contains("1") == false)
            {
                return new Constant('0');
            }

            Or expression = new Or();
            List<Symbol> listClauses = new List<Symbol>();

            foreach (var row in rows)
            {
                And clause = new And();

                for (int i = 0; i < headers.Length - 1; i++)
                {
                    Symbol literal = new Variable(headers[i][0]);

                    if (row[i] == '0')
                        literal = new Not(literal);

                    if (headers.Length - 1 == 1)
                    {
                        listClauses.Add(literal);
                        break;
                    }

                    if (i == 0)
                    {
                        clause.Operate(literal, new Placeholder('#'));
                    }
                    else
                    {
                        clause.Operate(clause.Childs[0], literal);
                        if (i != headers.Length - 2)
                            clause = new And(clause, new Placeholder('#'));
                    }
                }

                if (headers.Length - 1 != 1)
                    listClauses.Add(clause);
            }

            if (listClauses.Count() == 1)
                return listClauses[0];

            for (int i = 0; i < listClauses.Count(); i++)
            {
                if (i == 0)
                {
                    expression.Operate(listClauses[i], new Placeholder('#'));
                }
                else
                {
                    expression.Operate(expression.Childs[0], listClauses[i]);

                    if (i != listClauses.Count() - 1)
                        expression = new Or(expression, new Placeholder('#'));
                }
            }

            return expression;
        }

        public Symbol ToDNF()
        {
            string[] headers = GenerateHeaders();
            List<string> rows = GenerateSimplifiedRows();

            // filter out the 0 rows
            rows = rows.Where(row => row.Last() == '1').ToList();

            // Tautology
            if (Result.Contains("0") == false)
            {
                return new Constant('1');
            }
            // Contradiction
            if (Result.Contains("1") == false)
            {
                return new Constant('0');
            }

            Or expression = new Or();
            List<Symbol> listClauses = new List<Symbol>();

            foreach (var row in rows)
            {
                int numNonFree = 0;
                int curNonFree = 0;
                And clause = new And();

                for (int i = 0; i < headers.Length - 1; i++)
                    if (row[i] != '*')
                        numNonFree++;

                for (int i = 0; i < headers.Length - 1; i++)
                {
                    if (row[i] == '*')
                        continue;
                    curNonFree++;

                    Symbol literal = new Variable(headers[i][0]);

                    if (row[i] == '0')
                        literal = new Not(literal);

                    if (numNonFree == 1)
                    {
                        listClauses.Add(literal);
                        break;
                    }

                    if (curNonFree == 1)
                    {
                        clause.Operate(literal, new Placeholder('#'));
                    }
                    else
                    {
                        clause.Operate(clause.Childs[0], literal);
                        if (curNonFree != numNonFree)
                            clause = new And(clause, new Placeholder('#'));
                    }
                }

                if (numNonFree != 1)
                    listClauses.Add(clause);
            }

            if (listClauses.Count() == 1)
                return listClauses[0];

            for (int i = 0; i < listClauses.Count(); i++)
            {
                if (i == 0)
                {
                    expression.Operate(listClauses[i], new Placeholder('#'));
                }
                else
                {
                    expression.Operate(expression.Childs[0], listClauses[i]);

                    if (i != listClauses.Count() - 1)
                        expression = new Or(expression, new Placeholder('#'));
                }
            }

            return expression;
        }
        #endregion
    }
}
