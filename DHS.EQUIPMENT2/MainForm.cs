using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DHS.EQUIPMENT2.CDC;
using DHS.EQUIPMENT2.Common;

namespace DHS.EQUIPMENT2
{
    public partial class MainForm : Form
    {
        private DCIRControl dCIRControl;
        private ManualModeControl manualModeControl;
        private ConfigControl configControl;
        private MeasureInfoControl measureInfoControl;
        private MeasureChartControl measureChartControl;
        private CalibrationControl calibrationControl;
        private EquipmentStatusControl equipmentStatusControl;
        Util util = new Util();

        private CDProcess _CDProcess = null;
        private MeasureInfoForm measureInfoForm = null;
        private MeasureChartForm measureChartForm = null;
        private ManualModeForm manualForm = null;
        private CalibrationForm caliForm = null;
        private DCIRForm dcirForm = null;
        ConfigForm configForm;
        MariadbForm mariadbForm;
        MariaDB mariadb = null;

        DoubleBufferedPanel[] ParentPanel = new DoubleBufferedPanel[12];

        public MainForm()
        {
            InitializeComponent();
            InitializeUsercontrol();

            //* 실행시 윈도우즈 크기 및 위치
            this.CenterToScreen();
            this.Width = 1920;
            this.Height = 1050;
            this.Left = 0;
            this.Top = 0;

            //* Title 추가 (IROCV, PRECHARGER)
            enumEquipType equipType = enumEquipType.IROCV;
            //AddTitlePanel(equipType);
            //pnlTitle.Dock = DockStyle.Top;

            //* MariaDB
            mariadb = MariaDB.GetInstance();
            mariadb.OnDBConnection += _MariaDB_Connection;

        }

        private void InitializeUsercontrol()
        {
            /// Equipment Status 
            /// 
            equipmentStatusControl = EquipmentStatusControl.GetInstance();
            equipmentStatusControl.Dock = DockStyle.Fill;
            equipmentStatusControl.Parent = pnlMainBody;
            equipmentStatusControl.Visible = false;

            /// DCIR Mode
            /// 
            dCIRControl = DCIRControl.GetInstance();
            dCIRControl.Dock = DockStyle.Fill;
            dCIRControl.Parent = pnlMainBody;
            dCIRControl.Visible = false;

            /// Manual Mode
            /// 
            manualModeControl = ManualModeControl.GetInstance();
            manualModeControl.Dock = DockStyle.Fill;
            manualModeControl.Parent = pnlMainBody;
            manualModeControl.Visible = false;

            /// Calibration Mode
            /// 
            calibrationControl = CalibrationControl.GetInstance();
            calibrationControl.Dock = DockStyle.Fill;
            calibrationControl.Parent = pnlMainBody;
            calibrationControl.Visible = false;

            /// Config 
            /// 
            configControl = ConfigControl.GetInstance();
            configControl.Dock = DockStyle.Fill;
            configControl.Parent = pnlMainBody;
            configControl.Visible = false;

            /// Measure Info
            /// 
            measureInfoControl = MeasureInfoControl.GetInstance();
            measureInfoControl.Dock = DockStyle.Fill;
            measureInfoControl.Parent = pnlMainBody;
            measureInfoControl.Visible = false;

            /// Measure Info Chart
            /// 
            measureChartControl = MeasureChartControl.GetInstance();
            measureChartControl.Dock = DockStyle.Fill;
            measureChartControl.Parent = pnlMainBody;
            measureChartControl.Visible = false;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            mariadbForm = new MariadbForm();

            _CDProcess = CDProcess.GetInstance();
            _CDProcess.OnSetControllerInfo += CDProcess_SetControllerInfo;

            _CDProcess.ControllerConnect();
            _CDProcess.ContConnectionTimer(true);

            for (int nIndex = 0; nIndex < 12; nIndex++)
                ParentPanel[nIndex] = new DoubleBufferedPanel();
        }

        private void _DbsClient_Connection(bool isconnected)
        {
            if (isconnected == true) lblDBSConnection.BackColor = Color.Lime;
            else if (isconnected == false) lblDBSConnection.BackColor = Color.Red;
        }
        private void _MariaDB_Connection(bool isconnected)
        {
            if (isconnected == true) lblDBConnection.BackColor = Color.Lime;
            else if (isconnected == false) lblDBConnection.BackColor = Color.Red;
        }

        #region Menu Button Control
        private void radbtn_SensorInfo_Click(object sender, EventArgs e)
        {
            SelectTabPage(enumTabType.SENSORINFO);
        }
        private void radbtn_ContactResult_Click(object sender, EventArgs e)
        {
            SelectTabPage(enumTabType.CONTACTRESULT);
        }
        private void radbtn_ContactError_Click(object sender, EventArgs e)
        {
            SelectTabPage(enumTabType.CONTACTERROR);
        }
        private void radbtn_ResultInfo_Click(object sender, EventArgs e)
        {
            SelectTabPage(enumTabType.RESULTINFO);
        }
        private void radbtn_Chart_Click(object sender, EventArgs e)
        {
            SetControlVisible(false);
            measureChartControl.Visible = true;
        }
        private void radbtn_MeasureInfo_Click(object sender, EventArgs e)
        {
            SetControlVisible(false);
            measureInfoControl.Visible = true;
        }
        
        private void radbtn_ManualMode_Click(object sender, EventArgs e)
        {
            SetControlVisible(false);
            manualModeControl.Visible = true;
        }
        private void radButton1_Click(object sender, EventArgs e)
        {
            SetControlVisible(false);
            dCIRControl.Visible = true;
        }
        private void radbtn_Calibration_Click(object sender, EventArgs e)
        {
            SetControlVisible(false);
            calibrationControl.Visible = true;
        }
        private void radbtn_Config_Click(object sender, EventArgs e)
        {
            SetControlVisible(false);
            configControl.Visible = true;
        }
        private void radbtn_MANU_Click(object sender, EventArgs e)
        {
            SetControlVisible(false);
            equipmentStatusControl.Visible = true;
        }
        private void SetControlVisible(bool bVisible)
        {
            dCIRControl.Visible = bVisible;
            manualModeControl.Visible = bVisible;
            calibrationControl.Visible = bVisible;
            configControl.Visible = bVisible;
            measureChartControl.Visible = bVisible;
            measureInfoControl.Visible = bVisible;
            equipmentStatusControl.Visible = bVisible;
        }
        private void SelectTabPage(enumTabType enumType)
        {

        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            var result = MessageBox.Show("Are you want to exit CHARGER?", "EXIT CHARGER", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                //SaveDBConfig();

                if (_CDProcess != null) _CDProcess.Close();
                //* Thread stop
                Process.GetCurrentProcess().Kill();
                // application stop
                Application.Exit();
            }
            else
            {
                e.Cancel = true;
            }
        }
        #endregion

        #region CDPROCESS
        private void CDProcess_SetControllerInfo(int stageno, string ip, string port, string stagename, bool bBtConnected)
        {
            configControl.ShowControllerConnectionInfo(stageno, ip, port, stagename, bBtConnected);
        }

        #endregion

    }
}
