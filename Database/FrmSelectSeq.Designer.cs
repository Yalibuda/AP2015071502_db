namespace LCY_Database
{
    partial class FrmSelectSeq
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
            this.components = new System.ComponentModel.Container();
            this.ckShowUploaded = new System.Windows.Forms.CheckBox();
            this.lvAvailable = new System.Windows.Forms.ListView();
            this.colAvailableId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.textFilter = new System.Windows.Forms.TextBox();
            this.lvSelected = new System.Windows.Forms.ListView();
            this.colSelectedId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btOK = new System.Windows.Forms.Button();
            this.btCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btSelLeft = new System.Windows.Forms.Button();
            this.btSelRight = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // ckShowUploaded
            // 
            this.ckShowUploaded.AutoSize = true;
            this.ckShowUploaded.Location = new System.Drawing.Point(12, 275);
            this.ckShowUploaded.Name = "ckShowUploaded";
            this.ckShowUploaded.Size = new System.Drawing.Size(87, 20);
            this.ckShowUploaded.TabIndex = 0;
            this.ckShowUploaded.Text = "顯示已上傳";
            this.toolTip1.SetToolTip(this.ckShowUploaded, "於清單中顯示已上傳實驗結果的實驗序號");
            this.ckShowUploaded.UseVisualStyleBackColor = true;
            this.ckShowUploaded.CheckedChanged += new System.EventHandler(this.ckShowUploaded_CheckedChanged);
            // 
            // lvAvailable
            // 
            this.lvAvailable.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colAvailableId});
            this.lvAvailable.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lvAvailable.Location = new System.Drawing.Point(12, 58);
            this.lvAvailable.Name = "lvAvailable";
            this.lvAvailable.Size = new System.Drawing.Size(144, 211);
            this.lvAvailable.TabIndex = 1;
            this.lvAvailable.UseCompatibleStateImageBehavior = false;
            this.lvAvailable.View = System.Windows.Forms.View.Details;
            // 
            // colAvailableId
            // 
            this.colAvailableId.Text = "Lot ID";
            this.colAvailableId.Width = 140;
            // 
            // textFilter
            // 
            this.textFilter.Location = new System.Drawing.Point(12, 9);
            this.textFilter.Name = "textFilter";
            this.textFilter.Size = new System.Drawing.Size(134, 23);
            this.textFilter.TabIndex = 19;
            this.textFilter.TextChanged += new System.EventHandler(this.textFilter_TextChanged);
            // 
            // lvSelected
            // 
            this.lvSelected.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colSelectedId});
            this.lvSelected.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lvSelected.Location = new System.Drawing.Point(198, 58);
            this.lvSelected.Name = "lvSelected";
            this.lvSelected.Size = new System.Drawing.Size(144, 210);
            this.lvSelected.TabIndex = 21;
            this.lvSelected.UseCompatibleStateImageBehavior = false;
            this.lvSelected.View = System.Windows.Forms.View.Details;
            // 
            // colSelectedId
            // 
            this.colSelectedId.Text = "Seq ID";
            this.colSelectedId.Width = 140;
            // 
            // btOK
            // 
            this.btOK.Location = new System.Drawing.Point(190, 294);
            this.btOK.Name = "btOK";
            this.btOK.Size = new System.Drawing.Size(75, 30);
            this.btOK.TabIndex = 24;
            this.btOK.Text = "確定";
            this.btOK.UseVisualStyleBackColor = true;
            this.btOK.Click += new System.EventHandler(this.btOK_Click);
            // 
            // btCancel
            // 
            this.btCancel.Location = new System.Drawing.Point(271, 294);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 30);
            this.btCancel.TabIndex = 25;
            this.btCancel.Text = "取消";
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft JhengHei", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(12, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 16);
            this.label1.TabIndex = 26;
            this.label1.Text = "可選擇項";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft JhengHei", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label2.Location = new System.Drawing.Point(195, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 16);
            this.label2.TabIndex = 27;
            this.label2.Text = "已選擇";
            // 
            // btSelLeft
            // 
            this.btSelLeft.BackgroundImage = global::LCY_Database.Properties.Resources.selectleft;
            this.btSelLeft.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btSelLeft.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btSelLeft.FlatAppearance.BorderSize = 0;
            this.btSelLeft.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btSelLeft.Location = new System.Drawing.Point(162, 160);
            this.btSelLeft.Name = "btSelLeft";
            this.btSelLeft.Size = new System.Drawing.Size(30, 30);
            this.btSelLeft.TabIndex = 23;
            this.btSelLeft.UseVisualStyleBackColor = true;
            this.btSelLeft.Click += new System.EventHandler(this.btSelLeft_Click);
            // 
            // btSelRight
            // 
            this.btSelRight.BackgroundImage = global::LCY_Database.Properties.Resources.selectright;
            this.btSelRight.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btSelRight.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btSelRight.FlatAppearance.BorderSize = 0;
            this.btSelRight.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btSelRight.Location = new System.Drawing.Point(162, 114);
            this.btSelRight.Name = "btSelRight";
            this.btSelRight.Size = new System.Drawing.Size(30, 30);
            this.btSelRight.TabIndex = 22;
            this.btSelRight.UseVisualStyleBackColor = true;
            this.btSelRight.Click += new System.EventHandler(this.btSelRight_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::LCY_Database.Properties.Resources.filter;
            this.pictureBox1.Location = new System.Drawing.Point(147, 9);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(23, 23);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 28;
            this.pictureBox1.TabStop = false;
            // 
            // FrmSelectSeq
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(358, 336);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lvAvailable);
            this.Controls.Add(this.lvSelected);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.btOK);
            this.Controls.Add(this.btSelLeft);
            this.Controls.Add(this.btSelRight);
            this.Controls.Add(this.textFilter);
            this.Controls.Add(this.ckShowUploaded);
            this.Font = new System.Drawing.Font("Microsoft JhengHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmSelectSeq";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "選擇實驗序號";
            this.Load += new System.EventHandler(this.FrmSelectSeq_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox ckShowUploaded;
        private System.Windows.Forms.ListView lvAvailable;
        private System.Windows.Forms.TextBox textFilter;
        private System.Windows.Forms.ListView lvSelected;
        private System.Windows.Forms.Button btSelRight;
        private System.Windows.Forms.Button btSelLeft;
        private System.Windows.Forms.Button btOK;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ColumnHeader colAvailableId;
        private System.Windows.Forms.ColumnHeader colSelectedId;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}