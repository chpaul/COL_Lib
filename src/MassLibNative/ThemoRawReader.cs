using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MSFileReaderLib;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace COL.MassLib
{


    public class ThermoRawReader : IRawFileReader,IDisposable
    {

        bool disposed = false;
        private string _fullFilePath;
        private float tolPPM = 30;
        private float tolSN = 2;
        private float tolMinPeakCount;
        private enum RawLabelDataColumn
        {
            MZ = 0,
            Intensity = 1,
            Resolution = 2,
            NoiseBaseline = 3,
            NoiseLevel = 4,
            Charge = 5
        }

        public enum ThermoMzAnalyzer
        {
            None = -1,
            ITMS = 0,
            TQMS = 1,
            SQMS = 2,
            TOFMS = 3,
            FTMS = 4,
            Sector = 5
        }

        public enum Smoothing
        {
            None = 0,
            Boxcar = 1,
            Gauusian = 2
        }
        private IXRawfile5 _rawConnection;

        public ThermoRawReader(string argFullPath)
        {
            
            //Check if XRawfle2.dll exist
            CheckDLL();



            _fullFilePath = argFullPath;
            //Open
            _rawConnection = (IXRawfile5)new MSFileReader_XRawfile();
            _rawConnection.Open(_fullFilePath);
            _rawConnection.SetCurrentController(0, 1); // first 0 is for mass spectrometer
        }
        public int NumberOfScans
        {
            get
            {
                int spectrumNumber = 0;
                _rawConnection.GetLastSpectrumNumber(ref spectrumNumber);
                return spectrumNumber;
            }
        }
        public int GetMsLevel(int argScan)
        {
            int msnOrder = 0;
            _rawConnection.GetMSOrderForScanNum(argScan, ref msnOrder);
            return msnOrder;
        }

        public double GetRetentionTime(int argScanNo)
        {
            double retentionTime = 0;
            _rawConnection.RTFromScanNum(argScanNo, ref retentionTime);
            return retentionTime;
        }
        public string RawFilePath
        {
            get { return _fullFilePath; }
        }
        public bool IsCIDScan(int argScanNum)
        {
            string filter = GetScanDescription(argScanNum);
            if (filter.ToLower().Contains("cid"))
            {
                return true;
            }
            return false;
        }
        public bool IsHCDScan(int argScanNum)
        {
            string filter = GetScanDescription(argScanNum);
            if (filter.ToLower().Contains("hcd"))
            {
                return true;
            }
            return false;
        }
        public bool IsFTScan(int argScanNum)
        {
            string filter = GetScanDescription(argScanNum);
            if (filter.ToLower().Contains("ms2"))
            {
                return false;
            }
            if (filter.ToLower().Contains("ft"))
            {
                return true;
            }
            return false;
        }
        public float PPM
        {
            get { return tolPPM; }
            set { tolPPM = value; }
        }
        public float SN
        {
            get { return tolSN; }
            set { tolSN = value; }
        }
        public string GetScanDescription(int argScanNum)
        {
            string filter = null;
            _rawConnection.GetFilterForScanNum(argScanNum, ref filter);
            return filter;
        }
        private MSScan GetScanFromFile(int argScanNo, float argMinSN = 2)//, float argPPM = 6, int argMinPeakCount=3)
        {


            int mslevel = GetMsLevel(argScanNo);
            tolSN = argMinSN;
            MSScan scan = new MSScan(argScanNo);
            scan.MsLevel = GetMsLevel(argScanNo);
            scan.Time = GetRetentionTime(argScanNo);
            scan.ScanHeader =  GetScanDescription(argScanNo);

            

            double[,] peakData;
                if (GetMzAnalyzer(argScanNo) == ThermoMzAnalyzer.ITMS) //Low Res
                {
                    object massList = null;
                    object peakFlags = null;
                    int arraySize = -1;
                    double centroidPeakWidth = double.NaN;
                    _rawConnection.GetMassListFromScanNum(ref argScanNo, null, 0, 0, 0,1, ref centroidPeakWidth, ref massList, ref peakFlags, ref arraySize);
                    peakData = (double[,])massList;                
                }
                else
                {
                    object labels = null;
                    object flags = null;
                    _rawConnection.GetLabelData(ref labels, ref flags, ref argScanNo);
                    peakData = (double[,])labels;
                }
                scan.RawMZs = new float[peakData.GetLength(1)];
                scan.RawIntensities = new float[peakData.GetLength(1)];
            if (mslevel == 1)
            {
                List<float> MzsAboveSN = new List<float>();
                List<float> IntensitysAboveSN = new List<float>();
                for (int i = 0; i < peakData.GetLength(1); i++)
                {
                    scan.RawMZs[i] = Convert.ToSingle(peakData[(int) RawLabelDataColumn.MZ, i]);
                    scan.RawIntensities[i] = Convert.ToSingle(peakData[(int) RawLabelDataColumn.Intensity, i]);
                    double Noise = peakData[(int) RawLabelDataColumn.NoiseLevel, i];
                    double SN = 0;
                    if (Noise.Equals(0))
                    {
                        SN = float.NaN;
                    }
                    else
                    {
                        SN = scan.RawIntensities[i]/Noise;
                    }
                    if (SN >= tolSN)
                    {
                        MzsAboveSN.Add(scan.RawMZs[i]);
                        IntensitysAboveSN.Add(scan.RawIntensities[i]);
                    }
                }
                scan.MZs = MzsAboveSN.ToArray();
                scan.Intensities = IntensitysAboveSN.ToArray();
            }
            else // MS/MS
            {
                scan.MZs = new float[peakData.GetLength(1)];
                scan.Intensities = new float[peakData.GetLength(1)];
                for (int i = 0; i < peakData.GetLength(1); i++)
                {
                    scan.MZs[i] = Convert.ToSingle(peakData[(int) RawLabelDataColumn.MZ, i]);
                    scan.Intensities[i] = Convert.ToSingle(peakData[(int) RawLabelDataColumn.Intensity, i]);
                }
                scan.ParentScanNo = GetParentScanNumber(argScanNo);
                scan.ParentMZ = GetPrecusorMz(argScanNo);
                scan.ParentCharge = GetPrecusorCharge(argScanNo);
                
                //Sometime get wrong value
                //scan.ParentMonoMz = Convert.ToSingle(GetExtraValue(argScanNo, "Monoisotopic M/Z:"));
                //Find in raw data
                scan.ParentMonoMz = GetPrecusorMz(argScanNo);
                MSScan parentScan = GetScanFromFile(scan.ParentScanNo);
                float peakBefore = scan.ParentMZ - (1/(float) scan.ParentCharge);
                float foundPeak = parentScan.RawMZs.OrderBy(x => Math.Abs(x - peakBefore)).ToList()[0];
                while (MassUtility.GetMassPPM(foundPeak, peakBefore) <= 10) //10PPM
                {
                    //Find another
                    scan.ParentMonoMz = foundPeak;
                    peakBefore = foundPeak - (1 / (float)scan.ParentCharge);
                    foundPeak = parentScan.MZs.OrderBy(x => Math.Abs(x - peakBefore)).ToList()[0];
                }

            }

       
            scan.IsCIDScan = IsCIDScan(argScanNo);
            scan.IsHCDScan = IsHCDScan(argScanNo);
            scan.IsFTScan = IsFTScan(argScanNo);
            return scan;
        }

        public MSScan ReadScan(int argScanNo)
        {
            return GetScanFromFile(argScanNo, 2);
        }
        public MSScan ReadScan(int argScanNo, float argSN)
        {
            return GetScanFromFile(argScanNo, argSN);
        }
        public List<MSScan> ReadScans(int argStart, int argEnd)
        {
            List<MSScan> scans = new List<MSScan>();
            if (argStart <= 0)
            {
                argStart = 1;
            }
            if (argEnd > this.NumberOfScans)
            {
                argEnd = this.NumberOfScans;
            }
            for (int i = argStart; i <= argEnd; i++)
            {
                scans.Add(ReadScan(i));
            }
            return scans;
        }
        public List<MSScan> ReadAllScans()
        {
            int EndScan = this.NumberOfScans;
            return ReadScans(1, EndScan);
        }

        public int GetParentScanNumber(int argScanNum)
        {
            if (GetMsLevel(argScanNum) == 1)
                return 0;

            
            object parentScanNumber = GetExtraValue(argScanNum, "Master Scan Number:");
            int scanNumber = Convert.ToInt32(parentScanNumber);
            
            if (scanNumber == 0)
            {
                int masterIndex = Convert.ToInt32(GetExtraValue(argScanNum, "Master Index:"));
                if (masterIndex == 0)
                    throw new ArgumentException("Scan Number " + argScanNum + " has no parent");

                for (int i = argScanNum - 1; i >= 1; i--)
                {
                    int currentScanEvent = Convert.ToInt32(GetExtraValue(i, "Scan Event:"));
                    if (currentScanEvent == masterIndex)
                    {
                        scanNumber = i;
                        break;
                    }
                }
                //int scanIndex = Convert.ToInt32(GetExtraValue(argScanNum, "Scan Event:"));
                //scanNumber = argScanNum - scanIndex + masterIndex;
            }
            return scanNumber;
        }
        public  float GetPrecusorMz(int argScanNum, int argMsLevel = 2)
        {
            double mz = double.NaN;
            _rawConnection.GetPrecursorMassForScanNum(argScanNum, argMsLevel, ref mz);
            return Convert.ToSingle(mz);
        }
        public int GetPrecusorCharge(int argScanNum, int argMSLevel = 2)
        {
            return Convert.ToInt16(GetExtraValue(argScanNum, "Charge State:"));
        }

        public HCDInfo GetHCDInfo(int argScanNum)
        {
            HCDScoring Scoring = new HCDScoring();
            MSScan scan = ReadScan(argScanNum);

            double HCDScore = Scoring.CalculateHCDScore(scan.MSPeaks);
            enumGlycanType GType = Scoring.DetermineGlycanType(scan.MSPeaks);
            HCDInfo HInfo = new HCDInfo(argScanNum, GType, HCDScore);
            return HInfo;
        }
        public List<MSScan> ReadScanWMSLevel(int argStart, int argEnd, int argMSLevel)
        {
            List<MSScan> scans = new List<MSScan>();
            if (argStart <= 0)
            {
                argStart = 1;
            }
            if (argEnd > this.NumberOfScans)
            {
                argEnd = this.NumberOfScans;
            }
            for (int i = argStart; i <= argEnd; i++)
            {
                MSScan scan = ReadScan(i);
                if (scan.MsLevel == argMSLevel)
                {
                    scans.Add(scan);
                }
            }
            return scans;
        }
        public object GetExtraValue(int argScanNum, string filter)
        {
            object value = null;
            _rawConnection.GetTrailerExtraValueForScanNum(argScanNum, filter, ref value);
            return value;
        }
        public string[] GetTrailerExtraLabelArray(int argScanNum)
        {
            object arraylist = null;
            int arraySize = 0;
            _rawConnection.GetTrailerExtraLabelsForScanNum(argScanNum, ref arraylist, ref arraySize);
            return (string[])arraylist;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        // Protected implementation of Dispose pattern. 
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;
            if (disposing)
            {
                // Free any other managed objects here. 
                _rawConnection.Close();
            }
            // Free any unmanaged objects here. 
            disposed = true;
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

   
        private void CheckDLL()
        {
            //Store resource file into disk
            Assembly assembly = Assembly.GetExecutingAssembly();
            string RunningFolder = Environment.CurrentDirectory;
            string DllLocation = Environment.GetEnvironmentVariable("ProgramFiles");
            
            if (Environment.Is64BitOperatingSystem)
            {
                DllLocation = Environment.GetEnvironmentVariable("ProgramFiles(x86)");
            }
            DllLocation += "\\Thermo\\MSFileReader";

            List<string> lstFiles = new List<string>();
            lstFiles.Add( "XRawfile2.dll");
            lstFiles.Add("fileio.dll");
            lstFiles.Add("fregistry.dll");

            foreach (string strFileName in lstFiles)
            {
                //if (!File.Exists(RunningFolder + "\\" + strFileName))
                //{
                //    Stream input = assembly.GetManifestResourceStream("COL.MassLib.Resources."+strFileName);
                //    Stream output = File.Create(RunningFolder + "\\" + strFileName);
                //    input.CopyTo(output);
                //    output.Close();
                //}
                if (!File.Exists(DllLocation + "\\" + strFileName ))
                {
                    MessageBox.Show(strFileName + " not found, program will copy and install to Program File Folder");
                    File.Copy(RunningFolder + "\\" + strFileName , DllLocation + "\\" + strFileName );
                    Registar_Dlls(DllLocation + "\\" + strFileName );
                }
            }
        }
        public static void Registar_Dlls(string filePath)
        {
            try
            {
                //'/s' : Specifies regsvr32 to run silently and to not display any message boxes.
                string fileinfo = "/s" + " " + "\"" + filePath + "\"";
                Process reg = new Process();
                //This file registers .dll files as command components in the registry.
                reg.StartInfo.FileName = "regsvr32.exe";
                reg.StartInfo.Arguments = fileinfo;
                reg.StartInfo.UseShellExecute = false;
                reg.StartInfo.CreateNoWindow = true;
                reg.StartInfo.RedirectStandardOutput = true;
                reg.Start();
                reg.WaitForExit();
                reg.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public ThermoMzAnalyzer GetMzAnalyzer(int spectrumNumber)
        {
            int mzanalyzer = 0;
            _rawConnection.GetMassAnalyzerTypeForScanNum(spectrumNumber, ref mzanalyzer);
            return (ThermoMzAnalyzer) mzanalyzer;
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct PrecursorInfo
        {
            [MarshalAs(UnmanagedType.I4)]
            public int scannumber;
             [MarshalAs(UnmanagedType.R8)]
            public double isolationmass;
             [MarshalAs(UnmanagedType.I4)]
             public int chargestate;
            [MarshalAs(UnmanagedType.R8)]
             public double monoisotopicmass;        
        }
    }
}
