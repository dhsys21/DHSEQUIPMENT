using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DHS.EQUIPMENT2.CDC
{
    public partial class EquipmentStatusControl : UserControl
    {
        private static EquipmentStatusControl equipmentStatusControl = new EquipmentStatusControl();
        public static EquipmentStatusControl GetInstance()
        {
            if (equipmentStatusControl == null) equipmentStatusControl = new EquipmentStatusControl();
            return equipmentStatusControl;
        }
        public EquipmentStatusControl()
        {
            InitializeComponent();
        }
    }
}
