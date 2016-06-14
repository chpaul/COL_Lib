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
            this.components = new System.ComponentModel.Container();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.button1 = new System.Windows.Forms.Button();
            this.btnLoadRawCSMSL = new System.Windows.Forms.Button();
            this.txtPeak = new System.Windows.Forms.TextBox();
            this.txtScanNo_CSMSL = new System.Windows.Forms.TextBox();
            this.txtCharge = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnRead_CSMSL = new System.Windows.Forms.Button();
            this.txtTargetMZ = new System.Windows.Forms.TextBox();
            this.btnBackward_CSMSL = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnForward_CSMSL = new System.Windows.Forms.Button();
            this.btnGetPeaks = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.lblRawInfo = new System.Windows.Forms.Label();
            this.txtPPM_CSMSL = new System.Windows.Forms.TextBox();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.txtPeaks_CSMSL = new System.Windows.Forms.TextBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.zedGraphControl1 = new ZedGraph.ZedGraphControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.chkChargeMode = new System.Windows.Forms.CheckBox();
            this.txtAdduct_user = new System.Windows.Forms.TextBox();
            this.chkAdduct_user = new System.Windows.Forms.CheckBox();
            this.chkLoadFile = new System.Windows.Forms.CheckBox();
            this.chkAdduct_K = new System.Windows.Forms.CheckBox();
            this.chkAdduct_Na = new System.Windows.Forms.CheckBox();
            this.chkAdduct_NH4 = new System.Windows.Forms.CheckBox();
            this.chkAdduct_H = new System.Windows.Forms.CheckBox();
            this.chkPermethylation = new System.Windows.Forms.CheckBox();
            this.chkReducedReducingEnd = new System.Windows.Forms.CheckBox();
            this.rdoDRAG = new System.Windows.Forms.RadioButton();
            this.rdoMultiplexPermethylation = new System.Windows.Forms.RadioButton();
            this.rdoLabelFree = new System.Windows.Forms.RadioButton();
            this.txtHex = new System.Windows.Forms.TextBox();
            this.txtHexNAc = new System.Windows.Forms.TextBox();
            this.txtSia = new System.Windows.Forms.TextBox();
            this.txtFuc = new System.Windows.Forms.TextBox();
            this.btnGlycan = new System.Windows.Forms.Button();
            this.lblSia = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.labelFuc = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dgTable = new System.Windows.Forms.DataGridView();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.label10 = new System.Windows.Forms.Label();
            this.cboPepMutation = new System.Windows.Forms.ComboBox();
            this.chkEnzy_GlucE = new System.Windows.Forms.CheckBox();
            this.chkEnzy_Trypsin = new System.Windows.Forms.CheckBox();
            this.chkEnzy_GlucED = new System.Windows.Forms.CheckBox();
            this.chkEnzy_None = new System.Windows.Forms.CheckBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtMissCleavage = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtPeptides = new System.Windows.Forms.TextBox();
            this.chkCYS_CAM = new System.Windows.Forms.CheckBox();
            this.btnReadFasta = new System.Windows.Forms.Button();
            this.lblPeptideMass = new System.Windows.Forms.Label();
            this.btnGetPeptideMass = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.txtPeptideSeq = new System.Windows.Forms.TextBox();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.picGlycan = new System.Windows.Forms.PictureBox();
            this.btnGlycanDraw = new System.Windows.Forms.Button();
            this.txtIUPAC = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.button2 = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgTable)).BeginInit();
            this.tabPage5.SuspendLayout();
            this.tabPage6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picGlycan)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Controls.Add(this.tabPage6);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(885, 534);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.splitContainer2);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(877, 508);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Scan Info";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(3, 3);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.button1);
            this.splitContainer2.Panel1.Controls.Add(this.btnLoadRawCSMSL);
            this.splitContainer2.Panel1.Controls.Add(this.txtPeak);
            this.splitContainer2.Panel1.Controls.Add(this.txtScanNo_CSMSL);
            this.splitContainer2.Panel1.Controls.Add(this.txtCharge);
            this.splitContainer2.Panel1.Controls.Add(this.label2);
            this.splitContainer2.Panel1.Controls.Add(this.label3);
            this.splitContainer2.Panel1.Controls.Add(this.btnRead_CSMSL);
            this.splitContainer2.Panel1.Controls.Add(this.txtTargetMZ);
            this.splitContainer2.Panel1.Controls.Add(this.btnBackward_CSMSL);
            this.splitContainer2.Panel1.Controls.Add(this.label1);
            this.splitContainer2.Panel1.Controls.Add(this.btnForward_CSMSL);
            this.splitContainer2.Panel1.Controls.Add(this.btnGetPeaks);
            this.splitContainer2.Panel1.Controls.Add(this.label4);
            this.splitContainer2.Panel1.Controls.Add(this.lblRawInfo);
            this.splitContainer2.Panel1.Controls.Add(this.txtPPM_CSMSL);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.tabControl2);
            this.splitContainer2.Size = new System.Drawing.Size(871, 502);
            this.splitContainer2.SplitterDistance = 208;
            this.splitContainer2.TabIndex = 92;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(115, 13);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 92;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnLoadRawCSMSL
            // 
            this.btnLoadRawCSMSL.Location = new System.Drawing.Point(3, 13);
            this.btnLoadRawCSMSL.Name = "btnLoadRawCSMSL";
            this.btnLoadRawCSMSL.Size = new System.Drawing.Size(75, 23);
            this.btnLoadRawCSMSL.TabIndex = 74;
            this.btnLoadRawCSMSL.Text = "Load Raw";
            this.btnLoadRawCSMSL.UseVisualStyleBackColor = true;
            this.btnLoadRawCSMSL.Click += new System.EventHandler(this.btnLoadRawCSMSL_Click);
            // 
            // txtPeak
            // 
            this.txtPeak.Location = new System.Drawing.Point(3, 277);
            this.txtPeak.Multiline = true;
            this.txtPeak.Name = "txtPeak";
            this.txtPeak.Size = new System.Drawing.Size(187, 166);
            this.txtPeak.TabIndex = 91;
            // 
            // txtScanNo_CSMSL
            // 
            this.txtScanNo_CSMSL.Location = new System.Drawing.Point(6, 117);
            this.txtScanNo_CSMSL.Name = "txtScanNo_CSMSL";
            this.txtScanNo_CSMSL.Size = new System.Drawing.Size(75, 20);
            this.txtScanNo_CSMSL.TabIndex = 75;
            // 
            // txtCharge
            // 
            this.txtCharge.Location = new System.Drawing.Point(71, 222);
            this.txtCharge.Name = "txtCharge";
            this.txtCharge.Size = new System.Drawing.Size(75, 20);
            this.txtCharge.TabIndex = 90;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 101);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 77;
            this.label2.Text = "Scan Num:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 222);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 13);
            this.label3.TabIndex = 89;
            this.label3.Text = "Charge";
            // 
            // btnRead_CSMSL
            // 
            this.btnRead_CSMSL.Enabled = false;
            this.btnRead_CSMSL.Location = new System.Drawing.Point(115, 115);
            this.btnRead_CSMSL.Name = "btnRead_CSMSL";
            this.btnRead_CSMSL.Size = new System.Drawing.Size(75, 23);
            this.btnRead_CSMSL.TabIndex = 78;
            this.btnRead_CSMSL.Text = "Read";
            this.btnRead_CSMSL.UseVisualStyleBackColor = true;
            this.btnRead_CSMSL.Click += new System.EventHandler(this.btnRead_CSMSL_Click);
            // 
            // txtTargetMZ
            // 
            this.txtTargetMZ.Location = new System.Drawing.Point(71, 194);
            this.txtTargetMZ.Name = "txtTargetMZ";
            this.txtTargetMZ.Size = new System.Drawing.Size(75, 20);
            this.txtTargetMZ.TabIndex = 88;
            // 
            // btnBackward_CSMSL
            // 
            this.btnBackward_CSMSL.Enabled = false;
            this.btnBackward_CSMSL.Location = new System.Drawing.Point(115, 144);
            this.btnBackward_CSMSL.Name = "btnBackward_CSMSL";
            this.btnBackward_CSMSL.Size = new System.Drawing.Size(29, 23);
            this.btnBackward_CSMSL.TabIndex = 79;
            this.btnBackward_CSMSL.Text = "<";
            this.btnBackward_CSMSL.UseVisualStyleBackColor = true;
            this.btnBackward_CSMSL.Click += new System.EventHandler(this.btnBackward_CSMSL_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 197);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 87;
            this.label1.Text = "Target M/Z";
            // 
            // btnForward_CSMSL
            // 
            this.btnForward_CSMSL.Enabled = false;
            this.btnForward_CSMSL.Location = new System.Drawing.Point(164, 144);
            this.btnForward_CSMSL.Name = "btnForward_CSMSL";
            this.btnForward_CSMSL.Size = new System.Drawing.Size(26, 23);
            this.btnForward_CSMSL.TabIndex = 80;
            this.btnForward_CSMSL.Text = ">";
            this.btnForward_CSMSL.UseVisualStyleBackColor = true;
            this.btnForward_CSMSL.Click += new System.EventHandler(this.btnForward_CSMSL_Click);
            // 
            // btnGetPeaks
            // 
            this.btnGetPeaks.Location = new System.Drawing.Point(1, 248);
            this.btnGetPeaks.Name = "btnGetPeaks";
            this.btnGetPeaks.Size = new System.Drawing.Size(75, 23);
            this.btnGetPeaks.TabIndex = 86;
            this.btnGetPeaks.Text = "Get Peak";
            this.btnGetPeaks.UseVisualStyleBackColor = true;
            this.btnGetPeaks.Click += new System.EventHandler(this.btnGetPeaks_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 174);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 13);
            this.label4.TabIndex = 82;
            this.label4.Text = "PPM";
            // 
            // lblRawInfo
            // 
            this.lblRawInfo.AutoSize = true;
            this.lblRawInfo.Location = new System.Drawing.Point(3, 39);
            this.lblRawInfo.Name = "lblRawInfo";
            this.lblRawInfo.Size = new System.Drawing.Size(52, 13);
            this.lblRawInfo.TabIndex = 85;
            this.lblRawInfo.Text = "Raw info:";
            // 
            // txtPPM_CSMSL
            // 
            this.txtPPM_CSMSL.Location = new System.Drawing.Point(39, 171);
            this.txtPPM_CSMSL.Name = "txtPPM_CSMSL";
            this.txtPPM_CSMSL.Size = new System.Drawing.Size(42, 20);
            this.txtPPM_CSMSL.TabIndex = 84;
            this.txtPPM_CSMSL.Text = "6";
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tabPage3);
            this.tabControl2.Controls.Add(this.tabPage4);
            this.tabControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl2.Location = new System.Drawing.Point(0, 0);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(659, 502);
            this.tabControl2.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.txtPeaks_CSMSL);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(651, 476);
            this.tabPage3.TabIndex = 0;
            this.tabPage3.Text = "Peak List";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // txtPeaks_CSMSL
            // 
            this.txtPeaks_CSMSL.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtPeaks_CSMSL.Location = new System.Drawing.Point(3, 3);
            this.txtPeaks_CSMSL.Multiline = true;
            this.txtPeaks_CSMSL.Name = "txtPeaks_CSMSL";
            this.txtPeaks_CSMSL.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtPeaks_CSMSL.Size = new System.Drawing.Size(645, 470);
            this.txtPeaks_CSMSL.TabIndex = 76;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.zedGraphControl1);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(651, 476);
            this.tabPage4.TabIndex = 1;
            this.tabPage4.Text = "Graph";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // zedGraphControl1
            // 
            this.zedGraphControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.zedGraphControl1.Location = new System.Drawing.Point(3, 3);
            this.zedGraphControl1.Name = "zedGraphControl1";
            this.zedGraphControl1.ScrollGrace = 0D;
            this.zedGraphControl1.ScrollMaxX = 0D;
            this.zedGraphControl1.ScrollMaxY = 0D;
            this.zedGraphControl1.ScrollMaxY2 = 0D;
            this.zedGraphControl1.ScrollMinX = 0D;
            this.zedGraphControl1.ScrollMinY = 0D;
            this.zedGraphControl1.ScrollMinY2 = 0D;
            this.zedGraphControl1.Size = new System.Drawing.Size(645, 470);
            this.zedGraphControl1.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.splitContainer1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(877, 508);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Glycan";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.chkChargeMode);
            this.splitContainer1.Panel1.Controls.Add(this.txtAdduct_user);
            this.splitContainer1.Panel1.Controls.Add(this.chkAdduct_user);
            this.splitContainer1.Panel1.Controls.Add(this.chkLoadFile);
            this.splitContainer1.Panel1.Controls.Add(this.chkAdduct_K);
            this.splitContainer1.Panel1.Controls.Add(this.chkAdduct_Na);
            this.splitContainer1.Panel1.Controls.Add(this.chkAdduct_NH4);
            this.splitContainer1.Panel1.Controls.Add(this.chkAdduct_H);
            this.splitContainer1.Panel1.Controls.Add(this.chkPermethylation);
            this.splitContainer1.Panel1.Controls.Add(this.chkReducedReducingEnd);
            this.splitContainer1.Panel1.Controls.Add(this.rdoDRAG);
            this.splitContainer1.Panel1.Controls.Add(this.rdoMultiplexPermethylation);
            this.splitContainer1.Panel1.Controls.Add(this.rdoLabelFree);
            this.splitContainer1.Panel1.Controls.Add(this.txtHex);
            this.splitContainer1.Panel1.Controls.Add(this.txtHexNAc);
            this.splitContainer1.Panel1.Controls.Add(this.txtSia);
            this.splitContainer1.Panel1.Controls.Add(this.txtFuc);
            this.splitContainer1.Panel1.Controls.Add(this.btnGlycan);
            this.splitContainer1.Panel1.Controls.Add(this.lblSia);
            this.splitContainer1.Panel1.Controls.Add(this.label7);
            this.splitContainer1.Panel1.Controls.Add(this.labelFuc);
            this.splitContainer1.Panel1.Controls.Add(this.label5);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dgTable);
            this.splitContainer1.Size = new System.Drawing.Size(877, 508);
            this.splitContainer1.SplitterDistance = 78;
            this.splitContainer1.TabIndex = 1;
            // 
            // chkChargeMode
            // 
            this.chkChargeMode.AutoSize = true;
            this.chkChargeMode.Checked = true;
            this.chkChargeMode.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkChargeMode.Location = new System.Drawing.Point(655, 7);
            this.chkChargeMode.Name = "chkChargeMode";
            this.chkChargeMode.Size = new System.Drawing.Size(100, 17);
            this.chkChargeMode.TabIndex = 2;
            this.chkChargeMode.Text = "Positive Charge";
            this.chkChargeMode.UseVisualStyleBackColor = true;
            // 
            // txtAdduct_user
            // 
            this.txtAdduct_user.Location = new System.Drawing.Point(402, 27);
            this.txtAdduct_user.Name = "txtAdduct_user";
            this.txtAdduct_user.Size = new System.Drawing.Size(80, 20);
            this.txtAdduct_user.TabIndex = 22;
            // 
            // chkAdduct_user
            // 
            this.chkAdduct_user.AutoSize = true;
            this.chkAdduct_user.Location = new System.Drawing.Point(402, 7);
            this.chkAdduct_user.Name = "chkAdduct_user";
            this.chkAdduct_user.Size = new System.Drawing.Size(48, 17);
            this.chkAdduct_user.TabIndex = 21;
            this.chkAdduct_user.Text = "User";
            this.chkAdduct_user.UseVisualStyleBackColor = true;
            // 
            // chkLoadFile
            // 
            this.chkLoadFile.AutoSize = true;
            this.chkLoadFile.Location = new System.Drawing.Point(764, 41);
            this.chkLoadFile.Name = "chkLoadFile";
            this.chkLoadFile.Size = new System.Drawing.Size(69, 17);
            this.chkLoadFile.TabIndex = 19;
            this.chkLoadFile.Text = "Load File";
            this.chkLoadFile.UseVisualStyleBackColor = true;
            // 
            // chkAdduct_K
            // 
            this.chkAdduct_K.AutoSize = true;
            this.chkAdduct_K.Location = new System.Drawing.Point(348, 49);
            this.chkAdduct_K.Name = "chkAdduct_K";
            this.chkAdduct_K.Size = new System.Drawing.Size(33, 17);
            this.chkAdduct_K.TabIndex = 15;
            this.chkAdduct_K.Text = "K";
            this.chkAdduct_K.UseVisualStyleBackColor = true;
            // 
            // chkAdduct_Na
            // 
            this.chkAdduct_Na.AutoSize = true;
            this.chkAdduct_Na.Location = new System.Drawing.Point(348, 28);
            this.chkAdduct_Na.Name = "chkAdduct_Na";
            this.chkAdduct_Na.Size = new System.Drawing.Size(40, 17);
            this.chkAdduct_Na.TabIndex = 14;
            this.chkAdduct_Na.Text = "Na";
            this.chkAdduct_Na.UseVisualStyleBackColor = true;
            // 
            // chkAdduct_NH4
            // 
            this.chkAdduct_NH4.AutoSize = true;
            this.chkAdduct_NH4.Location = new System.Drawing.Point(348, 7);
            this.chkAdduct_NH4.Name = "chkAdduct_NH4";
            this.chkAdduct_NH4.Size = new System.Drawing.Size(48, 17);
            this.chkAdduct_NH4.TabIndex = 13;
            this.chkAdduct_NH4.Text = "NH4";
            this.chkAdduct_NH4.UseVisualStyleBackColor = true;
            // 
            // chkAdduct_H
            // 
            this.chkAdduct_H.AutoSize = true;
            this.chkAdduct_H.Checked = true;
            this.chkAdduct_H.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAdduct_H.Enabled = false;
            this.chkAdduct_H.Location = new System.Drawing.Point(194, 49);
            this.chkAdduct_H.Name = "chkAdduct_H";
            this.chkAdduct_H.Size = new System.Drawing.Size(57, 17);
            this.chkAdduct_H.TabIndex = 14;
            this.chkAdduct_H.Text = "Proton";
            this.chkAdduct_H.UseVisualStyleBackColor = true;
            // 
            // chkPermethylation
            // 
            this.chkPermethylation.AutoSize = true;
            this.chkPermethylation.Location = new System.Drawing.Point(194, 28);
            this.chkPermethylation.Name = "chkPermethylation";
            this.chkPermethylation.Size = new System.Drawing.Size(95, 17);
            this.chkPermethylation.TabIndex = 12;
            this.chkPermethylation.Text = "Permethylation";
            this.chkPermethylation.UseVisualStyleBackColor = true;
            // 
            // chkReducedReducingEnd
            // 
            this.chkReducedReducingEnd.AutoSize = true;
            this.chkReducedReducingEnd.Location = new System.Drawing.Point(194, 7);
            this.chkReducedReducingEnd.Name = "chkReducedReducingEnd";
            this.chkReducedReducingEnd.Size = new System.Drawing.Size(141, 17);
            this.chkReducedReducingEnd.TabIndex = 11;
            this.chkReducedReducingEnd.Text = "Reduced Reducing End";
            this.chkReducedReducingEnd.UseVisualStyleBackColor = true;
            // 
            // rdoDRAG
            // 
            this.rdoDRAG.AutoSize = true;
            this.rdoDRAG.Location = new System.Drawing.Point(505, 49);
            this.rdoDRAG.Name = "rdoDRAG";
            this.rdoDRAG.Size = new System.Drawing.Size(56, 17);
            this.rdoDRAG.TabIndex = 18;
            this.rdoDRAG.Text = "DRAG";
            this.rdoDRAG.UseVisualStyleBackColor = true;
            // 
            // rdoMultiplexPermethylation
            // 
            this.rdoMultiplexPermethylation.AutoSize = true;
            this.rdoMultiplexPermethylation.Location = new System.Drawing.Point(505, 28);
            this.rdoMultiplexPermethylation.Name = "rdoMultiplexPermethylation";
            this.rdoMultiplexPermethylation.Size = new System.Drawing.Size(138, 17);
            this.rdoMultiplexPermethylation.TabIndex = 17;
            this.rdoMultiplexPermethylation.Text = "Multiplex Permethylation";
            this.rdoMultiplexPermethylation.UseVisualStyleBackColor = true;
            // 
            // rdoLabelFree
            // 
            this.rdoLabelFree.AutoSize = true;
            this.rdoLabelFree.Checked = true;
            this.rdoLabelFree.Location = new System.Drawing.Point(505, 7);
            this.rdoLabelFree.Name = "rdoLabelFree";
            this.rdoLabelFree.Size = new System.Drawing.Size(75, 17);
            this.rdoLabelFree.TabIndex = 16;
            this.rdoLabelFree.TabStop = true;
            this.rdoLabelFree.Text = "Label-Free";
            this.rdoLabelFree.UseVisualStyleBackColor = true;
            // 
            // txtHex
            // 
            this.txtHex.Location = new System.Drawing.Point(56, 40);
            this.txtHex.Name = "txtHex";
            this.txtHex.Size = new System.Drawing.Size(32, 20);
            this.txtHex.TabIndex = 8;
            // 
            // txtHexNAc
            // 
            this.txtHexNAc.Location = new System.Drawing.Point(56, 12);
            this.txtHexNAc.Name = "txtHexNAc";
            this.txtHexNAc.Size = new System.Drawing.Size(32, 20);
            this.txtHexNAc.TabIndex = 7;
            // 
            // txtSia
            // 
            this.txtSia.Location = new System.Drawing.Point(135, 40);
            this.txtSia.Name = "txtSia";
            this.txtSia.Size = new System.Drawing.Size(44, 20);
            this.txtSia.TabIndex = 10;
            // 
            // txtFuc
            // 
            this.txtFuc.Location = new System.Drawing.Point(135, 12);
            this.txtFuc.Name = "txtFuc";
            this.txtFuc.Size = new System.Drawing.Size(44, 20);
            this.txtFuc.TabIndex = 9;
            // 
            // btnGlycan
            // 
            this.btnGlycan.Location = new System.Drawing.Point(763, 12);
            this.btnGlycan.Name = "btnGlycan";
            this.btnGlycan.Size = new System.Drawing.Size(75, 23);
            this.btnGlycan.TabIndex = 20;
            this.btnGlycan.Text = "Generate";
            this.btnGlycan.UseVisualStyleBackColor = true;
            this.btnGlycan.Click += new System.EventHandler(this.btnGlycan_Click);
            // 
            // lblSia
            // 
            this.lblSia.AutoSize = true;
            this.lblSia.Location = new System.Drawing.Point(94, 44);
            this.lblSia.Name = "lblSia";
            this.lblSia.Size = new System.Drawing.Size(40, 13);
            this.lblSia.TabIndex = 3;
            this.lblSia.Text = "NeuAc";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(24, 44);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(26, 13);
            this.label7.TabIndex = 2;
            this.label7.Text = "Hex";
            // 
            // labelFuc
            // 
            this.labelFuc.AutoSize = true;
            this.labelFuc.Location = new System.Drawing.Point(96, 16);
            this.labelFuc.Name = "labelFuc";
            this.labelFuc.Size = new System.Drawing.Size(38, 13);
            this.labelFuc.TabIndex = 1;
            this.labelFuc.Text = "deHex";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "HexNAc";
            // 
            // dgTable
            // 
            this.dgTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgTable.Location = new System.Drawing.Point(0, 0);
            this.dgTable.Name = "dgTable";
            this.dgTable.Size = new System.Drawing.Size(877, 426);
            this.dgTable.TabIndex = 0;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.button2);
            this.tabPage5.Controls.Add(this.label10);
            this.tabPage5.Controls.Add(this.cboPepMutation);
            this.tabPage5.Controls.Add(this.chkEnzy_GlucE);
            this.tabPage5.Controls.Add(this.chkEnzy_Trypsin);
            this.tabPage5.Controls.Add(this.chkEnzy_GlucED);
            this.tabPage5.Controls.Add(this.chkEnzy_None);
            this.tabPage5.Controls.Add(this.label9);
            this.tabPage5.Controls.Add(this.txtMissCleavage);
            this.tabPage5.Controls.Add(this.label8);
            this.tabPage5.Controls.Add(this.txtPeptides);
            this.tabPage5.Controls.Add(this.chkCYS_CAM);
            this.tabPage5.Controls.Add(this.btnReadFasta);
            this.tabPage5.Controls.Add(this.lblPeptideMass);
            this.tabPage5.Controls.Add(this.btnGetPeptideMass);
            this.tabPage5.Controls.Add(this.label6);
            this.tabPage5.Controls.Add(this.txtPeptideSeq);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(877, 508);
            this.tabPage5.TabIndex = 2;
            this.tabPage5.Text = "Peptide";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(17, 185);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(113, 13);
            this.label10.TabIndex = 36;
            this.label10.Text = "Allow peptide mutation";
            this.label10.Click += new System.EventHandler(this.label10_Click);
            // 
            // cboPepMutation
            // 
            this.cboPepMutation.FormattingEnabled = true;
            this.cboPepMutation.Items.AddRange(new object[] {
            "No Mutation",
            "Aspartic Acid(D) -> Asparagine (N)",
            "Any -> Asparagine (N)"});
            this.cboPepMutation.Location = new System.Drawing.Point(137, 182);
            this.cboPepMutation.Name = "cboPepMutation";
            this.cboPepMutation.Size = new System.Drawing.Size(202, 21);
            this.cboPepMutation.TabIndex = 35;
            this.cboPepMutation.SelectedIndexChanged += new System.EventHandler(this.cboPepMutation_SelectedIndexChanged);
            // 
            // chkEnzy_GlucE
            // 
            this.chkEnzy_GlucE.AutoSize = true;
            this.chkEnzy_GlucE.Location = new System.Drawing.Point(639, 180);
            this.chkEnzy_GlucE.Name = "chkEnzy_GlucE";
            this.chkEnzy_GlucE.Size = new System.Drawing.Size(65, 17);
            this.chkEnzy_GlucE.TabIndex = 34;
            this.chkEnzy_GlucE.Text = "GluC (E)";
            this.chkEnzy_GlucE.UseVisualStyleBackColor = true;
            this.chkEnzy_GlucE.CheckedChanged += new System.EventHandler(this.chkEnzy_GlucE_CheckedChanged);
            // 
            // chkEnzy_Trypsin
            // 
            this.chkEnzy_Trypsin.AutoSize = true;
            this.chkEnzy_Trypsin.Location = new System.Drawing.Point(710, 180);
            this.chkEnzy_Trypsin.Name = "chkEnzy_Trypsin";
            this.chkEnzy_Trypsin.Size = new System.Drawing.Size(60, 17);
            this.chkEnzy_Trypsin.TabIndex = 33;
            this.chkEnzy_Trypsin.Text = "Trypsin";
            this.chkEnzy_Trypsin.UseVisualStyleBackColor = true;
            this.chkEnzy_Trypsin.CheckedChanged += new System.EventHandler(this.chkEnzy_Trypsin_CheckedChanged);
            // 
            // chkEnzy_GlucED
            // 
            this.chkEnzy_GlucED.AutoSize = true;
            this.chkEnzy_GlucED.Location = new System.Drawing.Point(782, 180);
            this.chkEnzy_GlucED.Name = "chkEnzy_GlucED";
            this.chkEnzy_GlucED.Size = new System.Drawing.Size(73, 17);
            this.chkEnzy_GlucED.TabIndex = 32;
            this.chkEnzy_GlucED.Text = "GluC (ED)";
            this.chkEnzy_GlucED.UseVisualStyleBackColor = true;
            this.chkEnzy_GlucED.CheckedChanged += new System.EventHandler(this.chkEnzy_GlucED_CheckedChanged);
            // 
            // chkEnzy_None
            // 
            this.chkEnzy_None.AutoSize = true;
            this.chkEnzy_None.Checked = true;
            this.chkEnzy_None.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkEnzy_None.Location = new System.Drawing.Point(581, 180);
            this.chkEnzy_None.Name = "chkEnzy_None";
            this.chkEnzy_None.Size = new System.Drawing.Size(52, 17);
            this.chkEnzy_None.TabIndex = 31;
            this.chkEnzy_None.Text = "None";
            this.chkEnzy_None.UseVisualStyleBackColor = true;
            this.chkEnzy_None.CheckedChanged += new System.EventHandler(this.chkEnzy_None_CheckedChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Enabled = false;
            this.label9.Location = new System.Drawing.Point(520, 182);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(44, 13);
            this.label9.TabIndex = 30;
            this.label9.Text = "Enzyme";
            // 
            // txtMissCleavage
            // 
            this.txtMissCleavage.Location = new System.Drawing.Point(178, 147);
            this.txtMissCleavage.Name = "txtMissCleavage";
            this.txtMissCleavage.Size = new System.Drawing.Size(37, 20);
            this.txtMissCleavage.TabIndex = 8;
            this.txtMissCleavage.Text = "2";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(89, 152);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(83, 13);
            this.label8.TabIndex = 7;
            this.label8.Text = "Miss cleavages:";
            // 
            // txtPeptides
            // 
            this.txtPeptides.Location = new System.Drawing.Point(8, 211);
            this.txtPeptides.Multiline = true;
            this.txtPeptides.Name = "txtPeptides";
            this.txtPeptides.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtPeptides.Size = new System.Drawing.Size(848, 289);
            this.txtPeptides.TabIndex = 6;
            // 
            // chkCYS_CAM
            // 
            this.chkCYS_CAM.AutoSize = true;
            this.chkCYS_CAM.Location = new System.Drawing.Point(31, 67);
            this.chkCYS_CAM.Name = "chkCYS_CAM";
            this.chkCYS_CAM.Size = new System.Drawing.Size(76, 17);
            this.chkCYS_CAM.TabIndex = 5;
            this.chkCYS_CAM.Text = "CYS_CAM";
            this.chkCYS_CAM.UseVisualStyleBackColor = true;
            // 
            // btnReadFasta
            // 
            this.btnReadFasta.Location = new System.Drawing.Point(8, 147);
            this.btnReadFasta.Name = "btnReadFasta";
            this.btnReadFasta.Size = new System.Drawing.Size(75, 23);
            this.btnReadFasta.TabIndex = 4;
            this.btnReadFasta.Text = "ReadFasta";
            this.btnReadFasta.UseVisualStyleBackColor = true;
            this.btnReadFasta.Click += new System.EventHandler(this.btnReadFasta_Click);
            // 
            // lblPeptideMass
            // 
            this.lblPeptideMass.AutoSize = true;
            this.lblPeptideMass.Location = new System.Drawing.Point(419, 30);
            this.lblPeptideMass.Name = "lblPeptideMass";
            this.lblPeptideMass.Size = new System.Drawing.Size(74, 13);
            this.lblPeptideMass.TabIndex = 3;
            this.lblPeptideMass.Text = "Peptide Mass:";
            // 
            // btnGetPeptideMass
            // 
            this.btnGetPeptideMass.Location = new System.Drawing.Point(304, 21);
            this.btnGetPeptideMass.Name = "btnGetPeptideMass";
            this.btnGetPeptideMass.Size = new System.Drawing.Size(75, 37);
            this.btnGetPeptideMass.TabIndex = 2;
            this.btnGetPeptideMass.Text = "Peptide Weight";
            this.btnGetPeptideMass.UseVisualStyleBackColor = true;
            this.btnGetPeptideMass.Click += new System.EventHandler(this.btnGetPeptideMass_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(28, 34);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(68, 13);
            this.label6.TabIndex = 1;
            this.label6.Text = "Peptide Seq:";
            // 
            // txtPeptideSeq
            // 
            this.txtPeptideSeq.Location = new System.Drawing.Point(99, 30);
            this.txtPeptideSeq.Name = "txtPeptideSeq";
            this.txtPeptideSeq.Size = new System.Drawing.Size(199, 20);
            this.txtPeptideSeq.TabIndex = 0;
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.picGlycan);
            this.tabPage6.Controls.Add(this.btnGlycanDraw);
            this.tabPage6.Controls.Add(this.txtIUPAC);
            this.tabPage6.Location = new System.Drawing.Point(4, 22);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage6.Size = new System.Drawing.Size(877, 508);
            this.tabPage6.TabIndex = 3;
            this.tabPage6.Text = "GlycanDraw";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // picGlycan
            // 
            this.picGlycan.Location = new System.Drawing.Point(6, 51);
            this.picGlycan.Name = "picGlycan";
            this.picGlycan.Size = new System.Drawing.Size(440, 340);
            this.picGlycan.TabIndex = 2;
            this.picGlycan.TabStop = false;
            // 
            // btnGlycanDraw
            // 
            this.btnGlycanDraw.Location = new System.Drawing.Point(452, 22);
            this.btnGlycanDraw.Name = "btnGlycanDraw";
            this.btnGlycanDraw.Size = new System.Drawing.Size(75, 23);
            this.btnGlycanDraw.TabIndex = 1;
            this.btnGlycanDraw.Text = "Draw";
            this.btnGlycanDraw.UseVisualStyleBackColor = true;
            this.btnGlycanDraw.Click += new System.EventHandler(this.btnGlycanDraw_Click);
            // 
            // txtIUPAC
            // 
            this.txtIUPAC.Location = new System.Drawing.Point(8, 25);
            this.txtIUPAC.Name = "txtIUPAC";
            this.txtIUPAC.Size = new System.Drawing.Size(438, 20);
            this.txtIUPAC.TabIndex = 0;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(780, 24);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(885, 534);
            this.Controls.Add(this.tabControl1);
            this.Name = "frmMain";
            this.Text = "Form1";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.tabControl2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgTable)).EndInit();
            this.tabPage5.ResumeLayout(false);
            this.tabPage5.PerformLayout();
            this.tabPage6.ResumeLayout(false);
            this.tabPage6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picGlycan)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TextBox txtPPM_CSMSL;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnForward_CSMSL;
        private System.Windows.Forms.Button btnBackward_CSMSL;
        private System.Windows.Forms.Button btnRead_CSMSL;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPeaks_CSMSL;
        private System.Windows.Forms.TextBox txtScanNo_CSMSL;
        private System.Windows.Forms.Button btnLoadRawCSMSL;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label lblRawInfo;
        private System.Windows.Forms.TextBox txtPeak;
        private System.Windows.Forms.TextBox txtCharge;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtTargetMZ;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnGetPeaks;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.CheckBox chkLoadFile;
        private System.Windows.Forms.CheckBox chkAdduct_K;
        private System.Windows.Forms.CheckBox chkAdduct_Na;
        private System.Windows.Forms.CheckBox chkAdduct_NH4;
        private System.Windows.Forms.CheckBox chkAdduct_H;
        private System.Windows.Forms.CheckBox chkPermethylation;
        private System.Windows.Forms.CheckBox chkReducedReducingEnd;
        private System.Windows.Forms.RadioButton rdoDRAG;
        private System.Windows.Forms.RadioButton rdoMultiplexPermethylation;
        private System.Windows.Forms.RadioButton rdoLabelFree;
        private System.Windows.Forms.TextBox txtHex;
        private System.Windows.Forms.TextBox txtHexNAc;
        private System.Windows.Forms.TextBox txtSia;
        private System.Windows.Forms.TextBox txtFuc;
        private System.Windows.Forms.Button btnGlycan;
        private System.Windows.Forms.Label lblSia;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label labelFuc;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView dgTable;
        private System.Windows.Forms.TextBox txtAdduct_user;
        private System.Windows.Forms.CheckBox chkAdduct_user;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private ZedGraph.ZedGraphControl zedGraphControl1;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.Label lblPeptideMass;
        private System.Windows.Forms.Button btnGetPeptideMass;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtPeptideSeq;
        private System.Windows.Forms.Button btnReadFasta;
        private System.Windows.Forms.CheckBox chkCYS_CAM;
        private System.Windows.Forms.TextBox txtPeptides;
        private System.Windows.Forms.TextBox txtMissCleavage;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox chkEnzy_GlucE;
        private System.Windows.Forms.CheckBox chkEnzy_Trypsin;
        private System.Windows.Forms.CheckBox chkEnzy_GlucED;
        private System.Windows.Forms.CheckBox chkEnzy_None;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cboPepMutation;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.PictureBox picGlycan;
        private System.Windows.Forms.Button btnGlycanDraw;
        private System.Windows.Forms.TextBox txtIUPAC;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox chkChargeMode;
        private System.Windows.Forms.Button button2;
    }
}

