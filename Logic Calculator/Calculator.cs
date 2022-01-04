using System.Collections.Generic;
using System.Linq;

namespace UseYourBrainLogicLib.LogicCalculator
{
    // This class is used mainly for DNF conversion

    // HOWEVER, after moving the DNF functions to class TruthTable
    // this class is not used anymore.
    // Will be updated (removed) soon.



    public class Calculator
    {
        /*
         * Generate the DNF from the full truth table
         * **/
        #region Full DNF Methods

        /// <summary>
        /// Generate a DNF clause from a row of the truth table
        /// </summary>
        /// <param name="header">string[] that stores variables' name</param>
        /// <param name="row">a row of the truth table</param>
        /// <returns>The clause in prefix form of the row of full truth table</returns>
        public string ConvertToFullDNFClause(string[] header, string row)
        {
            string clause = "";

            if (row.Length == 2)
            {
                string literal = (row[0] == '0' ? "~(" + header[0] + ")" : header[0]);
                return literal;
            }

            // for, exclude the result column
            for (int j = 0; j < row.Length - 1; j++)
            {
                string literal = (row[j] == '0' ? "~(" + header[j] + ")" : header[j]);

                if (j != row.Length - 2) // j is not the last variable
                    clause = clause + (j == 0 ? "" : ", ") + "&(" + literal;
                else
                    clause = clause + ", " + literal;
            }

            for (int j = 0; j < row.Length - 2; j++)
                clause += ')';

            return clause;
        }

        /// <summary>
        /// Generate the DNF in prefix form, from the truth table
        /// </summary>
        /// <param name="truthTable">The truth table</param>
        /// <returns>Full DNF in prefix form</returns>
        public string ConvertToFullDNF(TruthTable truthTable)
        {
            string[] header = truthTable.GenerateHeaders();

            List<string> rows = truthTable.GenerateRows(true); // row with result

            // filter out the 0 rows
            rows = rows.Where(row => row.Last() == '1').ToList();

            // Tautology
            if (truthTable.Result.Contains("0") == false)
            {
                return "|(a, ~(a))";
            }
            // Contradiction
            if (truthTable.Result.Contains("1") == false)
            {
                return "&(a, ~(a))";
            }

            string expression = "";

            for (int i = 0; i < rows.Count; i++)
            {
                string clause = ConvertToFullDNFClause(header, rows[i]);

                if (rows.Count == 1)
                    return string.Format("{0}", clause);

                if (i != rows.Count - 1)
                    expression = expression + "|(" + clause + ",";
                else
                    expression += clause;
            }

            for (int i = 0; i < rows.Count - 1; i++)
                expression += ")";

            return expression;
        }

        #endregion


        /*
         * Generate the DNF from the simplified truth table
         * **/
        #region DNF

        /// <summary>
        /// Generate a DNF clause from a row of the simplified truth table
        /// </summary>
        /// <param name="header">string[] that stores variables' name</param>
        /// <param name="row">a row of the truth table</param>
        /// <returns>The clause in prefix form of the row</returns>
        public string ConvertToDNFClause(string[] header, string row)
        {
            // Get fixed variables (that are not *)
            List<int> fixedVar = new List<int>();

            for (int i = 0; i < header.Length - 1; i++)
                if (row[i] != '*')
                    fixedVar.Add(i);

            if (fixedVar.Count == 1)
            {
                // fixed var is 0
                if (row[fixedVar[0]] == '0')
                    return "~(" + header[fixedVar[0]] + ")";

                return header[fixedVar[0]];
            }

            string clause = "";
            for (int i = 0; i < fixedVar.Count; i++)
            {
                int j = fixedVar[i];
                string literal = (row[j] == '0' ? "~(" + header[j] + ")" : header[j]);

                if (i != fixedVar.Count - 1) // j is not the last variable
                    clause = clause + (i == 0 ? "" : ", ") + "&(" + literal;
                else
                    clause = clause + ", " + literal;
            }

            for (int j = 0; j < fixedVar.Count - 1; j++)
                clause += ')';

            return clause;
        }

        /// <summary>
        /// Generate the DNF in prefix form, from the simplified truth table
        /// </summary>
        /// <param name="truthTable">The truth table</param>
        /// <returns>DNF in prefix form</returns>
        public string ConvertToDNF(TruthTable truthTable)
        {
            string[] header = truthTable.GenerateHeaders();
            List<string> rows = truthTable.GenerateSimplifiedRows();

            // filter out the 0 rows
            rows = rows.Where(row => row.Last() == '1').ToList();

            // Tautology
            if (truthTable.Result.Contains("0") == false)
            {
                return "|(a, ~(a))";
            }
            // Contradiction
            if (truthTable.Result.Contains("1") == false)
            {
                return "&(a, ~(a))";
            }

            string expression = "";

            // Convert each row to a DNF clause
            for (int i = 0; i < rows.Count; i++)
            {
                string clause = ConvertToDNFClause(header, rows[i]);

                // only one clause
                if (rows.Count == 1)
                    return string.Format("{0}", clause);

                if (i != rows.Count - 1)
                    expression = expression + "|(" + clause + ",";
                else // last row
                    expression += clause;
            }

            for (int i = 0; i < rows.Count - 1; i++)
                expression += ")";

            return expression;
        }
        #endregion

    }
}
