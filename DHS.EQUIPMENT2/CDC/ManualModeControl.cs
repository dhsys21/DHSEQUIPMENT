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
    public partial class ManualModeControl : UserControl
    {
        #region Delegation
        public delegate void SendKeysightCommand(int stageno, string cmd, enumCommandType enCmddType, enumBTCommandType enBtCmdType);
        public event SendKeysightCommand OnKeysightCommand = null;
        protected void RaiseOnKeysightCommand(int stageno, string cmd, enumCommandType enCmddType, enumBTCommandType enBtCmdType)
        {
            if (OnKeysightCommand != null)
            {
                OnKeysightCommand(stageno, cmd, enCmddType, enBtCmdType);
            }
        }
        public delegate void StartCharging(STAGEINFO stageinfo);
        public event StartCharging OnStartCharging = null;
        protected void RaiseOnStartCharging(STAGEINFO stageinfo)
        {
            if (OnStartCharging != null)
            {
                OnStartCharging(stageinfo);
            }
        }
        public delegate void StopCharging(int stageno);
        public event StopCharging OnStopCharging = null;
        protected void RaiseOnStopCharging(int stageno)
        {
            if (OnStopCharging != null)
            {
                OnStopCharging(stageno);
            }
        }
        public delegate void RESET(int stageno);
        public event RESET OnRESET = null;
        protected void RaiseOnReset(int stageno)
        {
            if (OnRESET != null)
            {
                OnRESET(stageno);
            }
        }
        #endregion

        Util util = new Util();
        private int nStageno;
        private bool bVisible;
        public int STAGENO { get => nStageno; set => nStageno = value; }
        public bool VISIBLE { get => bVisible; set => bVisible = value; }


        private static ManualModeControl manualModeControl = new ManualModeControl();
        public static ManualModeControl GetInstance()
        {
            if (manualModeControl == null) manualModeControl = new ManualModeControl();
            return manualModeControl;
        }
        public ManualModeControl()
        {
            InitializeComponent();

            initGridView();
        }

        public void initGridView()
        {
            dgvSequence.RowsDefaultCellStyle.BackColor = Color.White;
            dgvSequence.AlternatingRowsDefaultCellStyle.BackColor = Color.White;
            dgvSequence.ClearSelection();
            dgvSequence.DefaultCellStyle.Font = new Font("Times New Roman", 10);
            dgvSequence.ColumnHeadersDefaultCellStyle.Font = new Font("Times New Roman", 10);
        }

        #region Control Event
        private void btnChargingStart_Click(object sender, EventArgs e)
        {
            int stageno = cbStageNo.SelectedIndex;
            string recipeno = cbRecipeNo.Text;
            bool bFset = chkFSET.Checked;

            STAGEINFO stageinfo = new STAGEINFO();
            stageinfo.stageno = stageno;
            stageinfo.recipeno = recipeno;
            stageinfo.fset = bFset;

            RaiseOnStartCharging(stageinfo);
        }
        private void btnChargingStop_Click(object sender, EventArgs e)
        {
            int stageno = cbStageNo.SelectedIndex;
            RaiseOnStopCharging(stageno);
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            int stageno = cbStageNo.SelectedIndex;
            RaiseOnReset(stageno);
        }
        private void btnSendCommand_Click(object sender, EventArgs e)
        {
            lbCommandList.Visible = false;

            int stageno = cbStageNo.SelectedIndex;
            string type = string.Empty;
            string cmd = tbCommand.Text;

            if (cmd.StartsWith(_Constant.TRB))
            {
                RaiseOnKeysightCommand(stageno, cmd, enumCommandType.TRB, enumBTCommandType.QUERY);
            }
            else if (cmd.StartsWith(_Constant.CONT))
            {
                RaiseOnKeysightCommand(stageno, cmd, enumCommandType.CONT, enumBTCommandType.QUERY);
            }
            else
                MessageBox.Show("명령어는 @01 이나 @02로 시작해야 합니다.");
        }
        private void btnAddStep_Click(object sender, EventArgs e)
        {
            string recipe_no = tbRecipeNo.Text;
            string recipe_method = cbStepMethod.Text;
            string time = tbTime.Text;
            string current = tbCurrent.Text;
            string voltage = tbVoltage.Text;

            AddRecipe(recipe_method, time, current, voltage);
        }
        private void btnUpdateStep_Click(object sender, EventArgs e)
        {
            string recipe_no = tbRecipeNo.Text;
            string recipe_method = cbStepMethod.Text;
            string time = tbTime.Text;
            string current = tbCurrent.Text;
            string voltage = tbVoltage.Text;

            UpdateRecipe(recipe_method, time, current, voltage);
        }

        private void btnDeleteStep_Click(object sender, EventArgs e)
        {
            int row = 0;
            try
            {
                if (dgvSequence.Rows.Count > 0)
                {
                    row = dgvSequence.SelectedRows[0].Index;
                    DeleteRecipe(row);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        private void btnReadRecipe_Click(object sender, EventArgs e)
        {
            ReadRecipe();
        }

        private void btnSaveRecipe_Click(object sender, EventArgs e)
        {
            SaveRecipeAsync();
        }
        private void cbStepMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            string stepname = cb.Text;
            if (stepname == "REST")
            {
                tbCurrent.Text = "10";
                tbVoltage.Text = "2000";
            }
            else if (stepname == "PRECHARGE")
            {
                tbCurrent.Text = "1000";
                tbVoltage.Text = "1000";
            }
            else if (stepname == "CHARGE")
            {
                tbCurrent.Text = "2000";
                tbVoltage.Text = "4000";
            }
            else if (stepname == "DISCHARGE")
            {
                tbCurrent.Text = "4000";
                tbVoltage.Text = "3000";
            }
        }
        private void dgvSequence_SelectionChanged(object sender, EventArgs e)
        {
            int curRow = -1;
            string stepmode = string.Empty;
            string time = string.Empty;
            string current = string.Empty;
            string voltage = string.Empty;

            DataGridView dgv = sender as DataGridView;
            if (dgv.CurrentRow == null) return;

            if (dgv.CurrentRow.Index != curRow)
            {
                curRow = dgv.CurrentRow.Index;
                if (dgv.Rows[curRow].Cells[1].Value == null) return;

                tbRecipeNo.Text = cbRecipeNo.Text;
                cbStepMethod.Text = dgv.Rows[curRow].Cells[1].Value.ToString();
                tbCurrent.Text = dgv.Rows[curRow].Cells[2].Value.ToString();
                tbVoltage.Text = dgv.Rows[curRow].Cells[3].Value.ToString();
                tbTime.Text = dgv.Rows[curRow].Cells[4].Value.ToString();
            }
        }
        private void lbCommandList_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListBox lb = sender as ListBox;
            tbCommand.Text = lb.SelectedItem.ToString();
            lb.Visible = false;
        }
        private void tbCommand_Click(object sender, EventArgs e)
        {
            lbCommandList.Visible = !lbCommandList.Visible;
        }
        private void cbStageNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            string stage_no = cb.Text;
            int iStage = cb.SelectedIndex;
            if (iStage >= 0) nStageno = iStage;
        }
        #endregion Control Event

        #region Method
        public void WriteLog(int stageno, string msg)
        {
            if (stageno == nStageno)
                util.SetValueToTextBox(tbLog, msg);
        }
        private void AddRecipe(string recipe_method, string time, string current, string voltage)
        {
            dgvSequence.Rows.Add();
            int row = dgvSequence.Rows.Count - 1;

            util.SetValueToGridView((row + 1).ToString(), row, 0, dgvSequence);
            util.SetValueToGridView(recipe_method, row, 1, dgvSequence);
            util.SetValueToGridView(current, row, 2, dgvSequence);
            util.SetValueToGridView(voltage, row, 3, dgvSequence);
            util.SetValueToGridView(time, row, 4, dgvSequence);
        }
        private void DeleteRecipe(int rowIndex)
        {
            dgvSequence.Rows.RemoveAt(rowIndex);
        }
        private void UpdateRecipe(string recipe_method, string time, string current, string voltage)
        {
            int selectedRow = dgvSequence.SelectedRows[0].Index;

            util.SetValueToGridView((selectedRow + 1).ToString(), selectedRow, 0, dgvSequence);
            util.SetValueToGridView(recipe_method, selectedRow, 1, dgvSequence);
            util.SetValueToGridView(current, selectedRow, 2, dgvSequence);
            util.SetValueToGridView(voltage, selectedRow, 3, dgvSequence);
            util.SetValueToGridView(time, selectedRow, 4, dgvSequence);
        }
        private async Task SaveRecipeAsync()
        {
            string recipe_no = cbRecipeNo.Text;
            int rowCount = dgvSequence.Rows.Count;
            string recipe_method = string.Empty;
            string time = string.Empty;
            string current = string.Empty;
            string voltage = string.Empty;

            int recipeno = util.TryParseInt(recipe_no, 0);
            if (recipeno < 0) return;

            //if (mariadb.DELETERECIPEDATA(recipe_no) == true)
            //{
            //    for (int nIndex = 0; nIndex < rowCount; nIndex++)
            //    {
            //        recipe_method = dgvSequence.Rows[nIndex].Cells[1].Value.ToString();
            //        current = dgvSequence.Rows[nIndex].Cells[2].Value.ToString();
            //        voltage = dgvSequence.Rows[nIndex].Cells[3].Value.ToString();
            //        time = dgvSequence.Rows[nIndex].Cells[4].Value.ToString();


            //        await mariadb.INSERTRECIPEAsync(recipeno, (nIndex + 1).ToString(), recipe_method, time, current, voltage);
            //    }
            //}

        }
        private void ReadRecipe()
        {
            //string recipe_no = cbRecipeNo.Text;
            //int recipeno = util.TryParseInt(recipe_no, 0);
            //if (recipeno < 0) return;

            //List<Recipe> recipes = mariadb.GETRECIPEDATAAsync(recipe_no);
            //util.DeleteRowsGridView(dgvSequence);
            //if (recipes.Count > 0)
            //{
            //    dgvSequence.Rows.Add(recipes.Count);
            //    for (int nIndex = 0; nIndex < recipes.Count; nIndex++)
            //    {
            //        util.SetValueToGridView(recipes[nIndex].orderno, nIndex, 0, dgvSequence);
            //        util.SetValueToGridView(recipes[nIndex].recipemethod, nIndex, 1, dgvSequence);
            //        util.SetValueToGridView(recipes[nIndex].current, nIndex, 2, dgvSequence);
            //        util.SetValueToGridView(recipes[nIndex].voltage, nIndex, 3, dgvSequence);
            //        util.SetValueToGridView(recipes[nIndex].time, nIndex, 4, dgvSequence);
            //    }
            //}
        }


        #endregion

        
    }
}
