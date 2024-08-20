using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Utils.Extensions;
using DHS.EQUIPMENT2.Equipment;
using Microsoft.VisualBasic.FileIO;
using SoftCircuits.IniFileParser;
using Telerik.WinControls.UI;

namespace DHS.EQUIPMENT2.Common
{
    class Util
    {
        MariaDBConfig mariaConfig = null;
        IniFile ini = new IniFile();
        Form form = null;
        Panel panel = null;

        #region Add Form to Panel
        public void loadFormIntoPanel(Form childform, Panel parentpanel)
        {
            form = null;

            form = childform;
            panel = parentpanel;

            if (form != null && panel != null)
            {
                form.TopLevel = false;
                form.AutoScroll = true;
                InsertForm(form, panel);
                form.Dock = DockStyle.Fill;

                panel.Controls.Add(form);
                form.Show();
            }
        }
        public void loadFormIntoPanelWithLeft(Form childform, Panel parentpanel)
        {
            form = null;

            form = childform;
            panel = parentpanel;

            if (form != null && panel != null)
            {
                form.TopLevel = false;
                form.AutoScroll = true;
                InsertForm(form, panel);
                form.Dock = DockStyle.Left;

                panel.Controls.Add(form);
                form.Show();
            }
        }

        private void InsertForm(Form f, Control c)
        {
            if (c != null)
            {
                f.TopLevel = false;
                f.Dock = DockStyle.Fill;
                f.FormBorderStyle = FormBorderStyle.None;
                f.MaximizeBox = false;
                f.MinimizeBox = false;
                f.ControlBox = false;

                c.Controls.Add(f);
                f.Show();
            }
        }

        public void deleteFormFromPanel(Form childform, Panel parentpanel)
        {
            form = childform;
            panel = parentpanel;
            if (form != null && panel != null)
            {
                RemoveForm(form, panel);
            }
        }
        static public void RemoveForm(Form f, Control c)
        {
            if (c != null)
            {
                f.TopLevel = false;
                f.Dock = DockStyle.None;
                f.FormBorderStyle = FormBorderStyle.None;
                f.MaximizeBox = false;
                f.MinimizeBox = false;
                f.ControlBox = false;

                c.Controls.Remove(f);
                f = null;
            }
        }
        #endregion

        #region Tryparse
        public int TryParseInt(string text, int nDefaultValue)
        {
            int res;
            int value;
            if (Int32.TryParse(text,
                System.Globalization.NumberStyles.Integer,
                System.Globalization.CultureInfo.InvariantCulture,
                out res))
            {
                value = res;
                return value;
            }
            return nDefaultValue;
        }

        public double TryParseDouble(string text, double dDefaultValue)
        {
            double res;
            double value;
            if (Double.TryParse(text,
                System.Globalization.NumberStyles.Float,
                System.Globalization.CultureInfo.InvariantCulture,
                out res))
            {
                value = res;
                return value;
            }
            return dDefaultValue;
        }

        #endregion

        #region Config (ini/inf File)
        public void saveConfig(string filename, string main_title, string sub_title, string sValue)
        {

            // Network Setting
            ini[main_title][sub_title] = sValue;


            ini.Save(filename);
        }

        public string readConfig(string filename, string main_title, string sub_title)
        {
            string strValue = "";

            ini.Load(filename);
            strValue = ini[main_title][sub_title].ToString();

            return strValue;
        }
        #endregion

        #region CSV File
        public List<double[]> ReadCsvFile(string filename)
        {
            List<double[]> csvRecords = new List<double[]>();
            using (var parser = new TextFieldParser(filename))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");

                // 첫 라인은 title 이므로 skip 함.
                if (!parser.EndOfData)
                    parser.ReadLine();

                while (!parser.EndOfData)
                {
                    string[] fields = parser.ReadFields();
                    // 가장 앞 행은 채널 번호 (CHANNEL_01, CHANNEL_02, ...)
                    double[] values = new double[fields.Length - 1];
                    for (int nIndex = 1; nIndex < fields.Length; nIndex++)
                    {
                        values[nIndex - 1] = Convert.ToDouble(fields[nIndex]);
                    }
                    csvRecords.Add(values);
                }
            }
            return csvRecords;
        }
        public string[] ReadCsvToStrArray(int nstageno, string filename)
        {
            string dir = _Constant.DATA_PATH;
            string stagename = "STAGE" + (nstageno + 1).ToString("D3");
            dir += System.DateTime.Now.ToString("yyyyMMdd") + "\\" + stagename + "\\";

            filename = dir + filename;

            string[] csvRecords = File.ReadAllLines(filename);
            return csvRecords;
        }
        public void WriteCsvFile(string filename, string[] strOffset)
        {
            using (var writer = new StreamWriter(filename))
            {
                // Write header row
                writer.WriteLine("CHANNEL,STANDARD,MEASURED,OFFSET"); // Replace with your column headers

                // Write data rows
                for (int nIndex = 0; nIndex < strOffset.Length; nIndex++)
                    writer.WriteLine("CH_" + (nIndex + 1).ToString() + "," + strOffset[nIndex]);
            }
        }
        #endregion

        #region File Write/ Read
        public void FileWrite(string filePath, string strData)
        {
            using (FileStream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                using (StreamWriter streamWriter = new StreamWriter(fileStream, Encoding.UTF8))
                {
                    streamWriter.Write(strData);

                    //다쓴 StreamWriter 와 FileStream 닫기
                    streamWriter.Flush();
                    streamWriter.Close();

                    fileStream.Close();
                }
            }
        }
        public void FileWrite_old(string filePath, string strData)
        {
            FileStream fileStream = new FileStream(
                filePath,              //저장경로
                FileMode.Create,       //파일스트림 모드
                FileAccess.Write       //접근 권한
                );

            StreamWriter streamWriter = new StreamWriter(fileStream, Encoding.UTF8);
            streamWriter.Write(strData);

            //다쓴 StreamWriter 와 FileStream 닫기
            streamWriter.Close();
            fileStream.Close();
        }
        public void FileAppend(string filePath, string timeData, double[,] spData, string forceData)
        {
            string strData = "";
            using (FileStream fileStream = new FileStream(filePath, FileMode.Append, FileAccess.Write))
            {
                using (StreamWriter sw = new StreamWriter(fileStream))
                {
                    for (int i = 0; i < spData.GetLength(0); i++)
                    {
                        strData += spData[i, 0].ToString() + ";";
                    }
                    sw.WriteLine(timeData + ";" + strData + forceData);
                }
            }
        }
        public void FileAppend(string filePath, string strData)
        {
            try
            {
                using (FileStream fileStream = new FileStream(filePath, FileMode.Append, FileAccess.Write))
                {
                    using (StreamWriter sw = new StreamWriter(fileStream))
                    {
                        sw.WriteLine(strData);
                    }
                }
            }
            catch (Exception ex) { Console.Write(ex.ToString()); }
        }
        public void DeleteOldFiles(string dirPath, string strDate)
        {
            try
            {
                DirectoryInfo dirInfo = new DirectoryInfo(dirPath);
                DateTime fileCreatedTime;
                DateTime compareTime = DateTime.ParseExact(strDate, "yyyyMMdd", null);
                foreach (FileInfo file in dirInfo.GetFiles())
                {
                    fileCreatedTime = file.CreationTime;
                    if (DateTime.Compare(fileCreatedTime, compareTime) < 0)
                    {
                        File.Delete(file.FullName);
                    }
                }
            }
            catch (Exception ex) { Console.Write(ex.ToString()); }
        }
        #endregion

