using DHS.EQUIPMENT2.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DHS.EQUIPMENT2.Common;

namespace DHS.EQUIPMENT2.Equipment
{
    public class KeysightController : CSocketDriver
    {
        Util util = new Util();
        private int _nStage = 0;
        private string _strIPaddress;
        private int _iPort1;
        private int _iPort2;
        
        private int _iReconnectCount;
        private int _nSendFlag = 0;

        #region SEN DATA
        enumSenStatus _enSenStatus;
        enumSenStatus _enOldSenStatus;
        private string _strConnection;
        private bool _bBTConnected;
        private int _iSenRunCount;
        private string _strServo;
        private string _strStepping1;
        private string _strStepping2;
        #endregion SEN DATA

        #region MON DATA
        private int _iRunCount;
        private int _iMaxChannel;
        private string _strStageName;
        private UInt64 _iTime;
        private string _dtDateTime;
        private int[] _iStatus = new int[_Constant.ChannelCount];
        private List<float> _dVoltage = new List<float>();
        private List<float> _dCurrent = new List<float>();
        private List<float> _dCapacity = new List<float>();
        private List<float> _dOldCapacity = new List<float>();
        #endregion MON DATA

        public Timer _tmrSend = null;
        bool bSendTimer = false;
        public int MONLENGTH;
        public int SENLENGTH;//1638;

        private string _strLastCommand;
        private string _strLastResponse;
        private bool _bCommandRun;
        private bool _bFset;
        string strReadMsg = string.Empty;
        TRAYINFO[] nTrayInfo = new TRAYINFO[_Constant.ControllerCount];
        bool bRead = false;

        public int STAGE { get => _nStage; set => _nStage = value; }
        public int SENDFLAG { get => _nSendFlag; set => _nSendFlag = value; }
        public string IPADDRESS { get => _strIPaddress; set => _strIPaddress = value; }
        public int PORT { get => _iPort1; set => _iPort1 = value; }
        public int PORT2 { get => _iPort2; set => _iPort2 = value; }
        public int RECONNECTCOUNT { get => _iReconnectCount; set => _iReconnectCount = value; }

        #region SEN DATA
        public enumSenStatus SENSTATUS { get => _enSenStatus; set => _enSenStatus = value; }
        public enumSenStatus OLDSENSTATUS { get => _enOldSenStatus; set => _enOldSenStatus = value; }
        public string CONNECTION { get => _strConnection; set => _strConnection = value; }
        public bool BTCONNECTED { get => _bBTConnected; set => _bBTConnected = value; }
        public int SENRUNCOUNT { get => _iSenRunCount; set => _iSenRunCount = value; }
        public string SERVO { get => _strServo; set => _strServo = value; }
        public string STEPPING1 { get => _strStepping1; set => _strStepping1 = value; }
        public string STEPPING2 { get => _strStepping2; set => _strStepping2 = value; }
        #endregion SENDATA

        #region MON DATA
        public int RUNCOUNT { get => _iRunCount; set => _iRunCount = value; }
        public int MAXCHANNEL { get => _iMaxChannel; set => _iMaxChannel = value; }
        public string STAGENAME { get => _strStageName; set => _strStageName = value; }
        public UInt64 STAGETIME { get => _iTime; set => _iTime = value; }
        public string DATETIME { get => _dtDateTime; set => _dtDateTime = value; }
        public int[] CHANNELSTATUS { get => _iStatus; set => _iStatus = value; }
        public List<float> CHANNELVOLTAGE { get => _dVoltage; set => _dVoltage = value; }
        public List<float> CHANNELCURRENT { get => _dCurrent; set => _dCurrent = value; }
        public List<float> CHANNELCAPACITY { get => _dCapacity; set => _dCapacity = value; }
        public List<float> OLDCHANNELCAPACITY { get => _dOldCapacity; set => _dOldCapacity = value; }
        #endregion MON DATA

