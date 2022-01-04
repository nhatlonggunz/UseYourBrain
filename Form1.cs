using System;
using System.Diagnostics;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
//using UseYourBrain.Logic_Components;
//using UseYourBrain.LogicCalculator;
using UseYourBrainLogicLib.Logic_Components;
using UseYourBrainLogicLib.LogicCalculator;

namespace UseYourBrain
{
    public partial class Form1 : Form
    {
        AbstractionSyntaxTree ast;

        public Form1()
        {
            InitializeComponent();
        }

        #region Utility functions

        public bool ReadInputFunctionForm(string manualInput = "#")
        {
            try
            {
                string input = textBoxInput.Text;

                /* For testing */
                if (manualInput != "#")
                    input = manualInput;
                /* End Testing*/

                input = Regex.Replace(input, @"\s+", "");

                if (string.IsNullOrEmpty(input))
                    throw new Exception("Input cannot be empty.");

                ast = new AbstractionSyntaxTree(input);

                lblInputMessage.Text = "Input Received";
                lblInputMessage.ForeColor = Color.Black;

                txtBoxInputInfix.Text = ast.ToString();

                return true;
            }
            catch (Exception ex)
            {
                lblInputMessage.Text = ex.Message;
                lblInputMessage.ForeColor = Color.Red;

                return false;
            }
        }

        public void ShowVariables()
        {
            string gg = "";
            foreach (char c in ast.ListVariable)
            {
                gg = gg + c + " ";
            }
            tbVariables.Text = gg;
        }

        public string ShowHash()
        {
            string combinedHash = "";

            // Original hash
            TruthTable truthTable = new TruthTable(ast);
            truthTable.Calculate();
            tbHash.Text = truthTable.GetHash();

            // Original Hash
            TruthTable tmpTb;
            tmpTb = ast.GenerateTruthTable();
            lbHash.Items.Add("Original: " + tmpTb.GetHash());
            combinedHash += tmpTb.GetHash();

            // Nandification hash
            AbstractionSyntaxTree nandTree = ObjectExtensions.Copy(ast);
            nandTree.Root = nandTree.Root.toNand();
            tmpTb = nandTree.GenerateTruthTable();
            lbHash.Items.Add("Nandify: " + tmpTb.GetHash());
            combinedHash += tmpTb.GetHash();

            // Full DNF hash
            tmpTb = ast.GenerateTruthTable();
            Calculator cal = new Calculator();
            string fullDNF = cal.ConvertToFullDNF(tmpTb);
            AbstractionSyntaxTree fullDnfTree = new AbstractionSyntaxTree(fullDNF);
            //lbHash.Items.Add("Full DNF: " + fullDnfTree.GenerateTruthTable().GetHash());
            //combinedHash += fullDnfTree.GenerateTruthTable().GetHash();

            // DNF hash
            string DNF = cal.ConvertToDNF(tmpTb);
            AbstractionSyntaxTree dnfTree = new AbstractionSyntaxTree(DNF);
            //lbHash.Items.Add("DNF: " + dnfTree.GenerateTruthTable().GetHash());
            //combinedHash += fullDnfTree.GenerateTruthTable().GetHash();

            return combinedHash;
        }

        public void ShowGraphAST()
        {
            Process dot = new Process();

            dot.StartInfo.FileName = @"dot.exe";
            dot.StartInfo.Arguments = "-Tpng -ogg.png gg.dot";
            dot.Start();
            dot.WaitForExit();

            Process.Start("gg.png");
        }

        public void ShowTruthTable()
        {
            dataGViewTruthTable.Rows.Clear();
            dataGViewTruthTable.Columns.Clear();

            TruthTable truthTable = new TruthTable(ast);
            truthTable.Calculate();

            var listRows = truthTable.GenerateRows(true);
            var dataGViewData = truthTable.GenerateDataGridViewData(listRows, true);

            int len = dataGViewData[0].Length;

            dataGViewTruthTable.ColumnCount = len;

            for (int i = 0; i < len; i++)
                dataGViewTruthTable.Columns[i].Name = dataGViewData[0][i];

            for (int i = 1; i < dataGViewData.Count; i++)
                dataGViewTruthTable.Rows.Add(dataGViewData[i]);
        }

