using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySqlConnector;

namespace DHS.EQUIPMENT2
{
    public partial class MariadbForm : Form
    {
        string connectString = string.Format("Server={0};Database={1};Uid={2};Pwd={3};",
                "172.29.219.119", "dhsys", "guseh", "guseh");
        MySqlConnection conn;
        bool bOpen;
        public MariadbForm()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Connection() == true) SetText(textBox1, "DB is connected" + Environment.NewLine);
            else SetText(textBox1, "DB Connection Error!" + Environment.NewLine);
        }

        private bool Connection()
        {
            try
            {
                conn = new MySqlConnection(connectString);
                conn.Open();
                bOpen = true;
                return bOpen;
            }
            catch(Exception ex)
            {
                Console.Write(ex.ToString());
                return false;
            }
        }
        private void Close()
        {
            try
            {
                if (bOpen == true)
                {
                    conn.Close();
                    bOpen = false;
                    SetText(textBox1, "DB is closed");
                }
            }
            catch(Exception ex)
            {
                Console.Write(ex.ToString());
            }
            conn = null;
        }
        private void SetText(TextBox tb, string msg)
        {
            msg = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + msg;
            tb.AppendText(msg);
        }
        private void button3_Click(object sender, EventArgs e)
        {
            Close();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