        #region Make Command
        public string MakeCMD(string cmd)
        {
            string STX = string.Format("{0}", (char)2);
            string ETX = string.Format("{0}", (char)3);

            return STX + CheckSum(cmd) + ETX;
        }

        public string CheckSum(string strData)
        {
            string sRtnVal = "";

            if (strData != null)
            {
                byte checksum = 0x00;
                byte[] aa = new byte[strData.Length];

                for (int i = 0; i < strData.Length; i++)
                    aa[i] = (byte)strData[i];

                for (int i = 0; i < strData.Length; i++)
                    checksum += (byte)strData[i];

                string kwon = checksum.ToString("X2");

                sRtnVal = strData + kwon;
            }

            return sRtnVal;
        }
        #endregion

        #region Directory
        /// <summary>
        /// Get sub directory list from main directory
        /// </summary>
        /// <param name="dir">Directory Name</param>
        /// <returns></returns>
        public string[] GetDirList(string dir)
        {
            string[] subDirs = Directory.GetDirectories(dir);
            return subDirs;
        }
        /// <summary>
        /// Get file list from directory
        /// </summary>
        /// <param name="dir">Directory Name</param>
        /// <returns></returns>
        public List<FILEINFO> GetFileList(string dir)
        {
            List<FILEINFO> fileList = new List<FILEINFO>();
            DirectoryInfo di = new DirectoryInfo(dir);
            
            foreach(FileInfo file in di.GetFiles())
            {
                FILEINFO fi = new FILEINFO();
                if(file.Extension.ToLower().CompareTo(".csv") == 0)
                {
                    fi.NameOnly = file.Name.Substring(0, file.Name.Length - 4);
                    fi.FullName = file.FullName;
                    fileList.Add(fi);
                }
            }
            return fileList;
        }
        #endregion

