using DevExpress.Internal;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraRichEdit.Model;
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
using Telerik.WinControls;
using Telerik.WinControls.UI;

namespace DHS.EQUIPMENT2
{
    public partial class MeasureInfoControl : UserControl
    {
        Util util = new Util();
        TRAYINFO[] nTrayInfo = new TRAYINFO[_Constant.ControllerCount];

        private int nStageno;
        private bool bVisible;
        public int STAGENO { get => nStageno; set => nStageno = value; }
        public bool VISIBLE { get => bVisible; set => bVisible = value; }
        public int SEPERATOR { get; set; }
        private int nResultColumn;
        private int nRowHeight;

        private Timer _tmrGetMon = null;

        #region Delegation
        public delegate void SAVEFINDATA(int stageno);
        public event SAVEFINDATA OnSAVEFINDATA = null;
        protected void RaiseOnSaveFinData(int stageno)
        {
            if (OnSAVEFINDATA != null)
            {
                OnSAVEFINDATA(stageno);
            }
        }
        #endregion

        private static MeasureInfoControl measureinfoControl = new MeasureInfoControl();
        public static MeasureInfoControl GetInstance()
        {
            if (measureinfoControl == null) measureinfoControl = new MeasureInfoControl();
            return measureinfoControl;
        }
        public MeasureInfoControl()
        {
            InitializeComponent();

            for (int nIndex = 0; nIndex < _Constant.ControllerCount; nIndex++)
                nTrayInfo[nIndex] = TRAYINFO.GetInstance(nIndex);

            //* 결과 컬럼
            nResultColumn = 5;
            nRowHeight = 25;
            /// 채널 일정 갯수 마다 구분선
            /// 
            SEPERATOR = 10;
            /// 채널 절반 표시
            makeGridView(gvLeft, _Constant.ChannelCount / 2, 1);
            SetColumnStyle(gvLeft, Color.DarkGray);

            /// 채널 나머지 절반 표시
            makeGridView(gvRight, _Constant.ChannelCount / 2, _Constant.ChannelCount / 2 + 1);
            SetColumnStyle(gvRight, Color.DarkGray);

            //* Get Mon Data Timer
            _tmrGetMon = new Timer();
            _tmrGetMon.Interval = 1000;
            _tmrGetMon.Tick += new EventHandler(GetMonDataTimer_TickAsync);
            _tmrGetMon.Enabled = true;
        }

        public void SetStage(int stageno)
        {
            nStageno = stageno;
        }
        public void SetVisible(bool isVisibled)
        {
            bVisible = isVisibled;
        }

        #region Make Grid View
        private void makeGridView(GridControl gc, int nRowLength, int nStartIndex)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("채널");
            dt.Columns.Add("전압[mV]");
            dt.Columns.Add("전류[mA]");
            dt.Columns.Add("용량[mAh]");
            dt.Columns.Add("셀온도[℃]");
            dt.Columns.Add("RESULT");

            string channel = string.Empty;
            for (int i = nStartIndex; i < nRowLength + nStartIndex; i++)
            {
                channel = i.ToString();
                dt.Rows.Add(new string[] { channel, "-", "-", "-", "-", "-"});
            }

            gc.DataSource = dt;
        }
        private void SetColumnStyle(GridControl gc, Color clr)
        {
            int nWidth = 100;
            GridView view = gc.MainView as GridView;
            view.RowHeight = nRowHeight;

            for (int nIndex = 0; nIndex < view.Columns.Count; nIndex++)
            {
                view.Columns[nIndex].AppearanceHeader.BackColor = clr;
                view.Columns[nIndex].AppearanceHeader.Font = new Font("Verdana", 14F, FontStyle.Bold);
                view.Columns[nIndex].AppearanceCell.Font = new Font("Verdana", 12F, FontStyle.Bold);

                if (nIndex == 0) nWidth = 90;
                else nWidth = 159;
                view.Columns[nIndex].Width = nWidth;
            }

            view.Columns[nResultColumn].Visible = false;
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
            var strValue = view.GetRowCellValue(e.RowHandle, view.Columns[nResultColumn]).ToString();
            int cellValue = util.TryParseInt(strValue, 0);

            /// 여러 컬럼에 적용
            if (strValue == "-") return;

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
        private void gridView1_CustomDrawRowPreview(object sender, DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventArgs e)
        {
            int rowNumber = e.RowHandle + 1;
            if (rowNumber % SEPERATOR == 0)
            {
                e.Cache.FillRectangle(e.Cache.GetSolidBrush(Color.Black), e.Bounds);
                e.Handled = true;
            }
        }

        private void gridView1_MeasurePreviewHeight(object sender, RowHeightEventArgs e)
        {
            int rowNumber = e.RowHandle + 1;
            if (rowNumber % SEPERATOR == 0)
            {
                e.RowHeight = 1;
            }
            else
            {
                e.RowHeight = 0;
            }
        }
        #endregion Make Grid View

        #region Devexpress Grid Control
        private void gridView1_CellMerge(object sender, CellMergeEventArgs e)
        {
            GridView view = sender as GridView;
            if (view == null) return;
            if (e.Column.FieldName == "CHANNEL")
            {
                string text1 = view.GetRowCellDisplayText(e.RowHandle1, "CHANNEL");
                string text2 = view.GetRowCellDisplayText(e.RowHandle2, "CHANNEL");
                e.Merge = (text1 == text2);
                e.Handled = true;
            }
            else
            {
                e.Merge = false;
                e.Handled = true;
            }
        }

        private void gvLeft_Load(object sender, EventArgs e)
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
        }

        
        #endregion Devexpress Grid Control

