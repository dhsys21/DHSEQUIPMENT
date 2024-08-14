using DevExpress.XtraCharts;
using DHS.EQUIPMENT2.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Telerik.WinControls.NativeMethods;
using Util = DHS.EQUIPMENT2.Common.Util;

namespace DHS.EQUIPMENT2
{
    public partial class MeasureChartControl : UserControl
    {
        CheckBox[] chkChannel = new CheckBox[_Constant.ChannelCount];
        List<DataValue>[] nDataValue = new List<DataValue>[_Constant.ChannelCount];
        Util util = new Util();
        List<FILEINFO> nFileInfo = new List<FILEINFO>();

        private static MeasureChartControl measureChartControl = new MeasureChartControl();

        public static MeasureChartControl GetInstance()
        {
            if (measureChartControl == null) measureChartControl = new MeasureChartControl();
            return measureChartControl;
        }
        public MeasureChartControl()
        {
            InitializeComponent();

            //* make checkbox
            MakeChannelCheckBox();
        }

        private void MakeChannelCheckBox()
        {
            int xPos = 160, yPos = 5;
            int width = 80, height = 22;

            for (int nIndex = 0; nIndex < _Constant.ChannelCount;)
            {
                chkChannel[nIndex] = new CheckBox();
                chkChannel[nIndex].Name = "chkChannel" + (nIndex + 1).ToString();
                chkChannel[nIndex].Tag = nIndex;
                chkChannel[nIndex].Text = "CH" + (nIndex + 1).ToString();
                chkChannel[nIndex].Width = width;
                chkChannel[nIndex].Height = height;
                chkChannel[nIndex].Left = xPos;
                chkChannel[nIndex].Top = yPos;
                chkChannel[nIndex].Checked = true;
                chkChannel[nIndex].CheckedChanged += chkChannelArray_CheckedChanged;
                pnlFooter.Controls.Add(chkChannel[nIndex]);

                nIndex += 1;

                xPos = xPos + width + 5;
                if (nIndex % 16 == 0)
                {
                    xPos = 160;
                    yPos = yPos + height + 5;
                }
            }
        }

        private void LoadTrayId(string stagename)
        {
            string dir = _Constant.DATA_PATH;

            try
            {
                //* 날짜 폴더
                string[] date_dirs = util.GetDirList(dir);
                //for (int nIndex = 0; nIndex < date_dirs.Length; nIndex++)
                for (int nIndex = date_dirs.Length - 1; nIndex >= 0; nIndex--)
                {
                    //* 스테이지 리스트 폴더
                    string[] stage_dirs = util.GetDirList(date_dirs[nIndex]);
                    FILEINFO fiDateDir = new FILEINFO();
                    fiDateDir.NameOnly = date_dirs[nIndex];
                    fiDateDir.FullName = "DATE DIRECTORY";
                    nFileInfo.Add(fiDateDir);
                    foreach (string dirname in stage_dirs)
                    {
                        //if (dirname == stagename)
                        if (dirname.Contains(stagename))
                        {
                            List<FILEINFO> fiList = util.GetFileList(dirname);

                            foreach (FILEINFO fi in fiList)
                                nFileInfo.Add(fi);
                        }
                    }
                }

                dgvFileList.Rows.Add(nFileInfo.Count());
                int nRow = 0;
                foreach (FILEINFO fi in nFileInfo)
                {
                    if (fi.FullName == "DATE DIRECTORY")
                        dgvFileList.Rows[nRow].DefaultCellStyle.Font = new Font("Arial", 12, FontStyle.Bold);
                    else
                        dgvFileList.Rows[nRow].DefaultCellStyle.Font = new Font("Arial", 12, FontStyle.Regular);

                    dgvFileList.Rows[nRow].Cells[0].Value = fi.NameOnly;
                    dgvFileList.Rows[nRow++].Cells[1].Value = fi.FullName;
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.ToString()); }
        }

