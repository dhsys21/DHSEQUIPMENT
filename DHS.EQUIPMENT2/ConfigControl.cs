using DHS.EQUIPMENT2.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telerik.WinControls.UI;

namespace DHS.EQUIPMENT2
{
    public partial class ConfigControl : UserControl
    {
        Util util = new Util();
        private static ConfigControl configControl = new ConfigControl();
        public static ConfigControl GetInstance()
        {
            if (configControl == null) configControl = new ConfigControl();
            return configControl;
        }
        public ConfigControl()
        {
            InitializeComponent();

            initGridView();

            ReadController();
        }

        public void initGridView()
        {
            dgvControllerList.RowHeadersVisible = false;
            dgvControllerList.ColumnHeadersDefaultCellStyle.Font = new Font("Verdana", 11, FontStyle.Bold);
            dgvControllerList.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvControllerList.Rows.Add(_Constant.ControllerCount);
            dgvControllerList.DefaultCellStyle.Font = new Font("Verdana", 11);
            dgvControllerList.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvControllerList.ReadOnly = true;
            dgvControllerList.MultiSelect = false;
            dgvControllerList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        #region Controller Event
        private void radbtnAddController_Click(object sender, EventArgs e)
        {
            try
            {
                AddController();

                /// 추가 된 recipe 다시 불러오기
                ReadController();
            }
            catch (Exception ex) { Console.WriteLine(ex.ToString()); }
        }
        private void radbtnDelController_Click(object sender, EventArgs e)
        {
            try
            {
                int stageno = util.TryParseInt(tbControllerNo.Text, 0);
                DeleteController(stageno);

                ///  추가 된 recipe 다시 불러오기
                ReadController();
            }
            catch(Exception ex) { Console.WriteLine(ex.ToString()); }
            
        }
        #endregion  Controller Event

        #region controller info
        private void DeleteController(int stageno)
        {
            util.DeleteController(stageno);
        }
        private void AddController()
        {
            Controller controller = new Controller();
            controller.STAGENO = util.TryParseInt(tbControllerNo.Text, 0);
            controller.IPADDRESS = tbContIPaddress01.Text;
            controller.PORT = util.TryParseInt(tbContPort01.Text, 0);
            controller.PORT2 = util.TryParseInt(tbContPort02.Text, 0);
            controller.MAXCHANNEL = util.TryParseInt(tbMaxChannel.Text, 0);

            if(util.ReadControllerInfo(controller.STAGENO.ToString()) == null)
                util.AddControllerInfo(controller);
            else
                util.UpdateControllerInfo(controller);
        }
        private void ReadController()
        {
            util.DeleteRowsGridView(dgvControllerList);
            List<Controller> controllerList = ReadControllerInfo();
            if (controllerList.Count > 0)
            {
                dgvControllerList.Rows.Add(controllerList.Count);
                ShowControllerToDGV(controllerList);
            }
        }
        private List<Controller> ReadControllerInfo()
        {
            return util.ReadControllerInfo();
        }
        private void ShowControllerToDGV(List<Controller> controllerList)
        {
            DataGridView dgv = dgvControllerList;
            int rowIndex = 0;
            foreach (Controller controller in controllerList)
            {
                util.SetValueToGridView((controller.STAGENO).ToString(), rowIndex, 0, dgv);
                util.SetValueToGridView(controller.MAXCHANNEL.ToString(), rowIndex, 1, dgv);
                util.SetValueToGridView(controller.IPADDRESS, rowIndex, 2, dgv);
                util.SetValueToGridView(controller.PORT.ToString(), rowIndex, 3, dgv);
                util.SetValueToGridView(controller.PORT2.ToString(), rowIndex, 4, dgv);
                util.SetValueToGridView("-", rowIndex, 5, dgv);

                rowIndex++;
            }
        }
        #endregion controller info

        #region Show Controller Connection Info
        public void ShowControllerConnectionInfo(int stageno, string ip, string port, string stagename, bool bBtConnected)
        {
            dgvControllerList.Rows[stageno].Cells[0].Value = stagename;
            dgvControllerList.Rows[stageno].Cells[3].Value = port;
            dgvControllerList.Rows[stageno].Cells[5].Value = bBtConnected.ToString();
        }
        #endregion

        private void dgvControllerList_SelectionChanged(object sender, EventArgs e)
        {
            int curRow = -1;
            string serial = string.Empty;
            string maxch = string.Empty;
            string ip = string.Empty;
            string port1 = string.Empty;
            string port2 = string.Empty;
            string name = string.Empty;

            DataGridView dgv = sender as DataGridView;

            if (dgv.CurrentRow.Index != curRow)
            {
                curRow = dgv.CurrentRow.Index;
                if (dgv.Rows[curRow].Cells[1].Value == null) return;

                tbControllerNo.Text = (curRow + 1).ToString();
                tbMaxChannel.Text = dgv.Rows[curRow].Cells[1].Value.ToString();
                tbContIPaddress01.Text = dgv.Rows[curRow].Cells[2].Value.ToString();
                tbContPort01.Text = dgv.Rows[curRow].Cells[3].Value.ToString();
                tbContPort02.Text = dgv.Rows[curRow].Cells[4].Value.ToString();
            }
        }
    }
}
