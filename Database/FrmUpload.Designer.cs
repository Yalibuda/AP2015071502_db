namespace LCY_Database
{
    partial class FrmUpload
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
            this.tabUpload = new System.Windows.Forms.TabControl();
            this.tabPagTool = new System.Windows.Forms.TabPage();
            this.cbToolName_Tool = new System.Windows.Forms.ComboBox();
            this.labToolName_Tool = new System.Windows.Forms.Label();
            this.btCancel_Tool = new System.Windows.Forms.Button();
            this.btBrowse_Tool = new System.Windows.Forms.Button();
            this.btOK_Tool = new System.Windows.Forms.Button();
            this.textUploadPath_Tool = new System.Windows.Forms.TextBox();
            this.labStep2_Tool = new System.Windows.Forms.Label();
            this.btDownloadSample_Tool = new System.Windows.Forms.Button();
            this.labStep1_Tool = new System.Windows.Forms.Label();
            this.labPurpose_Tool = new System.Windows.Forms.Label();
            this.cbPurpose_Tool = new System.Windows.Forms.ComboBox();
            this.tabPagePlan = new System.Windows.Forms.TabPage();
            this.labTestPlan = new System.Windows.Forms.Label();
            this.btCancel_Plan = new System.Windows.Forms.Button();
            this.btOK_Plan = new System.Windows.Forms.Button();
            this.dataGridViewTestPlan = new System.Windows.Forms.DataGridView();
            this.labTestPlanName = new System.Windows.Forms.Label();
            this.labToolInfo = new System.Windows.Forms.Label();
            this.cbTestPlanEdit = new System.Windows.Forms.ComboBox();
            this.btPlanRead = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.cbCondition_Plan = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cbToolName_Plan = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbToolType_Plan = new System.Windows.Forms.ComboBox();
            this.textTestPlanName = new System.Windows.Forms.TextBox();
            this.cbSelTestPlan = new System.Windows.Forms.ComboBox();
            this.tabPageItems = new System.Windows.Forms.TabPage();
            this.label10 = new System.Windows.Forms.Label();
            this.cbPurpose_Item = new System.Windows.Forms.ComboBox();
            this.btCancel_Item = new System.Windows.Forms.Button();
            this.btOK_item = new System.Windows.Forms.Button();
            this.btBrowse_Items = new System.Windows.Forms.Button();
            this.textUploadPath_Item = new System.Windows.Forms.TextBox();
            this.labStep2_Item = new System.Windows.Forms.Label();
            this.btDownloadSampe_Item = new System.Windows.Forms.Button();
            this.labStep1_Item = new System.Windows.Forms.Label();
            this.rbtTItem = new System.Windows.Forms.RadioButton();
            this.rbtRItem = new System.Windows.Forms.RadioButton();
            this.dataGridView_Item = new System.Windows.Forms.DataGridView();
            this.tabPageExp = new System.Windows.Forms.TabPage();
            this.textUploadPath_Result = new System.Windows.Forms.TextBox();
            this.lvSelectedSeqID = new System.Windows.Forms.ListView();
            this.btDownloadSample_Result = new System.Windows.Forms.Button();
            this.labStep2_Result = new System.Windows.Forms.Label();
            this.btSetSeqId_Result = new System.Windows.Forms.Button();
            this.labStep3_Result = new System.Windows.Forms.Label();
            this.labStep1_Result = new System.Windows.Forms.Label();
            this.btCancel_Result = new System.Windows.Forms.Button();
            this.btOK_Result = new System.Windows.Forms.Button();
            this.btBrowse_Result = new System.Windows.Forms.Button();
            this.tabPageCustomer = new System.Windows.Forms.TabPage();
            this.textUploadPath_Customer = new System.Windows.Forms.TextBox();
            this.labStep1_Customer = new System.Windows.Forms.Label();
            this.btBrowse_Customer = new System.Windows.Forms.Button();
            this.btCancel_Customer = new System.Windows.Forms.Button();
            this.btOK_Customer = new System.Windows.Forms.Button();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.linkLab = new System.Windows.Forms.LinkLabel();
            this.backgroundWorker2 = new System.ComponentModel.BackgroundWorker();
            this.tabUpload.SuspendLayout();
            this.tabPagTool.SuspendLayout();
            this.tabPagePlan.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTestPlan)).BeginInit();
            this.tabPageItems.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Item)).BeginInit();
            this.tabPageExp.SuspendLayout();
            this.tabPageCustomer.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabUpload
            // 
            this.tabUpload.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabUpload.Controls.Add(this.tabPagTool);
            this.tabUpload.Controls.Add(this.tabPagePlan);
            this.tabUpload.Controls.Add(this.tabPageItems);
            this.tabUpload.Controls.Add(this.tabPageExp);
            this.tabUpload.Controls.Add(this.tabPageCustomer);
            this.tabUpload.Location = new System.Drawing.Point(6, 30);
            this.tabUpload.Name = "tabUpload";
            this.tabUpload.SelectedIndex = 0;
            this.tabUpload.Size = new System.Drawing.Size(518, 271);
            this.tabUpload.TabIndex = 0;
            // 
            // tabPagTool
            // 
            this.tabPagTool.Controls.Add(this.cbToolName_Tool);
            this.tabPagTool.Controls.Add(this.labToolName_Tool);
            this.tabPagTool.Controls.Add(this.btCancel_Tool);
            this.tabPagTool.Controls.Add(this.btBrowse_Tool);
            this.tabPagTool.Controls.Add(this.btOK_Tool);
            this.tabPagTool.Controls.Add(this.textUploadPath_Tool);
            this.tabPagTool.Controls.Add(this.labStep2_Tool);
            this.tabPagTool.Controls.Add(this.btDownloadSample_Tool);
            this.tabPagTool.Controls.Add(this.labStep1_Tool);
            this.tabPagTool.Controls.Add(this.labPurpose_Tool);
            this.tabPagTool.Controls.Add(this.cbPurpose_Tool);
            this.tabPagTool.Location = new System.Drawing.Point(4, 25);
            this.tabPagTool.Name = "tabPagTool";
            this.tabPagTool.Padding = new System.Windows.Forms.Padding(3);
            this.tabPagTool.Size = new System.Drawing.Size(510, 242);
            this.tabPagTool.TabIndex = 0;
            this.tabPagTool.Text = "機台資訊";
            this.tabPagTool.UseVisualStyleBackColor = true;
            // 
            // cbToolName_Tool
            // 
            this.cbToolName_Tool.FormattingEnabled = true;
            this.cbToolName_Tool.Location = new System.Drawing.Point(280, 17);
            this.cbToolName_Tool.Name = "cbToolName_Tool";
            this.cbToolName_Tool.Size = new System.Drawing.Size(121, 24);
            this.cbToolName_Tool.TabIndex = 8;
            // 
            // labToolName_Tool
            // 
            this.labToolName_Tool.AutoSize = true;
            this.labToolName_Tool.Location = new System.Drawing.Point(218, 21);
            this.labToolName_Tool.Name = "labToolName_Tool";
            this.labToolName_Tool.Size = new System.Drawing.Size(56, 16);
            this.labToolName_Tool.TabIndex = 7;
            this.labToolName_Tool.Text = "機台名稱";
            // 
            // btCancel_Tool
            // 
            this.btCancel_Tool.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btCancel_Tool.Location = new System.Drawing.Point(431, 210);
            this.btCancel_Tool.Name = "btCancel_Tool";
            this.btCancel_Tool.Size = new System.Drawing.Size(71, 29);
            this.btCancel_Tool.TabIndex = 2;
            this.btCancel_Tool.Text = "取消";
            this.btCancel_Tool.UseVisualStyleBackColor = true;
            this.btCancel_Tool.Click += new System.EventHandler(this.btCancel_Tool_Click);
            // 
            // btBrowse_Tool
            // 
            this.btBrowse_Tool.Location = new System.Drawing.Point(371, 111);
            this.btBrowse_Tool.Name = "btBrowse_Tool";
            this.btBrowse_Tool.Size = new System.Drawing.Size(86, 23);
            this.btBrowse_Tool.TabIndex = 6;
            this.btBrowse_Tool.Text = "瀏覽(*CSV)";
            this.btBrowse_Tool.UseVisualStyleBackColor = true;
            this.btBrowse_Tool.Click += new System.EventHandler(this.btBrowse_Tool_Click);
            // 
            // btOK_Tool
            // 
            this.btOK_Tool.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btOK_Tool.Location = new System.Drawing.Point(354, 210);
            this.btOK_Tool.Name = "btOK_Tool";
            this.btOK_Tool.Size = new System.Drawing.Size(71, 29);
            this.btOK_Tool.TabIndex = 1;
            this.btOK_Tool.Text = "確定";
            this.btOK_Tool.UseVisualStyleBackColor = true;
            this.btOK_Tool.Click += new System.EventHandler(this.btOK_Tool_Click);
            // 
            // textUploadPath_Tool
            // 
            this.textUploadPath_Tool.Location = new System.Drawing.Point(57, 111);
            this.textUploadPath_Tool.Name = "textUploadPath_Tool";
            this.textUploadPath_Tool.Size = new System.Drawing.Size(308, 23);
            this.textUploadPath_Tool.TabIndex = 5;
            // 
            // labStep2_Tool
            // 
            this.labStep2_Tool.AutoSize = true;
            this.labStep2_Tool.Location = new System.Drawing.Point(8, 114);
            this.labStep2_Tool.Name = "labStep2_Tool";
            this.labStep2_Tool.Size = new System.Drawing.Size(41, 16);
            this.labStep2_Tool.TabIndex = 4;
            this.labStep2_Tool.Text = "Step2";
            // 
            // btDownloadSample_Tool
            // 
            this.btDownloadSample_Tool.Location = new System.Drawing.Point(57, 66);
            this.btDownloadSample_Tool.Name = "btDownloadSample_Tool";
            this.btDownloadSample_Tool.Size = new System.Drawing.Size(75, 23);
            this.btDownloadSample_Tool.TabIndex = 3;
            this.btDownloadSample_Tool.Text = "下載範本";
            this.btDownloadSample_Tool.UseVisualStyleBackColor = true;
            this.btDownloadSample_Tool.Click += new System.EventHandler(this.btDownloadSample_Tool_Click);
            // 
            // labStep1_Tool
            // 
            this.labStep1_Tool.AutoSize = true;
            this.labStep1_Tool.Location = new System.Drawing.Point(8, 69);
            this.labStep1_Tool.Name = "labStep1_Tool";
            this.labStep1_Tool.Size = new System.Drawing.Size(41, 16);
            this.labStep1_Tool.TabIndex = 2;
            this.labStep1_Tool.Text = "Step1";
            // 
            // labPurpose_Tool
            // 
            this.labPurpose_Tool.AutoSize = true;
            this.labPurpose_Tool.Location = new System.Drawing.Point(8, 21);
            this.labPurpose_Tool.Name = "labPurpose_Tool";
            this.labPurpose_Tool.Size = new System.Drawing.Size(32, 16);
            this.labPurpose_Tool.TabIndex = 1;
            this.labPurpose_Tool.Text = "目的";
            // 
            // cbPurpose_Tool
            // 
            this.cbPurpose_Tool.FormattingEnabled = true;
            this.cbPurpose_Tool.Location = new System.Drawing.Point(57, 17);
            this.cbPurpose_Tool.Name = "cbPurpose_Tool";
            this.cbPurpose_Tool.Size = new System.Drawing.Size(121, 24);
            this.cbPurpose_Tool.TabIndex = 0;
            this.cbPurpose_Tool.SelectedIndexChanged += new System.EventHandler(this.cbPurpose_Tool_SelectedIndexChanged);
            // 
            // tabPagePlan
            // 
            this.tabPagePlan.Controls.Add(this.labTestPlan);
            this.tabPagePlan.Controls.Add(this.btCancel_Plan);
            this.tabPagePlan.Controls.Add(this.btOK_Plan);
            this.tabPagePlan.Controls.Add(this.dataGridViewTestPlan);
            this.tabPagePlan.Controls.Add(this.labTestPlanName);
            this.tabPagePlan.Controls.Add(this.labToolInfo);
            this.tabPagePlan.Controls.Add(this.cbTestPlanEdit);
            this.tabPagePlan.Controls.Add(this.btPlanRead);
            this.tabPagePlan.Controls.Add(this.label6);
            this.tabPagePlan.Controls.Add(this.cbCondition_Plan);
            this.tabPagePlan.Controls.Add(this.label5);
            this.tabPagePlan.Controls.Add(this.cbToolName_Plan);
            this.tabPagePlan.Controls.Add(this.label4);
            this.tabPagePlan.Controls.Add(this.cbToolType_Plan);
            this.tabPagePlan.Controls.Add(this.textTestPlanName);
            this.tabPagePlan.Controls.Add(this.cbSelTestPlan);
            this.tabPagePlan.Location = new System.Drawing.Point(4, 25);
            this.tabPagePlan.Name = "tabPagePlan";
            this.tabPagePlan.Padding = new System.Windows.Forms.Padding(3);
            this.tabPagePlan.Size = new System.Drawing.Size(510, 242);
            this.tabPagePlan.TabIndex = 1;
            this.tabPagePlan.Text = "測試計畫";
            this.tabPagePlan.UseVisualStyleBackColor = true;
            // 
            // labTestPlan
            // 
            this.labTestPlan.Font = new System.Drawing.Font("Microsoft JhengHei", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.labTestPlan.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.labTestPlan.Location = new System.Drawing.Point(104, 153);
            this.labTestPlan.Name = "labTestPlan";
            this.labTestPlan.Size = new System.Drawing.Size(133, 37);
            this.labTestPlan.TabIndex = 17;
            this.labTestPlan.Text = "尚未載入";
            this.labTestPlan.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btCancel_Plan
            // 
            this.btCancel_Plan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btCancel_Plan.Location = new System.Drawing.Point(431, 210);
            this.btCancel_Plan.Name = "btCancel_Plan";
            this.btCancel_Plan.Size = new System.Drawing.Size(71, 29);
            this.btCancel_Plan.TabIndex = 15;
            this.btCancel_Plan.Text = "取消";
            this.btCancel_Plan.UseVisualStyleBackColor = true;
            this.btCancel_Plan.Click += new System.EventHandler(this.btCancel_Plan_Click);
            // 
            // btOK_Plan
            // 
            this.btOK_Plan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btOK_Plan.Location = new System.Drawing.Point(354, 210);
            this.btOK_Plan.Name = "btOK_Plan";
            this.btOK_Plan.Size = new System.Drawing.Size(71, 29);
            this.btOK_Plan.TabIndex = 14;
            this.btOK_Plan.Text = "確定";
            this.btOK_Plan.UseVisualStyleBackColor = true;
            this.btOK_Plan.Click += new System.EventHandler(this.btOK_Plan_Click);
            // 
            // dataGridViewTestPlan
            // 
            this.dataGridViewTestPlan.AllowUserToAddRows = false;
            this.dataGridViewTestPlan.AllowUserToDeleteRows = false;
            this.dataGridViewTestPlan.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewTestPlan.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewTestPlan.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridViewTestPlan.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dataGridViewTestPlan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewTestPlan.Location = new System.Drawing.Point(260, 85);
            this.dataGridViewTestPlan.Name = "dataGridViewTestPlan";
            this.dataGridViewTestPlan.RowHeadersVisible = false;
            this.dataGridViewTestPlan.RowTemplate.Height = 24;
            this.dataGridViewTestPlan.Size = new System.Drawing.Size(242, 119);
            this.dataGridViewTestPlan.TabIndex = 13;
            // 
            // labTestPlanName
            // 
            this.labTestPlanName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labTestPlanName.AutoSize = true;
            this.labTestPlanName.Location = new System.Drawing.Point(301, 57);
            this.labTestPlanName.Name = "labTestPlanName";
            this.labTestPlanName.Size = new System.Drawing.Size(56, 16);
            this.labTestPlanName.TabIndex = 11;
            this.labTestPlanName.Text = "計畫名稱";
            // 
            // labToolInfo
            // 
            this.labToolInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labToolInfo.AutoSize = true;
            this.labToolInfo.Location = new System.Drawing.Point(325, 21);
            this.labToolInfo.Name = "labToolInfo";
            this.labToolInfo.Size = new System.Drawing.Size(32, 16);
            this.labToolInfo.TabIndex = 10;
            this.labToolInfo.Text = "目的";
            // 
            // cbTestPlanEdit
            // 
            this.cbTestPlanEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbTestPlanEdit.FormattingEnabled = true;
            this.cbTestPlanEdit.Items.AddRange(new object[] {
            "新增計畫",
            "編輯計畫",
            "刪除計畫"});
            this.cbTestPlanEdit.Location = new System.Drawing.Point(363, 15);
            this.cbTestPlanEdit.Name = "cbTestPlanEdit";
            this.cbTestPlanEdit.Size = new System.Drawing.Size(121, 24);
            this.cbTestPlanEdit.TabIndex = 9;
            // 
            // btPlanRead
            // 
            this.btPlanRead.Location = new System.Drawing.Point(23, 153);
            this.btPlanRead.Name = "btPlanRead";
            this.btPlanRead.Size = new System.Drawing.Size(75, 37);
            this.btPlanRead.TabIndex = 8;
            this.btPlanRead.Text = "載入條件";
            this.btPlanRead.UseVisualStyleBackColor = true;
            this.btPlanRead.Click += new System.EventHandler(this.btPlanRead_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(20, 104);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 16);
            this.label6.TabIndex = 7;
            this.label6.Text = "參數類型";
            // 
            // cbCondition_Plan
            // 
            this.cbCondition_Plan.FormattingEnabled = true;
            this.cbCondition_Plan.Location = new System.Drawing.Point(82, 100);
            this.cbCondition_Plan.Name = "cbCondition_Plan";
            this.cbCondition_Plan.Size = new System.Drawing.Size(121, 24);
            this.cbCondition_Plan.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(20, 61);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 16);
            this.label5.TabIndex = 5;
            this.label5.Text = "機台名稱";
            // 
            // cbToolName_Plan
            // 
            this.cbToolName_Plan.FormattingEnabled = true;
            this.cbToolName_Plan.Location = new System.Drawing.Point(82, 59);
            this.cbToolName_Plan.Name = "cbToolName_Plan";
            this.cbToolName_Plan.Size = new System.Drawing.Size(121, 24);
            this.cbToolName_Plan.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 16);
            this.label4.TabIndex = 3;
            this.label4.Text = "機台類型";
            // 
            // cbToolType_Plan
            // 
            this.cbToolType_Plan.FormattingEnabled = true;
            this.cbToolType_Plan.Items.AddRange(new object[] {
            "混鍊機台",
            "射出機台"});
            this.cbToolType_Plan.Location = new System.Drawing.Point(82, 18);
            this.cbToolType_Plan.Name = "cbToolType_Plan";
            this.cbToolType_Plan.Size = new System.Drawing.Size(121, 24);
            this.cbToolType_Plan.TabIndex = 2;
            this.cbToolType_Plan.SelectedIndexChanged += new System.EventHandler(this.cbToolType_SelectedIndexChanged);
            // 
            // textTestPlanName
            // 
            this.textTestPlanName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textTestPlanName.Location = new System.Drawing.Point(363, 53);
            this.textTestPlanName.Name = "textTestPlanName";
            this.textTestPlanName.Size = new System.Drawing.Size(121, 23);
            this.textTestPlanName.TabIndex = 18;
            this.textTestPlanName.TextChanged += new System.EventHandler(this.textTestPlanName_TextChanged);
            // 
            // cbSelTestPlan
            // 
            this.cbSelTestPlan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbSelTestPlan.FormattingEnabled = true;
            this.cbSelTestPlan.Location = new System.Drawing.Point(363, 53);
            this.cbSelTestPlan.Name = "cbSelTestPlan";
            this.cbSelTestPlan.Size = new System.Drawing.Size(121, 24);
            this.cbSelTestPlan.TabIndex = 16;
            // 
            // tabPageItems
            // 
            this.tabPageItems.Controls.Add(this.label10);
            this.tabPageItems.Controls.Add(this.cbPurpose_Item);
            this.tabPageItems.Controls.Add(this.btCancel_Item);
            this.tabPageItems.Controls.Add(this.btOK_item);
            this.tabPageItems.Controls.Add(this.btBrowse_Items);
            this.tabPageItems.Controls.Add(this.textUploadPath_Item);
            this.tabPageItems.Controls.Add(this.labStep2_Item);
            this.tabPageItems.Controls.Add(this.btDownloadSampe_Item);
            this.tabPageItems.Controls.Add(this.labStep1_Item);
            this.tabPageItems.Controls.Add(this.rbtTItem);
            this.tabPageItems.Controls.Add(this.rbtRItem);
            this.tabPageItems.Controls.Add(this.dataGridView_Item);
            this.tabPageItems.Location = new System.Drawing.Point(4, 25);
            this.tabPageItems.Name = "tabPageItems";
            this.tabPageItems.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageItems.Size = new System.Drawing.Size(510, 242);
            this.tabPageItems.TabIndex = 2;
            this.tabPageItems.Text = "物性/配方";
            this.tabPageItems.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(8, 59);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(32, 16);
            this.label10.TabIndex = 15;
            this.label10.Text = "目的";
            // 
            // cbPurpose_Item
            // 
            this.cbPurpose_Item.FormattingEnabled = true;
            this.cbPurpose_Item.Location = new System.Drawing.Point(57, 55);
            this.cbPurpose_Item.Name = "cbPurpose_Item";
            this.cbPurpose_Item.Size = new System.Drawing.Size(121, 24);
            this.cbPurpose_Item.TabIndex = 14;
            this.cbPurpose_Item.SelectedIndexChanged += new System.EventHandler(this.cbPurpose_Item_SelectedIndexChanged);
            // 
            // btCancel_Item
            // 
            this.btCancel_Item.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btCancel_Item.Location = new System.Drawing.Point(431, 210);
            this.btCancel_Item.Name = "btCancel_Item";
            this.btCancel_Item.Size = new System.Drawing.Size(71, 29);
            this.btCancel_Item.TabIndex = 13;
            this.btCancel_Item.Text = "取消";
            this.btCancel_Item.UseVisualStyleBackColor = true;
            this.btCancel_Item.Click += new System.EventHandler(this.btCancel_Item_Click);
            // 
            // btOK_item
            // 
            this.btOK_item.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btOK_item.Location = new System.Drawing.Point(354, 210);
            this.btOK_item.Name = "btOK_item";
            this.btOK_item.Size = new System.Drawing.Size(71, 29);
            this.btOK_item.TabIndex = 12;
            this.btOK_item.Text = "確定";
            this.btOK_item.UseVisualStyleBackColor = true;
            this.btOK_item.Click += new System.EventHandler(this.btOK_item_Click);
            // 
            // btBrowse_Items
            // 
            this.btBrowse_Items.Location = new System.Drawing.Point(371, 140);
            this.btBrowse_Items.Name = "btBrowse_Items";
            this.btBrowse_Items.Size = new System.Drawing.Size(86, 23);
            this.btBrowse_Items.TabIndex = 11;
            this.btBrowse_Items.Text = "瀏覽(*CSV)";
            this.btBrowse_Items.UseVisualStyleBackColor = true;
            this.btBrowse_Items.Click += new System.EventHandler(this.btBrowse_Item_Click);
            // 
            // textUploadPath_Item
            // 
            this.textUploadPath_Item.Location = new System.Drawing.Point(57, 140);
            this.textUploadPath_Item.Name = "textUploadPath_Item";
            this.textUploadPath_Item.Size = new System.Drawing.Size(308, 23);
            this.textUploadPath_Item.TabIndex = 10;
            // 
            // labStep2_Item
            // 
            this.labStep2_Item.AutoSize = true;
            this.labStep2_Item.Location = new System.Drawing.Point(8, 143);
            this.labStep2_Item.Name = "labStep2_Item";
            this.labStep2_Item.Size = new System.Drawing.Size(41, 16);
            this.labStep2_Item.TabIndex = 9;
            this.labStep2_Item.Text = "Step2";
            // 
            // btDownloadSampe_Item
            // 
            this.btDownloadSampe_Item.Location = new System.Drawing.Point(57, 95);
            this.btDownloadSampe_Item.Name = "btDownloadSampe_Item";
            this.btDownloadSampe_Item.Size = new System.Drawing.Size(75, 23);
            this.btDownloadSampe_Item.TabIndex = 8;
            this.btDownloadSampe_Item.Text = "下載範本";
            this.btDownloadSampe_Item.UseVisualStyleBackColor = true;
            this.btDownloadSampe_Item.Click += new System.EventHandler(this.btDownloadSampe_Item_Click);
            // 
            // labStep1_Item
            // 
            this.labStep1_Item.AutoSize = true;
            this.labStep1_Item.Location = new System.Drawing.Point(8, 98);
            this.labStep1_Item.Name = "labStep1_Item";
            this.labStep1_Item.Size = new System.Drawing.Size(41, 16);
            this.labStep1_Item.TabIndex = 7;
            this.labStep1_Item.Text = "Step1";
            // 
            // rbtTItem
            // 
            this.rbtTItem.AutoSize = true;
            this.rbtTItem.Location = new System.Drawing.Point(67, 18);
            this.rbtTItem.Name = "rbtTItem";
            this.rbtTItem.Size = new System.Drawing.Size(50, 20);
            this.rbtTItem.TabIndex = 1;
            this.rbtTItem.Text = "物性";
            this.rbtTItem.UseVisualStyleBackColor = true;
            this.rbtTItem.CheckedChanged += new System.EventHandler(this.rbtTItem_CheckedChanged);
            // 
            // rbtRItem
            // 
            this.rbtRItem.AutoSize = true;
            this.rbtRItem.Checked = true;
            this.rbtRItem.Location = new System.Drawing.Point(11, 18);
            this.rbtRItem.Name = "rbtRItem";
            this.rbtRItem.Size = new System.Drawing.Size(50, 20);
            this.rbtRItem.TabIndex = 0;
            this.rbtRItem.TabStop = true;
            this.rbtRItem.Text = "配方";
            this.rbtRItem.UseVisualStyleBackColor = true;
            this.rbtRItem.CheckedChanged += new System.EventHandler(this.rbtRItem_CheckedChanged);
            // 
            // dataGridView_Item
            // 
            this.dataGridView_Item.AllowUserToAddRows = false;
            this.dataGridView_Item.AllowUserToDeleteRows = false;
            this.dataGridView_Item.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView_Item.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView_Item.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridView_Item.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_Item.Location = new System.Drawing.Point(226, 18);
            this.dataGridView_Item.Name = "dataGridView_Item";
            this.dataGridView_Item.RowHeadersVisible = false;
            this.dataGridView_Item.RowTemplate.Height = 24;
            this.dataGridView_Item.Size = new System.Drawing.Size(266, 186);
            this.dataGridView_Item.TabIndex = 16;
            // 
            // tabPageExp
            // 
            this.tabPageExp.Controls.Add(this.textUploadPath_Result);
            this.tabPageExp.Controls.Add(this.lvSelectedSeqID);
            this.tabPageExp.Controls.Add(this.btDownloadSample_Result);
            this.tabPageExp.Controls.Add(this.labStep2_Result);
            this.tabPageExp.Controls.Add(this.btSetSeqId_Result);
            this.tabPageExp.Controls.Add(this.labStep3_Result);
            this.tabPageExp.Controls.Add(this.labStep1_Result);
            this.tabPageExp.Controls.Add(this.btCancel_Result);
            this.tabPageExp.Controls.Add(this.btOK_Result);
            this.tabPageExp.Controls.Add(this.btBrowse_Result);
            this.tabPageExp.Location = new System.Drawing.Point(4, 25);
            this.tabPageExp.Name = "tabPageExp";
            this.tabPageExp.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageExp.Size = new System.Drawing.Size(510, 242);
            this.tabPageExp.TabIndex = 3;
            this.tabPageExp.Text = "實驗上傳";
            this.tabPageExp.UseVisualStyleBackColor = true;
            // 
            // textUploadPath_Result
            // 
            this.textUploadPath_Result.Location = new System.Drawing.Point(6, 167);
            this.textUploadPath_Result.Name = "textUploadPath_Result";
            this.textUploadPath_Result.Size = new System.Drawing.Size(308, 23);
            this.textUploadPath_Result.TabIndex = 12;
            // 
            // lvSelectedSeqID
            // 
            this.lvSelectedSeqID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvSelectedSeqID.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lvSelectedSeqID.Font = new System.Drawing.Font("Microsoft JhengHei", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lvSelectedSeqID.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.lvSelectedSeqID.GridLines = true;
            this.lvSelectedSeqID.Location = new System.Drawing.Point(53, 46);
            this.lvSelectedSeqID.Name = "lvSelectedSeqID";
            this.lvSelectedSeqID.ShowItemToolTips = true;
            this.lvSelectedSeqID.Size = new System.Drawing.Size(451, 42);
            this.lvSelectedSeqID.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lvSelectedSeqID.TabIndex = 22;
            this.lvSelectedSeqID.UseCompatibleStateImageBehavior = false;
            this.lvSelectedSeqID.View = System.Windows.Forms.View.List;
            // 
            // btDownloadSample_Result
            // 
            this.btDownloadSample_Result.Location = new System.Drawing.Point(53, 102);
            this.btDownloadSample_Result.Name = "btDownloadSample_Result";
            this.btDownloadSample_Result.Size = new System.Drawing.Size(145, 23);
            this.btDownloadSample_Result.TabIndex = 21;
            this.btDownloadSample_Result.Text = "下載上傳範本";
            this.btDownloadSample_Result.UseVisualStyleBackColor = true;
            this.btDownloadSample_Result.Click += new System.EventHandler(this.btDownloadSample_Result_Click);
            // 
            // labStep2_Result
            // 
            this.labStep2_Result.AutoSize = true;
            this.labStep2_Result.Location = new System.Drawing.Point(6, 105);
            this.labStep2_Result.Name = "labStep2_Result";
            this.labStep2_Result.Size = new System.Drawing.Size(41, 16);
            this.labStep2_Result.TabIndex = 19;
            this.labStep2_Result.Text = "Step2";
            // 
            // btSetSeqId_Result
            // 
            this.btSetSeqId_Result.Location = new System.Drawing.Point(53, 17);
            this.btSetSeqId_Result.Name = "btSetSeqId_Result";
            this.btSetSeqId_Result.Size = new System.Drawing.Size(145, 23);
            this.btSetSeqId_Result.TabIndex = 18;
            this.btSetSeqId_Result.Text = "設定實驗序號";
            this.btSetSeqId_Result.UseVisualStyleBackColor = true;
            this.btSetSeqId_Result.Click += new System.EventHandler(this.btSetSeqId_Result_Click);
            // 
            // labStep3_Result
            // 
            this.labStep3_Result.AutoSize = true;
            this.labStep3_Result.Location = new System.Drawing.Point(6, 148);
            this.labStep3_Result.Name = "labStep3_Result";
            this.labStep3_Result.Size = new System.Drawing.Size(95, 16);
            this.labStep3_Result.TabIndex = 17;
            this.labStep3_Result.Text = "Step3: 上傳資料";
            // 
            // labStep1_Result
            // 
            this.labStep1_Result.AutoSize = true;
            this.labStep1_Result.Location = new System.Drawing.Point(6, 20);
            this.labStep1_Result.Name = "labStep1_Result";
            this.labStep1_Result.Size = new System.Drawing.Size(41, 16);
            this.labStep1_Result.TabIndex = 16;
            this.labStep1_Result.Text = "Step1";
            // 
            // btCancel_Result
            // 
            this.btCancel_Result.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btCancel_Result.Location = new System.Drawing.Point(431, 210);
            this.btCancel_Result.Name = "btCancel_Result";
            this.btCancel_Result.Size = new System.Drawing.Size(71, 29);
            this.btCancel_Result.TabIndex = 15;
            this.btCancel_Result.Text = "取消";
            this.btCancel_Result.UseVisualStyleBackColor = true;
            this.btCancel_Result.Click += new System.EventHandler(this.btCancel_Result_Click);
            // 
            // btOK_Result
            // 
            this.btOK_Result.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btOK_Result.Location = new System.Drawing.Point(354, 210);
            this.btOK_Result.Name = "btOK_Result";
            this.btOK_Result.Size = new System.Drawing.Size(71, 29);
            this.btOK_Result.TabIndex = 14;
            this.btOK_Result.Text = "上傳";
            this.btOK_Result.UseVisualStyleBackColor = true;
            this.btOK_Result.Click += new System.EventHandler(this.btOK_result_Click);
            // 
            // btBrowse_Result
            // 
            this.btBrowse_Result.Location = new System.Drawing.Point(320, 167);
            this.btBrowse_Result.Name = "btBrowse_Result";
            this.btBrowse_Result.Size = new System.Drawing.Size(135, 23);
            this.btBrowse_Result.TabIndex = 13;
            this.btBrowse_Result.Text = "瀏覽(*.XLS, *.XLSX)";
            this.btBrowse_Result.UseVisualStyleBackColor = true;
            this.btBrowse_Result.Click += new System.EventHandler(this.btBrowse_Result_Click);
            // 
            // tabPageCustomer
            // 
            this.tabPageCustomer.Controls.Add(this.textUploadPath_Customer);
            this.tabPageCustomer.Controls.Add(this.labStep1_Customer);
            this.tabPageCustomer.Controls.Add(this.btBrowse_Customer);
            this.tabPageCustomer.Controls.Add(this.btCancel_Customer);
            this.tabPageCustomer.Controls.Add(this.btOK_Customer);
            this.tabPageCustomer.Location = new System.Drawing.Point(4, 25);
            this.tabPageCustomer.Name = "tabPageCustomer";
            this.tabPageCustomer.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageCustomer.Size = new System.Drawing.Size(510, 242);
            this.tabPageCustomer.TabIndex = 4;
            this.tabPageCustomer.Text = "客戶資料管理";
            this.tabPageCustomer.UseVisualStyleBackColor = true;
            // 
            // textUploadPath_Customer
            // 
            this.textUploadPath_Customer.Location = new System.Drawing.Point(6, 40);
            this.textUploadPath_Customer.Name = "textUploadPath_Customer";
            this.textUploadPath_Customer.Size = new System.Drawing.Size(308, 23);
            this.textUploadPath_Customer.TabIndex = 18;
            // 
            // labStep1_Customer
            // 
            this.labStep1_Customer.AutoSize = true;
            this.labStep1_Customer.Location = new System.Drawing.Point(6, 21);
            this.labStep1_Customer.Name = "labStep1_Customer";
            this.labStep1_Customer.Size = new System.Drawing.Size(80, 16);
            this.labStep1_Customer.TabIndex = 20;
            this.labStep1_Customer.Text = "指定客戶檔案";
            // 
            // btBrowse_Customer
            // 
            this.btBrowse_Customer.Location = new System.Drawing.Point(320, 40);
            this.btBrowse_Customer.Name = "btBrowse_Customer";
            this.btBrowse_Customer.Size = new System.Drawing.Size(105, 23);
            this.btBrowse_Customer.TabIndex = 19;
            this.btBrowse_Customer.Text = "瀏覽(*.CSV)";
            this.btBrowse_Customer.UseVisualStyleBackColor = true;
            this.btBrowse_Customer.Click += new System.EventHandler(this.btBrowse_Customer_Click);
            // 
            // btCancel_Customer
            // 
            this.btCancel_Customer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btCancel_Customer.Location = new System.Drawing.Point(431, 210);
            this.btCancel_Customer.Name = "btCancel_Customer";
            this.btCancel_Customer.Size = new System.Drawing.Size(71, 29);
            this.btCancel_Customer.TabIndex = 17;
            this.btCancel_Customer.Text = "取消";
            this.btCancel_Customer.UseVisualStyleBackColor = true;
            this.btCancel_Customer.Click += new System.EventHandler(this.btCancel_Customer_Click);
            // 
            // btOK_Customer
            // 
            this.btOK_Customer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btOK_Customer.Location = new System.Drawing.Point(354, 210);
            this.btOK_Customer.Name = "btOK_Customer";
            this.btOK_Customer.Size = new System.Drawing.Size(71, 29);
            this.btOK_Customer.TabIndex = 16;
            this.btOK_Customer.Text = "上傳";
            this.btOK_Customer.UseVisualStyleBackColor = true;
            this.btOK_Customer.Click += new System.EventHandler(this.btOK_Customer_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog1";
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
            this.linkLab.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLab.AutoSize = true;
            this.linkLab.Location = new System.Drawing.Point(460, 9);
            this.linkLab.Name = "linkLab";
            this.linkLab.Size = new System.Drawing.Size(56, 16);
            this.linkLab.TabIndex = 1;
            this.linkLab.TabStop = true;
            this.linkLab.Text = "切換帳號";
            this.linkLab.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLab_LinkClicked);
            // 
            // backgroundWorker2
            // 
            this.backgroundWorker2.WorkerReportsProgress = true;
            this.backgroundWorker2.WorkerSupportsCancellation = true;
            this.backgroundWorker2.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker2_DoWork);
            this.backgroundWorker2.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            this.backgroundWorker2.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // FrmUpload
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(530, 314);
            this.Controls.Add(this.linkLab);
            this.Controls.Add(this.tabUpload);
            this.Font = new System.Drawing.Font("Microsoft JhengHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(546, 352);
            this.Name = "FrmUpload";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "資料管理...";
            this.Load += new System.EventHandler(this.FrmUpload_Load);
            this.tabUpload.ResumeLayout(false);
            this.tabPagTool.ResumeLayout(false);
            this.tabPagTool.PerformLayout();
            this.tabPagePlan.ResumeLayout(false);
            this.tabPagePlan.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTestPlan)).EndInit();
            this.tabPageItems.ResumeLayout(false);
            this.tabPageItems.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Item)).EndInit();
            this.tabPageExp.ResumeLayout(false);
            this.tabPageExp.PerformLayout();
            this.tabPageCustomer.ResumeLayout(false);
            this.tabPageCustomer.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabUpload;
        private System.Windows.Forms.TabPage tabPagTool;
        private System.Windows.Forms.Button btCancel_Tool;
        private System.Windows.Forms.Button btBrowse_Tool;
        private System.Windows.Forms.Button btOK_Tool;
        private System.Windows.Forms.TextBox textUploadPath_Tool;
        private System.Windows.Forms.Label labStep2_Tool;
        private System.Windows.Forms.Button btDownloadSample_Tool;
        private System.Windows.Forms.Label labStep1_Tool;
        private System.Windows.Forms.Label labPurpose_Tool;
        private System.Windows.Forms.ComboBox cbPurpose_Tool;
        private System.Windows.Forms.TabPage tabPagePlan;
        private System.Windows.Forms.DataGridView dataGridViewTestPlan;
        private System.Windows.Forms.Label labTestPlanName;
        private System.Windows.Forms.Label labToolInfo;
        private System.Windows.Forms.ComboBox cbTestPlanEdit;
        private System.Windows.Forms.Button btPlanRead;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbCondition_Plan;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbToolName_Plan;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbToolType_Plan;
        private System.Windows.Forms.TabPage tabPageItems;
        private System.Windows.Forms.TabPage tabPageExp;
        private System.Windows.Forms.Button btCancel_Plan;
        private System.Windows.Forms.Button btOK_Plan;
        private System.Windows.Forms.ComboBox cbSelTestPlan;
        private System.Windows.Forms.Button btCancel_Item;
        private System.Windows.Forms.Button btOK_item;
        private System.Windows.Forms.Button btBrowse_Items;
        private System.Windows.Forms.TextBox textUploadPath_Item;
        private System.Windows.Forms.Label labStep2_Item;
        private System.Windows.Forms.Button btDownloadSampe_Item;
        private System.Windows.Forms.Label labStep1_Item;
        private System.Windows.Forms.RadioButton rbtTItem;
        private System.Windows.Forms.RadioButton rbtRItem;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cbPurpose_Item;
        private System.Windows.Forms.Button btCancel_Result;
        private System.Windows.Forms.Button btOK_Result;
        private System.Windows.Forms.Button btBrowse_Result;
        private System.Windows.Forms.TextBox textUploadPath_Result;
        private System.Windows.Forms.Label labTestPlan;
        private System.Windows.Forms.TextBox textTestPlanName;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.DataGridView dataGridView_Item;
        private System.Windows.Forms.Label labToolName_Tool;
        private System.Windows.Forms.ComboBox cbToolName_Tool;
        private System.Windows.Forms.LinkLabel linkLab;
        private System.Windows.Forms.Label labStep3_Result;
        private System.Windows.Forms.Label labStep1_Result;
        private System.Windows.Forms.Button btSetSeqId_Result;
        private System.Windows.Forms.Label labStep2_Result;
        private System.Windows.Forms.Button btDownloadSample_Result;
        private System.Windows.Forms.ListView lvSelectedSeqID;
        private System.ComponentModel.BackgroundWorker backgroundWorker2;
        private System.Windows.Forms.TabPage tabPageCustomer;
        private System.Windows.Forms.Label labStep1_Customer;
        private System.Windows.Forms.Button btBrowse_Customer;
        private System.Windows.Forms.TextBox textUploadPath_Customer;
        private System.Windows.Forms.Button btCancel_Customer;
        private System.Windows.Forms.Button btOK_Customer;
    }
}