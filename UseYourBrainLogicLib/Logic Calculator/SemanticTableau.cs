using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UseYourBrainLogicLib.Logic_Components;

namespace UseYourBrainLogicLib.LogicCalculator
{
    public enum TableauRule
    {
        NONE,
        ALPHA,
        DELTA,
        BETA,
        GAMMA
    }
    /*
        A proof procedure for formulae of First-Order Logic
        
        To prove a formula F, is to prove that it is a tautology (or it is valid)
        F is valid <=> ~F is unsatisfiable

        Able to prove or refute the whole formulae. 
        Able to determine the satisfiability (validity) of a formulae

        Satisfiability: there exists an interpretation that makes the formulae true
        Validity:       all interpretation makes the formulae true

        Unsatisfiability: all interpretation make the formula false
        Invalidity:      there exists an interpretation that makes the formula false
    */
    public class SemanticTableau
    {
        /*
            If there is (P^Q) ^ ~(P^Q), then try to conclude the node, or further expand ? 
            For most manual test cases, especially the case {P, Q} which Q  = ...~P...,
            let P wait for Q to be decomposed into ~P
            is computationally slower than decomposing both P and Q to the primitive level.

            Also, to conclude P and ~P in which P is a kinda complex formulae
            requires additional O(nlogn) for checking the existance of P and ~P
            in every node of the analytical tableau.

            In conclusion, just expand to the primitive level.
        */

        /*
         * Generally: 
         * - Expand all the And
         * - Check for contradiction
         * - continue with Or expansion
         */


        public TableauNode Root;
        public Symbol Formula;
        public int State { get; set; } // -1: not calculated, 0: inconclusion, 1: unsatisfiable

        // Unique ID of a TableauNode
        private int Counter;

        public SemanticTableau(Symbol formula)
        {
            Formula = formula;
            State = -1;
        }

        public void Build()
        {
            // Root of Tableau
            Root = new TableauNode(new HashSet<Symbol>(new SymbolComparer()) { new Not(Formula) });
            Root.RuleApplied = TableauRule.NONE;

            bool unsatis = BuildUtil(Root);

            State = 0;
            if (unsatis)
                State = 1;
        }

        /*
            1) Check contradiction
            2) Alpha rules
            3) Delta rules
            4) Beta rules
            5) Gamma rules
        */
        /// <summary>
        /// Build the Semantic Tableau
        /// </summary>
        /// <returns>True if the formula is Unsatifiable (has contradiction)</returns>
        public bool BuildUtil(TableauNode curNode) // return true if contradiction
        {
            TableauNode newNode = new TableauNode(curNode.SetFormulas, curNode.CurrentVariables);

            if(CheckContradiction(newNode.SetFormulas))
            {
                newNode.SetFormulas = new HashSet<Symbol>() { new Constant('0') };
                curNode.Childs.Add(newNode);
                return true;
            }

            HashSet<Symbol> formulas = newNode.SetFormulas;

            //////////////////////////////////////////////////////////////
            // Alpha Rules
            //////////////////////////////////////////////////////////////
            
            formulas = ExpandAllAnds(newNode, true, false);

            if (!TableauNode.EqualSet(formulas, curNode.SetFormulas)) // successful
            {
                newNode.RuleApplied = TableauRule.ALPHA;
                curNode.Childs.Add(newNode);

                return BuildUtil(newNode);
            }

            // Haven't applied Alpha rules
            // Continue with 

            //////////////////////////////////////////////////////////////
            // Delta rules
            //////////////////////////////////////////////////////////////

            formulas = ExpandAllAnds(newNode, false, true);

            if (!TableauNode.EqualSet(formulas, curNode.SetFormulas)) // successful
            {
                newNode.RuleApplied = TableauRule.DELTA;
                curNode.Childs.Add(newNode);

                return BuildUtil(newNode);
            }


            //////////////////////////////////////////////////////////////
            // Beta Rules
            //////////////////////////////////////////////////////////////
            bool CurrentlyContradicted = true;

            Symbol chosenFormula = null; // The formula that can be applied beta rules
            List<Symbol> expansion = new List<Symbol>(); // expanded version of the chosen formula

            // Find a subformula that can be applied beta rules
            foreach (Symbol subformula in formulas)
            {
                expansion = ExpandToOr(subformula);

                if (expansion is null)
                    continue;

                chosenFormula = subformula;
                break;
            }

            // There exist a formula expanded.
            if (!(chosenFormula == null))
            {
                formulas.Remove(chosenFormula); // remove the formula

                foreach (Symbol term in expansion)
                {
                    formulas.Add(term); // Add an expanded subformula

                    var newSet = new HashSet<Symbol>(new SymbolComparer());
                    foreach (var item in formulas)
                    {
                        newSet.Add(ObjectExtensions.Copy(item));
                    }

                    TableauNode tmpNode = new TableauNode(newSet, curNode.CurrentVariables);
                    tmpNode.RuleApplied = TableauRule.BETA;

                    curNode.Childs.Add(tmpNode);

                    if (!BuildUtil(tmpNode))
                        CurrentlyContradicted = false;

                    formulas.Remove(term);
                }

                return CurrentlyContradicted; // both childs must be contradition
            }

            //////////////////////////////////////////////////////////////
            // Gamma rules
            //////////////////////////////////////////////////////////////
            

            var expansionStorage = new HashSet<Symbol>(new SymbolComparer());
            expansion = new List<Symbol>();

            foreach (var subformula in formulas)
            {
                expansion = GammaRules(subformula, newNode);

                if (expansion is null)
                    continue;

                foreach (var expansionTerm in expansion)
                    expansionStorage.Add(expansionTerm);
            }

            if (expansionStorage.Count > 0)
            {
                foreach (var expansionTerm in expansionStorage)
                    formulas.Add(expansionTerm);

                curNode.Childs.Add(newNode);
                newNode.RuleApplied = TableauRule.GAMMA;

                return BuildUtil(newNode);
            }

            return false;
        }




