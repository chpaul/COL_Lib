using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using COL.MassLib;
namespace COLLibImplTool
{
    public partial class frmMain : Form
    {
        IRawFileReader rawReader;
        public frmMain()
        {
            InitializeComponent();
        }

        private void btnLoadRaw_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                
                if (Path.GetExtension(openFileDialog1.FileName.ToLower()) == ".raw")
                {
                    rawReader = new RawReader(openFileDialog1.FileName, enumRawDataType.raw);
                }
                else
                {
                    rawReader = new RawReader(openFileDialog1.FileName, enumRawDataType.mzxml);
                }
                btnRead.Enabled = true;
                btnBackward.Enabled = true;
                btnForward.Enabled = true;
                lblScanRange.Text = "1~" + rawReader.NumberOfScans.ToString();
                txtScanNo.Text = "1";
            }
        }

        private void trackSN_Scroll(object sender, EventArgs e)
        {
            txtSN.Text = trackSN.Value.ToString();
        }

        private void trackPeakBackgroundRatio_Scroll(object sender, EventArgs e)
        {
            txtPeakPeakBackgroundRatioRatio.Text = (trackPeakBackgroundRatio.Value / 100.0).ToString();
        }

        private void trackPeptideMinRatio_Scroll(object sender, EventArgs e)
        {
            txtPeptideMinRatio.Text = (trackPeptideMinRatio.Value / 100.0).ToString();
        }

        private void trackMaxCharge_Scroll(object sender, EventArgs e)
        {
            txtMaxCharge.Text = (11 - trackMaxCharge.Value).ToString();
        }

        private void chkPeptideMinAbso_CheckedChanged(object sender, EventArgs e)
        {
            txtPeptideMinAbso.Enabled = chkPeptideMinAbso.Checked;
            trackPeptideMinRatio.Enabled = !chkPeptideMinAbso.Checked;
            txtPeptideMinRatio.Enabled = !chkPeptideMinAbso.Checked;
        }

        private void txtSN_TextChanged(object sender, EventArgs e)
        {
            int ParsedValue = 0;
            if (Int32.TryParse(txtSN.Text, out ParsedValue))
            {
                if (ParsedValue > trackSN.Maximum)
                {
                    trackSN.Value = trackSN.Maximum;
                    txtSN.Text = trackSN.Value.ToString();
                }
                else if (ParsedValue < trackSN.Minimum)
                {
                    trackSN.Value = trackSN.Minimum;
                    txtSN.Text = trackSN.Value.ToString();
                }
                else
                {
                    trackSN.Value = Convert.ToInt32(txtSN.Text);
                }
            }
            else
            {
                MessageBox.Show("Please enter value: " + trackSN.Minimum.ToString() + " ~ " + trackSN.Maximum.ToString());
                txtSN.Text = trackSN.Value.ToString();
            }
        }

        private void txtPeakPeakBackgroundRatioRatio_TextChanged(object sender, EventArgs e)
        {
            float ParsedValue = 0;
            if (float.TryParse(txtPeakPeakBackgroundRatioRatio.Text, out ParsedValue))
            {
                ParsedValue = ParsedValue * 100.0f;
                if (ParsedValue > trackPeakBackgroundRatio.Maximum)
                {
                    trackPeakBackgroundRatio.Value = (trackPeakBackgroundRatio.Maximum);
                    txtPeakPeakBackgroundRatioRatio.Text = (trackPeakBackgroundRatio.Value / 100.0).ToString();
                }
                else if (ParsedValue < trackPeakBackgroundRatio.Minimum)
                {
                    trackPeakBackgroundRatio.Value = trackPeakBackgroundRatio.Minimum;
                    txtPeakPeakBackgroundRatioRatio.Text = (trackPeakBackgroundRatio.Value / 100.0).ToString();
                }
                else
                {
                    trackPeakBackgroundRatio.Value = Convert.ToInt32(ParsedValue);
                }
            }
            else
            {
                MessageBox.Show("Please enter value: " + (trackPeakBackgroundRatio.Minimum / 100.0).ToString() + " ~ " + (trackPeakBackgroundRatio.Maximum / 100.0).ToString());
                txtPeakPeakBackgroundRatioRatio.Text = (trackPeakBackgroundRatio.Value / 100.0).ToString();
            }
        }

        private void txtPeptideMinRatio_TextChanged(object sender, EventArgs e)
        {
            float ParsedValue = 0;
            if (float.TryParse(txtPeptideMinRatio.Text, out ParsedValue))
            {
                ParsedValue = ParsedValue * 100.0f;
                if (ParsedValue > trackPeptideMinRatio.Maximum)
                {
                    trackPeptideMinRatio.Value = (trackPeptideMinRatio.Maximum);
                    txtPeptideMinRatio.Text = (trackPeptideMinRatio.Value / 100.0).ToString();
                }
                else if (ParsedValue < trackPeptideMinRatio.Minimum)
                {
                    trackPeptideMinRatio.Value = trackPeptideMinRatio.Minimum;
                    txtPeptideMinRatio.Text = (trackPeptideMinRatio.Value / 100.0).ToString();
                }
                else
                {
                    trackPeptideMinRatio.Value = Convert.ToInt32(ParsedValue);
                }
            }
            else
            {
                MessageBox.Show("Please enter value: " + (trackPeptideMinRatio.Minimum / 100.0).ToString() + " ~ " + (trackPeptideMinRatio.Maximum / 100.0).ToString());
                txtPeptideMinRatio.Text = (trackPeptideMinRatio.Value / 100.0).ToString();
            }
        }

        private void txtMaxCharge_TextChanged(object sender, EventArgs e)
        {
            int ParsedValue = 0;
            if (Int32.TryParse(txtMaxCharge.Text, out ParsedValue))
            {
                if (ParsedValue > trackMaxCharge.Maximum)
                {
                    trackMaxCharge.Value = trackMaxCharge.Maximum;
                    txtMaxCharge.Text = trackMaxCharge.Value.ToString();
                }
                else if (ParsedValue < trackMaxCharge.Minimum)
                {
                    trackMaxCharge.Value = trackMaxCharge.Minimum;
                    txtMaxCharge.Text = trackMaxCharge.Value.ToString();
                }
                else
                {
                    trackMaxCharge.Value = Convert.ToInt32(ParsedValue);
                }
            }
            else
            {
                MessageBox.Show("Please enter value: " + trackMaxCharge.Minimum.ToString() + " ~ " + trackMaxCharge.Maximum.ToString());
                txtMaxCharge.Text = trackMaxCharge.Value.ToString();
            }
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            if (rawReader == null)
            {
                MessageBox.Show("Load Raw data first");
                return;
            }
            if (txtScanNo.Text == "")
            {
                MessageBox.Show("Input Scan number");
                return;
            }
            txtPeaks.Text = "";
            int ScanNo = Convert.ToInt32(txtScanNo.Text);
            GlypID.Peaks.clsPeakProcessorParameters _peakParameter = new GlypID.Peaks.clsPeakProcessorParameters();
            GlypID.HornTransform.clsHornTransformParameters _transformParameters = new GlypID.HornTransform.clsHornTransformParameters();


            _transformParameters.UseAbsolutePeptideIntensity = chkPeptideMinAbso.Checked;
            _transformParameters.AbsolutePeptideIntensity = 0.0;
            if (_transformParameters.UseAbsolutePeptideIntensity)
            {
                _transformParameters.AbsolutePeptideIntensity = Convert.ToDouble(txtPeptideMinAbso.Text);
            }
            _transformParameters.MaxCharge = Convert.ToInt16(txtMaxCharge.Text);
            _transformParameters.PeptideMinBackgroundRatio = Convert.ToDouble(txtPeptideMinRatio.Text);
            _peakParameter.SignalToNoiseThreshold = Convert.ToDouble(txtSN.Text);
            _peakParameter.PeakBackgroundRatio = Convert.ToDouble(txtPeakPeakBackgroundRatioRatio.Text);


            rawReader.SetPeakProcessorParameter(_peakParameter);
            rawReader.SetTransformParameter(_transformParameters);

            for (int i = 0; i <= 10; i++)
            {
                MSScan GMSScan = rawReader.ReadScan(ScanNo);
                List<MSPeak> deIsotopedPeaks = GMSScan.MSPeaks;
                deIsotopedPeaks.Sort();
                txtPeaks.Text = "Scan Number" + ScanNo.ToString() + Environment.NewLine;
                txtPeaks.Text = txtPeaks.Text + "MSLevel:" + GMSScan.MsLevel.ToString() + "\t\tNumber Peaks" + deIsotopedPeaks.Count.ToString() + Environment.NewLine;
                txtPeaks.Text = txtPeaks.Text + "MonoisotopicMZ\tMonoIntensity\tCharge" + Environment.NewLine;
                foreach (MSPeak p in deIsotopedPeaks)
                {
                    txtPeaks.Text = txtPeaks.Text + p.MonoisotopicMZ.ToString("0.0000") + "\t\t" + p.MonoIntensity.ToString() + "\t\t" + p.ChargeState.ToString() + Environment.NewLine;
                }
            }
        }

        private void btnForward_Click(object sender, EventArgs e)
        {
            int ScanNo = Convert.ToInt32(txtScanNo.Text);
            if (ScanNo + 1 >rawReader.NumberOfScans)
            {
                MessageBox.Show("Last Scan");
                return;
            }
            else
            {
                txtScanNo.Text = (ScanNo + 1).ToString();
                btnRead_Click(this, e);
            }

        }

        private void btnBackward_Click(object sender, EventArgs e)
        {
            int ScanNo = Convert.ToInt32(txtScanNo.Text);
            if (ScanNo -1  ==0)
            {
                MessageBox.Show("First Scan");
                return;
            }
            else
            {
                txtScanNo.Text = (ScanNo -1).ToString();
                btnRead_Click(this, e);
            }

        }

        private void btnLoadRawCSMSL_Click(object sender, EventArgs e)
        {
            if(openFileDialog1.ShowDialog () == System.Windows.Forms.DialogResult.OK)
            {
                COL.MassLib.ThermoRawReader raw = new ThermoRawReader(openFileDialog1.FileName);

            }
        }
    }
}
