using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

using UseYourBrain.Calculator;
using UseYourBrain.Logic_Components;

namespace UseYourBrain
{
    public partial class Form1 : Form
    {
        AbstractionSyntaxTree ast = new AbstractionSyntaxTree();

        public Form1()
        {
            InitializeComponent();
        }

        #region Utility functions

        public void ReadInputFunctionForm()
        {
            try
            {
                string input = textBoxInput.Text;

                input = Regex.Replace(input, @"\s+", "");

                if (string.IsNullOrEmpty(input))
                    throw new NullReferenceException("Input cannot be empty.");

                ast = new AbstractionSyntaxTree();
                ast.Expression = input;
                ast.Build();

                lblInputMessage.Text = "Input Received";
                lblInputMessage.ForeColor = Color.Black;

                txtBoxInputInfix.Text = ast.ToString();
            }
            catch (Exception ex)
            {
                lblInputMessage.Text = ex.Message;
                lblInputMessage.ForeColor = Color.Red;
            }
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

            for(int i = 1; i < dataGViewData.Count; i++)
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

        #endregion

        #region UI Components
        private void button1_Click(object sender, EventArgs e)
        {
            ReadInputFunctionForm();
            ShowTruthTable();
            ShowTruthTableSimplified();
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
            string gg = "";
            foreach (char c in ast.listVariable)
            {
                gg = gg + c + " ";
            }
            MessageBox.Show(gg);
        }
        #endregion

        /// <summary>
        /// Convert to DNF button
        /// </summary>
        private void button4_Click(object sender, EventArgs e)
        {
            TruthTable truthTable = new TruthTable(ast);
            LogicCalculator calculator = new LogicCalculator();

            string DNFEexpression = calculator.ConvertToFullDNF(truthTable);
            AbstractionSyntaxTree cnfAst = new AbstractionSyntaxTree(DNFEexpression);
            MessageBox.Show(cnfAst.ToString());
        }

        private void button5_Click(object sender, EventArgs e)
        {
            TruthTable truthTable = new TruthTable(ast);
            LogicCalculator calculator = new LogicCalculator();

            string DNFEexpression = calculator.ConvertToDNF(truthTable);
            AbstractionSyntaxTree dnfAst = new AbstractionSyntaxTree(DNFEexpression);
            MessageBox.Show(dnfAst.ToString());
        }
    }
}
