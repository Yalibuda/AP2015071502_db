namespace LCY_Database
{
    partial class FrmProgress
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
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.labProgress = new System.Windows.Forms.Label();
            this.labStatus = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(6, 28);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(262, 21);
            this.progressBar.TabIndex = 0;
            // 
            // labProgress
            // 
            this.labProgress.Location = new System.Drawing.Point(12, 9);
            this.labProgress.Name = "labProgress";
            this.labProgress.Size = new System.Drawing.Size(256, 16);
            this.labProgress.TabIndex = 1;
            // 
            // labStatus
            // 
            this.labStatus.Location = new System.Drawing.Point(12, 56);
            this.labStatus.Name = "labStatus";
            this.labStatus.Size = new System.Drawing.Size(256, 16);
            this.labStatus.TabIndex = 2;
            // 
            // FrmProgress
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(280, 81);
            this.Controls.Add(this.labStatus);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.labProgress);
            this.Font = new System.Drawing.Font("Microsoft JhengHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "FrmProgress";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label labProgress;
        private System.Windows.Forms.Label labStatus;
    }
}