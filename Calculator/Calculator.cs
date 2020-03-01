using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseYourBrain.Calculator
{
    class LogicCalculator
    {
        private string ConvertToFullDNFClause(string[] header, string row)
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

        public string ConvertToFullDNF(TruthTable truthTable)
        {
            string[] header = truthTable.GenerateHeaders();

            List<string> rows = truthTable.GenerateRows(true);
            // filter out the 0 rows
            rows = rows.Where(row => row.Last() == '1').ToList();

            string expression = "";

            for(int i = 0; i < rows.Count; i++)
            {
                string clause = ConvertToFullDNFClause(header, rows[i]);

                if(rows.Count == 1)
                    return string.Format("({0})", clause);

                if (i != rows.Count - 1)
                    expression = expression + "|(" + clause + ",";
                else
                    expression += clause;
            }

            for (int i = 0; i < rows.Count - 1; i++)
                expression += ")";

            return expression;
        }

        public string ConvertToDNFClause(string[] header, string row)
        {
            List<int> fixedVar = new List<int>();

            for (int i = 0; i < header.Length - 1; i++)
                if (row[i] != '*')
                    fixedVar.Add(i);

            if (fixedVar.Count == 1)
            {
                string literal = (row[fixedVar[0]] == '0' ? "~(" + header[fixedVar[0]] + ")" : header[fixedVar[0]]);
                return literal;
            }

            string clause = "";
            for (int i = 0; i < fixedVar.Count; i++)
            {
                int j = fixedVar[i];
                string literal = (row[j] == '0' ? "~(" + header[j] + ")" : header[j]);

                if (j != fixedVar.Count - 1) // j is not the last variable
                    clause = clause + (j == 0 ? "" : ", ") + "&(" + literal;
                else
                    clause = clause + ", " + literal;
            }

            for (int j = 0; j < fixedVar.Count - 1; j++)
                clause += ')';

            return clause;
        }

        public string ConvertToDNF(TruthTable truthTable)
        {
            string[] header = truthTable.GenerateHeaders();
            List<string> rows = truthTable.GenerateSimplifiedRows();

            // filter out the 0 rows
            rows = rows.Where(row => row.Last() == '1').ToList();

            if(truthTable.Result.Contains("0") == false)
            {
                return "|(a, ~(a))";
            }
            if(truthTable.Result.Contains("1") == false)
            {
                return "&(a, ~(a))";
            }

            string expression = "";

            for (int i = 0; i < rows.Count; i++)
            {
                string clause = ConvertToDNFClause(header, rows[i]);

                if (rows.Count == 1)
                    return string.Format("({0})", clause);

                if (i != rows.Count - 1)
                    expression = expression + "|(" + clause + ",";
                else
                    expression += clause;
            }

            for (int i = 0; i < rows.Count - 1; i++)
                expression += ")";

            return expression;
        }
    }
}
