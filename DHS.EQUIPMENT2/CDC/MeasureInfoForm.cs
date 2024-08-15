using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DHS.EQUIPMENT2.Common;

namespace DHS.EQUIPMENT2
{
    public partial class MeasureInfoForm : Form
    {
        Util util = new Util();
        MariaDB mariadb = null;

        TRAYINFO[] nTrayInfo = new TRAYINFO[_Constant.ControllerCount];
        private Label[] lblCH = new Label[_Constant.ChannelCount];
        private Label[] lblVOLT = new Label[_Constant.ChannelCount];
        private Label[] lblCURR = new Label[_Constant.ChannelCount];
        private Label[] lblCAPA = new Label[_Constant.ChannelCount];
        private Label[] lblTEMP = new Label[_Constant.ChannelCount];
        private Label[] lblCD = new Label[_Constant.ChannelCount];

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

        private static MeasureInfoForm measureinfoForm = new MeasureInfoForm();
        public static MeasureInfoForm GetInstance()
        {
            if (measureinfoForm == null) measureinfoForm = new MeasureInfoForm();
            return measureinfoForm;
        }
        public MeasureInfoForm()
        {
            InitializeComponent();

            for (int nIndex = 0; nIndex < _Constant.ControllerCount; nIndex++)
                nTrayInfo[nIndex] = TRAYINFO.GetInstance(nIndex);

            //* Label 그리기
            int width = 120;
            int height = 30;
            int startx = 0, starty = 0;
            makeHeaderLabel(pBase, width, height);
            makeLabel(pBase, width, height, startx, starty + height);
            initLabel();

            mariadb = MariaDB.GetInstance();

            //* Get Mon Data Timer
            _tmrGetMon = new Timer();
            _tmrGetMon.Interval = 1000;
            _tmrGetMon.Tick += new EventHandler(GetMonDataTimer_TickAsync);
            //_tmrGetMon.Enabled = true;
        }
        public void SetStage(int stageno)
        {
            nStageno = stageno;
        }
        public void SetVisible(bool isVisibled)
        {
            bVisible = isVisibled;
        }

        #region 64채널 LABEL / DaTA GRID VIEW
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
            for (int nIndex = 0; nIndex < 8; )
            {
                lbl = CustomHeaderLabel(width, height, yPos, xPos + width, (nIndex+1).ToString());
                basepanel.Controls.Add(lbl);

                nIndex += 1;
                xPos += width;
                if (nIndex % 2 == 0)
                    xPos += 3;
            }

