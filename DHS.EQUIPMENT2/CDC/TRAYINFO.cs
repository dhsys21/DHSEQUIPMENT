using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DHS.EQUIPMENT2.Common;

namespace DHS.EQUIPMENT2
{
    public class STAGEINFO
    {
        public int stageno { get; set; }
        public string recipeno { get; set; }
        public bool fset { get; set; }
    }

    public class Recipe
    {
        public string recipeno { get; set; }
        public string orderno { get; set; }
        public string recipemethod { get; set; }
        public string time { get; set; }
        public string current { get; set; }
        public string voltage { get; set; }
    }

    public class RecipeDcir
    {
        public int RecipeNo { get; set; }
        public string SeqId { get; set; }
        public string StepId { get; set; }
        public string MaxVoltLimit { get; set; }
        public string MinVoltLimit { get; set; }
        public string PauseBeforeFirstPulse { get; set; }
        public string FirstPulseLevel { get; set; }
        public string FirstPulseWidth { get; set; }
        public string PauseAfterFirstPulse { get; set; }
        public string SecondPulseLevel { get; set; }
        public string SecondPulseWidth { get; set; }
        public string PauseAfterSecondPulse { get; set; }
    }

    public class TRAYINFO
    {
        private int _iStageNo;
        private int _iRemeasureCnt;
        private int _iCellCount;
        private string _sTrayId;
        private bool[] _isCell = new bool[_Constant.ChannelCount];
        private Color[] _clrChannelColor = new Color[_Constant.ChannelCount];
        private string[] _sCellSerial = new string[_Constant.ChannelCount];
        private bool[] _bMeasureResult = new bool[_Constant.ChannelCount];
        private bool _bTrayin;
        private bool _bCharging;
        private bool _bEndCharging;
        private string _iRecipeNo;
        private List<Recipe> _listRecipe;
        private RecipeDcir _recipeDcir;
        private string _strLastCommand;
        private string _strLastResponse;
        private bool _bCommandRun;
        private bool _bFset;

        private DateTime _dtArriveTime;
        private DateTime _dtFinishTime;

        public int STAGENO { get => _iStageNo; set => _iStageNo = value; }
        public int REMEASURECNT { get => _iRemeasureCnt; set => _iRemeasureCnt = value; }
        public bool[] CELL { get => _isCell; set => _isCell = value; }
        public Color[] CHANNELCOLOR { get => _clrChannelColor; set => _clrChannelColor = value; }
        public string[] CELLSERIAL { get => _sCellSerial; set => _sCellSerial = value; }
        public bool[] MEASURERESULT { get => _bMeasureResult; set => _bMeasureResult = value; }
        public int CELLCOUNT { get => _iCellCount; set => _iCellCount = value; }
        public string TRAYID { get => _sTrayId; set => _sTrayId = value; }
        public bool TRAYIN { get => _bTrayin; set => _bTrayin = value; }
        public bool CHARGING { get => _bCharging; set => _bCharging = value; }
        public bool ENDCHARGING { get => _bEndCharging; set => _bEndCharging = value; }
        public DateTime ARRIVETIME { get => _dtArriveTime; set => _dtArriveTime = value; }
        public DateTime FINISHTIME { get => _dtFinishTime; set => _dtFinishTime = value; }
        public bool COMMANDRUN { get => _bCommandRun; set => _bCommandRun = value; }
        public string LASTCOMMAND { get => _strLastCommand; set => _strLastCommand = value; }
        public string LASTRESPONSE { get => _strLastResponse; set => _strLastResponse = value; }
        public List<Recipe> RECIPE { get => _listRecipe; set => _listRecipe = value; }
        public RecipeDcir RECIPEDCIR { get => _recipeDcir; set => _recipeDcir = value; }
        public string RECIPENO { get => _iRecipeNo; set => _iRecipeNo = value; }
        public bool FSET { get => _bFset; set => _bFset = value; }

        private static TRAYINFO[] nTrayInfo = new TRAYINFO[_Constant.ControllerCount];
        public static TRAYINFO GetInstance(int nIndex)
        {
            if (nTrayInfo[nIndex] == null) nTrayInfo[nIndex] = new TRAYINFO();
            return nTrayInfo[nIndex];
        }
        public TRAYINFO()
        {

        }

        public void InitData(int stageno)
        {
            _iStageNo = stageno;
            _iRemeasureCnt = 0;
            _iCellCount = 0;
            _sTrayId = string.Empty;
            _bTrayin = false;
            _bCharging = false;
            _bEndCharging = false;

            for (int nIndex = 0; nIndex < _Constant.ChannelCount; nIndex++)
            {
                _isCell[nIndex] = false;
                _clrChannelColor[nIndex] = _Constant.ColorReady;
                _sCellSerial[nIndex] = string.Empty;
                _bMeasureResult[nIndex] = false;
            }
        }
    }

