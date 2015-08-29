using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
namespace TestApp
{
    public partial class frmE3D : Form
    {
        public frmE3D()
        {
            InitializeComponent();
        }
        Dictionary<string, Dictionary<float, List<string>>> dictValue;
        Dictionary<float, List<string>> mz2GlycanAdductCharge; // Key: m/z  Value: Glycan_Adduct_Charge
        Dictionary<string, List<string>> MergeResult;
        private void btnLoad_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (!File.Exists(openFileDialog1.FileName.Replace("_FullList", "")))
                {
                    MessageBox.Show("Merge Result File Missing, Please put merge and full reuslt in the same folder");
                    return;
                }
                dictValue = new Dictionary<string, Dictionary<float, List<string>>>();
                mz2GlycanAdductCharge = new Dictionary<float, List<string>>();
                cboGlycan.Items.Clear();

                ArrayList alstGlycans = new ArrayList();


                //Read Merge Result
                MergeResult = new Dictionary<string, List<string>>();
                StreamReader sr = new StreamReader(openFileDialog1.FileName.Replace("_FullList", ""));
                string[] tmp = null;
                Dictionary<string, int> dictTitle = new Dictionary<string, int>();
                do
                {
                    tmp = sr.ReadLine().Split(',');
                    if (!tmp[0].StartsWith("Start Time"))
                    {
                        continue;
                    }
                    for (int i = 0; i < tmp.Length; i++)
                    {
                        dictTitle.Add(tmp[i], i);
                    }
                    break;
                } while (true);
                do
                {
                    tmp = sr.ReadLine().Split(',');
                    string Key = tmp[dictTitle["HexNac-Hex-deHex-Sia"]]; // hex-hexnac-dehax-sia
                    string Time = tmp[dictTitle["Start Time"]] + ":" + tmp[dictTitle["End Time"]] + ":" + tmp[dictTitle["Peak Intensity"]];
                    if (!MergeResult.ContainsKey(Key))
                    {
                        MergeResult.Add(Key, new List<string>());
                    }
                    MergeResult[Key].Add(Time);

                } while (!sr.EndOfStream);
                sr.Close();



                //Read Full File
                sr = new StreamReader(openFileDialog1.FileName);
                tmp = sr.ReadLine().Split(',');
                dictTitle = new Dictionary<string, int>();
                //Get Title mapping
                for (int i = 0; i < tmp.Length; i++)
                {
                    dictTitle.Add(tmp[i], i);
                }

                do
                {
                    tmp = sr.ReadLine().Split(',');
                    int Charge = Convert.ToInt32(Math.Round(Convert.ToSingle(tmp[dictTitle["Composition mono"]]) / Convert.ToSingle(tmp[dictTitle["m/z"]]), 0));
                    string Key = tmp[dictTitle["HexNac-Hex-deHex-Sia"]] + "-" + Charge.ToString(); // hex-hexnac-dehax-sia
                    string Adduct = tmp[dictTitle["Adduct"]];
                    if (!dictValue.ContainsKey(Key))
                    {
                        dictValue.Add(Key, new Dictionary<float, List<string>>());
                        alstGlycans.Add(Key);
                    }
                    int mz = (int)Convert.ToSingle(tmp[dictTitle["m/z"]].ToString());

                    if (!dictValue[Key].ContainsKey(mz))
                    {
                        dictValue[Key].Add(mz, new List<string>());
                    }
                    dictValue[Key][mz].Add(tmp[dictTitle["Time"]] + "-" + tmp[dictTitle["Abundance"]]); // scan time - abuntance
                    if (!mz2GlycanAdductCharge.ContainsKey(mz))
                    {
                        mz2GlycanAdductCharge.Add(mz, new List<string>());
                    }
                    if (!mz2GlycanAdductCharge[mz].Contains(Key + " + " + Adduct + " z=" + Charge.ToString()))
                    {
                        mz2GlycanAdductCharge[mz].Add(Key + " + " + Adduct + " z=" + Charge.ToString()); //Glycan_Adduct_Charge
                    }

                } while (!sr.EndOfStream);
                sr.Close();
                alstGlycans.Sort();
                cboGlycan.Items.AddRange(alstGlycans.ToArray());

            }
        }

        private void cboGlycan_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboGlycan.SelectedItem == null)
            {
                return;
            }
            string key = cboGlycan.SelectedItem.ToString();
            Dictionary<float, List<string>> keyValue = dictValue[key];

            string[] tmpLst = null;  

            double MaxIntensity = 0.0;

            List<int> GlycanMZ = new List<int>();
            COL.ElutionViewer.MSPointSet3D Eluction3DRaw = new COL.ElutionViewer.MSPointSet3D();
            foreach (float mz in keyValue.Keys)
            {
               
                foreach (string tmp in keyValue[mz])
                {
                    tmpLst = tmp.Split('-');
                    float time = Convert.ToSingle(tmpLst[0]);
                    float intensity = Convert.ToSingle(tmpLst[1]);

                    Eluction3DRaw.Add(time, mz, intensity);
                }

            }
            eluctionViewer1.SetData(COL.ElutionViewer.EViewerHandler.Create3DHandler(Eluction3DRaw));
            //eluctionViewer1.Smooth();
        }

        private void chkCalcAbuandance_CheckedChanged(object sender, EventArgs e)
        {
            eluctionViewer1.UseMousrGetAbundance = chkCalcAbuandance.Checked;
        }
    }
}
