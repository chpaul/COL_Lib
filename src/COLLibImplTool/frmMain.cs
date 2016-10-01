using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using COL.MassLib;
using COL.GlycoLib;
using COL.ProtLib;
using Facet.Combinatorics;
using ZedGraph;
namespace COLLibImplTool
{
    public partial class frmMain : Form
    {
        DateTime STimer;
        IRawFileReader raw;
        MSScan scan;
        DataTable DTGlycan = new DataTable();
        public frmMain()
        {
            InitializeComponent();
            DTGlycan.Columns.Add(new DataColumn("HexNAc", System.Type.GetType("System.Int32")));
            DTGlycan.Columns.Add(new DataColumn("Hex", System.Type.GetType("System.Int32")));
            DTGlycan.Columns.Add(new DataColumn("deHex", System.Type.GetType("System.Int32")));
            DTGlycan.Columns.Add(new DataColumn("NeuAc", System.Type.GetType("System.Int32")));
            DTGlycan.Columns.Add(new DataColumn("Charge", System.Type.GetType("System.Int32")));
            DTGlycan.Columns.Add(new DataColumn("m/z", System.Type.GetType("System.Single")));
            DTGlycan.Columns.Add(new DataColumn("Permethylation", System.Type.GetType("System.Boolean")));
            DTGlycan.Columns.Add(new DataColumn("Reduced Reducing End", System.Type.GetType("System.Boolean")));
            DTGlycan.Columns.Add(new DataColumn("Adduct", System.Type.GetType("System.String")));
            DTGlycan.Columns.Add(new DataColumn("Label", System.Type.GetType("System.String")));
            DTGlycan.Columns.Add(new DataColumn("Composition", System.Type.GetType("System.String")));
            dgTable.DataSource = DTGlycan;
            dgTable.Columns[0].Width = 50;
            dgTable.Columns[1].Width = 45;
            dgTable.Columns[2].Width = 45;
            dgTable.Columns[3].Width = 45;
            dgTable.Columns[4].Width = 60;
            dgTable.Columns[5].Width = 70;
            dgTable.Columns[6].Width = 80;
            dgTable.Columns[7].Width = 60;
            dgTable.Columns[8].Width = 100;
            dgTable.Columns[9].Width = 80;
            dgTable.Columns[10].Width = 170;
        }
        

        private void btnLoadRawCSMSL_Click(object sender, EventArgs e)
        {

            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                STimer = DateTime.Now;
                if (Path.GetExtension(openFileDialog1.FileName).ToLower() == ".raw")
                {
                    raw = new ThermoRawReader(openFileDialog1.FileName);

                    lblRawInfo.Text = "Raw information\n" +
                                      "Last scan:" + raw.NumberOfScans.ToString() + "\n" +
                                      "Initial Time:" + DateTime.Now.Subtract(STimer).TotalSeconds.ToString("0.00") +
                                      " s";
                    btnBackward_CSMSL.Enabled = true;
                    btnForward_CSMSL.Enabled = true;
                    btnRead_CSMSL.Enabled = true;
                }
                else if (Path.GetExtension(openFileDialog1.FileName).ToLower() == ".mzxml")
                {
                    
                    raw = new mzXMLRawReader(openFileDialog1.FileName);
                    DateTime starTime = DateTime.Now;
                    for (int i = 1; i <= 1000; i++)
                    {
                        MSScan S1 = raw.ReadScan(i);
                    }
                    double Total = DateTime.Now.Subtract(starTime).TotalSeconds;
                }
                else
                {
                    MessageBox.Show("File not supported");
                }

            }
        }

        private void btnRead_CSMSL_Click(object sender, EventArgs e)
        {
            int ScanNo = 1;
            if (!int.TryParse(txtScanNo_CSMSL.Text, out ScanNo))
            {
                MessageBox.Show("Fill number in Scan number");
                return;
            }
            ReadandDisplayScan(ScanNo);
        }

        private void btnBackward_CSMSL_Click(object sender, EventArgs e)
        {
            int ScanNo = 1;
            if (!int.TryParse(txtScanNo_CSMSL.Text, out ScanNo))
            {
                MessageBox.Show("Fill number in Scan number");
                return;
            }
            ScanNo = ScanNo - 1;
            if (ScanNo <= 0)
            {
                ScanNo = 1;
            }
            txtScanNo_CSMSL.Text = ScanNo.ToString();
            ReadandDisplayScan(ScanNo);
        }