        #region CDC Result File
        public KeysightMonData ReadMonData(int stageno, TRAYINFO trayinfo)
        {
            KeysightMonData[] monData = null;
            string dir = _Constant.DATA_PATH;
            string filename = trayinfo.TRAYID + "_MON.csv";
            string[] monDatas = ReadCsvToStrArray(stageno, filename);
            int nDataLength = _Constant.ChannelCount * 4 + 1;
            string[] strDatas = new string[nDataLength];
            

            for(int nIndex = 0; nIndex < monDatas.Length; nIndex++)
            {
                strDatas = monDatas[nIndex].Split(',');
                if (strDatas.Length != nDataLength)
                    continue;

                monData[nIndex] = new KeysightMonData();
                monData[nIndex].DATETIME = strDatas[0];
                for (int nCh = 0; nCh < _Constant.ChannelCount; nCh++)
                {
                    monData[nIndex].CHANNELSTATUS[nCh] = int.Parse(strDatas[4 * nCh + 1]);
                    monData[nIndex].CHANNELCURRENT[nCh] = float.Parse(strDatas[4 * nCh + 2]);
                    monData[nIndex].CHANNELVOLTAGE[nCh] = float.Parse(strDatas[4 * nCh + 3]);
                    monData[nIndex].CHANNELCAPACITY[nCh] = float.Parse(strDatas[4 * nCh + 4]);
                }
            }

            return monData[_Constant.ChannelCount - 1];
        }
        public string ConvertMsTimeToString(ulong time)
        {
            string strTime = string.Empty;
            TimeSpan ts = TimeSpan.FromMilliseconds(time);
            strTime = ts.ToString(@"hh\:mm\:ss\.fff");
            return strTime;
        }
        public void SaveCalData(int stageno, string calTime, string calStatus, string calOKNG, string calProcess, string moduleno, string channelno)
        {
            string dir = _Constant.DATA_PATH;
            string stagename = "STAGE" + (stageno + 1).ToString("D3");
            dir += System.DateTime.Now.ToString("yyyyMMdd") + "\\" + stagename + "\\";
            if (Directory.Exists(dir) == false) Directory.CreateDirectory(dir);
            string filename = dir + stagename + "_CAL.csv";
            string strMonHeader;
            string strMonBody = string.Empty;

            //DATETIME, CAL TIME, CAL STATUS, OK/NG, CAL PROCESS, MODULE NO, CHANNEL NO
            strMonHeader = "DATETIME,CAL TIME,CAL STATUS, CAL OK/NG, CAL PROCESS, MODULE No., CHANNEL No.";
            strMonHeader += Environment.NewLine;

            strMonBody += DateTime.Now.ToString("yyyyMMdd HH:mm:ss") + ",";
            strMonBody += calTime + "," + calStatus + "," + calOKNG + "," 
                + calProcess + "," + moduleno + "," + channelno;

            if (System.IO.File.Exists(filename) == false)
            {
                FileWrite(filename, strMonHeader + strMonBody + Environment.NewLine);
            }
            else
            {
                FileAppend(filename, strMonBody.TrimEnd(','));
            }
        }
        public string SaveMonData(int stageno, string trayid, string recipeinfo, KeysightMonData mondata)
        {
            string dir = _Constant.DATA_PATH;
            string stagename = "STAGE" + (stageno + 1).ToString("D3");
            dir += System.DateTime.Now.ToString("yyyyMMdd") + "\\" + stagename + "\\";
            if (Directory.Exists(dir) == false) Directory.CreateDirectory(dir);
            string filename = dir + trayid + "_MON.csv";
            string strMonHeader;
            string strMonBody = string.Empty;

            //DATETIME, TIME, CH1 STATUS, CH1 CURR, CH1 VOLT ...
            strMonHeader = "DATETIME,TIMESTAMP,RUNCOUNT";
            for (int nIndex = 0; nIndex < _Constant.ChannelCount; nIndex++)
                strMonHeader += ",CH" + (nIndex + 1) + " STATUS" + ",CH" + (nIndex + 1) + " CURR" + ",CH" + (nIndex + 1) + " VOLT" + ",CH" + (nIndex + 1) + " CAPA";
            strMonHeader += Environment.NewLine;

            //* DATETIME, TIMESTAMP, RUNCOUNT
            strMonBody += mondata.DATETIME + "," 
                + ConvertMsTimeToString(mondata.STAGETIME) + ","
                + mondata.RUNCOUNT + ",";
            
            for (int nIndex = 0; nIndex < _Constant.ChannelCount; nIndex++)
            {
                //* STATUS
                strMonBody += mondata.CHANNELSTATUS[nIndex].ToString() + ",";

                //* CURRENT Value
                strMonBody += (mondata.CHANNELCURRENT[nIndex] * 1000).ToString("F1") + ",";

                //* VOLTAGE Value
                strMonBody += (mondata.CHANNELVOLTAGE[nIndex] * 1000).ToString("F2") + ",";

                //* CAPACITY Value
                strMonBody += (mondata.CHANNELCAPACITY[nIndex] * 1000).ToString("F1") + ",";
            }

            //strMonBody += Environment.NewLine;

            if (System.IO.File.Exists(filename) == false)
            {
                FileWrite(filename, recipeinfo + strMonHeader + strMonBody.TrimEnd(',') + Environment.NewLine);
                return strMonHeader + strMonBody.TrimEnd(',');
            }
            else
            {
                FileAppend(filename, strMonBody.TrimEnd(','));
                return strMonBody.TrimEnd(',');
            }
        }
        public string SaveMonData(int stageno, string trayid, string recipeinfo, KeysightController keysightcontroller)
        {
            string dir = _Constant.DATA_PATH;
            string stagename = "STAGE" + (stageno + 1).ToString("D3");
            dir += System.DateTime.Now.ToString("yyyyMMdd") + "\\" + stagename + "\\";
            if (Directory.Exists(dir) == false) Directory.CreateDirectory(dir);
            string filename = dir + trayid + "_MON.csv";
            string strMonHeader;
            string strMonBody = string.Empty;

            //DATETIME, TIME, CH1 STATUS, CH1 CURR, CH1 VOLT ...
            strMonHeader = "DATETIME,TIMESTAMP,RUNCOUNT";
            for (int nIndex = 0; nIndex < _Constant.ChannelCount; nIndex++)
                strMonHeader += ",CH" + (nIndex + 1) + " STATUS" + ",CH" + (nIndex + 1) + " CURR" + ",CH" + (nIndex + 1) + " VOLT" + ",CH" + (nIndex + 1) + " CAPA";
            strMonHeader += Environment.NewLine;

            //* DATETIME, TIMESTAMP, RUNCOUNT
            strMonBody += keysightcontroller.DATETIME + ","
                + ConvertMsTimeToString(keysightcontroller.STAGETIME) + ","
                + keysightcontroller.RUNCOUNT + ",";

            for (int nIndex = 0; nIndex < _Constant.ChannelCount; nIndex++)
            {
                //* STATUS
                strMonBody += keysightcontroller.CHANNELSTATUS[nIndex].ToString() + ",";

                //* CURRENT Value
                strMonBody += (keysightcontroller.CHANNELCURRENT[nIndex] * 1000).ToString("F1") + ",";

                //* VOLTAGE Value
                strMonBody += (keysightcontroller.CHANNELVOLTAGE[nIndex] * 1000).ToString("F2") + ",";

                //* CAPACITY Value
                strMonBody += (keysightcontroller.CHANNELCAPACITY[nIndex] * 1000).ToString("F1") + ",";
            }

            //strMonBody += Environment.NewLine;

            if (System.IO.File.Exists(filename) == false)
            {
                FileWrite(filename, recipeinfo + strMonHeader + strMonBody.TrimEnd(',') + Environment.NewLine);
                return strMonHeader + strMonBody.TrimEnd(',');
            }
            else
            {
                FileAppend(filename, strMonBody.TrimEnd(','));
                return strMonBody.TrimEnd(',');
            }
        }
        public string SaveMonData3(int stageno, KeysightMonData mondata, TRAYINFO trayinfo)
        {
            string dir = _Constant.DATA_PATH;
            string stagename = "STAGE" + (trayinfo.STAGENO + 1).ToString("D3");
            dir += System.DateTime.Now.ToString("yyyyMMdd") + "\\" + stagename + "\\";
            if (Directory.Exists(dir) == false) Directory.CreateDirectory(dir);
            string filename = dir + trayinfo.TRAYID + "_MON.csv";
            string strMonHeader;
            string strMonBody = string.Empty;

            //DATETIME, TIME, CH1 STATUS, CH1 CURR, CH1 VOLT ...
            strMonHeader = "DATETIME";
            for (int nIndex = 0; nIndex < _Constant.ChannelCount; nIndex++)
                strMonHeader += ",CH" + (nIndex + 1) + " STATUS" + ",CH" + (nIndex + 1) + " CURR" + ",CH" + (nIndex + 1) + " VOLT" + ",CH" + (nIndex + 1) + " CAPA";
            strMonHeader += Environment.NewLine;

            //* DATETIME
            strMonBody += mondata.DATETIME + ",";

            //* TIME STAMP
            //strMonBody += mondata.STAGETIME + ",";

            for (int nIndex = 0; nIndex < _Constant.ChannelCount; nIndex++)
            {
                //* STATUS
                strMonBody += mondata.CHANNELSTATUS[nIndex].ToString() + ",";

                //* CURRENT Value
                strMonBody += (mondata.CHANNELCURRENT[nIndex] * 1000).ToString("F1") + ",";

                //* VOLTAGE Value
                strMonBody += (mondata.CHANNELVOLTAGE[nIndex] * 1000).ToString("F2") + ",";

                //* CAPACITY Value
                strMonBody += (mondata.CHANNELCAPACITY[nIndex] * 1000).ToString("F1") + ",";
            }

            //strMonBody += Environment.NewLine;

            if (System.IO.File.Exists(filename) == false)
            {
                FileWrite(filename, strMonHeader + strMonBody.TrimEnd(',') + Environment.NewLine);
                return strMonHeader + strMonBody.TrimEnd(',');
            }
            else
            {
                FileAppend(filename, strMonBody.TrimEnd(','));
                return strMonBody.TrimEnd(',');
            }
        }
        public string SaveMonData2(int stageno, KeysightMonData mondata, TRAYINFO trayinfo)
        {
            string dir = _Constant.DATA_PATH;
            string stagename = "STAGE" + (trayinfo.STAGENO + 1).ToString("D3");
            dir += System.DateTime.Now.ToString("yyyyMMdd") + "\\" + stagename + "\\";
            if (Directory.Exists(dir) == false) Directory.CreateDirectory(dir);
            string filename = dir + trayinfo.TRAYID + "_MON.csv";
            string strMonHeader;
            string strMonBody = string.Empty;

            strMonHeader = "CHANNEL,DATETIME,TIME,STAUTS,CURRENT,VOLTAGE,CAPACITY" + Environment.NewLine;
            for (int nIndex = 0; nIndex < _Constant.ChannelCount; nIndex++)
            {
                //* CHANNEL
                strMonBody += (nIndex + 1).ToString("D3") + ",";

                //* DATETIME
                strMonBody += mondata.DATETIME + ",";

                //* TIME STAMP
                strMonBody += mondata.STAGETIME + ",";

                //* STATUS
                strMonBody += mondata.CHANNELSTATUS[nIndex].ToString() + ",";

                //* CURRENT Value
                strMonBody += (mondata.CHANNELCURRENT[nIndex] * 1000).ToString("F1") + ",";

                //* VOLTAGE Value
                strMonBody += (mondata.CHANNELVOLTAGE[nIndex] * 1000).ToString("F2");

                strMonBody += Environment.NewLine;
            }

            if (System.IO.File.Exists(filename) == false)
            {
                FileWrite(filename, strMonHeader + strMonBody);
            }
            else
            {
                FileAppend(filename, strMonBody);
            }

            return strMonHeader + strMonBody;
        }
        public void SaveFinData(int stageno, string findata, TRAYINFO trayinfo)
        {
            string dir = _Constant.DATA_PATH;
            string stagename = "STAGE" + (trayinfo.STAGENO + 1).ToString("D3");
            dir += System.DateTime.Now.ToString("yyyyMMdd") + "\\" + stagename + "\\";
            if (Directory.Exists(dir) == false) Directory.CreateDirectory(dir);
            string filename = dir + trayinfo.TRAYID + "_FIN.csv";

            if (System.IO.File.Exists(filename) == false)
            {
                FileWrite(filename, findata + Environment.NewLine);
            }
            else
            {
                FileAppend(filename, findata);
            }
        }
        public void SaveFinData(int stageno, string trayid, string recipeinfo, List<string> finData)
        {
            string dir = _Constant.DATA_PATH;
            string stagename = "STAGE" + (stageno + 1).ToString("D3");
            dir += System.DateTime.Now.ToString("yyyyMMdd") + "\\" + stagename + "\\";
            if (Directory.Exists(dir) == false) Directory.CreateDirectory(dir);
            string filename = dir + trayid + "_FIN.csv";

            string strFinHeader = string.Empty;
            string strFinData = string.Empty;
            foreach (string str in finData)
                strFinData += str + Environment.NewLine;

            strFinHeader = "CHANNEL,DATETIME,STAUTS,CURRENT,VOLTAGE,CAPACITY" + Environment.NewLine;
            //* final data는 recipe 마다 헤더가 필요함.
            if (System.IO.File.Exists(filename) == false)
            {
                FileWrite(filename, recipeinfo + strFinHeader + strFinData + Environment.NewLine);
            }
            else
            {
                FileAppend(filename, recipeinfo + strFinHeader + strFinData + Environment.NewLine);
            }
        }
        public void SaveFinData2(int stageno, List<string> finData, TRAYINFO trayinfo)
        {
            string dir = _Constant.DATA_PATH;
            string stagename = "STAGE" + (trayinfo.STAGENO + 1).ToString("D3");
            dir += System.DateTime.Now.ToString("yyyyMMdd") + "\\" + stagename + "\\";
            if (Directory.Exists(dir) == false) Directory.CreateDirectory(dir);
            string filename = dir + trayinfo.TRAYID + "_FIN.csv";

            string strFinHeader = string.Empty;
            string findata = string.Empty;
            foreach (string str in finData)
                findata += str + Environment.NewLine;

            strFinHeader = "CHANNEL,DATETIME,TIME,STAUTS,CURRENT,VOLTAGE,CAPACITY" + Environment.NewLine;
            if (System.IO.File.Exists(filename) == false)
            {
                FileWrite(filename, strFinHeader + findata + Environment.NewLine);
            }
            else
            {
                FileAppend(filename, findata);
            }
        }
        #endregion

