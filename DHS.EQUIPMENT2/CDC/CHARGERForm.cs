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
    public partial class CHARGERForm : Form
    {
        Util util = new Util();

        private int nStageno;
        public int STAGENO { get => nStageno; set => nStageno = value; }
        private static CHARGERForm[] cdForm = new CHARGERForm[_Constant.ControllerCount];

        public static CHARGERForm GetInstance(int nIndex)
        {
            if (cdForm[nIndex] == null) cdForm[nIndex] = new CHARGERForm();
            return cdForm[nIndex];
        }
        public CHARGERForm()
        {
            InitializeComponent();
        }
        public void SetStage(int stageno)
        {
            nStageno = stageno;
            SetTitle(stageno);
        }
        public void SetTitle(int stageno)
        {
            int stage_no = (stageno + 1);
            util.SetValueToLabel(lblStageTitle, "STAGE " + stage_no.ToString("D3"));
            //SetValueToLabel(lblStageTitle, "STAGE " + stage_no.ToString("D3"));
        }
        public void SetStatus(int stageno)
        {
            string msg = "STAGE is ready";
            if(stageno == nStageno)
            {
                util.SetValueToLabel(lblStatus, msg);
            }
        }
    }
}
