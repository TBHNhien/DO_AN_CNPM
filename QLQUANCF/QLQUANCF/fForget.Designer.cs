namespace QLQUANCF
{
    partial class fForget
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fForget));
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnExit = new System.Windows.Forms.Button();
            this.MailAddress = new System.Windows.Forms.Label();
            this.txbMailRecover = new System.Windows.Forms.TextBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.textBox2);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.btnExit);
            this.panel1.Controls.Add(this.MailAddress);
            this.panel1.Controls.Add(this.txbMailRecover);
            this.panel1.Font = new System.Drawing.Font("Arial", 16.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.panel1.Location = new System.Drawing.Point(10, 9);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(713, 394);
            this.panel1.TabIndex = 0;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 16.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(108, 246);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(225, 27);
            this.label2.TabIndex = 9;
            this.label2.Text = "Nhập lại mật khẩu :";
            this.label2.Click += new System.EventHandler(this.label2_Click_1);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 16.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(88, 171);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(240, 27);
            this.label1.TabIndex = 8;
            this.label1.Text = "Nhập mật khẩu mới :";
            // 
            // textBox2
            // 
            this.textBox2.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.textBox2.Location = new System.Drawing.Point(360, 245);
            this.textBox2.Name = "textBox2";
            this.textBox2.PlaceholderText = "Nhập lại password";
            this.textBox2.Size = new System.Drawing.Size(347, 29);
            this.textBox2.TabIndex = 7;
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.textBox1.Location = new System.Drawing.Point(360, 170);
            this.textBox1.Name = "textBox1";
            this.textBox1.PlaceholderText = "Nhập password";
            this.textBox1.Size = new System.Drawing.Size(347, 29);
            this.textBox1.TabIndex = 6;
            // 
            // btnExit
            // 
            this.btnExit.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnExit.ForeColor = System.Drawing.Color.IndianRed;
            this.btnExit.Location = new System.Drawing.Point(265, 325);
            this.btnExit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(170, 29);
            this.btnExit.TabIndex = 5;
            this.btnExit.Text = "Xác nhận";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // MailAddress
            // 
            this.MailAddress.AutoSize = true;
            this.MailAddress.Font = new System.Drawing.Font("Arial", 16.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.MailAddress.Location = new System.Drawing.Point(81, 98);
            this.MailAddress.Name = "MailAddress";
            this.MailAddress.Size = new System.Drawing.Size(251, 27);
            this.MailAddress.TabIndex = 2;
            this.MailAddress.Text = "Nhập email hoặc sđt :\r\n";
            this.MailAddress.Click += new System.EventHandler(this.label2_Click);
            // 
            // txbMailRecover
            // 
            this.txbMailRecover.AccessibleDescription = "";
            this.txbMailRecover.AccessibleName = "";
            this.txbMailRecover.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txbMailRecover.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.txbMailRecover.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txbMailRecover.Location = new System.Drawing.Point(363, 98);
            this.txbMailRecover.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txbMailRecover.Name = "txbMailRecover";
            this.txbMailRecover.PlaceholderText = "Nhập vào email hoặc sđt";
            this.txbMailRecover.Size = new System.Drawing.Size(347, 29);
            this.txbMailRecover.TabIndex = 0;
            this.txbMailRecover.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            // 
            // fForget
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(732, 414);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.Name = "fForget";
            this.Text = "fForget Password";
            this.Load += new System.EventHandler(this.fForget_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Panel panel1;
        private TextBox txbMailRecover;
        private Label MailAddress;
        private Button btnExit;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private TextBox textBox2;
        private TextBox textBox1;
        private Label label1;
        private Label label2;
    }
}