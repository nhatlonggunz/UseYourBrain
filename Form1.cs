using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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


        #region Input

        public void ReadInputFunctionForm()
        {
            string input = textBoxInput.Text;

            ast.Build(input);
        }


        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            ReadInputFunctionForm();

            var truthValues = new Dictionary<char, bool>()
            {
                { 'a', true },
                { 'b', false }
            };

            MessageBox.Show(ast.Evaluate(truthValues).ToString());
        }
    }
}
