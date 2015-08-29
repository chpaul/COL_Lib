using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace COL.MassLib.Filters
{
    public class BackgroundFilter : IFilter
    {
        private MSScan _msScan;
        private double _peakToBackgroundRatio = 2;
        private double _signalToNoiseThreshold = 3;
        public double PeakToBackgroundRatio
        {
            get { return _peakToBackgroundRatio; }
            set { _peakToBackgroundRatio = value; }
        }

        public double SignalToNoiseThreshold
        {
            get { return _signalToNoiseThreshold; }
            set { _signalToNoiseThreshold = value; }
        }
        public BackgroundFilter(MSScan argScan)
        {
            _msScan = argScan;
        }
        public void ApplyFilter()
        {
            
            float[] xData = _msScan.RawMZs;
            float[] yData = _msScan.RawIntensities;
            DeconToolsV2.Peaks.clsPeakProcessor peakProcessor = new DeconToolsV2.Peaks.clsPeakProcessor();
            DeconToolsV2.Peaks.clsPeakProcessorParameters parameters = new DeconToolsV2.Peaks.clsPeakProcessorParameters();

            parameters.PeakBackgroundRatio = PeakToBackgroundRatio;
            parameters.SignalToNoiseThreshold = SignalToNoiseThreshold;
            parameters.PeakFitType = DeconToolsV2.Peaks.PEAK_FIT_TYPE.QUADRATIC;

            peakProcessor.SetOptions(parameters);
            DeconToolsV2.Peaks.clsPeak[] peakList = new DeconToolsV2.Peaks.clsPeak[1];
            peakProcessor.DiscoverPeaks(ref xData, ref yData, ref peakList, 0, float.MaxValue);
            float[] mz = new float[peakList.Length];
            float[] intensity = new float[peakList.Length];
            for (int i = 0; i < peakList.Length; i++)
            {
                mz[i] = Convert.ToSingle(peakList[i].mdbl_mz);
                intensity[i] = Convert.ToSingle(peakList[i].mdbl_intensity);
            }
            _msScan.MZs = mz;
            _msScan.Intensities = intensity;
        }

    }
}