        public bool COMMANDRUN { get => _bCommandRun; set => _bCommandRun = value; }
        public string LASTCOMMAND { get => _strLastCommand; set => _strLastCommand = value; }
        public string LASTRESPONSE { get => _strLastResponse; set => _strLastResponse = value; }
        public bool FSET { get => _bFset; set => _bFset = value; }
        

        #region Delegation
        public delegate void ReceiveKeysightResult(int stageno, string msg);
        public event ReceiveKeysightResult OnKeysightResult = null;
        protected void RaiseOnKeysightResult(int stageno, string msg)
        {
            if (OnKeysightResult != null)
            {
                OnKeysightResult(stageno, msg);
            }
        }
        #endregion

        #region Class Method
        private static KeysightController[] KeysightControllers = new KeysightController[_Constant.ControllerCount];
        public static KeysightController GetInstance(int nIndex)
        {
            if (KeysightControllers[nIndex] == null) KeysightControllers[nIndex] = new KeysightController(nIndex);
            return KeysightControllers[nIndex];
        }
        public KeysightController(int stageno)
        {
            #region MONLENGTH/ SENLENGTH Setting
            if (_Constant.ChannelCount == 64)
            {
                MONLENGTH = 1828;
                SENLENGTH = 486;
            }
            else if (_Constant.ChannelCount == 256)
            {
                MONLENGTH = 7204;
                SENLENGTH = 58;//1638;
            }
            #endregion

            _iReconnectCount = 0;

            _tmrSend = new Timer();
            _tmrSend.Interval = 300;
            _tmrSend.Tag = stageno;
            _tmrSend.Tick += new EventHandler(SendTimer_Tick);
            _tmrSend.Enabled = true;
        }

        public void InitData()
        {
            _iTime = 0;
            _iRunCount = 0;
            for (int nIndex = 0; nIndex < _Constant.ChannelCount; nIndex++)
            {
                _iStatus[nIndex] = 0;
            }

            _dCurrent = new List<float>();
            _dVoltage = new List<float>();
            _dCapacity = new List<float>();
            _dOldCapacity = new List<float>();

            bRead = false;
            strReadMsg = string.Empty;
        }

        public KeysightController(string _strIP, int _iPort, int stageno)
            : base()
        {
            Open(_strIP, _iPort, stageno);
        }

        ~KeysightController()
        {
            CloseSocket();
        }
        #endregion

