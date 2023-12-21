namespace Order
{
    partial class SignupForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SignupForm));
            this.labelLogin = new System.Windows.Forms.Label();
            this.labelAbout = new System.Windows.Forms.Label();
            this.labelCon = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.labelForgot = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SignButton = new System.Windows.Forms.Button();
            this.tboxName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tboxPass = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cboxType = new System.Windows.Forms.ComboBox();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelLogin
            // 
            this.labelLogin.AutoSize = true;
            this.labelLogin.Font = new System.Drawing.Font("Courier New", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelLogin.ForeColor = System.Drawing.Color.Blue;
            this.labelLogin.Location = new System.Drawing.Point(396, 106);
            this.labelLogin.Name = "labelLogin";
            this.labelLogin.Size = new System.Drawing.Size(82, 23);
            this.labelLogin.TabIndex = 2;
            this.labelLogin.Text = "Log in";
            this.labelLogin.Click += new System.EventHandler(this.labelLogin_Click);
            // 
            // labelAbout
            // 
            this.labelAbout.AutoSize = true;
            this.labelAbout.Font = new System.Drawing.Font("Courier New", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAbout.ForeColor = System.Drawing.Color.Blue;
            this.labelAbout.Location = new System.Drawing.Point(652, 106);
            this.labelAbout.Name = "labelAbout";
            this.labelAbout.Size = new System.Drawing.Size(106, 23);
            this.labelAbout.TabIndex = 3;
            this.labelAbout.Text = "About us";
            // 
            // labelCon
            // 
            this.labelCon.AutoSize = true;
            this.labelCon.Font = new System.Drawing.Font("Courier New", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCon.ForeColor = System.Drawing.Color.Blue;
            this.labelCon.Location = new System.Drawing.Point(514, 106);
            this.labelCon.Name = "labelCon";
            this.labelCon.Size = new System.Drawing.Size(94, 23);
            this.labelCon.TabIndex = 4;
            this.labelCon.Text = "Contact";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.RosyBrown;
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Location = new System.Drawing.Point(2, 178);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(310, 416);
            this.panel1.TabIndex = 5;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.RosyBrown;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.labelForgot);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.SignButton);
            this.panel2.Controls.Add(this.tboxName);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.tboxPass);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.cboxType);
            this.panel2.Location = new System.Drawing.Point(304, 178);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(498, 416);
            this.panel2.TabIndex = 6;
            // 
            // labelForgot
            // 
            this.labelForgot.AutoSize = true;
            this.labelForgot.Font = new System.Drawing.Font("Courier New", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelForgot.ForeColor = System.Drawing.Color.Blue;
            this.labelForgot.Location = new System.Drawing.Point(20, 312);
            this.labelForgot.Name = "labelForgot";
            this.labelForgot.Size = new System.Drawing.Size(190, 23);
            this.labelForgot.TabIndex = 3;
            this.labelForgot.Text = "Forgot Password";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Courier New", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(39, 186);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(118, 23);
            this.label3.TabIndex = 3;
            this.label3.Text = "User Type";
            // 
            // SignButton
            // 
            this.SignButton.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.SignButton.Font = new System.Drawing.Font("Courier New", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SignButton.Location = new System.Drawing.Point(252, 245);
            this.SignButton.Name = "SignButton";
            this.SignButton.Size = new System.Drawing.Size(118, 37);
            this.SignButton.TabIndex = 2;
            this.SignButton.Text = "Sign up";
            this.SignButton.UseVisualStyleBackColor = false;
            this.SignButton.Click += new System.EventHandler(this.SignButton_Click);
            // 
            // tboxName
            // 
            this.tboxName.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.tboxName.Font = new System.Drawing.Font("Courier New", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tboxName.Location = new System.Drawing.Point(177, 60);
            this.tboxName.Multiline = true;
            this.tboxName.Name = "tboxName";
            this.tboxName.Size = new System.Drawing.Size(262, 39);
            this.tboxName.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Courier New", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(38, 126);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 23);
            this.label2.TabIndex = 3;
            this.label2.Text = "Password";
            // 
            // tboxPass
            // 
            this.tboxPass.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.tboxPass.Font = new System.Drawing.Font("Courier New", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tboxPass.Location = new System.Drawing.Point(177, 126);
            this.tboxPass.Multiline = true;
            this.tboxPass.Name = "tboxPass";
            this.tboxPass.Size = new System.Drawing.Size(262, 39);
            this.tboxPass.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Courier New", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(39, 60);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(106, 23);
            this.label4.TabIndex = 3;
            this.label4.Text = "Fullname";
            // 
            // cboxType
            // 
            this.cboxType.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.cboxType.Font = new System.Drawing.Font("Courier New", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboxType.FormattingEnabled = true;
            this.cboxType.Items.AddRange(new object[] {
            "Customer",
            "Admin",
            "Seller",
            "Delivery Driver"});
            this.cboxType.Location = new System.Drawing.Point(177, 186);
            this.cboxType.Name = "cboxType";
            this.cboxType.Size = new System.Drawing.Size(262, 31);
            this.cboxType.TabIndex = 1;
            // 
            // SignupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.OldLace;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(800, 585);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.labelLogin);
            this.Controls.Add(this.labelAbout);
            this.Controls.Add(this.labelCon);
            this.Name = "SignupForm";
            this.Text = "Signup";
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelLogin;
        private System.Windows.Forms.Label labelAbout;
        private System.Windows.Forms.Label labelCon;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label labelForgot;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button SignButton;
        private System.Windows.Forms.TextBox tboxName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tboxPass;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cboxType;
    }
}