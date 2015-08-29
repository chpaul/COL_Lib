namespace COLLibImplTool
{
    partial class frmMain
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPeakDetection = new System.Windows.Forms.TabPage();
            this.lblScanRange = new System.Windows.Forms.Label();
            this.btnForward = new System.Windows.Forms.Button();
            this.btnBackward = new System.Windows.Forms.Button();
            this.btnRead = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPeaks = new System.Windows.Forms.TextBox();
            this.txtScanNo = new System.Windows.Forms.TextBox();
            this.btnLoadRaw = new System.Windows.Forms.Button();
            this.txtPeptideMinAbso = new System.Windows.Forms.TextBox();
            this.trackSN = new System.Windows.Forms.TrackBar();
            this.chkPeptideMinAbso = new System.Windows.Forms.CheckBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtMaxCharge = new System.Windows.Forms.TextBox();
            this.trackPeptideMinRatio = new System.Windows.Forms.TrackBar();
            this.txtPeptideMinRatio = new System.Windows.Forms.TextBox();
            this.trackPeakBackgroundRatio = new System.Windows.Forms.TrackBar();
            this.txtPeakPeakBackgroundRatioRatio = new System.Windows.Forms.TextBox();
            this.trackMaxCharge = new System.Windows.Forms.TrackBar();
            this.txtSN = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnForward_CSMSL = new System.Windows.Forms.Button();
            this.btnBackward_CSMSL = new System.Windows.Forms.Button();
            this.btnRead_CSMSL = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPeaks_CSMSL = new System.Windows.Forms.TextBox();
            this.txtScanNo_CSMSL = new System.Windows.Forms.TextBox();
            this.btnLoadRawCSMSL = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtSN_CSMSL = new System.Windows.Forms.TextBox();
            this.txtPPM_CSMSL = new System.Windows.Forms.TextBox();
            this.tabControl1.SuspendLayout();
            this.tabPeakDetection.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackSN)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackPeptideMinRatio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackPeakBackgroundRatio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackMaxCharge)).BeginInit();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPeakDetection);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(811, 505);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPeakDetection
            // 
            this.tabPeakDetection.Controls.Add(this.lblScanRange);
            this.tabPeakDetection.Controls.Add(this.btnForward);
            this.tabPeakDetection.Controls.Add(this.btnBackward);
            this.tabPeakDetection.Controls.Add(this.btnRead);
            this.tabPeakDetection.Controls.Add(this.label1);
            this.tabPeakDetection.Controls.Add(this.txtPeaks);
            this.tabPeakDetection.Controls.Add(this.txtScanNo);
            this.tabPeakDetection.Controls.Add(this.btnLoadRaw);
            this.tabPeakDetection.Controls.Add(this.txtPeptideMinAbso);
            this.tabPeakDetection.Controls.Add(this.trackSN);
            this.tabPeakDetection.Controls.Add(this.chkPeptideMinAbso);
            this.tabPeakDetection.Controls.Add(this.label13);
            this.tabPeakDetection.Controls.Add(this.label7);
            this.tabPeakDetection.Controls.Add(this.label12);
            this.tabPeakDetection.Controls.Add(this.label8);
            this.tabPeakDetection.Controls.Add(this.label11);
            this.tabPeakDetection.Controls.Add(this.label9);
            this.tabPeakDetection.Controls.Add(this.label10);
            this.tabPeakDetection.Controls.Add(this.txtMaxCharge);
            this.tabPeakDetection.Controls.Add(this.trackPeptideMinRatio);
            this.tabPeakDetection.Controls.Add(this.txtPeptideMinRatio);
            this.tabPeakDetection.Controls.Add(this.trackPeakBackgroundRatio);
            this.tabPeakDetection.Controls.Add(this.txtPeakPeakBackgroundRatioRatio);
            this.tabPeakDetection.Controls.Add(this.trackMaxCharge);
            this.tabPeakDetection.Controls.Add(this.txtSN);
            this.tabPeakDetection.Location = new System.Drawing.Point(4, 22);
            this.tabPeakDetection.Name = "tabPeakDetection";
            this.tabPeakDetection.Padding = new System.Windows.Forms.Padding(3);
            this.tabPeakDetection.Size = new System.Drawing.Size(803, 479);
            this.tabPeakDetection.TabIndex = 0;
            this.tabPeakDetection.Text = "Peak detection(DeCon)";
            this.tabPeakDetection.UseVisualStyleBackColor = true;
            // 
            // lblScanRange
            // 
            this.lblScanRange.AutoSize = true;
            this.lblScanRange.Location = new System.Drawing.Point(8, 50);
            this.lblScanRange.Name = "lblScanRange";
            this.lblScanRange.Size = new System.Drawing.Size(0, 13);
            this.lblScanRange.TabIndex = 63;
            // 
            // btnForward
            // 
            this.btnForward.Enabled = false;
            this.btnForward.Location = new System.Drawing.Point(285, 14);
            this.btnForward.Name = "btnForward";
            this.btnForward.Size = new System.Drawing.Size(26, 23);
            this.btnForward.TabIndex = 62;
            this.btnForward.Text = ">";
            this.btnForward.UseVisualStyleBackColor = true;
            this.btnForward.Click += new System.EventHandler(this.btnForward_Click);
            // 
            // btnBackward
            // 
            this.btnBackward.Enabled = false;
            this.btnBackward.Location = new System.Drawing.Point(250, 14);
            this.btnBackward.Name = "btnBackward";
            this.btnBackward.Size = new System.Drawing.Size(29, 23);
            this.btnBackward.TabIndex = 61;
            this.btnBackward.Text = "<";
            this.btnBackward.UseVisualStyleBackColor = true;
            this.btnBackward.Click += new System.EventHandler(this.btnBackward_Click);
            // 
            // btnRead
            // 
            this.btnRead.Enabled = false;
            this.btnRead.Location = new System.Drawing.Point(169, 40);
            this.btnRead.Name = "btnRead";
            this.btnRead.Size = new System.Drawing.Size(75, 23);
            this.btnRead.TabIndex = 60;
            this.btnRead.Text = "Read";
            this.btnRead.UseVisualStyleBackColor = true;
            this.btnRead.Click += new System.EventHandler(this.btnRead_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(103, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 59;
            this.label1.Text = "Scan Num:";
            // 
            // txtPeaks
            // 
            this.txtPeaks.Location = new System.Drawing.Point(367, 18);
            this.txtPeaks.Multiline = true;
            this.txtPeaks.Name = "txtPeaks";
            this.txtPeaks.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtPeaks.Size = new System.Drawing.Size(419, 430);
            this.txtPeaks.TabIndex = 58;
            // 
            // txtScanNo
            // 
            this.txtScanNo.Location = new System.Drawing.Point(169, 14);
            this.txtScanNo.Name = "txtScanNo";
            this.txtScanNo.Size = new System.Drawing.Size(75, 20);
            this.txtScanNo.TabIndex = 57;
            // 
            // btnLoadRaw
            // 
            this.btnLoadRaw.Location = new System.Drawing.Point(11, 14);
            this.btnLoadRaw.Name = "btnLoadRaw";
            this.btnLoadRaw.Size = new System.Drawing.Size(75, 23);
            this.btnLoadRaw.TabIndex = 56;
            this.btnLoadRaw.Text = "Load Raw";
            this.btnLoadRaw.UseVisualStyleBackColor = true;
            this.btnLoadRaw.Click += new System.EventHandler(this.btnLoadRaw_Click);
            // 
            // txtPeptideMinAbso
            // 
            this.txtPeptideMinAbso.Enabled = false;
            this.txtPeptideMinAbso.Location = new System.Drawing.Point(242, 324);
            this.txtPeptideMinAbso.Name = "txtPeptideMinAbso";
            this.txtPeptideMinAbso.Size = new System.Drawing.Size(100, 20);
            this.txtPeptideMinAbso.TabIndex = 55;
            // 
            // trackSN
            // 
            this.trackSN.Location = new System.Drawing.Point(8, 121);
            this.trackSN.Maximum = 100;
            this.trackSN.Name = "trackSN";
            this.trackSN.Size = new System.Drawing.Size(271, 45);
            this.trackSN.TabIndex = 45;
            this.trackSN.Value = 2;
            this.trackSN.Scroll += new System.EventHandler(this.trackSN_Scroll);
            // 
            // chkPeptideMinAbso
            // 
            this.chkPeptideMinAbso.AutoSize = true;
            this.chkPeptideMinAbso.Location = new System.Drawing.Point(11, 326);
            this.chkPeptideMinAbso.Name = "chkPeptideMinAbso";
            this.chkPeptideMinAbso.Size = new System.Drawing.Size(167, 17);
            this.chkPeptideMinAbso.TabIndex = 54;
            this.chkPeptideMinAbso.Text = "Use absolute peptide intensity";
            this.chkPeptideMinAbso.UseVisualStyleBackColor = true;
            this.chkPeptideMinAbso.CheckedChanged += new System.EventHandler(this.chkPeptideMinAbso_CheckedChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(8, 103);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(99, 13);
            this.label13.TabIndex = 39;
            this.label13.Text = "Single to noise ratio";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 257);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(180, 13);
            this.label7.TabIndex = 53;
            this.label7.Text = "(this value * background = threshold)";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(8, 171);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(115, 13);
            this.label12.TabIndex = 40;
            this.label12.Text = "Peak background ratio";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(259, 83);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(83, 13);
            this.label8.TabIndex = 52;
            this.label8.Text = "Less Peak(Fast)";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(8, 239);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(145, 13);
            this.label11.TabIndex = 41;
            this.label11.Text = "Peptide min background ratio";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(8, 83);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(86, 13);
            this.label9.TabIndex = 51;
            this.label9.Text = "More Peak(slow)";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(8, 355);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(63, 13);
            this.label10.TabIndex = 42;
            this.label10.Text = "Max charge";
            // 
            // txtMaxCharge
            // 
            this.txtMaxCharge.Location = new System.Drawing.Point(285, 370);
            this.txtMaxCharge.Name = "txtMaxCharge";
            this.txtMaxCharge.Size = new System.Drawing.Size(57, 20);
            this.txtMaxCharge.TabIndex = 50;
            this.txtMaxCharge.Text = "5";
            this.txtMaxCharge.TextChanged += new System.EventHandler(this.txtMaxCharge_TextChanged);
            // 
            // trackPeptideMinRatio
            // 
            this.trackPeptideMinRatio.Location = new System.Drawing.Point(8, 275);
            this.trackPeptideMinRatio.Maximum = 1000;
            this.trackPeptideMinRatio.Name = "trackPeptideMinRatio";
            this.trackPeptideMinRatio.Size = new System.Drawing.Size(271, 45);
            this.trackPeptideMinRatio.TabIndex = 43;
            this.trackPeptideMinRatio.TickFrequency = 10;
            this.trackPeptideMinRatio.Value = 200;
            this.trackPeptideMinRatio.Scroll += new System.EventHandler(this.trackPeptideMinRatio_Scroll);
            // 
            // txtPeptideMinRatio
            // 
            this.txtPeptideMinRatio.Location = new System.Drawing.Point(285, 287);
            this.txtPeptideMinRatio.Name = "txtPeptideMinRatio";
            this.txtPeptideMinRatio.Size = new System.Drawing.Size(57, 20);
            this.txtPeptideMinRatio.TabIndex = 49;
            this.txtPeptideMinRatio.Text = "2";
            this.txtPeptideMinRatio.TextChanged += new System.EventHandler(this.txtPeptideMinRatio_TextChanged);
            // 
            // trackPeakBackgroundRatio
            // 
            this.trackPeakBackgroundRatio.Location = new System.Drawing.Point(8, 189);
            this.trackPeakBackgroundRatio.Maximum = 1000;
            this.trackPeakBackgroundRatio.Minimum = 1;
            this.trackPeakBackgroundRatio.Name = "trackPeakBackgroundRatio";
            this.trackPeakBackgroundRatio.Size = new System.Drawing.Size(271, 45);
            this.trackPeakBackgroundRatio.TabIndex = 44;
            this.trackPeakBackgroundRatio.TickFrequency = 10;
            this.trackPeakBackgroundRatio.Value = 100;
            this.trackPeakBackgroundRatio.Scroll += new System.EventHandler(this.trackPeakBackgroundRatio_Scroll);
            // 
            // txtPeakPeakBackgroundRatioRatio
            // 
            this.txtPeakPeakBackgroundRatioRatio.Location = new System.Drawing.Point(285, 201);
            this.txtPeakPeakBackgroundRatioRatio.Name = "txtPeakPeakBackgroundRatioRatio";
            this.txtPeakPeakBackgroundRatioRatio.Size = new System.Drawing.Size(57, 20);
            this.txtPeakPeakBackgroundRatioRatio.TabIndex = 48;
            this.txtPeakPeakBackgroundRatioRatio.Text = "1";
            this.txtPeakPeakBackgroundRatioRatio.TextChanged += new System.EventHandler(this.txtPeakPeakBackgroundRatioRatio_TextChanged);
            // 
            // trackMaxCharge
            // 
            this.trackMaxCharge.Location = new System.Drawing.Point(8, 370);
            this.trackMaxCharge.Minimum = 1;
            this.trackMaxCharge.Name = "trackMaxCharge";
            this.trackMaxCharge.Size = new System.Drawing.Size(271, 45);
            this.trackMaxCharge.TabIndex = 46;
            this.trackMaxCharge.Value = 5;
            this.trackMaxCharge.Scroll += new System.EventHandler(this.trackMaxCharge_Scroll);
            // 
            // txtSN
            // 
            this.txtSN.Location = new System.Drawing.Point(285, 133);
            this.txtSN.Name = "txtSN";
            this.txtSN.Size = new System.Drawing.Size(57, 20);
            this.txtSN.TabIndex = 47;
            this.txtSN.Text = "2";
            this.txtSN.TextChanged += new System.EventHandler(this.txtSN_TextChanged);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.txtPPM_CSMSL);
            this.tabPage1.Controls.Add(this.txtSN_CSMSL);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.btnForward_CSMSL);
            this.tabPage1.Controls.Add(this.btnBackward_CSMSL);
            this.tabPage1.Controls.Add(this.btnRead_CSMSL);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.txtPeaks_CSMSL);
            this.tabPage1.Controls.Add(this.txtScanNo_CSMSL);
            this.tabPage1.Controls.Add(this.btnLoadRawCSMSL);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(803, 479);
            this.tabPage1.TabIndex = 1;
            this.tabPage1.Text = "Peak detection(CSMSL)";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btnForward_CSMSL
            // 
            this.btnForward_CSMSL.Enabled = false;
            this.btnForward_CSMSL.Location = new System.Drawing.Point(288, 22);
            this.btnForward_CSMSL.Name = "btnForward_CSMSL";
            this.btnForward_CSMSL.Size = new System.Drawing.Size(26, 23);
            this.btnForward_CSMSL.TabIndex = 69;
            this.btnForward_CSMSL.Text = ">";
            this.btnForward_CSMSL.UseVisualStyleBackColor = true;
            // 
            // btnBackward_CSMSL
            // 
            this.btnBackward_CSMSL.Enabled = false;
            this.btnBackward_CSMSL.Location = new System.Drawing.Point(253, 22);
            this.btnBackward_CSMSL.Name = "btnBackward_CSMSL";
            this.btnBackward_CSMSL.Size = new System.Drawing.Size(29, 23);
            this.btnBackward_CSMSL.TabIndex = 68;
            this.btnBackward_CSMSL.Text = "<";
            this.btnBackward_CSMSL.UseVisualStyleBackColor = true;
            // 
            // btnRead_CSMSL
            // 
            this.btnRead_CSMSL.Enabled = false;
            this.btnRead_CSMSL.Location = new System.Drawing.Point(172, 48);
            this.btnRead_CSMSL.Name = "btnRead_CSMSL";
            this.btnRead_CSMSL.Size = new System.Drawing.Size(75, 23);
            this.btnRead_CSMSL.TabIndex = 67;
            this.btnRead_CSMSL.Text = "Read";
            this.btnRead_CSMSL.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(106, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 66;
            this.label2.Text = "Scan Num:";
            // 
            // txtPeaks_CSMSL
            // 
            this.txtPeaks_CSMSL.Location = new System.Drawing.Point(370, 26);
            this.txtPeaks_CSMSL.Multiline = true;
            this.txtPeaks_CSMSL.Name = "txtPeaks_CSMSL";
            this.txtPeaks_CSMSL.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtPeaks_CSMSL.Size = new System.Drawing.Size(419, 430);
            this.txtPeaks_CSMSL.TabIndex = 65;
            // 
            // txtScanNo_CSMSL
            // 
            this.txtScanNo_CSMSL.Location = new System.Drawing.Point(172, 22);
            this.txtScanNo_CSMSL.Name = "txtScanNo_CSMSL";
            this.txtScanNo_CSMSL.Size = new System.Drawing.Size(75, 20);
            this.txtScanNo_CSMSL.TabIndex = 64;
            // 
            // btnLoadRawCSMSL
            // 
            this.btnLoadRawCSMSL.Location = new System.Drawing.Point(14, 22);
            this.btnLoadRawCSMSL.Name = "btnLoadRawCSMSL";
            this.btnLoadRawCSMSL.Size = new System.Drawing.Size(75, 23);
            this.btnLoadRawCSMSL.TabIndex = 63;
            this.btnLoadRawCSMSL.Text = "Load Raw";
            this.btnLoadRawCSMSL.UseVisualStyleBackColor = true;
            this.btnLoadRawCSMSL.Click += new System.EventHandler(this.btnLoadRawCSMSL_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 157);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(22, 13);
            this.label3.TabIndex = 70;
            this.label3.Text = "SN";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 179);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 13);
            this.label4.TabIndex = 71;
            this.label4.Text = "PPM";
            // 
            // txtSN_CSMSL
            // 
            this.txtSN_CSMSL.Location = new System.Drawing.Point(82, 150);
            this.txtSN_CSMSL.Name = "txtSN_CSMSL";
            this.txtSN_CSMSL.Size = new System.Drawing.Size(100, 20);
            this.txtSN_CSMSL.TabIndex = 72;
            this.txtSN_CSMSL.Text = "2";
            // 
            // txtPPM_CSMSL
            // 
            this.txtPPM_CSMSL.Location = new System.Drawing.Point(82, 176);
            this.txtPPM_CSMSL.Name = "txtPPM_CSMSL";
            this.txtPPM_CSMSL.Size = new System.Drawing.Size(100, 20);
            this.txtPPM_CSMSL.TabIndex = 73;
            this.txtPPM_CSMSL.Text = "6";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(811, 505);
            this.Controls.Add(this.tabControl1);
            this.Name = "frmMain";
            this.Text = "COL MS Tools";
            this.tabControl1.ResumeLayout(false);
            this.tabPeakDetection.ResumeLayout(false);
            this.tabPeakDetection.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackSN)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackPeptideMinRatio)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackPeakBackgroundRatio)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackMaxCharge)).EndInit();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPeakDetection;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPeaks;
        private System.Windows.Forms.TextBox txtScanNo;
        private System.Windows.Forms.Button btnLoadRaw;
        private System.Windows.Forms.TextBox txtPeptideMinAbso;
        private System.Windows.Forms.TrackBar trackSN;
        private System.Windows.Forms.CheckBox chkPeptideMinAbso;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtMaxCharge;
        private System.Windows.Forms.TrackBar trackPeptideMinRatio;
        private System.Windows.Forms.TextBox txtPeptideMinRatio;
        private System.Windows.Forms.TrackBar trackPeakBackgroundRatio;
        private System.Windows.Forms.TextBox txtPeakPeakBackgroundRatioRatio;
        private System.Windows.Forms.TrackBar trackMaxCharge;
        private System.Windows.Forms.TextBox txtSN;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btnRead;
        private System.Windows.Forms.Button btnForward;
        private System.Windows.Forms.Button btnBackward;
        private System.Windows.Forms.Label lblScanRange;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TextBox txtPPM_CSMSL;
        private System.Windows.Forms.TextBox txtSN_CSMSL;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnForward_CSMSL;
        private System.Windows.Forms.Button btnBackward_CSMSL;
        private System.Windows.Forms.Button btnRead_CSMSL;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPeaks_CSMSL;
        private System.Windows.Forms.TextBox txtScanNo_CSMSL;
        private System.Windows.Forms.Button btnLoadRawCSMSL;
    }
}

