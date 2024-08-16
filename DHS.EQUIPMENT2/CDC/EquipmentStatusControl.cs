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
        int[] cells = new int[_Constant.ControllerCount];
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
            SetColumnStyle(gvEquipStatus, Color.DarkGray);

            
        }

        #region Grid Control - Dev Express
        
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

                //* for test
                cells[i] = 1;
                if (i == 2) cells[i] = 0;
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

                if (nIndex == 0) nWidth = 200;
                else nWidth = 300;
                view.Columns[nIndex].Width = nWidth;
            }
        }
        private void SetValueToGrid(GridControl gc, int nRow, int nCol, string value) 
        {
            GridView view = gc.MainView as GridView;
            view.SetRowCellValue(nRow, view.Columns[nCol].FieldName, value);
        }
        private void gridView1_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.Column == view.Columns[0])
                e.Appearance.BackColor = _Constant.ColorReady;


            var strValue = view.GetRowCellValue(e.RowHandle, e.Column).ToString();
            double cellValue = util.TryParseDouble(strValue, 0);

            //* 첫번째 컬럼만 테스트
            if(e.Column == view.Columns[1])
            {
                if (strValue == "-") return;

                if (cells[e.RowHandle] == 1)
                {
                    if (cellValue > 2000 && cellValue <= 4200)
                        e.Appearance.BackColor = _Constant.ColorVoltage;
                    else
                        e.Appearance.BackColor = _Constant.ColorFail;
                }
                else if (cells[e.RowHandle] == 0)
                {
                    e.Appearance.BackColor = _Constant.ColorNoCell;
                }
            }
        }

        #endregion Grid View

        private void radButton13_Click(object sender, EventArgs e)
        {
            SetValueToGrid(gvEquipStatus, 0, 1, "3300");
            SetValueToGrid(gvEquipStatus, 1, 1, "0");
            SetValueToGrid(gvEquipStatus, 2, 1, "3300");
            SetValueToGrid(gvEquipStatus, 3, 1, "3300");
        }
    }
}
