namespace LCY_Database
{
    partial class FrmLogon
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
            this.labACC = new System.Windows.Forms.Label();
            this.labPSW = new System.Windows.Forms.Label();
            this.textACC = new System.Windows.Forms.TextBox();
            this.textPSW = new System.Windows.Forms.TextBox();
            this.btLogon = new System.Windows.Forms.Button();
            this.btCancel = new System.Windows.Forms.Button();
            this.rbtRegular = new System.Windows.Forms.RadioButton();
            this.rbtGuest = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // labACC
            // 
            this.labACC.AutoSize = true;
            this.labACC.Location = new System.Drawing.Point(12, 40);
            this.labACC.Name = "labACC";
            this.labACC.Size = new System.Drawing.Size(39, 19);
            this.labACC.TabIndex = 0;
            this.labACC.Text = "帳號";
            // 
            // labPSW
            // 
            this.labPSW.AutoSize = true;
            this.labPSW.Location = new System.Drawing.Point(12, 69);
            this.labPSW.Name = "labPSW";
            this.labPSW.Size = new System.Drawing.Size(39, 19);
            this.labPSW.TabIndex = 1;
            this.labPSW.Text = "密碼";
            // 
            // textACC
            // 
            this.textACC.Location = new System.Drawing.Point(50, 37);
            this.textACC.Name = "textACC";
            this.textACC.Size = new System.Drawing.Size(198, 27);
            this.textACC.TabIndex = 2;
            this.textACC.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textACC_KeyDown);
            // 
            // textPSW
            // 
            this.textPSW.Location = new System.Drawing.Point(50, 66);
            this.textPSW.Name = "textPSW";
            this.textPSW.PasswordChar = '*';
            this.textPSW.Size = new System.Drawing.Size(198, 27);
            this.textPSW.TabIndex = 3;
            this.textPSW.UseSystemPasswordChar = true;
            this.textPSW.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textPSW_KeyDown);
            // 
            // btLogon
            // 
            this.btLogon.Location = new System.Drawing.Point(108, 101);
            this.btLogon.Name = "btLogon";
            this.btLogon.Size = new System.Drawing.Size(75, 34);
            this.btLogon.TabIndex = 4;
            this.btLogon.Text = "登入";
            this.btLogon.UseVisualStyleBackColor = true;
            this.btLogon.Click += new System.EventHandler(this.btLogon_Click);
            // 
            // btCancel
            // 
            this.btCancel.Location = new System.Drawing.Point(189, 101);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 34);
            this.btCancel.TabIndex = 5;
            this.btCancel.Text = "取消";
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // rbtRegular
            // 
            this.rbtRegular.AutoSize = true;
            this.rbtRegular.Checked = true;
            this.rbtRegular.Location = new System.Drawing.Point(15, 11);
            this.rbtRegular.Name = "rbtRegular";
            this.rbtRegular.Size = new System.Drawing.Size(150, 23);
            this.rbtRegular.TabIndex = 6;
            this.rbtRegular.TabStop = true;
            this.rbtRegular.Text = "使用一般帳號登入";
            this.rbtRegular.UseVisualStyleBackColor = true;
            this.rbtRegular.CheckedChanged += new System.EventHandler(this.rbtRegular_CheckedChanged);
            // 
            // rbtGuest
            // 
            this.rbtGuest.AutoSize = true;
            this.rbtGuest.Enabled = false;
            this.rbtGuest.Location = new System.Drawing.Point(148, 11);
            this.rbtGuest.Name = "rbtGuest";
            this.rbtGuest.Size = new System.Drawing.Size(90, 23);
            this.rbtGuest.TabIndex = 7;
            this.rbtGuest.TabStop = true;
            this.rbtGuest.Text = "訪客登入";
            this.rbtGuest.UseVisualStyleBackColor = true;
            this.rbtGuest.CheckedChanged += new System.EventHandler(this.rbtGuest_CheckedChanged);
            // 
            // FrmLogon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(276, 147);
            this.Controls.Add(this.rbtGuest);
            this.Controls.Add(this.rbtRegular);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.btLogon);
            this.Controls.Add(this.textPSW);
            this.Controls.Add(this.textACC);
            this.Controls.Add(this.labPSW);
            this.Controls.Add(this.labACC);
            this.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmLogon";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "登入...";
            this.Load += new System.EventHandler(this.FrmLogon_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labACC;
        private System.Windows.Forms.Label labPSW;
        private System.Windows.Forms.TextBox textACC;
        private System.Windows.Forms.TextBox textPSW;
        private System.Windows.Forms.Button btLogon;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.RadioButton rbtRegular;
        private System.Windows.Forms.RadioButton rbtGuest;
    }
}