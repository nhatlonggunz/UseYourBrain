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
            this.button3 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.dataGViewTruthTable = new System.Windows.Forms.DataGridView();
            this.dataGViewTruthTableSimplified = new System.Windows.Forms.DataGridView();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGViewTruthTable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGViewTruthTableSimplified)).BeginInit();
            this.SuspendLayout();
            // 
            // textBoxInput
            // 
            this.textBoxInput.Location = new System.Drawing.Point(107, 65);
            this.textBoxInput.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxInput.Name = "textBoxInput";
            this.textBoxInput.Size = new System.Drawing.Size(203, 22);
            this.textBoxInput.TabIndex = 0;
            this.textBoxInput.TextChanged += new System.EventHandler(this.textBoxInput_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 68);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Prefix Form:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(339, 63);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 28);
            this.button1.TabIndex = 0;
            this.button1.Text = "Submit";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(339, 119);
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
            this.lblInputMessage.Location = new System.Drawing.Point(104, 32);
            this.lblInputMessage.Name = "lblInputMessage";
            this.lblInputMessage.Size = new System.Drawing.Size(0, 17);
            this.lblInputMessage.TabIndex = 3;
            // 
            // txtBoxInputInfix
            // 
            this.txtBoxInputInfix.Location = new System.Drawing.Point(107, 119);
            this.txtBoxInputInfix.Name = "txtBoxInputInfix";
            this.txtBoxInputInfix.Size = new System.Drawing.Size(203, 22);
            this.txtBoxInputInfix.TabIndex = 4;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(339, 182);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(100, 28);
            this.button3.TabIndex = 5;
            this.button3.Text = "List Variable";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 122);
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
            this.dataGViewTruthTable.Location = new System.Drawing.Point(479, 47);
            this.dataGViewTruthTable.Name = "dataGViewTruthTable";
            this.dataGViewTruthTable.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dataGViewTruthTable.RowTemplate.Height = 24;
            this.dataGViewTruthTable.Size = new System.Drawing.Size(309, 408);
            this.dataGViewTruthTable.TabIndex = 7;
            // 
            // dataGViewTruthTableSimplified
            // 
            this.dataGViewTruthTableSimplified.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGViewTruthTableSimplified.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGViewTruthTableSimplified.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGViewTruthTableSimplified.Location = new System.Drawing.Point(794, 47);
            this.dataGViewTruthTableSimplified.Name = "dataGViewTruthTableSimplified";
            this.dataGViewTruthTableSimplified.RowHeadersWidth = 51;
            this.dataGViewTruthTableSimplified.RowTemplate.Height = 24;
            this.dataGViewTruthTableSimplified.Size = new System.Drawing.Size(309, 408);
            this.dataGViewTruthTableSimplified.TabIndex = 8;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(289, 251);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(150, 28);
            this.button4.TabIndex = 9;
            this.button4.Text = "Convert To Full DNF";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(289, 300);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(150, 28);
            this.button5.TabIndex = 10;
            this.button5.Text = "Convert To DNF";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1402, 765);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.dataGViewTruthTableSimplified);
            this.Controls.Add(this.dataGViewTruthTable);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button3);
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
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dataGViewTruthTable;
        private System.Windows.Forms.DataGridView dataGViewTruthTableSimplified;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
    }
}