        #region Connection
        public void Close()
        {
            CloseSocket();
        }
        public void Open(string _strIP, int _iPort, int stageno)
        {
            try
            {
                STAGE = stageno;
                InitConnectionString(_strIP, _iPort, stageno, "ACTIVE"); //* ACTIVE -> client socket
                int iRet = 0;
                iRet = OpenSocketPort();

                if (iRet == 0)
                {
                    bSendTimer = true;
                    util.SaveLog(STAGE, "Controller is connected");
                    //RaiseOnConnectionState(true);
                }
                else
                {
                    bSendTimer = false;
                    //* 에러처리
                    //RaiseOnConnectionState(false);
                    util.SaveLog(STAGE, "Controller is not connected");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Keysightcontroller Open : " + ex.ToString());
                // _Logger.Log(Level.Exception, "Label Print Driver Open Fail!!! : " + ex.ToString());
            }
        }

        public void ChangeSetting(string _strIP, int _iPort, int stageno)
        {
            _iPort1 = _iPort;
            util.SaveLog(STAGE, "Controller ChangeSetting ...");
            try
            {
                InitConnectionString(_strIP, _iPort, stageno, "ACTIVE");
                int iRet = 0;
                iRet = OpenSocketPort();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Keysightcontroller ChangeSetting : " + ex.ToString());
                // _Logger.Log(Level.Exception, "Label Print Driver Open Fail!!! : " + ex.ToString());
            }
        }
        #endregion

        #region Send / Receive & Send Timer
        int _iBuffLength = 1;
        string _strBuffData = string.Empty;
        string _strRemainData = string.Empty;

        private string BytesToBinary(byte[] bytes)
        {
            string binaryString = string.Empty;
            foreach (byte b in bytes)
            {
                string str = Convert.ToString(b, 2);
                binaryString += str.PadLeft(8, '0');
            }

            return binaryString;
        }
        private string BytesToHex(byte[] bytes)
        {
            string hex = BitConverter.ToString(bytes);
            return hex.Replace("-", "");
        }
        private string GetAscii(string strMessage)
        {
            char[] senAscii = toAscii(strMessage, strMessage.Length / 2);
            string str = new string(senAscii);
            return str;
        }
        protected override int ParseMessage(byte[] bytes)
        {
            try
            {
                string _strError = string.Empty;
                string hex = BytesToHex(bytes);

                _strBuffData = _strBuffData + hex;
                if (_strBuffData.Length > 2048)
                {
                    if (_strBuffData.Substring(0, 2) != "40" && !_strBuffData.Contains("3B"))
                    {
                        _strBuffData = string.Empty;
                    }
                    else if (_strBuffData.Contains("3B"))
                    {
                        string[] _strSplit = _strBuffData.Split(new char[] { (char)0x3B });
                        _strBuffData = _strSplit[_strSplit.Length - 1];
                    }
                    _strBuffData = string.Empty;
                }

                util.SaveLog(_iStage, _strBuffData, "RX_Raw");

                if (_strBuffData.Substring(0, 2) == "40")
                {
                    string[] strData = _strBuffData.Split(new char[] { (char)0x3B });
                    util.SaveLog(_iStage, strData[0].Trim(), "RX");
                    _strBuffData = strData[1].Trim();

                    //* @MON, @SEN, @FIN
                    RaiseOnKeysightResult(this._nStage, strData[0].Trim());
                }
                return 0;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
        
        protected override int ParseMessage(string strMessage)
        {
            try
            {
                int iStxIndex = 0;
                string _strError = string.Empty;

                _iBuffLength = 1;

                _strBuffData += _strRemainData + strMessage;
                _strRemainData = string.Empty;
                iStxIndex = strMessage.IndexOf(_Constant.STX);

                if (_strBuffData.Length >= 10000) _strBuffData = string.Empty;

                if (strMessage.Contains(_Constant.STX) && strMessage.Contains(_Constant.SHARP)
                    && GetAscii(strMessage).Contains("MON") && strMessage.Length >= 18)
                {
                    _strBuffData = strMessage;//.Substring(iStxIndex, 18);
                }
                else if (strMessage.Contains(_Constant.STX) && strMessage.Contains(_Constant.SHARP)
                    && GetAscii(strMessage).Contains("SEN") && strMessage.Length >= 28)
                {
                    _strBuffData = strMessage;//.Substring(iStxIndex, 28);
                }
                else if (strMessage.Contains(_Constant.STX) && strMessage.Contains(_Constant.SHARP)
                    && GetAscii(strMessage).Contains("CAL") && strMessage.Length >= 10)
                {
                    _strBuffData = strMessage;//.Substring(iStxIndex, 10);
                }

                //* 1. 메세지가 쌓여서 마지막에 길이가 일치하는 경우
                //* 2. 마지막 메세지가 다음 시작을 포함하는 경우 

                //_strBuffData += _strRemainData + strMessage;
                #region SEN, MON, CAL 
                string strBody = string.Empty;
                //* 첫 글자가 @ 이고 SEN, MON, CAL 로시작하면 RaiseOnKeysightResult 실행
                //* 그외에는 그대로 넘겨줌.

                COMMANDRUN = false;
                LASTRESPONSE = _strBuffData;

                if (GetAscii(_strBuffData.Substring(0, 8)) == "@MON" || GetAscii(_strBuffData.Substring(0, 8)) == "@SEN"
                    || GetAscii(_strBuffData.Substring(0, 8)) == "@CAL")
                {
                    if (GetAscii(_strBuffData.Substring(0, 8)) == "@MON" && _strBuffData.Length >= MONLENGTH)
                    {
                        strBody = _strBuffData.Substring(0, MONLENGTH);
                        _strRemainData = _strBuffData.Substring(MONLENGTH);
                        RaiseOnKeysightResult(this._nStage, strBody);

                        //* 로그
                        util.SaveLog(_iStage, strBody, "RX");
                        string tmpHeader = strBody.Substring(0, 18);
                        string tmpBody = strBody.Substring(18);
                        char[] senAscii = toAscii(tmpHeader, tmpHeader.Length / 2);
                        string strHeader = new string(senAscii);
                        util.SaveLog(_iStage, strHeader + tmpBody, "RX");
                        _strBuffData = string.Empty;
                    }
                    else if (GetAscii(_strBuffData.Substring(0, 8)) == "@SEN" && _strBuffData.Length >= SENLENGTH)
                    {
                        strBody = _strBuffData.Substring(0, SENLENGTH);
                        _strRemainData = _strBuffData.Substring(SENLENGTH);
                        RaiseOnKeysightResult(this._nStage, strBody);

                        //* 로그
                        util.SaveLog(_iStage, strBody, "RX");
                        char[] senAscii = toAscii(strBody, strBody.Length / 2);
                        string str = new string(senAscii);
                        util.SaveLog(_iStage, str, "RX");
                        _strBuffData = string.Empty;
                    }
                    else if (GetAscii(_strBuffData.Substring(0, 8)) == "@CAL" && _strBuffData.Split(',').Length == 7)
                    {
                        RaiseOnKeysightResult(this._nStage, _strBuffData);

                        //* 로그
                        util.SaveLog(_iStage, strBody, "RX");
                        char[] senAscii = toAscii(strBody, strBody.Length / 2);
                        string str = new string(senAscii);
                        util.SaveLog(_iStage, str, "RX");
                        _strBuffData = string.Empty;
                    }
                }
                else
                {
                    char[] senAscii = toAscii(_strBuffData, _strBuffData.Length / 2);
                    string str = new string(senAscii);
                    util.SaveLog(_iStage, str, "RX");
                    RaiseOnKeysightResult(this._nStage, str);
                    _strBuffData = string.Empty;
                }
                #endregion

                return 0;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
        protected int ParseMessage3(string strMessage)
        {
            try
            {
                int iEtxIndex = 0;
                string _strError = string.Empty;
                _iBuffLength = 1;
                if (_strRemainData.Length > 3000)
                {
                    _strBuffData = string.Empty;
                    _strRemainData = string.Empty;
                }
                _strBuffData += _strRemainData + strMessage;

                while (_iBuffLength > 0)
                {
                    #region 첫 글자가 @ 인지 확인
                    if (_strBuffData.Length > 2 && _strBuffData.Substring(0, 2) == _Constant.STX)
                    {
                        #region 마지막 글자가 ; 인지 확인
                        if (_strBuffData.Contains(_Constant.ETX))
                        {
                            string _strStart, _strEnd;
                            iEtxIndex = _strBuffData.IndexOf(_Constant.ETX);
                            if (_strBuffData.Length >= iEtxIndex + 2)
                            {
                                _strEnd = _strBuffData.Substring(0, iEtxIndex + 2);
                                _strStart = _strBuffData.Substring(iEtxIndex + 2);
                                _strBuffData = _strEnd;
                                _strRemainData = _strStart;
                            }

                            util.SaveLog(_iStage, _strBuffData, "RX");

                            if (CheckSenData(_strBuffData) == true || CheckMonData(_strBuffData) == true)
                            //    || CheckCalData(_strBuffData) == true)
                            {
                                //* @MON, @SEN, @CAL
                                RaiseOnKeysightResult(this._nStage, _strBuffData);
                                _strBuffData = string.Empty;
                            }
                            else
                            {
                                char[] senAscii = toAscii(_strBuffData, _strBuffData.Length / 2);
                                string str = new string(senAscii);
                                util.SaveLog(_iStage, str, "RX");
                                if (str.Contains("SEN") || str.Contains("MON"))
                                    _strBuffData = string.Empty;
                                else
                                {
                                    RaiseOnKeysightResult(this._nStage, str);
                                    _strBuffData = string.Empty;
                                }
                            }
                        }
                        else
                        {
                            //_strBuffData += strMessage;
                            _iBuffLength = 0;
                        }
                        #endregion
                    }
                    else if (_strBuffData.Contains(_Constant.STX))
                    {
                        iEtxIndex = _strBuffData.IndexOf(_Constant.STX);
                        _strBuffData = _strBuffData.Substring(iEtxIndex);
                        _iBuffLength = 0;
                    }
                    else
                    {
                        _strBuffData = string.Empty;
                        _iBuffLength = 0;
                    }
                    #endregion
                }

                return 0;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
        //* 1차 OK
        private int ParseMessage2(string strMessage)
        {
            try
            {
                string _strError = string.Empty;
                _strBuffData += strMessage;
                _iBuffLength = 1;

                if (_strBuffData.Length > 3704)
                    _strBuffData = string.Empty;

                while (_iBuffLength > 0)
                {
                    #region 첫 글자가 @ 인지 확인
                    if (_strBuffData.Substring(0, 2) == _Constant.STX)
                    {
                        #region 마지막 글자가 ; 인지 확인
                        if (_strBuffData.Substring(_strBuffData.Length - 2, 2) == _Constant.ETX)
                        {
                            util.SaveLog(_iStage, _strBuffData, "RX");

                            if (CheckSenData(_strBuffData) == true || CheckMonData(_strBuffData) == true)
                            {
                                //* @MON, @SEN, @FIN
                                RaiseOnKeysightResult(this._nStage, _strBuffData);
                                _strBuffData = string.Empty;
                            }
                            else
                            {
                                char[] senAscii = toAscii(_strBuffData, _strBuffData.Length / 2);
                                string str = new string(senAscii);
                                util.SaveLog(_iStage, str, "RX");
                                RaiseOnKeysightResult(this._nStage, str);
                                _strBuffData = string.Empty;
                            }
                        }
                        else
                        {
                            _iBuffLength = 0;
                        }
                        #endregion
                    }
                    else
                    {
                        //* 20231031
                        if (_strBuffData.Contains(_Constant.STX) && _strBuffData.Contains(_Constant.ETX))
                        {
                            int start = _strBuffData.IndexOf(_Constant.STX);
                            int end = _strBuffData.IndexOf(_Constant.ETX);
                            if (start < end)
                                _strBuffData = _strBuffData.Substring(start, end - start);
                            else if (start > end)
                                _strBuffData = _strBuffData.Substring(end + 2, _strBuffData.Length - end - 2);
                        }
                        _iBuffLength = 0;
                    }
                    #endregion
                }

                return 0;
            }
            catch (Exception ex)
            {
                return -1;
            }
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
        private bool CheckSenData(string message)
        {
            if (message.Substring(0, 8) == _Constant.SENHEADER
                && message.Substring(message.Length - 2, 2) == _Constant.ETX
                && message.Length == SENLENGTH)
            {
                char[] senAscii = toAscii(message, message.Length / 2);
                string str = new string(senAscii);
                util.SaveLog(_iStage, str, "RX");
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool CheckMonData(string message)
        {
            if (message.Substring(0, 8) == _Constant.MONHEADER
                && message.Substring(message.Length - 2, 2) == _Constant.ETX
                && message.Length == MONLENGTH)
            {
                string tmpHeader = message.Substring(0, 18);
                string strBody = message.Substring(18);
                char[] senAscii = toAscii(tmpHeader, tmpHeader.Length / 2);
                string strHeader = new string(senAscii);
                util.SaveLog(_iStage, strHeader + strBody, "RX");
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool CheckCalData(string message)
        {
            if (message.Substring(0, 8) == _Constant.CALHEADER
                    && message.Substring(message.Length - 2, 2) == _Constant.ETX)
            {
                char[] senAscii = toAscii(message, message.Length / 2);
                string str = new string(senAscii);
                util.SaveLog(_iStage, str, "RX");
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool CheckSenData2(string message)
        {
            if (message.Length == SENLENGTH && message.Substring(0, 2) == _Constant.STX
                    && message.Substring(SENLENGTH - 2, 2) == _Constant.ETX)
                return true;
            else
                return false;
        }
        private bool CheckMonData2(string message)
        {
            if (message.Length == MONLENGTH && message.Substring(0, 2) == _Constant.STX
                    && message.Substring(MONLENGTH - 2, 2) == _Constant.ETX)
                return true;
            else
                return false;
        }
        public override int Send(string strMessage)
        {
            util.SaveLog(STAGE, strMessage, "TX");
            return base.Send(strMessage);
        }
        public void StopTimer()
        {
            StopReconnectTimer();
        }
        private void SendTimer_Tick(object sender, EventArgs e)
        {
            if (bSendTimer == true)
            {
                switch (SENDFLAG)
                {
                    case 0:
                        break;
                    default:
                        break;
                }
            }
        }
        #endregion

        #region Keysight Controller Command
        string CMD = string.Empty;

        public string ReadBTResult()
        {
            strReadMsg = string.Empty;

            do
            {
                strReadMsg = LASTRESPONSE;
            }
            while (strReadMsg == string.Empty && bRead == false);

            return strReadMsg;
        }
        public void runCommand(string cmd)
        {
            bRead = false;

            CMD = cmd;
            Send(CMD);
        }
        public void runTRBCommand(string cmd)
        {
            CMD = cmd;
            CMD = MakeTRBCommand(cmd, string.Empty);

            LASTCOMMAND = CMD;
            COMMANDRUN = true;

            runCommand(CMD);
        }
        public void runCONTCommand(string cmd)
        {
            CMD = MakeCONTCommand(cmd, string.Empty);
            runCommand(CMD);
        }
        /// <summary>
        /// CONT 명령어 형식은 @명령어;
        /// @IDN; @ASB; @STP; @MON; @SEN; @RST; @ERR; @STC; @CAL;
        /// </summary>
        public string MakeCONTCommand(string cmd, string param)
        {
            string command = string.Empty;
            string header = _Constant.stx;
            string tail = _Constant.etx;

            command = header + cmd + param + tail;
            return command;
        }
        /// <summary>
        /// TRB 명령어 형식은 @TRB명령어;
        /// 명령어는 Keysight 명령어 형식 그대로 사용
        /// $0A(\n) 으로 명령어 여러개를 구분. 여러 명령어를 한번에 실행할 수 있음
        /// </summary>
        public string MakeTRBCommand(string cmd, string param)
        {
            string command = string.Empty;
            string a = string.Format("{0}", (char)39);
            string header = "@" + "TRB";
            string tail = "\n;";

            cmd = cmd.Replace("$0A", "\n");
            command = header + cmd + param + tail;
            return command;
        }
        public void RunKeysightCommand(string cmd, int stageno)
        {
            switch (cmd)
            {
                case "MON":
                    break;
                default:
                    break;
            }
        }
        public string ResetController()
        {
            string CMD = "RST";
            CMD = MakeCONTCommand(CMD, string.Empty);
            runCommand(CMD);

            string response = ReadBTResult();
            return response;
        }
        private string SystErr()
        {
            string systerr = "SYST:ERR?";
            return systerr;
        }
        #endregion

        #region Calibration Command
        public void StartCalibration()
        {
            bRead = true;

            string CMD = "CAL";
            CMD = MakeCONTCommand(CMD, string.Empty);
            runCommand(CMD);
        }
        public void StopCalibration()
        {
            bRead = true;
            
            string CMD = "STC";
            CMD = MakeCONTCommand(CMD, string.Empty);
            runCommand(CMD);
        }
        #endregion Calibration Command

        #region DCIR Definition and Command
        public void StartDcir()
        {
            bRead = true;

            string CMD = "MEAS:CELL:DCIR? (@1001:@1032)";
            CMD = MakeTRBCommand(CMD, string.Empty);
            runCommand(CMD);
        }
        public string RunDcirDefinition(TRAYINFO trayinfo)
        {
            string cmd = string.Empty;
            bool bfset = trayinfo.FSET;
            string recipeno = trayinfo.RECIPENO;
            RecipeDcir recipe = trayinfo.RECIPEDCIR;
            cmd = SetDcirDefineCommand(recipeno, recipe);
            runTRBCommand(cmd);

            string response = ReadBTResult();
            return response;
        }
        private string SetDcirDefineCommand(string recipe_no, RecipeDcir recipe)
        {
            string define = string.Empty;
            string sep = _Constant.SEP;

            #region controller에서 RST할 때 아래 quick, problimit 값은 자동으로 설정함.
            //define += SetQuick(64) + _Constant.SEP;
            //define += SetProbLimit("2") + _Constant.SEP;
            #endregion

            define += SetDcirDefine(recipe_no, recipe);
            //DCIR SEQ:STEP:DCIR:DEF 1,1,3.9,3.8,3,100,3,5,-100,3,3
            define += SystErr();

            return define;
        }
        private string SetDcirDefine(string recipe_no, RecipeDcir recipe)
        {
            string define = string.Empty;
            int recipeno = util.TryParseInt(recipe_no, 0);

            string seqid = "1";
            string stepid = "1";

            string maxVoltLimit = recipe.MaxVoltLimit;
            string minVoltLimit = recipe.MinVoltLimit;
            string pauseBeforeFirstPulse = recipe.PauseBeforeFirstPulse;
            string firstPulseLevel = recipe.FirstPulseLevel;
            string firstPulseWidth = recipe.FirstPulseWidth;
            string pauseAfterFirstPulse = recipe.PauseAfterFirstPulse;
            string secondPulseLevel = recipe.SecondPulseLevel;
            string secondPulseWidth = recipe.SecondPulseWidth;
            string pauseAfterSecondPulse = recipe.PauseAfterSecondPulse;

            define += SetDcirDefine(seqid, stepid, maxVoltLimit, minVoltLimit, pauseBeforeFirstPulse,
                firstPulseLevel, firstPulseWidth, pauseAfterFirstPulse,
                secondPulseLevel, secondPulseWidth, pauseAfterSecondPulse) + _Constant.SEP;
            return define;
        }
        private string SetDcirDefine(string seqid, string stepid,
            string maxVoltLimit, string minVoltLimit, string pauseBeforeFirstPulse,
            string firstPulseLevel, string firstPulseWidth, string pauseAfterFirstPulse,
            string secondPulseLevel, string secondPulseWidth, string pauseAfterSecondPulse)
        {
            string cmd = "SEQ:STEP:DCIR:DEF";

            cmd += " " + seqid + ", " + stepid + ", "
                + maxVoltLimit + ", " + minVoltLimit + ", " + pauseBeforeFirstPulse + ", "
                + firstPulseLevel + ", " + firstPulseWidth + ", " + pauseAfterFirstPulse
                + ", " + secondPulseLevel + ", " + secondPulseWidth + ", " + pauseAfterSecondPulse;

            return cmd;
        }
        #endregion DCIR Definition

        #region Charging/Discharging Definition and Command
        public void StartCharging()
        {
            string CMD = "ASB";
            CMD = MakeCONTCommand(CMD, string.Empty);
            runCommand(CMD);
        }
        public void StopCharging()
        {
            bRead = true;
            string CMD = "STP";
            CMD = MakeCONTCommand(CMD, string.Empty);
            runCommand(CMD);
        }
        public void CheckDefinition(List<Recipe> recipes)
        {
            string[] cmds = CheckDefineCommand(recipes);
            string cmd = string.Empty;
            int cmdIndex = 0;

            while (cmdIndex < cmds.Length)
            {
                if (COMMANDRUN == false)
                {
                    cmd = cmds[cmdIndex];
                    runTRBCommand(cmd);
                    cmdIndex++;
                }
            }
        }
        public string RunDefinition(TRAYINFO trayinfo)
        {
            string cmd = string.Empty;
            bool bfset = trayinfo.FSET;
            string recipeno = trayinfo.RECIPENO;
            List<Recipe> recipes = trayinfo.RECIPE;
            cmd = SetDefineCommand(recipeno, recipes, bfset);
            runTRBCommand(cmd);

            string response = ReadBTResult();
            return response;
        }
        //* @01*RST$0A*CLS$0ASYST:PROB:LIM 2,0$0ACELL:DEF:QUIC 4$0A
        //* SEQ:STEP:DEF 1, 1, CHARGE, 60, 1, 4.0$0A
        //* SEQ:STEP:DEF 1, 2, REST, 30$0A
        //* SEQ:STEP:DEF 1, 3, DISCHARGE, 60, 4, 3.0$0A
        //SYST:ERR?
        private string SetDefineCommand(string recipe_no, List<Recipe> recipes, bool fset)
        {
            string define = string.Empty;
            string sep = _Constant.SEP;

            #region controller에서 RST할 때 아래 quick, problimit 값은 자동으로 설정함.
            //define += SetQuick(64) + _Constant.SEP;
            //define += SetProbLimit("2") + _Constant.SEP;
            #endregion

            #region TEST용. 실제 사용시에는 controller에서 RST실행시 자동으로 ON/IMM 옵션셋팅
            if (fset == true)
            {
                define += "CELL:FSET ON" + _Constant.SEP;
                define += "CELL:TRAN IMM" + _Constant.SEP;
            }
            else
            {
                define += "CELL:FSET OFF" + _Constant.SEP;
                define += "CELL:TRAN REST" + _Constant.SEP;
            }
            #endregion

            define += SetDefine(recipe_no, recipes);
            //* Current/Voltage 시간은 20초 이상
            //define += "SEQ:TEST:DEF 1,2,1,CURR_LE,0.2,BEFORE,20,FAIL" + _Constant.SEP;
            //define += "SEQ:TEST:DEF 1,1,1,VOLT_LE,0.5,AFTER,20,FAIL" + _Constant.SEP;
            define += SystErr();

            return define;
        }
        private string[] CheckDefineCommand(List<Recipe> recipes)
        {
            string sep = _Constant.SEP;
            string[] querys = new string[recipes.Count];
            for (int nIndex = 0; nIndex < recipes.Count; nIndex++)
            {
                querys[nIndex] = "SEQ:STEP:DEF? 1," + (nIndex + 1).ToString();
            }

            return querys;
        }
        private string SetDefine(string recipe_no, List<Recipe> recipes)
        {
            string define = string.Empty;
            int recipeno = util.TryParseInt(recipe_no, 0);

            if (recipeno > 0)
            {
                if (recipes.Count > 0)
                {
                    for (int nIndex = 0; nIndex < recipes.Count; nIndex++)
                    {
                        string seqid = recipeno.ToString();
                        string stepid = recipes[nIndex].orderno;
                        string stepmode = recipes[nIndex].recipemethod;
                        string time = recipes[nIndex].time;
                        string current = recipes[nIndex].current;
                        string voltage = recipes[nIndex].voltage;

                        define += SetDefine(seqid, stepid, stepmode, time, current, voltage) + _Constant.SEP;
                    }
                }
            }
            return define;
        }
        private string SetDefine(string seqid, string stepid, string stepmode, string time, string current, string voltage)
        {
            double dCurr = util.TryParseDouble(current, 0.0) / 1000.0;
            double dVolt = util.TryParseDouble(voltage, 0.0) / 1000.0;
            string cmd = "SEQ:STEP:DEF";

            seqid = "1"; //* seqid 는 항상 1로 ...
            if (stepmode == "REST")
                cmd += " " + seqid + ", " + stepid + ", " + stepmode + ", " + time;
            else
                cmd += " " + seqid + ", " + stepid + ", " + stepmode + ", " + time + ", " + dCurr.ToString("F2") + ", " + dVolt.ToString("F2");

            return cmd;
        }
        #endregion Charging/Discharging Definition
    }
}
