using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Telerik.WinControls.NativeMethods;

namespace DHS.EQUIPMENT2.Common
{
    public partial class DEGridControl : UserControl
    {
        public int ROWSEPARATOR {  get; set; }
        public DEGridControl()
        {
            InitializeComponent();

            ROWSEPARATOR = 10;

            DataTable dt = new DataTable();
            dt.Columns.Add("STAGE NO.");
            dt.Columns.Add("작업상태");
            dt.Columns.Add("타입");
            dt.Columns.Add("프로세스");
            dt.Columns.Add("STEP");
            dt.Columns.Add("TRAY ID");
            dt.Columns.Add("RESULT");

            makeGridView(gridControl, dt, _Constant.ChannelCount);
        }

        #region Grid Control - Dev Express
        private void makeGridView(GridControl gc, DataTable dt, int rowCount)
        {
            string channel = string.Empty;
            string[] fields = new string[dt.Columns.Count];
            for (int i = 0; i < fields.Length; i++)
                fields[i] = " - ";
            for (int i = 0; i < rowCount; i++)
            {
                channel = "STAGE " + (i + 1).ToString("D3");
                //dt.Rows.Add(new string[] { channel, "-", "-", "-", "-", "-", "0" });
                dt.Rows.Add(fields);
            }

            gc.DataSource = dt;
        }
        private void SetColumnStyle(GridControl gc, Color clr)
        {
            int nWidth = 300;
            GridView view = gc.MainView as GridView;
            for (int nIndex = 0; nIndex < view.Columns.Count; nIndex++)
            {
                view.Columns[nIndex].AppearanceHeader.BackColor = clr;
                view.Columns[nIndex].AppearanceHeader.Font = new Font("Verdana", 14F, FontStyle.Bold);
                view.Columns[nIndex].AppearanceCell.Font = new Font("Verdana", 12F, FontStyle.Bold);

                if (nIndex == 0) nWidth = 208;
                else nWidth = 330;
                view.Columns[nIndex].Width = nWidth;
            }

            view.Columns[6].Visible = false;
        }
        private void SetValueToGrid(GridControl gc, int nRow, int nCol, string value)
        {
            GridView view = gc.MainView as GridView;
            view.SetRowCellValue(nRow, view.Columns[nCol].FieldName, value);
        }
        private void gridView1_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {

        }
        private void gridView1_CustomDrawRowPreview(object sender, DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventArgs e)
        {

        }

        private void gridView1_MeasurePreviewHeight(object sender, RowHeightEventArgs e)
        {

        }
        #endregion Grid Control - Dev Express
    }
}
