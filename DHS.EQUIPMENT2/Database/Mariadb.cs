using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using DHS.EQUIPMENT2.Common;
using MySqlConnector;

namespace DHS.EQUIPMENT2
{
    class MariaDB
    {
        protected string connectString; // "172.29.219.119", "dhsys", "guseh", "guseh"
        protected string connectString2; // "172.29.219.119", "dhsys", "guseh", "guseh"
                                         //protected MySqlConnection dbconn;
        protected bool bOpen;
        protected string _IP = "192.168.10.1";
        protected string _strPort = "3306";
        protected string _strDBName = "dhsys";
        protected string _strUser = "guseh";
        protected string _strPwd = "guseh";
        private int iMaxChannel;
        Util util = new Util();
        private bool bConnected;
        public bool CONNECTED { get => bConnected; set => bConnected = value; }
        protected int MAXCHANNEL { get => iMaxChannel; set => iMaxChannel = value; }

        #region delegate
        public delegate void DBConnection(bool isconnected);
        public event DBConnection OnDBConnection = null;
        protected void RaiseOnDBConnection(bool isconnected)
        {
            if (OnDBConnection != null)
            {
                OnDBConnection(isconnected);
            }
        }

        public delegate void DBUpdateError();
        public event DBUpdateError OnDBUpdateError = null;
        protected void RaiseOnDBUpdateError()
        {
            if (OnDBUpdateError != null)
            {
                OnDBUpdateError();
            }
        }
        #endregion

        private static MariaDB mariadb;
        public static MariaDB GetInstance()
        {
            if (mariadb == null) mariadb = new MariaDB();
            return mariadb;
        }
        public MariaDB()
        {

        }

