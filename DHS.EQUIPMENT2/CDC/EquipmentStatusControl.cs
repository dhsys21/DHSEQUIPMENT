using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid;
using DHS.EQUIPMENT2.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telerik.WinControls.UI;

namespace DHS.EQUIPMENT2.CDC
{
    public partial class EquipmentStatusControl : UserControl
    {
        Util util = new Util();
        private static EquipmentStatusControl equipmentStatusControl = new EquipmentStatusControl();
        public static EquipmentStatusControl GetInstance()
        {
            if (equipmentStatusControl == null) equipmentStatusControl = new EquipmentStatusControl();
            return equipmentStatusControl;
        }
        public EquipmentStatusControl()
        {
            InitializeComponent();

            makeGridView(gvEquipStatus, _Constant.ControllerCount);
            SetColumnHeaderColor(gvEquipStatus, Color.DarkGray);
            //GridView view = gvEquipStatus.MainView as GridView;
            //view.Columns[0].AppearanceHeader.BackColor = Color.DarkGray;
            
        }

        #region Make Grid Control - Dev Express
        /// row color
        ///  column header color
        private void SetColumnHeaderColor(GridControl gc, Color clr)
        {
            GridView view = gc.MainView as GridView;
            for(int nIndex = 0; nIndex < view.Columns.Count; nIndex++)
                view.Columns[nIndex].AppearanceHeader.BackColor = clr;
        }
        public void setcolumnheadercolor(GridControl gc, GridView gridView)
        {
            gridView.CustomDrawColumnHeader += (s, e) => {
                if (e.Column == null || e.Column.FieldName != "CHANNEL")
                    return;
                // Fill column headers with the specified colors.
                e.Cache.FillRectangle(Color.Coral, e.Bounds);
                e.Appearance.DrawString(e.Cache, e.Info.Caption, e.Info.CaptionRect);
                e.Handled = true;
            };
        }
        private void makeGridView(GridControl gc, int rowCount)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("STAGE NO.");
            dt.Columns.Add("작업상태");
            dt.Columns.Add("타입");
            dt.Columns.Add("프로세스");
            dt.Columns.Add("STEP");
            dt.Columns.Add("TRAY ID");

            string channel = string.Empty;
            for (int i = 0; i < rowCount; i++)
            {
                channel = (i + 1).ToString();
                dt.Rows.Add(new string[] { channel, "-", "-", "-", "-", "-" });
            }

            gc.DataSource = dt;
        }

        #endregion Make Grid View

        private void gridView1_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.Column != view.Columns[0])
                return;
            e.Appearance.BackColor = _Constant.ColorReady;
        }
    }
}
