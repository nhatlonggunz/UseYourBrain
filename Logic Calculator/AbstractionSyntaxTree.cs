using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using UseYourBrainLogicLib.Logic_Components;

namespace UseYourBrainLogicLib.LogicCalculator
{
    /***
     * The Symbol OpenParenthesis is created for simplifying
     * the whole process of lexing and parsing the expression.
     */

    /// <summary>
    /// This class store an Abstract Syntax Tree and contains related functions
    /// </summary>
    public class AbstractionSyntaxTree
    {
        /// <summary>
        /// The root of the AST
        /// </summary>
        public Symbol Root { get; set; }

        /// <summary>
        /// Available Symbols
        /// </summary>
        private const string availableOperator = "~>=&|%";
        private const string availableConstant = "01";
        private const string availableVariable = "abcdefghijklmnopqrstuvwxyz" + availablePredicate;
        private const string availablePredicate = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const string availableQuantifier = "@!";
        private const string availablePlaceholder = "(.";

        // If there is quantifier, truth table cannot be made
        public bool HasQuantifier = false;

        // The formula is a proposition if it contains only variables, operators, 0-place predicate
        public bool IsProposition
        {
            get
            {
                for (int i = 'A'; i <= 'Z'; i++)
                    if (NumPredicateVariables[i] > 0)
                        return false;

                if (HasQuantifier)
                    return false;

                return true;
            }
        }

        // Keep track of the number of parameters of a predicate
        public int[] NumPredicateVariables = new int[130];

        private int Counter = 0;

        public string Expression { get; set; }

        public List<char> ListVariable { get; set; }


        public AbstractionSyntaxTree()
        {
            ListVariable = new List<char>();
            NumPredicateVariables = new int[130];
        }
        public AbstractionSyntaxTree(string expression)
        {
            ListVariable = new List<char>();

            NumPredicateVariables = new int[130];
            for (int i = 'A'; i <= 'Z'; i++)
                NumPredicateVariables[i] = -1;

            this.Expression = expression;
            this.Build();
        }
        public AbstractionSyntaxTree(Symbol root, List<char> listVariable)
        {
            if (root == null)
                throw new ArgumentNullException("root cannot be null");

            ListVariable = new List<char>(listVariable);
            this.Root = root;
        }

        public AbstractionSyntaxTree(Symbol root)
        {
            if (root == null)
                throw new ArgumentNullException("root cannot be null");

            this.Root = ObjectExtensions.Copy<Symbol>(root);

            ListVariable = new List<char>();
            FillListVariable();
        }

