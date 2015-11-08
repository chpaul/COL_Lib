using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using CSMSL;
using CSMSL.Spectral;
using CSMSL.IO.Thermo;
namespace COL.MassLib
{
    public class ThermoRawReader : IRawFileReader,IDisposable
    {
        bool disposed = false;
        private string _fullFilePath;
        private float tolPPM = 30;
        private float tolSN =2;
        private float tolMinPeakCount;
        ThermoRawFile _raw;
       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="argFullPath"></param>
        /// <param name="argFileType">ThermoRawFile</param>
        public ThermoRawReader(string argFullPath)
        {
            _fullFilePath = argFullPath;
            _raw = new CSMSL.IO.Thermo.ThermoRawFile(_fullFilePath);
            _raw.Open();
        }
        public int NumberOfScans
        {
            get
            {
                return _raw.LastSpectrumNumber;
            }
        }
        public int GetMsLevel(int argScan)
        {
            return _raw.GetMsnOrder(argScan);           
        }
        public string RawFilePath
        {
            get { return _fullFilePath; }
        }
        public bool IsCIDScan(int argScanNum)
        {            
           if (_raw.GetScanFilter(argScanNum).ToLower().Contains("cid"))
            {
                return true;
            }
            return false;
        }
        public bool IsHCDScan(int argScanNum)
        {
            if (_raw.GetScanFilter(argScanNum).ToLower().Contains("hcd"))
            {
                return true;
            }
            return false;
        }
        public bool IsFTScan(int argScanNum)
        {
            if (_raw.GetScanFilter(argScanNum).ToLower().Contains("ms2"))
            {
                return false;
            }
            if (_raw.GetScanFilter(argScanNum).ToLower().Contains("ft"))
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
           return _raw.GetScanFilter(argScanNum);
        }
        //private MSScan GetScanFromFile(int argScanNo)
        //{
        //    return GetScanFromFile(argScanNo);
        //}
        private MSScan GetScanFromFile(int argScanNo, float argMinSN = 2)//, float argPPM = 6, int argMinPeakCount=3)
        {
            MSDataScan DataScan = _raw.GetMsScan(argScanNo);
            tolSN = argMinSN;
            //tolMinPeakCount = argMinPeakCount;
            //tolPPM = argPPM;
            MZPeak[] peaks;
            List<ThermoLabeledPeak> TPeaks = new List<ThermoLabeledPeak>();
            peaks = DataScan.MassSpectrum.GetPeaks();
            
            //Start Read Scan                    
            MSScan  scan = new MSScan(argScanNo);
            scan.MsLevel = GetMsLevel(argScanNo);
            scan.Time = _raw.GetRetentionTime(argScanNo);
            scan.ScanHeader = _raw.GetScanFilter(argScanNo);


            List<MSPeak> MSPsks = new List<MSPeak>();

            #region MSScan
            if (scan.MsLevel == 1)
            {
                float[] RawMz = new float[peaks.Length];
                float[] RawIntensity = new float[peaks.Length];

                for (int i = 0; i < peaks.Length; i++)
                {
                    RawMz[i] = Convert.ToSingle(peaks[i].MZ);
                    RawIntensity[i] = Convert.ToSingle(peaks[i].Intensity);
                    ThermoLabeledPeak TPeak = (ThermoLabeledPeak)peaks[i];
                    if ( TPeak.SN >= tolSN ) 
                    {
                        TPeaks.Add(TPeak);
                    }
                }
                float[] Mz = new float[TPeaks.Count];
                float[] Intensity = new float[TPeaks.Count];
                for (int i = 0; i < TPeaks.Count; i++)
                {
                    Mz[i] = Convert.ToSingle(TPeaks[i].MZ);
                    Intensity[i] = Convert.ToSingle(TPeaks[i].Intensity);
                }
                scan.RawMZs = RawMz;
                scan.RawIntensities = RawIntensity;
                scan.MZs = Mz;
                scan.Intensities = Intensity;
                //************************Peak Picking
                //scan.MSPeaks = MSPsks;
                //List<float> TPeaksMZ = new List<float>();
                //for (int i = 0; i < TPeaks.Count; i++)
                //{
                //    TPeaksMZ.Add(Convert.ToSingle(TPeaks[i].MZ));
                //}
                //do
                //{
                //    ThermoLabeledPeak BasePeak = TPeaks[0];
                //    List<ThermoLabeledPeak> clusterPeaks = new List<ThermoLabeledPeak>();
                //    List<int> RemoveIdx = new List<int>();
                //    RemoveIdx.Add(0);
                //    clusterPeaks.Add(BasePeak);
                //    double Interval = 1 / (double)BasePeak.Charge;
                //    double FirstMZ = BasePeak.MZ;
                //    for (int i = 1; i < TPeaks.Count; i++)
                //    {
                //        if (TPeaks[i].MZ - FirstMZ > Interval * 10)
                //        {
                //            break;
                //        }

                //        List<int> ClosePeaks = MassUtility.GetClosestMassIdxsWithinPPM(TPeaksMZ.ToArray(), Convert.ToSingle(BasePeak.MZ + Interval), argPPM);
                //        if (ClosePeaks.Count == 1 && TPeaks[ClosePeaks[0]].Charge == BasePeak.Charge)
                //        {
                //            BasePeak = TPeaks[i];
                //            clusterPeaks.Add(TPeaks[i]);
                //            RemoveIdx.Add(i);
                //        }
                //        else if (ClosePeaks.Count > 1)
                //        {
                //            double minPPM = 100;
                //            int minPPMIdx = 0;
                //            float maxIntensity = 0;
                //            int maxIntensityIdx = 0;
                //            for (int j = 0; j < ClosePeaks.Count; j++)
                //            {
                //                if (MassUtility.GetMassPPM(BasePeak.MZ + Interval, mz[ClosePeaks[j]]) < minPPM)
                //                {
                //                    minPPMIdx = ClosePeaks[j];
                //                    minPPM = MassUtility.GetMassPPM(BasePeak.MZ + Interval, mz[ClosePeaks[j]]);
                //                }
                //                if (intensity[ClosePeaks[j]] > maxIntensity)
                //                {
                //                    maxIntensity = intensity[ClosePeaks[j]];
                //                    maxIntensityIdx = ClosePeaks[j];
                //                }
                //            }
                //            BasePeak = TPeaks[minPPMIdx];
                //            clusterPeaks.Add(TPeaks[minPPMIdx]);
                //            RemoveIdx.Add(minPPMIdx);
                //        }
                //    }
                //    if (clusterPeaks.Count < argMinPeakCount)
                //    {
                //        TPeaks.RemoveAt(RemoveIdx[0]);
                //        TPeaksMZ.RemoveAt(RemoveIdx[0]);
                //    }
                //    else
                //    {
                //        float MostIntenseMZ = 0.0f;
                //        double MostIntenseIntneisty = 0;
                //        double ClusterIntensity = 0;
                //        RemoveIdx.Reverse();
                //        for (int i = 0; i < RemoveIdx.Count; i++)
                //        {
                //            if (TPeaks[RemoveIdx[i]].Intensity > MostIntenseIntneisty)
                //            {
                //                MostIntenseIntneisty = TPeaks[RemoveIdx[i]].Intensity;
                //                MostIntenseMZ = Convert.ToSingle(TPeaks[RemoveIdx[i]].MZ);
                //            }
                //            ClusterIntensity = ClusterIntensity + TPeaks[RemoveIdx[i]].Intensity;
                //            TPeaks.RemoveAt(RemoveIdx[i]);
                //            TPeaksMZ.RemoveAt(RemoveIdx[i]);
                //        }
                //        scan.MSPeaks.Add(new MSPeak(
                //                                                                 Convert.ToSingle(clusterPeaks[0].MZ),
                //                                                                 Convert.ToSingle(clusterPeaks[0].Intensity),
                //                                                                 clusterPeaks[0].Charge,
                //                                                                 Convert.ToSingle(clusterPeaks[0].MZ),
                //                                                                 Convert.ToSingle(clusterPeaks[0].SN),
                //                                                                 MostIntenseMZ,
                //                                                                 MostIntenseIntneisty,
                //                                                                 ClusterIntensity));

                //    }
                //} while (TPeaks.Count != 0);

            }
            #endregion
            #region MS/MS Scan
            else
            {
                float[] mz = new float[peaks.Length];
                float[] intensity = new float[peaks.Length];
                for (int i = 0; i < peaks.Length; i++)
                {
                    mz[i] = Convert.ToSingle(peaks[i].MZ);
                    intensity[i] = Convert.ToSingle(peaks[i].Intensity);
                    //MSPsks.Add(new MSPeak(mz[i], intensity[i]));
                }
                scan.MZs = mz;
                scan.Intensities = intensity;
                //scan.MSPeaks = MSPsks;
               // Get parent information

                scan.ParentScanNo = _raw.GetParentSpectrumNumber(argScanNo);
                scan.ParentMZ = Convert.ToSingle(_raw.GetPrecusorMz(argScanNo));
                scan.ParentCharge = _raw.GetPrecusorCharge(argScanNo);

                //Parent Mono
                if (scan.ParentScanNo != 0)
                {
                    MSScan ParentScan = GetScanFromFile(scan.ParentScanNo);
                    int ClosedIdx = MassUtility.GetClosestMassIdx(ParentScan.MZs, scan.ParentMZ);
                    List<int> Peaks = FindPeakIdx(ParentScan.MZs, ClosedIdx, scan.ParentCharge, 10);
                    scan.ParentMonoMz = ParentScan.MZs[Peaks[0]];
                }
            }
            #endregion  
            scan.IsCIDScan = IsCIDScan(argScanNo);
            scan.IsHCDScan = IsHCDScan(argScanNo);
            scan.IsFTScan = IsFTScan(argScanNo);
            return scan;
        }
        public MSScan ReadScan(int argScanNo)
        {
            return GetScanFromFile(argScanNo,2);
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
                _raw.Dispose();
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
        
        
    }

      
}
