
namespace DHS.EQUIPMENT2
{
    partial class MeasureInfoForm
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.pBase = new System.Windows.Forms.Panel();
            this.tbTime = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cbStageName = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbStageRow = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbStageCol = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblTrayId = new System.Windows.Forms.Label();
            this.lblStepNo = new System.Windows.Forms.Label();
            this.lblStepStatus = new System.Windows.Forms.Label();
            this.lblStepTime = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblRecipe = new System.Windows.Forms.Label();
            this.btnFinData = new System.Windows.Forms.Button();
            this.flowLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.LightBlue;
            this.lblTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTitle.Font = new System.Drawing.Font("굴림", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblTitle.ForeColor = System.Drawing.Color.Black;
            this.lblTitle.Location = new System.Drawing.Point(21, 5);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.lblTitle.Size = new System.Drawing.Size(316, 59);
            this.lblTitle.TabIndex = 24;
            this.lblTitle.Text = "STAGE";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.label1);
            this.flowLayoutPanel1.Controls.Add(this.label2);
            this.flowLayoutPanel1.Controls.Add(this.label14);
            this.flowLayoutPanel1.Controls.Add(this.label17);
            this.flowLayoutPanel1.Controls.Add(this.label18);
            this.flowLayoutPanel1.Controls.Add(this.label13);
            this.flowLayoutPanel1.Controls.Add(this.label10);
            this.flowLayoutPanel1.Controls.Add(this.label11);
            this.flowLayoutPanel1.Controls.Add(this.label12);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(21, 71);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(3);
            this.flowLayoutPanel1.Size = new System.Drawing.Size(316, 63);
            this.flowLayoutPanel1.TabIndex = 38;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.LightSkyBlue;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.Location = new System.Drawing.Point(4, 4);
            this.label1.Margin = new System.Windows.Forms.Padding(1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 24);
            this.label1.TabIndex = 26;
            this.label1.Text = "Voltage";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.LightPink;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.Location = new System.Drawing.Point(66, 4);
            this.label2.Margin = new System.Windows.Forms.Padding(1);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 24);
            this.label2.TabIndex = 27;
            this.label2.Text = "Current";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.Gainsboro;
            this.label14.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(128, 4);
            this.label14.Margin = new System.Windows.Forms.Padding(1);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(60, 24);
            this.label14.TabIndex = 40;
            this.label14.Text = "Status";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label17
            // 
            this.label17.BackColor = System.Drawing.Color.Silver;
            this.label17.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(190, 4);
            this.label17.Margin = new System.Windows.Forms.Padding(1);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(60, 24);
            this.label17.TabIndex = 35;
            this.label17.Text = "No Cell";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label18
            // 
            this.label18.BackColor = System.Drawing.Color.Red;
            this.label18.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(252, 4);
            this.label18.Margin = new System.Windows.Forms.Padding(1);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(60, 24);
            this.label18.TabIndex = 34;
            this.label18.Text = "Error";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.Yellow;
            this.label13.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(4, 30);
            this.label13.Margin = new System.Windows.Forms.Padding(1);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(60, 24);
            this.label13.TabIndex = 36;
            this.label13.Text = "Cell Flow";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.Orange;
            this.label10.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label10.Location = new System.Drawing.Point(66, 32);
            this.label10.Margin = new System.Windows.Forms.Padding(1, 3, 1, 1);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(79, 24);
            this.label10.TabIndex = 37;
            this.label10.Text = "Charging";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.DarkOrange;
            this.label11.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label11.Location = new System.Drawing.Point(147, 32);
            this.label11.Margin = new System.Windows.Forms.Padding(1, 3, 1, 1);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(103, 24);
            this.label11.TabIndex = 38;
            this.label11.Text = "DisCharging";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.LimeGreen;
            this.label12.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(252, 32);
            this.label12.Margin = new System.Windows.Forms.Padding(1, 3, 1, 1);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(60, 24);
            this.label12.TabIndex = 39;
            this.label12.Text = "Finish";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pBase
            // 
            this.pBase.AutoScroll = true;
            this.pBase.BackColor = System.Drawing.Color.Black;
            this.pBase.Location = new System.Drawing.Point(360, 5);
            this.pBase.Name = "pBase";
            this.pBase.Padding = new System.Windows.Forms.Padding(2);
            this.pBase.Size = new System.Drawing.Size(1478, 780);
            this.pBase.TabIndex = 39;
            // 
            // tbTime
            // 
            this.tbTime.Font = new System.Drawing.Font("굴림", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.tbTime.Location = new System.Drawing.Point(108, 498);
            this.tbTime.Name = "tbTime";
            this.tbTime.Size = new System.Drawing.Size(75, 35);
            this.tbTime.TabIndex = 10;
            this.tbTime.Text = "30";
            this.tbTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.cbStageName);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.cbStageRow);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cbStageCol);
            this.groupBox1.Location = new System.Drawing.Point(21, 144);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(316, 127);
            this.groupBox1.TabIndex = 53;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "STAGE 선택";
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
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblTrayId);
            this.groupBox2.Controls.Add(this.lblStepNo);
            this.groupBox2.Controls.Add(this.lblStepStatus);
            this.groupBox2.Controls.Add(this.lblStepTime);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Location = new System.Drawing.Point(21, 277);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(316, 204);
            this.groupBox2.TabIndex = 54;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "STAGE 상태";
            // 
            // lblTrayId
            // 
            this.lblTrayId.BackColor = System.Drawing.Color.Black;
            this.lblTrayId.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblTrayId.ForeColor = System.Drawing.Color.White;
            this.lblTrayId.Location = new System.Drawing.Point(177, 30);
            this.lblTrayId.Name = "lblTrayId";
            this.lblTrayId.Size = new System.Drawing.Size(127, 34);
            this.lblTrayId.TabIndex = 56;
            this.lblTrayId.Text = "-";
            this.lblTrayId.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblStepNo
            // 
            this.lblStepNo.BackColor = System.Drawing.Color.Black;
            this.lblStepNo.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblStepNo.ForeColor = System.Drawing.Color.White;
            this.lblStepNo.Location = new System.Drawing.Point(177, 70);
            this.lblStepNo.Name = "lblStepNo";
            this.lblStepNo.Size = new System.Drawing.Size(127, 34);
            this.lblStepNo.TabIndex = 55;
            this.lblStepNo.Text = "-";
            this.lblStepNo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblStepStatus
            // 
            this.lblStepStatus.BackColor = System.Drawing.Color.Black;
            this.lblStepStatus.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblStepStatus.ForeColor = System.Drawing.Color.White;
            this.lblStepStatus.Location = new System.Drawing.Point(177, 110);
            this.lblStepStatus.Name = "lblStepStatus";
            this.lblStepStatus.Size = new System.Drawing.Size(127, 34);
            this.lblStepStatus.TabIndex = 54;
            this.lblStepStatus.Text = "-";
            this.lblStepStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblStepTime
            // 
            this.lblStepTime.BackColor = System.Drawing.Color.Black;
            this.lblStepTime.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblStepTime.ForeColor = System.Drawing.Color.White;
            this.lblStepTime.Location = new System.Drawing.Point(177, 150);
            this.lblStepTime.Name = "lblStepTime";
            this.lblStepTime.Size = new System.Drawing.Size(127, 34);
            this.lblStepTime.TabIndex = 53;
            this.lblStepTime.Text = "-";
            this.lblStepTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.LightBlue;
            this.label9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label9.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(12, 150);
            this.label9.Name = "label9";
            this.label9.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.label9.Size = new System.Drawing.Size(159, 34);
            this.label9.TabIndex = 31;
            this.label9.Text = "PROCESS TIME";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.LightBlue;
            this.label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label8.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(12, 110);
            this.label8.Name = "label8";
            this.label8.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.label8.Size = new System.Drawing.Size(159, 34);
            this.label8.TabIndex = 30;
            this.label8.Text = "STATUS";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.LightBlue;
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label6.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(12, 70);
            this.label6.Name = "label6";
            this.label6.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.label6.Size = new System.Drawing.Size(159, 34);
            this.label6.TabIndex = 29;
            this.label6.Text = "STEP";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.LightBlue;
            this.label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label7.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(12, 30);
            this.label7.Name = "label7";
            this.label7.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.label7.Size = new System.Drawing.Size(159, 34);
            this.label7.TabIndex = 27;
            this.label7.Text = "TRAY ID";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lblRecipe);
            this.groupBox3.Location = new System.Drawing.Point(21, 541);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(316, 223);
            this.groupBox3.TabIndex = 55;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "RECIPE 정보";
            // 
            // lblRecipe
            // 
            this.lblRecipe.Location = new System.Drawing.Point(12, 17);
            this.lblRecipe.Name = "lblRecipe";
            this.lblRecipe.Size = new System.Drawing.Size(292, 195);
            this.lblRecipe.TabIndex = 0;
            // 
            // btnFinData
            // 
            this.btnFinData.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnFinData.Location = new System.Drawing.Point(201, 495);
            this.btnFinData.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnFinData.Name = "btnFinData";
            this.btnFinData.Size = new System.Drawing.Size(136, 42);
            this.btnFinData.TabIndex = 56;
            this.btnFinData.Text = "Get FIN DAta";
            this.btnFinData.UseVisualStyleBackColor = true;
            this.btnFinData.Click += new System.EventHandler(this.btnFinData_Click);
            // 
            // MeasureInfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1900, 830);
            this.Controls.Add(this.tbTime);
            this.Controls.Add(this.btnFinData);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pBase);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MeasureInfoForm";
            this.Text = "MeasureInfoForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MeasureInfoForm_FormClosing);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Panel pBase;
        private System.Windows.Forms.TextBox tbTime;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbStageName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbStageRow;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbStageCol;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label lblTrayId;
        private System.Windows.Forms.Label lblStepNo;
        private System.Windows.Forms.Label lblStepStatus;
        private System.Windows.Forms.Label lblStepTime;
        private System.Windows.Forms.Button btnFinData;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label lblRecipe;
    }
}