namespace TicTacToeClient
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.btn1 = new System.Windows.Forms.Button();
            this.btn2 = new System.Windows.Forms.Button();
            this.btn3 = new System.Windows.Forms.Button();
            this.btn4 = new System.Windows.Forms.Button();
            this.btn5 = new System.Windows.Forms.Button();
            this.btn6 = new System.Windows.Forms.Button();
            this.btn7 = new System.Windows.Forms.Button();
            this.btn8 = new System.Windows.Forms.Button();
            this.btn9 = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btn1
            // 
            this.btn1.Location = new System.Drawing.Point(12, 12);
            this.btn1.Name = "btn1";
            this.btn1.Size = new System.Drawing.Size(75, 75);
            this.btn1.TabIndex = 0;
            this.btn1.UseVisualStyleBackColor = true;
            // 
            // btn2
            // 
            this.btn2.Location = new System.Drawing.Point(93, 12);
            this.btn2.Name = "btn2";
            this.btn2.Size = new System.Drawing.Size(75, 75);
            this.btn2.TabIndex = 1;
            this.btn2.UseVisualStyleBackColor = true;
            // 
            // btn3
            // 
            this.btn3.Location = new System.Drawing.Point(174, 12);
            this.btn3.Name = "btn3";
            this.btn3.Size = new System.Drawing.Size(75, 75);
            this.btn3.TabIndex = 2;
            this.btn3.UseVisualStyleBackColor = true;
            // 
            // btn4
            // 
            this.btn4.Location = new System.Drawing.Point(12, 93);
            this.btn4.Name = "btn4";
            this.btn4.Size = new System.Drawing.Size(75, 75);
            this.btn4.TabIndex = 3;
            this.btn4.UseVisualStyleBackColor = true;
            // 
            // btn5
            // 
            this.btn5.Location = new System.Drawing.Point(93, 93);
            this.btn5.Name = "btn5";
            this.btn5.Size = new System.Drawing.Size(75, 75);
            this.btn5.TabIndex = 4;
            this.btn5.UseVisualStyleBackColor = true;
            // 
            // btn6
            // 
            this.btn6.Location = new System.Drawing.Point(174, 93);
            this.btn6.Name = "btn6";
            this.btn6.Size = new System.Drawing.Size(75, 75);
            this.btn6.TabIndex = 5;
            this.btn6.UseVisualStyleBackColor = true;
            // 
            // btn7
            // 
            this.btn7.Location = new System.Drawing.Point(12, 174);
            this.btn7.Name = "btn7";
            this.btn7.Size = new System.Drawing.Size(75, 75);
            this.btn7.TabIndex = 6;
            this.btn7.UseVisualStyleBackColor = true;
            // 
            // btn8
            // 
            this.btn8.Location = new System.Drawing.Point(93, 174);
            this.btn8.Name = "btn8";
            this.btn8.Size = new System.Drawing.Size(75, 75);
            this.btn8.TabIndex = 7;
            this.btn8.UseVisualStyleBackColor = true;
            // 
            // btn9
            // 
            this.btn9.Location = new System.Drawing.Point(174, 174);
            this.btn9.Name = "btn9";
            this.btn9.Size = new System.Drawing.Size(75, 75);
            this.btn9.TabIndex = 8;
            this.btn9.UseVisualStyleBackColor = true;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(12, 260);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(51, 13);
            this.lblStatus.TabIndex = 9;
            this.lblStatus.Text = "Your turn";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(260, 282);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btn9);
            this.Controls.Add(this.btn8);
            this.Controls.Add(this.btn7);
            this.Controls.Add(this.btn6);
            this.Controls.Add(this.btn5);
            this.Controls.Add(this.btn4);
            this.Controls.Add(this.btn3);
            this.Controls.Add(this.btn2);
            this.Controls.Add(this.btn1);
            this.Name = "Form1";
            this.Text = "Tic Tac Toe";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Button btn1;
        private System.Windows.Forms.Button btn2;
        private System.Windows.Forms.Button btn3;
        private System.Windows.Forms.Button btn4;
        private System.Windows.Forms.Button btn5;
        private System.Windows.Forms.Button btn6;
        private System.Windows.Forms.Button btn7;
        private System.Windows.Forms.Button btn8;
        private System.Windows.Forms.Button btn9;
        private System.Windows.Forms.Label lblStatus;
    }
}

