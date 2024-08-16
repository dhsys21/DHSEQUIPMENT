using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DHS.EQUIPMENT2.Common
{
    static class _Constant
    {
        public static readonly int ControllerCount = 30;
        public static readonly int ChannelCount = 256;
        public static readonly int SenCount = 18;
        public static readonly int StepCount = 10;
        public static readonly double delDayInterval = 180;

        /// <summary>
        /// Keysight용 Controller 에서 값을 가져올때 필요한 변수
        /// </summary>
        public static readonly string SHARP = "23";
        public static readonly string sharp = "#";
        public static readonly string stx = "@";
        public static readonly string etx = ";";
        public static readonly string STX = "40";
        public static readonly string ETX = "3B";
        public static readonly string MONHEADER = "404D4F4E";
        public static readonly string SENHEADER = "4053454E";
        public static readonly string CALHEADER = "4043414C";
        
        public static readonly string TRB = "@01";
        public static readonly string CONT = "@02";
        public static readonly string SEP = "$0A";

        #region PATH
        public static readonly string APP_PATH = @"D:\INSPECTION\";
        public static readonly string BIN_PATH = APP_PATH + "Bin\\";
        public static readonly string DATA_PATH = APP_PATH + "Data\\";
        public static readonly string LOG_PATH = APP_PATH + "Log\\";
        public static readonly string MSA_PATH = APP_PATH + "MSA\\";
        public static readonly string OFFSET_PATH = APP_PATH + "OFFSET\\";
        public static readonly string TRAY_PATH = APP_PATH + "Tray\\";
        public static readonly string CONFIG_FILE = BIN_PATH + "Systeminfo.inf";
        public static readonly string RECIPE_FILE = BIN_PATH + "Recipe.inf";
        #endregion

        #region mariadb/ DBS config file
        public static readonly string dbs_name = "DBSCLIENT";

        public static readonly string host_name = "IPADDRESS";
        public static readonly string port_name = "PORT";
        public static readonly string db_use_name = "DHSYS";
        public static readonly string db_name = "MARIADB";
        public static readonly string db_user_name = "USER";
        public static readonly string db_pwd_name = "PASSWORD";
        #endregion

        #region Color Status
        //* 측정 color
        public static readonly Color ColorReady = Color.LightBlue;
        public static readonly Color ColorNoCell = Color.Silver;
        public static readonly Color ColorOutFlow = Color.Yellow;
        public static readonly Color ColorCharging = Color.Orange;
        public static readonly Color ColorDischarging = Color.DarkOrange;
        public static readonly Color ColorFinish = Color.LimeGreen;
        public static readonly Color ColorVoltage = Color.LightBlue;
        public static readonly Color ColorCurrent = Color.Thistle;
        public static readonly Color ColorCapacity = Color.PeachPuff;
        public static readonly Color colorStatus = Color.Gainsboro;
        public static readonly Color ColorFail = Color.Red;
        #endregion

        #region PLC - PC ADDRESS
        //public static readonly int DB_NUMBER = 85;
        public static readonly int PLC_DATA_LENGTH = 50;
        public static readonly int PC_DATA_LENGTH = 50;
        public static readonly int TRAY_ID_LENGTH = 20;

        public static readonly int PLC_HEART_BEAT = 0;
        public static readonly int PLC_ATUO_MANUAL = 1;
        public static readonly int PLC_ERROR = 2;
        public static readonly int PLC_TRAY_IN = 3;
        public static readonly int PLC_TRAY_DOWN = 4;
        public static readonly int PLC_TRAY_UP = 5;
        public static readonly int PLC_JOB_CHANGE = 6;
        public static readonly int PLC_READY_COMPLETE = 7;
        public static readonly int PLC_UNLOADING_COMPLETE = 8;
        public static readonly int PLC_TRAY_ID = 10;


        public static readonly int PC_HEART_BEAT = 0;
        public static readonly int PC_AUTO_MANUAL = 1;
        public static readonly int PC_ERROR = 2;
        public static readonly int PC_TRAY_OUT = 3;
        public static readonly int PC_TRAY_DOWN = 4;
        public static readonly int PC_TRAY_UP = 5;
        public static readonly int PC_MEASUREMENT_WAIT = 6;
        public static readonly int PC_RUNNING = 7;
        public static readonly int PC_MEASUREMENT_COMPLETE = 8;

        #endregion

        #region Error (SYST:ERR?)
        public static readonly string[] BtErrorList = {
            //* No Error
            "+0,\"No error\"",
            //* Error List
            //* Device-dependent Errors
            "308, \"Not allowed while sequence is running\"",
            "309, \"Cell has no sequence enabled\"",
            "310, \"Seq current exceeds cell capacity\"",
            "311, \"Cell is running a sequence\"",
            "312,\"Seq current exceeds limit for precharge step\"",
            "313, \"Cell init error, inhibit is latched\"",
            "314, \"Seq current does not meet minimum channel requirements\"",
            "315, \"Invalid Sequence Step\"",
            "316, \"Sequencer Call Failed\"",
            "400, \"System Monitor: Cell Enable Line Asserted. Hardware Panic Detected.\"",
            "401, \"System Monitor: Temperature Out of Range.\"",
            "402, \"System Monitor: Fan Speed Out of Range.\"",
            "403, \"System Monitor: PFC Temperature Out of Range.\"",
            "500, \"Output Protection Asserted\"",
            "513, \"LAN invalid IP address\"",
            "514,\"LAN duplicate IP address\"",
            "515,\"LAN failed to renew DHCP lease\"",
            "516,\"LAN failed to configure\"",
            //* 4 Programming Reference
            //* 130 BT2200 Series Operating and Service Guide
            "517,\"LAN failed to initialize\"",
            "518, \"LAN VXI-11 fault\"",
            "544, \"Backplane ID doesn't match expected ID for model number\"",
            "609,\"System ADC test failed\"",
            "610, \"Fan test failed\"",
            "611, \"EEPROM load failed\"",
            "612, \"EEPROM checksum failed\"",
            "613, \"EEPROM save failed\"",
            "614, \"Invalid serial number\"",
            "615, \"Invalid MAC address\"",
            "900, \"Firmware update failed\"",
            "901, \"Fgpa update start failed\"",
            "-1000, \"Cell Add Error\"",
            "-1001, \"Invalid Cell Id\"",
            "-1002, \"FW update: invalid data size!\"",
            "-1003, \"Unable to get CAN send lock!\"",
            "-1004, \"Sequences aborted due to watchdog timeout\"",
            "-1005, \"Card Error Detected!\"",
            "-1006, \"Timeout.\"",
            "-1007, \"Lan Settings Error! Send SYST:ERR? for details.\"",
            "-1008, \"CRC Error\"",
            "-1009, \"FW update No Ack from Card\"",
            "-1010, \"Interface Card Cal Corrupted!\"",
            "-1011, \"Sequencer Locked.\"",
            "-1012, \"Brownout Condition Detected.\"",
            "-1013, \"Model Number does not match PFC dipswitch setting, Fets Locked!\"",
            "-1014, \"Calibration Step Missing!\"",
            "-1015, \"Calculated Calibration Constants outside of expected limits.\"",
            //* Command Errors
            "-100, \"Command error\"",
            //* BT2200 Series Operating and Service Guide 131
            //* 4 Programming Reference
            "-101, \"Invalid character\"",
            "-102, \"Syntax error\"",
            "-103, \"Invalid separator\"",
            "-104, \"Data type error\"",
            "-105, \"GET not allowed\"",
            "-108, \"Parameter not allowed\"",
            "-109, \"Missing parameter\"",
            "-110, \"Command header error\"",
            "-111, \"Header separator error\"",
            "-112, \"Program mnemonic too long\"",
            "-113, \"Undefined header\"",
            "-114, \"Header suffix out of range\"",
            "-120, \"Numeric data error\"",
            "-121, \"Invalid character in number\"",
            "-123, \"Exponent too large\"",
            "-124, \"Too many digits\"",
            "-128, \"Numeric data not allowed\"",
            "-130, \"Suffix error\"",
            "-131, \"Invalid suffix\"",
            "-134, \"Suffix too long\"",
            "-138, \"Suffix not allowed\"",
            "-140, \"Character data error\"",
            "-141, \"Invalid character data\"",
            "-144, \"Character data too long\"",
            "-148, \"Character data not allowed\"",
            "-150, \"String data error\"",
            "-151, \"Invalid string data\"",
            "-158, \"String data not allowed\"",
            "-160, \"Block data error\"",
            "-161, \"Invalid block data\"",
            //* 4 Programming Reference
            //* 132 BT2200 Series Operating and Service Guide
            "-168, \"Block data not allowed\"",
            "-170, \"Expression error\"",
            "-171, \"Invalid expression\"",
            "-178, \"Expression data not allowed\"",
            //* Execution Errors
            "-222, \"Data out of range\"",
            "-223, \"Too much data\"",
            "-224, \"Illegal parameter value\"",
            "-225, \"Out of memory\"",
            "-226, \"Command msg invalid\"",
            "-227, \"CAN aborted\"",
            "-228, \"CAN send failed\"",
            "-229, \"Invalid card and/or channel param\"",
            "-231, \"Output buffer overflow\"",
            "-234, \"Data corrupt or stale\"",
            "-241, \"Hardware missing\"",
            //* Internal Errors
            "-310, \"System error\"",
            "-311, \"Memory error\"",
            "-313, \"Calibration memory lost\"",
            "-321, \"Out of memory\"",
            "-330, \"Self-test failed\"",
            "-350, \"Queue overflow\"",
            "-363, \"Input buffer overrun\"",
            //* Query Errors
            "-400, \"Query error\"",
            "-410, \"Query INTERRUPTED\"",
            "-420, \"Query UNTERMINATED\"",
            "-430, \"Query DEADLOCKED\"",
            "-440, \"Query UNTERMINATED after indefinite response\""
        };
        #endregion
    }
    public enum enumControllerStatus
    {
        IDL = 1,
        EMP = 2,
        ARV = 3,
        RDY = 4,
        RUN = 5,
        STB = 6,
        EMS = 7,
        MAN = 8,
        RST = 9,
        LOC = 10,
        ERR = 11,
        BZY = 12
    }
    public enum enumBTCommandType
    {
        DEFINE = 1,
        AMS = 2,
        STP = 3,
        ERR = 4,
        QUERY = 5
    }
    public enum enumCommandType
    {
        TRB = 1,
        CONT = 2,
        TRS = 3
    }
    public enum enumEquipMode
    {
        AUTO = 0,
        MANUAL = 1, //* MANUAL == MSA
        OFFSET = 2
    }
    public enum enumMsaStatus
    {
        StepTrayDown = 0,
        StepTrayUp = 1,
        StepFinish = 3
    }
    public enum enumEquipStatus
    {
        StepVacancy = 0,
        StepTrayIn = 1,
        StepReady = 2,
        StepRun = 3,
        StepEnd = 4,
        StepTrayOut = 5,
        StepManual = 6,
        StepNoAnswer = 7,
        StepEmergency = 8
    }
    public enum enumSenStatus
    {
        IDL = 0,
        RUN = 1
    }
    public enum enumSocketConnectionMode
    {
        // Server Style
        PASSIVE,
        // Client Style
        ACTIVE
    }
    public enum enumSocketMessageType
    {
        RECEIVE,
        SEND
    }
    public enum enumConnectionState
    {
        Disabled = 0,
        Enabled = 1,
        Disconnected = 2,
        Connected = 3,
        Retry = 4,
        TimeOut = 5
    }
    public enum enumEquipType
    {
        IROCV = 0,
        PRECHARGER = 1
    }
    public enum enumStartPosition
    {
        LEFTTOPTORIGHT = 0,
        LEFTTOPTOBELOW = 1,
        LEFTBOTTOMTORIGHT = 2,
        LEFTBOTTOMETOABOVE = 3,
        RIGHTTOPTOLEFT = 4,
        RIGHTTOPTOBELOW = 5,
        RIGHTBOTTOMTOLEFT = 6,
        RIGHTBOTTOMTOABOVE = 7
    }
    public enum enumTabType
    {
        EQUIPSTATUS = 0,
        MEASUREINFO = 1,
        SENSORINFO = 2,
        RESULTINFO = 3,
        CONTACTRESULT = 4,
        CONTACTERROR = 5,
        MANUALMODE = 6,
        MEASURECHART = 7,
        CALIBRATION = 8,
        DCIR = 9
    }
    public enum enumValueType
    {
        VOLTAGE = 0,
        CURRENT = 1,
        STATUS = 2
    }
    public enum enumMeasureResult
    {
        NOCELL = -1,
        OK = 0,
        IRNG = 1,
        OCVNG = 2,
        VOLTAGENG = 3,
        CURRENTNG = 4
    }
}
