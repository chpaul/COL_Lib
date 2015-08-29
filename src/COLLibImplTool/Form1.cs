using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using COL.MassLib;
using ZedGraph;
using System.IO;
using COL.ProtLib;
namespace COLLibImplTool
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //StreamReader SR = new StreamReader(@"D:\Data\Jacobson Tang Meeting\bkdsub_smooth_centroid RNASE5.txt");
            //List<double> mzs = new List<double>();
            //List<double> intensities = new List<double>();
    //do
            //{
            //    string tmp = SR.ReadLine();
            //    mzs.Add(Convert.ToDouble(tmp.Split('\t')[0]));
            //    intensities.Add(Convert.ToDouble(tmp.Split('\t')[1]));
            //} while (!SR.EndOfStream);
            //SR.Close();
            //GraphPane gp = zedGraphControl1.GraphPane;
            //PointPairList ppl = new PointPairList(mzs.ToArray(), intensities.ToArray());
            //gp.AddBar("X-Y", ppl, Color.Black);
            //gp.AxisChange();
            //zedGraphControl1.Refresh();
            //List<Protease.Type> proteasList = new List<Protease.Type>();
            //proteasList.Add(Protease.Type.Trypsin);
            //List<ProteinInfo> PInfos = FastaReader.ReadFasta(@"E:\Dropbox\@Paper\MyPaper\GlycanSeq_AC\Data\H1N1\H1N1.fasta");

            //List<string> Peptides = PInfos[0].NGlycopeptide(0, proteasList, enumPeptideMutation.NoMutation);
            //List<string> MutaPeptides = PInfos[0].NGlycopeptide(0, proteasList,enumPeptideMutation.DtoN);
            //Console.WriteLine(Peptides.Count);
            //Console.WriteLine(MutaPeptides.Count);
            COL.GlycoLib.ReadGlycanListFromFile.ReadGlycanList(@"D:\!Git\MultiGlycan\lib\Default_Combination.csv", true,
                false, false, true, true);
            COL.MassLib.ThermoRawReader Raw = new ThermoRawReader(@"E:\Dropbox\@Paper\MyPaper\GlycanSeq_AC\Data\H1N1\P-Glu-C_IU_090710_HCD.RAW");
            List<MSScan> Scan = Raw.ReadScanWMSLevel(13107, 13127, 1);
            Console.WriteLine(Raw.NumberOfScans.ToString());
            List<MSScan> MS1 = Raw.ReadScanWMSLevel(1,Raw.NumberOfScans,1);
            List<MSScan> MS2 = Raw.ReadScanWMSLevel(1, Raw.NumberOfScans, 2);
            int CIDCount = 0;
            int HCDCount = 0;
            foreach (var m in MS2)
            {
                if (m.IsCIDScan)
                {
                    CIDCount++;
                }
                if (m.IsHCDScan)
                {
                    HCDCount++;
                }
            }
            Console.WriteLine(CIDCount.ToString());
        }
    }
}
