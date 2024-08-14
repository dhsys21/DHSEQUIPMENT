using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DHS.EQUIPMENT2
{
    public partial class TITLE_CD : Form
    {
        public TITLE_CD()
        {
            InitializeComponent();

            //* Title 색상 하얀색으로 - picturebox에 있는 label은 배경색이 투명이 안되기 때문에 코딩으로 처리
            radlbl_Title.Parent = pboxTitle;
            radlbl_Title.ForeColor = Color.White;
        }
    }
}
