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
            dt.Columns.Add("RESULT");

            string channel = string.Empty;
            for (int i = 0; i < rowCount; i++)
            {
                channel = "STAGE " + (i + 1).ToString("D3");
                dt.Rows.Add(new string[] { channel, "-", "-", "-", "-", "-", "0" });

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
            GridView view = sender as GridView;
            if (e.Column == view.Columns[0])
            {
                e.Appearance.BackColor = _Constant.ColorReady;
                return;
            }

            /// result 값 가져오기
            var strValue = view.GetRowCellValue(e.RowHandle, view.Columns[6]).ToString();
            int cellValue = util.TryParseInt(strValue, 0);

            /// 첫번째 컬럼만 테스트
            if (e.Column == view.Columns[0])
            {
                if (strValue == "-") return;

                /// 판정을 RowCellStyle에서 할 때
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

                /// 판정을 set value 에서 할 때
                if (cellValue == -1) e.Appearance.BackColor = _Constant.ColorNoCell;
                else if (cellValue == 0) e.Appearance.BackColor = Color.White;
                else if (cellValue == 1 || cellValue == 3) e.Appearance.BackColor = _Constant.ColorFail;
                else if (cellValue == 2 || cellValue == 4) e.Appearance.BackColor = Color.Blue;
            }

            /// 여러 컬럼에 적용
            if (strValue == "-") return;

            /// 판정을 set value 에서 할 때
            ///e.Column.AppearanceCell.BackColor
            if (cellValue == -1) e.Appearance.BackColor = _Constant.ColorNoCell;
            else if (cellValue == 0) e.Appearance.BackColor = Color.White;
            else if ((cellValue == 1 || cellValue == 3) 
                && (e.Column == view.Columns[1] || e.Column == view.Columns[2]))    
            {
                e.Appearance.BackColor = _Constant.ColorFail;
            }
            else if ((cellValue == 2 || cellValue == 4)
                && (e.Column == view.Columns[1] || e.Column == view.Columns[2]))
            {
                e.Appearance.BackColor = Color.Blue;
            }
        }

        #endregion Grid View

        private void radButton13_Click(object sender, EventArgs e)
        {
            SetValueToGrid(gvEquipStatus, 0, 1, "3300");
            SetValueToGrid(gvEquipStatus, 0, 6, "0");

            SetValueToGrid(gvEquipStatus, 1, 1, "0");
            SetValueToGrid(gvEquipStatus, 1, 6, "1");

            SetValueToGrid(gvEquipStatus, 2, 1, "3300");
            SetValueToGrid(gvEquipStatus, 2, 6, "-1");

            SetValueToGrid(gvEquipStatus, 3, 1, "3300");
            SetValueToGrid(gvEquipStatus, 3, 6, "2");
        }

        private void gridView1_CustomDrawRowPreview(object sender, DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventArgs e)
        {
            int rowNumber = e.RowHandle + 1;
            if (rowNumber % 10 == 0)
            {
                e.Cache.FillRectangle(e.Cache.GetSolidBrush(Color.Black), e.Bounds);
                e.Handled = true;
            }
        }

        private void gridView1_MeasurePreviewHeight(object sender, RowHeightEventArgs e)
        {
            int rowNumber = e.RowHandle + 1;
            if(rowNumber % 10 == 0)
            {
                e.RowHeight = 1;
            }
            else
            {
                e.RowHeight = 0;
            }
        }
    }
}
