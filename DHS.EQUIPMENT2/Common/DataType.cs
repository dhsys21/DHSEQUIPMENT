using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DHS.EQUIPMENT2.Common
{
    public class FILEINFO
    {
        public string NameOnly { get; set; }
        public string FullName { get; set; }
    }

    public class DataValue
    {
        public int Channel { get; set; }
        public int Time { get; set; }
        public int Status { get; set; }
        public double Current { get; set; }
        public double Voltage { get; set; }
        public double Capacity { get; set; }
    }
    public class DataValue2
    {
        private int[] channel = new int[240];
        private int[] time = new int[240];
        private double[] current = new double[240];
        private double[] voltage = new double[240];
        private double[] capacity = new double[240];

        public int[] Channel { get => channel; set => channel = value; }
        public int[] Time { get => time; set => time = value; }
        public double[] Current { get => current; set => current = value; }
        public double[] Voltage { get => voltage; set => voltage = value; }
        public double[] Capacity { get => capacity; set => capacity = value; }
    }
}
