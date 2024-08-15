using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DHS.EQUIPMENT2.CDC;
using DHS.EQUIPMENT2.Common;
using DHS.EQUIPMENT2.Equipment;

namespace DHS.EQUIPMENT2
{
    class CDProcess
    {
        Util util;
        ConfigForm config;
        MeasureInfoForm measureinfo;
        ManualModeControl manualmode;
        CalibrationControl calibrationmode;
        DCIRControl dcirmode;
        MariaDB mariadb = null;
        MariaDBConfig mariaConfig;

        TRAYINFO[] nTrayInfo = new TRAYINFO[_Constant.ControllerCount];
        ControllerSenData[] nSenData = new ControllerSenData[_Constant.ControllerCount];
        KeysightCalData[] nCalData = new KeysightCalData[_Constant.ControllerCount];

        /// <summary>
        /// Keysight 용 controller 접속
        /// </summary>
        /// 
        KeysightController[] keysightcontrollers = new KeysightController[_Constant.ControllerCount];
        private bool[] _bKeysightControllerConnected = new bool[_Constant.ControllerCount];
        public bool[] KEYSIGHTCONTROLLERCONNECTED { get => _bKeysightControllerConnected; set => _bKeysightControllerConnected = value; }
        private bool bContConnected;

        /// <summary>
        /// 
        /// </summary>
        private Timer _tmrContConnection = null;
        private Timer _tmrGetData = null;
        private Timer _tmrCalData = null;
        private int _iHeaderCount;
        public readonly int MONLENGTH;
        public readonly int SENLENGTH;

        #region delegate
        public delegate void delegateReport_AddPanel(DoubleBufferedPanel pnl);
        public event delegateReport_AddPanel OnAddPanel = null;
        protected void RaiseOnAddPanel(DoubleBufferedPanel pnl)
        {
            if (OnAddPanel != null)
            {
                OnAddPanel(pnl);
            }
        }
        public delegate void SetControllerInfo(int stageno, string ip, string port, string stagename, bool bBtConnected);
        public event SetControllerInfo OnSetControllerInfo = null;
        protected void RaiseOnSetControllerInfo(int stageno, string ip, string port, string stagename, bool bBtConnected)
        {
            if (OnSetControllerInfo != null)
            {
                OnSetControllerInfo(stageno, ip, port, stagename, bBtConnected);
            }
        }
        #endregion

        public static CDProcess cdprocess = null;

        public int HEADERCOUNT { get => _iHeaderCount; set => _iHeaderCount = value; }

        public static CDProcess GetInstance()
        {
            if (cdprocess == null) cdprocess = new CDProcess();
            return cdprocess;
        }
        public CDProcess()
        {
            cdprocess = this;
            util = new Util();

            //* 64(channel) * 4(status, curr, volt, capa) + 1(DATETIME) = 257
            //* 256(channel) * 4(status, curr, volt, capa) + 1(DATETIME) = 1025
            if (_Constant.ChannelCount == 64) _iHeaderCount = 257;
            else if (_Constant.ChannelCount == 256) _iHeaderCount = 1025;

            MakeFolder();
            
            /// Config File
            config = ConfigForm.GetInstance();
            config.OnChangeServerSetting += _Config_ChangeServer;
            config.ReadDBConfig();

            /// Recipe File (DCIR)
            /// read recipe file
            
            //* Charger Form / TrayInfo / SEN
            for(int nIndex = 0; nIndex < _Constant.ControllerCount; nIndex++)
            {
                nTrayInfo[nIndex] = TRAYINFO.GetInstance(nIndex);

                nCalData[nIndex] = KeysightCalData.GetInstance(nIndex);

                //* Keysight Controller
                keysightcontrollers[nIndex] = KeysightController.GetInstance(nIndex);
                keysightcontrollers[nIndex].OnKeysightResult += _KeysightResult;
            }

            //* Measure Info Form
            measureinfo = MeasureInfoForm.GetInstance();
            measureinfo.OnSAVEFINDATA += _MeasureInfo_SaveFinData;

            //* Manual Mode Form
            manualmode = ManualModeControl.GetInstance();
            manualmode.OnKeysightCommand += _ManualMode_KeysightCommand;
            manualmode.OnStartCharging += _ManualMode_StartCharging;
            manualmode.OnStopCharging += _ManualMode_StopCharging;
            manualmode.OnRESET += _ManualMode_RESET;

            //* DCIR Form
            dcirmode = DCIRControl.GetInstance();
            dcirmode.OnStartDCIR += _DCIRMode_StartDCIRAsync;
            dcirmode.OnStopDCIR += _DCIRMode_StopDCIR;
            dcirmode.OnRESET += _DCIRMode_RESET;

            //* Calibration Mode Form
            calibrationmode = CalibrationControl.GetInstance();
            calibrationmode.OnStartCalibration += _CalMode_StartCalibration;
            calibrationmode.OnStopCalibration += _CalMode_StopCalibration;

            /// Connect Controller
            //ControllerConnect();
            _tmrContConnection = new Timer();
            _tmrContConnection.Interval = 3000;
            _tmrContConnection.Tick += new EventHandler(ContConnectionTimer_Tick);
            //_tmrContConnection.Enabled = true;

        }

        public void Close()
        {

        }

        private void _Config_ChangeServer(string ipaddr, int port)
        {
            util.ReadDBConfig();
            MariaDBConnect();
        }

        #region Folder 
        private void MakeFolder()
        {
            if (Directory.Exists(_Constant.APP_PATH) == false) Directory.CreateDirectory(_Constant.APP_PATH);
            if (Directory.Exists(_Constant.BIN_PATH) == false) Directory.CreateDirectory(_Constant.BIN_PATH);
            if (Directory.Exists(_Constant.DATA_PATH) == false) Directory.CreateDirectory(_Constant.DATA_PATH);
            if (Directory.Exists(_Constant.LOG_PATH) == false) Directory.CreateDirectory(_Constant.LOG_PATH);
        }
        #endregion

