namespace UseYourBrain
{
    partial class FormGrahpAST
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.pBoxGraphAST = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pBoxGraphAST)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.AutoSize = true;
            this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(0, 0);
            this.panel1.TabIndex = 0;
            // 
            // pBoxGraphAST
            // 
            this.pBoxGraphAST.Location = new System.Drawing.Point(2, 1);
            this.pBoxGraphAST.Name = "pBoxGraphAST";
            this.pBoxGraphAST.Size = new System.Drawing.Size(949, 615);
            this.pBoxGraphAST.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pBoxGraphAST.TabIndex = 1;
            this.pBoxGraphAST.TabStop = false;
            // 
            // FormGrahpAST
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(946, 628);
            this.Controls.Add(this.pBoxGraphAST);
            this.Controls.Add(this.panel1);
            this.Name = "FormGrahpAST";
            this.Text = "GrahpAST";
            ((System.ComponentModel.ISupportInitialize)(this.pBoxGraphAST)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pBoxGraphAST;
    }
}