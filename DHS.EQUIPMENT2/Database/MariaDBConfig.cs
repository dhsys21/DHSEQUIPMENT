using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DHS.EQUIPMENT2
{
    public class MariaDBConfig
    {
        private bool _bRead;
        private string _strDbIPaddress;
        private string _strDbPort;
        private string _strDBName;
        private string _strDbUser;
        private string _strDbPwd;

        public string DBIPADDRESS { get => _strDbIPaddress; set => _strDbIPaddress = value; }
        public string DBPORT { get => _strDbPort; set => _strDbPort = value; }
        public string DBUSER { get => _strDbUser; set => _strDbUser = value; }
        public string DBPWD { get => _strDbPwd; set => _strDbPwd = value; }
        public bool ISREAD { get => _bRead; set => _bRead = value; }
        public string DBNAME { get => _strDBName; set => _strDBName = value; }

        private static MariaDBConfig mariaConfig;
        public static MariaDBConfig GetInstance()
        {
            if (mariaConfig == null) mariaConfig = new MariaDBConfig();
            return mariaConfig;
        }
        public MariaDBConfig()
        {

        }
        public void SetInitConfig()
        {
            _bRead = false;

            _strDbIPaddress = "192.168.10.1";
            _strDbPort = "3306";
            _strDBName = "dhsys";
            _strDbUser = "guseh";
            _strDbPwd = "guseh";
        }
    }
}
