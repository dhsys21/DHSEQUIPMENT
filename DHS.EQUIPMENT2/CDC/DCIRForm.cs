using DHS.EQUIPMENT2.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DHS.EQUIPMENT2
{
    public partial class DCIRForm : Form
    {
        #region Delegation
        public delegate void StartDCIR(STAGEINFO stageinfo);
        public event StartDCIR OnStartDCIR = null;
        protected void RaiseOnStartDCIR(STAGEINFO stageinfo)
        {
            if (OnStartDCIR != null)
            {
                OnStartDCIR(stageinfo);
            }
        }
        public delegate void StopDCIR(int stageno);
        public event StopDCIR OnStopDCIR = null;
        protected void RaiseOnStopDCIR(int stageno)
        {
            if (OnStopDCIR != null)
            {
                OnStopDCIR(stageno);
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
        private static DCIRForm dcirForm = new DCIRForm();
        public static DCIRForm GetInstance()
        {
            if (dcirForm == null) dcirForm = new DCIRForm();
            return dcirForm;
        }
        public DCIRForm()
        {
            InitializeComponent();

            ReadRecipe();
        }

        #region Control

        #region Recipe
        private void btnReadRecipe_Click(object sender, EventArgs e)
        {
            ReadRecipe();
        }
        private void lblMaxVoltLimit_Click(object sender, EventArgs e)
        {
            int nIndex = int.Parse((sender as Label).Tag.ToString());
            if (nIndex == 0)
            {
                tbEdit1.Text = lblMaxVoltLimit.Text;
                tbEdit1.Visible = true;
                tbEdit1.BringToFront();
            }
            else if (nIndex == 1)
            {
                tbEdit2.Text = lblMinVoltLimit.Text;
                tbEdit2.Visible = true;
                tbEdit2.BringToFront();
            }
            else if (nIndex == 2)
            {
                tbEdit3.Text = lblPauseBeforeFirstPulse.Text;
                tbEdit3.Visible = true;
                tbEdit3.BringToFront();
            }
            else if (nIndex == 3)
            {
                tbEdit4.Text = lblFirstPulseLevel.Text;
                tbEdit4.Visible = true;
                tbEdit4.BringToFront();
            }
            else if (nIndex == 4)
            {
                tbEdit5.Text = lblFirstPulseWidth.Text;
                tbEdit5.Visible = true;
                tbEdit5.BringToFront();
            }
            else if (nIndex == 5)
            {
                tbEdit6.Text = lblPauseAfterFirstPulse.Text;
                tbEdit6.Visible = true;
                tbEdit6.BringToFront();
            }
            else if (nIndex == 6)
            {
                tbEdit7.Text = lblSecondPulseLevel.Text;
                tbEdit7.Visible = true;
                tbEdit7.BringToFront();
            }
            else if (nIndex == 7)
            {
                tbEdit8.Text = lblSecondPulseWidth.Text;
                tbEdit8.Visible = true;
                tbEdit8.BringToFront();
            }
            else if (nIndex == 8)
            {
                tbEdit9.Text = lblPauseAfterSecondPulse.Text;
                tbEdit9.Visible = true;
                tbEdit9.BringToFront();
            }

        }
        private void tbEdit1_KeyPress(object sender, KeyPressEventArgs e)
        {
            int nIndex = int.Parse((sender as TextBox).Tag.ToString());
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (nIndex == 0)
                {
                    lblMaxVoltLimit.Text = tbEdit1.Text;
                    tbEdit1.Visible = false;
                }
                else if (nIndex == 1)
                {
                    lblMinVoltLimit.Text = tbEdit2.Text;
                    tbEdit2.Visible = false;
                }
                else if (nIndex == 2)
                {
                    lblPauseBeforeFirstPulse.Text = tbEdit3.Text;
                    tbEdit3.Visible = false;
                }
                else if (nIndex == 3)
                {
                    lblFirstPulseLevel.Text = tbEdit4.Text;
                    tbEdit4.Visible = false;
                }
                else if (nIndex == 4)
                {
                    lblFirstPulseWidth.Text = tbEdit5.Text;
                    tbEdit5.Visible = false;
                }
                else if (nIndex == 5)
                {
                    lblPauseAfterFirstPulse.Text = tbEdit6.Text;
                    tbEdit6.Visible = false;
                }
                else if (nIndex == 6)
                {
                    lblSecondPulseLevel.Text = tbEdit7.Text;
                    tbEdit7.Visible = false;
                }
                else if (nIndex == 7)
                {
                    lblSecondPulseWidth.Text = tbEdit8.Text;
                    tbEdit8.Visible = false;
                }
                else if (nIndex == 8)
                {
                    lblPauseAfterSecondPulse.Text = tbEdit9.Text;
                    tbEdit9.Visible = false;
                }

            }
        }
        private void dgvRecipe_SelectionChanged(object sender, EventArgs e)
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

                tbRecipeNo.Text = dgv.Rows[curRow].Cells[0].Value.ToString();
                lblMaxVoltLimit.Text = dgv.Rows[curRow].Cells[1].Value.ToString();
                lblMinVoltLimit.Text = dgv.Rows[curRow].Cells[2].Value.ToString();
                lblPauseBeforeFirstPulse.Text = dgv.Rows[curRow].Cells[3].Value.ToString();
                lblFirstPulseLevel.Text = dgv.Rows[curRow].Cells[4].Value.ToString();
                lblFirstPulseWidth.Text = dgv.Rows[curRow].Cells[5].Value.ToString();
                lblPauseAfterFirstPulse.Text = dgv.Rows[curRow].Cells[6].Value.ToString();
                lblSecondPulseLevel.Text = dgv.Rows[curRow].Cells[7].Value.ToString();
                lblSecondPulseWidth.Text = dgv.Rows[curRow].Cells[8].Value.ToString();
                lblPauseAfterSecondPulse.Text = dgv.Rows[curRow].Cells[9].Value.ToString();
            }
        }
        private void btnAddRecipe_Click(object sender, EventArgs e)
        {
            try
            {
                AddRecipe();

                /// 추가 된 recipe 다시 불러오기
                ReadRecipe();
            }
            catch(Exception ex) { Console.WriteLine(ex.Message); }
        }

        private void btnUpdateRecipe_Click(object sender, EventArgs e)
        {
            try
            {
                UpdateRecipe();

                /// 수정 된 recipe 다시 불러오기
                ReadRecipe();
            }
            catch(Exception ex) { Console.WriteLine(ex.Message); }
        }
        #endregion Recipe

        #region DCIR start/stop/reset
        private void btnDcirStart_Click(object sender, EventArgs e)
        {
            int stageno = cbStageNo.SelectedIndex;
            if (stageno < 0)
            {
                MessageBox.Show("No Stage");
                return;
            }

            string recipeno = tbRecipeNo.Text;
            if (recipeno == string.Empty)
            {
                MessageBox.Show("No Recipe");
                return;
            }

            STAGEINFO stageinfo = new STAGEINFO();
            stageinfo.stageno = stageno;
            stageinfo.recipeno = recipeno;

            RaiseOnStartDCIR(stageinfo);
        }

        private void btnDcirStop_Click(object sender, EventArgs e)
        {
            int stageno = cbStageNo.SelectedIndex;
            RaiseOnStopDCIR(stageno);
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            int stageno = cbStageNo.SelectedIndex;
            RaiseOnReset(stageno);
        }
        #endregion DCIR start/stop/reset
        
        #endregion Control

        #region Method
        private void AddRecipe()
        {
            RecipeDcir recipe = new RecipeDcir();
            recipe.MaxVoltLimit = lblMaxVoltLimit.Text;
            recipe.MinVoltLimit = lblMinVoltLimit.Text;
            recipe.PauseBeforeFirstPulse = lblPauseBeforeFirstPulse.Text;
            recipe.FirstPulseLevel = lblFirstPulseLevel.Text;
            recipe.FirstPulseWidth = lblFirstPulseWidth.Text;
            recipe.PauseAfterFirstPulse = lblPauseAfterFirstPulse.Text;
            recipe.SecondPulseLevel = lblSecondPulseLevel.Text;
            recipe.SecondPulseWidth = lblSecondPulseWidth.Text;
            recipe.PauseAfterSecondPulse = lblPauseAfterSecondPulse.Text;

            util.AddRecipeDcir(recipe);
        }
        private void UpdateRecipe()
        {
            RecipeDcir recipe = new RecipeDcir();
            recipe.RecipeNo = int.Parse(tbRecipeNo.Text);
            recipe.MaxVoltLimit = lblMaxVoltLimit.Text;
            recipe.MinVoltLimit = lblMinVoltLimit.Text;
            recipe.PauseBeforeFirstPulse = lblPauseBeforeFirstPulse.Text;
            recipe.FirstPulseLevel = lblFirstPulseLevel.Text;
            recipe.FirstPulseWidth = lblFirstPulseWidth.Text;
            recipe.PauseAfterFirstPulse = lblPauseAfterFirstPulse.Text;
            recipe.SecondPulseLevel = lblSecondPulseLevel.Text;
            recipe.SecondPulseWidth = lblSecondPulseWidth.Text;
            recipe.PauseAfterSecondPulse = lblPauseAfterSecondPulse.Text;

            util.UpdateRecipeDcir(recipe);
        }
        private RecipeDcir GetRecipe(string recipeno)
        {
            RecipeDcir recipe = new RecipeDcir();

            return recipe;
        }
        private void ReadRecipe()
        {
            util.DeleteRowsGridView(dgvRecipe);
            List<RecipeDcir> recipes = ReadRecipeDcir();
            if (recipes.Count > 0)
            {
                dgvRecipe.Rows.Add(recipes.Count);
                ShowRecipeToDGV(recipes);
            }
        }
        private List<RecipeDcir> ReadRecipeDcir()
        {
            List<RecipeDcir> recipes = new List<RecipeDcir>();

            recipes = util.ReadRecipeDcir();

            return recipes;
        }
        private void ShowRecipeToDGV(List<RecipeDcir> recipes)
        {
            int rowIndex = 0;
            foreach(RecipeDcir recipe in recipes)
            {
                util.SetValueToGridView((rowIndex + 1).ToString(), rowIndex, 0, dgvRecipe);
                util.SetValueToGridView(recipe.MaxVoltLimit, rowIndex, 1, dgvRecipe);
                util.SetValueToGridView(recipe.MinVoltLimit, rowIndex, 2, dgvRecipe);
                util.SetValueToGridView(recipe.PauseBeforeFirstPulse, rowIndex, 3, dgvRecipe);
                util.SetValueToGridView(recipe.FirstPulseLevel, rowIndex, 4, dgvRecipe);
                util.SetValueToGridView(recipe.FirstPulseWidth, rowIndex, 5, dgvRecipe);
                util.SetValueToGridView(recipe.PauseAfterFirstPulse, rowIndex, 6, dgvRecipe);
                util.SetValueToGridView(recipe.SecondPulseLevel, rowIndex, 7, dgvRecipe);
                util.SetValueToGridView(recipe.SecondPulseWidth, rowIndex, 8, dgvRecipe);
                util.SetValueToGridView(recipe.PauseAfterSecondPulse, rowIndex, 9, dgvRecipe);

                rowIndex++;
            }
        }

        #endregion

        private void btnDeleteRecipe_Click(object sender, EventArgs e)
        {

        }
    }
}