        private void btnForward_CSMSL_Click(object sender, EventArgs e)
        {
            int ScanNo = 1;
            if (!int.TryParse(txtScanNo_CSMSL.Text, out ScanNo))
            {
                MessageBox.Show("Fill number in Scan number");
                return;
            }
            ScanNo = ScanNo + 1;
            if (ScanNo  > raw.NumberOfScans)
            {
                ScanNo = raw.NumberOfScans;
            }
            txtScanNo_CSMSL.Text = ScanNo.ToString();
            ReadandDisplayScan(ScanNo);
        }
        private void ReadandDisplayScan(int argMSScan)
        {
    
            if (raw != null)
            {
                scan = raw.ReadScan(argMSScan);
                StringBuilder SB = new StringBuilder();
                SB.Append("Scan Num:" + scan.ScanNo + Environment.NewLine);
                SB.Append("Scan Time:" + scan.Time + Environment.NewLine);
                SB.Append("Scan header:" + scan.ScanHeader + Environment.NewLine);
                SB.Append("Scan range:" + scan.MinMZ + "~" + scan.MaxMZ + Environment.NewLine);
                SB.Append("Scan MS Level:" + scan.MsLevel + Environment.NewLine);
                SB.Append("Scan is FT Scan:" + scan.IsFTScan + Environment.NewLine);
                SB.Append("Scan is CID:" + scan.IsCIDScan + Environment.NewLine + Environment.NewLine);
                if (scan.MsLevel == 2)
                {
                    SB.Append("Parent Scan Num:" + scan.ParentScanNo + Environment.NewLine);
                    SB.Append("Precursor M/Z:" + scan.ParentMZ + Environment.NewLine);
                    SB.Append("Precursor Charge:" + scan.ParentCharge + Environment.NewLine);
                    SB.Append("Precursor mono mass:" + scan.ParentMonoMW + Environment.NewLine + Environment.NewLine);
                }
                if (scan.IsHCDScan)
                {
                    HCDInfo HCD = ((ThermoRawReader) raw).GetHCDInfo(argMSScan);
                    SB.Append("HCD Score:" + HCD.HCDScore.ToString("0.00")+Environment.NewLine);
                    SB.Append("HCD Glycan Type:" + HCD.GlycanType + Environment.NewLine);
                }
                SB.Append("MS Peaks:" + scan.MSPeaks.Count + Environment.NewLine);
                SB.Append("DeisotopeMZ\tPeak Intensity\tMost intense Mz\tMost intense int\t Cluster int"+Environment.NewLine);
                foreach (MSPeak peak in scan.MSPeaks)
                {
                    SB.Append(peak.DeisotopeMz + "\t\t" + peak.MonoIntensity + "\t\t" + peak.MostIntenseMass + "\t\t" + peak.MostIntenseIntensity + "\t\t" + peak.ClusterIntensity + Environment.NewLine);
                }

                SB.Append(Environment.NewLine + Environment.NewLine + "MZs:" + scan.MZs.Length + Environment.NewLine);
                for (int i = 0; i < scan.MZs.Length; i++)
                {
                    SB.Append("[" + i.ToString() + "]" + scan.MZs[i].ToString("0.0000") + "\t" + scan.Intensities[i].ToString("0.0000") + Environment.NewLine);
                }
                txtPeaks_CSMSL.Text = SB.ToString();
                ZedGraphDisplay(scan);
            }
        }
        private void ZedGraphDisplay(MSScan argScan)
        {
            GraphPane GP = zedGraphControl1.GraphPane;
            GP.XAxis.Title.Text = "m/z";
            GP.YAxis.Title.Text = "Intensity";
            PointPairList pplRaw = new PointPairList();
            for (int i = 0; i < argScan.MZs.Length; i++)
            {
                pplRaw.Add(argScan.MZs[i], argScan.Intensities[i]);
            }
            PointPairList pplPeak = new PointPairList();
            for (int i = 0; i < argScan.MSPeaks.Count; i++)
            {
                pplPeak.Add(argScan.MSPeaks[i].MonoisotopicMZ,argScan.MSPeaks[i].MonoIntensity);
            }
            GP.AddBar("Raw", pplRaw, Color.Black);
            GP.AddBar("Peak", pplPeak, Color.Blue);
            zedGraphControl1.AxisChange();
        }
        private void btnGetPeaks_Click(object sender, EventArgs e)
        {
            if (raw != null)
            {
                int ScanNo = 1;
                if (!int.TryParse(txtScanNo_CSMSL.Text, out ScanNo))
                {
                    MessageBox.Show("Fill number in Scan number");
                    return;
                }
                MSScan scan = raw.ReadScan(ScanNo);
                int ClosedIdx = MassUtility.GetClosestMassIdx(scan.MZs, Convert.ToSingle(txtTargetMZ.Text));
                List<int> Peaks = FindPeakIdx(scan.MZs, ClosedIdx, Convert.ToInt32(txtCharge.Text), Convert.ToSingle(txtPPM_CSMSL.Text));
                string optStr = "";
                for (int i = 0; i < Peaks.Count; i++)
                {
                    optStr = optStr + "[" + Peaks[i] + "]" + scan.MZs[Peaks[i]] + "\t   " + scan.Intensities[Peaks[i]] + Environment.NewLine;
                }
                txtPeak.Text = optStr;
            }
        }
        private List<int> FindPeakIdx(float[] argMZAry, int argTargetIdx, int argCharge, float argPPM)
        {
            List<int> Peak = new List<int>();
            float Interval = 1 / (float)argCharge;
            float FirstMZ = argMZAry[argTargetIdx];
            int CurrentIdx = argTargetIdx;
            Peak.Add(argTargetIdx);
            //Forward  Peak
            for (int i = argTargetIdx - 1; i >= 0; i--)
            {
                if (argMZAry[argTargetIdx] - argMZAry[i] >= Interval * 10)
                {
                    break;
                }
                List<int> ClosedPeaks = MassUtility.GetClosestMassIdxsWithinPPM(argMZAry, argMZAry[CurrentIdx] - Interval, argPPM);
                if (ClosedPeaks.Count == 1)
                {
                    CurrentIdx = ClosedPeaks[0];
                    Peak.Insert(0, ClosedPeaks[0]);
                }
                else if (ClosedPeaks.Count > 1)
                {
                    double minPPM = 100;
                    int minPPMIdx = 0;
                    for (int j = 0; j < ClosedPeaks.Count; j++)
                    {
                        if (MassUtility.GetMassPPM(argMZAry[ClosedPeaks[j]], argMZAry[CurrentIdx] - Interval) < minPPM)
                        {
                            minPPMIdx = ClosedPeaks[j];
                            minPPM = MassUtility.GetMassPPM(argMZAry[ClosedPeaks[j]], argMZAry[CurrentIdx] + Interval);
                        }
                    }
                    CurrentIdx = minPPMIdx;
                    Peak.Insert(0, CurrentIdx);
                }
            }
            //Backward  Peak
            CurrentIdx = argTargetIdx;
            for (int i = argTargetIdx + 1; i < argMZAry.Length; i++)
            {
                if (argMZAry[i] - argMZAry[argTargetIdx] >= Interval * 10)
                {
                    break;
                }
                List<int> ClosedPeaks = MassUtility.GetClosestMassIdxsWithinPPM(argMZAry, argMZAry[CurrentIdx] + Interval, argPPM);
                if (ClosedPeaks.Count == 1)
                {
                    CurrentIdx = ClosedPeaks[0];
                    Peak.Add(ClosedPeaks[0]);
                }
                else if (ClosedPeaks.Count > 1)
                {
                    double minPPM = 100;
                    int minPPMIdx = 0;
                    for (int j = 0; j < ClosedPeaks.Count; j++)
                    {
                        if (MassUtility.GetMassPPM(argMZAry[ClosedPeaks[j]], argMZAry[CurrentIdx] + Interval) < minPPM)
                        {
                            minPPMIdx = ClosedPeaks[j];
                            minPPM = MassUtility.GetMassPPM(argMZAry[ClosedPeaks[j]], argMZAry[CurrentIdx] + Interval);
                        }
                    }
                    CurrentIdx = minPPMIdx;
                    Peak.Add(CurrentIdx);
                }
            }

            return Peak;
        }
        
        
        private void btnGlycan_Click(object sender, EventArgs e)
        {
            float userAdduct =0;
            if (chkAdduct_user.Checked && !Single.TryParse(txtAdduct_user.Text,out userAdduct)) 
            {
                MessageBox.Show("User Adduct mass error");
                return;
            }
            DTGlycan.Clear();
            List<GlycanCompound> Glycans= new List<GlycanCompound>();
            List<enumLabelingTag> LabelTags = new List<enumLabelingTag>();
            List<string> Adducts = new List<string>();
            List<float> AdductsMass = new List<float>();
            Adducts.Add("H");
            AdductsMass.Add(Atoms.ProtonMass);
            if (chkAdduct_K.Checked)
            {
                Adducts.Add("K");
                AdductsMass.Add(Atoms.Potassium);
            }
            if (chkAdduct_Na.Checked)
            {
                Adducts.Add("Na");
                AdductsMass.Add(Atoms.SodiumMass);
            }
            if (chkAdduct_NH4.Checked)
            {
                Adducts.Add("NH4");
                AdductsMass.Add(Atoms.NitrogenMass  + Atoms.HydrogenMass *3 + Atoms.ProtonMass);
            }
            if (chkAdduct_user.Checked)
            {
                Adducts.Add("User");
                AdductsMass.Add(Convert.ToSingle(txtAdduct_user.Text));
            }
            enumGlycanLabelingMethod LabelMethod;
            if (rdoDRAG.Checked)
            {
                LabelMethod = enumGlycanLabelingMethod.DRAG;
                LabelTags.Add(enumLabelingTag.DRAG_Light);
                LabelTags.Add(enumLabelingTag.DRAG_Heavy);                
            }
            else if (rdoMultiplexPermethylation.Checked)
            {
                LabelMethod = enumGlycanLabelingMethod.MultiplexPermethylated;
                LabelTags.Add(enumLabelingTag.MP_13CD3);
                LabelTags.Add(enumLabelingTag.MP_13CH3);
                LabelTags.Add(enumLabelingTag.MP_13CHD2);
                LabelTags.Add(enumLabelingTag.MP_CH3);
                LabelTags.Add(enumLabelingTag.MP_CH2D);                
                LabelTags.Add(enumLabelingTag.MP_CHD2);
                LabelTags.Add(enumLabelingTag.MP_CD3); 
            }
            else if(rdoHDEAT.Checked)
            {
                LabelMethod = enumGlycanLabelingMethod.HDEAT;
                LabelTags.Add(enumLabelingTag.HDEAT_Light);
                LabelTags.Add(enumLabelingTag.HDEAT_Heavy);
            }
            else
            {
                LabelMethod = enumGlycanLabelingMethod.None;
                LabelTags.Add(enumLabelingTag.None);
            }
            List<GlycanCompound> tmps =new List<GlycanCompound>();
            if (chkLoadFile.Checked && openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                tmps = COL.GlycoLib.ReadGlycanListFromFile.ReadGlycanList(openFileDialog1.FileName,
                                                                                                                                Convert.ToBoolean(chkPermethylation.Checked),
                                                                                                                                 true,
                                                                                                                                 Convert.ToBoolean(chkReducedReducingEnd.Checked));

            }
            else
            {
                tmps.Add(new GlycanCompound(Convert.ToInt32(txtHexNAc.Text),
                                                     Convert.ToInt32(txtHex.Text),
                                                     Convert.ToInt32(txtFuc.Text),
                                                     Convert.ToInt32(txtSia.Text)));
                
            }
            foreach (GlycanCompound gCompound in tmps)
            {
                foreach (enumLabelingTag Tag in LabelTags)
                {
                    for (int i = 1; i <= 5; i++) //charge
                    {
                        foreach (List<Tuple<int, int>> combin in GetCombinations(Adducts.Count, i))
                        {
                            List<Tuple<string, float, int>> LstAdducts = new List<Tuple<string, float, int>>();
                            for (int j = 0; j < combin.Count; j++)
                            {
                                LstAdducts.Add(new Tuple<string, float, int>(Adducts[combin[j].Item1], AdductsMass[combin[j].Item1], combin[j].Item2));
                            }
                            Glycans.Add(new GlycanCompound(gCompound.NoOfHexNAc,
                                                         gCompound.NoOfHex,
                                                         gCompound.NoOfDeHex,
                                                         gCompound.NoOfSia,
                                                         Convert.ToBoolean(chkPermethylation.Checked),
                                                         false,
                                                         Convert.ToBoolean(chkReducedReducingEnd.Checked),
                                                         false,
                                                         true,
                                                         LstAdducts,
                                                         Tag));
                            
                            Glycans[Glycans.Count - 1].PositiveCharge = chkChargeMode.Checked;
                            double a = Glycans[Glycans.Count - 1].MZ;
                            double a1 = Glycans[Glycans.Count - 1].MZ;
                            double a2 = Glycans[Glycans.Count - 1].MZ;
                            double a3 = Glycans[Glycans.Count - 1].MZ;
                            double a4 = Glycans[Glycans.Count - 1].MZ;
                            double a5 = Glycans[Glycans.Count - 1].MZ;
                            double a6 = Glycans[Glycans.Count - 1].MZ;
                            if(a!=a1)
                            {

                            }
                        }
                    }
                }
            }

            //Display
            foreach (GlycanCompound GlyComp in Glycans)
            {
                                DataRow row = DTGlycan.NewRow();
                                row[0] = GlyComp.NoOfHexNAc;
                                row[1] = GlyComp.NoOfHex;
                                row[2] = GlyComp.NoOfDeHex;
                                row[3] = GlyComp.NoOfSia;
                                row[4] = GlyComp.Charge;
                                row[6] = GlyComp.isPermethylated;
                                row[7] = GlyComp.isReducedReducingEnd;
                                foreach (Tuple<string, float, int> adduct in GlyComp.Adducts)
                                {
                                    row[8] = row[8] + adduct.Item1 + " * " + adduct.Item3;
                                }
                                row[9] = GlyComp.LabelingTag;


                                row[5] = GlyComp.MZ;
                                row[10] = "C" + GlyComp.Carbon + "H" + GlyComp.Hydrogen + "O" + GlyComp.Oxygen;
                                if (GlyComp.Carbon13 != 0)
                                {
                                    row[10] = row[10] + "[13C]" + GlyComp.Carbon13;
                                }
                                if (GlyComp.Deuterium != 0)
                                {
                                    row[10] = row[10] + "D" + GlyComp.Deuterium;
                                }
                                DTGlycan.Rows.Add(row);
            }
            
        }
        private IEnumerable<List<Tuple<int, int>>> GetCombinations(int argAdductNumber, int argMaxCharge)
        {
            //Generate Adduct Combination
            int TotalAdductNumber = argAdductNumber;
            char[] inputset = new char[TotalAdductNumber];
            for (int i = 0; i < TotalAdductNumber; i++)
            {
                inputset[i] = Convert.ToChar(i + 48);
            }
            Combinations<char> combinations = new Combinations<char>(inputset, argMaxCharge, GenerateOption.WithRepetition);
            Dictionary<char, int> DictAdductCount = new Dictionary<char, int>();
            foreach (IList<char> c in combinations)
            {
                DictAdductCount.Clear();
                for (int i = 0; i < c.Count; i++)
                {
                    if (!DictAdductCount.ContainsKey(c[i]))
                    {
                        DictAdductCount[c[i]] = 0;
                    }
                    DictAdductCount[c[i]] = DictAdductCount[c[i]] + 1;
                }
                List<Tuple<int, int>> ReturnValues = new List<Tuple<int,int>>();
                for (int j = 0; j < TotalAdductNumber; j++)
                {
                    if (DictAdductCount.ContainsKey(Convert.ToChar(j + 48)))
                    {
                        ReturnValues.Add( new Tuple<int, int>(j, DictAdductCount[Convert.ToChar(j + 48)]));
                    }
                }
                yield return ReturnValues;
            }
        }

