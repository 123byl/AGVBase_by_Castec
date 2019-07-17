namespace GLCore.UI
{
    /// <summary>
    /// 標示物編輯器
    /// </summary>
    partial class SingleTowardPairEditor
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.nmrX = new System.Windows.Forms.NumericUpDown();
            this.nmrY = new System.Windows.Forms.NumericUpDown();
            this.nmrToward = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnDone = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nmrX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmrY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmrToward)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(50, 6);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(108, 22);
            this.txtName.TabIndex = 1;
            // 
            // nmrX
            // 
            this.nmrX.Location = new System.Drawing.Point(50, 45);
            this.nmrX.Maximum = new decimal(new int[] {
            2147483640,
            0,
            0,
            0});
            this.nmrX.Minimum = new decimal(new int[] {
            2147483640,
            0,
            0,
            -2147483648});
            this.nmrX.Name = "nmrX";
            this.nmrX.Size = new System.Drawing.Size(108, 22);
            this.nmrX.TabIndex = 2;
            // 
            // nmrY
            // 
            this.nmrY.Location = new System.Drawing.Point(50, 75);
            this.nmrY.Maximum = new decimal(new int[] {
            2147483640,
            0,
            0,
            0});
            this.nmrY.Minimum = new decimal(new int[] {
            2147483640,
            0,
            0,
            -2147483648});
            this.nmrY.Name = "nmrY";
            this.nmrY.Size = new System.Drawing.Size(108, 22);
            this.nmrY.TabIndex = 3;
            // 
            // nmrToward
            // 
            this.nmrToward.Location = new System.Drawing.Point(50, 103);
            this.nmrToward.Maximum = new decimal(new int[] {
            2147483640,
            0,
            0,
            0});
            this.nmrToward.Minimum = new decimal(new int[] {
            2147483640,
            0,
            0,
            -2147483648});
            this.nmrToward.Name = "nmrToward";
            this.nmrToward.Size = new System.Drawing.Size(108, 22);
            this.nmrToward.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(31, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(13, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "X";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(31, 85);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(13, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "Y";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 113);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "Toward";
            // 
            // btnDone
            // 
            this.btnDone.Image = global::GLCore.Properties.Resources.Done;
            this.btnDone.Location = new System.Drawing.Point(163, 11);
            this.btnDone.Margin = new System.Windows.Forms.Padding(2);
            this.btnDone.Name = "btnDone";
            this.btnDone.Size = new System.Drawing.Size(60, 64);
            this.btnDone.TabIndex = 13;
            this.btnDone.UseVisualStyleBackColor = true;
            this.btnDone.Click += new System.EventHandler(this.btnDone_Click);
            // 
            // SingleTowardPairEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(235, 140);
            this.Controls.Add(this.btnDone);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.nmrToward);
            this.Controls.Add(this.nmrY);
            this.Controls.Add(this.nmrX);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label1);
            this.Name = "SingleTowardPairEditor";
            this.Text = "Editor";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.nmrX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmrY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmrToward)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.NumericUpDown nmrX;
        private System.Windows.Forms.NumericUpDown nmrY;
        private System.Windows.Forms.NumericUpDown nmrToward;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnDone;
    }
}