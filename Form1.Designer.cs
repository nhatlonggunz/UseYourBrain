namespace UseYourBrain
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBoxInput = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.lblInputMessage = new System.Windows.Forms.Label();
            this.txtBoxInputInfix = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dataGViewTruthTable = new System.Windows.Forms.DataGridView();
            this.dataGViewTruthTableSimplified = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.txtBoxNandForm = new System.Windows.Forms.TextBox();
            this.button6 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.tbVariables = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbFullDNF = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tbDNF = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tbHash = new System.Windows.Forms.TextBox();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textBox9 = new System.Windows.Forms.TextBox();
            this.lbHash = new System.Windows.Forms.ListBox();
            this.button3 = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.tbGenerateInput = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGViewTruthTable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGViewTruthTableSimplified)).BeginInit();
            this.SuspendLayout();
            // 
            // textBoxInput
            // 
            this.textBoxInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxInput.Location = new System.Drawing.Point(151, 32);
            this.textBoxInput.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxInput.Name = "textBoxInput";
            this.textBoxInput.Size = new System.Drawing.Size(729, 38);
            this.textBoxInput.TabIndex = 0;
            this.textBoxInput.TextChanged += new System.EventHandler(this.textBoxInput_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 36);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Prefix Form:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(889, 30);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 28);
            this.button1.TabIndex = 1;
            this.button1.Text = "Submit";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(889, 150);
            this.button2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(100, 28);
            this.button2.TabIndex = 2;
            this.button2.Text = "Show Graph";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // lblInputMessage
            // 
            this.lblInputMessage.AutoSize = true;
            this.lblInputMessage.Location = new System.Drawing.Point(1133, 309);
            this.lblInputMessage.Name = "lblInputMessage";
            this.lblInputMessage.Size = new System.Drawing.Size(0, 17);
            this.lblInputMessage.TabIndex = 3;
            // 
            // txtBoxInputInfix
            // 
            this.txtBoxInputInfix.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBoxInputInfix.Location = new System.Drawing.Point(151, 76);
            this.txtBoxInputInfix.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtBoxInputInfix.Name = "txtBoxInputInfix";
            this.txtBoxInputInfix.Size = new System.Drawing.Size(729, 38);
            this.txtBoxInputInfix.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 89);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 17);
            this.label2.TabIndex = 6;
            this.label2.Text = "Infix Form:";
            // 
            // dataGViewTruthTable
            // 
            this.dataGViewTruthTable.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGViewTruthTable.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGViewTruthTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGViewTruthTable.Location = new System.Drawing.Point(32, 517);
            this.dataGViewTruthTable.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dataGViewTruthTable.Name = "dataGViewTruthTable";
            this.dataGViewTruthTable.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dataGViewTruthTable.RowTemplate.Height = 24;
            this.dataGViewTruthTable.Size = new System.Drawing.Size(309, 409);
            this.dataGViewTruthTable.TabIndex = 7;
            // 
            // dataGViewTruthTableSimplified
            // 
            this.dataGViewTruthTableSimplified.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGViewTruthTableSimplified.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGViewTruthTableSimplified.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGViewTruthTableSimplified.Location = new System.Drawing.Point(347, 517);
            this.dataGViewTruthTableSimplified.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dataGViewTruthTableSimplified.Name = "dataGViewTruthTableSimplified";
            this.dataGViewTruthTableSimplified.RowHeadersWidth = 51;
            this.dataGViewTruthTableSimplified.RowTemplate.Height = 24;
            this.dataGViewTruthTableSimplified.Size = new System.Drawing.Size(309, 409);
            this.dataGViewTruthTableSimplified.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 305);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 17);
            this.label3.TabIndex = 12;
            this.label3.Text = "NAND Form:";
            // 
            // txtBoxNandForm
            // 
            this.txtBoxNandForm.Location = new System.Drawing.Point(151, 301);
            this.txtBoxNandForm.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtBoxNandForm.Name = "txtBoxNandForm";
            this.txtBoxNandForm.Size = new System.Drawing.Size(729, 22);
            this.txtBoxNandForm.TabIndex = 11;
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(1036, 30);
            this.button6.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(149, 28);
            this.button6.TabIndex = 13;
            this.button6.Text = "Check Tautology";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 156);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 17);
            this.label4.TabIndex = 15;
            this.label4.Text = "Variables:";
            // 
            // tbVariables
            // 
            this.tbVariables.Location = new System.Drawing.Point(151, 153);
            this.tbVariables.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbVariables.Name = "tbVariables";
            this.tbVariables.Size = new System.Drawing.Size(367, 22);
            this.tbVariables.TabIndex = 14;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(19, 187);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 17);
            this.label5.TabIndex = 17;
            this.label5.Text = "Full DNF:";
            // 
            // tbFullDNF
            // 
            this.tbFullDNF.Location = new System.Drawing.Point(151, 183);
            this.tbFullDNF.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbFullDNF.Name = "tbFullDNF";
            this.tbFullDNF.Size = new System.Drawing.Size(729, 22);
            this.tbFullDNF.TabIndex = 16;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(151, 212);
            this.textBox3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(729, 22);
            this.textBox3.TabIndex = 18;
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(151, 271);
            this.textBox4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(729, 22);
            this.textBox4.TabIndex = 21;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(17, 246);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(86, 17);
            this.label6.TabIndex = 20;
            this.label6.Text = "DNF Simple:";
            // 
            // tbDNF
            // 
            this.tbDNF.Location = new System.Drawing.Point(151, 242);
            this.tbDNF.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbDNF.Name = "tbDNF";
            this.tbDNF.Size = new System.Drawing.Size(729, 22);
            this.tbDNF.TabIndex = 19;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(573, 158);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(45, 17);
            this.label7.TabIndex = 23;
            this.label7.Text = "Hash:";
            // 
            // tbHash
            // 
            this.tbHash.Location = new System.Drawing.Point(627, 153);
            this.tbHash.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbHash.Name = "tbHash";
            this.tbHash.Size = new System.Drawing.Size(253, 22);
            this.tbHash.TabIndex = 22;
            // 
            // textBox7
            // 
            this.textBox7.Location = new System.Drawing.Point(151, 331);
            this.textBox7.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new System.Drawing.Size(729, 22);
            this.textBox7.TabIndex = 24;
            // 
            // textBox8
            // 
            this.textBox8.Location = new System.Drawing.Point(151, 364);
            this.textBox8.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new System.Drawing.Size(729, 22);
            this.textBox8.TabIndex = 25;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(17, 364);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(75, 17);
            this.label8.TabIndex = 27;
            this.label8.Text = "CNF Form:";
            // 
            // textBox9
            // 
            this.textBox9.Location = new System.Drawing.Point(151, 393);
            this.textBox9.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new System.Drawing.Size(729, 22);
            this.textBox9.TabIndex = 26;
            // 
            // lbHash
            // 
            this.lbHash.FormattingEnabled = true;
            this.lbHash.ItemHeight = 16;
            this.lbHash.Location = new System.Drawing.Point(663, 517);
            this.lbHash.Margin = new System.Windows.Forms.Padding(4);
            this.lbHash.Name = "lbHash";
            this.lbHash.Size = new System.Drawing.Size(351, 404);
            this.lbHash.TabIndex = 28;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(889, 417);
            this.button3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(100, 28);
            this.button3.TabIndex = 29;
            this.button3.Text = "Generate";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(19, 423);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(100, 17);
            this.label9.TabIndex = 31;
            this.label9.Text = "Random Input:";
            // 
            // tbGenerateInput
            // 
            this.tbGenerateInput.Location = new System.Drawing.Point(151, 419);
            this.tbGenerateInput.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbGenerateInput.Name = "tbGenerateInput";
            this.tbGenerateInput.Size = new System.Drawing.Size(729, 22);
            this.tbGenerateInput.TabIndex = 30;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1555, 937);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.tbGenerateInput);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.lbHash);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.textBox9);
            this.Controls.Add(this.textBox8);
            this.Controls.Add(this.textBox7);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.tbHash);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.tbDNF);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tbFullDNF);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tbVariables);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtBoxNandForm);
            this.Controls.Add(this.dataGViewTruthTableSimplified);
            this.Controls.Add(this.dataGViewTruthTable);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtBoxInputInfix);
            this.Controls.Add(this.lblInputMessage);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxInput);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dataGViewTruthTable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGViewTruthTableSimplified)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxInput;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label lblInputMessage;
        private System.Windows.Forms.TextBox txtBoxInputInfix;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dataGViewTruthTable;
        private System.Windows.Forms.DataGridView dataGViewTruthTableSimplified;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtBoxNandForm;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbVariables;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbFullDNF;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbDNF;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbHash;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.TextBox textBox8;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBox9;
        private System.Windows.Forms.ListBox lbHash;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tbGenerateInput;
    }
}