        #region Save Log
        string strPLCMessage = string.Empty;
        /// <summary>
        /// Controller Log
        /// </summary>
        public void SaveLog(int nIndex, string strMessage)
        {
            if (strMessage == oldIROCVMessage) return;
            oldIROCVMessage = strMessage;
            string dir = string.Empty;
            string StageTitle = "STAGE" + (nIndex + 1).ToString();
            dir = _Constant.LOG_PATH;
            dir += System.DateTime.Now.ToString("yyyyMMdd") + "\\" + StageTitle + "\\";
            if (Directory.Exists(dir) == false) Directory.CreateDirectory(dir);
            string filename = dir + StageTitle + "_" + DateTime.Now.ToString("yyMMdd-HH") + ".log";
            string strMonitoring = string.Empty;

            strMonitoring = strMessage;

            strMonitoring = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "\t" + strMonitoring;
            if (System.IO.File.Exists(filename) == false) FileWrite(filename, strMonitoring);
            else FileAppend(filename, strMonitoring);

        }
        public void SaveLog(int nIndex, string strMessage, string type)
        {
            string dir = "";
            string StageTitle = "STAGE" + (nIndex + 1).ToString();
            dir = _Constant.LOG_PATH;
            dir += System.DateTime.Now.ToString("yyyyMMdd") + "\\" + StageTitle + "\\";
            if (Directory.Exists(dir) == false) Directory.CreateDirectory(dir);
            string filename = dir + StageTitle + "_" + DateTime.Now.ToString("yyMMdd-HH") + ".log";
            string strMonitoring = "";

            strMonitoring = strMessage;

            strMonitoring = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "\t" + type + "\t" + strMonitoring;
            if (System.IO.File.Exists(filename) == false) FileWrite(filename, strMonitoring);
            else FileAppend(filename, strMonitoring);

        }
        public void SavePLCLog(int nIndex, string strMessage)
        {
            if (strMessage == strPLCMessage) return;
            strPLCMessage = strMessage;

            string dir = "";
            string StageTitle = "STAGE" + (nIndex + 1).ToString();
            dir = _Constant.LOG_PATH;
            dir += System.DateTime.Now.ToString("yyyyMMdd") + "\\" + StageTitle + "\\";
            if (Directory.Exists(dir) == false) Directory.CreateDirectory(dir);
            string filename = dir + StageTitle + "_PLC_" + DateTime.Now.ToString("yyMMdd-HH") + ".log";
            string strMonitoring = "";

            strMonitoring = strMessage;

            strMonitoring = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\t" + strMonitoring;
            if (System.IO.File.Exists(filename) == false) FileWrite(filename, strMonitoring);
            else FileAppend(filename, strMonitoring);

        }
        public void SaveCDCLog(int nIndex, string strMessage, string type)
        {
            string dir = "";
            string StageTitle = "CDC" + (nIndex + 1).ToString();
            dir = _Constant.LOG_PATH;
            dir += System.DateTime.Now.ToString("yyyyMMdd") + "\\" + StageTitle + "\\";
            if (Directory.Exists(dir) == false) Directory.CreateDirectory(dir);
            string filename = dir + StageTitle + "_" + DateTime.Now.ToString("yyMMdd-HH") + ".log";
            string strMonitoring = "";

            if (type == "RX" && strMessage.Contains("MON"))
            {
                strMonitoring = strMessage.Substring(0, 33) + Environment.NewLine;
                strMonitoring = strMonitoring + ParseMon2859(strMessage);
            }
            else
                strMonitoring = strMessage;

            strMonitoring = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "\t" + type + "\t" + strMonitoring;
            if (System.IO.File.Exists(filename) == false) FileWrite(filename, strMonitoring);
            else FileAppend(filename, strMonitoring);

        }
        string oldIROCVMessage = string.Empty;
        public void SaveCDCLog(int nIndex, string strMessage)
        {
            if (strMessage == oldIROCVMessage) return;
            oldIROCVMessage = strMessage;
            string dir = string.Empty;
            string StageTitle = "CDC" + (nIndex + 1).ToString();
            dir = _Constant.LOG_PATH;
            dir += System.DateTime.Now.ToString("yyyyMMdd") + "\\" + StageTitle + "\\";
            if (Directory.Exists(dir) == false) Directory.CreateDirectory(dir);
            string filename = dir + StageTitle + "_" + DateTime.Now.ToString("yyMMdd-HH") + ".log";
            string strMonitoring = string.Empty;

            strMonitoring = strMessage;

            strMonitoring = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "\t" + strMonitoring;
            if (System.IO.File.Exists(filename) == false) FileWrite(filename, strMonitoring);
            else FileAppend(filename, strMonitoring);

        }

        public void SaveDBLog(string strMessage)
        {
            if (strMessage == oldIROCVMessage) return;
            oldIROCVMessage = strMessage;
            string dir = string.Empty;
            dir = _Constant.LOG_PATH;
            dir += System.DateTime.Now.ToString("yyyyMMdd") + "\\" + "DB" + "\\";
            if (Directory.Exists(dir) == false) Directory.CreateDirectory(dir);
            string filename = dir + "DB_" + DateTime.Now.ToString("yyMMdd-HH") + ".log";
            string strMonitoring = string.Empty;

            strMonitoring = strMessage;

            strMonitoring = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "\t" + strMonitoring;
            if (System.IO.File.Exists(filename) == false) FileWrite(filename, strMonitoring);
            else FileAppend(filename, strMonitoring);

        }

