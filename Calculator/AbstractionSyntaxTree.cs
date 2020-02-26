using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

using UseYourBrain.Logic_Components;

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
        private const string availableVariable = "abcdefghijklmnopqrstuvwxyz";

        /// <summary>
        /// Build the Abstract Syntax Tree using prefix expression input
        /// </summary>
        /// <param name="expression">a prefix expression</param>
        public void Build(string expression)
        {
            // Remove white space and separator (assume that operand has only 1 character)
            expression = Regex.Replace(expression, @"\s+", "");
            expression = Regex.Replace(expression, ",", "");

            // The stack used for building AST
            List<Symbol> utilityStack = new List<Symbol>();

            for (int i = 0; i < expression.Length; i++)
            {
                // Close parethesis, the signal to trigger an operation
                if (expression[i] == ')')  
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
                else if (expression[i] == '(')
                {
                    utilityStack.Add(new OpenParathesis());
                }
                // Variable
                else if (availableVariable.Contains(expression[i]))
                {
                    Variable var = new Variable(expression[i]);
                    utilityStack.Add(var);
                }
                // Constant (True/False)
                else if (availableConstant.Contains(expression[i]))
                {
                    Constant cons = new Constant(expression[i]);
                    utilityStack.Add(cons);
                }
                // Operator
                else if (availableOperator.Contains(expression[i]))
                {
                    Symbol currentOperator;

                    switch (expression[i])
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
            }

            if(utilityStack.Count != 1)
            {
                throw new Exception("Wrong format.");
            }

            Root = utilityStack[0];
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

    }
}