        #region Get Mon Data Timer
        private void SetValueToGrid(KeysightMonData mondata, ControllerSenData sendata)
        {
            GridControl gc = null;
            int nRow = 0;
            string strResult = string.Empty;
            try
            {
                for (int nIndex = 0; nIndex < _Constant.ChannelCount; nIndex++)
                {
                    /// gvLeft : 1 ~ _Constant.ChannelCount / 2
                    /// gvRight : _Constant.ChannelCount / 2 + 1 ~ _Constant.Channelcount
                    if (nIndex < _Constant.ChannelCount / 2) {
                        gc = gvLeft;
                        nRow = nIndex;
                    }
                    else {
                        gc = gvRight;
                        nRow = nIndex - _Constant.ChannelCount / 2;
                    }

                    //* Status
                    string sMon = string.Empty;
                    sMon = mondata.CHANNELSTATUS[nIndex].ToString();
                    int nStatus = util.TryParseInt(sMon, -1);
                    if (nStatus > 0) strResult = "0"; // OK
                    else strResult = "2";   // NG
                    SetValueToGrid(gc, nRow, nResultColumn, "1");

                    //* Voltage Value
                    string vMon = string.Empty;
                    vMon = (mondata.CHANNELVOLTAGE[nIndex] * 1000).ToString("F2");
                    SetValueToGrid(gc, nRow, 1, vMon);

                    //* Current Value
                    string iMon = string.Empty;
                    iMon = (mondata.CHANNELCURRENT[nIndex] * 1000).ToString("F1");
                    SetValueToGrid(gc, nRow, 2, iMon);

                    //* CAPA Value
                    string cMon = string.Empty;
                    cMon = (mondata.CHANNELCAPACITY[nIndex] * 1000).ToString("F1");
                    SetValueToGrid(gc, nRow, 3, cMon);

                    //* TEMPERATURE Value
                    string tMon = string.Empty;
                    if (sendata.TEMPERATURE.Count() == 0 || sendata.TEMPERATURE[nIndex] == null) tMon = "0.0";
                    else tMon = sendata.TEMPERATURE[nIndex].ToString();
                    SetValueToGrid(gc, nRow, 4, tMon);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        private void GetMonDataTimer_TickAsync(object sender, EventArgs e)
        {
            try
            {
                if (bVisible == true)
                {
                    int nStageNo;
                    nStageNo = util.TryParseInt(cbStageName.Text.Split(' ')[1], 0);
                    if (nStageNo > 0)
                    {
                        //* 현재 선택된 MON DATA
                        KeysightMonData mondata = util.ReadMonData(nStageno, nTrayInfo[nStageno]);// mariadb.GETMONDATAFORCAPACITY(nStageNo);
                        //ControllerSenData nSenData = mariadb.GETSENDATA(nStageNo);
                        ControllerSenData nSenData = null;

                        //* Recipe 정보 표시
                        List<Recipe> recipes = nTrayInfo[nStageNo - 1].RECIPE;
                        if (recipes != null && recipes.Count > 0)
                        {
                            lblRecipe.Text = "NO., Type, Current, Voltage, Time" + Environment.NewLine;
                            for (int nIndex = 0; nIndex < recipes.Count; nIndex++)
                            {
                                lblRecipe.Text += recipes[nIndex].orderno + ", " + recipes[nIndex].recipemethod
                                    + ", " + recipes[nIndex].current + ", " + recipes[nIndex].voltage
                                    + ", " + recipes[nIndex].time + Environment.NewLine;
                            }
                        }

                        if (mondata == null) return;

                        //* grid control에 값 입력으로 수정해야 함
                        SetValueToLabel(mondata, nSenData);

                        //* STEP TIME
                        util.SetValueToLabel(lblStepTime, ConvertMsTimeToString(mondata.STAGETIME));
                    }
                }

                //SaveMonData();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        
        public string ConvertMsTimeToString(ulong time)
        {
            string strTime = string.Empty;
            TimeSpan ts = TimeSpan.FromMilliseconds(time);
            strTime = ts.ToString(@"hh\:mm\:ss\.fff");
            return strTime;
        }
        #endregion

        #region Equipment Status
        public void SetSenStatus(ControllerSenData[] sendata)
        {
            int stageno;
            if (bVisible == true)
            {
                stageno = util.TryParseInt(cbStageName.Text.Split(' ')[1], 0) - 1;
                if (stageno >= 0 && sendata[stageno] != null)
                    util.SetValueToLabel(lblStepStatus, sendata[stageno].SENSTATUS.ToString());
            }
        }
        #endregion

        private void btnFinData_Click(object sender, EventArgs e)
        {
            int stageno;
            stageno = util.TryParseInt(cbStageName.Text.Split(' ')[1], 0) - 1;
            RaiseOnSaveFinData(stageno);
        }

        private void cbStageName_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
    }
}
