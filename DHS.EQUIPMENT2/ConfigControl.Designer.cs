namespace DHS.EQUIPMENT2
{
    partial class ConfigControl
    {
        /// <summary> 
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.radPanel4 = new Telerik.WinControls.UI.RadPanel();
            this.lblServerStatus = new System.Windows.Forms.Label();
            this.lblClientStatus = new System.Windows.Forms.Label();
            this.tbServerMsg = new System.Windows.Forms.TextBox();
            this.radpnl_processinfo = new Telerik.WinControls.UI.RadPanel();
            this.tbControllerNo = new System.Windows.Forms.TextBox();
            this.radLabel11 = new Telerik.WinControls.UI.RadLabel();
            this.radbtnDelController = new Telerik.WinControls.UI.RadButton();
            this.tbContPort02 = new System.Windows.Forms.TextBox();
            this.radLabel9 = new Telerik.WinControls.UI.RadLabel();
            this.tbMaxChannel = new System.Windows.Forms.TextBox();
            this.radLabel8 = new Telerik.WinControls.UI.RadLabel();
            this.radbtnAddController = new Telerik.WinControls.UI.RadButton();
            this.tbContPort01 = new System.Windows.Forms.TextBox();
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            this.tbContIPaddress01 = new System.Windows.Forms.TextBox();
            this.radlblOffsetChannel = new Telerik.WinControls.UI.RadLabel();
            this.label12 = new System.Windows.Forms.Label();
            this.radPanel1 = new Telerik.WinControls.UI.RadPanel();
            this.dgvControllerList = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel4)).BeginInit();
            this.radPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radpnl_processinfo)).BeginInit();
            this.radpnl_processinfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radbtnDelController)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radbtnAddController)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radlblOffsetChannel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).BeginInit();
            this.radPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvControllerList)).BeginInit();
            this.SuspendLayout();
            // 
            // radPanel4
            // 
            this.radPanel4.Controls.Add(this.lblServerStatus);
            this.radPanel4.Controls.Add(this.lblClientStatus);
            this.radPanel4.Controls.Add(this.tbServerMsg);
            this.radPanel4.Location = new System.Drawing.Point(744, 26);
            this.radPanel4.Name = "radPanel4";
            this.radPanel4.Padding = new System.Windows.Forms.Padding(5);
            this.radPanel4.Size = new System.Drawing.Size(481, 174);
            this.radPanel4.TabIndex = 22;
            this.radPanel4.ThemeName = "ControlDefault";
            ((Telerik.WinControls.UI.RadPanelElement)(this.radPanel4.GetChildAt(0))).Text = "";
            ((Telerik.WinControls.UI.RadPanelElement)(this.radPanel4.GetChildAt(0))).BorderHighlightColor = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(14)))), ((int)(((byte)(248)))));
            ((Telerik.WinControls.UI.RadPanelElement)(this.radPanel4.GetChildAt(0))).Padding = new System.Windows.Forms.Padding(5);
            // 
            // lblServerStatus
            // 
            this.lblServerStatus.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblServerStatus.ForeColor = System.Drawing.Color.SteelBlue;
            this.lblServerStatus.Location = new System.Drawing.Point(8, 5);
            this.lblServerStatus.Name = "lblServerStatus";
            this.lblServerStatus.Size = new System.Drawing.Size(180, 13);
            this.lblServerStatus.TabIndex = 50;
            this.lblServerStatus.Text = "SERVER STATUS";
            // 
            // lblClientStatus
            // 
            this.lblClientStatus.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblClientStatus.ForeColor = System.Drawing.Color.SteelBlue;
            this.lblClientStatus.Location = new System.Drawing.Point(205, 4);
            this.lblClientStatus.Name = "lblClientStatus";
            this.lblClientStatus.Size = new System.Drawing.Size(260, 16);
            this.lblClientStatus.TabIndex = 83;
            this.lblClientStatus.Text = "CLIENT STATUS";
            // 
            // tbServerMsg
            // 
            this.tbServerMsg.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tbServerMsg.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbServerMsg.Location = new System.Drawing.Point(5, 45);
            this.tbServerMsg.Multiline = true;
            this.tbServerMsg.Name = "tbServerMsg";
            this.tbServerMsg.Size = new System.Drawing.Size(471, 124);
            this.tbServerMsg.TabIndex = 82;
            // 
            // radpnl_processinfo
            // 
            this.radpnl_processinfo.Controls.Add(this.tbControllerNo);
            this.radpnl_processinfo.Controls.Add(this.radLabel11);
            this.radpnl_processinfo.Controls.Add(this.radbtnDelController);
            this.radpnl_processinfo.Controls.Add(this.tbContPort02);
            this.radpnl_processinfo.Controls.Add(this.radLabel9);
            this.radpnl_processinfo.Controls.Add(this.tbMaxChannel);
            this.radpnl_processinfo.Controls.Add(this.radLabel8);
            this.radpnl_processinfo.Controls.Add(this.radbtnAddController);
            this.radpnl_processinfo.Controls.Add(this.tbContPort01);
            this.radpnl_processinfo.Controls.Add(this.radLabel1);
            this.radpnl_processinfo.Controls.Add(this.tbContIPaddress01);
            this.radpnl_processinfo.Controls.Add(this.radlblOffsetChannel);
            this.radpnl_processinfo.Controls.Add(this.label12);
            this.radpnl_processinfo.Location = new System.Drawing.Point(20, 26);
            this.radpnl_processinfo.Name = "radpnl_processinfo";
            this.radpnl_processinfo.Padding = new System.Windows.Forms.Padding(5);
            this.radpnl_processinfo.Size = new System.Drawing.Size(705, 174);
            this.radpnl_processinfo.TabIndex = 21;
            this.radpnl_processinfo.ThemeName = "ControlDefault";
            ((Telerik.WinControls.UI.RadPanelElement)(this.radpnl_processinfo.GetChildAt(0))).Text = "";
            ((Telerik.WinControls.UI.RadPanelElement)(this.radpnl_processinfo.GetChildAt(0))).BorderHighlightColor = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(14)))), ((int)(((byte)(248)))));
            ((Telerik.WinControls.UI.RadPanelElement)(this.radpnl_processinfo.GetChildAt(0))).Padding = new System.Windows.Forms.Padding(5);
            // 
            // tbControllerNo
            // 
            this.tbControllerNo.Font = new System.Drawing.Font("Times New Roman", 16F);
            this.tbControllerNo.Location = new System.Drawing.Point(150, 45);
            this.tbControllerNo.Name = "tbControllerNo";
            this.tbControllerNo.Size = new System.Drawing.Size(177, 32);
            this.tbControllerNo.TabIndex = 81;
            this.tbControllerNo.Text = "1";
            this.tbControllerNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // radLabel11
            // 
            this.radLabel11.AutoSize = false;
            this.radLabel11.BackColor = System.Drawing.Color.LightGray;
            this.radLabel11.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.radLabel11.Location = new System.Drawing.Point(20, 45);
            this.radLabel11.Name = "radLabel11";
            this.radLabel11.Size = new System.Drawing.Size(122, 32);
            this.radLabel11.TabIndex = 80;
            this.radLabel11.Text = "NO.";
            this.radLabel11.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // radbtnDelController
            // 
            this.radbtnDelController.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
            this.radbtnDelController.ForeColor = System.Drawing.Color.Black;
            this.radbtnDelController.Location = new System.Drawing.Point(485, 19);
            this.radbtnDelController.Margin = new System.Windows.Forms.Padding(4);
            this.radbtnDelController.Name = "radbtnDelController";
            this.radbtnDelController.Size = new System.Drawing.Size(96, 40);
            this.radbtnDelController.TabIndex = 79;
            this.radbtnDelController.Text = "DELETE";
            this.radbtnDelController.ThemeName = "ControlDefault";
            this.radbtnDelController.Click += new System.EventHandler(this.radbtnDelController_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.radbtnDelController.GetChildAt(0))).Text = "DELETE";
            ((Telerik.WinControls.UI.RadButtonElement)(this.radbtnDelController.GetChildAt(0))).Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.radbtnDelController.GetChildAt(0).GetChildAt(0))).BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(241)))), ((int)(((byte)(252)))));
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.radbtnDelController.GetChildAt(0).GetChildAt(0))).BackColor3 = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(226)))), ((int)(((byte)(244)))));
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.radbtnDelController.GetChildAt(0).GetChildAt(0))).BackColor4 = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(226)))), ((int)(((byte)(244)))));
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.radbtnDelController.GetChildAt(0).GetChildAt(0))).BackColor = System.Drawing.Color.White;
            // 
            // tbContPort02
            // 
            this.tbContPort02.Font = new System.Drawing.Font("Times New Roman", 16F);
            this.tbContPort02.Location = new System.Drawing.Point(500, 125);
            this.tbContPort02.Name = "tbContPort02";
            this.tbContPort02.Size = new System.Drawing.Size(177, 32);
            this.tbContPort02.TabIndex = 78;
            this.tbContPort02.Text = "50001";
            this.tbContPort02.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // radLabel9
            // 
            this.radLabel9.AutoSize = false;
            this.radLabel9.BackColor = System.Drawing.Color.LightGray;
            this.radLabel9.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.radLabel9.Location = new System.Drawing.Point(370, 125);
            this.radLabel9.Name = "radLabel9";
            this.radLabel9.Size = new System.Drawing.Size(122, 32);
            this.radLabel9.TabIndex = 77;
            this.radLabel9.Text = "PORT 2";
            this.radLabel9.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tbMaxChannel
            // 
            this.tbMaxChannel.Font = new System.Drawing.Font("Times New Roman", 16F);
            this.tbMaxChannel.Location = new System.Drawing.Point(150, 125);
            this.tbMaxChannel.Name = "tbMaxChannel";
            this.tbMaxChannel.Size = new System.Drawing.Size(177, 32);
            this.tbMaxChannel.TabIndex = 76;
            this.tbMaxChannel.Text = "256";
            this.tbMaxChannel.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // radLabel8
            // 
            this.radLabel8.AutoSize = false;
            this.radLabel8.BackColor = System.Drawing.Color.LightGray;
            this.radLabel8.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.radLabel8.Location = new System.Drawing.Point(20, 125);
            this.radLabel8.Name = "radLabel8";
            this.radLabel8.Size = new System.Drawing.Size(122, 32);
            this.radLabel8.TabIndex = 75;
            this.radLabel8.Text = "MAX CHANNEL";
            this.radLabel8.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // radbtnAddController
            // 
            this.radbtnAddController.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
            this.radbtnAddController.ForeColor = System.Drawing.Color.Black;
            this.radbtnAddController.Location = new System.Drawing.Point(343, 19);
            this.radbtnAddController.Margin = new System.Windows.Forms.Padding(4);
            this.radbtnAddController.Name = "radbtnAddController";
            this.radbtnAddController.Size = new System.Drawing.Size(134, 40);
            this.radbtnAddController.TabIndex = 74;
            this.radbtnAddController.Text = "ADD/ UPDATE";
            this.radbtnAddController.ThemeName = "ControlDefault";
            this.radbtnAddController.Click += new System.EventHandler(this.radbtnAddController_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.radbtnAddController.GetChildAt(0))).Text = "ADD/ UPDATE";
            ((Telerik.WinControls.UI.RadButtonElement)(this.radbtnAddController.GetChildAt(0))).Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.radbtnAddController.GetChildAt(0).GetChildAt(0))).BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(241)))), ((int)(((byte)(252)))));
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.radbtnAddController.GetChildAt(0).GetChildAt(0))).BackColor3 = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(226)))), ((int)(((byte)(244)))));
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.radbtnAddController.GetChildAt(0).GetChildAt(0))).BackColor4 = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(226)))), ((int)(((byte)(244)))));
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.radbtnAddController.GetChildAt(0).GetChildAt(0))).BackColor = System.Drawing.Color.White;
            // 
            // tbContPort01
            // 
            this.tbContPort01.Font = new System.Drawing.Font("Times New Roman", 16F);
            this.tbContPort01.Location = new System.Drawing.Point(500, 85);
            this.tbContPort01.Name = "tbContPort01";
            this.tbContPort01.Size = new System.Drawing.Size(177, 32);
            this.tbContPort01.TabIndex = 72;
            this.tbContPort01.Text = "50000";
            this.tbContPort01.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // radLabel1
            // 
            this.radLabel1.AutoSize = false;
            this.radLabel1.BackColor = System.Drawing.Color.LightGray;
            this.radLabel1.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.radLabel1.Location = new System.Drawing.Point(370, 85);
            this.radLabel1.Name = "radLabel1";
            this.radLabel1.Size = new System.Drawing.Size(122, 32);
            this.radLabel1.TabIndex = 71;
            this.radLabel1.Text = "PORT 1";
            this.radLabel1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tbContIPaddress01
            // 
            this.tbContIPaddress01.Font = new System.Drawing.Font("Times New Roman", 16F);
            this.tbContIPaddress01.Location = new System.Drawing.Point(150, 85);
            this.tbContIPaddress01.Name = "tbContIPaddress01";
            this.tbContIPaddress01.Size = new System.Drawing.Size(177, 32);
            this.tbContIPaddress01.TabIndex = 70;
            this.tbContIPaddress01.Text = "192.168.100.101";
            this.tbContIPaddress01.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // radlblOffsetChannel
            // 
            this.radlblOffsetChannel.AutoSize = false;
            this.radlblOffsetChannel.BackColor = System.Drawing.Color.LightGray;
            this.radlblOffsetChannel.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.radlblOffsetChannel.Location = new System.Drawing.Point(20, 85);
            this.radlblOffsetChannel.Name = "radlblOffsetChannel";
            this.radlblOffsetChannel.Size = new System.Drawing.Size(122, 32);
            this.radlblOffsetChannel.TabIndex = 69;
            this.radlblOffsetChannel.Text = "IP ADDRESS";
            this.radlblOffsetChannel.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.SteelBlue;
            this.label12.Location = new System.Drawing.Point(5, 5);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(132, 16);
            this.label12.TabIndex = 50;
            this.label12.Text = "Controller Setting";
            // 
            // radPanel1
            // 
            this.radPanel1.Controls.Add(this.dgvControllerList);
            this.radPanel1.Controls.Add(this.label1);
            this.radPanel1.Location = new System.Drawing.Point(20, 219);
            this.radPanel1.Name = "radPanel1";
            this.radPanel1.Padding = new System.Windows.Forms.Padding(5);
            this.radPanel1.Size = new System.Drawing.Size(705, 588);
            this.radPanel1.TabIndex = 23;
            this.radPanel1.ThemeName = "ControlDefault";
            ((Telerik.WinControls.UI.RadPanelElement)(this.radPanel1.GetChildAt(0))).Text = "";
            ((Telerik.WinControls.UI.RadPanelElement)(this.radPanel1.GetChildAt(0))).BorderHighlightColor = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(14)))), ((int)(((byte)(248)))));
            ((Telerik.WinControls.UI.RadPanelElement)(this.radPanel1.GetChildAt(0))).Padding = new System.Windows.Forms.Padding(5);
            // 
            // dgvControllerList
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvControllerList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvControllerList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvControllerList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column5,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column6});
            this.dgvControllerList.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgvControllerList.Location = new System.Drawing.Point(5, 39);
            this.dgvControllerList.Name = "dgvControllerList";
            this.dgvControllerList.RowTemplate.Height = 23;
            this.dgvControllerList.Size = new System.Drawing.Size(695, 544);
            this.dgvControllerList.TabIndex = 51;
            this.dgvControllerList.SelectionChanged += new System.EventHandler(this.dgvControllerList_SelectionChanged);
            // 
            // Column1
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            this.Column1.DefaultCellStyle = dataGridViewCellStyle2;
            this.Column1.HeaderText = "Stage";
            this.Column1.Name = "Column1";
            this.Column1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "MaxChannel";
            this.Column5.Name = "Column5";
            this.Column5.Width = 90;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "IP";
            this.Column2.Name = "Column2";
            this.Column2.Width = 160;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Port";
            this.Column3.Name = "Column3";
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Port2";
            this.Column4.Name = "Column4";
            // 
            // Column6
            // 
            this.Column6.HeaderText = "Status";
            this.Column6.Name = "Column6";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.SteelBlue;
            this.label1.Location = new System.Drawing.Point(5, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 16);
            this.label1.TabIndex = 50;
            this.label1.Text = "Controller List";
            // 
            // ConfigControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.radPanel1);
            this.Controls.Add(this.radPanel4);
            this.Controls.Add(this.radpnl_processinfo);
            this.Name = "ConfigControl";
            this.Size = new System.Drawing.Size(1900, 830);
            ((System.ComponentModel.ISupportInitialize)(this.radPanel4)).EndInit();
            this.radPanel4.ResumeLayout(false);
            this.radPanel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radpnl_processinfo)).EndInit();
            this.radpnl_processinfo.ResumeLayout(false);
            this.radpnl_processinfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radbtnDelController)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radbtnAddController)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radlblOffsetChannel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).EndInit();
            this.radPanel1.ResumeLayout(false);
            this.radPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvControllerList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadPanel radPanel4;
        private System.Windows.Forms.Label lblServerStatus;
        private System.Windows.Forms.Label lblClientStatus;
        private System.Windows.Forms.TextBox tbServerMsg;
        private Telerik.WinControls.UI.RadPanel radpnl_processinfo;
        private System.Windows.Forms.TextBox tbControllerNo;
        private Telerik.WinControls.UI.RadLabel radLabel11;
        private Telerik.WinControls.UI.RadButton radbtnDelController;
        private System.Windows.Forms.TextBox tbContPort02;
        private Telerik.WinControls.UI.RadLabel radLabel9;
        private System.Windows.Forms.TextBox tbMaxChannel;
        private Telerik.WinControls.UI.RadLabel radLabel8;
        private Telerik.WinControls.UI.RadButton radbtnAddController;
        private System.Windows.Forms.TextBox tbContPort01;
        private Telerik.WinControls.UI.RadLabel radLabel1;
        private System.Windows.Forms.TextBox tbContIPaddress01;
        private Telerik.WinControls.UI.RadLabel radlblOffsetChannel;
        private System.Windows.Forms.Label label12;
        private Telerik.WinControls.UI.RadPanel radPanel1;
        private System.Windows.Forms.DataGridView dgvControllerList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
    }
}