        private void btnGetPeptideMass_Click(object sender, EventArgs e)
        {
            AminoAcidMass AAMS = new AminoAcidMass();
            lblPeptideMass.Text = "       Mass:" + AAMS.GetMonoMW(txtPeptideSeq.Text, chkCYS_CAM.Checked) + "\nAvg Mass:" +
                                  AAMS.GetAVGMonoMW(txtPeptideSeq.Text, chkCYS_CAM.Checked);

        }

        private void btnReadFasta_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                List<Protease.Type> ProteaseType = new List<Protease.Type>();

                if (chkEnzy_Trypsin.Checked)//Trypsin
                {
                    ProteaseType.Add(Protease.Type.Trypsin);
                }
                if (chkEnzy_GlucE.Checked)//GlucE
                {
                    ProteaseType.Add(Protease.Type.GlucE);
                }
                if (chkEnzy_GlucED.Checked) //GlucED
                {
                    ProteaseType.Add(Protease.Type.GlucED);
                }
                if (chkEnzy_None.Checked || ProteaseType.Count == 0)
                {
                    ProteaseType.Add(Protease.Type.None);
                }

                enumPeptideMutation pepMuta = enumPeptideMutation.NoMutation;
                if (cboPepMutation.SelectedIndex == 1)
                {
                    pepMuta = enumPeptideMutation.DtoN;
                }
                else if (cboPepMutation.SelectedIndex == 2)
                {
                    pepMuta = enumPeptideMutation.AnyToN;
                }


