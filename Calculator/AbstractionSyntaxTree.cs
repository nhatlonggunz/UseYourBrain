using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

using UseYourBrain.Logic_Components;
using System.Diagnostics;
using System.IO;

namespace UseYourBrain.Calculator
{
    /***
     * The Symbol OpenParenthesis is created for simplifying
     * the whole process of lexing and parsing the expression.
     */

    /// <summary>
    /// This class store an Abstract Syntax Tree and contains related functions
    /// </summary>
    class AbstractionSyntaxTree
    {
        /// <summary>
        /// The root of the AST
        /// </summary>
        Symbol Root;

        /// <summary>
        /// Available Symbols
        /// </summary>
        private const string availableOperator = "~>=&|";
        private const string availableConstant = "01";
        private const string availableVariable = "abcdefghijklmnopqrstuvwxyz" +
                                                 "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        private int Counter = 0;

        public string Expression { get; set; }

        public List<Char> listVariable { get; set; }
        

        public AbstractionSyntaxTree()
        {
            listVariable = new List<char>();
        }
        public AbstractionSyntaxTree(string expression)
        {
            listVariable = new List<char>();
            this.Expression = expression;
        }

        /// <summary>
        /// Build the Abstract Syntax Tree using prefix expression input
        /// </summary>
        /// <param name="expression">a prefix expression</param>
        public void Build()
        {
            // Remove white space and separator (assume that operand has only 1 character)
            Expression = Regex.Replace(Expression, @"\s+", "");
            Expression = Regex.Replace(Expression, ",", "");

            // Mark the appeared variables
            bool[] variableAppeared = new bool[100];

            // The stack used for building AST
            List<Symbol> utilityStack = new List<Symbol>();

            for (int i = 0; i < Expression.Length; i++)
            {
                // Close parethesis, the signal to trigger an operation
                if (Expression[i] == ')')  
                {
                    List<Symbol> operands = new List<Symbol>();
                    
                    // the index of the operator
                    int j; 

                    for(j = utilityStack.Count - 1; j >= 0; j--)
                    {
                        if(utilityStack[j] is OpenParathesis)
                        {
                            j--;
                            break;
                        }
                        operands.Add(utilityStack[j]);
                    }

                    // there's no operator before the parenthesis (not allowed !!!)
                    if (j < 0 || utilityStack[j].Type != SymbolType.operational)
                    {
                        throw new Exception("Expression wrong format.");
                    }
                    if (utilityStack[j].nOperand != utilityStack.Count - j - 2)
                    {
                        throw new Exception("Some operation has the wrong number of operand");
                    }

                    // Apply the operation, keep the result in the stack
                    operands.Reverse();
                    utilityStack[j].Operate(operands);

                    // pop the operands and open parenthesis from the stack
                    utilityStack.RemoveRange(j + 1, 1 + operands.Count);
                }
                else if (Expression[i] == '(')
                {
                    utilityStack.Add(new OpenParathesis());
                }
                // Variable
                else if (availableVariable.Contains(Expression[i]))
                {
                    variableAppeared[Expression[i] - 'A'] = true;

                    Variable var = new Variable(Expression[i]);
                    utilityStack.Add(var);
                }
                // Constant (True/False)
                else if (availableConstant.Contains(Expression[i]))
                {
                    Constant cons = new Constant(Expression[i]);
                    utilityStack.Add(cons);
                }
                // Operator
                else if (availableOperator.Contains(Expression[i]))
                {
                    Symbol currentOperator;

                    switch (Expression[i])
                    {
                        case '&':
                            currentOperator = new And();
                            break;
                        case '|':
                            currentOperator = new Or();
                            break;
                        case '=':
                            currentOperator = new BiImplication();
                            break;
                        case '>':
                            currentOperator = new Implication();
                            break;
                        case '~':
                            currentOperator = new Not();
                            break;
                        default:
                            currentOperator = new And();
                            break;
                    }

                    // add to stack
                    utilityStack.Add(currentOperator);
                }
                // Invalid symbol
                else
                {
                    throw new Exception("Invalid symbol.");
                }
                // System.Windows.Forms.MessageBox.Show(i.ToString());
            }

            if(utilityStack.Count != 1)
            {
                throw new Exception("Wrong format.");
            }

            Root = utilityStack[0];

            for (char c = 'A'; c <= 'Z'; c++)
            {
                if (variableAppeared[c - 'A'])
                    listVariable.Add(c);
            }

            for (char c = 'a'; c <= 'z'; c++)
            {
                if (variableAppeared[c - 'A'])
                    listVariable.Add(c);
            }
        }

        /// <summary>
        /// Evaluate the expression
        /// </summary>
        /// <param name="truthValues">
        /// a Dictionary in the form (char,bool) represents variable, truth value
        /// </param>
        /// <returns>The truth value of the expression</returns>
        public bool Evaluate(Dictionary<char, bool> truthValues)
        {
            return Root.GetTruthValue(truthValues);
        }

        public bool Evaluate(bool[] truthValues)
        {
            return Root.GetTruthValue(truthValues);
        }

        public void GenerateGraph(string fileName)
        {
            FileStream fs = new FileStream(fileName + ".dot",
                                            FileMode.OpenOrCreate,
                                            FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);

            try
            {
                string content = "graph AST {";
                sw.WriteLine(content);

                GenerateGraphUtil(sw, Root);

                sw.WriteLine("}");
            }
            catch(IOException)
            {
                return;
            }
            finally
            {
                if (sw != null) sw.Close();
                if (fs != null) fs.Close();
            }
        }

        public int GenerateGraphUtil(StreamWriter sw, Symbol u)
        {
            int uNum = Counter;
            sw.WriteLine("\tnode{0} [label = \"{1}\"];", uNum, u.Name);

            foreach(Symbol v in u.Childs)
            {
                Counter++;
                int vNum = GenerateGraphUtil(sw, v);
                sw.WriteLine("\tnode{0} -- node{1};", uNum, vNum);
            }

            return uNum;
        }

        public override string ToString()
        {
            if (Root == null)
                this.Build();
            return Root.ToString();
        }
    }
}