        public void ShowTruthTableSimplified()
        {
            dataGViewTruthTableSimplified.Rows.Clear();
            dataGViewTruthTableSimplified.Columns.Clear();

            TruthTable truthTable = new TruthTable(ast);
            truthTable.Calculate();

            var simplifiedListRows = truthTable.GenerateSimplifiedRows();
            var dataGViewData = truthTable.GenerateDataGridViewData(simplifiedListRows, true);

            int len = dataGViewData[0].Length;

            dataGViewTruthTableSimplified.ColumnCount = len;

            for (int i = 0; i < len; i++)
                dataGViewTruthTableSimplified.Columns[i].Name = dataGViewData[0][i];

            for (int i = 1; i < dataGViewData.Count; i++)
                dataGViewTruthTableSimplified.Rows.Add(dataGViewData[i]);
        }

        public void ShowNand()
        {
            AbstractionSyntaxTree nandTree = ObjectExtensions.Copy(ast);
            nandTree.Root = nandTree.Root.toNand();

            txtBoxNandForm.Text = nandTree.ToString();

            TruthTable tb = new TruthTable(nandTree);
            tb.Calculate();

            tb = new TruthTable(ast);
            tb.Calculate();
        }

        public string ShowFullDNF()
        {
            //TruthTable truthTable = new TruthTable(ast);
            //LogicCalculator.Calculator calculator = new LogicCalculator.Calculator();

            //string DNFEexpression = calculator.ConvertToFullDNF(truthTable);
            //AbstractionSyntaxTree dnfAst = new AbstractionSyntaxTree(DNFEexpression);
            //tbFullDNF.Text = dnfAst.ToString();

            TruthTable truthTable = new TruthTable(ast);
            Symbol FullDNF = truthTable.ToFullDNF();
            AbstractionSyntaxTree dnfAst = new AbstractionSyntaxTree(FullDNF);
            tbFullDNF.Text = dnfAst.ToString();

            return dnfAst.ToString();
        }

        public string ShowDNF()
        {
            //TruthTable truthTable = new TruthTable(ast);
            //LogicCalculator.Calculator calculator = new LogicCalculator.Calculator();

            //string DNFEexpression = calculator.ConvertToDNF(truthTable);
            //AbstractionSyntaxTree dnfAst = new AbstractionSyntaxTree(DNFEexpression);
            //tbDNF.Text = dnfAst.ToString();
            TruthTable truthTable = new TruthTable(ast);
            Symbol DNF = truthTable.ToDNF();
            AbstractionSyntaxTree dnfAst = new AbstractionSyntaxTree(DNF);
            tbDNF.Text = dnfAst.ToString();

            return dnfAst.ToString();
        }
        public bool CheckTautology()
        {
            bool success = false;

            SemanticTableau sm = new SemanticTableau(ast.Root);
            sm.Build();

            sm.GenerateGraph("SemanticTableau");

            if (sm.State == 1)
            {
                MessageBox.Show("Tautology");
                success = true;
            }
            else
            {
                MessageBox.Show("Inconclusion");
                success = false;
            }

            Process dot = new Process();

            dot.StartInfo.FileName = @"dot.exe";
            dot.StartInfo.Arguments = "-Tpng -oSemanticTableau.png SemanticTableau.dot";
            dot.Start();
            dot.WaitForExit();

            Process.Start("SemanticTableau.png");

            return success;
        }
        #endregion



        #region UI Components
        private void button1_Click(object sender, EventArgs e)
        {
            if (ReadInputFunctionForm())
            {
                lbHash.Items.Clear();
                ShowVariables();
                ShowNand();

                if (!ast.IsProposition)
                    return;
                ShowTruthTable();
                ShowTruthTableSimplified();
                ShowHash();
                ShowFullDNF();
                ShowDNF();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ast.GenerateGraph("gg");
            ShowGraphAST();
        }

        private void textBoxInput_TextChanged(object sender, EventArgs e)
        {
            lblInputMessage.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //int maxDepth = 10;
            //int max
        }
        #endregion

        private void button6_Click(object sender, EventArgs e)
        {
            if (ast == null)
            { MessageBox.Show("Please input"); return; }
            CheckTautology();
        }




    }
}
