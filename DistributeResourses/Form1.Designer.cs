namespace DistributeResourses
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
            this.txtBxPrc1 = new System.Windows.Forms.TextBox();
            this.txtBxPrc2 = new System.Windows.Forms.TextBox();
            this.txtBxPrc3 = new System.Windows.Forms.TextBox();
            this.txtBxPrc4 = new System.Windows.Forms.TextBox();
            this.txtBxPrc5 = new System.Windows.Forms.TextBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtBxPrc1
            // 
            this.txtBxPrc1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txtBxPrc1.Location = new System.Drawing.Point(92, 51);
            this.txtBxPrc1.Name = "txtBxPrc1";
            this.txtBxPrc1.ReadOnly = true;
            this.txtBxPrc1.Size = new System.Drawing.Size(117, 20);
            this.txtBxPrc1.TabIndex = 1;
            // 
            // txtBxPrc2
            // 
            this.txtBxPrc2.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txtBxPrc2.Location = new System.Drawing.Point(92, 111);
            this.txtBxPrc2.Name = "txtBxPrc2";
            this.txtBxPrc2.ReadOnly = true;
            this.txtBxPrc2.Size = new System.Drawing.Size(117, 20);
            this.txtBxPrc2.TabIndex = 2;
            // 
            // txtBxPrc3
            // 
            this.txtBxPrc3.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txtBxPrc3.Location = new System.Drawing.Point(92, 188);
            this.txtBxPrc3.Name = "txtBxPrc3";
            this.txtBxPrc3.ReadOnly = true;
            this.txtBxPrc3.Size = new System.Drawing.Size(117, 20);
            this.txtBxPrc3.TabIndex = 3;
            // 
            // txtBxPrc4
            // 
            this.txtBxPrc4.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txtBxPrc4.Location = new System.Drawing.Point(92, 229);
            this.txtBxPrc4.Name = "txtBxPrc4";
            this.txtBxPrc4.ReadOnly = true;
            this.txtBxPrc4.Size = new System.Drawing.Size(117, 20);
            this.txtBxPrc4.TabIndex = 4;
            // 
            // txtBxPrc5
            // 
            this.txtBxPrc5.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txtBxPrc5.Location = new System.Drawing.Point(92, 268);
            this.txtBxPrc5.Name = "txtBxPrc5";
            this.txtBxPrc5.ReadOnly = true;
            this.txtBxPrc5.Size = new System.Drawing.Size(117, 20);
            this.txtBxPrc5.TabIndex = 5;
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(299, 273);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 6;
            this.btnStart.Text = "Старт";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(403, 273);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 23);
            this.btnStop.TabIndex = 7;
            this.btnStop.Text = "Стоп";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 379);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.txtBxPrc5);
            this.Controls.Add(this.txtBxPrc4);
            this.Controls.Add(this.txtBxPrc3);
            this.Controls.Add(this.txtBxPrc2);
            this.Controls.Add(this.txtBxPrc1);
            this.Name = "Form1";
            this.Text = "Распределенный алгоритм";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtBxPrc1;
        private System.Windows.Forms.TextBox txtBxPrc2;
        private System.Windows.Forms.TextBox txtBxPrc3;
        private System.Windows.Forms.TextBox txtBxPrc4;
        private System.Windows.Forms.TextBox txtBxPrc5;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
    }
}

