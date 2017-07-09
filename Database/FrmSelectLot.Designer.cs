namespace LCY_Database
{
    partial class FrmSelectLot
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
            this.textFilter = new System.Windows.Forms.TextBox();
            this.lvSelect = new System.Windows.Forms.ListView();
            this.colLotName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvSelectedLots = new System.Windows.Forms.ListView();
            this.btCancel = new System.Windows.Forms.Button();
            this.btOK = new System.Windows.Forms.Button();
            this.btDel = new System.Windows.Forms.Button();
            this.btSel = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.colLotId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // textFilter
            // 
            this.textFilter.Location = new System.Drawing.Point(12, 12);
            this.textFilter.Name = "textFilter";
            this.textFilter.Size = new System.Drawing.Size(148, 23);
            this.textFilter.TabIndex = 0;
            this.textFilter.TextChanged += new System.EventHandler(this.textFilter_TextChanged);
            // 
            // lvSelect
            // 
            this.lvSelect.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvSelect.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colLotName});
            this.lvSelect.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lvSelect.Location = new System.Drawing.Point(12, 41);
            this.lvSelect.Name = "lvSelect";
            this.lvSelect.OwnerDraw = true;
            this.lvSelect.Size = new System.Drawing.Size(170, 220);
            this.lvSelect.TabIndex = 1;
            this.lvSelect.UseCompatibleStateImageBehavior = false;
            this.lvSelect.View = System.Windows.Forms.View.List;
            this.lvSelect.DrawItem += new System.Windows.Forms.DrawListViewItemEventHandler(this.lvSelect_DrawItem);
            // 
            // colLotName
            // 
            this.colLotName.Text = "Code";
            this.colLotName.Width = 166;
            // 
            // lvSelectedLots
            // 
            this.lvSelectedLots.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colLotId});
            this.lvSelectedLots.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lvSelectedLots.Location = new System.Drawing.Point(232, 41);
            this.lvSelectedLots.Name = "lvSelectedLots";
            this.lvSelectedLots.OwnerDraw = true;
            this.lvSelectedLots.Size = new System.Drawing.Size(170, 220);
            this.lvSelectedLots.TabIndex = 2;
            this.lvSelectedLots.UseCompatibleStateImageBehavior = false;
            this.lvSelectedLots.View = System.Windows.Forms.View.List;
            this.lvSelectedLots.DrawItem += new System.Windows.Forms.DrawListViewItemEventHandler(this.lvSelect_DrawItem);
            this.lvSelectedLots.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvSelectedLots_KeyDown);
            // 
            // btCancel
            // 
            this.btCancel.Location = new System.Drawing.Point(327, 273);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 29);
            this.btCancel.TabIndex = 15;
            this.btCancel.Text = "Cancel";
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // btOK
            // 
            this.btOK.Location = new System.Drawing.Point(246, 273);
            this.btOK.Name = "btOK";
            this.btOK.Size = new System.Drawing.Size(75, 29);
            this.btOK.TabIndex = 14;
            this.btOK.Text = "OK";
            this.btOK.UseVisualStyleBackColor = true;
            this.btOK.Click += new System.EventHandler(this.btOK_Click);
            // 
            // btDel
            // 
            this.btDel.BackgroundImage = global::LCY_Database.Properties.Resources.selectleft;
            this.btDel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btDel.FlatAppearance.BorderSize = 0;
            this.btDel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btDel.Location = new System.Drawing.Point(192, 140);
            this.btDel.Name = "btDel";
            this.btDel.Size = new System.Drawing.Size(30, 30);
            this.btDel.TabIndex = 17;
            this.btDel.UseVisualStyleBackColor = true;
            this.btDel.Click += new System.EventHandler(this.btDel_Click);
            // 
            // btSel
            // 
            this.btSel.BackgroundImage = global::LCY_Database.Properties.Resources.selectright;
            this.btSel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btSel.FlatAppearance.BorderSize = 0;
            this.btSel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btSel.Location = new System.Drawing.Point(192, 90);
            this.btSel.Name = "btSel";
            this.btSel.Size = new System.Drawing.Size(30, 30);
            this.btSel.TabIndex = 16;
            this.btSel.UseVisualStyleBackColor = true;
            this.btSel.Click += new System.EventHandler(this.btSel_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::LCY_Database.Properties.Resources.filter;
            this.pictureBox1.Location = new System.Drawing.Point(161, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(23, 23);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 18;
            this.pictureBox1.TabStop = false;
            // 
            // FrmSelectLot
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(414, 310);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btDel);
            this.Controls.Add(this.btSel);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.btOK);
            this.Controls.Add(this.lvSelectedLots);
            this.Controls.Add(this.lvSelect);
            this.Controls.Add(this.textFilter);
            this.Font = new System.Drawing.Font("Microsoft JhengHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmSelectLot";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "選擇批號...";
            this.Load += new System.EventHandler(this.FrmSelectLot_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textFilter;
        private System.Windows.Forms.ListView lvSelect;
        private System.Windows.Forms.ListView lvSelectedLots;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.Button btOK;
        private System.Windows.Forms.ColumnHeader colLotName;
        private System.Windows.Forms.Button btSel;
        private System.Windows.Forms.Button btDel;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ColumnHeader colLotId;
    }
}