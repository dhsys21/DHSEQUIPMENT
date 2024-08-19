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

            #region Label 이용
            /// Label 그리기
            //int width = 120;
            //int height = 30;
            //int startx = 0, starty = 0;

            //makeHeaderLabel(pBase, width, height);
            //makeLabel(pBase, width, height, startx, starty + height);
            //initLabel();
            #endregion

            /// column header color
            //initGridView(gvLeft, gridView1);

            //makeGridViewGroup(gvLeft, _Constant.ChannelCount / 2);
            makeGridView(gvLeft, _Constant.ChannelCount / 2);

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
        public void initGridView(RadGridView gv)
        {
            gv.AllowColumnResize = false;
            gv.TableElement.TableHeaderHeight = 40;

            gv.MasterTemplate.AllowAddNewRow = false;
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
            dt.Columns.Add("CHANNEL");
            dt.Columns.Add("전압");
            dt.Columns.Add("전류");
            dt.Columns.Add("용량");
            dt.Columns.Add("CD");

            string channel = string.Empty;
            for (int i = 0; i < rowCount; i++)
            {
                channel = (i + 1).ToString();
                dt.Rows.Add(new string[] { channel, "-", "-", "-", "-"});
            }

            gc.DataSource = dt;
        }
        private void makeGridView_IROCV(GridControl gc, int rowCount)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("CHANNEL");
            dt.Columns.Add("전압");
            dt.Columns.Add("전류");
            dt.Columns.Add("용량");
            dt.Columns.Add("CD");

            string channel = string.Empty;
            for(int i = 0; i < rowCount; i++)
            {
                channel = (i + 1).ToString();
                dt.Rows.Add(new string[] { channel, i.ToString() , (i + 1).ToString(), (i+2).ToString(), (i+3).ToString()});
                dt.Rows.Add(new string[] { channel, i.ToString(), (i + 2).ToString(), (i + 3).ToString(), (i + 1).ToString() });
            }

            gc.DataSource = dt;
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

        private void gridView1_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.Column != view.Columns["CHANNEL"])
                return;
            e.Appearance.BackColor = _Constant.ColorReady;
        }
        #endregion Devexpress Grid Control

        #region Get Mon Data Timer
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
                        //KeysightMonData mondata = mariadb.GETMONDATAFORCAPACITY(nStageNo);
                        //ControllerSenData nSenData = mariadb.GETSENDATA(nStageNo);
                        KeysightMonData mondata = null;
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
                        //SetValueToLabel(mondata, nSenData);

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
