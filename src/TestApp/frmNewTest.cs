using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TestApp
{
    public partial class frmNewTest : Form
    {
        public frmNewTest()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            COL.MassLib.ThermoRawReader raw = new COL.MassLib.ThermoRawReader(@"C:\Users\Paul\Desktop\MultiGlycan_HD\doc\TestData\D1_06262014.raw");
            var x = raw.NumberOfScans;
            COL.MassLib.MSScan scan1 = raw.ReadScan(846);
            //COL.MassLib.MSScan scan2 = raw.ReadScan(415);
            //GlycanCompound CP1 = new GlycanCompound(5,6,0 ,2,false,false,false,false,true);
            //CP1.isHuman = true;
            
            //CP1.LabelingTag = enumLabelingTag.DRAG_Light;

            //double a = COL.MassLib.MassUtility.GetMassPPM(CP1.MonoMass,2748.063);
           

       
        }
    }
}