        /// <summary>
        /// Check for contradiction among variables/predicates
        /// </summary>
        /// <param name="formulas">Set of formulas</param>
        /// <returns>true if there is a contradiction, false otherwise</returns>
        public static bool CheckContradiction(HashSet<Symbol> formulas)
        {
            //bool[] negative = new bool[130];    // ~P
            //bool[] positive = new bool[130];    // P

            var negative = new HashSet<Symbol>(new SymbolComparer());
            var positive = new HashSet<Symbol>(new SymbolComparer());

            foreach (Symbol subformula in formulas)
            {
                // there's a false
                if (subformula is Constant && ((Constant)subformula).Value == false)
                    return false;

                if (subformula is Not)
                {
                    if (positive.Contains(subformula.Childs[0]))
                        return true;
                    negative.Add(subformula.Childs[0]);
                }
                else
                {
                    if (negative.Contains(subformula))
                        return true;
                    positive.Add(subformula);
                }
            }

            return false;
        }

        // NOTE: Maybe should REMOVE this method
        // because the formulas that are And
        // will eventually be expanded by ExpandToAnd(Symbol).
        // It will also make the Tableau more easy to look.
        /// <summary>
        /// Expand all the Ands formula
        /// </summary>
        /// <param name="formulas"></param>
        /// <returns>A set of formulas with all the And expanded</returns>
        public static HashSet<Symbol> ExpandAllAnds(
            TableauNode curNode, bool alphaRules, bool deltaRules)
        {
            List<Symbol> delList = new List<Symbol>();
            List<Symbol> addList = new List<Symbol>();

            HashSet<Symbol> formulas = curNode.SetFormulas;

            // Subformula that is expanded to And
            foreach (Symbol subformula in formulas)
            {
                List<Symbol> expansion = new List<Symbol>();
                    
                if(alphaRules)
                    expansion = ExpandToAnd(subformula);

                // Delta rules - Expand !x and ~@x 
                if(deltaRules)
                    expansion = DeltaRules(subformula, curNode);

                if (!(expansion is null))
                {
                    foreach (Symbol term in expansion)
                        addList.Add(term);

                    delList.Add(subformula);
                }
            }

            foreach (Symbol subformula in delList)
                formulas.Remove(subformula);
            foreach (Symbol subformula in addList)
                formulas.Add(subformula);

            delList.Clear();
            addList.Clear();
            

            return formulas;
        }

