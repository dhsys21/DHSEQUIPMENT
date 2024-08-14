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
    public partial class ConfigForm : Form
    {
        Util util = new Util();

        #region Delegation
        public delegate void ChangeServerSetting(string ipaddr, int port);
        public event ChangeServerSetting OnChangeServerSetting = null;
        protected void RaiseOnChangeServerSetting(string ipaddr, int port)
        {
            if (OnChangeServerSetting != null)
            {
                OnChangeServerSetting(ipaddr, port);
            }
        }
        #endregion

        private static ConfigForm configForm = new ConfigForm();
        public static ConfigForm GetInstance()
        {
            if (configForm == null) configForm = new ConfigForm();
            return configForm;
        }
        public ConfigForm()
        {
            InitializeComponent();
        }

        private void radBtnCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void ConfigForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        public string GetIPAddress(int nIndex)
        {
            string ip = tbIpaddress.Text;
            string[] ips = ip.Split('.');
            ip = ips[0] + "." + ips[1] + "." + ips[2] + "." + (Convert.ToInt32(ips[3]) + nIndex);
            return ip;
        }
        public string GetIPAddress()
        {
            string ip = tbIpaddress.Text;
            return ip;
        }
        public int GetPort()
        {
            int port = 10000;
            Int32.TryParse(tbPort.Text, out port);
            //return Convert.ToInt32(tbPort.Text);
            return port;
        }

        private void radBtnSave_Click(object sender, EventArgs e)
        {
            SaveDBConfig();
            RaiseOnChangeServerSetting(tbIpaddress.Text, util.TryParseInt(tbPort.Text, 50000));

            this.Hide();
        }
        public void ReadDBConfig()
        {
            MariaDBConfig mariaConfig = MariaDBConfig.GetInstance();
            util.ReadDBConfig();

            tbDBIPAddress.Text = mariaConfig.DBIPADDRESS;
            tbDBName.Text = mariaConfig.DBNAME;
            tbDBPort.Text = mariaConfig.DBPORT;
            tbDBUser.Text = mariaConfig.DBUSER;
            tbDBPwd.Text = mariaConfig.DBPWD;
        }
        public void SaveDBConfig()
        {
            MariaDBConfig mariaConfig = MariaDBConfig.GetInstance();
            mariaConfig.DBIPADDRESS = tbDBIPAddress.Text;
            mariaConfig.DBNAME = tbDBName.Text;
            mariaConfig.DBPORT = tbDBPort.Text;
            mariaConfig.DBUSER = tbDBUser.Text;
            mariaConfig.DBPWD = tbDBPwd.Text;

            util.SaveDBConfig(mariaConfig);
        }
    }
}