        #region Connect Controller
        public void ContConnectionTimer(bool bEnabled)
        {
            if (bEnabled == true)
            {
                _tmrContConnection.Enabled = true;
            }
            else
            {
                _tmrContConnection.Enabled = false;
            }
        }
        private void ContConnectionTimer_Tick(object sender, EventArgs e)
        {
            //* Keysight Controller Connection
            string ip , port , stagename , port1 , port2;
            ip = port = stagename = port1 = port2 = string.Empty;
            int maxContCount = _Constant.ControllerCount;

            List<Controller> contList = util.ReadControllerInfo();
            if(contList.Count == 0) return;

            for (int nIndex = 0; nIndex < maxContCount; nIndex++)
            {
                if (nIndex >= contList.Count) break;

                ip = contList[nIndex].IPADDRESS;
                //* 현재 연결된 port 값 불러오기. 이 port가 연결이 안되면 다른 port로 연결하기 위해
                if (keysightcontrollers[nIndex] != null)
                    port = keysightcontrollers[nIndex].PORT.ToString();
                port1 = contList[nIndex].PORT.ToString();
                port2 = contList[nIndex].PORT2.ToString();
                stagename = "STAGE " + contList[nIndex].STAGENO;

                if (keysightcontrollers[nIndex].ConnectionState == enumConnectionState.Connected)
                {
                    keysightcontrollers[nIndex].BTCONNECTED = true;
                    keysightcontrollers[nIndex].RECONNECTCOUNT = 0;
                }
                else
                {
                    keysightcontrollers[nIndex].BTCONNECTED = false;
                    keysightcontrollers[nIndex].RECONNECTCOUNT += 1;
                    if (keysightcontrollers[nIndex].RECONNECTCOUNT > 5)
                    {
                        if (port1 == port)
                            keysightcontrollers[nIndex].ChangeSetting(ip, Convert.ToInt32(port2), nIndex);
                        else
                            keysightcontrollers[nIndex].ChangeSetting(ip, Convert.ToInt32(port1), nIndex);

                        keysightcontrollers[nIndex].RECONNECTCOUNT = 0;
                    }
                }

                RaiseOnSetControllerInfo(nIndex, ip, port, stagename, keysightcontrollers[nIndex].BTCONNECTED);
            }
        }
        
        public void ControllerConnect()
        {
            List<Controller> contList = util.ReadControllerInfo();
            int maxContCount = _Constant.ControllerCount;
            string ip = string.Empty;
            int port = 50000;
            int maxchannel = 256;
            int stageno = 0;

            for (int nIndex = 0; nIndex < maxContCount; nIndex++)
            {
                if (nIndex >= contList.Count) break;

                stageno = util.TryParseInt(contList[nIndex].STAGENO.ToString(), 0);
                maxchannel = util.TryParseInt(contList[nIndex].MAXCHANNEL.ToString(), 256);
                ip = contList[nIndex].IPADDRESS;
                port = util.TryParseInt(contList[nIndex].PORT.ToString(), 50000);

                ControllerConnect(nIndex, ip, port, maxchannel);
            }
        }
        public void ControllerConnect(int stageno, string ip, int port, int maxchannel)
        {
            keysightcontrollers[stageno].MAXCHANNEL = maxchannel;
            keysightcontrollers[stageno].IPADDRESS = ip;
            keysightcontrollers[stageno].PORT = port;

            //keysightmondata[stageno].InitData(maxchannel);

            keysightcontrollers[stageno].Close();
            keysightcontrollers[stageno].Open(ip, port, stageno);
        }
        public void CloseController()
        {
            for (int i = 0; i < keysightcontrollers.Length; i++)
            {
                if (keysightcontrollers[i] != null) keysightcontrollers[i].Close();
            }

        }

        #endregion