        /*
         * The result of the expansion is an And,
         * which means split into several subformulas
         * 
         * A ^ B -> A,B
         * ~~A -> A
         * ~(A v B) = ~A ^ ~B
         * ~(A => B) = A ^ ~B
         * 
         */
        /// <summary>
        /// Applying Alpha rule (the result of expansion is an And)
        /// </summary>
        /// <param name="formula">The formula to expand</param>
        /// <returns>A list of formulas after expansion, null if not applicable</returns>
        public static List<Symbol> ExpandToAnd(Symbol formula)
        {
            // A ^ B -> A, B
            if (formula is And)
                return formula.Childs;

            if (formula is Not)
            {
                Symbol subformula = ((Not)formula).Subformula;

                if (subformula is Not) // ~~A
                    return new List<Symbol>() { subformula.Childs[0] };

                // ~A
                if (subformula is Variable ||
                    subformula is Predicate ||
                    subformula is Quantifier)
                    return null;


                Symbol A = subformula.Childs[0];
                Symbol B = subformula.Childs[1];

                // ~(A v B)
                if (subformula is Or)
                    return new List<Symbol>() { new Not(A), new Not(B) };

                // ~(A => B)
                if (subformula is Implication)
                    return new List<Symbol>() { A, new Not(B) };
            }

            return null; // not applicable
        }

        public static List<Symbol> DeltaRules(Symbol formula, TableauNode currentNode)
        {
            char newVar = '#';
            for (char c = 'a'; c <= 'z'; c++)
            {
                if (!currentNode.CurrentVariables[c])
                {
                    newVar = c;
                    break;
                }
            }

            if (formula is Existential)
            {
                char boundedVar = ((Existential)formula).BoundVariables[0]; // extend later

                formula = formula.ChangeVariableName(boundedVar, newVar, true).Childs[0];
                currentNode.CurrentVariables[newVar] = true;
            }
            else if (formula is Not && formula.Childs[0] is Universal)
            {
                formula = formula.Childs[0];
                char boundedVar = ((Universal)formula).BoundVariables[0]; // extend later

                formula = new Not(formula.ChangeVariableName(boundedVar, newVar, true).Childs[0]);
                currentNode.CurrentVariables[newVar] = true;
            }
            else
                return null;

            return new List<Symbol>() { formula };
        }

        /*
         * The result of the expansion is an Or,
         * which split the current node into several branches
         * 
         * A v B -> A | B (2 branches)
         * ~(A ^ B) = ~A v ~B
         * ~(A <=> B) = (A ^ ~B) v (~A ^ B)
         * 
         * A => B = ~A v B
         * A <=> B = (A ^ B) v (~A ^ ~B)
         * 
         */
        /// <summary>
        /// Applying Beta rules (the result of expansion is an Or)
        /// </summary>
        /// <param name="formula">The formula to expand</param>
        /// <returns>A list of formulas after expansion, null if not applicable</returns>
        public static List<Symbol> ExpandToOr(Symbol formula)
        {
            // A v B
            if (formula is Or)
                return formula.Childs;

            if (formula.Type is SymbolType.primitive ||
                formula.Type is SymbolType.predicate ||
                formula is Quantifier)
                return null;

            Symbol A;
            Symbol B;

            if (formula is Not)
            {
                Symbol subformula = ((Not)formula).Subformula;

                if (subformula.Type is SymbolType.primitive ||
                    subformula.Type is SymbolType.predicate ||
                    subformula is Quantifier ||
                    subformula is Not)
                    return null;

                A = subformula.Childs[0];
                B = subformula.Childs[1];

                if (subformula is And)
                    return new List<Symbol>() { new Not(A),
                                                new Not(B) };
                if (subformula is BiImplication)
                    return new List<Symbol>() { new And(A, new Not(B)),
                                                new And(new Not(A), B) };
            }

            A = formula.Childs[0];
            B = formula.Childs[1];


            if (formula is Implication)
                return new List<Symbol>() { new Not(A), B };
            if (formula is BiImplication)
                return new List<Symbol>() { new And(A, B),
                                            new And(new Not(A), new Not(B))};

            return null;
        }

        public static List<Symbol> GammaRules(Symbol formula, TableauNode currentNode)
        {

            List<Symbol> returnList = new List<Symbol>();

            if (formula is Universal)
            {

                foreach (char c in currentNode.ListVariables)
                {
                    if (formula.GammaApplied[c])
                        continue;
                    char boundVariable = ((Universal)formula).BoundVariables[0];

                    returnList.Add(formula.ChangeVariableName(boundVariable, c, true).Childs[0]);

                    formula.GammaApplied[c] = true;
                }
            }
            else if (formula is Not && formula.Childs[0] is Existential)
            {
                char boundVariable = ((Existential)formula.Childs[0]).BoundVariables[0];

                foreach (char c in currentNode.ListVariables)
                {
                    if (formula.GammaApplied[c])
                        continue;
                    returnList.Add(new Not(formula.ChangeVariableName(boundVariable, c, true).Childs[0].Childs[0]));
                    formula.GammaApplied[c] = true;
                }
            }

            return returnList.Count == 0 ? null : returnList;
        }


