namespace LCY_Database
{
    partial class FrmCreateTable
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.cbUser = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbType = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textPurpose = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dtPickerApply = new System.Windows.Forms.DateTimePicker();
            this.dtPickerExpDate = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.cbCTool = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cbITool = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBWeight = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textBNumber = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.textGrade = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.cbCustomer = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.ckCompetitor = new System.Windows.Forms.CheckBox();
            this.lvFormula = new System.Windows.Forms.ListView();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.colCheck = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colCMPDProcess = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.colCMPDTemp = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.colINJProcess = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.colINJTemp = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.colRemark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btProperty = new System.Windows.Forms.Button();
            this.btAdd = new System.Windows.Forms.Button();
            this.btDelete = new System.Windows.Forms.Button();
            this.btCopy = new System.Windows.Forms.Button();
            this.btOK = new System.Windows.Forms.Button();
            this.btCancel = new System.Windows.Forms.Button();
            this.btFormulation = new System.Windows.Forms.Button();
            this.lvProperty = new System.Windows.Forms.ListView();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.linkLab = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(19, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "申請人";
            // 
            // cbUser
            // 
            this.cbUser.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbUser.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbUser.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.cbUser.FormattingEnabled = true;
            this.cbUser.Location = new System.Drawing.Point(81, 12);
            this.cbUser.Name = "cbUser";
            this.cbUser.Size = new System.Drawing.Size(162, 27);
            this.cbUser.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label2.Location = new System.Drawing.Point(19, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 19);
            this.label2.TabIndex = 2;
            this.label2.Text = "實驗目的";
            // 
            // cbType
            // 
            this.cbType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbType.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.cbType.FormattingEnabled = true;
            this.cbType.Location = new System.Drawing.Point(81, 106);
            this.cbType.Name = "cbType";
            this.cbType.Size = new System.Drawing.Size(162, 27);
            this.cbType.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label3.Location = new System.Drawing.Point(19, 110);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 19);
            this.label3.TabIndex = 4;
            this.label3.Text = "產品類型";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // textPurpose
            // 
            this.textPurpose.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textPurpose.Location = new System.Drawing.Point(81, 50);
            this.textPurpose.Multiline = true;
            this.textPurpose.Name = "textPurpose";
            this.textPurpose.Size = new System.Drawing.Size(392, 43);
            this.textPurpose.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label4.Location = new System.Drawing.Point(19, 149);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 19);
            this.label4.TabIndex = 7;
            this.label4.Text = "申請日期";
            // 
            // dtPickerApply
            // 
            this.dtPickerApply.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dtPickerApply.Location = new System.Drawing.Point(81, 146);
            this.dtPickerApply.Name = "dtPickerApply";
            this.dtPickerApply.Size = new System.Drawing.Size(162, 27);
            this.dtPickerApply.TabIndex = 8;
            // 
            // dtPickerExpDate
            // 
            this.dtPickerExpDate.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dtPickerExpDate.Location = new System.Drawing.Point(320, 146);
            this.dtPickerExpDate.Name = "dtPickerExpDate";
            this.dtPickerExpDate.Size = new System.Drawing.Size(153, 27);
            this.dtPickerExpDate.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label5.Location = new System.Drawing.Point(258, 149);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 19);
            this.label5.TabIndex = 9;
            this.label5.Text = "實驗日期";
            // 
            // cbCTool
            // 
            this.cbCTool.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbCTool.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbCTool.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.cbCTool.FormattingEnabled = true;
            this.cbCTool.Location = new System.Drawing.Point(81, 183);
            this.cbCTool.Name = "cbCTool";
            this.cbCTool.Size = new System.Drawing.Size(165, 27);
            this.cbCTool.TabIndex = 12;
            this.cbCTool.SelectedIndexChanged += new System.EventHandler(this.cbCTool_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label6.Location = new System.Drawing.Point(19, 187);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(69, 19);
            this.label6.TabIndex = 11;
            this.label6.Text = "混練機台";
            // 
            // cbITool
            // 
            this.cbITool.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbITool.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbITool.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.cbITool.FormattingEnabled = true;
            this.cbITool.Location = new System.Drawing.Point(320, 183);
            this.cbITool.Name = "cbITool";
            this.cbITool.Size = new System.Drawing.Size(153, 27);
            this.cbITool.TabIndex = 14;
            this.cbITool.SelectedIndexChanged += new System.EventHandler(this.cbITool_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label7.Location = new System.Drawing.Point(258, 187);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(69, 19);
            this.label7.TabIndex = 13;
            this.label7.Text = "射出機台";
            // 
            // textBWeight
            // 
            this.textBWeight.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBWeight.Location = new System.Drawing.Point(591, 36);
            this.textBWeight.Name = "textBWeight";
            this.textBWeight.Size = new System.Drawing.Size(121, 27);
            this.textBWeight.TabIndex = 16;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label8.Location = new System.Drawing.Point(553, 39);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(39, 19);
            this.label8.TabIndex = 15;
            this.label8.Text = "袋重";
            // 
            // textBNumber
            // 
            this.textBNumber.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBNumber.Location = new System.Drawing.Point(591, 74);
            this.textBNumber.Name = "textBNumber";
            this.textBNumber.Size = new System.Drawing.Size(121, 27);
            this.textBNumber.TabIndex = 18;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label9.Location = new System.Drawing.Point(553, 77);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(39, 19);
            this.label9.TabIndex = 17;
            this.label9.Text = "數量";
            // 
            // textGrade
            // 
            this.textGrade.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textGrade.Location = new System.Drawing.Point(591, 112);
            this.textGrade.Name = "textGrade";
            this.textGrade.Size = new System.Drawing.Size(121, 27);
            this.textGrade.TabIndex = 20;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label10.Location = new System.Drawing.Point(529, 115);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(69, 19);
            this.label10.TabIndex = 19;
            this.label10.Text = "試料牌號";
            // 
            // cbCustomer
            // 
            this.cbCustomer.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbCustomer.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbCustomer.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.cbCustomer.FormattingEnabled = true;
            this.cbCustomer.Location = new System.Drawing.Point(591, 149);
            this.cbCustomer.Name = "cbCustomer";
            this.cbCustomer.Size = new System.Drawing.Size(121, 27);
            this.cbCustomer.TabIndex = 22;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label11.Location = new System.Drawing.Point(553, 153);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(39, 19);
            this.label11.TabIndex = 21;
            this.label11.Text = "客戶";
            // 
            // ckCompetitor
            // 
            this.ckCompetitor.AutoSize = true;
            this.ckCompetitor.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.ckCompetitor.Location = new System.Drawing.Point(546, 201);
            this.ckCompetitor.Name = "ckCompetitor";
            this.ckCompetitor.Size = new System.Drawing.Size(166, 23);
            this.ckCompetitor.TabIndex = 23;
            this.ckCompetitor.Text = "設置為競爭者實驗單";
            this.ckCompetitor.UseVisualStyleBackColor = true;
            this.ckCompetitor.CheckedChanged += new System.EventHandler(this.ckCompetitor_CheckedChanged);
            // 
            // lvFormula
            // 
            this.lvFormula.Alignment = System.Windows.Forms.ListViewAlignment.SnapToGrid;
            this.lvFormula.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvFormula.BackColor = System.Drawing.SystemColors.Menu;
            this.lvFormula.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lvFormula.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lvFormula.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.lvFormula.HoverSelection = true;
            this.lvFormula.Location = new System.Drawing.Point(81, 230);
            this.lvFormula.MultiSelect = false;
            this.lvFormula.Name = "lvFormula";
            this.lvFormula.OwnerDraw = true;
            this.lvFormula.ShowItemToolTips = true;
            this.lvFormula.Size = new System.Drawing.Size(669, 52);
            this.lvFormula.TabIndex = 25;
            this.lvFormula.UseCompatibleStateImageBehavior = false;
            this.lvFormula.View = System.Windows.Forms.View.List;
            this.lvFormula.DrawItem += new System.Windows.Forms.DrawListViewItemEventHandler(this.lvFormula_DrawItem);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colCheck,
            this.colCMPDProcess,
            this.colCMPDTemp,
            this.colINJProcess,
            this.colINJTemp,
            this.colRemark});
            this.dataGridView1.Location = new System.Drawing.Point(12, 288);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridView1.Size = new System.Drawing.Size(738, 119);
            this.dataGridView1.TabIndex = 26;
            this.dataGridView1.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dataGridView1_EditingControlShowing);
            // 
            // colCheck
            // 
            this.colCheck.HeaderText = "";
            this.colCheck.MinimumWidth = 20;
            this.colCheck.Name = "colCheck";
            this.colCheck.Width = 20;
            // 
            // colCMPDProcess
            // 
            this.colCMPDProcess.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
            this.colCMPDProcess.DisplayStyleForCurrentCellOnly = true;
            this.colCMPDProcess.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.colCMPDProcess.HeaderText = "混練加工條件";
            this.colCMPDProcess.Name = "colCMPDProcess";
            // 
            // colCMPDTemp
            // 
            this.colCMPDTemp.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
            this.colCMPDTemp.DisplayStyleForCurrentCellOnly = true;
            this.colCMPDTemp.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.colCMPDTemp.HeaderText = "區段溫度";
            this.colCMPDTemp.Name = "colCMPDTemp";
            // 
            // colINJProcess
            // 
            this.colINJProcess.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
            this.colINJProcess.DisplayStyleForCurrentCellOnly = true;
            this.colINJProcess.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.colINJProcess.HeaderText = "射出加工條件";
            this.colINJProcess.Name = "colINJProcess";
            // 
            // colINJTemp
            // 
            this.colINJTemp.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
            this.colINJTemp.DisplayStyleForCurrentCellOnly = true;
            this.colINJTemp.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.colINJTemp.HeaderText = "射出溫度";
            this.colINJTemp.Name = "colINJTemp";
            // 
            // colRemark
            // 
            this.colRemark.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.colRemark.DefaultCellStyle = dataGridViewCellStyle1;
            this.colRemark.HeaderText = "備註";
            this.colRemark.MinimumWidth = 100;
            this.colRemark.Name = "colRemark";
            this.colRemark.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colRemark.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // btProperty
            // 
            this.btProperty.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btProperty.Location = new System.Drawing.Point(12, 425);
            this.btProperty.Name = "btProperty";
            this.btProperty.Size = new System.Drawing.Size(66, 35);
            this.btProperty.TabIndex = 27;
            this.btProperty.Text = "物性";
            this.btProperty.UseVisualStyleBackColor = true;
            this.btProperty.Click += new System.EventHandler(this.btProperty_Click);
            // 
            // btAdd
            // 
            this.btAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btAdd.Location = new System.Drawing.Point(513, 425);
            this.btAdd.Name = "btAdd";
            this.btAdd.Size = new System.Drawing.Size(75, 33);
            this.btAdd.TabIndex = 29;
            this.btAdd.Text = "新增";
            this.btAdd.UseVisualStyleBackColor = true;
            this.btAdd.Click += new System.EventHandler(this.btAdd_Click);
            // 
            // btDelete
            // 
            this.btDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btDelete.Location = new System.Drawing.Point(675, 425);
            this.btDelete.Name = "btDelete";
            this.btDelete.Size = new System.Drawing.Size(75, 33);
            this.btDelete.TabIndex = 30;
            this.btDelete.Text = "刪除";
            this.btDelete.UseVisualStyleBackColor = true;
            this.btDelete.Click += new System.EventHandler(this.btDelete_Click);
            // 
            // btCopy
            // 
            this.btCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btCopy.Location = new System.Drawing.Point(594, 425);
            this.btCopy.Name = "btCopy";
            this.btCopy.Size = new System.Drawing.Size(75, 33);
            this.btCopy.TabIndex = 31;
            this.btCopy.Text = "複製";
            this.btCopy.UseVisualStyleBackColor = true;
            this.btCopy.Click += new System.EventHandler(this.btCopy_Click);
            // 
            // btOK
            // 
            this.btOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btOK.Location = new System.Drawing.Point(571, 475);
            this.btOK.Name = "btOK";
            this.btOK.Size = new System.Drawing.Size(86, 33);
            this.btOK.TabIndex = 32;
            this.btOK.Text = "建立表單";
            this.btOK.UseVisualStyleBackColor = true;
            this.btOK.Click += new System.EventHandler(this.btOK_Click);
            // 
            // btCancel
            // 
            this.btCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btCancel.Location = new System.Drawing.Point(663, 475);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(87, 33);
            this.btCancel.TabIndex = 33;
            this.btCancel.Text = "取消";
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // btFormulation
            // 
            this.btFormulation.Location = new System.Drawing.Point(12, 230);
            this.btFormulation.Name = "btFormulation";
            this.btFormulation.Size = new System.Drawing.Size(66, 35);
            this.btFormulation.TabIndex = 34;
            this.btFormulation.Text = "配方";
            this.btFormulation.UseVisualStyleBackColor = true;
            this.btFormulation.Click += new System.EventHandler(this.btFormulation_Click);
            // 
            // lvProperty
            // 
            this.lvProperty.Alignment = System.Windows.Forms.ListViewAlignment.SnapToGrid;
            this.lvProperty.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvProperty.BackColor = System.Drawing.SystemColors.Menu;
            this.lvProperty.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lvProperty.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lvProperty.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.lvProperty.HoverSelection = true;
            this.lvProperty.Location = new System.Drawing.Point(93, 425);
            this.lvProperty.MultiSelect = false;
            this.lvProperty.Name = "lvProperty";
            this.lvProperty.OwnerDraw = true;
            this.lvProperty.ShowItemToolTips = true;
            this.lvProperty.Size = new System.Drawing.Size(414, 83);
            this.lvProperty.TabIndex = 35;
            this.lvProperty.UseCompatibleStateImageBehavior = false;
            this.lvProperty.View = System.Windows.Forms.View.List;
            this.lvProperty.DrawItem += new System.Windows.Forms.DrawListViewItemEventHandler(this.lvFormula_DrawItem);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // linkLab
            // 
            this.linkLab.AutoSize = true;
            this.linkLab.Location = new System.Drawing.Point(681, 9);
            this.linkLab.Name = "linkLab";
            this.linkLab.Size = new System.Drawing.Size(69, 19);
            this.linkLab.TabIndex = 36;
            this.linkLab.TabStop = true;
            this.linkLab.Text = "切換帳號";
            this.linkLab.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLab_LinkClicked);
            // 
            // FrmCreateTable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(762, 515);
            this.Controls.Add(this.linkLab);
            this.Controls.Add(this.textPurpose);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbUser);
            this.Controls.Add(this.cbType);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lvProperty);
            this.Controls.Add(this.btFormulation);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.btOK);
            this.Controls.Add(this.btCopy);
            this.Controls.Add(this.btDelete);
            this.Controls.Add(this.btAdd);
            this.Controls.Add(this.btProperty);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.lvFormula);
            this.Controls.Add(this.ckCompetitor);
            this.Controls.Add(this.cbCustomer);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.textGrade);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.textBNumber);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.textBWeight);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.cbITool);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cbCTool);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.dtPickerExpDate);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dtPickerApply);
            this.Controls.Add(this.label4);
            this.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MinimizeBox = false;
            this.Name = "FrmCreateTable";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "建立實驗表";
            this.Load += new System.EventHandler(this.FrmCreateTable_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbUser;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbType;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textPurpose;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtPickerApply;
        private System.Windows.Forms.DateTimePicker dtPickerExpDate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbCTool;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbITool;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBWeight;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBNumber;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textGrade;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cbCustomer;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.CheckBox ckCompetitor;
        private System.Windows.Forms.ListView lvFormula;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btProperty;
        private System.Windows.Forms.Button btAdd;
        private System.Windows.Forms.Button btDelete;
        private System.Windows.Forms.Button btCopy;
        private System.Windows.Forms.Button btOK;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.Button btFormulation;
        private System.Windows.Forms.ListView lvProperty;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colCheck;
        private System.Windows.Forms.DataGridViewComboBoxColumn colCMPDProcess;
        private System.Windows.Forms.DataGridViewComboBoxColumn colCMPDTemp;
        private System.Windows.Forms.DataGridViewComboBoxColumn colINJProcess;
        private System.Windows.Forms.DataGridViewComboBoxColumn colINJTemp;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRemark;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.LinkLabel linkLab;
    }
}