        //* MON 응답의 전체 길이가 2859 - 대흥시스텍 프리차져
        private string ParseMon2859(string sMessage)
        {
            string sMon = "", code = "", tmpStr = "", msg = "";
            int nvolt = 0, ncurr = 0, ncapa = 0;
            sMon = sMessage.Substring(32);
            int iLength = sMon.Length - 10;
            int cnt = 1, nChannel = 1;
            while (iLength > cnt)
            {
                tmpStr = sMon.Substring(0, 11);
                sMon = sMon.Remove(0, 11);
                //sMon = sMon.Substring(11);
                cnt += 11;

                char[] charValues = tmpStr.ToCharArray();
                code = String.Format("{0:X}", Convert.ToInt32(charValues[0]));
                nvolt = (charValues[1] << 24) + (charValues[2] << 16) + (charValues[3] << 8) + charValues[4];
                ncurr = (charValues[5] << 24) + (charValues[6] << 16) + (charValues[7] << 8) + charValues[8];
                ncapa = (charValues[9] << 8) + (int)charValues[10];

                if (nChannel > 125)
                    code = code + "";

                msg = msg + "CH-" + nChannel.ToString() + "-" + code + "-" + nvolt + "-" + ncurr + "-" + ncapa + "\t";
                nChannel++;
            }


            return msg;
        }
        #endregion Save Log