        #region Draw Chart Method
        private List<DataValue>[] ReadValue(string filename)
        {
            List<DataValue>[] nDataValue = new List<DataValue>[_Constant.ChannelCount];
            DataValue dv = new DataValue();
            if (System.IO.File.Exists(filename) == false) return nDataValue;

            string[] csvRecords = File.ReadAllLines(filename);
            for (int i = 0; i < _Constant.ChannelCount; i++)
                nDataValue[i] = new List<DataValue>();

            int nTime = 0;
            int nVerify = _Constant.ChannelCount * 4 + 3;
            foreach (string d in csvRecords)
            {
                string[] split = d.Split(',');
                if (split.Length == nVerify)
                {
                    if (split[0] != "DATETIME")
                    {
                        nTime += 1;
                        for (int nIndex = 0; nIndex < _Constant.ChannelCount; nIndex++)
                        {
                            dv = new DataValue();
                            dv.Channel = (nIndex + 1);
                            dv.Time = nTime;
                            dv.Status = util.TryParseInt(split[nIndex * 4 + 3], 0);
                            dv.Current = util.TryParseDouble(split[nIndex * 4 + 4], 0);
                            dv.Voltage = util.TryParseDouble(split[nIndex * 4 + 5], 0);
                            dv.Capacity = util.TryParseDouble(split[nIndex * 4 + 6], 0);

                            nDataValue[dv.Channel - 1].Add(dv);
                        }
                    }
                }
            }
            return nDataValue;
        }
        private List<DataValue>[] ReadValue(int stageno, string filename)
        {
            stageno = 0;
            string dir = _Constant.DATA_PATH;
            string stagename = "STAGE" + (stageno + 1).ToString("D3");
            dir += System.DateTime.Now.ToString("yyyyMMdd") + "\\" + stagename + "\\";
            filename = dir + filename;

            List<DataValue>[] nDataValue = new List<DataValue>[_Constant.ChannelCount];
            DataValue dv = new DataValue();
            if (System.IO.File.Exists(filename) == false) return nDataValue;

            string[] csvRecords = File.ReadAllLines(filename);
            for (int i = 0; i < _Constant.ChannelCount; i++)
                nDataValue[i] = new List<DataValue>();

            int nTime = 0;
            foreach (string d in csvRecords)
            {
                nTime += 1;
                string[] split = d.Split(',');
                if (split.Length == 257)
                {
                    if (split[0] != "DATETIME")
                    {
                        for (int nIndex = 0; nIndex < _Constant.ChannelCount; nIndex++)
                        {
                            dv = new DataValue();
                            dv.Channel = (nIndex + 1);
                            dv.Time = nTime;
                            dv.Current = util.TryParseDouble(split[nIndex * 4 + 2], 0);
                            dv.Voltage = util.TryParseDouble(split[nIndex * 4 + 3], 0);
                            dv.Capacity = util.TryParseDouble(split[nIndex * 4 + 4], 0);

                            nDataValue[dv.Channel - 1].Add(dv);
                        }
                    }
                }
            }
            return nDataValue;
        }
        private List<DataValue>[] ReadValue2(string filename)
        {
            List<DataValue>[] nDataValue = new List<DataValue>[_Constant.ChannelCount];
            DataValue dv = new DataValue();
            if (System.IO.File.Exists(filename) == false) return nDataValue;

            string[] csvRecords = File.ReadAllLines(filename);
            for (int i = 0; i < _Constant.ChannelCount; i++)
                nDataValue[i] = new List<DataValue>();

            int nIndex = 1;
            foreach (string d in csvRecords)
            {
                string[] split = d.Split(',');
                if (split.Length == 6)
                {
                    if (split[0] != "CHANNEL")
                    {
                        dv = new DataValue();
                        dv.Channel = util.TryParseInt(split[0], 0);
                        dv.Time = nIndex++;
                        dv.Current = util.TryParseDouble(split[4], 0);
                        dv.Voltage = util.TryParseDouble(split[5], 0);
                        //dv.Capacity = util.TryParseDouble(split[6], 0);
                        if (dv.Channel > 0)
                            nDataValue[dv.Channel - 1].Add(dv);
                    }
                }
            }
            return nDataValue;
        }
        private List<DataValue>[] ReadValue2(int stageno, string filename)
        {
            stageno = 0;
            string dir = _Constant.DATA_PATH;
            string stagename = "STAGE" + (stageno + 1).ToString("D3");
            dir += System.DateTime.Now.ToString("yyyyMMdd") + "\\" + stagename + "\\";
            filename = dir + filename;

            List<DataValue>[] nDataValue = new List<DataValue>[_Constant.ChannelCount];
            DataValue dv = new DataValue();
            if (System.IO.File.Exists(filename) == false) return nDataValue;

            string[] csvRecords = File.ReadAllLines(filename);
            for (int i = 0; i < _Constant.ChannelCount; i++)
                nDataValue[i] = new List<DataValue>();

            int nIndex = 1;
            foreach (string d in csvRecords)
            {
                string[] split = d.Split(',');
                if (split.Length == 6)
                {
                    if (split[0] != "CHANNEL")
                    {
                        dv = new DataValue();
                        dv.Channel = util.TryParseInt(split[0], 0);
                        dv.Time = nIndex++;
                        dv.Current = util.TryParseDouble(split[4], 0);
                        dv.Voltage = util.TryParseDouble(split[5], 0);
                        //dv.Capacity = util.TryParseDouble(split[6], 0);
                        if (dv.Channel > 0)
                            nDataValue[dv.Channel - 1].Add(dv);
                    }
                }
            }
            return nDataValue;
        }
        private void DrawChartInvoke(List<DataValue>[] nDataValue)
        {
            //* 차트 초기화
            devChart.Titles.Clear();
            devChart.Series.Clear();

            //* 차트 그리기
            ChartTitle title = new ChartTitle();
            title.Text = "VOLTAGE / CURRENT";
            devChart.Titles.Add(title);
            devChart.CrosshairEnabled = DevExpress.Utils.DefaultBoolean.False;

            try
            {
                XYDiagram diagram = (XYDiagram)devChart.Diagram;
                diagram.AxisY.VisualRange.SetMinMaxValues(-4000, 4000);
                diagram.AxisX.VisualRange.SetMinMaxValues(0, 150);
                diagram.EnableAxisXScrolling = true;
                diagram.EnableAxisXZooming = true;
                diagram.EnableAxisYScrolling = true;
                diagram.EnableAxisYZooming = true;
                if (devChart.InvokeRequired)
                {
                    devChart.BeginInvoke(new Action(() =>
                    {
                        Console.WriteLine("Draw chart start : " + DateTime.Now.ToString("hh:mm:ss.fff"));
                        for (int nIndex = 0; nIndex < _Constant.ChannelCount; nIndex++)
                        {
                            if (chkChannel[nIndex].Checked == false) break;
                            string linename1 = "VOLT" + (nIndex + 1).ToString();
                            string linename2 = "CURR" + (nIndex + 1).ToString();

                            DevExpress.XtraCharts.Series series = new DevExpress.XtraCharts.Series(linename1, ViewType.Line);
                            DevExpress.XtraCharts.Series series2 = new DevExpress.XtraCharts.Series(linename2, ViewType.Line);
                            devChart.Series.Add(series);
                            devChart.Series.Add(series2);

                            devChart.Series[linename1].ArgumentScaleType = ScaleType.Numerical;
                            devChart.Series[linename2].ArgumentScaleType = ScaleType.Numerical;

                            if (nDataValue[nIndex] == null) return;
                            int nTime = 1;
                            devChart.Series[linename1].Points.BeginUpdate();
                            devChart.Series[linename2].Points.BeginUpdate();

                            List<DataValue> nData = nDataValue[nIndex];
                            foreach (DataValue dv in nData)
                            {
                                SeriesPoint point = new SeriesPoint(nTime, dv.Voltage);
                                SeriesPoint point2 = new SeriesPoint(nTime, dv.Current);

                                nTime += 1;
                                devChart.Series[linename1].Points.Add(point);
                                devChart.Series[linename2].Points.Add(point2);
                            }
                            devChart.Series[linename1].Points.EndUpdate();
                            devChart.Series[linename2].Points.EndUpdate();
                        }
                    }));
                }

                Console.WriteLine("Draw chart end : " + DateTime.Now.ToString("hh:mm:ss.fff"));
            }
            catch (Exception ex) { Console.WriteLine(ex.ToString()); }
        }
        private void DrawChart(List<DataValue>[] nDataValue)
        {
            //* 차트 초기화
            devChart.Titles.Clear();
            devChart.Series.Clear();

            //* 차트 그리기
            ChartTitle title = new ChartTitle();
            title.Text = "VOLTAGE / CURRENT";
            devChart.Titles.Add(title);
            devChart.CrosshairEnabled = DevExpress.Utils.DefaultBoolean.False;

            try
            {
                Console.WriteLine("Draw chart start : " + DateTime.Now.ToString("hh:mm:ss.fff"));
                XYDiagram diagram = (XYDiagram)devChart.Diagram;
                diagram.AxisX.QualitativeScaleOptions.AggregateFunction = AggregateFunction.Sum;
                diagram.AxisX.NumericScaleOptions.ScaleMode = ScaleMode.Automatic;
                //diagram.AxisX.NumericScaleOptions.ScaleMode = ScaleMode.Manual;
                //diagram.AxisX.NumericScaleOptions.MeasureUnit = NumericMeasureUnit.Tens;

                diagram.AxisY.VisualRange.SetMinMaxValues(-4000, 4000);
                diagram.AxisX.VisualRange.SetMinMaxValues(0, 3600);
                diagram.EnableAxisXScrolling = true;
                diagram.EnableAxisXZooming = true;
                diagram.EnableAxisYScrolling = true;
                diagram.EnableAxisYZooming = true;

                DevExpress.XtraCharts.Series[] seriesCurr = new DevExpress.XtraCharts.Series[_Constant.ChannelCount];
                DevExpress.XtraCharts.Series[] seriesVolt = new DevExpress.XtraCharts.Series[_Constant.ChannelCount];
                for (int nIndex = 0; nIndex < _Constant.ChannelCount; nIndex++)
                {
                    string linename1 = "VOLT" + (nIndex + 1).ToString();
                    string linename2 = "CURR" + (nIndex + 1).ToString();
                    seriesCurr[nIndex] = new DevExpress.XtraCharts.Series(linename1, ViewType.Line);
                    seriesVolt[nIndex] = new DevExpress.XtraCharts.Series(linename2, ViewType.Line);
                    devChart.Series.Add(seriesCurr[nIndex]);
                    devChart.Series.Add(seriesVolt[nIndex]);
                    seriesCurr[nIndex].ArgumentScaleType = ScaleType.Numerical;
                    seriesVolt[nIndex].ArgumentScaleType = ScaleType.Numerical;

                    seriesCurr[nIndex].Points.BeginUpdate();
                    seriesVolt[nIndex].Points.BeginUpdate();
                }

                for (int nIndex = 0; nIndex < _Constant.ChannelCount; nIndex++)
                {
                    if (chkChannel[nIndex].Checked == false) break;
                    if (nDataValue[nIndex] == null) return;

                    int nTime = 1;
                    foreach (DataValue dv in nDataValue[nIndex])
                    {
                        SeriesPoint point = new SeriesPoint(nTime, dv.Voltage);
                        SeriesPoint point2 = new SeriesPoint(nTime, dv.Current);

                        nTime += 1;
                        seriesCurr[nIndex].Points.Add(point);
                        seriesVolt[nIndex].Points.Add(point2);
                    }
                }

                for (int nIndex = 0; nIndex < _Constant.ChannelCount; nIndex++)
                {
                    seriesCurr[nIndex].Points.EndUpdate();
                    seriesVolt[nIndex].Points.EndUpdate();
                }
                Console.WriteLine("Draw chart end : " + DateTime.Now.ToString("hh:mm:ss.fff"));
            }
            catch (Exception ex) { Console.WriteLine(ex.ToString()); }
        }
        private void DrawChartSwiftPoint(List<DataValue>[] nDataValue)
        {
            //* 차트 초기화
            devChart.Titles.Clear();
            devChart.Series.Clear();

            //* 차트 그리기
            ChartTitle title = new ChartTitle();
            title.Text = "VOLTAGE / CURRENT";
            devChart.Titles.Add(title);
            devChart.CrosshairEnabled = DevExpress.Utils.DefaultBoolean.False;

            try
            {
                Console.WriteLine("Draw chart start : " + DateTime.Now.ToString("hh:mm:ss.fff"));

                for (int nIndex = 0; nIndex < _Constant.ChannelCount; nIndex++)
                {
                    if (chkChannel[nIndex].Checked == false) break;
                    string linename1 = "VOLT" + (nIndex + 1).ToString();
                    string linename2 = "CURR" + (nIndex + 1).ToString();

                    if (nDataValue[nIndex] == null) return;
                    int nTime = 1;
                    List<Point> data = new List<Point>(nDataValue[nIndex].Count());
                    List<Point> data2 = new List<Point>(nDataValue[nIndex].Count());
                    foreach (DataValue dv in nDataValue[nIndex])
                    {
                        data.Add(new Point(nTime, (int)dv.Voltage));
                        data2.Add(new Point(nTime, (int)dv.Current));
                        nTime += 1;
                    }

                    DevExpress.XtraCharts.Series series = new DevExpress.XtraCharts.Series(linename1, ViewType.SwiftPoint);
                    DevExpress.XtraCharts.Series series2 = new DevExpress.XtraCharts.Series(linename2, ViewType.SwiftPoint);
                    devChart.Series.Add(series);
                    devChart.Series.Add(series2);

                    series.BindToData(data, "X", "Y");
                    series2.BindToData(data2, "X", "Y");

                    SwiftPlotDiagram diagram = (SwiftPlotDiagram)devChart.Diagram;
                    diagram.AxisY.VisualRange.SetMinMaxValues(-4200, 4200);
                    diagram.AxisX.VisualRange.SetMinMaxValues(0, nDataValue[0].Count());

                    SwiftPointSeriesView view = (SwiftPointSeriesView)series.View;
                    view.Color = Color.FromArgb(180, devChart.GetPaletteEntries(1)[0].Color);
                    view.PointMarkerOptions.Size = 6;
                    view.PointMarkerOptions.Kind = MarkerKind.Square;
                }
                Console.WriteLine("Draw chart end : " + DateTime.Now.ToString("hh:mm:ss.fff"));
            }
            catch (Exception ex) { Console.WriteLine(ex.ToString()); }
        }
        private void DrawChart(List<DataValue>[] nDataValue, bool bCheck, int nTag)
        {
            //* 차트에서 해당 series 추가 또는 삭제
            string linename1 = "VOLT" + (nTag + 1).ToString();
            string linename2 = "CURR" + (nTag + 1).ToString();
            try
            {
                if (bCheck == true)
                {
                    devChart.Series.Add(new DevExpress.XtraCharts.Series(linename1, ViewType.Line));
                    devChart.Series.Add(new DevExpress.XtraCharts.Series(linename2, ViewType.Line));
                    devChart.Series[linename1].ArgumentScaleType = ScaleType.Numerical;
                    devChart.Series[linename2].ArgumentScaleType = ScaleType.Numerical;
                    ((XYDiagram)devChart.Diagram).AxisY.VisualRange.SetMinMaxValues(-4000, 4000);
                    ((XYDiagram)devChart.Diagram).AxisX.VisualRange.SetMinMaxValues(0, 1500);

                    int nTime = 1;
                    foreach (DataValue dv in nDataValue[nTag])
                    {
                        SeriesPoint point = new SeriesPoint(nTime, dv.Voltage);
                        SeriesPoint point2 = new SeriesPoint(nTime, dv.Current);

                        nTime += 1;
                        devChart.Series[linename1].Points.Add(point);
                        devChart.Series[linename2].Points.Add(point2);
                    }
                }
                else
                {
                    Series s1 = devChart.Series[linename1];
                    Series s2 = devChart.Series[linename2];
                    devChart.Series.Remove(s1);
                    devChart.Series.Remove(s2);
                }

                Console.WriteLine(DateTime.Now.ToString("hh:mm:ss.fff"));
            }
            catch (Exception ex) { Console.WriteLine(ex.ToString()); }
        }
        private void DrawChart2(DataValue2[] nDataValue)
        {
            ChartTitle title = new ChartTitle();
            title.Text = "CURRENT";
            devChart.Titles.Add(title);
            devChart.CrosshairEnabled = DevExpress.Utils.DefaultBoolean.False;

            //* X, Y 축 지정
            //int nIndex = 9;
            for (int nIndex = 0; nIndex < _Constant.ChannelCount; nIndex++)
            {
                string linename1 = "VOLT" + (nIndex + 1).ToString();
                string linename2 = "CURR" + (nIndex + 1).ToString();
                devChart.Series.Add(new DevExpress.XtraCharts.Series(linename1, ViewType.Line));
                devChart.Series.Add(new DevExpress.XtraCharts.Series(linename2, ViewType.Line));
                //devChart.Series[linename].LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
                //(devChart.Series[linename].View as LineSeriesView).MarkerVisibility = DevExpress.Utils.DefaultBoolean.True;
                //devChart.Series[linename].ArgumentScaleType = ScaleType.Qualitative;

                devChart.Series[linename1].Label.PointOptions.PointView = PointView.ArgumentAndValues;
                //devChart.Series[linename1].Label.PointOptions.ValueNumericOptions.Format = DevExpress.XtraCharts.NumericFormat.Percent;
                devChart.Series[linename1].Label.LineVisible = false;
                devChart.Series[linename1].LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;

                ((XYDiagram)devChart.Diagram).AxisY.VisualRange.SetMinMaxValues(-4000, 4000);
                ((XYDiagram)devChart.Diagram).AxisX.VisualRange.SetMinMaxValues(0, 300);

                int nTime = 1;
                //SeriesPoint point3 = new SeriesPoint()
                SeriesPoint point = new SeriesPoint(nTime, nDataValue[nIndex].Current);
                //SeriesPoint point2 = new SeriesPoint(nTime++, nDataValue[nIndex].Current);

                devChart.Series[linename1].Points.Add(point);
                //devChart.Series[linename2].Points.Add(point2);
            }
        }
        private void DrawChartWithFileAsync(string filename)
        {
            try
            {
                Console.WriteLine(DateTime.Now.ToString("hh:mm:ss.fff"));
                nDataValue = ReadValue(filename);
                Console.WriteLine(DateTime.Now.ToString("hh:mm:ss.fff"));
                DrawChart(nDataValue);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        #endregion

        #region Control Event
        private void chkChannel_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = sender as CheckBox;
            int nTag = util.TryParseInt(chk.Tag.ToString(), 0);

            for (int nIndex = 0; nIndex < 16; nIndex++)
            {
                if (chk.Checked == true) chkChannel[nTag * 16 + nIndex].Checked = true;
                else chkChannel[nTag * 16 + nIndex].Checked = false;
            }

            //DrawChart(nDataValue);
        }
        private void chkChannelArray_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = sender as CheckBox;
            int nTag = util.TryParseInt(chk.Tag.ToString(), 0);
            bool bCheck = chk.Checked;
            DrawChart(nDataValue, bCheck, nTag);
        }
        private void cbStageName_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            string stagename = cb.Text;
            // stage001
            if (stagename.Length != 8) return;

            nFileInfo = new List<FILEINFO>();
            dgvFileList.Rows.Clear();
            LoadTrayId(stagename);
        }
        private void dgvFileList_SelectionChanged(object sender, EventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            int curRow = -1;
            string filename = string.Empty;
            if (dgv.CurrentRow.Index != curRow)
            {
                curRow = dgv.CurrentRow.Index;
                if (dgv.Rows[curRow].Cells[0].Value == null) return;

                filename = dgv.Rows[curRow].Cells[1].Value.ToString();
                DrawChartWithFileAsync(filename);
                //nDataValue = ReadValue(filename);
                //DrawChart(nDataValue);

            }
        }
        #endregion
    }
}