        #region Keysight Controller Delegate
        private void _KeysightResult(int stageno, string msg)
        {
            try
            {
                //* MON이면 MON_STAGE001 테이블에 저장
                if (msg.Substring(0, 8) == _Constant.MONHEADER && msg.Substring(msg.Length - 2, 2) == _Constant.ETX)
                {
                    //INSERT_MON(stageno, msg);
                    SAVE_MON(stageno, msg);
                }
                //* SEN이면 SEN 테이블에 저장
                else if (msg.Substring(0, 8) == _Constant.SENHEADER && msg.Substring(msg.Length - 2, 2) == _Constant.ETX)
                {
                    //INSERT_SEN(stageno, msg);
                    SAVE_SEN(stageno, msg);
                }
                //* CAL이면 CAL 테이블에 저장
                else if (msg.Substring(0, 8) == _Constant.CALHEADER && msg.Substring(msg.Length - 2, 2) == _Constant.ETX)
                {
                    //NSERT_CAL(stageno, msg);
                    SAVE_CAL(stageno, msg);
                }
                //else
                //    keysightdbsserver.WriteToClient(stageno, msg);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        private void SAVE_MON(int stageno, string mondata)
        {
            /// 처음 18개는 byte => ascii로 변환해야 함.
            /// 변환된 ascii 글자 9개 => @MON0020# : 가운데 숫자는 runcount (=> 0020)
            /// 나머지는 hex 데이터
            /// 마지막 글자는 ; (ETX)
            /// run_count : 4자리
            /// run_time : 16
            /// status : 4
            /// current, voltage, capacity : 8
            
            int mon_length = 9;
            char[] monAscii = toAscii(mondata, mon_length);
            string mondata2 = mondata.Substring(mon_length * 2, mondata.Length - mon_length * 2);

            string run_count = string.Empty; // mondata2[0].Substring(4, 4);
            
            string run_time = string.Empty;
            int runtimelen = 16;

            string status = string.Empty;
            int statuslen = 4 * _Constant.ChannelCount;

            string current = string.Empty;
            int currentlen = 8 * _Constant.ChannelCount;
            
            string voltage = string.Empty;
            int voltagelen = 8 * _Constant.ChannelCount;
            
            string capacity = string.Empty;
            int capacitylen = 8 * _Constant.ChannelCount;

            for(int i = 0; i < mon_length; i++)
            {
                if (i >= 4 && i < 8)
                    run_count += monAscii[i];
            }

            for (int i = 0; i < mondata2.Length; i++)
            {
                if (i >= 0 && i < runtimelen)           //* run_time 16
                    run_time += mondata2[i];
                else if (i >= runtimelen && i < (runtimelen + statuslen))  //* status 4 (channel count * 4)
                    status += mondata2[i];
                else if (i >= (runtimelen + statuslen) && i < (runtimelen + statuslen + currentlen))
                    current += mondata2[i];
                else if (i >= (runtimelen + statuslen + currentlen) && i < (runtimelen + statuslen + currentlen + voltagelen))
                    voltage += mondata2[i];
                else if (i >= (runtimelen + statuslen + currentlen + voltagelen) && i < (runtimelen + statuslen + currentlen + voltagelen + capacitylen))
                    capacity += mondata2[i];
            }

            /// status, current, voltage, capacity 다시 parsing해서 파일에 저장
            /// 
            SET_MONDATA(stageno, run_count, run_time, status, current, voltage, capacity);
        }

        private void SET_MONDATA(int stageno, string run_count, string run_time, string status, string current, string voltage, string capacity)
        {
            keysightcontrollers[stageno].DATETIME = DateTime.Now.ToString("yyyyMMdd HH:mm:ss");
            keysightcontrollers[stageno].RUNCOUNT = util.TryParseInt(run_count, 0);
            keysightcontrollers[stageno].STAGETIME = GetTime(run_time);
            keysightcontrollers[stageno].CHANNELSTATUS = GETSTATUS(status);
            keysightcontrollers[stageno].CHANNELCURRENT = GetCurrent(current);
            keysightcontrollers[stageno].CHANNELVOLTAGE = GetVoltage(voltage);
            keysightcontrollers[stageno].CHANNELCAPACITY = GetCapacity(stageno, 
                keysightcontrollers[stageno].CHANNELCURRENT, keysightcontrollers[stageno].OLDCHANNELCAPACITY);
            keysightcontrollers[stageno].OLDCHANNELCAPACITY = keysightcontrollers[stageno].CHANNELCAPACITY;

            if(nTrayInfo[stageno].CHARGING == true)
                util.SaveMonData(stageno, nTrayInfo[stageno].TRAYID, nTrayInfo[stageno].RECIPENO, keysightcontrollers[stageno]);
        }

        private void SAVE_SEN(int stageno, string sendata)
        {
            /// 모든 데이터가 byte. ascii로 모두 변환해야함.
            /// 처음 14개 글자 : @SENIDL0F8901#
            /// 나머지는 hex 데이터 
            /// 마지막 글자는 ; (ETX)
            /// temperature 4자리 614 -> 307
            /// temperature 3자리 486 -> 243

            int sen_length = 28;
            string[] sens = new string[sen_length];
            char[] senAscii = toAscii(sendata, 29); //* test로 글자수 58개만 사용

            string eqstatus = string.Empty;
            string runcount = string.Empty;
            string connection = string.Empty;
            string servo = string.Empty;
            string stepping1 = string.Empty;
            string stepping2 = string.Empty;
            string temperature = string.Empty;

            //* @SENIDL0F8901#
            for (int nIndex = 0; nIndex < senAscii.Length; nIndex++)
            {
                if (nIndex >= 4 && nIndex < 7)
                    eqstatus += senAscii[nIndex];
                else if (nIndex >= 7 && nIndex < 11)
                    runcount += senAscii[nIndex];
                else if (nIndex >= 11 && nIndex < 13)
                    connection += senAscii[nIndex];
            }

            /// BT Controller에서 받은 SEN 정보는 Data 변수로 저장 (=>keysightcontroller , trayinfo 등)
            ///
            SET_SENDATA(stageno, eqstatus, runcount, connection, servo, stepping1, stepping2, temperature);
        }
        private void SET_SENDATA(int stageno, string eqstatus, string runcount, string connection, string servo, string stepping1, string stepping2, string temperature)
        {
            /// control 보드에서 keysight 장비에 연결이 되어 있는지 확인
            /// 
            if (connection == "01") keysightcontrollers[stageno].BTCONNECTED = true;
            else keysightcontrollers[stageno].BTCONNECTED = false;

            /// runcount 확인
            /// 
            keysightcontrollers[stageno].RUNCOUNT = GetRunCount(runcount);

            /// 키사이트 장비가 동작중인지 확인
            /// 
            if (eqstatus == "RUN")
            {
                if (keysightcontrollers[stageno].SENSTATUS == keysightcontrollers[stageno].OLDSENSTATUS)
                    keysightcontrollers[stageno].SENSTATUS = enumSenStatus.RUN;
                else if (keysightcontrollers[stageno].SENSTATUS != keysightcontrollers[stageno].OLDSENSTATUS)
                    keysightcontrollers[stageno].OLDSENSTATUS = enumSenStatus.RUN;
            }
            else if (eqstatus == "IDL")
            {
                if (keysightcontrollers[stageno].SENSTATUS == keysightcontrollers[stageno].OLDSENSTATUS)
                    keysightcontrollers[stageno].SENSTATUS = enumSenStatus.IDL;
                else if (keysightcontrollers[stageno].SENSTATUS != keysightcontrollers[stageno].OLDSENSTATUS)
                    keysightcontrollers[stageno].OLDSENSTATUS = enumSenStatus.IDL;
            }

            /// TRAYINFO에 충전중인지 아닌지 설정
            /// 
            if (keysightcontrollers[stageno].SENSTATUS == enumSenStatus.RUN)
            {
                nTrayInfo[stageno].CHARGING = true;
            }
            else if (keysightcontrollers[stageno].SENSTATUS == enumSenStatus.IDL)
            {
                nTrayInfo[stageno].CHARGING = false;
            }

            /// bt controller 접속 확인
            /// 
            keysightcontrollers[stageno].CONNECTION = connection;

            /// servo, stepping1, stepping2, temperature parsing해서 파일에 저장
            /// 현재(2024 08 06)는 정보가 없음. 
            ///

            if (keysightcontrollers[stageno].SENSTATUS == enumSenStatus.RUN
                            && keysightcontrollers[stageno].OLDSENSTATUS == enumSenStatus.IDL)
            {
                /// Mon data 저장 시작
                /// 
                //* var result = await Task.Run(() => SaveMonDataAllAsync(stageno, nTrayInfo[stageno]));
            }
            else if (keysightcontrollers[stageno].SENSTATUS == enumSenStatus.IDL
                            && keysightcontrollers[stageno].OLDSENSTATUS == enumSenStatus.RUN)
            {
                /// FIN data 저장
                /// recipe와 각 recipe별 마지막 데이터 저장
                //* SaveFinData(stageno, result, nTrayInfo[stageno]);
            }
        }

        private void SAVE_CAL(int stageno, string caldata)
        {
            //* 모든 데이터가 byte. ascii로 모두 변환해야함.
            //* 예제 : @CAL#+0,RUNNING,OK,1%,Module 1,Channel 5,Elapsed 44 s;

            char[] calAscii = toAscii(caldata, caldata.Length / 2);
            string caltmp = new string(calAscii);

            string lastcaldate = string.Empty;
            string calstartdate = string.Empty;
            string calenddate = string.Empty;

            string[] calstr = caltmp.Split(',');
            string calStatus = calstr[1];       //* RUNNING/IDLE
            string strOkNg = calstr[2];         //* OK/NG
            string calProcess = calstr[3];      //* 1%
            string strModuleNo = calstr[4];     //* Module 1
            string strChannelNo = calstr[5];    //* Channel 5
            string calTime = calstr[6];  //* Elapsed 44 s

            /// Cal 데이터를 파일에 저장
            /// 
            util.SaveCalData(stageno, calTime, calStatus, strOkNg, calProcess, strModuleNo, strChannelNo);
        }
        #endregion Keysight Controller Delegate 

        #region current, voltage, capacity, status, run count, run time, temperature, servo, stepping 등 parsing
        public List<float> GetCurrent(string current)
        {
            List<float> oIMon = new List<float>();
            List<float> oCMon = new List<float>();

            string hex = string.Empty;
            for (int i = 0; i < current.Length / 8; i++)
            {
                hex = current.Substring(i * 8 + 6, 2) + current.Substring(i * 8 + 4, 2)
                    + current.Substring(i * 8 + 2, 2) + current.Substring(i * 8, 2);

                uint num = uint.Parse(hex, System.Globalization.NumberStyles.AllowHexSpecifier);

                byte[] floatVals = BitConverter.GetBytes(num);
                //* 전류
                float f = BitConverter.ToSingle(floatVals, 0);
                oIMon.Add(f);
            }

            return oIMon;
        }
        public List<float> GetVoltage(string voltage)
        {
            List<float> oVMon = new List<float>();

            string hex = string.Empty;
            for (int i = 0; i < voltage.Length / 8; i++)
            {
                hex = voltage.Substring(i * 8 + 6, 2) + voltage.Substring(i * 8 + 4, 2)
                    + voltage.Substring(i * 8 + 2, 2) + voltage.Substring(i * 8, 2);
                uint num = uint.Parse(hex, System.Globalization.NumberStyles.AllowHexSpecifier);

                byte[] floatVals = BitConverter.GetBytes(num);
                float f = BitConverter.ToSingle(floatVals, 0);
                oVMon.Add(f);
            }

            return oVMon;
        }
        /// <summary>
        /// 미들웨어와 클라이언트 프로그램을 합쳤을 경우 capacity 계산을 다시 해야 함
        /// </summary>
        public List<float> GetCapacity(int nIndex, List<float> listCurrent, List<float> listCapacity)
        {
            List<float> oCMon = new List<float>();
            float f = 0.0f;

            for (int i = 0; i < listCurrent.Count(); i++)
            {
                if (nIndex == 0)
                    f = listCurrent[i] * (1.0f / 3600.0f);
                else
                    f = listCapacity[i] + listCurrent[i] * (float)(1.0f / 3600.0f);

                oCMon.Add(f);
            }

            return oCMon;
        }
        public List<float> GetCapacity_original(int nIndex, List<float> listCurrent, List<float> listCapacity)
        {
            List<float> oCMon = new List<float>();
            float f = 0.0f;

            for (int i = 0; i < listCurrent.Count(); i++)
            {
                if (nIndex == 0)
                    f = listCurrent[i] * (1.0f / 3600.0f);
                else
                    f = listCapacity[i] + listCurrent[i] * (float)(1.0f / 3600.0f);

                oCMon.Add(f);
            }

            return oCMon;
        }
        private int[] GETSTATUS(string strStatus)
        {
            int[] status = new int[_Constant.ChannelCount];
            string tempStr = string.Empty;

            if (strStatus.Length != 4 * _Constant.ChannelCount) return status;

            try
            {
                for (int nIndex = 0; nIndex < strStatus.Length; nIndex++)
                {
                    tempStr = strStatus.Substring(nIndex * 4 + 2, 2) + strStatus.Substring(nIndex * 4, 2);
                    status[nIndex] = Convert.ToInt16(tempStr, 16);// util.TryParseInt(tempStr, 0);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return status;
        }
        public int GetRunCount(string runcount)
        {
            int count = 0;
            count = Convert.ToInt32(runcount, 16);
            return count;
        }
        public UInt64 GetTime(string time)
        {
            UInt64 iMs = 0;
            string hexValue = string.Empty;

            UInt64 iMs1 = HexToInt(time.Substring(0, 2));
            UInt64 iMs2 = HexToInt(time.Substring(2, 2)) * (UInt64)Math.Pow(2, 8);
            UInt64 iMs3 = HexToInt(time.Substring(4, 2)) * (UInt64)Math.Pow(2, 16);
            UInt64 iMs4 = HexToInt(time.Substring(6, 2)) * (UInt64)Math.Pow(2, 24);
            UInt64 iMs5 = HexToInt(time.Substring(8, 2)) * (UInt64)Math.Pow(2, 32);
            UInt64 iMs6 = HexToInt(time.Substring(10, 2)) * (UInt64)Math.Pow(2, 40);
            UInt64 iMs7 = HexToInt(time.Substring(12, 2)) * (UInt64)Math.Pow(2, 48);
            UInt64 iMs8 = HexToInt(time.Substring(14, 2)) * (UInt64)Math.Pow(2, 56);

            iMs = iMs1 + iMs2 + iMs3 + iMs4 + iMs5 + iMs6 + iMs7 + iMs8;
            return iMs;
        }
        public List<float> GetTemperature(string temperature)
        {
            List<float> oTemperature = new List<float>();

            string temp = string.Empty;
            for (int i = 0; i < temperature.Length / 3; i++)
            {
                temp = temperature.Substring(i * 3, 3);

                float f = (float)(Convert.ToInt32(temp) / 10.0);
                oTemperature.Add(f);
            }

            return oTemperature;
        }
        public string GetStepping(string stepping)
        {
            return string.Empty;
        }
        private byte[] toBinary(string str)
        {
            System.Text.ASCIIEncoding encoding = new ASCIIEncoding();
            return encoding.GetBytes(str);
        }
        private char[] toAscii(string str, int length)
        {
            char[] asciiData = new char[length];
            for (int i = 0; i < length; i++)
            {
                int value = Convert.ToInt32(str.Substring(i * 2, 2), 16);
                asciiData[i] = (char)value;
            }

            return asciiData;
        }
        private UInt64 HexToInt(string hexValue)
        {
            return Convert.ToUInt64(hexValue, 16);
        }

        /// <summary>
        /// Converts a byte array to UInt16 values.
        /// </summary>
        /// <param name="bByte">The byte Array</param>
        /// <param name="iIndex">The index to start in the byte array</param>
        /// <param name="iElements">The number of values to get</param>
        /// <returns>A list of values from the byte array</returns>
        private static List<UInt16> toUInt16(byte[] bByte, int iIndex, int iElements)
        {
            List<UInt16> iValues = new List<UInt16>();
            for (int i = 0; i < iElements; i++)
            {
                UInt16 iValue = System.BitConverter.ToUInt16(bByte, iIndex);
                iIndex += sizeof(UInt16);
                iValues.Add(iValue);
            }
            return iValues;
        }

        /// <summary>
        /// Converts a byte array to Int16 values.
        /// </summary>
        /// <param name="bByte">The byte Array</param>
        /// <param name="iIndex">The index to start in the byte array</param>
        /// <param name="iElements">The number of values to get</param>
        /// <returns>A list of values from the byte array</returns>
        private static List<Int16> toInt16(byte[] bByte, int iIndex, int iElements)
        {
            List<Int16> iValues = new List<Int16>();
            for (int i = 0; i < iElements; i++)
            {
                Int16 iValue = System.BitConverter.ToInt16(bByte, iIndex);
                iIndex += sizeof(Int16);
                iValues.Add(iValue);
            }
            return iValues;
        }

        /// <summary>
        /// Converts a byte array to Int32 values.
        /// </summary>
        /// <param name="bByte">The byte Array</param>
        /// <param name="iIndex">The index to start in the byte array</param>
        /// <param name="iElements">The number of values to get</param>
        /// <returns>A list of values from the byte array</returns>
        private static List<Int32> toInt32(byte[] bByte, int iIndex, int iElements)
        {
            List<Int32> iValues = new List<Int32>();
            for (int i = 0; i < iElements; i++)
            {
                Int32 iValue = System.BitConverter.ToInt32(bByte, iIndex);
                iIndex += sizeof(Int32);
                iValues.Add(iValue);
            }
            return iValues;
        }

        /// <summary>
        /// Converts a byte array to float values.
        /// </summary>
        /// <param name="bByte">The byte Array</param>
        /// <param name="iIndex">The index to start in the byte array</param>
        /// <param name="iElements">The number of values to get</param>
        /// <returns>A list of values from the byte array</returns>
        private static List<float> toFloat(byte[] bByte, int iIndex, int iElements)
        {
            List<float> iValues = new List<float>();
            for (int i = 0; i < iElements; i++)
            {
                float iValue = System.BitConverter.ToSingle(bByte, iIndex);
                iIndex += sizeof(float);
                iValues.Add(iValue);
            }
            return iValues;
        }

        /// <summary>
        /// Converts a byte array to UInt64 values.
        /// </summary>
        /// <param name="bByte">The byte Array</param>
        /// <param name="iIndex">The index to start in the byte array</param>
        /// <param name="iElements">The number of values to get</param>
        /// <returns>A list of values from the byte array</returns>
        private static List<UInt64> toUInt64(byte[] bByte, int iIndex, int iElements)
        {
            List<UInt64> iValues = new List<UInt64>();

            for (int i = 0; i < iElements; i++)
            {
                UInt64 iValue = System.BitConverter.ToUInt64(bByte, iIndex);
                iIndex += sizeof(UInt64);
                iValues.Add(iValue);
            }
            return iValues;
        }
        #endregion status, run count, run time 등 parsing

        #region Delegation ChargerDBS
        private void _DbsClient_ControllerResult(int stageno, string msg)
        {
            manualmode.WriteLog(stageno, msg);

            var data = from error in _Constant.BtErrorList
                       where msg.Contains(error)
                       select error;
        }
        #endregion

        #region Delegation ManualMode
        private async void _ManualMode_KeysightCommand(int stageno, string cmd, enumCommandType enCmdType, enumBTCommandType enBtCmdType)
        {
            //CmdKeysightCommand(stageno, cmd, enCmdType);
            await Task.Run(() => CmdKeysightCommand(stageno, cmd, enCmdType));
        }
        private async void _ManualMode_StartCharging(STAGEINFO stageinfo)
        {
            int stageno = stageinfo.stageno;
            string recipeno = stageinfo.recipeno;

            if (stageno < 0) return;

            nTrayInfo[stageno].STAGENO = stageno;
            nTrayInfo[stageno].TRAYID = "Tray" + System.DateTime.Now.ToString("yyMMddHHmmss");
            nTrayInfo[stageno].RECIPENO = recipeno;

            SetTrayInfo(stageno);

            //* Task 사용
            //await Task.Factory.StartNew(new Action<object>(StartChargingAsync), stageinfo);
            await Task.Run(() => StartChargingAsync(stageinfo));
        }
        private void _ManualMode_StopCharging(int stageno)
        {
            StopCharging(stageno);
        }
        private void _ManualMode_RESET(int stageno)
        {
            ResetAsync(stageno);
        }
        #endregion

        #region Delegation DCIR Mode
        private async void _DCIRMode_StartDCIRAsync(STAGEINFO stageinfo)
        {
            int stageno = stageinfo.stageno;
            string recipeno = stageinfo.recipeno;

            if (stageno < 0) return;

            nTrayInfo[stageno].STAGENO = stageno;
            nTrayInfo[stageno].TRAYID = "Tray" + System.DateTime.Now.ToString("yyMMddHHmmss");
            nTrayInfo[stageno].RECIPENO = recipeno;

            SetTrayInfo(stageno);

            //* Task 사용
            //await Task.Factory.StartNew(new Action<object>(StartChargingAsync), stageinfo);
            await Task.Run(() => StartDCIRAsync(stageinfo));
        }
        private void _DCIRMode_RESET(int stageno)
        {
            ResetDCIR(stageno);
        }

        private void _DCIRMode_StopDCIR(int stageno)
        {
            StopDCIR(stageno);
        }
        #endregion

        #region Delegation Calibration Mode
        private void _CalMode_StartCalibration(int stageno)
        {
            StartCalibration(stageno);
        }

        private void _CalMode_StopCalibration(int stageno)
        {
            StopCalibration(stageno);
        }
        #endregion

        #region Delegation MeasureInfo
        private async void _MeasureInfo_SaveFinData(int stageno)
        {
            var result = await Task.Run(() => SaveMonDataAllAsync(stageno, nTrayInfo[stageno]));
            SaveFinData(stageno, result, nTrayInfo[stageno]);
        }
        #endregion

        #region TrayInfo / Charger Initialize
        public void Initialization(int stageno)
        {
            nTrayInfo[stageno].CHARGING = false;
            nTrayInfo[stageno].ENDCHARGING = false;
            nTrayInfo[stageno].TRAYID = string.Empty;
        }
        //* manual mode
        public void SetTrayInfo(int stageno)
        {
            int nChannel = _Constant.ChannelCount;
            try
            {
                nTrayInfo[stageno].CELLCOUNT = nChannel;
                for (int nIndex = 0; nIndex < nChannel; nIndex++)
                    nTrayInfo[stageno].CELL[nIndex] = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        #endregion

        #region DCIR Command
        private async void StartDCIRAsync(object objStageInfo)
        {
            STAGEINFO stageinfo = (STAGEINFO)objStageInfo;

            string cmd = string.Empty;
            int stageno = stageinfo.stageno;
            string recipeno = stageinfo.recipeno;

            keysightcontrollers[stageno].COMMANDRUN = true;
            keysightcontrollers[stageno].LASTRESPONSE = string.Empty;

            try
            {
                //* Reset Controller
                var result = await Task.Run(() => keysightcontrollers[stageno].ResetController());
                //result = "No error";
                if (result.Contains("No error"))
                {
                    /// Set Step Definition
                    /// Read Dcir Recipe
                    /// Set Definition with Dcir Recipe
                    RecipeDcir recipe = util.ReadRecipeDcir(recipeno);
                    nTrayInfo[stageno].RECIPEDCIR = recipe;

                    result = string.Empty;
                    result = await Task.Run(() => keysightcontrollers[stageno].RunDcirDefinition(nTrayInfo[stageno]));

                    //* Start DCIR
                    if (result.Contains("No error"))
                    {
                        keysightcontrollers[stageno].StartDcir();
                    }
                    else
                    {
                        manualmode.WriteLog(stageno, result);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        private async Task<bool> ResetDCIR(int stageno)
        {
            var result = await Task.Run(() => keysightcontrollers[stageno].ResetController());
            //result = "No error";
            if (result.Contains("No error"))
                return true;
            else return false;
        }
        private void StopDCIR(int stageno)
        {

        }
        #endregion DCIR Command

        #region CDC Command
        private async void StartChargingAsync(object objStageInfo)
        {
            STAGEINFO stageinfo = (STAGEINFO)objStageInfo;

            string cmd = string.Empty;
            int stageno = stageinfo.stageno;
            string recipeno = stageinfo.recipeno;
            bool bFset = stageinfo.fset;
            nTrayInfo[stageno].COMMANDRUN = true;
            nTrayInfo[stageno].LASTRESPONSE = string.Empty;
            nTrayInfo[stageno].FSET = bFset;
            try
            {
                //* Reset Controller
                var result = await Task.Run(() => keysightcontrollers[stageno].ResetController());
                //result = "No error"; 
                if (result.Contains("No error"))
                {
                    //* Set Step Definition
                    List<Recipe> recipes = await Task.Run(() => mariadb.GETRECIPEDATAAsync(recipeno));
                    nTrayInfo[stageno].RECIPE = recipes;

                    result = string.Empty;
                    result = await Task.Run(() => keysightcontrollers[stageno].RunDefinition(nTrayInfo[stageno]));

                    //* Check Definition
                    //dbsClient.CheckDefinition(stageno, recipes);

                    //* Start Charging
                    if (result.Contains("No error"))
                    {
                        keysightcontrollers[stageno].StartCharging();
                    }
                    else
                    {
                        manualmode.WriteLog(stageno, result);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        private void StopCharging(int stageno)
        {
            keysightcontrollers[stageno].StopCharging();
        }
        private void ResetAsync(int stageno)
        {
            keysightcontrollers[stageno].ResetController();
        }
        #endregion CDC Command

        #region Calibration Command
        private void StartCalibration(int stageno)
        {
            keysightcontrollers[stageno].StartCalibration();
        }
        private void StopCalibration(int stageno)
        {
            keysightcontrollers[stageno].StopCalibration();
        }
        #endregion Calibration Command

        #region Keysight Command
        private void CmdKeysightCommand(int stageno, string cmd, enumCommandType enCommandType)
        {
            string command = string.Empty;

            //* TRB command
            if(cmd.Contains("@01") && cmd.Length > 3)
            {
                command = keysightcontrollers[stageno].MakeTRBCommand(cmd.Remove(0,3), string.Empty);
            }
            //* CONT command
            else if (cmd.Contains("@02") && cmd.Length > 3)
            {
                //command = _Constant.stx + cmd + _Constant.etx;
                command = keysightcontrollers[stageno].MakeCONTCommand(cmd.Remove(0, 3), string.Empty);
            }
            //* Servo command
            else if(cmd.Contains("TRS") && cmd.Length > 5)
            {
                //* Servo 명령어
            }

            keysightcontrollers[stageno].Send(command);
            util.SaveCDCLog(stageno, command);
        }
        #endregion

        #region Get Mon Data Timer
        private async void GetDataTimer_TickAsync(object sender, EventArgs e)
        {
            //* sendata 는 모든 controller의 데이터를 한번에 읽어와서 처리함.
            //* 현재는 sen 데이터와 mon/fin 데이터만 처리
            //* 추후 sensor 데이터도 처리해야 함.
            try
            {
                nSenData = await Task.Run(() => mariadb.GETSENDATAAsync());
                measureinfo.SetSenStatus(nSenData);
                for (int nIndex = 0; nIndex < nSenData.Length; nIndex++)
                {
                    if (nSenData[nIndex] != null)
                    {
                        if (nSenData[nIndex].SENSTATUS == enumSenStatus.RUN)
                        {
                            nTrayInfo[nIndex].CHARGING = true;
                        }
                        else if (nSenData[nIndex].SENSTATUS == enumSenStatus.IDL)
                        {
                            nTrayInfo[nIndex].CHARGING = false;
                        }

                        if (nSenData[nIndex].SENSTATUS == enumSenStatus.IDL
                            && nSenData[nIndex].OLDSENSTATUS == enumSenStatus.RUN)
                        {
                            //var result = await Task.Factory.StartNew(new Action<object>(SaveMonData), nIndex);
                            var result = await Task.Run(() => SaveMonDataAllAsync(nIndex, nTrayInfo[nIndex]));
                            SaveFinData(nIndex, result, nTrayInfo[nIndex]);
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        private void SaveFinData(int stageno, List<string> mons, TRAYINFO trayINFO)
        {
            if (mons.Count < 1) return;
            //* recipe 는 설정에서 찾아야 함. trayid 가져오는 것처럼.
            List<Recipe> recipes = mariadb.GETRECIPEDATAAsync(trayINFO.RECIPENO);
            
            //* 정상 충전시 OK / recipe 대로 정상 충전이 안되면 NG => 어떻게?
            List<string> finData;
            string recipeinfo = string.Empty;
            for (int nIndex = 0; nIndex < recipes.Count; nIndex++)
            {
                finData = new List<string>();
                for (int nChannel = 0; nChannel < _Constant.ChannelCount; nChannel++)
                {
                    List<string> nData = new List<string>();
                    //* DATETIME,CH1 STATUS,CH1 CURR,CH1 VOLT,CH1 CAPA,CH2 STATUS,CH2 CURR,CH2 VOLT,CH2 CAPA, 
                    for (int i = 0; i < mons.Count(); i++)
                    {
                        string[] elements = mons[i].Split(',');
                        if(elements.Length == HEADERCOUNT 
                            && (elements[nChannel * 4 + 1] == (nIndex + 1).ToString() 
                                || (util.TryParseInt(elements[nChannel * 4 + 1], 0) <= 0)))
                        {
                            //* CHANNEL,DATETIME,STATUS,CURR,COLT,CAPA
                            string result = (nChannel + 1) + ", " + elements[0] + ","
                                + elements[nChannel * 4 + 1] + "," + elements[nChannel * 4 + 2] + ","
                                + elements[nChannel * 4 + 3] + "," + elements[nChannel * 4 + 4]; 
                            nData.Add(result);
                        }
                    }

                    if (nData.Count() > 0)
                    {
                        var nLast = nData.Last();
                        finData.Add(nLast);
                    }
                }

                if(finData.Count() == _Constant.ChannelCount)
                {
                    recipeinfo = "RECIPE INFO." + Environment.NewLine;
                    recipeinfo += "RECIPE METHOD," + recipes[nIndex].recipemethod + Environment.NewLine;
                    recipeinfo += "TIME," + recipes[nIndex].time + Environment.NewLine;
                    recipeinfo += "CURRENT," + recipes[nIndex].current + Environment.NewLine;
                    recipeinfo += "VOLTAGE," + recipes[nIndex].voltage + Environment.NewLine;
                    util.SaveFinData(stageno, nTrayInfo[stageno].TRAYID, recipeinfo, finData);
                }
            }

        }
        //* 예전방식 데이터
        private void SaveFinData2(int stageno, List<string> mons)
        {
            //if (result == string.Empty) return;
            //string[] mons = result.Split(new string[] { Environment.NewLine },StringSplitOptions.None);
            if (mons.Count < 1) return;
            //* CHANNEL,DATETIME,TIME,STAUTS,CURRENT,VOLTAGE,CAPACITY
            List<Recipe> recipes = mariadb.GETRECIPEDATAAsync("2");

            #region 정상 충전시 OK / recipe 대로 정상 충전이 안되면 NG
            //for (int nIndex = 0; nIndex < recipes.Count; nIndex++)
            //{
            //    for (int nChannel = 0; nChannel < _Constant.ChannelCount; nChannel++)
            //    {

            //        var nData = from line in mons
            //                    let elements = line.Split(',')
            //                    where !line.Equals(string.Empty) && elements[0] == (nChannel + 1).ToString("D3")
            //                        && ((int.Parse(elements[3]) > 0 && elements[3] == (nIndex + 1).ToString()) ||
            //                        (int.Parse(elements[3]) <= 0))
            //                    select line;

            //        foreach (var str in nData)
            //            Console.WriteLine(str);

            //        if (nData.Count() > 0)
            //        {
            //            var nLast = nData.Last();
            //            util.SaveFinData(stageno, nLast, nTrayInfo[stageno]);
            //        }
            //    }
            //}
            #endregion

            List<string> finData;
            string recipeinfo = string.Empty;
            for (int nIndex = 0; nIndex < recipes.Count; nIndex++)
            {
                finData = new List<string>();
                for (int nChannel = 0; nChannel < _Constant.ChannelCount; nChannel++)
                {

                    var nData = from line in mons
                                let elements = line.Split(',')
                                where !line.Equals(string.Empty) && elements[0] == (nChannel + 1).ToString("D3")
                                    && ((int.Parse(elements[3]) > 0 && elements[3] == (nIndex + 1).ToString()) ||
                                    (int.Parse(elements[3]) <= 0))
                                select line;

                    if (nData.Count() > 0)
                    {
                        var nLast = nData.Last();
                        finData.Add(nLast);
                    }
                }

                if (finData.Count() == _Constant.ChannelCount)
                {
                    util.SaveFinData2(stageno, finData, nTrayInfo[stageno]);
                }
            }

        }
        /// <summary>
        /* 데이터 저장방식
        DATETIME, TIME, CH1 STATUS, CH1 CURR, CH1 VOLT, CH1 CAPA ...
        */
        /// </summary>
        private async Task<List<string>> SaveMonDataAllAsync(object stageno, TRAYINFO trayINFO)
        {
            int nStageNo = (int)stageno + 1;
            string recipeinfo = "RECIPE INFO." + Environment.NewLine;
            List<string> nResult = new List<string>();
            try
            {
                List<Recipe> recipes = mariadb.GETRECIPEDATAAsync(trayINFO.RECIPENO);
                for(int nIndex = 0; nIndex < recipes.Count(); nIndex++)
                {
                    recipeinfo += "STEP ID," + recipes[nIndex].orderno;
                    recipeinfo += ",RECIPE METHOD," + recipes[nIndex].recipemethod;
                    recipeinfo += ",TIME," + recipes[nIndex].time;
                    recipeinfo += ",CURRENT," + recipes[nIndex].current;
                    recipeinfo += ",VOLTAGE," + recipes[nIndex].voltage + Environment.NewLine;
                }

                string result;
                KeysightMonData[] mondata = await Task.Run(() => mariadb.GETMONDATAALL(nStageNo));
                if (mondata == null) return nResult;
                for(int nIndex = 0; nIndex < mondata.Length; nIndex++)
                {
                    result = util.SaveMonData((int)stageno, trayINFO.TRAYID, recipeinfo, mondata[nIndex]);
                    string[] re = result.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                    for (int i = 0; i < re.Length; i++)
                        nResult.Add(re[i]);
                }
                return nResult;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return nResult;
            }
        }
        /// <summary>
        /* 데이터 저장방식
        CHANNEL,DATETIME,TIME,STAUTS,CURRENT,VOLTAGE,CAPACITY
        001,20231212 14:51:46,16461743,1,-1.1,0.77
        002,20231212 14:51:46,16461743,1,-1.0,0.58
        003,20231212 14:51:46,16461743,1,-1.4,0.96
        004,20231212 14:51:46,16461743,1,-1.4,0.58
        */
        /// </summary>
        private async Task<List<string>> SaveMonDataAllAsync2(object stageno)
        {
            int nStageNo = (int)stageno + 1;
            List<string> nResult = new List<string>();
            try
            {
                string result;
                KeysightMonData[] mondata = await Task.Run(() => mariadb.GETMONDATAALL(nStageNo));
                if (mondata == null) return nResult;
                for (int nIndex = 0; nIndex < mondata.Length; nIndex++)
                {
                    result = util.SaveMonData2(nStageNo, mondata[nIndex], nTrayInfo[nStageNo - 1]);
                    string[] re = result.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                    for (int i = 0; i < re.Length; i++)
                        nResult.Add(re[i]);
                }
                return nResult;
            }
            catch (Exception ex)
           {
                Console.WriteLine(ex.ToString());
                return nResult;
            }
        }
        #endregion

        #region Get CAL Data Timer
        private async void GetCalDataTimer_Tick(object sender, EventArgs e)
        {
            //* sendata 는 모든 controller의 데이터를 한번에 읽어와서 처리함.
            //nCalData = mariadb.GETCALDATA();
            nCalData = await Task.Run(() => mariadb.GETCALDATA());
            calibrationmode.SetCalStatus(nCalData);
        }
        #endregion

        #region MariaDB 
        public void MariaDBConnect()
        {
            int iDB = 0;
            //* Task 사용
            //Task.Factory.StartNew(new Action<object>(MariaDBOpen), (object)iDB);
            Task.Run(() => MariaDBOpen(iDB));
        }
        private void MariaDBOpen(object iDB)
        {
            //* DB Connect
            mariadb.Open(mariaConfig.DBIPADDRESS, mariaConfig.DBPORT, mariaConfig.DBNAME,
                    mariaConfig.DBUSER, mariaConfig.DBPWD);
        }
        private async Task MariaDBDeleteMonAsync(int nStageNo)
        {
            await Task.Run(() => mariadb.DELETEMONDATA(nStageNo));
        }
        #endregion
    }
}