        /// <summary>
        /// Build the Abstract Syntax Tree using prefix expression input
        /// </summary>
        public void Build()
        {
            // Remove white space and separator (assume that operand has only 1 character)
            Expression = Regex.Replace(Expression, @"\s+", "");

            if (Expression.Contains(",,"))
                throw new ArgumentException();

            Expression = Regex.Replace(Expression, ",", "");

            // Mark the appeared variables
            int[] variableAppeared = new int[130];
            bool[] currentlyBounded = new bool[130];

            // The stack used for building AST
            List<Symbol> utilityStack = new List<Symbol>();

            for (int i = 0; i < Expression.Length; i++)
            {
                /////// Close parenthesis, the signal to trigger an operation
                if (Expression[i] == ')')
                {
                    HandleCloseBracket(utilityStack, currentlyBounded, variableAppeared);
                }
                /////// End Close parenthesis
                else if (availablePlaceholder.Contains(Expression[i]))
                {
                    utilityStack.Add(new Placeholder(Expression[i]));
                }
                // Predicate P(x,y,z,t)
                else if (availablePredicate.Contains(Expression[i]))
                {
                    //NumPredicateVariables[Expression[i]] = 
                    Predicate P = new Predicate(Expression[i]);
                    utilityStack.Add(P);

                    // P takes 0 object variables
                    if (i == Expression.Length - 1 || Expression[i + 1] != '(')
                    {
                        if (NumPredicateVariables[P.Name] > 0)
                            throw new InvalidDataException("Wrong number of object variables for predicate " + P.Name);

                        NumPredicateVariables[P.Name] = 0;
                        P.nChild = 0;
                    }

                    variableAppeared[P.Name]++;
                }
                // Variable
                else if (availableVariable.Contains(Expression[i]))
                {
                    variableAppeared[Expression[i]]++;

                    Variable v = new Variable(Expression[i]);
                    if (currentlyBounded[Expression[i]])
                        v.Bounded = true;
                    utilityStack.Add(v);
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
                        case '%':
                            currentOperator = new Nand();
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
                else if (availableQuantifier.Contains(Expression[i]))
                {
                    HasQuantifier = true;
                    char quantName = Expression[i];
                    string tmpBoundVar = "";

                    for (int j = i + 1; j < Expression.Length; j++)
                    {
                        if (availableVariable.Contains(Expression[j]))
                        {
                            tmpBoundVar += Expression[j];
                            currentlyBounded[Expression[j]] = true;
                        }
                        else if (Expression[j] != '.' || j == i + 1)
                            throw new ArgumentException("Wrong input format at " + j);
                        else
                        {
                            i = j;
                            break;
                        }
                    }

                    Symbol currentQuantifier;

                    switch (quantName)
                    {
                        case '@':
                            currentQuantifier = new Universal(tmpBoundVar);
                            break;
                        default:
                            currentQuantifier = new Existential(tmpBoundVar);
                            break;
                    }

                    utilityStack.Add(currentQuantifier);
                }
                // Invalid symbol
                else
                    throw new ArgumentException("Invalid symbol.");
            }

            if (utilityStack.Count != 1)
            {
                throw new ArgumentException("Wrong format.");
            }

            Root = utilityStack[0];

            for (char c = 'A'; c <= 'Z'; c++)
            {
                if (variableAppeared[c] > 0)
                    ListVariable.Add(c);
            }

            for (char c = 'a'; c <= 'z'; c++)
            {
                if (variableAppeared[c] > 0)
                    ListVariable.Add(c);
            }
        }

        private void HandleCloseBracket(List<Symbol> utilityStack, bool[] currentlyBounded, int[] variableAppeared)
        {
            List<Symbol> content = new List<Symbol>(); // content in-between ()

            // the index of the operator
            int j;

            for (j = utilityStack.Count - 1; j >= 0; j--)
            {
                // end at the open bracket
                if (utilityStack[j] is Placeholder)
                    break;

                content.Add(utilityStack[j]);
            }
            j--; // right before the open bracket

            // there's no operator before the parenthesis (not allowed !!!)
            if (j < 0)
            {
                throw new ArgumentException("There is no operator before the parenthesis");
            }
            if ((utilityStack[j].Type == SymbolType.quantifier ||
                 utilityStack[j].Type == SymbolType.operational) &&
                 utilityStack[j].nOperand != utilityStack.Count - j - 2)
            {
                throw new ArgumentException("Some operation has the wrong number of operand");
            }

            // Apply the operation, keep the result in the stack
            content.Reverse();
            utilityStack[j].Operate(content);

            // pop the operands and open parenthesis from the stack
            utilityStack.RemoveRange(j + 1, 1 + content.Count);

            if (utilityStack[j] is Predicate)
            {
                int numObjVar = utilityStack[j].nOperand;
                int supposedNumObjVar = NumPredicateVariables[utilityStack[j].Name];

                if (supposedNumObjVar != -1 && numObjVar != supposedNumObjVar)
                    throw new ArgumentException("Different number of object variables.");

                foreach (Symbol v in utilityStack[j].Childs)
                    variableAppeared[v.Name]--;

                NumPredicateVariables[utilityStack[j].Name] = numObjVar;
            }
            else if (utilityStack[j] is Quantifier)
            {
                var CurBoundedVar = ((Quantifier)utilityStack[j]).ListBoundVariables;

                for (int i = 'a'; i <= 'z'; i++)
                    if (CurBoundedVar[i])
                        currentlyBounded[i] = false;
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
            catch (IOException)
            {
                return;
            }
            finally
            {
                if (sw != null) sw.Close();
                if (fs != null) fs.Close();
            }
        }

        private int GenerateGraphUtil(StreamWriter sw, Symbol u)
        {
            int uNum = Counter;
            sw.WriteLine("\tnode{0} [label = \"{1}\"];", uNum, u.FullName);

            if (u is Predicate)
                return uNum;

            foreach (Symbol v in u.Childs)
            {
                Counter++;
                int vNum = GenerateGraphUtil(sw, v);
                sw.WriteLine("\tnode{0} -- node{1};", uNum, vNum);
            }

            return uNum;
        }

        public TruthTable GenerateTruthTable()
        {
            TruthTable truthTable = new TruthTable(this);
            truthTable.Calculate();
            return truthTable;
        }

        public void FillListVariable()
        {
            FillListVariableUtil(Root);

            ListVariable = ListVariable.Distinct().ToList();
            ListVariable.Sort();
        }

        public void FillListVariableUtil(Symbol u)
        {
            if (u is Variable)
                ListVariable.Add(((Variable)u).Name);
            if (u is Predicate)
                NumPredicateVariables[u.Name] = ((Predicate)u).nOperand;
            if (u is Quantifier)
                HasQuantifier = true;

            foreach (var v in u.Childs)
                FillListVariableUtil(v);
        }


        public Symbol ToNand()
        {
            return this.Root.toNand();
        }

        public override string ToString()
        {
            if (Root == null)
                throw new ArgumentNullException("There is no tree");
            return Root.ToString();
        }
    }
}