                string fastaFile = openFileDialog1.FileName;
                FastaFile file = new FastaFile(fastaFile);
                List<string> Peptides = file.CreateCleavedPeptides(Convert.ToInt32(txtMissCleavage.Text), ProteaseType);
                txtPeptides.Text = "Digested Peptides" + Environment.NewLine + Environment.NewLine;
                foreach (string p in Peptides)
                {
                    txtPeptides.Text += p + Environment.NewLine;

                }
                txtPeptides.Text += Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine;
                txtPeptides.Text += "N-linked Peptides" + Environment.NewLine + Environment.NewLine;
                List<string> lstPeptides = file.CreateCleavedNGlycoPeptides(Convert.ToInt32(txtMissCleavage.Text), ProteaseType, pepMuta);
                foreach (string p in lstPeptides)
                {
                    txtPeptides.Text += p + Environment.NewLine;

                }
            }
        }

        private void chkEnzy_None_CheckedChanged(object sender, EventArgs e)
        {
            if (chkEnzy_None.Checked)
            {
                chkEnzy_GlucE.Checked = false;
                chkEnzy_GlucED.Checked = false;
                chkEnzy_Trypsin.Checked = false;
            }
        }

        private void chkEnzy_GlucE_CheckedChanged(object sender, EventArgs e)
        {
            if (chkEnzy_GlucE.Checked)
            {
                chkEnzy_None.Checked = false;
                chkEnzy_GlucED.Checked = false;
            }
        }

        private void chkEnzy_GlucED_CheckedChanged(object sender, EventArgs e)
        {
            if (chkEnzy_GlucED.Checked)
            {
                chkEnzy_None.Checked = false;
                chkEnzy_GlucE.Checked = false;
            }
        }

        private void chkEnzy_Trypsin_CheckedChanged(object sender, EventArgs e)
        {
            if (chkEnzy_Trypsin.Checked)
            {
                chkEnzy_None.Checked = false;
            }
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void cboPepMutation_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnGlycanDraw_Click(object sender, EventArgs e)
        {
            GlycansDrawer draw = new GlycansDrawer();
            picGlycan.Image = draw.GlycanCartoon(Glycan.Type.DeHex);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            ThermoRawReader TRaw = (ThermoRawReader)raw;
            string[] TagArray = TRaw.GetTrailerExtraLabelArray(1);
            //Title
            int msCount = 0;
            int msmsCount = 0;
            int CIDcount = 0;
            int HCDcount = 0;
            for (int i = 1; i <= raw.NumberOfScans; i++)
            {
             
                if (TRaw.GetMsLevel(i) == 1)
                {
                    msCount += 1;
                }
                else
                {
                    msmsCount += 1;
                    if (TRaw.IsCIDScan(i))
                    {
                        CIDcount += 1;
                    }
                    else
                    {
                        HCDcount += 1;
                    }
                }
            }
        }


    }
}
