
namespace DHS.EQUIPMENT2
{
    partial class CalibrationForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlLeft = new System.Windows.Forms.Panel();
            this.gbStageInfo = new System.Windows.Forms.GroupBox();
            this.btnStartCalibration = new System.Windows.Forms.Button();
            this.btnStopCalibration = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.cbStageName = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbStageRow = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbStageCol = new System.Windows.Forms.ComboBox();
            this.pnlBody = new System.Windows.Forms.Panel();
            this.radPanel1 = new Telerik.WinControls.UI.RadPanel();
            this.dgvCalStatusList = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlLeft.SuspendLayout();
            this.gbStageInfo.SuspendLayout();
            this.pnlBody.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).BeginInit();
            this.radPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCalStatusList)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlLeft
            // 
            this.pnlLeft.BackColor = System.Drawing.Color.White;
            this.pnlLeft.Controls.Add(this.gbStageInfo);
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlLeft.Location = new System.Drawing.Point(0, 0);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Padding = new System.Windows.Forms.Padding(5);
            this.pnlLeft.Size = new System.Drawing.Size(328, 791);
            this.pnlLeft.TabIndex = 56;
            // 
            // gbStageInfo
            // 
            this.gbStageInfo.Controls.Add(this.btnStartCalibration);
            this.gbStageInfo.Controls.Add(this.btnStopCalibration);
            this.gbStageInfo.Controls.Add(this.label5);
            this.gbStageInfo.Controls.Add(this.cbStageName);
            this.gbStageInfo.Controls.Add(this.label4);
            this.gbStageInfo.Controls.Add(this.cbStageRow);
            this.gbStageInfo.Controls.Add(this.label3);
            this.gbStageInfo.Controls.Add(this.cbStageCol);
            this.gbStageInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbStageInfo.Location = new System.Drawing.Point(5, 5);
            this.gbStageInfo.Name = "gbStageInfo";
            this.gbStageInfo.Size = new System.Drawing.Size(318, 781);
            this.gbStageInfo.TabIndex = 55;
            this.gbStageInfo.TabStop = false;
            this.gbStageInfo.Text = "STAGE 선택";
            // 
            // btnStartCalibration
            // 
            this.btnStartCalibration.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnStartCalibration.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnStartCalibration.ForeColor = System.Drawing.Color.White;
            this.btnStartCalibration.Location = new System.Drawing.Point(30, 190);
            this.btnStartCalibration.Name = "btnStartCalibration";
            this.btnStartCalibration.Size = new System.Drawing.Size(210, 50);
            this.btnStartCalibration.TabIndex = 59;
            this.btnStartCalibration.Text = "START CALIBRATION";
            this.btnStartCalibration.UseVisualStyleBackColor = false;
            this.btnStartCalibration.Click += new System.EventHandler(this.btnStartCalibration_Click);
            // 
            // btnStopCalibration
            // 
            this.btnStopCalibration.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnStopCalibration.Location = new System.Drawing.Point(30, 259);
            this.btnStopCalibration.Name = "btnStopCalibration";
            this.btnStopCalibration.Size = new System.Drawing.Size(210, 50);
            this.btnStopCalibration.TabIndex = 58;
            this.btnStopCalibration.Text = "STOP CALIBRATION";
            this.btnStopCalibration.UseVisualStyleBackColor = true;
            this.btnStopCalibration.Click += new System.EventHandler(this.btnStopCalibration_Click);
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.LightBlue;
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label5.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(30, 81);
            this.label5.Name = "label5";
            this.label5.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.label5.Size = new System.Drawing.Size(131, 31);
            this.label5.TabIndex = 29;
            this.label5.Text = "STAGE NO.";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cbStageName
            // 
            this.cbStageName.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbStageName.FormattingEnabled = true;
            this.cbStageName.Items.AddRange(new object[] {
            "STAGE 001",
            "STAGE 002",
            "STAGE 003",
            "STAGE 004",
            "STAGE 005",
            "STAGE 006"});
            this.cbStageName.Location = new System.Drawing.Point(162, 81);
            this.cbStageName.Name = "cbStageName";
            this.cbStageName.Size = new System.Drawing.Size(132, 31);
            this.cbStageName.TabIndex = 28;
            this.cbStageName.Text = "        -";
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.LightBlue;
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(163, 30);
            this.label4.Name = "label4";
            this.label4.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.label4.Size = new System.Drawing.Size(65, 31);
            this.label4.TabIndex = 27;
            this.label4.Text = "ROW";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cbStageRow
            // 
            this.cbStageRow.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbStageRow.FormattingEnabled = true;
            this.cbStageRow.Location = new System.Drawing.Point(230, 30);
            this.cbStageRow.Name = "cbStageRow";
            this.cbStageRow.Size = new System.Drawing.Size(64, 31);
            this.cbStageRow.TabIndex = 26;
            this.cbStageRow.Text = "1";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.LightBlue;
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(30, 30);
            this.label3.Name = "label3";
            this.label3.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.label3.Size = new System.Drawing.Size(65, 31);
            this.label3.TabIndex = 25;
            this.label3.Text = "COL";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cbStageCol
            // 
            this.cbStageCol.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbStageCol.FormattingEnabled = true;
            this.cbStageCol.Location = new System.Drawing.Point(97, 30);
            this.cbStageCol.Name = "cbStageCol";
            this.cbStageCol.Size = new System.Drawing.Size(64, 31);
            this.cbStageCol.TabIndex = 0;
            this.cbStageCol.Text = "1";
            // 
            // pnlBody
            // 
            this.pnlBody.BackColor = System.Drawing.Color.White;
            this.pnlBody.Controls.Add(this.radPanel1);
            this.pnlBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBody.Location = new System.Drawing.Point(328, 0);
            this.pnlBody.Name = "pnlBody";
            this.pnlBody.Padding = new System.Windows.Forms.Padding(5);
            this.pnlBody.Size = new System.Drawing.Size(1556, 791);
            this.pnlBody.TabIndex = 57;
            // 
            // radPanel1
            // 
            this.radPanel1.Controls.Add(this.dgvCalStatusList);
            this.radPanel1.Controls.Add(this.label1);
            this.radPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radPanel1.Location = new System.Drawing.Point(5, 5);
            this.radPanel1.Name = "radPanel1";
            this.radPanel1.Padding = new System.Windows.Forms.Padding(5);
            this.radPanel1.Size = new System.Drawing.Size(1546, 781);
            this.radPanel1.TabIndex = 19;
            this.radPanel1.ThemeName = "ControlDefault";
            ((Telerik.WinControls.UI.RadPanelElement)(this.radPanel1.GetChildAt(0))).Text = "";
            ((Telerik.WinControls.UI.RadPanelElement)(this.radPanel1.GetChildAt(0))).BorderHighlightColor = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(14)))), ((int)(((byte)(248)))));
            ((Telerik.WinControls.UI.RadPanelElement)(this.radPanel1.GetChildAt(0))).Padding = new System.Windows.Forms.Padding(5);
            // 
            // dgvCalStatusList
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvCalStatusList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvCalStatusList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvCalStatusList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column4,
            this.Column2,
            this.Column5,
            this.Column3,
            this.Column6});
            this.dgvCalStatusList.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgvCalStatusList.Location = new System.Drawing.Point(5, 30);
            this.dgvCalStatusList.Name = "dgvCalStatusList";
            this.dgvCalStatusList.RowTemplate.Height = 23;
            this.dgvCalStatusList.Size = new System.Drawing.Size(1536, 746);
            this.dgvCalStatusList.TabIndex = 51;
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
            this.Column1.Width = 140;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "마지막 보정 일자";
            this.Column4.Name = "Column4";
            this.Column4.Width = 180;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "보정 시작시간";
            this.Column2.Name = "Column2";
            this.Column2.Width = 180;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "진행 상태";
            this.Column5.Name = "Column5";
            this.Column5.Width = 150;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "보정 진행률";
            this.Column3.Name = "Column3";
            this.Column3.Width = 150;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "진행 시간";
            this.Column6.Name = "Column6";
            this.Column6.Width = 150;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.SteelBlue;
            this.label1.Location = new System.Drawing.Point(5, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(131, 16);
            this.label1.TabIndex = 50;
            this.label1.Text = "Calibration Status";
            // 
            // CalibrationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1884, 791);
            this.Controls.Add(this.pnlBody);
            this.Controls.Add(this.pnlLeft);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "CalibrationForm";
            this.Text = "CalibrationForm";
            this.pnlLeft.ResumeLayout(false);
            this.gbStageInfo.ResumeLayout(false);
            this.pnlBody.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).EndInit();
            this.radPanel1.ResumeLayout(false);
            this.radPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCalStatusList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlLeft;
        private System.Windows.Forms.GroupBox gbStageInfo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbStageName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbStageRow;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbStageCol;
        private System.Windows.Forms.Panel pnlBody;
        private Telerik.WinControls.UI.RadPanel radPanel1;
        private System.Windows.Forms.DataGridView dgvCalStatusList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnStartCalibration;
        private System.Windows.Forms.Button btnStopCalibration;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
    }
}