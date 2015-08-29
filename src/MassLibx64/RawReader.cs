using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Threading;
using System.IO;
using CSNamedPipe;
namespace COL.MassLib
{
    public class RawReader : IRawFileReader
    {
        private string _fullFilePath;
        private enumRawDataType _fileType;
        private double _peptideMinBackgroundRatio = 5.0;
        private double _peakBackgroundRatio = 5.0;
        private double _singleToNoiseRatio = 3.0;
        private short _maxCharge = 10;
        //private int _scanNo = 1;
        private int _numOfScans = 0;
        private int _usagecountter = 0;
        Process GlypIDReader;
        ProcessStartInfo startInfo;
        NativeNamedPipeServer NamedPipeSrv;
        byte[] msgBytes;
        Thread PipeServer;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="argFullPath"></param>
        /// <param name="argFileType">raw or mzxml</param>
        public RawReader(string argFullPath, string argFileType)
        {
            //ListenServr.StartNamedPipeServer();
            Thread PipeServer = new Thread(StartNamedPipeServer);
            PipeServer.Start();
            _fullFilePath = argFullPath;
            if (argFileType.ToLower() == "raw")
            {
                _fileType = enumRawDataType.raw;
            }
            else
            {
                _fileType = enumRawDataType.mzxml;
            }
            GlypIDReader = new Process();
            //GlypIDReader.OutputDataReceived += new DataReceivedEventHandler(
            //(s, e) =>
            //{
            //    Desc += e.Data + "\n";
            //}
            //);
            startInfo = new ProcessStartInfo();
            startInfo.FileName = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + "\\GlypIDWrapper.exe";
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardInput = true;
            startInfo.RedirectStandardError = true;
            startInfo.UseShellExecute = false;
            startInfo.CreateNoWindow = true;
            GlypIDReader.StartInfo = startInfo;
            GlypIDReader.Start();


        }
        public RawReader(string argFullPath, enumRawDataType argFileType)
        {
            //ListenServr.StartNamedPipeServer();
            Thread PipeServer = new Thread(StartNamedPipeServer);
            PipeServer.Start();
            _fullFilePath = argFullPath;
            _fileType = argFileType;

            GlypIDReader = new Process();
            //GlypIDReader.OutputDataReceived += new DataReceivedEventHandler(
            //(s, e) =>
            //{
            //    Desc += e.Data + "\n";
            //}
            //);
            startInfo = new ProcessStartInfo();
            startInfo.FileName = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + "\\GlypIDWrapper.exe";
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardInput = true;
            startInfo.RedirectStandardError = true;
            startInfo.UseShellExecute = false;
            startInfo.CreateNoWindow = true;
            GlypIDReader.StartInfo = startInfo;
            GlypIDReader.Start();


        }
        public RawReader(string argFullPath, string argFileType, double argSingleToNoise, double argPeakBackground, double argPeptideBackground, short argMaxCharge)
        {
            Thread PipeServer = new Thread(StartNamedPipeServer);
            PipeServer.Start();
            _fullFilePath = argFullPath;
            if (argFileType.ToLower() == "raw")
            {
                _fileType = enumRawDataType.raw;
            }
            else
            {
                _fileType = enumRawDataType.mzxml;
            }
            _singleToNoiseRatio=argSingleToNoise;
            _peakBackgroundRatio=argPeakBackground;
            _peptideMinBackgroundRatio=argPeptideBackground;
            _maxCharge = argMaxCharge;

            GlypIDReader = new Process();
            //GlypIDReader.OutputDataReceived += new DataReceivedEventHandler(
            //    (s, e) =>
            //    {
            //        Desc += e.Data + "\n";
            //    }
            //);
            startInfo = new ProcessStartInfo();
            startInfo.FileName = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + "\\GlypIDWrapper.exe";
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;
            startInfo.UseShellExecute = false;
            startInfo.CreateNoWindow = true;

            //PipeServerThread = new Thread(StartNamePipeServer);
            //PipeServerThread.Start();
            //PipeServerThread.Join();
        }
        public RawReader(string argFullPath, enumRawDataType argFileType, double argSingleToNoise, double argPeakBackground, double argPeptideBackground, short argMaxCharge)
        {
            Thread PipeServer = new Thread(StartNamedPipeServer);
            PipeServer.Start();
            _fullFilePath = argFullPath;
            _fileType = argFileType;
            _singleToNoiseRatio = argSingleToNoise;
            _peakBackgroundRatio = argPeakBackground;
            _peptideMinBackgroundRatio = argPeptideBackground;
            _maxCharge = argMaxCharge;

            GlypIDReader = new Process();
            //GlypIDReader.OutputDataReceived += new DataReceivedEventHandler(
            //    (s, e) =>
            //    {
            //        Desc += e.Data + "\n";
            //    }
            //);
            startInfo = new ProcessStartInfo();
            startInfo.FileName = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + "\\GlypIDWrapper.exe";
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;
            startInfo.UseShellExecute = false;
            startInfo.CreateNoWindow = true;

            //PipeServerThread = new Thread(StartNamePipeServer);
            //PipeServerThread.Start();
            //PipeServerThread.Join();
        }
        public void SetPeakProcessorParameter(float argSignalToNoiseThreshold, float argPeakBackgroundRatio)
        {
            _singleToNoiseRatio = argSignalToNoiseThreshold;
            _peakBackgroundRatio = argPeakBackgroundRatio;
        }
        public int NumberOfScans
        {
            get 
            {
                if (_numOfScans == 0)
                {
                    _usagecountter++;
                    if (_usagecountter == 100)
                    {
                        GlypIDReader.Close();
                        GlypIDReader.Start();
                        _usagecountter = 1;
                    }
           
                    //Process GlypIDReader = new Process();
                    //GlypIDReader.StartInfo.FileName = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + "\\GlypIDWrapper.exe";
                    //GlypIDReader.StartInfo.Arguments = "\"" + _fullFilePath + "\" " + _fileType + " N ";
                    //GlypIDReader.StartInfo.RedirectStandardOutput = true;
                    //GlypIDReader.StartInfo.RedirectStandardError = true;
                    //GlypIDReader.StartInfo.UseShellExecute = false;
                    ////GlypIDReader.StartInfo.CreateNoWindow = true;
                    //startInfo.Arguments = "\"" + _fullFilePath + "\" " + _fileType + " N ";
                    //GlypIDReader.StartInfo = startInfo;
                    //GlypIDReader.Start();
                    //GlypIDReader.WaitForExit();
                    //_numOfScans = GlypIDReader.ExitCode;
                    GlypIDReader.StandardInput.WriteLine("n");
                    GlypIDReader.StandardInput.WriteLine(_fullFilePath);
                    GlypIDReader.StandardInput.WriteLine(_fileType);
                    
                    string output = "";
                    
                    do
                    {
                        output = GlypIDReader.StandardOutput.ReadLine();
                    }while(!output.StartsWith("ANS:"));
                    Console.WriteLine(output);
                    _numOfScans = Convert.ToInt32(output.Substring(4));
                }


                return _numOfScans;
            }
        }
        public int GetMsLevel(int argScan)
        {
            _usagecountter++;
            if (_usagecountter == 100)
            {
                GlypIDReader.Close();
                GlypIDReader.Start();
                _usagecountter = 1;
            }
            //Process GlypIDReader = new Process();
            //GlypIDReader.StartInfo.FileName = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + "\\GlypIDWrapper.exe";
            //GlypIDReader.StartInfo.Arguments = "\"" + _fullFilePath + "\" " + _fileType + " M " + argScan.ToString();
            //GlypIDReader.StartInfo.RedirectStandardOutput = true;
            //GlypIDReader.StartInfo.UseShellExecute = false;
            //GlypIDReader.StartInfo.CreateNoWindow = true;
            //startInfo.Arguments = "\"" + _fullFilePath + "\" " + _fileType + " M " + argScan.ToString();
            //GlypIDReader.StartInfo = startInfo;
            //GlypIDReader.Start();
            //GlypIDReader.WaitForExit();
            //return GlypIDReader.ExitCode;
            GlypIDReader.StandardInput.WriteLine("m");
            GlypIDReader.StandardInput.WriteLine(_fullFilePath);
            GlypIDReader.StandardInput.WriteLine(_fileType);
            GlypIDReader.StandardInput.WriteLine(argScan.ToString());
            StreamReader sr = GlypIDReader.StandardOutput;
            string output = "";
            do
            {
                output = sr.ReadLine(); 
            } while (!output.StartsWith("ANS:"));
            
            //GlypIDReader.StandardOutput.Close();
            Console.WriteLine(output);
            return  Convert.ToInt32(output.Substring(4));
        }
        public string RawFilePath
        {
            get { return _fullFilePath; }
        }
        public HCDInfo GetHCDInfo(int argScanNum)
        {
            _usagecountter++;
            if (_usagecountter == 100)
            {
                GlypIDReader.Close();
                GlypIDReader.Start();
                _usagecountter = 1;
            }
            GlypIDReader.StandardInput.WriteLine("h");
            GlypIDReader.StandardInput.WriteLine(_fullFilePath);
            GlypIDReader.StandardInput.WriteLine(_fileType);
            GlypIDReader.StandardInput.WriteLine(argScanNum.ToString());
            string output = "";
            do
            {
                output = GlypIDReader.StandardOutput.ReadLine();
            } while (!output.StartsWith("ANS:"));
            Console.WriteLine(output);
            output = output.Substring(4);
            if (output == "false")
            {
                return null;
            }
            string HCDString = output;


            int ScanNum = Convert.ToInt32(HCDString.Split(';')[0]);
            string StrType = HCDString.Split(';')[1];
            double Score = Convert.ToDouble(HCDString.Split(';')[2]);
            enumGlycanType GType = enumGlycanType.NA;
            //     CA = 1, CS, HM, HY, NA 
            if (StrType == "CA")
            {
                GType = enumGlycanType.CA;
            }
            else if (StrType == "CS")
            {
                GType = enumGlycanType.CS;
            }
            else if (StrType == "HM")
            {
                GType = enumGlycanType.HM;
            }
            else if (StrType == "HY")
            {
                GType = enumGlycanType.HY;
            }

            return new HCDInfo(ScanNum, GType, Score);
        }
        public bool IsCIDScan(int argScanNum)
        {
            //Process GlypIDReader = new Process();
            //GlypIDReader.StartInfo.FileName = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + "\\GlypIDWrapper.exe";
            //GlypIDReader.StartInfo.Arguments = "\"" + _fullFilePath + "\" " + _fileType + " c " + argScanNum.ToString();
            //GlypIDReader.StartInfo.RedirectStandardOutput = true;
            //GlypIDReader.StartInfo.UseShellExecute = false;
            //GlypIDReader.StartInfo.CreateNoWindow = true;
            //startInfo.Arguments = "\"" + _fullFilePath + "\" " + _fileType + " c " + argScanNum.ToString();
            //GlypIDReader.StartInfo = startInfo;
            //GlypIDReader.Start();
            //GlypIDReader.WaitForExit();
            //int isCID = GlypIDReader.ExitCode;
            //if (isCID == 1)
            //{
            //    return true;
            //}
            //return false;
            _usagecountter++;
            if (_usagecountter == 100)
            {
                GlypIDReader.Close();
                GlypIDReader.Start();
                _usagecountter = 1;
            }
            GlypIDReader.StandardInput.WriteLine("c");
            GlypIDReader.StandardInput.WriteLine(_fullFilePath);
            GlypIDReader.StandardInput.WriteLine(_fileType);
            GlypIDReader.StandardInput.WriteLine(argScanNum.ToString());
            string output = "";
            do
            {
                output = GlypIDReader.StandardOutput.ReadLine();
            } while (!output.StartsWith("ANS:"));
            Console.WriteLine(output);
            if (output.Substring(4) == "true")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //string Desc = "";
        public string GetScanDescription(int argScanNum)
        {
            
            //Process GlypIDReader = new Process();
            //GlypIDReader.StartInfo.FileName = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + "\\GlypIDWrapper.exe";
            //GlypIDReader.StartInfo.Arguments = "\"" + _fullFilePath + "\" " + _fileType + " S " +argScanNum;
            //GlypIDReader.StartInfo.RedirectStandardOutput = true;
            //GlypIDReader.StartInfo.RedirectStandardError = true;
            //GlypIDReader.StartInfo.UseShellExecute = false;
            //GlypIDReader.StartInfo.CreateNoWindow = true;
            

            //startInfo.Arguments = "\"" + _fullFilePath + "\" " + _fileType + " S " + argScanNum;
            //GlypIDReader.StartInfo = startInfo;
            //GlypIDReader.Start();
            ////GlypIDReader.BeginOutputReadLine();
            
            //System.IO.StreamReader myStreamReader = GlypIDReader.StandardOutput;
            //string myString = myStreamReader.ReadToEnd();
     
            //GlypIDReader.WaitForExit();
            //return myString;
            //aResetEvt = new AutoResetEvent(false);
            //_scanNo = argScanNum;

            //Thread WrapperThread = new Thread(StartGetDescriptionWrapper);
            //WrapperThread.Start();
            //WrapperThread.Join();


            //System.Runtime.Serialization.IFormatter f = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

            //aResetEvt.WaitOne();
            //System.IO.MemoryStream memStream = new System.IO.MemoryStream(RecivedScan);
            //return (string)f.Deserialize(memStream);
            _usagecountter++;
            if (_usagecountter == 100)
            {
                GlypIDReader.Close();
                GlypIDReader.Start();
                _usagecountter = 1;
            }
            GlypIDReader.StandardInput.WriteLine("s");
            GlypIDReader.StandardInput.WriteLine(_fullFilePath);
            GlypIDReader.StandardInput.WriteLine(_fileType);
            GlypIDReader.StandardInput.WriteLine(argScanNum.ToString());
            string output = "";
            do
            {
                output = GlypIDReader.StandardOutput.ReadLine();
            } while (!output.StartsWith("ANS:"));
            Console.WriteLine(output);
            return output;
        }
        //private void StartGetDescriptionWrapper()
        //{
        //    Process GlypIDReader = new Process();
        //    GlypIDReader.StartInfo.FileName = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + "\\GlypIDWrapper.exe";
        //    GlypIDReader.StartInfo.Arguments = "\"" + _fullFilePath + "\" " + _fileType + " S " + _scanNo.ToString() + "  " +
        //                                        _singleToNoiseRatio.ToString() + " " +
        //                                        _peakBackgroundRatio.ToString() + " " +
        //                                        _peptideMinBackgroundRatio.ToString() + " " +
        //                                        _maxCharge.ToString();
        //    GlypIDReader.StartInfo.RedirectStandardOutput = true;
        //    GlypIDReader.StartInfo.UseShellExecute = false;
        //    GlypIDReader.StartInfo.CreateNoWindow = true;

        //    GlypIDReader.Start();
        //    while (!GlypIDReader.StandardOutput.EndOfStream)
        //    {
        //        Console.WriteLine(GlypIDReader.StandardOutput.ReadLine());
        //    }


        //    GlypIDReader.WaitForExit();
        //}
    
        public MSScan ReadScan(int argScan)
        {

            //if (ListenServr.aResetEvt == null)
            //{
            //    ListenServr.aResetEvt = new AutoResetEvent(false);
            //}
            //else
            //{
            //    ListenServr.aResetEvt.Reset();
            //}
            //_scanNo = argScan;

            //Thread WrapperThread = new Thread(StartReadScanWrapper);
            //WrapperThread.Name = "WrapperInvokeThread";
            //WrapperThread.IsBackground = true;
            //WrapperThread.Start();
            //WrapperThread.Join();
            _usagecountter++;
            if (_usagecountter == 100)
            {
                GlypIDReader.Close();
                GlypIDReader.Start();
                _usagecountter = 1;
            }
            string output = "";

            GlypIDReader.StandardInput.WriteLine("r");
            GlypIDReader.StandardInput.WriteLine(_fullFilePath);
            GlypIDReader.StandardInput.WriteLine(_fileType);
            GlypIDReader.StandardInput.WriteLine(argScan.ToString());
            GlypIDReader.StandardInput.WriteLine(_singleToNoiseRatio.ToString());
            GlypIDReader.StandardInput.WriteLine(_peakBackgroundRatio.ToString());
            GlypIDReader.StandardInput.WriteLine(_peptideMinBackgroundRatio.ToString());
            GlypIDReader.StandardInput.WriteLine(_maxCharge.ToString());

            do
            {
                output = GlypIDReader.StandardOutput.ReadLine();
            } while (!output.StartsWith("ANS:"));
            do
            {
                msgBytes = NamedPipeSrv.ReceivedData;
            } while (msgBytes == null);
            try
            {
                System.Runtime.Serialization.IFormatter f = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                System.IO.MemoryStream memStream = new System.IO.MemoryStream(msgBytes);
                MSScan scan = (MSScan)f.Deserialize(memStream);
                msgBytes = null;
                memStream.Close();
                return scan;
            }
            catch (Exception e)
            {
                throw new Exception("Named piped scan " + argScan.ToString()+"  empty    "+e.Message);
            }
            
        }
        private void StartReadScanWrapper()
        {
            //Process GlypIDReader = new Process();
            //GlypIDReader.StartInfo.FileName = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + "\\GlypIDWrapper.exe";
            //GlypIDReader.StartInfo.Arguments = "\"" + _fullFilePath + "\" " + _fileType + " R " + _scanNo.ToString() + "  " +
            //                                    _singleToNoiseRatio.ToString() + " " +
            //                                    _peakBackgroundRatio.ToString() + " " +
            //                                    _peptideMinBackgroundRatio.ToString() + " " +
            //                                    _maxCharge.ToString();
            //GlypIDReader.StartInfo.RedirectStandardOutput = true;
            //GlypIDReader.StartInfo.UseShellExecute = false;
            //GlypIDReader.StartInfo.CreateNoWindow = true;
            //startInfo.Arguments = "\"" + _fullFilePath + "\" " + _fileType + " R " + _scanNo.ToString() + "  " +
            //                                    _singleToNoiseRatio.ToString() + " " +
            //                                    _peakBackgroundRatio.ToString() + " " +
            //                                    _peptideMinBackgroundRatio.ToString() + " " +
            //                                    _maxCharge.ToString();
            //GlypIDReader.StartInfo = startInfo;
            //GlypIDReader.Start();
            //while (!GlypIDReader.StandardOutput.EndOfStream)
            //{
            //    Console.WriteLine(GlypIDReader.StandardOutput.ReadLine());
            //}


            //GlypIDReader.WaitForExit();
        }
        //private void StartNamePipeServer()
        //{
        //    pipeSrv = new PipeServer();
        //    pipeSrv.MessageReceived += new PipeServer.MessageReceivedHandler(MsgRevied);
        //    pipeSrv.Start("\\\\.\\pipe\\GlypIDPipe");
 
        //}


  
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
            int EndScan  =  this.NumberOfScans;
            return ReadScans( 1, EndScan);
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
            for(int i =argStart;i<=argEnd;i++)
            {
                MSScan scan = ReadScan(i);
                if (scan.MsLevel == argMSLevel)
                {
                    scans.Add(scan);
                }
            }
            return scans;
        }