    public class Controller
    {
        public string IPADDRESS { get; set; }
        public int PORT { get; set; }
        public int PORT2 { get; set; }
        public int MAXCHANNEL { get; set; }
        public int STAGENO { get; set; }
    }

    public class KeysightMonData
    {
        Util util = new Util();

        private static KeysightMonData mondata;
        public static KeysightMonData GetInstance()
        {
            if (mondata == null) mondata = new KeysightMonData();
            return mondata;
        }

        private int _iStageNo;
        private int _iMaxChannel;

        private string _dtDateTime;
        private UInt64 _iTime;
        private int _iRuncount;
        private int[] _iStatus = new int[_Constant.ChannelCount];
        private List<float> _dVoltage = new List<float>();
        private List<float> _dCurrent = new List<float>();
        private List<float> _dCapacity = new List<float>();

        public int STAGENO { get => _iStageNo; set => _iStageNo = value; }
        public int MAXCHANNEL { get => _iMaxChannel; set => _iMaxChannel = value; }
        public UInt64 STAGETIME { get => _iTime; set => _iTime = value; }
        public int[] CHANNELSTATUS { get => _iStatus; set => _iStatus = value; }
        public List<float> CHANNELVOLTAGE { get => _dVoltage; set => _dVoltage = value; }
        public List<float> CHANNELCURRENT { get => _dCurrent; set => _dCurrent = value; }
        public List<float> CHANNELCAPACITY { get => _dCapacity; set => _dCapacity = value; }
        public string DATETIME { get => _dtDateTime; set => _dtDateTime = value; }
        public int RUNCOUNT { get => _iRuncount; set => _iRuncount = value; }

        public KeysightMonData()
        {
            _iStageNo = 0;
            _iTime = 0;
            _iRuncount = 0;
            for(int nIndex = 0; nIndex < _Constant.ChannelCount; nIndex++)
            {
                _iStatus[nIndex] = 0;
            }
            _dCurrent = new List<float>();
            _dVoltage = new List<float>();
            _dCapacity = new List<float>();
        }
        public KeysightMonData(int iStageNo)
        {
            _iStageNo = iStageNo;
            _iTime = 0;
            _iRuncount = 0;
            for (int nIndex = 0; nIndex < _Constant.ChannelCount; nIndex++)
            {
                _iStatus[nIndex] = 0;
            }
            _dCurrent = new List<float>();
            _dVoltage = new List<float>();
            _dCapacity = new List<float>();
        }

        public void InitData()
        {
            _iTime = 0;
            _iRuncount = 0;
            for (int nIndex = 0; nIndex < _Constant.ChannelCount; nIndex++)
            {
                _iStatus[nIndex] = 0;
            }

            _dCurrent = new List<float>();
            _dVoltage = new List<float>();
            _dCapacity = new List<float>();
        }
        public void SetMonData(string recvMonData)
        {

        }
        public void SetFinData(string recvFinData)
        {

        }
        public void SetSenData(string recvSenData)
        {

        }

        #region 데이터 변환
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
        private int HexToInt2(string hexValue)
        {
            return Convert.ToInt32(hexValue, 16);
        }
        private UInt64 HexToInt(string hexValue)
        {
            return Convert.ToUInt64(hexValue, 16);
        }
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
        public List<float> GetCapacity(int nIndex, List<float> listCurrent, List<float> listCapacity)
        {
            List<float> oCMon = new List<float>();
            float f = 0.0f;

            for (int i = 0; i < listCurrent.Count(); i++)
            {
                if(nIndex == 0)
                    f = listCurrent[i] * (1.0f / 3600.0f);
                else
                    f = listCapacity[i] + listCurrent[i] * (float)(1.0f / 3600.0f);

                oCMon.Add(f);
            }

            return oCMon;
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
        #endregion
    }