        /// <summary>
        /// Generate a file that can be run by GraphViz,
        /// file name: <fileName>.dot
        /// </summary>
        /// <param name="fileName"></param>
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

                Counter = 0;
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
        public int GenerateGraphUtil(StreamWriter sw, TableauNode u)
        {
            string content = ""; // text of the Tableau Node (the set of formulas at the current node)

            // Add all formulas to the content
            foreach (var formula in u.SetFormulas)
            {
                content += formula.ToString() + "\n";
            }
            //content = content.Remove(content.Length - 1); // remove last ','
            //content = content.Remove(1, 1); // remove first space
            //content += "]";

            // List active variables
            content += " [";

            foreach (char c in u.ListVariables)
                content += c + ",";

            if (content[content.Length - 1] == ',')
                content = content.Remove(content.Length - 1); // remove last ','
            content += "]";

            int uNum = Counter; // ID of current node

            string fillColor = (u.RuleApplied == TableauRule.ALPHA ? "yellow" :
                                u.RuleApplied == TableauRule.BETA ? "palegreen" :
                                u.RuleApplied == TableauRule.DELTA ? "skyblue" :
                                u.RuleApplied == TableauRule.GAMMA ? "brown1" :
                                "gray88");
            // content of the node
            string graphvizContent = $"\tnode{uNum} [shape=rectangle; style = filled;" +
                                     $"label = \"{content}\"; color = black; fillcolor={fillColor}];";

            // if this node is a contradiction (a node containing only false)
            // then color it red
            if (TableauNode.EqualSet(u.SetFormulas, new HashSet<Symbol>() { new Constant('0') }))
                graphvizContent = $"\tnode{uNum} [shape=rectangle; style=filled;" +
                                  $"label = \"{content}\"; color=crimson fillcolor={fillColor}];";

            // draw current node
            sw.WriteLine(graphvizContent);

            foreach (TableauNode v in u.Childs)
            {
                Counter++;
                int vNum = GenerateGraphUtil(sw, v);
                sw.WriteLine("\tnode{0} -- node{1};", uNum, vNum); // draw edge to child node
            }

            return uNum; // return ID of current node
        }
    }







    /// <summary>
    /// A node of Semantic Tableau
    /// </summary>
    public class TableauNode
    {
        public HashSet<Symbol> SetFormulas { get; set; }
        public List<TableauNode> Childs { get; set; }
        public TableauRule RuleApplied { get; set; }

        public string ListVariables
        {
            get
            {
                string tmp = "";
                for (char i = 'a'; i <= 'z'; i++)
                {
                    if (CurrentVariables[i])
                        tmp += i;
                }
                return tmp;
            }
        }

        public bool[] CurrentVariables { get; set; }

        public TableauNode(HashSet<Symbol> formulas)
        {
            SetFormulas = new HashSet<Symbol>(new SymbolComparer());

            foreach (var subformula in formulas)
                SetFormulas.Add(ObjectExtensions.Copy(subformula));

            Childs = new List<TableauNode>();
            CurrentVariables = new bool[130];
            RuleApplied = TableauRule.NONE;
        }
        public TableauNode(HashSet<Symbol> formulas, bool[] vars)
        {
            SetFormulas = new HashSet<Symbol>(new SymbolComparer());

            foreach (var subformula in formulas)
                SetFormulas.Add(ObjectExtensions.Copy(subformula));

            Childs = new List<TableauNode>();
            CurrentVariables = ObjectExtensions.Copy(vars);
            RuleApplied = TableauRule.NONE;
        }

        // Compare two HashSet<Symbol>
        public static bool EqualSet(HashSet<Symbol> a, HashSet<Symbol> b)
        {
            if (a.Count != b.Count)
                return false;

            var listA = a.ToList();
            var listB = b.ToList();

            for (int i = 0; i < a.Count; i++)
            {
                if (listA[i].ToString() != listB[i].ToString())
                    return false;
            }

            return true;
        }
    }
}