        #region Maria DB Connection and Query
        protected void InitConnectionString(string strIP, string strPort, string strDBName, string strUser, string strPwd)
        {
            _IP = strIP;
            _strPort = strPort;
            _strDBName = strDBName;
            _strUser = strUser;
            _strPwd = strPwd;

            connectString = string.Format("Server={0};Database={1};Uid={2};Pwd={3};Convert Zero Datetime=True;Connection Timeout=3", _IP, _strDBName, _strUser, _strPwd);
            connectString2 = string.Format("Server={0};Database={1};User ID={2};Password={3};", _IP, _strDBName, _strUser, _strPwd);
        }
        public void Open(string _strIP, string _strPort, string _strDBName, string _strUser, string _strPwd)
        {
            string msg = string.Empty;
            try
            {
                InitConnectionString(_strIP, _strPort, _strDBName, _strUser, _strPwd);

                if (ConnectionDB() == true)
                {
                    bConnected = true;
                    msg = "DB is connected!";
                }
                else
                {
                    bConnected = false;
                    msg = "DB is disconnected!";
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                util.SaveDBLog("DB Connection Error!");
                RaiseOnDBConnection(false);
            }

            util.SaveDBLog(msg);
            RaiseOnDBConnection(bConnected);
        }
        public async void OpenAsync(string _strIP, string _strPort, string _strDBName, string _strUser, string _strPwd)
        {
            string msg = string.Empty;
            try
            {
                InitConnectionString(_strIP, _strPort, _strDBName, _strUser, _strPwd);

                if (await ConnectionDBAsync() == true)
                {
                    bConnected = true;
                    msg = "DB is connected!";
                }
                else
                {
                    bConnected = false;
                    msg = "DB is disconnected!";
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                util.SaveDBLog("DB Connection Error!");
                RaiseOnDBConnection(false);
            }

            util.SaveDBLog(msg);
            RaiseOnDBConnection(bConnected);
        }
        private bool ConnectionDB()
        {
            using (MySqlConnection dbconn = new MySqlConnection(connectString2))
            {
                try
                {
                    dbconn.Open();
                    bOpen = true;
                }
                catch (Exception ex)
                {
                    Console.Write(ex.ToString());
                    bOpen = false;
                    return false;
                }
            }

            return true;
        }
        private async Task<bool> ConnectionDBAsync()
        {
            using (var dbconn = new MySqlConnector.MySqlConnection(connectString2))
            {
                try
                {
                    //dbconn.Open();
                    await dbconn.OpenAsync();
                    bOpen = true;
                }
                catch (Exception ex)
                {
                    Console.Write(ex.ToString());
                    bOpen = false;
                    return false;
                }
            }

            return true;
        }
        #endregion

        #region RECIPE
        public async Task INSERTRECIPEAsync(int recipeno, string orderno, string recipemethod, string time, string current, string voltage)
        {
            string sql = string.Empty;
            if (SELECT_RECIPE_COUNT(recipeno, orderno) > 0)
            {
                //await Task.Run(() => SaveMonDataAll(stageno));
                await Task.Run(() => UPDATE_RECIPE(recipeno, orderno, recipemethod, time, current, voltage));
            }
            else
            {
                await Task.Run(() => INSERT_RECIPE(recipeno, orderno, recipemethod, time, current, voltage));
            }
        }
        public int SELECT_RECIPE_COUNT(int recipeno, string orderno)
        {
            string sql = string.Empty;
            int rowCount = 0;
            var mySqlDataTable = new DataTable();
            using (var dbconn = new MySqlConnector.MySqlConnection(connectString))
            {
                try
                {
                    dbconn.Open();
                    sql = String.Format("SELECT * FROM RECIPE WHERE RECIPE_NO = '{0}' and ORDER_NO = '{1}'", recipeno, orderno);
                    var cmd = new MySqlConnector.MySqlCommand(sql, dbconn);
                    var dr = cmd.ExecuteReader();
                    mySqlDataTable.Load(dr);
                    rowCount = mySqlDataTable.Rows.Count;

                    util.SaveDBLog("Select RECIPE Count 성공!");
                    RaiseOnDBConnection(true);
                }
                catch (Exception ex)
                {
                    Console.Write(ex.ToString());
                    util.SaveDBLog("Select RECIPE Count 에러!");
                    RaiseOnDBConnection(false);
                }
            }

            return rowCount;
        }
        public void INSERT_RECIPE(int recipeno, string orderno, string recipemethod, string time, string current, string voltage)
        {
            string sql = string.Empty;
            var mySqlDataTable = new DataTable();
            using (var dbconn = new MySqlConnector.MySqlConnection(connectString))
            {
                try
                {
                    dbconn.Open();
                    sql = String.Format("INSERT INTO RECIPE(RECIPE_NO, ORDER_NO, RECIPE_METHOD, TIME, CURRENT, VOLTAGE) "
                        + "VALUES('{0}', '{1}', '{2}', '{3}', '{4}', '{5}')",
                        recipeno.ToString(), orderno, recipemethod, time, current, voltage);

                    var cmd = new MySqlConnector.MySqlCommand(sql, dbconn);
                    if (cmd.ExecuteNonQuery() != 1)
                    {
                        util.SaveDBLog("Insert RECIPE 실패");
                        RaiseOnDBConnection(false);
                    }
                    else
                    {
                        util.SaveDBLog("Insert RECIPE 설공");
                        RaiseOnDBConnection(true);
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex.ToString());
                    util.SaveDBLog("Insert RECIPE 에러!");
                    RaiseOnDBConnection(false);
                }
            }
        }
        public void UPDATE_RECIPE(int recipeno, string orderno, string recipemethod, string time, string current, string voltages)
        {
            string sql = string.Empty;
            var mySqlDataTable = new DataTable();
            using (var dbconn = new MySqlConnector.MySqlConnection(connectString))
            {
                try
                {
                    dbconn.Open();
                    sql = String.Format("UPDATE RECIPE SET RECIPE_METHOD = '{0}', TIME = '{1}', " +
                        "CURRENT = '{2}', VOLTAGE = '{3}' WHERE RECIPE_NO = '{4}' AND ORDER_NO = '{5}'", 
                        recipemethod, time, current, voltages, recipeno, orderno);

                    var cmd = new MySqlConnector.MySqlCommand(sql, dbconn);
                    if (cmd.ExecuteNonQuery() != 1)
                    {
                        util.SaveDBLog("Update RECIPE 실패");
                        RaiseOnDBUpdateError();
                    }
                    else
                    {
                        util.SaveDBLog("Update RECIPE 설공");
                        RaiseOnDBConnection(true);
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex.ToString());
                    util.SaveDBLog("Update RECIPE 에러!");
                    RaiseOnDBUpdateError();
                }
            }
        }
        public List<Recipe> GETRECIPEDATAAsync(string recipeno)
        {
            var mySqlDataTable = new DataTable();
            List<Recipe> recipes = new List<Recipe>();
            Recipe recipe;

            try
            {
                mySqlDataTable = SELECT_RECIPETABLE(recipeno);
                //mySqlDataTable = await Task.Run(() => SELECT_RECIPETABLE(recipeno));
                string order_no = string.Empty;
                string recipe_method = string.Empty;
                string current = string.Empty;
                string voltage = string.Empty;
                string time = string.Empty;

                if (mySqlDataTable.Rows.Count < 1) return recipes;
                int rowCount = mySqlDataTable.Rows.Count;

                for (int nIndex = 0; nIndex < rowCount; nIndex++)
                {
                    recipe = new Recipe();
                    DataRow r = mySqlDataTable.Rows[nIndex];

                    recipe.recipeno = recipeno;

                    order_no = r["ORDER_NO"].ToString();
                    recipe.orderno = order_no;

                    recipe_method = r["RECIPE_METHOD"].ToString();
                    recipe.recipemethod = recipe_method;

                    time = r["TIME"].ToString();
                    recipe.time = time;

                    current = r["CURRENT"].ToString();
                    recipe.current = current;

                    voltage = r["VOLTAGE"].ToString();
                    recipe.voltage = voltage;

                    recipes.Add(recipe);
                }
            }
            catch(Exception ex) { Console.WriteLine(ex.ToString()); }
            
            return recipes;
        }
        public async Task<List<Recipe>> GETRECIPEDATASAsync()
        {
            var mySqlDataTable = new DataTable();
            List<Recipe> recipes = new List<Recipe>();
            Recipe recipe = new Recipe();

            try
            {
                //mySqlDataTable = SELECT_RECIPETABLE();
                mySqlDataTable = await Task.Run(() => SELECT_RECIPETABLE());
                string recipe_no = string.Empty;
                string order_no = string.Empty;
                string recipe_method = string.Empty;
                string current = string.Empty;
                string voltage = string.Empty;
                string time = string.Empty;

                if (mySqlDataTable.Rows.Count < 1) return recipes;
                int rowCount = mySqlDataTable.Rows.Count;

                for (int nIndex = 0; nIndex < rowCount; nIndex++)
                {
                    DataRow r = mySqlDataTable.Rows[nIndex];

                    recipe_no = r["RECIPE_NO"].ToString();
                    recipe.recipeno = recipe_no;

                    order_no = r["ORDER_NO"].ToString();
                    recipe.orderno = order_no;

                    recipe_method = r["RECIPE_METHOD"].ToString();
                    recipe.recipemethod = recipe_method;

                    time = r["TIME"].ToString();
                    recipe.time = time;

                    current = r["CURRENT"].ToString();
                    recipe.current = current;

                    voltage = r["VOLTAGE"].ToString();
                    recipe.voltage = voltage;

                    recipes.Add(recipe);
                }
            }
            catch(Exception ex) { Console.WriteLine(ex.ToString()); }

            return recipes;
        }
        private DataTable SELECT_RECIPETABLE(string recipeno)
        {
            string sql = string.Empty;
            string msg = string.Empty;
            var mySqlDataTable = new DataTable();
            using (var dbconn = new MySqlConnector.MySqlConnection(connectString))
            {
                try
                {
                    dbconn.Open();
                    sql = String.Format("SELECT ORDER_NO, RECIPE_METHOD, TIME, CURRENT, VOLTAGE FROM RECIPE WHERE RECIPE_NO = '{0}' ORDER BY ORDER_NO asc", recipeno);
                    var cmd = new MySqlConnector.MySqlCommand(sql, dbconn);
                    var dr = cmd.ExecuteReader();
                    mySqlDataTable.Load(dr);
                    dr.Close();

                    msg = "SELECT RECIPE 성공 : " + sql;
                    RaiseOnDBConnection(true);
                }
                catch (Exception ex)
                {
                    Console.Write(ex.ToString());
                    msg = "SELECT RECIPE 실패 : " + sql;
                    RaiseOnDBConnection(false);
                }
                util.SaveDBLog(msg);
            }
            return mySqlDataTable;
        }
        private DataTable SELECT_RECIPETABLE()
        {
            string sql = string.Empty;
            string msg = string.Empty;
            var mySqlDataTable = new DataTable();
            using (var dbconn = new MySqlConnector.MySqlConnection(connectString))
            {
                try
                {
                    dbconn.Open();
                    sql = String.Format("SELECT ORDER_NO, RECIPE_METHOD, TIME, CURRENT, VOLTAGE FROM RECIPE ORDER BY ORDER_NO asc");
                    var cmd = new MySqlConnector.MySqlCommand(sql, dbconn);
                    var dr = cmd.ExecuteReader();
                    mySqlDataTable.Load(dr);
                    dr.Close();

                    msg = "SELECT RECIPE 성공 : " + sql;
                    RaiseOnDBConnection(true);
                }
                catch (Exception ex)
                {
                    Console.Write(ex.ToString());
                    msg = "SELECT RECIPE 실패 : " + sql;
                    RaiseOnDBConnection(false);
                }
                util.SaveDBLog(msg);
            }
            return mySqlDataTable;
        }
        public bool DELETERECIPEDATA(string recipeno)
        {
            string sql = string.Empty;
            string msg = string.Empty;
            using (var dbconn = new MySqlConnector.MySqlConnection(connectString))
            {
                try
                {
                    dbconn.Open();
                    sql = String.Format("DELETE FROM RECIPE WHERE RECIPE_NO = '{0}'", recipeno);
                    var cmd = new MySqlConnector.MySqlCommand(sql, dbconn);
                    var dr = cmd.ExecuteReader();
                    dr.Close();

                    msg = "DELETE RECIPE Number. " + recipeno + " 성공 : " + sql;
                    util.SaveDBLog(msg);
                    RaiseOnDBConnection(true);
                    return true;
                }
                catch (Exception ex)
                {
                    Console.Write(ex.ToString());
                    msg = "DELETE RECIPE Number. " + recipeno + " 실패 : " + sql;
                    util.SaveDBLog(msg);
                    RaiseOnDBConnection(false);
                    return false;
                }
            }
        }
        #endregion

        #region GET MON DATA
        public KeysightMonData GETMONDATAAsync(int stageno)
        {
            var mySqlMonTable = new DataTable();
            KeysightMonData monData = null;

            try
            {
                mySqlMonTable = SELECTMONTABLE(stageno);
                //mySqlDataTable = await Task.Run(() => SELECTMONTABLE(stageno));
                string datetime = string.Empty;
                string time = string.Empty;
                string status = string.Empty;
                string current = string.Empty;
                string voltage = string.Empty;
                string capacity = string.Empty;

                int nIndex = 0;
                if (mySqlMonTable.Rows.Count < 1) return monData;
                monData = new KeysightMonData();
                //nIndex = mySqlDataTable.Rows.Count - 1;
                nIndex = 0;

                DataRow r = mySqlMonTable.Rows[nIndex];

                //* DATETIME
                datetime = r["DATETIME"].ToString();
                monData.DATETIME = datetime;

                //* Time
                time = r["TIME"].ToString();
                monData.STAGETIME = monData.GetTime(time);

                //* Status
                status = r["STATUS"].ToString();
                monData.CHANNELSTATUS = GETSTATUS(status);

                //* Current
                current = r["CURRENT"].ToString();
                monData.CHANNELCURRENT = monData.GetCurrent(current);

                //* Voltage
                voltage = r["VOLTAGE"].ToString();
                monData.CHANNELVOLTAGE = monData.GetVoltage(voltage);

                //* Capacity
                capacity = r["CAPACITY"].ToString();
                monData.CHANNELCAPACITY = monData.GetVoltage(capacity);
            }
            catch(Exception ex) { Console.WriteLine(ex.ToString()); }
            
            return monData;
        }
        public KeysightMonData GETMONDATAFORCAPACITY(int stageno)
        {
            var mySqlMonTables = new DataTable();
            KeysightMonData[] monData = null;

            try
            {
                string datetime = string.Empty;
                string time = string.Empty;
                string status = string.Empty;
                string current = string.Empty;
                string voltage = string.Empty;
                string capacity = string.Empty;

                mySqlMonTables = SELECTMONTABLEALL(stageno);
                monData = new KeysightMonData[mySqlMonTables.Rows.Count];
                for (int nIndex = 0; nIndex < mySqlMonTables.Rows.Count; nIndex++)
                {
                    monData[nIndex] = new KeysightMonData();
                    DataRow r = mySqlMonTables.Rows[nIndex];

                    //* DATETIME
                    datetime = r["DATETIME"].ToString();
                    monData[nIndex].DATETIME = datetime;

                    //* Time
                    time = r["TIME"].ToString();
                    monData[nIndex].STAGETIME = monData[nIndex].GetTime(time);

                    //* Status
                    status = r["STATUS"].ToString();
                    monData[nIndex].CHANNELSTATUS = GETSTATUS(status);

                    //* Current
                    current = r["CURRENT"].ToString();
                    monData[nIndex].CHANNELCURRENT = monData[nIndex].GetCurrent(current);

                    //* Voltage
                    voltage = r["VOLTAGE"].ToString();
                    monData[nIndex].CHANNELVOLTAGE = monData[nIndex].GetVoltage(voltage);

                    //* Capacity
                    capacity = r["CAPACITY"].ToString();
                    if (nIndex == 0)
                        monData[nIndex].CHANNELCAPACITY = monData[nIndex].GetCapacity(nIndex, monData[nIndex].CHANNELCURRENT, monData[nIndex].CHANNELCAPACITY);
                    else
                        monData[nIndex].CHANNELCAPACITY = monData[nIndex].GetCapacity(nIndex, monData[nIndex].CHANNELCURRENT, monData[nIndex - 1].CHANNELCAPACITY);
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.ToString()); }

            return monData[mySqlMonTables.Rows.Count - 1];
        }
        public KeysightMonData[] GETMONDATAALL(int stageno)
        {
            var mySqlDataTable = new DataTable();
            KeysightMonData[] monData = null;

            try
            {
                string datetime = string.Empty;
                string runcount = string.Empty;
                string time = string.Empty;
                string status = string.Empty;
                string current = string.Empty;
                string voltage = string.Empty;
                string capacity = string.Empty;

                mySqlDataTable = SELECTMONTABLEALL(stageno);
                //mySqlDataTable = await Task.Run(() => SELECTMONTABLEALL(stageno));
                monData = new KeysightMonData[mySqlDataTable.Rows.Count];

                for (int nIndex = 0; nIndex < mySqlDataTable.Rows.Count; nIndex++)
                {
                    monData[nIndex] = new KeysightMonData();

                    DataRow r = mySqlDataTable.Rows[nIndex];

                    //* DATETIME
                    datetime = r["DATETIME"].ToString();
                    monData[nIndex].DATETIME = datetime;

                    //* RUNCOUNT
                    runcount = r["RUNCOUNT"].ToString();
                    monData[nIndex].RUNCOUNT = util.TryParseInt(runcount, 0);

                    //* Time Stamp -> 시작시간 기준으로 흐른 시간으로 변경 필요
                    time = r["TIME"].ToString();
                    monData[nIndex].STAGETIME = monData[nIndex].GetTime(time);

                    //* Status
                    status = r["STATUS"].ToString();
                    monData[nIndex].CHANNELSTATUS = GETSTATUS(status);

                    //* Current
                    current = r["CURRENT"].ToString();
                    monData[nIndex].CHANNELCURRENT = monData[nIndex].GetCurrent(current);

                    //* Voltage
                    voltage = r["VOLTAGE"].ToString();
                    monData[nIndex].CHANNELVOLTAGE = monData[nIndex].GetVoltage(voltage);

                    //* Capacity
                    capacity = r["CAPACITY"].ToString();
                    if(nIndex == 0)
                        monData[nIndex].CHANNELCAPACITY = monData[nIndex].GetCapacity(nIndex, monData[nIndex].CHANNELCURRENT, monData[nIndex].CHANNELCAPACITY);
                    else
                        monData[nIndex].CHANNELCAPACITY = monData[nIndex].GetCapacity(nIndex, monData[nIndex].CHANNELCURRENT, monData[nIndex - 1].CHANNELCAPACITY);
                }
            }
            catch(Exception ex) { Console.WriteLine(ex.ToString()); }

            return monData;
        }
        private DataTable SELECTMONTABLE(int stageno)
        {
            string sql = string.Empty;
            string msg = string.Empty;
            var mySqlDataTable = new DataTable();
            using (var dbconn = new MySqlConnector.MySqlConnection(connectString))
            {
                try
                {
                    dbconn.Open();
                    //sql = String.Format("SELECT TIME, STATUS, CURRENT, VOLTAGE, CAPACITY FROM MON WHERE STAGENO = '{0}' AND TRAYID = '{1}'", stageno, trayid);
                    sql = String.Format("SELECT DATETIME, TIME, STATUS, CURRENT, VOLTAGE, CAPACITY FROM {1} WHERE STAGENO = '{0}' ORDER BY DATETIME desc limit 1", stageno, "mon_stage" + stageno.ToString("D3"));
                    var cmd = new MySqlConnector.MySqlCommand(sql, dbconn);
                    var dr = cmd.ExecuteReader();
                    mySqlDataTable.Load(dr);
                    dr.Close();

                    msg = "SELECT MON DATA 성공 : " + sql;
                    RaiseOnDBConnection(true);
                }
                catch (Exception ex)
                {
                    Console.Write(ex.ToString());
                    msg = "SELECT MON DATA 실패 : " + sql;
                    RaiseOnDBConnection(false);
                }
                util.SaveDBLog(msg);
            }
            return mySqlDataTable;
        }
        private DataTable SELECTMONTABLE_LAST2ROWS(int stageno)
        {
            string sql = string.Empty;
            string msg = string.Empty;
            var mySqlDataTable = new DataTable();
            using (var dbconn = new MySqlConnector.MySqlConnection(connectString))
            {
                try
                {
                    dbconn.Open();
                    sql = String.Format("SELECT DATETIME, TIME, STATUS, CURRENT, VOLTAGE, CAPACITY FROM (SELECT * FROM {1} WHERE STAGENO = '{0}' ORDER BY DATETIME desc limit 2) AS dt ORDER BY DATETIME asc", stageno, "mon_stage" + stageno.ToString("D3"));
                    var cmd = new MySqlConnector.MySqlCommand(sql, dbconn);
                    var dr = cmd.ExecuteReader();
                    mySqlDataTable.Load(dr);
                    dr.Close();

                    msg = "SELECT MON DATA 성공 : " + sql;
                    RaiseOnDBConnection(true);
                }
                catch (Exception ex)
                {
                    Console.Write(ex.ToString());
                    msg = "SELECT MON DATA 실패 : " + sql;
                    RaiseOnDBConnection(false);
                }
                util.SaveDBLog(msg);
            }
            return mySqlDataTable;
        }
        private DataTable SELECTMONTABLEALL(int stageno)
        {
            string sql = string.Empty;
            string tablename;
            string msg;
            var mySqlDataTable = new DataTable();
            using (var dbconn = new MySqlConnector.MySqlConnection(connectString))
            {
                try
                {
                    dbconn.Open();
                    tablename = "mon_stage" + stageno.ToString("D3");
                    sql = String.Format("SELECT DATETIME, RUNCOUNT, TIME, STATUS, CURRENT, VOLTAGE, CAPACITY FROM {1} WHERE STAGENO = '{0}' ORDER BY DATETIME ASC", stageno, tablename);
                    var cmd = new MySqlConnector.MySqlCommand(sql, dbconn);
                    var dr = cmd.ExecuteReader();
                    mySqlDataTable.Load(dr);
                    dr.Close();

                    msg = "SELECT MON DATA 성공 : " + sql;
                    RaiseOnDBConnection(true);
                }
                catch (Exception ex)
                {
                    Console.Write(ex.ToString());
                    msg = "SELECT MON DATA 실패 : " + sql;
                    RaiseOnDBConnection(false);
                }
                util.SaveDBLog(msg);
            }
            return mySqlDataTable;
        }
        #endregion

        #region GET SEN DATA
        public async Task<ControllerSenData[]> GETSENDATAAsync()
        {
            var mySqlDataTable = new DataTable();
            ControllerSenData[] senData = new ControllerSenData[_Constant.ControllerCount];
            int istageno = 0;

            try
            {
                //mySqlDataTable = SELECTSENTABLEALL();
                mySqlDataTable = await Task.Run(() => SELECTSENTABLEALL());

                if (mySqlDataTable.Rows.Count < 1) return senData;
                for (int nIndex = 0; nIndex < mySqlDataTable.Rows.Count; nIndex++)
                {
                    DataRow r = mySqlDataTable.Rows[nIndex];

                    string stageno = r["STAGENO"].ToString();
                    istageno = Convert.ToInt32(stageno) - 1;

                    senData[istageno] = ControllerSenData.GetInstance(nIndex);
                    senData[istageno].STAGENO = istageno;

                    string count = r["COUNT"].ToString();
                    senData[istageno].RUNCOUNT = senData[istageno].GetRunCount(count);

                    string eqstatus = r["EQSTATUS"].ToString();
                    if (eqstatus == "RUN")
                    {
                        if (senData[istageno].SENSTATUS == senData[istageno].OLDSENSTATUS)
                            senData[istageno].SENSTATUS = enumSenStatus.RUN;
                        else if (senData[istageno].SENSTATUS != senData[istageno].OLDSENSTATUS)
                            senData[istageno].OLDSENSTATUS = enumSenStatus.RUN;
                    }
                    else if (eqstatus == "IDL")
                    {
                        if (senData[istageno].SENSTATUS == senData[istageno].OLDSENSTATUS)
                            senData[istageno].SENSTATUS = enumSenStatus.IDL;
                        else if (senData[istageno].SENSTATUS != senData[istageno].OLDSENSTATUS)
                            senData[istageno].OLDSENSTATUS = enumSenStatus.IDL;
                    }

                    string connection = r["CONNECTION"].ToString();
                    senData[istageno].CONNECTION = connection;

                    string stepping1 = r["STEPPING1"].ToString();
                    senData[istageno].STEPPING1 = senData[istageno].GetStepping(stepping1);

                    string stepping2 = r["STEPPING2"].ToString();
                    senData[istageno].STEPPING2 = senData[istageno].GetStepping(stepping2);

                    string servo = r["SERVO"].ToString();
                    senData[istageno].SERVO = senData[istageno].GetStepping(servo);

                    string temperature = r["TEMPERATURE"].ToString();
                    senData[istageno].TEMPERATURE = senData[istageno].GetTemperature(temperature);
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.ToString()); }

            return senData;
        }
        public ControllerSenData GETSENDATA(int nStageNo)
        {
            var mySqlDataTable = new DataTable();
            ControllerSenData senData = new ControllerSenData(nStageNo - 1);

            try
            {
                //mySqlDataTable = SELECTSENTABLEALL();
                mySqlDataTable = SELECTSENTABLE(nStageNo);

                if (mySqlDataTable.Rows.Count < 1) return senData;
                for (int nIndex = 0; nIndex < mySqlDataTable.Rows.Count; nIndex++)
                {
                    DataRow r = mySqlDataTable.Rows[nIndex];

                    string stageno = r["STAGENO"].ToString();
                    if(nStageNo != Convert.ToInt32(stageno)) return null;

                    senData.STAGENO = nStageNo - 1;

                    string count = r["COUNT"].ToString();
                    senData.RUNCOUNT = senData.GetRunCount(count);

                    string eqstatus = r["EQSTATUS"].ToString();
                    if (eqstatus == "RUN")
                    {
                        if (senData.SENSTATUS == senData.OLDSENSTATUS)
                            senData.SENSTATUS = enumSenStatus.RUN;
                        else if (senData.SENSTATUS != senData.OLDSENSTATUS)
                            senData.OLDSENSTATUS = enumSenStatus.RUN;
                    }
                    else if (eqstatus == "IDL")
                    {
                        if (senData.SENSTATUS == senData.OLDSENSTATUS)
                            senData.SENSTATUS = enumSenStatus.IDL;
                        else if (senData.SENSTATUS != senData.OLDSENSTATUS)
                            senData.OLDSENSTATUS = enumSenStatus.IDL;
                    }

                    string connection = r["CONNECTION"].ToString();
                    senData.CONNECTION = connection;

                    string stepping1 = r["STEPPING1"].ToString();
                    senData.STEPPING1 = senData.GetStepping(stepping1);

                    string stepping2 = r["STEPPING2"].ToString();
                    senData.STEPPING2 = senData.GetStepping(stepping2);

                    string servo = r["SERVO"].ToString();
                    senData.SERVO = senData.GetStepping(servo);

                    string temperature = r["TEMPERATURE"].ToString();
                    senData.TEMPERATURE = senData.GetTemperature(temperature);
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.ToString()); }

            return senData;
        }
        public DataTable SELECTSENTABLEALL()
        {
            string sql = string.Empty;
            string msg = string.Empty;
            var mySqlDataTable = new DataTable();
            using (var dbconn = new MySqlConnector.MySqlConnection(connectString))
            {
                try
                {
                    dbconn.Open();
                    sql = "SELECT STAGENO, COUNT, TIME, EQSTATUS, CONNECTION, SERVO, " +
                        "STEPPING1, STEPPING2, TEMPERATURE FROM SEN ORDER BY STAGENO ASC";

                    var cmd = new MySqlConnector.MySqlCommand(sql, dbconn);
                    var dr = cmd.ExecuteReader();
                    mySqlDataTable.Load(dr);
                    dr.Close();

                    msg = "SELECT SEN DATA 성공 : " + sql;
                    RaiseOnDBConnection(true);
                }
                catch (Exception ex)
                {
                    Console.Write(ex.ToString());
                    msg = "SELECT SEN DATA 실패 : " + sql;
                    RaiseOnDBConnection(false);
                }
                util.SaveDBLog(msg);
            }
            return mySqlDataTable;
        }
        public DataTable SELECTSENTABLE(int stageno)
        {
            string sql = string.Empty;
            string msg = string.Empty;
            var mySqlDataTable = new DataTable();
            using (var dbconn = new MySqlConnector.MySqlConnection(connectString))
            {
                try
                {
                    dbconn.Open();
                    sql = "SELECT STAGENO, COUNT, TIME, EQSTATUS, CONNECTION, SERVO, " +
                        "STEPPING1, STEPPING2, TEMPERATURE FROM SEN WHERE STAGENO = " + stageno.ToString() + " ORDER BY STAGENO ASC";

                    var cmd = new MySqlConnector.MySqlCommand(sql, dbconn);
                    var dr = cmd.ExecuteReader();
                    mySqlDataTable.Load(dr);
                    dr.Close();

                    msg = "SELECT SEN DATA 성공 : " + sql;
                    RaiseOnDBConnection(true);
                }
                catch (Exception ex)
                {
                    Console.Write(ex.ToString());
                    msg = "SELECT SEN DATA 실패 : " + sql;
                    RaiseOnDBConnection(false);
                }
                util.SaveDBLog(msg);
            }
            return mySqlDataTable;
        }
        #endregion

        #region CALIBRATION
        public KeysightCalData[] GETCALDATA()
        {
            var mySqlDataTable = new DataTable();
            KeysightCalData[] calData = new KeysightCalData[_Constant.ControllerCount];

            int istageno = 0;
            mySqlDataTable = SELECTCALTABLEALL();

            if (mySqlDataTable.Rows.Count < 1) return calData;
            for (int nIndex = 0; nIndex < mySqlDataTable.Rows.Count; nIndex++)
            {
                DataRow r = mySqlDataTable.Rows[nIndex];

                string stageno = r["STAGENO"].ToString();
                istageno = Convert.ToInt32(stageno) - 1;

                calData[istageno] = KeysightCalData.GetInstance(nIndex);
                calData[istageno].STAGENO = istageno;

                string lastcaldate = r["LASTCALDATE"].ToString();
                calData[istageno].LASTCALDATE = lastcaldate;

                string calstartdate = r["CALSTARTDATE"].ToString();
                calData[istageno].CALSTARTDATE = calstartdate;

                string caltime = r["CALTIME"].ToString();
                calData[istageno].CALTIME = calData[istageno].GetCalTime(caltime);

                string calstatus = r["CALSTATUS"].ToString();
                calData[istageno].CALSTATUS = calstatus;

                string calprocess = r["CALPROCESS"].ToString();
                calData[istageno].CALPROCESS = calprocess;
            }

            return calData;
        }
        public DataTable SELECTCALTABLEALL()
        {
            string sql = string.Empty;
            string msg = string.Empty;
            var mySqlDataTable = new DataTable();
            using (var dbconn = new MySqlConnector.MySqlConnection(connectString))
            {
                try
                {
                    dbconn.Open();
                    sql = "SELECT STAGENO, LASTCALDATE, CALSTARTDATE, CALSTATUS, CALPROCESS, CALTIME FROM CAL ORDER BY STAGENO ASC";

                    var cmd = new MySqlConnector.MySqlCommand(sql, dbconn);
                    var dr = cmd.ExecuteReader();
                    mySqlDataTable.Load(dr);
                    dr.Close();

                    msg = "SELECT CAL DATA 성공 : " + sql;
                    RaiseOnDBConnection(true);
                }
                catch (Exception ex)
                {
                    Console.Write(ex.ToString());
                    msg = "SELECT CAL DATA 실패 : " + sql;
                    RaiseOnDBConnection(false);
                }
                util.SaveDBLog(msg);
            }
            return mySqlDataTable;
        }
        #endregion

        #region TABLE DATA to CHANNEL DATA
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
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            
            return status;
        }
        #endregion

        #region DELETE
        public bool DELETEMONDATA(int nStageNo)
        {
            string sql = string.Empty;
            string msg = string.Empty;
            string tablename = "mon_stage" + nStageNo.ToString("D3");
            using (var dbconn = new MySqlConnector.MySqlConnection(connectString))
            {
                try
                {
                    dbconn.Open();
                    sql = String.Format("DELETE FROM {0} WHERE STAGENO = '{1}'", tablename, nStageNo);
                    var cmd = new MySqlConnector.MySqlCommand(sql, dbconn);
                    var dr = cmd.ExecuteReader();
                    dr.Close();

                    msg = "DELETE " + tablename + " 성공 : " + sql;
                    util.SaveDBLog(msg);
                    RaiseOnDBConnection(true);
                    return true;
                }
                catch (Exception ex)
                {
                    Console.Write(ex.ToString());
                    msg = "DELETE " + tablename + " 실패 : " + sql;
                    util.SaveDBLog(msg);
                    RaiseOnDBConnection(false);
                    return false;
                }
            }
        }
        #endregion
    }
}
