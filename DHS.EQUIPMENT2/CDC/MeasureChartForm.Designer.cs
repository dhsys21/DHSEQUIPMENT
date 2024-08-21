
namespace DHS.EQUIPMENT2
{
    partial class MeasureChartForm
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
            DevExpress.XtraCharts.XYDiagram xyDiagram1 = new DevExpress.XtraCharts.XYDiagram();
            DevExpress.XtraCharts.LineSeriesView lineSeriesView1 = new DevExpress.XtraCharts.LineSeriesView();
            DevExpress.XtraCharts.ChartTitle chartTitle1 = new DevExpress.XtraCharts.ChartTitle();
            this.devChart = new DevExpress.XtraCharts.ChartControl();
            this.pnlLeft = new System.Windows.Forms.Panel();
            this.dgvFileList = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gbStageInfo = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cbStageName = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbStageRow = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbStageCol = new System.Windows.Forms.ComboBox();
            this.pnlFooter = new System.Windows.Forms.Panel();
            this.chkChannel4 = new System.Windows.Forms.CheckBox();
            this.chkChannel3 = new System.Windows.Forms.CheckBox();
            this.chkChannel2 = new System.Windows.Forms.CheckBox();
            this.chkChannel1 = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.devChart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(lineSeriesView1)).BeginInit();
            this.pnlLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFileList)).BeginInit();
            this.gbStageInfo.SuspendLayout();
            this.pnlFooter.SuspendLayout();
            this.SuspendLayout();
            // 
            // devChart
            // 
            this.devChart.CacheToMemory = true;
            xyDiagram1.AxisX.VisibleInPanesSerializable = "-1";
            xyDiagram1.AxisY.VisibleInPanesSerializable = "-1";
            this.devChart.Diagram = xyDiagram1;
            this.devChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.devChart.Legend.LegendID = -1;
            this.devChart.Location = new System.Drawing.Point(328, 0);
            this.devChart.Name = "devChart";
            this.devChart.SeriesDataMember = "CHANNEL";
            this.devChart.SeriesSerializable = new DevExpress.XtraCharts.Series[0];
            this.devChart.SeriesTemplate.SeriesDataMember = "CHANNEL";
            this.devChart.SeriesTemplate.ValueDataMembersSerializable = "CURRENT";
            lineSeriesView1.LineMarkerOptions.Kind = DevExpress.XtraCharts.MarkerKind.Square;
            lineSeriesView1.LineMarkerOptions.Size = 5;
            this.devChart.SeriesTemplate.View = lineSeriesView1;
            this.devChart.Size = new System.Drawing.Size(1572, 722);
            this.devChart.TabIndex = 0;
            chartTitle1.Text = "";
            chartTitle1.TitleID = 0;
            this.devChart.Titles.AddRange(new DevExpress.XtraCharts.ChartTitle[] {
            chartTitle1});
            // 
            // pnlLeft
            // 
            this.pnlLeft.BackColor = System.Drawing.Color.White;
            this.pnlLeft.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlLeft.Controls.Add(this.dgvFileList);
            this.pnlLeft.Controls.Add(this.gbStageInfo);
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlLeft.Location = new System.Drawing.Point(0, 0);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Padding = new System.Windows.Forms.Padding(5);
            this.pnlLeft.Size = new System.Drawing.Size(328, 722);
            this.pnlLeft.TabIndex = 55;
            // 
            // dgvFileList
            // 
            this.dgvFileList.BackgroundColor = System.Drawing.Color.White;
            this.dgvFileList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFileList.ColumnHeadersVisible = false;
            this.dgvFileList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2});
            this.dgvFileList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvFileList.Location = new System.Drawing.Point(5, 185);
            this.dgvFileList.MultiSelect = false;
            this.dgvFileList.Name = "dgvFileList";
            this.dgvFileList.RowHeadersVisible = false;
            this.dgvFileList.RowTemplate.Height = 23;
            this.dgvFileList.Size = new System.Drawing.Size(316, 530);
            this.dgvFileList.TabIndex = 56;
            this.dgvFileList.SelectionChanged += new System.EventHandler(this.dgvFileList_SelectionChanged);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "FILENAME";
            this.Column1.Name = "Column1";
            this.Column1.Width = 312;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "FULLNAME";
            this.Column2.Name = "Column2";
            this.Column2.Visible = false;
            // 
            // gbStageInfo
            // 
            this.gbStageInfo.BackColor = System.Drawing.Color.White;
            this.gbStageInfo.Controls.Add(this.label5);
            this.gbStageInfo.Controls.Add(this.cbStageName);
            this.gbStageInfo.Controls.Add(this.label4);
            this.gbStageInfo.Controls.Add(this.cbStageRow);
            this.gbStageInfo.Controls.Add(this.label3);
            this.gbStageInfo.Controls.Add(this.cbStageCol);
            this.gbStageInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbStageInfo.Location = new System.Drawing.Point(5, 5);
            this.gbStageInfo.Name = "gbStageInfo";
            this.gbStageInfo.Size = new System.Drawing.Size(316, 180);
            this.gbStageInfo.TabIndex = 55;
            this.gbStageInfo.TabStop = false;
            this.gbStageInfo.Text = "STAGE 선택";
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
            "STAGE001",
            "STAGE002",
            "STAGE003",
            "STAGE004",
            "STAGE005",
            "STAGE006"});
            this.cbStageName.Location = new System.Drawing.Point(162, 81);
            this.cbStageName.Name = "cbStageName";
            this.cbStageName.Size = new System.Drawing.Size(132, 31);
            this.cbStageName.TabIndex = 28;
            this.cbStageName.Text = "        -";
            this.cbStageName.SelectedIndexChanged += new System.EventHandler(this.cbStageName_SelectedIndexChanged);
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
            // pnlFooter
            // 
            this.pnlFooter.BackColor = System.Drawing.Color.White;
            this.pnlFooter.Controls.Add(this.chkChannel4);
            this.pnlFooter.Controls.Add(this.chkChannel3);
            this.pnlFooter.Controls.Add(this.chkChannel2);
            this.pnlFooter.Controls.Add(this.chkChannel1);
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Location = new System.Drawing.Point(0, 722);
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Padding = new System.Windows.Forms.Padding(5);
            this.pnlFooter.Size = new System.Drawing.Size(1900, 108);
            this.pnlFooter.TabIndex = 56;
            // 
            // chkChannel4
            // 
            this.chkChannel4.Checked = true;
            this.chkChannel4.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkChannel4.Font = new System.Drawing.Font("굴림", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.chkChannel4.ForeColor = System.Drawing.Color.Red;
            this.chkChannel4.Location = new System.Drawing.Point(19, 86);
            this.chkChannel4.Name = "chkChannel4";
            this.chkChannel4.Size = new System.Drawing.Size(120, 22);
            this.chkChannel4.TabIndex = 3;
            this.chkChannel4.Tag = "3";
            this.chkChannel4.Text = "CH49 - CH64";
            this.chkChannel4.UseVisualStyleBackColor = true;
            this.chkChannel4.CheckedChanged += new System.EventHandler(this.chkChannel_CheckedChanged);
            // 
            // chkChannel3
            // 
            this.chkChannel3.Checked = true;
            this.chkChannel3.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkChannel3.Font = new System.Drawing.Font("굴림", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.chkChannel3.ForeColor = System.Drawing.Color.Red;
            this.chkChannel3.Location = new System.Drawing.Point(19, 59);
            this.chkChannel3.Name = "chkChannel3";
            this.chkChannel3.Size = new System.Drawing.Size(120, 22);
            this.chkChannel3.TabIndex = 2;
            this.chkChannel3.Tag = "2";
            this.chkChannel3.Text = "CH33 - CH48";
            this.chkChannel3.UseVisualStyleBackColor = true;
            this.chkChannel3.CheckedChanged += new System.EventHandler(this.chkChannel_CheckedChanged);
            // 
            // chkChannel2
            // 
            this.chkChannel2.Checked = true;
            this.chkChannel2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkChannel2.Font = new System.Drawing.Font("굴림", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.chkChannel2.ForeColor = System.Drawing.Color.Red;
            this.chkChannel2.Location = new System.Drawing.Point(19, 32);
            this.chkChannel2.Name = "chkChannel2";
            this.chkChannel2.Size = new System.Drawing.Size(120, 22);
            this.chkChannel2.TabIndex = 1;
            this.chkChannel2.Tag = "1";
            this.chkChannel2.Text = "CH7 - CH32";
            this.chkChannel2.UseVisualStyleBackColor = true;
            this.chkChannel2.CheckedChanged += new System.EventHandler(this.chkChannel_CheckedChanged);
            // 
            // chkChannel1
            // 
            this.chkChannel1.Checked = true;
            this.chkChannel1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkChannel1.Font = new System.Drawing.Font("굴림", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.chkChannel1.ForeColor = System.Drawing.Color.Red;
            this.chkChannel1.Location = new System.Drawing.Point(19, 5);
            this.chkChannel1.Name = "chkChannel1";
            this.chkChannel1.Size = new System.Drawing.Size(120, 22);
            this.chkChannel1.TabIndex = 0;
            this.chkChannel1.Tag = "0";
            this.chkChannel1.Text = "CH1 - CH16";
            this.chkChannel1.UseVisualStyleBackColor = true;
            this.chkChannel1.CheckedChanged += new System.EventHandler(this.chkChannel_CheckedChanged);
            // 
            // MeasureChartForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1900, 830);
            this.Controls.Add(this.devChart);
            this.Controls.Add(this.pnlLeft);
            this.Controls.Add(this.pnlFooter);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MeasureChartForm";
            this.Text = "MeasureChartForm";
            ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(lineSeriesView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.devChart)).EndInit();
            this.pnlLeft.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFileList)).EndInit();
            this.gbStageInfo.ResumeLayout(false);
            this.pnlFooter.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraCharts.ChartControl devChart;
        private System.Windows.Forms.Panel pnlLeft;
        private System.Windows.Forms.GroupBox gbStageInfo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbStageName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbStageRow;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbStageCol;
        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.CheckBox chkChannel1;
        private System.Windows.Forms.CheckBox chkChannel4;
        private System.Windows.Forms.CheckBox chkChannel3;
        private System.Windows.Forms.CheckBox chkChannel2;
        private System.Windows.Forms.DataGridView dgvFileList;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
    }
}