    /* FIN DATA - Database 로 대체
    public class KeysightFinData
    {
        private static KeysightFinData[] findata = new KeysightFinData[_Constant.StepCount];
        public static KeysightFinData GetInstance(int nIndex)
        {
            if (findata[nIndex] == null) findata[nIndex] = new KeysightFinData(nIndex);
            return findata[nIndex];
        }

        private int _iStageNo;
        private int _iMaxChannel;

        private UInt64 _iTime;
        private int[] _iStatus = new int[_Constant.ChannelCount];
        private List<float> _dVoltage = new List<float>();
        private List<float> _dCurrent = new List<float>();
        private List<float> _dCapacity = new List<float>();

        public int STAGENO { get => _iStageNo; set => _iStageNo = value; }
        public int MAXCHANNEL { get => _iMaxChannel; set => _iMaxChannel = value; }
        public UInt64 STAGETIME { get => _iTime; set => _iTime = value; }
        public int[] CHANNELSTATUS { get => _iStatus; set => _iStatus = value; }
        public List<float> CHANNELVOLTAGE { get => _dVoltage; set => _dVoltage = value; }
        public List<float> CHANNELCURRENT { get => _dCurrent; set => _dCurrent = value; }
        public List<float> CHANNELCAPACITY { get => _dCapacity; set => _dCapacity = value; }

        public KeysightFinData()
        {
            _iStageNo = 0;
            _iTime = 0;
            for (int nIndex = 0; nIndex < _Constant.ChannelCount; nIndex++)
            {
                _iStatus[nIndex] = 0;
            }
            _dCurrent = new List<float>();
            _dVoltage = new List<float>();
            _dCapacity = new List<float>();
        }
        public KeysightFinData(int iStageNo)
        {
            _iStageNo = iStageNo;
            _iTime = 0;
            for (int nIndex = 0; nIndex < _Constant.ChannelCount; nIndex++)
            {
                _iStatus[nIndex] = 0;
            }
            _dCurrent = new List<float>();
            _dVoltage = new List<float>();
            _dCapacity = new List<float>();
        }
    }
    */

    public class ControllerSenData
    {
        int _iStageNo;
        int _iRunCount;
        enumSenStatus _enSenStatus;
        enumSenStatus _enOldSenStatus;
        string _strConnection;
        string _strServo;
        string _strStepping1;
        string _strStepping2;
        private List<float> _dTemperature = new List<float>();

        public int STAGENO { get => _iStageNo; set => _iStageNo = value; }
        public string CONNECTION { get => _strConnection; set => _strConnection = value; }
        public string STEPPING1 { get => _strStepping1; set => _strStepping1 = value; }
        public string STEPPING2 { get => _strStepping2; set => _strStepping2 = value; }
        public string SERVO { get => _strServo; set => _strServo = value; }
        public List<float> TEMPERATURE { get => _dTemperature; set => _dTemperature = value; }
        public int RUNCOUNT { get => _iRunCount; set => _iRunCount = value; }
        public enumSenStatus SENSTATUS { get => _enSenStatus; set => _enSenStatus = value; }
        public enumSenStatus OLDSENSTATUS { get => _enOldSenStatus; set => _enOldSenStatus = value; }

        private static ControllerSenData[] nSenData = new ControllerSenData[_Constant.ControllerCount];
        public static ControllerSenData GetInstance(int nIndex)
        {
            if (nSenData[nIndex] == null) nSenData[nIndex] = new ControllerSenData(nIndex);
            return nSenData[nIndex];
        }

        public ControllerSenData(int iStageNo)
        {
            _iStageNo = iStageNo;
            _enSenStatus = enumSenStatus.IDL;
            _enOldSenStatus = enumSenStatus.IDL;

            _dTemperature = new List<float>();
            _iRunCount = 0;
        }
        public int GetRunCount(string runcount)
        {
            int count = 0;
            count = Convert.ToInt32(runcount, 16);
            return count;
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
    }

    public class KeysightCalData
    {
        private int _iStageNo;
        private string _dtLastCalDate;
        private string _dtCalStartDate;
        private string _strCalStatus;
        private string _strOldCalStatus;
        private string _strCalProcess;
        private string _strCalTime;

        public int STAGENO { get => _iStageNo; set => _iStageNo = value; }
        public string LASTCALDATE { get => _dtLastCalDate; set => _dtLastCalDate = value; }
        public string CALSTARTDATE { get => _dtCalStartDate; set => _dtCalStartDate = value; }
        public string CALSTATUS { get => _strCalStatus; set => _strCalStatus = value; }
        public string OLDCALSTATUS { get => _strOldCalStatus; set => _strOldCalStatus = value; }
        public string CALPROCESS { get => _strCalProcess; set => _strCalProcess = value; }
        public string CALTIME { get => _strCalTime; set => _strCalTime = value; }

        private static KeysightCalData[] nCalData = new KeysightCalData[_Constant.ControllerCount];
        public static KeysightCalData GetInstance(int nIndex)
        {
            if (nCalData[nIndex] == null) nCalData[nIndex] = new KeysightCalData(nIndex);
            return nCalData[nIndex];
        }
        public KeysightCalData(int iStageNo)
        {
            _iStageNo = iStageNo;
        }
        public void SetCalOldStatus(string oldstatus)
        {
            _strOldCalStatus = oldstatus;
        }
        public string GetCalTime(string caltime)
        {
            return caltime.Replace(';', ' ');
        }
    }
}