            //* 세로
            xPos = 0;
            yPos = 0;
            for (int nIndex = 0; nIndex < 8; )
            {
                lbl = CustomHeaderLabel(width, height, yPos + height, xPos, (nIndex+1).ToString());
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
            switch(nIndex)
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
                if(nIndex % 8 == 0)
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
        private Label CustomLabel(int width, int height, int top, int left, Color clr, string value)
        {
            Label lbl = new Label();
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
            for(int nIndex = 0; nIndex < _Constant.ChannelCount; nIndex++)
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
                for(int nIndex = 0; nIndex < _Constant.ChannelCount; nIndex++)
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
            }catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        private void SetValueWithColor(enumValueType enumType, Label lbl, string status, string value)
        {
            Color clr = Color.Transparent;
            int nStatus = util.TryParseInt(status, -1);

            switch(enumType)
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
        private void makeGridView64(DataGridView dgv)
        {
            try
            {
                #region 행/열 제목, 갯수
                for (int nIndex = 0; nIndex < 8; nIndex++)
                {
                    dgv.Columns.Add("CH" + nIndex.ToString("D2"), (nIndex + 1).ToString("D2"));
                    dgv.Columns[nIndex].Width = 120;
                    dgv.Columns[nIndex].SortMode = DataGridViewColumnSortMode.NotSortable;
                }

                dgv.Rows.Clear();
                dgv.Rows.Add(16); // Voltage + Current => 8 * 2
                dgv.RowHeadersWidth = 80;
                dgv.ColumnHeadersHeight = 40;
                //gridView.RowTemplate.Height = 30;

                for (int i = 0; i < 8; i++)
                {
                    dgv.Rows[i * 2].HeaderCell.Value = (i + 1).ToString();
                    dgv.Rows[i * 2 + 1].HeaderCell.Value = "-";
                }
                #endregion
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        public void initGridView64(DataGridView dgv)
        {
            dgv.RowsDefaultCellStyle.BackColor = _Constant.ColorVoltage;
            dgv.AlternatingRowsDefaultCellStyle.BackColor = _Constant.ColorCurrent;
            dgv.ClearSelection();
            dgv.DefaultCellStyle.Font = new Font("Times New Roman", 10);
            try
            {
                int nRow, nCol, nIdx;
                for (int nRowIndex = 0; nRowIndex < 8; nRowIndex++)
                {
                    for (int nColIndex = 0; nColIndex < 8; nColIndex++)
                    {
                        //nCol = nColIndex;
                        //nRow = nRowIndex * 2;
                        nIdx = nRowIndex * 8 + nColIndex;

                        util.ChangeMapToGridView64(enumStartPosition.LEFTTOPTORIGHT, nIdx, out nRow, out nCol);
                        nRow = nRow * 2;

                        /// Voltage
                        dgv.Rows[nRow].Cells[nCol].Value = (nIdx + 1).ToString();
                        dgv.Rows[nRow].Cells[nCol].Style.BackColor = _Constant.ColorVoltage;
                        /// Current
                        dgv.Rows[nRow + 1].Cells[nCol].Value = (nIdx + 8) / 8 + " - " + ((nIdx % 8) + 1);
                        dgv.Rows[nRow + 1].Cells[nCol].Style.BackColor = _Constant.ColorCurrent;
                        /// Divider line
                        dgv.Rows[nRow + 1].DividerHeight = 2;

                        /// Row Height
                        dgv.Rows[nRow].Height = 48;
                        dgv.Rows[nRow + 1].Height = 48;
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        public void SetValueToGridView64(DataGridView dgv, KeysightMonData mondata, int maxChannel)
        {
            try
            {
                int nRow, nCol, nIdx;
                for (int nRowIndex = 0; nRowIndex < 8; nRowIndex++)
                {
                    for (int nColIndex = 0; nColIndex < 8; nColIndex++)
                    {
                        //nCol = nColIndex;
                        //nRow = nRowIndex * 2;
                        nIdx = nRowIndex * 8 + nColIndex;

                        util.ChangeMapToGridView64(enumStartPosition.LEFTTOPTORIGHT, nIdx, out nRow, out nCol);
                        nRow = nRow * 2;

                        //* Voltage Value
                        string vMon = string.Empty;
                        vMon = (mondata.CHANNELVOLTAGE[nIdx] * 1000).ToString("F2");
                        util.SetValueToGridView(vMon, nRow, nCol, dgv);
                        
                        //* Voltage Color
                        util.SetColorToGridView(_Constant.ColorVoltage, nRow, nCol, dgv);

                        //* Current Value
                        string iMon = string.Empty;
                        iMon = (mondata.CHANNELCURRENT[nIdx] * 1000).ToString("F1");
                        util.SetValueToGridView(iMon, nRow + 1, nCol, dgv);
                        
                        //* Current Color
                        util.SetColorToGridView(_Constant.ColorCurrent, nRow + 1, nCol, dgv);


                        //* Divider line
                        dgv.Rows[nRow + 1].DividerHeight = 2;

                        /// Row Height
                        dgv.Rows[nRow].Height = 48;
                        dgv.Rows[nRow + 1].Height = 48;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        #endregion

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
                        KeysightMonData mondata = mariadb.GETMONDATAFORCAPACITY(nStageNo);
                        ControllerSenData nSenData = mariadb.GETSENDATA(nStageNo);

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
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        private async Task SaveMonDataAsync()
        {
            for (int nIndex = 0; nIndex < _Constant.ControllerCount; nIndex++)
            {
                if (nTrayInfo[nIndex].CHARGING == true)
                {
                    KeysightMonData mondata = await Task.Run(() => mariadb.GETMONDATAAsync((nIndex + 1)));
                    if (mondata == null) return;
                    util.SaveMonData3((nIndex + 1), mondata, nTrayInfo[nIndex]);
                }
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
                if(stageno >= 0 && sendata[stageno] != null)
                    util.SetValueToLabel(lblStepStatus, sendata[stageno].SENSTATUS.ToString());
            }
        }
        #endregion

        private void MeasureInfoForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.bVisible = false;
        }

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
