using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseYourBrain.Calculator
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
    class TruthTable
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
            List<char> vars = AST.listVariable;
            bool[] truthValues = new bool[100];

            for (int mask = 0; mask < (1 << vars.Count); mask++)
            {
                for(int pos = 0; pos < vars.Count; pos++)
                {
                    truthValues[vars[pos] - 'A'] = (((mask >> pos) & 1) == 1);
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
            string[] headers = new string[AST.listVariable.Count + 1];
            
            for(int i = 0; i < AST.listVariable.Count; i++)
            {
                headers[i] = AST.listVariable[i].ToString();
            }

            headers[headers.Length - 1] = AST.ToString();

            return headers;
        }

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

            // loop through all posible binary string
            for (int mask = 0; mask < (1 << AST.listVariable.Count); mask++)
            {
                string curRow = "";

                for (int pos = 0; pos < AST.listVariable.Count; pos++)
                {
                    bool value = (((mask >> pos) & 1) == 1);
                    curRow += (value == true ? "1" : "0");
                }

                if (withResult)
                    curRow += Result[mask];

                listRows.Add(curRow);
            }

            return listRows;
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

            foreach(string row in listRows)
            {
                string[] DGVRow = new string[row.Length];

                for(int i = 0; i < row.Length; i++)
                    DGVRow[i] = row[i].ToString();
                
                rows.Add(DGVRow);
            }

            return rows;
        }

        /// <summary>
        /// Generate the simplified version of the truth table
        /// </summary>
        /// <returns></returns>
        public List<string> GenerateSimplifiedRows()
        {
            HashSet<string> setRows = new HashSet<string>(GenerateRows(true));

            // number of iteration, there are maximum |variable| stars
            int nIter = AST.listVariable.Count;
            
            while(nIter-- > 0)
            {
                HashSet<string> simplified = new HashSet<string>();
                
                // convert to list for easier iteration
                List<string> tmpSetRows = setRows.ToList(); 

                // to Mark the row that is considered
                bool[] considered = new bool[setRows.Count];

                // Loop O(N^2 * |Length|) to find pairs that differ by strictly 1 character
                for(int i = 0; i < tmpSetRows.Count; i++)
                {
                    string a = tmpSetRows[i]; // i row
                    bool hasDuplicate = false;

                    for(int j = i + 1; j < tmpSetRows.Count; j++)
                    {
                        string b = tmpSetRows[j]; // j row
                        int diff = -1; // the index of the different character

                        for(int k = 0; k < a.Length; k++) // loop O(|length|) characters
                        {
                            if(a[k] != b[k]) // find the different character
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
                        if(diff != -1)
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


    }
}