        private void StartNamedPipeServer()
        {
            NamedPipeSrv = new NativeNamedPipeServer(@"\\.\pipe\MSScan");
            NamedPipeSrv.Run();
            
        }
        public void Close()
        {
            NamedPipeSrv.Close();
            
            if (GlypIDReader.HasExited == false)
            {
                GlypIDReader.Kill();
            }
        }
        ~RawReader()
        {
            NamedPipeSrv.Close();
            
            if (GlypIDReader.HasExited == false)
            {
                GlypIDReader.Kill();
            }
        }
    }
    //public class ListenServr
    //{

    //    private static PipeServer pipeSrv;
    //    public static void StartNamedPipeServer()
    //    {
    //        if (pipeSrv == null)
    //        {
    //            pipeSrv = new PipeServer();
    //            pipeSrv.MessageReceived += new PipeServer.MessageReceivedHandler(MsgRevied);
    //            pipeSrv.Start("\\\\.\\pipe\\GlypIDPipe");
    //        }
    //    }
    //    public static byte[] BinRecivedScan;
    //    public static AutoResetEvent aResetEvt;
    //    private static void MsgRevied(byte[] message)
    //    {
    //        aResetEvt.Set();
    //        BinRecivedScan = message;
    //    }
    //    public static void StopNamedPipeServerThread()
    //    {
    //        if (pipeSrv.bolStopServer == false)
    //        {
    //            pipeSrv.bolStopServer = true;
    //        }
    //    }
    //}
}
