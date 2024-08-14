
namespace DHS.EQUIPMENT2
{
    partial class CHARGERForm
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
            this.radpnl_Body = new Telerik.WinControls.UI.RadPanel();
            this.radpnl_Status = new Telerik.WinControls.UI.RadPanel();
            this.radbtn_STOP = new Telerik.WinControls.UI.RadButton();
            this.lblStatus = new System.Windows.Forms.Label();
            this.radbtn_MANU = new Telerik.WinControls.UI.RadButton();
            this.radbtn_AUTO = new Telerik.WinControls.UI.RadButton();
            this.radpnl_Title = new Telerik.WinControls.UI.RadPanel();
            this.lblStageTitle = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.radpnl_Body)).BeginInit();
            this.radpnl_Body.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radpnl_Status)).BeginInit();
            this.radpnl_Status.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radbtn_STOP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radbtn_MANU)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radbtn_AUTO)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radpnl_Title)).BeginInit();
            this.radpnl_Title.SuspendLayout();
            this.SuspendLayout();
            // 
            // radpnl_Body
            // 
            this.radpnl_Body.Controls.Add(this.radpnl_Status);
            this.radpnl_Body.Controls.Add(this.radpnl_Title);
            this.radpnl_Body.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radpnl_Body.Location = new System.Drawing.Point(0, 0);
            this.radpnl_Body.Name = "radpnl_Body";
            this.radpnl_Body.Padding = new System.Windows.Forms.Padding(2);
            // 
            // 
            // 
            this.radpnl_Body.RootElement.BorderHighlightThickness = 2;
            this.radpnl_Body.Size = new System.Drawing.Size(220, 145);
            this.radpnl_Body.TabIndex = 16;
            this.radpnl_Body.ThemeName = "ControlDefault";
            ((Telerik.WinControls.UI.RadPanelElement)(this.radpnl_Body.GetChildAt(0))).Text = "";
            ((Telerik.WinControls.UI.RadPanelElement)(this.radpnl_Body.GetChildAt(0))).BorderHighlightColor = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(14)))), ((int)(((byte)(248)))));
            ((Telerik.WinControls.UI.RadPanelElement)(this.radpnl_Body.GetChildAt(0))).Padding = new System.Windows.Forms.Padding(2);
            // 
            // radpnl_Status
            // 
            this.radpnl_Status.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.radpnl_Status.Controls.Add(this.radbtn_STOP);
            this.radpnl_Status.Controls.Add(this.lblStatus);
            this.radpnl_Status.Controls.Add(this.radbtn_MANU);
            this.radpnl_Status.Controls.Add(this.radbtn_AUTO);
            this.radpnl_Status.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.radpnl_Status.Location = new System.Drawing.Point(2, 46);
            this.radpnl_Status.Name = "radpnl_Status";
            this.radpnl_Status.Padding = new System.Windows.Forms.Padding(5);
            this.radpnl_Status.Size = new System.Drawing.Size(216, 97);
            this.radpnl_Status.TabIndex = 17;
            this.radpnl_Status.ThemeName = "ControlDefault";
            ((Telerik.WinControls.UI.RadPanelElement)(this.radpnl_Status.GetChildAt(0))).Text = "";
            ((Telerik.WinControls.UI.RadPanelElement)(this.radpnl_Status.GetChildAt(0))).BorderHighlightColor = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(14)))), ((int)(((byte)(248)))));
            ((Telerik.WinControls.UI.RadPanelElement)(this.radpnl_Status.GetChildAt(0))).Padding = new System.Windows.Forms.Padding(5);
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.radpnl_Status.GetChildAt(0).GetChildAt(0))).Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.radpnl_Status.GetChildAt(0).GetChildAt(0))).Margin = new System.Windows.Forms.Padding(0, 3, 0, 0);
            // 
            // radbtn_STOP
            // 
            this.radbtn_STOP.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radbtn_STOP.ForeColor = System.Drawing.Color.Black;
            this.radbtn_STOP.Location = new System.Drawing.Point(143, 4);
            this.radbtn_STOP.Margin = new System.Windows.Forms.Padding(4);
            this.radbtn_STOP.Name = "radbtn_STOP";
            this.radbtn_STOP.Size = new System.Drawing.Size(64, 32);
            this.radbtn_STOP.TabIndex = 54;
            this.radbtn_STOP.Text = "STOP";
            this.radbtn_STOP.TextWrap = true;
            this.radbtn_STOP.ThemeName = "ControlDefault";
            ((Telerik.WinControls.UI.RadButtonElement)(this.radbtn_STOP.GetChildAt(0))).Text = "STOP";
            ((Telerik.WinControls.UI.RadButtonElement)(this.radbtn_STOP.GetChildAt(0))).Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.radbtn_STOP.GetChildAt(0).GetChildAt(0))).BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(57)))), ((int)(((byte)(53)))));
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.radbtn_STOP.GetChildAt(0).GetChildAt(0))).BackColor3 = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(57)))), ((int)(((byte)(53)))));
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.radbtn_STOP.GetChildAt(0).GetChildAt(0))).BackColor4 = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(154)))), ((int)(((byte)(154)))));
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.radbtn_STOP.GetChildAt(0).GetChildAt(0))).BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(154)))), ((int)(((byte)(154)))));
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.radbtn_STOP.GetChildAt(0).GetChildAt(0))).CanFocus = true;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.radbtn_STOP.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextWrap = true;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.radbtn_STOP.GetChildAt(0).GetChildAt(1).GetChildAt(1))).LineLimit = true;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.radbtn_STOP.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.radbtn_STOP.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Padding = new System.Windows.Forms.Padding(4);
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.radbtn_STOP.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Alignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblStatus
            // 
            this.lblStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblStatus.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.ForeColor = System.Drawing.Color.SteelBlue;
            this.lblStatus.Location = new System.Drawing.Point(5, 46);
            this.lblStatus.Margin = new System.Windows.Forms.Padding(3);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Padding = new System.Windows.Forms.Padding(3);
            this.lblStatus.Size = new System.Drawing.Size(206, 46);
            this.lblStatus.TabIndex = 57;
            this.lblStatus.Text = "STATUS";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // radbtn_MANU
            // 
            this.radbtn_MANU.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radbtn_MANU.ForeColor = System.Drawing.Color.Black;
            this.radbtn_MANU.Location = new System.Drawing.Point(75, 4);
            this.radbtn_MANU.Margin = new System.Windows.Forms.Padding(4);
            this.radbtn_MANU.Name = "radbtn_MANU";
            this.radbtn_MANU.Size = new System.Drawing.Size(64, 32);
            this.radbtn_MANU.TabIndex = 56;
            this.radbtn_MANU.Text = "MANU";
            this.radbtn_MANU.ThemeName = "ControlDefault";
            ((Telerik.WinControls.UI.RadButtonElement)(this.radbtn_MANU.GetChildAt(0))).Text = "MANU";
            ((Telerik.WinControls.UI.RadButtonElement)(this.radbtn_MANU.GetChildAt(0))).Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.radbtn_MANU.GetChildAt(0).GetChildAt(0))).BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(241)))), ((int)(((byte)(252)))));
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.radbtn_MANU.GetChildAt(0).GetChildAt(0))).BackColor3 = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(226)))), ((int)(((byte)(244)))));
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.radbtn_MANU.GetChildAt(0).GetChildAt(0))).BackColor4 = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(226)))), ((int)(((byte)(244)))));
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.radbtn_MANU.GetChildAt(0).GetChildAt(0))).BackColor = System.Drawing.Color.White;
            // 
            // radbtn_AUTO
            // 
            this.radbtn_AUTO.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radbtn_AUTO.ForeColor = System.Drawing.Color.White;
            this.radbtn_AUTO.Location = new System.Drawing.Point(7, 4);
            this.radbtn_AUTO.Margin = new System.Windows.Forms.Padding(4);
            this.radbtn_AUTO.Name = "radbtn_AUTO";
            this.radbtn_AUTO.Size = new System.Drawing.Size(64, 32);
            this.radbtn_AUTO.TabIndex = 55;
            this.radbtn_AUTO.Text = "AUTO";
            this.radbtn_AUTO.ThemeName = "ControlDefault";
            ((Telerik.WinControls.UI.RadButtonElement)(this.radbtn_AUTO.GetChildAt(0))).Text = "AUTO";
            ((Telerik.WinControls.UI.RadButtonElement)(this.radbtn_AUTO.GetChildAt(0))).Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.radbtn_AUTO.GetChildAt(0).GetChildAt(0))).BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(101)))), ((int)(((byte)(192)))));
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.radbtn_AUTO.GetChildAt(0).GetChildAt(0))).BackColor3 = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(101)))), ((int)(((byte)(192)))));
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.radbtn_AUTO.GetChildAt(0).GetChildAt(0))).BackColor4 = System.Drawing.Color.FromArgb(((int)(((byte)(144)))), ((int)(((byte)(202)))), ((int)(((byte)(249)))));
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.radbtn_AUTO.GetChildAt(0).GetChildAt(0))).BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(144)))), ((int)(((byte)(202)))), ((int)(((byte)(249)))));
            // 
            // radpnl_Title
            // 
            this.radpnl_Title.Controls.Add(this.lblStageTitle);
            this.radpnl_Title.Dock = System.Windows.Forms.DockStyle.Top;
            this.radpnl_Title.Location = new System.Drawing.Point(2, 2);
            this.radpnl_Title.Name = "radpnl_Title";
            this.radpnl_Title.Padding = new System.Windows.Forms.Padding(5);
            this.radpnl_Title.Size = new System.Drawing.Size(216, 42);
            this.radpnl_Title.TabIndex = 16;
            this.radpnl_Title.ThemeName = "ControlDefault";
            ((Telerik.WinControls.UI.RadPanelElement)(this.radpnl_Title.GetChildAt(0))).Text = "";
            ((Telerik.WinControls.UI.RadPanelElement)(this.radpnl_Title.GetChildAt(0))).BorderHighlightColor = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(14)))), ((int)(((byte)(248)))));
            ((Telerik.WinControls.UI.RadPanelElement)(this.radpnl_Title.GetChildAt(0))).Padding = new System.Windows.Forms.Padding(5);
            // 
            // lblStageTitle
            // 
            this.lblStageTitle.BackColor = System.Drawing.Color.White;
            this.lblStageTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblStageTitle.Font = new System.Drawing.Font("Arial", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStageTitle.ForeColor = System.Drawing.Color.SteelBlue;
            this.lblStageTitle.Location = new System.Drawing.Point(5, 5);
            this.lblStageTitle.Margin = new System.Windows.Forms.Padding(3);
            this.lblStageTitle.Name = "lblStageTitle";
            this.lblStageTitle.Padding = new System.Windows.Forms.Padding(3);
            this.lblStageTitle.Size = new System.Drawing.Size(206, 32);
            this.lblStageTitle.TabIndex = 50;
            this.lblStageTitle.Text = "STAGE NO.";
            this.lblStageTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CHARGERForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(220, 145);
            this.Controls.Add(this.radpnl_Body);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "CHARGERForm";
            this.Text = "CHARGERForm";
            ((System.ComponentModel.ISupportInitialize)(this.radpnl_Body)).EndInit();
            this.radpnl_Body.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.radpnl_Status)).EndInit();
            this.radpnl_Status.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.radbtn_STOP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radbtn_MANU)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radbtn_AUTO)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radpnl_Title)).EndInit();
            this.radpnl_Title.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadPanel radpnl_Body;
        private Telerik.WinControls.UI.RadPanel radpnl_Status;
        private Telerik.WinControls.UI.RadPanel radpnl_Title;
        private System.Windows.Forms.Label lblStageTitle;
        private System.Windows.Forms.Label lblStatus;
        private Telerik.WinControls.UI.RadButton radbtn_MANU;
        private Telerik.WinControls.UI.RadButton radbtn_AUTO;
        private Telerik.WinControls.UI.RadButton radbtn_STOP;
    }
}