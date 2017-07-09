namespace LCY_Database
{
    partial class FrmSelectProperty
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
            this.lvSelect = new System.Windows.Forms.ListView();
            this.colFormualtionName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colProdType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.colDelete = new System.Windows.Forms.DataGridViewImageColumn();
            this.btSel = new System.Windows.Forms.Button();
            this.btCancel = new System.Windows.Forms.Button();
            this.btOK = new System.Windows.Forms.Button();
            this.dataGridViewImageColumn1 = new System.Windows.Forms.DataGridViewImageColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // lvSelect
            // 
            this.lvSelect.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colFormualtionName});
            this.lvSelect.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lvSelect.Location = new System.Drawing.Point(12, 12);
            this.lvSelect.Name = "lvSelect";
            this.lvSelect.OwnerDraw = true;
            this.lvSelect.Size = new System.Drawing.Size(165, 257);
            this.lvSelect.TabIndex = 5;
            this.lvSelect.UseCompatibleStateImageBehavior = false;
            this.lvSelect.View = System.Windows.Forms.View.SmallIcon;
            this.lvSelect.DrawItem += new System.Windows.Forms.DrawListViewItemEventHandler(this.lvSelect_DrawItem);
            // 
            // colFormualtionName
            // 
            this.colFormualtionName.Width = 161;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colName,
            this.colProdType,
            this.colType,
            this.colDelete});
            this.dataGridView1.Location = new System.Drawing.Point(183, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(382, 212);
            this.dataGridView1.TabIndex = 7;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // colName
            // 
            this.colName.FillWeight = 120F;
            this.colName.HeaderText = "Name";
            this.colName.MinimumWidth = 150;
            this.colName.Name = "colName";
            this.colName.ReadOnly = true;
            this.colName.Width = 150;
            // 
            // colProdType
            // 
            this.colProdType.HeaderText = "Type";
            this.colProdType.MinimumWidth = 80;
            this.colProdType.Name = "colProdType";
            this.colProdType.ReadOnly = true;
            this.colProdType.Width = 80;
            // 
            // colType
            // 
            this.colType.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.Nothing;
            this.colType.FillWeight = 80F;
            this.colType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.colType.HeaderText = "Unit";
            this.colType.MinimumWidth = 100;
            this.colType.Name = "colType";
            this.colType.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // colDelete
            // 
            this.colDelete.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colDelete.HeaderText = "";
            this.colDelete.Image = global::LCY_Database.Properties.Resources.delete_icon;
            this.colDelete.Name = "colDelete";
            // 
            // btSel
            // 
            this.btSel.Location = new System.Drawing.Point(57, 275);
            this.btSel.Name = "btSel";
            this.btSel.Size = new System.Drawing.Size(75, 29);
            this.btSel.TabIndex = 6;
            this.btSel.Text = "Select";
            this.btSel.UseVisualStyleBackColor = true;
            this.btSel.Click += new System.EventHandler(this.btSel_Click);
            // 
            // btCancel
            // 
            this.btCancel.Location = new System.Drawing.Point(490, 283);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 29);
            this.btCancel.TabIndex = 9;
            this.btCancel.Text = "Cancel";
            this.btCancel.UseVisualStyleBackColor = true;
            // 
            // btOK
            // 
            this.btOK.Location = new System.Drawing.Point(409, 283);
            this.btOK.Name = "btOK";
            this.btOK.Size = new System.Drawing.Size(75, 29);
            this.btOK.TabIndex = 8;
            this.btOK.Text = "OK";
            this.btOK.UseVisualStyleBackColor = true;
            this.btOK.Click += new System.EventHandler(this.btOK_Click);
            // 
            // dataGridViewImageColumn1
            // 
            this.dataGridViewImageColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewImageColumn1.HeaderText = "";
            this.dataGridViewImageColumn1.Image = global::LCY_Database.Properties.Resources.delete_icon;
            this.dataGridViewImageColumn1.Name = "dataGridViewImageColumn1";
            // 
            // FrmSelectProperty
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(577, 324);
            this.Controls.Add(this.lvSelect);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btSel);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.btOK);
            this.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmSelectProperty";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "選擇物性...";
            this.Load += new System.EventHandler(this.FrmSelectProperty_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvSelect;
        private System.Windows.Forms.ColumnHeader colFormualtionName;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btSel;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn1;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.Button btOK;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProdType;
        private System.Windows.Forms.DataGridViewComboBoxColumn colType;
        private System.Windows.Forms.DataGridViewImageColumn colDelete;
    }
}