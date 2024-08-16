namespace DHS.EQUIPMENT2.CDC
{
    partial class EquipmentStatusControl
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
            DevExpress.XtraGrid.GridFormatRule gridFormatRule1 = new DevExpress.XtraGrid.GridFormatRule();
            this.pnlSubMenu = new System.Windows.Forms.Panel();
            this.radButton13 = new Telerik.WinControls.UI.RadButton();
            this.radButton17 = new Telerik.WinControls.UI.RadButton();
            this.radButton18 = new Telerik.WinControls.UI.RadButton();
            this.radButton19 = new Telerik.WinControls.UI.RadButton();
            this.radButton20 = new Telerik.WinControls.UI.RadButton();
            this.radButton21 = new Telerik.WinControls.UI.RadButton();
            this.pBase = new System.Windows.Forms.Panel();
            this.gvEquipStatus = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.pnlSubMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radButton13)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButton17)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButton18)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButton19)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButton20)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButton21)).BeginInit();
            this.pBase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvEquipStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlSubMenu
            // 
            this.pnlSubMenu.BackColor = System.Drawing.Color.White;
            this.pnlSubMenu.Controls.Add(this.radButton13);
            this.pnlSubMenu.Controls.Add(this.radButton17);
            this.pnlSubMenu.Controls.Add(this.radButton18);
            this.pnlSubMenu.Controls.Add(this.radButton19);
            this.pnlSubMenu.Controls.Add(this.radButton20);
            this.pnlSubMenu.Controls.Add(this.radButton21);
            this.pnlSubMenu.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSubMenu.Location = new System.Drawing.Point(0, 0);
            this.pnlSubMenu.Name = "pnlSubMenu";
            this.pnlSubMenu.Padding = new System.Windows.Forms.Padding(3, 3, 3, 5);
            this.pnlSubMenu.Size = new System.Drawing.Size(1900, 74);
            this.pnlSubMenu.TabIndex = 13;
            // 
            // radButton13
            // 
            this.radButton13.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radButton13.ForeColor = System.Drawing.Color.Black;
            this.radButton13.Location = new System.Drawing.Point(13, 7);
            this.radButton13.Margin = new System.Windows.Forms.Padding(4);
            this.radButton13.Name = "radButton13";
            this.radButton13.Size = new System.Drawing.Size(140, 60);
            this.radButton13.TabIndex = 65;
            this.radButton13.Text = "공정진행시간";
            this.radButton13.ThemeName = "ControlDefault";
            this.radButton13.Click += new System.EventHandler(this.radButton13_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.radButton13.GetChildAt(0))).Text = "공정진행시간";
            ((Telerik.WinControls.UI.RadButtonElement)(this.radButton13.GetChildAt(0))).Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.radButton13.GetChildAt(0).GetChildAt(0))).BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(241)))), ((int)(((byte)(252)))));
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.radButton13.GetChildAt(0).GetChildAt(0))).BackColor3 = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(226)))), ((int)(((byte)(244)))));
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.radButton13.GetChildAt(0).GetChildAt(0))).BackColor4 = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(226)))), ((int)(((byte)(244)))));
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.radButton13.GetChildAt(0).GetChildAt(0))).BackColor = System.Drawing.Color.White;
            // 
            // radButton17
            // 
            this.radButton17.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radButton17.ForeColor = System.Drawing.Color.Black;
            this.radButton17.Location = new System.Drawing.Point(753, 7);
            this.radButton17.Margin = new System.Windows.Forms.Padding(4);
            this.radButton17.Name = "radButton17";
            this.radButton17.Size = new System.Drawing.Size(140, 60);
            this.radButton17.TabIndex = 61;
            this.radButton17.Text = "작업설정";
            this.radButton17.ThemeName = "ControlDefault";
            ((Telerik.WinControls.UI.RadButtonElement)(this.radButton17.GetChildAt(0))).Text = "작업설정";
            ((Telerik.WinControls.UI.RadButtonElement)(this.radButton17.GetChildAt(0))).Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.radButton17.GetChildAt(0).GetChildAt(0))).BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(241)))), ((int)(((byte)(252)))));
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.radButton17.GetChildAt(0).GetChildAt(0))).BackColor3 = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(226)))), ((int)(((byte)(244)))));
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.radButton17.GetChildAt(0).GetChildAt(0))).BackColor4 = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(226)))), ((int)(((byte)(244)))));
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.radButton17.GetChildAt(0).GetChildAt(0))).BackColor = System.Drawing.Color.White;
            // 
            // radButton18
            // 
            this.radButton18.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radButton18.ForeColor = System.Drawing.Color.Black;
            this.radButton18.Location = new System.Drawing.Point(605, 7);
            this.radButton18.Margin = new System.Windows.Forms.Padding(4);
            this.radButton18.Name = "radButton18";
            this.radButton18.Size = new System.Drawing.Size(140, 60);
            this.radButton18.TabIndex = 60;
            this.radButton18.Text = "공정정보";
            this.radButton18.ThemeName = "ControlDefault";
            ((Telerik.WinControls.UI.RadButtonElement)(this.radButton18.GetChildAt(0))).Text = "공정정보";
            ((Telerik.WinControls.UI.RadButtonElement)(this.radButton18.GetChildAt(0))).Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.radButton18.GetChildAt(0).GetChildAt(0))).BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(241)))), ((int)(((byte)(252)))));
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.radButton18.GetChildAt(0).GetChildAt(0))).BackColor3 = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(226)))), ((int)(((byte)(244)))));
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.radButton18.GetChildAt(0).GetChildAt(0))).BackColor4 = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(226)))), ((int)(((byte)(244)))));
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.radButton18.GetChildAt(0).GetChildAt(0))).BackColor = System.Drawing.Color.White;
            // 
            // radButton19
            // 
            this.radButton19.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radButton19.ForeColor = System.Drawing.Color.Black;
            this.radButton19.Location = new System.Drawing.Point(457, 7);
            this.radButton19.Margin = new System.Windows.Forms.Padding(4);
            this.radButton19.Name = "radButton19";
            this.radButton19.Size = new System.Drawing.Size(140, 60);
            this.radButton19.TabIndex = 59;
            this.radButton19.Text = "스텝 잔여시간";
            this.radButton19.ThemeName = "ControlDefault";
            ((Telerik.WinControls.UI.RadButtonElement)(this.radButton19.GetChildAt(0))).Text = "스텝 잔여시간";
            ((Telerik.WinControls.UI.RadButtonElement)(this.radButton19.GetChildAt(0))).Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.radButton19.GetChildAt(0).GetChildAt(0))).BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(241)))), ((int)(((byte)(252)))));
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.radButton19.GetChildAt(0).GetChildAt(0))).BackColor3 = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(226)))), ((int)(((byte)(244)))));
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.radButton19.GetChildAt(0).GetChildAt(0))).BackColor4 = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(226)))), ((int)(((byte)(244)))));
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.radButton19.GetChildAt(0).GetChildAt(0))).BackColor = System.Drawing.Color.White;
            // 
            // radButton20
            // 
            this.radButton20.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radButton20.ForeColor = System.Drawing.Color.Black;
            this.radButton20.Location = new System.Drawing.Point(309, 7);
            this.radButton20.Margin = new System.Windows.Forms.Padding(4);
            this.radButton20.Name = "radButton20";
            this.radButton20.Size = new System.Drawing.Size(140, 60);
            this.radButton20.TabIndex = 58;
            this.radButton20.Text = "스텝 진행시간";
            this.radButton20.ThemeName = "ControlDefault";
            ((Telerik.WinControls.UI.RadButtonElement)(this.radButton20.GetChildAt(0))).Text = "스텝 진행시간";
            ((Telerik.WinControls.UI.RadButtonElement)(this.radButton20.GetChildAt(0))).Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.radButton20.GetChildAt(0).GetChildAt(0))).BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(241)))), ((int)(((byte)(252)))));
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.radButton20.GetChildAt(0).GetChildAt(0))).BackColor3 = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(226)))), ((int)(((byte)(244)))));
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.radButton20.GetChildAt(0).GetChildAt(0))).BackColor4 = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(226)))), ((int)(((byte)(244)))));
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.radButton20.GetChildAt(0).GetChildAt(0))).BackColor = System.Drawing.Color.White;
            // 
            // radButton21
            // 
            this.radButton21.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radButton21.ForeColor = System.Drawing.Color.Black;
            this.radButton21.Location = new System.Drawing.Point(161, 7);
            this.radButton21.Margin = new System.Windows.Forms.Padding(4);
            this.radButton21.Name = "radButton21";
            this.radButton21.Size = new System.Drawing.Size(140, 60);
            this.radButton21.TabIndex = 57;
            this.radButton21.Text = "잔여시간";
            this.radButton21.ThemeName = "ControlDefault";
            ((Telerik.WinControls.UI.RadButtonElement)(this.radButton21.GetChildAt(0))).Text = "잔여시간";
            ((Telerik.WinControls.UI.RadButtonElement)(this.radButton21.GetChildAt(0))).Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.radButton21.GetChildAt(0).GetChildAt(0))).BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(241)))), ((int)(((byte)(252)))));
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.radButton21.GetChildAt(0).GetChildAt(0))).BackColor3 = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(226)))), ((int)(((byte)(244)))));
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.radButton21.GetChildAt(0).GetChildAt(0))).BackColor4 = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(226)))), ((int)(((byte)(244)))));
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.radButton21.GetChildAt(0).GetChildAt(0))).BackColor = System.Drawing.Color.White;
            // 
            // pBase
            // 
            this.pBase.BackColor = System.Drawing.Color.Black;
            this.pBase.Controls.Add(this.gvEquipStatus);
            this.pBase.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pBase.Location = new System.Drawing.Point(0, 74);
            this.pBase.Name = "pBase";
            this.pBase.Padding = new System.Windows.Forms.Padding(2);
            this.pBase.Size = new System.Drawing.Size(1900, 756);
            this.pBase.TabIndex = 67;
            // 
            // gvEquipStatus
            // 
            this.gvEquipStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gvEquipStatus.Location = new System.Drawing.Point(2, 2);
            this.gvEquipStatus.MainView = this.gridView1;
            this.gvEquipStatus.Name = "gvEquipStatus";
            this.gvEquipStatus.Size = new System.Drawing.Size(1896, 752);
            this.gvEquipStatus.TabIndex = 2;
            this.gvEquipStatus.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Appearance.HeaderPanel.BackColor = System.Drawing.Color.Black;
            this.gridView1.Appearance.HeaderPanel.Options.UseBackColor = true;
            this.gridView1.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gridView1.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridView1.Appearance.Row.Options.UseTextOptions = true;
            this.gridView1.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridView1.ColumnPanelRowHeight = 50;
            gridFormatRule1.Name = "Format0";
            gridFormatRule1.Rule = null;
            this.gridView1.FormatRules.Add(gridFormatRule1);
            this.gridView1.GridControl = this.gvEquipStatus;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.ReadOnly = true;
            this.gridView1.OptionsCustomization.AllowColumnMoving = false;
            this.gridView1.OptionsCustomization.AllowColumnResizing = false;
            this.gridView1.OptionsCustomization.AllowSort = false;
            this.gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.False;
            this.gridView1.OptionsView.EnableAppearanceOddRow = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.RowHeight = 40;
            this.gridView1.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gridView1_RowCellStyle);
            // 
            // EquipmentStatusControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.pBase);
            this.Controls.Add(this.pnlSubMenu);
            this.Name = "EquipmentStatusControl";
            this.Size = new System.Drawing.Size(1900, 830);
            this.pnlSubMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.radButton13)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButton17)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButton18)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButton19)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButton20)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButton21)).EndInit();
            this.pBase.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvEquipStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Panel pnlSubMenu;
        private Telerik.WinControls.UI.RadButton radButton13;
        private Telerik.WinControls.UI.RadButton radButton17;
        private Telerik.WinControls.UI.RadButton radButton18;
        private Telerik.WinControls.UI.RadButton radButton19;
        private Telerik.WinControls.UI.RadButton radButton20;
        private Telerik.WinControls.UI.RadButton radButton21;
        private System.Windows.Forms.Panel pBase;
        private DevExpress.XtraGrid.GridControl gvEquipStatus;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
    }
}
