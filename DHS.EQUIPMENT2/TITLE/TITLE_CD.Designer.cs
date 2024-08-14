
namespace DHS.EQUIPMENT2
{
    partial class TITLE_CD
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pboxTitle = new System.Windows.Forms.PictureBox();
            this.radlbl_Title = new Telerik.WinControls.UI.RadLabel();
            ((System.ComponentModel.ISupportInitialize)(this.pboxTitle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radlbl_Title)).BeginInit();
            this.SuspendLayout();
            // 
            // pboxTitle
            // 
            this.pboxTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pboxTitle.Image = global::DHS.EQUIPMENT2.Properties.Resources.PRECHARGER_Background;
            this.pboxTitle.Location = new System.Drawing.Point(0, 0);
            this.pboxTitle.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pboxTitle.Name = "pboxTitle";
            this.pboxTitle.Size = new System.Drawing.Size(1884, 101);
            this.pboxTitle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pboxTitle.TabIndex = 5;
            this.pboxTitle.TabStop = false;
            // 
            // radlbl_Title
            // 
            this.radlbl_Title.BackColor = System.Drawing.Color.Transparent;
            this.radlbl_Title.Font = new System.Drawing.Font("Verdana", 32.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radlbl_Title.Location = new System.Drawing.Point(30, 18);
            this.radlbl_Title.Name = "radlbl_Title";
            this.radlbl_Title.Size = new System.Drawing.Size(698, 60);
            this.radlbl_Title.TabIndex = 6;
            this.radlbl_Title.Text = "CHARGING/ DISCHARGING";
            // 
            // TITLE_CD
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1884, 101);
            this.Controls.Add(this.radlbl_Title);
            this.Controls.Add(this.pboxTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "TITLE_CD";
            this.Text = "TITLE_CD";
            ((System.ComponentModel.ISupportInitialize)(this.pboxTitle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radlbl_Title)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pboxTitle;
        private Telerik.WinControls.UI.RadLabel radlbl_Title;
    }
}