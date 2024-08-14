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

namespace DHS.EQUIPMENT2.CDC
{
    public partial class CalibrationControl : UserControl
    {
        #region Delegation
        public delegate void StartCalibration(int stageno);
        public event StartCalibration OnStartCalibration = null;
        protected void RaiseOnStartCalibration(int stageno)
        {
            if (OnStartCalibration != null)
            {
                OnStartCalibration(stageno);
            }
        }
        public delegate void StopCalibration(int stageno);
        public event StopCalibration OnStopCalibration = null;
        protected void RaiseOnStopCalibration(int stageno)
        {
            if (OnStopCalibration != null)
            {
                OnStopCalibration(stageno);
            }
        }
        #endregion

        Util util = new Util();
        private bool bVisible;
        public bool VISIBLE { get => bVisible; set => bVisible = value; }
        private static CalibrationControl calibrationControl = new CalibrationControl();
        public static CalibrationControl GetInstance()
        {
            if (calibrationControl == null) calibrationControl = new CalibrationControl();
            return calibrationControl;
        }
        public CalibrationControl()
        {
            InitializeComponent();
            bVisible = false;

            MakeGridView();
        }

        #region Method
        private void MakeGridView()
        {
            dgvCalStatusList.Rows.Add(_Constant.ControllerCount);
        }
        public void SetVisible(bool isVisibled)
        {
            bVisible = isVisibled;
        }
        public void SetCalStatus(KeysightCalData[] caldata)
        {
            int stageno;
            string process;
            string status;
            string oldstatus;

            if (bVisible == true)
            {
                for (int nIndex = 0; nIndex < caldata.Length; nIndex++)
                {
                    if (caldata[nIndex] != null)
                    {
                        stageno = caldata[nIndex].STAGENO;
                        process = caldata[nIndex].CALPROCESS;
                        status = caldata[nIndex].CALSTATUS;
                        oldstatus = caldata[nIndex].OLDCALSTATUS;

                        util.SetValueToGridView(stageno.ToString(), stageno, 0, dgvCalStatusList);
                        util.SetValueToGridView(caldata[nIndex].LASTCALDATE, stageno, 1, dgvCalStatusList);
                        util.SetValueToGridView(caldata[nIndex].CALSTARTDATE, stageno, 2, dgvCalStatusList);
                        util.SetValueToGridView(caldata[nIndex].CALSTATUS, stageno, 3, dgvCalStatusList);
                        util.SetValueToGridView(caldata[nIndex].CALPROCESS, stageno, 4, dgvCalStatusList);
                        util.SetValueToGridView(caldata[nIndex].CALTIME, stageno, 5, dgvCalStatusList);

                        if (status == "RUNNING") caldata[nIndex].SetCalOldStatus(status);

                        if (oldstatus == "RUNNING" && status == "IDLE" && process == "100%")
                        {
                            RaiseOnStopCalibration(nIndex);
                        }

                    }
                }
            }
        }
        #endregion

        #region Control Event
        private void btnStartCalibration_Click(object sender, EventArgs e)
        {
            int stageno = cbStageName.SelectedIndex;
            RaiseOnStartCalibration(stageno);
        }

        private void btnStopCalibration_Click(object sender, EventArgs e)
        {
            int stageno = cbStageName.SelectedIndex;
            RaiseOnStopCalibration(stageno);
        }
        #endregion  Control Event
    }
}
