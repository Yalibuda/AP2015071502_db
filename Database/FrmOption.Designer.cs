namespace LCY_Database
{
    partial class FrmOption
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
            this.textServer = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textPort = new System.Windows.Forms.TextBox();
            this.btDefault = new System.Windows.Forms.Button();
            this.btOK = new System.Windows.Forms.Button();
            this.btCancel = new System.Windows.Forms.Button();
            this.labAcc = new System.Windows.Forms.Label();
            this.mtextAccount = new System.Windows.Forms.MaskedTextBox();
            this.mtextPsw = new System.Windows.Forms.MaskedTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textServer
            // 
            this.textServer.Location = new System.Drawing.Point(89, 22);
            this.textServer.Name = "textServer";
            this.textServer.Size = new System.Drawing.Size(209, 23);
            this.textServer.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Server IP";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(42, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Port";
            // 
            // textPort
            // 
            this.textPort.Location = new System.Drawing.Point(89, 53);
            this.textPort.Name = "textPort";
            this.textPort.Size = new System.Drawing.Size(209, 23);
            this.textPort.TabIndex = 3;
            // 
            // btDefault
            // 
            this.btDefault.Location = new System.Drawing.Point(17, 188);
            this.btDefault.Name = "btDefault";
            this.btDefault.Size = new System.Drawing.Size(75, 36);
            this.btDefault.TabIndex = 4;
            this.btDefault.Text = "預設值";
            this.btDefault.UseVisualStyleBackColor = true;
            this.btDefault.Click += new System.EventHandler(this.btDefault_Click);
            // 
            // btOK
            // 
            this.btOK.Location = new System.Drawing.Point(179, 188);
            this.btOK.Name = "btOK";
            this.btOK.Size = new System.Drawing.Size(75, 36);
            this.btOK.TabIndex = 5;
            this.btOK.Text = "確定";
            this.btOK.UseVisualStyleBackColor = true;
            this.btOK.Click += new System.EventHandler(this.btOK_Click);
            // 
            // btCancel
            // 
            this.btCancel.Location = new System.Drawing.Point(260, 188);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 36);
            this.btCancel.TabIndex = 6;
            this.btCancel.Text = "取消";
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // labAcc
            // 
            this.labAcc.AutoSize = true;
            this.labAcc.Location = new System.Drawing.Point(17, 85);
            this.labAcc.Name = "labAcc";
            this.labAcc.Size = new System.Drawing.Size(54, 16);
            this.labAcc.TabIndex = 7;
            this.labAcc.Text = "Account\r\n";
            // 
            // mtextAccount
            // 
            this.mtextAccount.Location = new System.Drawing.Point(89, 82);
            this.mtextAccount.Name = "mtextAccount";
            this.mtextAccount.PasswordChar = '*';
            this.mtextAccount.Size = new System.Drawing.Size(209, 23);
            this.mtextAccount.TabIndex = 8;
            this.mtextAccount.UseSystemPasswordChar = true;
            // 
            // mtextPsw
            // 
            this.mtextPsw.Location = new System.Drawing.Point(89, 111);
            this.mtextPsw.Name = "mtextPsw";
            this.mtextPsw.PasswordChar = '*';
            this.mtextPsw.Size = new System.Drawing.Size(209, 23);
            this.mtextPsw.TabIndex = 10;
            this.mtextPsw.UseSystemPasswordChar = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 114);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 16);
            this.label3.TabIndex = 9;
            this.label3.Text = "Password\n";
            // 
            // FrmOption
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(347, 236);
            this.Controls.Add(this.mtextPsw);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.mtextAccount);
            this.Controls.Add(this.labAcc);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.btOK);
            this.Controls.Add(this.btDefault);
            this.Controls.Add(this.textPort);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textServer);
            this.Font = new System.Drawing.Font("Microsoft JhengHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmOption";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "選項...";
            this.Load += new System.EventHandler(this.FrmOption_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textServer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textPort;
        private System.Windows.Forms.Button btDefault;
        private System.Windows.Forms.Button btOK;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.Label labAcc;
        private System.Windows.Forms.MaskedTextBox mtextAccount;
        private System.Windows.Forms.MaskedTextBox mtextPsw;
        private System.Windows.Forms.Label label3;
    }
}