        #region Save NG Info
        public void SaveNGInfo(int stageno, bool bIncrease, int[] ngUse, int[] ngCount)
        {
            string filename = _Constant.BIN_PATH + "RemeasureInfo_" + (stageno + 1) + ".inf";

            string ch_no = string.Empty;
            try
            {
                for (int nIndex = 0; nIndex < _Constant.ChannelCount; nIndex++)
                {
                    ch_no = "CH_" + (nIndex + 1).ToString("D2");
                    if (bIncrease == true)
                    {
                        ngUse[nIndex] += 1;
                        ngCount[nIndex] += 1;
                    }

                    saveConfig(filename, ch_no, "USE", ngUse[nIndex].ToString());
                    saveConfig(filename, ch_no, "NG", ngCount[nIndex].ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }
        public void ReadNGInfo(int stageno, out int[] ngUse, out int[] ngCount)
        {
            ngUse = new int[_Constant.ChannelCount];
            ngCount = new int[_Constant.ChannelCount];
            string filename = _Constant.BIN_PATH + "RemeasureInfo_" + (stageno + 1) + ".inf";
            string ch_no = string.Empty;
            try
            {
                if (File.Exists(filename))
                {
                    for (int nIndex = 0; nIndex < _Constant.ChannelCount; nIndex++)
                    {
                        ch_no = "CH_" + (nIndex + 1).ToString("D2");
                        ngUse[nIndex] = Convert.ToInt32(readConfig(filename, ch_no, "USE"));
                        ngCount[nIndex] = Convert.ToInt32(readConfig(filename, ch_no, "NG"));
                    }
                }
                else
                {
                    for (int nIndex = 0; nIndex < _Constant.ChannelCount; nIndex++)
                    {
                        ngUse[nIndex] = 0;
                        ngCount[nIndex] = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }
        #endregion

        #region Grid View
        /// <summary>
        /// 셀의 위치 변경 - ChangeMap
        /// </summary>
        /// <param name="nIndex">셀 번호 0 - 255</param>
        /// <param name="startposition">시작점 (left top, right top, left bottom, right bottom</param>
        /// <param name="rowIndex">row 번호</param>
        /// <param name="colIndex">column 번호</param>
        public void ChangeMapToGridView(int nIndex, int startposition, out int rowIndex, out int colIndex)
        {
            rowIndex = 0;
            colIndex = 0;
            switch (startposition)
            {
                case 1: // start at left top to right top
                    rowIndex = nIndex / 16;
                    colIndex = nIndex % 16;
                    break;
                case 5: // start at left top to left bottom
                    rowIndex = nIndex % 16;
                    colIndex = nIndex / 16;
                    break;
                case 2: // start at right top to left top
                    rowIndex = nIndex / 16;
                    colIndex = 15 - nIndex % 16;
                    break;
                case 6: // start at right top to right bottom
                    rowIndex = nIndex % 16;
                    colIndex = 15 - nIndex / 16;
                    break;
                case 3: // start at left bottom to right bottom
                    rowIndex = 15 - nIndex / 16;
                    colIndex = nIndex % 16;
                    break;
                case 7: // start at left bottom to left top
                    rowIndex = 15 - nIndex % 16;
                    colIndex = nIndex / 16;
                    break;
                case 4: // start at right bottom to left bottom
                    rowIndex = 15 - nIndex / 16;
                    colIndex = 15 - nIndex % 16;
                    break;
                case 8: // start at right bottom to right top
                    rowIndex = 15 - nIndex % 16;
                    colIndex = 15 - nIndex / 16;
                    break;
                default:
                    break;
            }
        }

        public void ChangeMapToGridView(enumStartPosition enumStartPos, int nIndex, out int rowIndex, out int colIndex)
        {
            rowIndex = 0;
            colIndex = 0;
            //int startposition = _Constant.StartPosition;
            switch (enumStartPos)
            {
                case enumStartPosition.LEFTTOPTORIGHT: // start at left top to right top
                    rowIndex = nIndex / 16;
                    colIndex = nIndex % 16;
                    break;
                case enumStartPosition.LEFTTOPTOBELOW: // start at left top to left bottom
                    rowIndex = nIndex % 16;
                    colIndex = nIndex / 16;
                    break;
                case enumStartPosition.RIGHTTOPTOLEFT: // start at right top to left top
                    rowIndex = nIndex / 16;
                    colIndex = 15 - nIndex % 16;
                    break;
                case enumStartPosition.RIGHTTOPTOBELOW: // start at right top to right bottom
                    rowIndex = nIndex % 16;
                    colIndex = 15 - nIndex / 16;
                    break;
                case enumStartPosition.LEFTBOTTOMTORIGHT: // start at left bottom to right bottom
                    rowIndex = 15 - nIndex / 16;
                    colIndex = nIndex % 16;
                    break;
                case enumStartPosition.LEFTBOTTOMETOABOVE: // start at left bottom to left top
                    rowIndex = 15 - nIndex % 16;
                    colIndex = nIndex / 16;
                    break;
                case enumStartPosition.RIGHTBOTTOMTOLEFT: // start at right bottom to left bottom
                    rowIndex = 15 - nIndex / 16;
                    colIndex = 15 - nIndex % 16;
                    break;
                case enumStartPosition.RIGHTBOTTOMTOABOVE: // start at right bottom to right top
                    rowIndex = 15 - nIndex % 16;
                    colIndex = 15 - nIndex / 16;
                    break;
                default:
                    break;
            }
        }
        public void ChangeMapToGridView64(enumStartPosition enumStartPos, int nIndex, out int rowIndex, out int colIndex)
        {
            rowIndex = 0;
            colIndex = 0;
            //int startposition = _Constant.StartPosition;
            switch (enumStartPos)
            {
                case enumStartPosition.LEFTTOPTORIGHT: // start at left top to right top
                    rowIndex = nIndex / 8;
                    colIndex = nIndex % 8;
                    break;
                case enumStartPosition.LEFTTOPTOBELOW: // start at left top to left bottom
                    rowIndex = nIndex % 8;
                    colIndex = nIndex / 8;
                    break;
                case enumStartPosition.RIGHTTOPTOLEFT: // start at right top to left top
                    rowIndex = nIndex / 8;
                    colIndex = 7 - nIndex % 8;
                    break;
                case enumStartPosition.RIGHTTOPTOBELOW: // start at right top to right bottom
                    rowIndex = nIndex % 8;
                    colIndex = 7 - nIndex / 8;
                    break;
                case enumStartPosition.LEFTBOTTOMTORIGHT: // start at left bottom to right bottom
                    rowIndex = 7 - nIndex / 8;
                    colIndex = nIndex % 8;
                    break;
                case enumStartPosition.LEFTBOTTOMETOABOVE: // start at left bottom to left top
                    rowIndex = 7 - nIndex % 8;
                    colIndex = nIndex / 8;
                    break;
                case enumStartPosition.RIGHTBOTTOMTOLEFT: // start at right bottom to left bottom
                    rowIndex = 7 - nIndex / 8;
                    colIndex = 7 - nIndex % 8;
                    break;
                case enumStartPosition.RIGHTBOTTOMTOABOVE: // start at right bottom to right top
                    rowIndex = 7 - nIndex % 8;
                    colIndex = 7 - nIndex / 8;
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region Keysight Controller config
        public void DeleteController(int stageno)
        {
            /// init file open
            /// file.Load(filename)을 하면 기존 값을 그대로 가져 오기 때문에 
            /// fileOld, fileNew 2개를 선언하고 fileNew로 옮김. 
            /// stage 이름으로 정렬 또는 삭제를 하기 위해서, 정렬 또는 삭제를 안해도 되면 하나로 사용해도 됨.
            string filename = _Constant.CONFIG_FILE;
            SoftCircuits.IniFileParser.IniFile fileOld = new SoftCircuits.IniFileParser.IniFile();
            SoftCircuits.IniFileParser.IniFile fileNew = new SoftCircuits.IniFileParser.IniFile();

            /// 기존 내용 읽어오기
            /// 
            fileOld.Load(filename);
            foreach (var sec in fileOld.GetSections())
            {
                if(sec.ToString() != ("STAGE" + stageno.ToString("D3")))
                {
                    foreach (var ini in fileOld.GetSectionSettings(sec))
                    {
                        fileNew.SetSetting(sec, ini.Name, ini.Value);
                    }
                }
            }

            /// 새로운 파일에 저장
            fileNew.Save(filename);
        }
        public void UpdateControllerInfo(Controller controller)
        {
            /// controller info 불러오기
            string filename = _Constant.CONFIG_FILE;
            List<Controller> controllerList = ReadControllerInfo();

            /// controller info 수정
            for (int i = 0; i < controllerList.Count; i++)
            {
                if (controllerList[i].STAGENO == controller.STAGENO)
                    controllerList.Remove(controllerList[i]);
            }

            /// controller info 추가 및 정렬
            controllerList.Add(controller);
            controllerList.Sort((x, y) => x.STAGENO.CompareTo(y.STAGENO));

            SaveControllerInfo(controllerList);
        }
        public void AddControllerInfo(Controller controller)
        {
            /// add controller info to controller list
            /// 
            List<Controller> controllerList = ReadControllerInfo();
            
            /// 기존에 stageno가 있으면 업데이트, 없으면 추가
            controllerList.Add(controller);
            controllerList.Sort((x, y) => x.STAGENO.CompareTo(y.STAGENO));

            SaveControllerInfo(controllerList);
        }
        public void SaveControllerInfo(List<Controller> controllerList)
        {
            /// init file open
            /// file.Load(filename)을 하면 기존 값을 그대로 가져 오기 때문에 
            /// fileOld, fileNew 2개를 선언하고 fileNew로 옮김. 
            /// stage 이름으로 정렬 또는 삭제를 하기 위해서, 정렬 또는 삭제를 안해도 되면 하나로 사용해도 됨.
            string filename = _Constant.CONFIG_FILE;
            SoftCircuits.IniFileParser.IniFile fileOld = new SoftCircuits.IniFileParser.IniFile();
            SoftCircuits.IniFileParser.IniFile fileNew = new SoftCircuits.IniFileParser.IniFile();

            /// 기존 내용 읽어오기
            /// controller (stage)관련 정보는 제외하고 나머지 정보는 그대로 저장.
            fileOld.Load(filename);
            foreach(var sec in fileOld.GetSections())
            {
                if (sec.ToString().Contains("STAGE") == false)
                {
                    foreach (var ini in fileOld.GetSectionSettings(sec))
                    {
                        fileNew.SetSetting(sec, ini.Name, ini.Value);
                    }
                }
            }
            
            /// save controller list to ini file
            /// 새로운 파일에 저장
            string section = string.Empty;
            foreach (Controller tmpController in controllerList)
            {
                section = "STAGE" + tmpController.STAGENO.ToString("D3");
                fileNew.SetSetting(section, "STAGENO", (tmpController.STAGENO));
                fileNew.SetSetting(section, "IPADDRESS", tmpController.IPADDRESS);
                fileNew.SetSetting(section, "PORT", tmpController.PORT);
                fileNew.SetSetting(section, "PORT2", tmpController.PORT2);
                fileNew.SetSetting(section, "MAXCHANNEL", tmpController.MAXCHANNEL);
                fileNew.Save(filename);
            }
        }
        public List<Controller> ReadControllerInfo()
        {
            List<Controller> controllerList = new List<Controller>();
            Controller controller;
            string filename = _Constant.CONFIG_FILE;
            SoftCircuits.IniFileParser.IniFile file = new SoftCircuits.IniFileParser.IniFile();
            try
            {
                file.Load(filename);

                foreach (var section in file.GetSections())
                {
                    if (section.Contains("STAGE"))
                    {
                        controller = new Controller();
                        controller.STAGENO = TryParseInt(file.GetSetting(section, "STAGENO"), 0);
                        controller.IPADDRESS = file.GetSetting(section, "IPADDRESS");
                        controller.PORT = TryParseInt(file.GetSetting(section, "PORT"), 0);
                        controller.PORT2 = TryParseInt(file.GetSetting(section, "PORT2"), 0);
                        controller.MAXCHANNEL = TryParseInt(file.GetSetting(section, "MAXCHANNEL"), 0);

                        controllerList.Add(controller);
                    }
                }

                controllerList.Sort((x, y) => x.STAGENO.CompareTo(y.STAGENO));
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return controllerList;
        }
        public Controller ReadControllerInfo(string stageno)
        {
            Controller controller;
            string filename = _Constant.CONFIG_FILE;
            SoftCircuits.IniFileParser.IniFile file = new SoftCircuits.IniFileParser.IniFile();
            try
            {
                file.Load(filename);

                foreach (var section in file.GetSections())
                {
                    if (section.ToString().Contains("STAGE"))
                    {
                        controller = new Controller();
                        controller.STAGENO = int.Parse(file.GetSetting(section, "STAGENO"));
                        controller.IPADDRESS = file.GetSetting(section, "IPADDRESS");
                        controller.PORT = int.Parse(file.GetSetting(section, "PORT"));
                        controller.PORT2 = int.Parse(file.GetSetting(section, "PORT2"));
                        controller.MAXCHANNEL = int.Parse(file.GetSetting(section, "MAXCHANNEL"));

                        if (controller.STAGENO.ToString() == stageno) return controller;
                    }
                }
            }
            catch(Exception ex) { Console.WriteLine(ex.ToString()); }  

            return null;
        }
        #endregion Keysight Controller config

        #region Charging/Discharging Recipe
        public void SaveRecipe(string recipe_no, List<Recipe> recipeList)
        {
            /// init file open
            /// 
            string filename = _Constant.BIN_PATH + "RECIPE_" + recipe_no + ".inf";
            SoftCircuits.IniFileParser.IniFile file = new SoftCircuits.IniFileParser.IniFile();

            /// save recipe list to ini file
            /// 
            string section = string.Empty;
            foreach (Recipe recipe in recipeList)
            {
                section = recipe.recipemethod;
                file.SetSetting(section, "ORDERNO", recipe.orderno);
                file.SetSetting(section, "TIME", recipe.time);
                file.SetSetting(section, "CURRENT", recipe.current);
                file.SetSetting(section, "VOLTAGE", recipe.voltage);
                file.Save(filename);
            }
        }
        public List<Recipe> ReadRecipe(string recipe_no)
        {
            List<Recipe> recipeList = new List<Recipe>();
            string filename = _Constant.BIN_PATH + "RECIPE_" + recipe_no + ".inf";
            SoftCircuits.IniFileParser.IniFile file = new SoftCircuits.IniFileParser.IniFile();

            try
            {
                file.Load(filename);

                string recipeno = string.Empty;
                foreach (var section in file.GetSections())
                {
                    Recipe recipe = new Recipe();
                    recipe.orderno = file.GetSetting(section, "ORDERNO");
                    recipe.recipemethod = section.ToString();
                    recipe.time = file.GetSetting(section, "TIME");
                    recipe.current = file.GetSetting(section, "CURRENT");
                    recipe.voltage = file.GetSetting(section, "VOLTAGE");

                    recipeList.Add(recipe);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return recipeList;
        }
        #endregion Charging/Discharging Recipe

        #region DCIR Recipe

        public void UpdateRecipeDcir(RecipeDcir recipe)
        {
            /// recipe 불러오기
            /// 
            string filename = _Constant.RECIPE_FILE;
            List<RecipeDcir> recipeList = ReadRecipeDcir();

            /// recipe 수정
            /// 
            for(int i = 0; i < recipeList.Count; i++)
            {
                if (recipeList[i].RecipeNo == recipe.RecipeNo)
                    recipeList.Remove(recipeList[i]);
            }

            /// recipe 추가 및 정렬
            /// 
            recipeList.Add(recipe);
            recipeList.Sort((x, y) => x.RecipeNo.CompareTo(y.RecipeNo));

            /// save recipe list to recipe file
            /// 
            SaveRecipeDcir(recipeList);
        }
        public void AddRecipeDcir(RecipeDcir recipe)
        {
            /// add recipe to recipe list
            /// 
            
            List<RecipeDcir> recipeList = ReadRecipeDcir();
            recipeList.Add(recipe);

            /// save recipe list to recipe file
            /// 
            SaveRecipeDcir(recipeList);
        }
        public void SaveRecipeDcir(List<RecipeDcir> recipeList)
        {
            /// init file open
            /// 

            string filename = _Constant.RECIPE_FILE;
            SoftCircuits.IniFileParser.IniFile file = new SoftCircuits.IniFileParser.IniFile();

            /// save recipe list to ini file
            /// 

            int recipeno = 0;
            string section = string.Empty;
            foreach (RecipeDcir tmpRecipe in recipeList)
            {
                recipeno = recipeno + 1;
                section = "Recipe_" + recipeno;
                file.SetSetting(section, "RecipeNo", recipeno);
                file.SetSetting(section, "MaxVoltLimit", tmpRecipe.MaxVoltLimit);
                file.SetSetting(section, "MinVoltLimit", tmpRecipe.MinVoltLimit);
                file.SetSetting(section, "PauseBeforeFirstPulse", tmpRecipe.PauseBeforeFirstPulse);
                file.SetSetting(section, "FirstPulseLevel", tmpRecipe.FirstPulseLevel);
                file.SetSetting(section, "FirstPulseWidth", tmpRecipe.FirstPulseWidth);
                file.SetSetting(section, "PauseAfterFirstPulse", tmpRecipe.PauseAfterFirstPulse);
                file.SetSetting(section, "SecondPulseLevel", tmpRecipe.SecondPulseLevel);
                file.SetSetting(section, "SecondPulseWidth", tmpRecipe.SecondPulseWidth);
                file.SetSetting(section, "PauseAfterSecondPulse", tmpRecipe.PauseAfterSecondPulse);
                file.Save(filename);
            }
        }
        public List<RecipeDcir> ReadRecipeDcir()
        {
            List<RecipeDcir> recipeList = new List<RecipeDcir>();
            RecipeDcir recipe;
            string filename = _Constant.RECIPE_FILE;
            SoftCircuits.IniFileParser.IniFile file = new SoftCircuits.IniFileParser.IniFile();

            try
            {
                file.Load(filename);

                string recipeno = string.Empty;
                foreach (var section in file.GetSections())
                {
                    if (section.Contains("Recipe"))
                    {
                        recipe = new RecipeDcir();
                        recipe.RecipeNo = int.Parse(file.GetSetting(section, "RecipeNo"));
                        recipe.MaxVoltLimit = file.GetSetting(section, "MaxVoltLimit");
                        recipe.MinVoltLimit = file.GetSetting(section, "MinVoltLimit");
                        recipe.PauseBeforeFirstPulse = file.GetSetting(section, "PauseBeforeFirstPulse");
                        recipe.FirstPulseLevel = file.GetSetting(section, "FirstPulseLevel");
                        recipe.FirstPulseWidth = file.GetSetting(section, "FirstPulseWidth");
                        recipe.PauseAfterFirstPulse = file.GetSetting(section, "PauseAfterFirstPulse");
                        recipe.SecondPulseLevel = file.GetSetting(section, "SecondPulseLevel");
                        recipe.SecondPulseWidth = file.GetSetting(section, "SecondPulseWidth");
                        recipe.PauseAfterSecondPulse = file.GetSetting(section, "PauseAfterSecondPulse");

                        recipeList.Add(recipe);
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return recipeList;
        }
        public RecipeDcir ReadRecipeDcir(string recipeno)
        {
            RecipeDcir recipe;
            string filename = _Constant.RECIPE_FILE;
            SoftCircuits.IniFileParser.IniFile file = new SoftCircuits.IniFileParser.IniFile();
            file.Load(filename);

            foreach (var section in file.GetSections())
            {
                recipe = new RecipeDcir();
                recipe.RecipeNo = int.Parse(file.GetSetting(section, "RecipeNo"));
                recipe.MaxVoltLimit = file.GetSetting(section, "MaxVoltLimit");
                recipe.MinVoltLimit = file.GetSetting(section, "MinVoltLimit");
                recipe.PauseBeforeFirstPulse = file.GetSetting(section, "PauseBeforeFirstPulse");
                recipe.FirstPulseLevel = file.GetSetting(section, "FirstPulseLevel");
                recipe.FirstPulseWidth = file.GetSetting(section, "FirstPulseWidth");
                recipe.PauseAfterFirstPulse = file.GetSetting(section, "PauseAfterFirstPulse");
                recipe.SecondPulseLevel = file.GetSetting(section, "SecondPulseLevel");
                recipe.SecondPulseWidth = file.GetSetting(section, "SecondPulseWidth");
                recipe.PauseAfterSecondPulse = file.GetSetting(section, "PauseAfterSecondPulse");

                if (recipe.RecipeNo.ToString() == recipeno) return recipe;
            }

            return null;
        }
        #endregion DCIR Recipe

        #region Maria DB
        public MariaDBConfig ReadDBConfig()
        {
            string filename = _Constant.CONFIG_FILE;
            string controller_index = string.Empty;
            mariaConfig = MariaDBConfig.GetInstance();

            try
            {
                if (File.Exists(filename))
                {
                    mariaConfig.DBIPADDRESS = readConfig(filename, _Constant.db_name, _Constant.host_name);
                    mariaConfig.DBNAME = readConfig(filename, _Constant.db_name, _Constant.db_use_name);
                    mariaConfig.DBPORT = readConfig(filename, _Constant.db_name, _Constant.port_name);
                    mariaConfig.DBUSER = readConfig(filename, _Constant.db_name, _Constant.db_user_name);
                    mariaConfig.DBPWD = readConfig(filename, _Constant.db_name, _Constant.db_pwd_name);

                    mariaConfig.ISREAD = true;
                }
                else
                {
                    mariaConfig.ISREAD = false;
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                SaveCDCLog(0, "Read Config File Error!");
                mariaConfig.ISREAD = false;
            }

            return mariaConfig;
        }
        public void SaveDBConfig(MariaDBConfig mariaConfig)
        {
            string filename = _Constant.CONFIG_FILE;
            string controller_index = string.Empty;

            mariaConfig = MariaDBConfig.GetInstance();

            try
            {
                saveConfig(filename, _Constant.db_name, _Constant.host_name, mariaConfig.DBIPADDRESS);
                saveConfig(filename, _Constant.db_name, _Constant.db_use_name, mariaConfig.DBNAME);
                saveConfig(filename, _Constant.db_name, _Constant.port_name, mariaConfig.DBPORT.ToString());
                saveConfig(filename, _Constant.db_name, _Constant.db_user_name, mariaConfig.DBUSER);
                saveConfig(filename, _Constant.db_name, _Constant.db_pwd_name, mariaConfig.DBPWD);
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                SaveCDCLog(0, "Save Config File Error!");
            }
        }
        #endregion

        #region Set Value to Control
        public void DeleteRowsGridView(DataGridView dgv)
        {
            int rowCount = dgv.Rows.Count;
            try
            {
                for (int nIndex = 0; nIndex < rowCount; nIndex++)
                    dgv.Rows.RemoveAt(0);
                dgv.Refresh();
            }catch(Exception ex)
            { Console.WriteLine(ex.ToString()); }
        }
        public void SetValueToGridView(string value, int nRow, int nCol, DataGridView dgv)
        {
            if (dgv.InvokeRequired)
            {
                // 작업쓰레드인 경우
                dgv.BeginInvoke(new Action(() => dgv.Rows[nRow].Cells[nCol].Value = value));
            }
            else
            {
                // UI 쓰레드인 경우
                dgv.Rows[nRow].Cells[nCol].Value = value;
            }
        }
        public void SetColorToGridView(Color clr, int nRow, int nCol, DataGridView dgv)
        {
            if (dgv.InvokeRequired)
            {
                dgv.Rows[nRow].Cells[nCol].Style.BackColor = clr;
            }
            else
            {
                dgv.Rows[nRow].Cells[nCol].Style.BackColor = clr;
            }
        }
        public void DeleteRowsGridView(RadGridView gv)
        {
            int rowCount = gv.Rows.Count;
            try
            {
                for (int nIndex = 0; nIndex < rowCount; nIndex++)
                    gv.Rows.RemoveAt(0);
                gv.Refresh();
            }
            catch (Exception ex)
            { Console.WriteLine(ex.ToString()); }
        }
        public void SetValueToGridView(string value, int nRow, int nCol, RadGridView gv)
        {
            if (gv.InvokeRequired)
            {
                // 작업쓰레드인 경우
                gv.BeginInvoke(new Action(() => gv.Rows[nRow].Cells[nCol].Value = value));
            }
            else
            {
                // UI 쓰레드인 경우
                gv.Rows[nRow].Cells[nCol].Value = value;
            }
        }
        public void SetColorToGridView(Color clr, int nRow, int nCol, RadGridView gv)
        {
            if (gv.InvokeRequired)
            {
                gv.Rows[nRow].Cells[nCol].Style.BackColor = clr;
            }
            else
            {
                gv.Rows[nRow].Cells[nCol].Style.BackColor = clr;
            }
        }
        public void SetValueToTextBox(TextBox tb, string value)
        {
            if (tb.InvokeRequired)
            {
                // 작업쓰레드인 경우
                tb.BeginInvoke(new Action(() => tb.AppendText(value + Environment.NewLine + Environment.NewLine)));
            }
            else
            {
                // UI 쓰레드인 경우
                tb.AppendText(value + Environment.NewLine + Environment.NewLine);
            }
        }
        public void SetValueToLabel(Label lbl, string value)
        {
            if (lbl.InvokeRequired)
            {
                // 작업쓰레드인 경우
                lbl.BeginInvoke(new Action(() => lbl.Text = value));
            }
            else
            {
                // UI 쓰레드인 경우
                lbl.Text = value;
            }
        }
        public void SetValueToLabel(Label lbl, string value, Color? clr = null)
        {
            if (lbl.InvokeRequired)
            {
                // 작업쓰레드인 경우
                lbl.BeginInvoke(new Action(() => lbl.Text = value));
                lbl.BackColor = clr.HasValue ? clr.Value : Color.White;
                if (lbl.BackColor == Color.Red) lbl.ForeColor = Color.White;
                else lbl.ForeColor = Color.Black;
            }
            else
            {
                // UI 쓰레드인 경우
                lbl.Text = value;
                lbl.BackColor = clr.HasValue ? clr.Value : Color.White;
                if (lbl.BackColor == Color.Red) lbl.ForeColor = Color.White;
                else lbl.ForeColor = Color.Black;
            }
        }
        public void SetValueToLabel2(Label lbl, string value, Color? clr = null)
        {
            if (lbl.InvokeRequired)
            {
                // 작업쓰레드인 경우
                lbl.BeginInvoke(new Action(() => lbl.Text = value));
                lbl.BackColor = clr.HasValue ? clr.Value : Color.White;
                if (lbl.BackColor == Color.Red) lbl.ForeColor = Color.White;
                else if (lbl.BackColor == Color.White) lbl.ForeColor = Color.Black;
            }
            else
            {
                // UI 쓰레드인 경우
                lbl.Text = value;
                lbl.BackColor = clr.HasValue ? clr.Value : Color.White;
                if (lbl.BackColor == Color.Red) lbl.ForeColor = Color.White;
                else if (lbl.BackColor == Color.White) lbl.ForeColor = Color.Black;
            }
        }
        #endregion

        #region 문자열 변환
        public string ByteToString(byte[] strByte)
        {
            string str = Encoding.Default.GetString(strByte);
            return str;
        }
        // String을 바이트 배열로 변환 
        public byte[] StringToByte(string str)
        {
            byte[] StrByte = Encoding.UTF8.GetBytes(str);
            return StrByte;
        }
        public byte[] HexToByte(string str)
        {
            byte[] byteArray = new byte[str.Length / 2];

            for (int i = 0; i < str.Length; i += 2)
            {
                byteArray[i / 2] = Convert.ToByte(str.Substring(i, 2), 16);
            }

            return byteArray;
        }

        #endregion
    }
}
