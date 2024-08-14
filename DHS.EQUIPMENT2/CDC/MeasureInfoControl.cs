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
        //private Label[] lblCH = new Label[_Constant.ChannelCount];
        //private Label[] lblVOLT = new Label[_Constant.ChannelCount];
        //private Label[] lblCURR = new Label[_Constant.ChannelCount];
        //private Label[] lblCAPA = new Label[_Constant.ChannelCount];
        //private Label[] lblTEMP = new Label[_Constant.ChannelCount];
        //private Label[] lblCD = new Label[_Constant.ChannelCount];

        private DoubleBufferedLabel[] lblCH = new DoubleBufferedLabel[_Constant.ChannelCount];
        private DoubleBufferedLabel[] lblVOLT = new DoubleBufferedLabel[_Constant.ChannelCount];
        private DoubleBufferedLabel[] lblCURR = new DoubleBufferedLabel[_Constant.ChannelCount];
        private DoubleBufferedLabel[] lblCAPA = new DoubleBufferedLabel[_Constant.ChannelCount];
        private DoubleBufferedLabel[] lblTEMP = new DoubleBufferedLabel[_Constant.ChannelCount];
        private DoubleBufferedLabel[] lblCD = new DoubleBufferedLabel[_Constant.ChannelCount];

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

            /// Label 그리기
            //int width = 120;
            //int height = 30;
            //int startx = 0, starty = 0;

            //makeHeaderLabel(pBase, width, height);
            //makeLabel(pBase, width, height, startx, starty + height);
            //initLabel();

            /// column header color
            //initGridView(gvLeft, gridView1);
            initGridView(gvRight);
            //makeGridViewGroup(gvLeft, _Constant.ChannelCount / 2);
            makeGridView(gvLeft, _Constant.ChannelCount / 2);
            makeGridView(gvRight, _Constant.ChannelCount / 2);

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

        #region Make LABEL
        private void makeHeaderLabel(Panel basepanel, int width, int height)
        {
            if (_Constant.ChannelCount == 64) makeHeaderLabel64(basepanel, width, height);
            else if (_Constant.ChannelCount == 256) makeHeaderLabel256(basepanel, width, height);
        }
        private void makeHeaderLabel64(Panel basepanel, int width, int height)
        {
            Label lbl;
            int xPos = 0;
            int yPos = 0;

            //* 첫번째 더미
            lbl = CustomHeaderLabel(width, height, yPos, xPos, string.Empty);
            basepanel.Controls.Add(lbl);

            //* 가로
            for (int nIndex = 0; nIndex < 8;)
            {
                lbl = CustomHeaderLabel(width, height, yPos, xPos + width, (nIndex + 1).ToString());
                basepanel.Controls.Add(lbl);

                nIndex += 1;
                xPos += width;
                if (nIndex % 2 == 0)
                    xPos += 3;
            }

            //* 세로
            xPos = 0;
            yPos = 0;
            for (int nIndex = 0; nIndex < 8;)
            {
                lbl = CustomHeaderLabel(width, height, yPos + height, xPos, (nIndex + 1).ToString());
                basepanel.Controls.Add(lbl);

                nIndex += 1;
                yPos += height;
                if (nIndex % 2 == 0)
                    yPos += 2;
            }
        }
        private void makeHeaderLabel256(Panel basepanel, int width, int height)
        {
            Label lbl;
            int xPos = 0;
            int yPos = 0;

            //* TITLE
            for (int nIndex = 0; nIndex < 12;)
            {
                lbl = CustomHeaderLabel(width, height, yPos, xPos, "");
                SetTitle(nIndex, lbl);
                basepanel.Controls.Add(lbl);

                nIndex += 1;
                xPos += width;

                if (nIndex % 6 == 0)
                    xPos += 20;
            }
        }
        private void SetTitle(int nIndex, Label lbl)
        {
            switch (nIndex)
            {
                case 0:
                case 6:
                    lbl.Text = "CH";
                    break;
                case 1:
                case 7:
                    lbl.Text = "전압[mV]";
                    break;
                case 2:
                case 8:
                    lbl.Text = "전류[mA]";
                    break;
                case 3:
                case 9:
                    lbl.Text = "용량[mAh]";
                    break;
                case 4:
                case 10:
                    lbl.Text = "셀온도[℃]";
                    break;
                case 5:
                case 11:
                    lbl.Text = "CD";
                    break;
            }
        }
        private void makeLabel(Panel basepanel, int width, int height, int startXPos, int startYPos)
        {
            if (_Constant.ChannelCount == 64) makeLabel64(basepanel, width, height, startXPos, startYPos);
            else if (_Constant.ChannelCount == 256) makeLabel256(basepanel, width, height, startXPos, startYPos);
        }
        private void makeLabel64(Panel basepanel, int width, int height, int startXPos, int startYPos)
        {
            int xPos = startXPos;
            int yPos = startYPos;
            string value = string.Empty;

            for (int nIndex = 0; nIndex < _Constant.ChannelCount;)
            {
                value = (nIndex + 1).ToString();
                lblVOLT[nIndex] = CustomLabel(width, height, yPos, xPos, _Constant.ColorVoltage, value);
                basepanel.Controls.Add(lblVOLT[nIndex]);

                value = (nIndex / 8 + 1).ToString() + " - " + (nIndex % 8 + 1).ToString();
                lblCURR[nIndex] = CustomLabel(width, height, yPos + height, xPos, _Constant.ColorCurrent, value);
                basepanel.Controls.Add(lblCURR[nIndex]);

                value = " - ";
                lblCD[nIndex] = CustomLabel(width, height, yPos + height * 2, xPos, _Constant.colorStatus, value);
                basepanel.Controls.Add(lblCD[nIndex]);

                nIndex += 1;
                xPos += width;

                //* 가로 4번째 마다 3씩 띄우기
                if (nIndex % 2 == 0) xPos += 3;

                //* 9번째 마다 한줄 밑으로 이동
                if (nIndex % 8 == 0)
                {
                    xPos = startXPos;
                    yPos += height * 3;
                    if ((nIndex / 8) % 2 == 0) yPos += 2;
                }

            }
        }
        private void makeLabel256(Panel basepanel, int width, int height, int startXPos, int startYPos)
        {
            int xPos = startXPos;
            int yPos = startYPos;
            string value = string.Empty;

            for (int nIndex = 0; nIndex < _Constant.ChannelCount;)
            {
                value = (nIndex + 1).ToString();
                lblCH[nIndex] = CustomLabel(width, height, yPos, xPos, _Constant.ColorReady, value);
                basepanel.Controls.Add(lblCH[nIndex]);

                value = (nIndex / 16 + 1) + " - " + (nIndex % 16 + 1);
                lblVOLT[nIndex] = CustomLabel(width, height, yPos, xPos + width, Color.White, value);
                basepanel.Controls.Add(lblVOLT[nIndex]);

                value = " - ";
                lblCURR[nIndex] = CustomLabel(width, height, yPos, xPos + width * 2, Color.White, value);
                basepanel.Controls.Add(lblCURR[nIndex]);

                value = " - ";
                lblCAPA[nIndex] = CustomLabel(width, height, yPos, xPos + width * 3, Color.White, value);
                basepanel.Controls.Add(lblCAPA[nIndex]);

                value = " - ";
                lblTEMP[nIndex] = CustomLabel(width, height, yPos, xPos + width * 4, Color.White, value);
                basepanel.Controls.Add(lblTEMP[nIndex]);

                value = " - ";
                lblCD[nIndex] = CustomLabel(width, height, yPos, xPos + width * 5, Color.White, value);
                basepanel.Controls.Add(lblCD[nIndex]);

                nIndex += 1;
                yPos += height;

                if (nIndex >= 128)
                    xPos = (width * 6) + 20;
                else
                    xPos = startXPos;

                if (nIndex % 128 == 0)
                    yPos = startYPos;
            }
        }
        private Label CustomHeaderLabel(int width, int height, int top, int left, string value)
        {
            Label lbl = new Label();
            lbl.Width = width;
            lbl.Height = height;
            lbl.Top = top;
            lbl.Left = left;
            lbl.BackColor = Color.DarkGray;
            lbl.BorderStyle = BorderStyle.FixedSingle;
            lbl.Font = new Font("Verdana", 12, FontStyle.Bold);
            lbl.TextAlign = ContentAlignment.MiddleCenter;
            lbl.Text = value;

            return lbl;
        }
        private DoubleBufferedLabel CustomLabel(int width, int height, int top, int left, Color clr, string value)
        {
            DoubleBufferedLabel lbl = new DoubleBufferedLabel();
            lbl.Width = width;
            lbl.Height = height;
            lbl.Top = top;
            lbl.Left = left;
            lbl.BackColor = clr;
            lbl.BorderStyle = BorderStyle.FixedSingle;
            lbl.Font = new Font("Verdana", 10, FontStyle.Bold);
            lbl.TextAlign = ContentAlignment.MiddleCenter;
            lbl.Text = value;
            return lbl;
        }
        private void initLabel()
        {
            if (_Constant.ChannelCount == 64) initLabel64(8);
            else if (_Constant.ChannelCount == 256) initLabel256();
            else if (_Constant.ChannelCount == 400) initLabel64(20);
        }
        private void initLabel64(int rowcount)
        {
            string value = string.Empty;
            for (int nIndex = 0; nIndex < _Constant.ChannelCount; nIndex++)
            {
                value = (nIndex + 1).ToString();
                util.SetValueToLabel(lblVOLT[nIndex], value, _Constant.ColorVoltage);

                value = (nIndex / rowcount) + 1 + " - " + (nIndex % rowcount) + 1;
                util.SetValueToLabel(lblCURR[nIndex], value, _Constant.ColorCurrent);

                value = " - ";
                util.SetValueToLabel(lblCD[nIndex], value, _Constant.colorStatus);
            }
        }
        private void initLabel256()
        {
            string value = string.Empty;
            for (int nIndex = 0; nIndex < _Constant.ChannelCount; nIndex++)
            {
                value = (nIndex / 16 + 1) + " - " + (nIndex % 16 + 1);
                util.SetValueToLabel(lblVOLT[nIndex], value, Color.White);

                value = " - ";
                util.SetValueToLabel(lblCURR[nIndex], value, Color.White);

                value = " - ";
                util.SetValueToLabel(lblCAPA[nIndex], value, Color.White);

                value = " - ";
                util.SetValueToLabel(lblTEMP[nIndex], value, Color.White);

                value = " - ";
                util.SetValueToLabel(lblCD[nIndex], value, Color.White);
            }
        }
        private void SetValueToLabel(KeysightMonData mondata, ControllerSenData sendata)
        {
            try
            {
                for (int nIndex = 0; nIndex < _Constant.ChannelCount; nIndex++)
                {
                    //* Status
                    string sMon = string.Empty;
                    sMon = mondata.CHANNELSTATUS[nIndex].ToString();
                    //util.SetValueToLabel(lblSTATUS[nIndex], sMon);
                    SetValueWithColor(enumValueType.STATUS, lblCD[nIndex], sMon, sMon);

                    //* Voltage Value
                    string vMon = string.Empty;
                    vMon = (mondata.CHANNELVOLTAGE[nIndex] * 1000).ToString("F2");
                    //util.SetValueToLabel(lblVOLT[nIndex], vMon);
                    SetValueWithColor(enumValueType.VOLTAGE, lblVOLT[nIndex], sMon, vMon);

                    //* Current Value
                    string iMon = string.Empty;
                    iMon = (mondata.CHANNELCURRENT[nIndex] * 1000).ToString("F1");
                    //util.SetValueToLabel(lblCURR[nIndex], iMon);
                    SetValueWithColor(enumValueType.CURRENT, lblCURR[nIndex], sMon, iMon);

                    //* CAPA Value
                    string cMon = string.Empty;
                    cMon = (mondata.CHANNELCAPACITY[nIndex] * 1000).ToString("F1");
                    //util.SetValueToLabel(lblCAPA[nIndex], cMon);
                    SetValueWithColor(enumValueType.CURRENT, lblCAPA[nIndex], sMon, cMon);

                    //* TEMPERATURE Value
                    string tMon = string.Empty;
                    if (sendata.TEMPERATURE.Count() == 0 || sendata.TEMPERATURE[nIndex] == null) tMon = "0.0";
                    else tMon = sendata.TEMPERATURE[nIndex].ToString();
                    //util.SetValueToLabel(lblTEMP[nIndex], tMon);
                    SetValueWithColor(enumValueType.CURRENT, lblTEMP[nIndex], sMon, tMon);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        private void SetValueWithColor(enumValueType enumType, Label lbl, string status, string value)
        {
            Color clr = Color.Transparent;
            int nStatus = util.TryParseInt(status, -1);

            switch (enumType)
            {
                case enumValueType.VOLTAGE:
                    if (nStatus > 0) clr = _Constant.ColorVoltage;
                    else clr = _Constant.ColorFail;
                    break;
                case enumValueType.CURRENT:
                    if (nStatus > 0) clr = _Constant.ColorCurrent;
                    else clr = _Constant.ColorFail;
                    break;
                case enumValueType.STATUS:
                    if (nStatus > 0) clr = _Constant.colorStatus;
                    else clr = _Constant.ColorFail;
                    break;
                default:
                    break;
            }

            util.SetValueToLabel(lbl, value, clr);
        }
        #endregion Make LABEL

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
        private void makeGridView2(GridControl gc, int rowCount)
        {
            try
            {
                GridView gv = gvLeft.MainView as GridView;

                for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
                {
                    gv.AddNewRow();
                    gv.AddNewRow();
                }
            }catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        private void makeGridView(RadGridView gv, int rowCount)
        {
            try
            {
                gv.Rows.Clear();

                for(int rowIndex = 0;  rowIndex < rowCount; rowIndex++)
                {
                    if(gv.Tag.ToString() == "Left")
                        gv.Rows.Add((rowIndex + 1), "-", "-", "-", "-", "-");
                    else if(gv.Tag.ToString() == "Right")
                        gv.Rows.Add((rowCount + rowIndex + 1), "-", "-", "-", "-", "-");

                    gv.Rows[rowIndex].Height = 40;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        HtmlViewDefinition htmlView;
        ColumnGroupsViewDefinition columnGroupsView;
        private void makeGridViewGroup(RadGridView gv, int rowCount)
        {
            try
            {

                // column groups view
                this.columnGroupsView = new ColumnGroupsViewDefinition();
                this.columnGroupsView.ColumnGroups.Add(new GridViewColumnGroup("General"));
                this.columnGroupsView.ColumnGroups.Add(new GridViewColumnGroup("Details"));
                this.columnGroupsView.ColumnGroups[1].Groups.Add(new GridViewColumnGroup("Address"));
                this.columnGroupsView.ColumnGroups[1].Groups.Add(new GridViewColumnGroup());
                GridViewColumnGroupRow groupRow = new GridViewColumnGroupRow();
                groupRow.MinHeight = 50;
                this.columnGroupsView.ColumnGroups[0].Rows.Add(groupRow);
                groupRow = new GridViewColumnGroupRow();
                groupRow.MinHeight = 50;
                this.columnGroupsView.ColumnGroups[0].Rows.Add(groupRow);
                this.columnGroupsView.ColumnGroups[0].Rows[0].ColumnNames.Add("CustomerID");
                this.columnGroupsView.ColumnGroups[0].Rows[0].ColumnNames.Add("ContactName");
                this.columnGroupsView.ColumnGroups[0].Rows[1].ColumnNames.Add("CompanyName");
                groupRow = new GridViewColumnGroupRow();
                groupRow.MinHeight = 50;
                this.columnGroupsView.ColumnGroups[1].Groups[0].Rows.Add(groupRow);
                this.columnGroupsView.ColumnGroups[1].Groups[0].Rows[0].ColumnNames.Add("City");
                this.columnGroupsView.ColumnGroups[1].Groups[0].Rows[0].ColumnNames.Add("Country");
                groupRow = new GridViewColumnGroupRow();
                groupRow.MinHeight = 50;
                this.columnGroupsView.ColumnGroups[1].Groups[1].Rows.Add(groupRow);
                this.columnGroupsView.ColumnGroups[1].Groups[1].Rows[0].ColumnNames.Add("Phone");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        public void SetValueToGridView(KeysightMonData mondata, int maxChannel)
        {
            try
            {
                int nIdx;
                RadGridView gv = new RadGridView();
                for (int nIndex = 0; nIndex < maxChannel; nIndex++)
                {
                    gv = gvRight;
                    nIdx = nIndex - maxChannel / 2;

                    //* Voltage Value and Color
                    string vMon = string.Empty;
                    vMon = (mondata.CHANNELVOLTAGE[nIndex] * 1000).ToString("F2");
                    util.SetValueToGridView(vMon, nIdx, 1, gv);
                    util.SetColorToGridView(_Constant.ColorVoltage, nIdx, 1, gv);

                    //* Current Value and Color
                    string iMon = string.Empty;
                    iMon = (mondata.CHANNELCURRENT[nIdx] * 1000).ToString("F1");
                    util.SetValueToGridView(iMon, nIdx, 2, gv);
                    util.SetColorToGridView(_Constant.ColorCurrent, nIdx, 2, gv);

                    //* Capacity Value and Color
                    string cMon = string.Empty;
                    cMon = (mondata.CHANNELCAPACITY[nIdx] * 1000).ToString("F1");
                    util.SetValueToGridView(cMon, nIdx, 3, gv);
                    util.SetColorToGridView(_Constant.ColorCapacity, nIdx, 3, gv);

                    //* Temperature value and Color
                    string tMon = string.Empty;
                    util.SetValueToGridView(tMon, nIdx, 4, gv);
                    util.SetColorToGridView(_Constant.ColorCapacity, nIdx, 4, gv);

                    //* CD
                    string cd = string.Empty;
                    util.SetValueToGridView(tMon, nIdx, 5, gv);
                    util.SetColorToGridView(_Constant.ColorCapacity, nIdx, 5, gv);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        #endregion Make Grid View

        #region Rad Grid View Control Event
        private void gvLeft_ViewCellFormatting(object sender, CellFormattingEventArgs e)
        {
            if (e.CellElement is GridHeaderCellElement)
            {
                e.CellElement.DrawBorder = true;
                e.CellElement.DrawFill = true;
                e.CellElement.GradientStyle = GradientStyles.Linear;
                e.CellElement.BackColor = Color.DarkGray;
                e.CellElement.Font = new Font("Verdana", 12, FontStyle.Bold);
            }
            else
            {
                e.CellElement.DrawBorder = true;
                e.CellElement.DrawFill = true;

                if ((e.CellElement.RowIndex + 1) % 10 == 0)
                    e.CellElement.BorderBottomWidth = 4;
                else
                    e.CellElement.BorderBottomWidth = 2;
                e.CellElement.BorderBottomColor = Color.Black;

                e.CellElement.BorderRightWidth = 2;
                e.CellElement.BorderRightColor = Color.DarkGray;

                e.CellElement.TextAlignment = ContentAlignment.MiddleCenter;
                e.CellElement.Font = new Font("Verdana", 11, FontStyle.Bold);

                if(e.CellElement.ColumnInfo.HeaderText.Contains("Channel"))
                    e.CellElement.BackColor = _Constant.ColorReady;

                //if (e.CellElement.ColumnInfo.HeaderText.Contains("mV"))
                //    e.CellElement.BackColor = Color.White;
                //else if (e.CellElement.ColumnInfo.HeaderText.Contains("mAh"))
                //    e.CellElement.BackColor = Color.White;
                //else if (e.CellElement.ColumnInfo.HeaderText.Contains("mA"))
                //    e.CellElement.BackColor = Color.White;
            }
        }
        #endregion Rad Grid View Control Event

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
            initLabel();
        }

        
    }
}
