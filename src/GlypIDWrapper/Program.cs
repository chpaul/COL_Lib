using System;
using System.Collections.Generic;
using System.Text;
using COL.MassLib;
using COL.GlycoLib;
using System.IO;
using System.Threading;
namespace GlypIDWrapper
{
    class Program
    {

        
 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args">
        /// Arg0: FilePath
        /// Arg1: FileType switch
        ///        raw
        ///        mzxml
        /// Arg2: Function switch
        ///        R:ReadScan
        ///        N:Number of Scan
        ///        H: Get HCD info
        ///        C: Checked if it is CID scan
        ///        S: Scan header Description 
        ///        M: Get MS Level
        /// Arg3: 
        ///      Switch R:  int scan number
        ///      Switch N:  none
        ///
        /// Arg4: PeakProcessorParameter  _singleToNoiseRatio
        /// Arg5: PeakProcessorParameter  _peakBackgroundRatio
        /// Arg6: TransformParameter  _peptideMinBackgroundRatio
        /// Arg7: TransformParameter _maxCharge
        /// </param>
        /// 
        static int Main(string[] args)
        {
            string FunctionSwitch;// = args[2].ToLower();
            GlypID.Readers.clsRawData Raw;
            //Debug

            //End Debug

            try
            {
                do
                {
                    Console.WriteLine(
                        "@@\nR:ReadScan\n" +
                        "N:Number of Scan\n" +
                        "H: Get HCD info\n" +
                        "C: Checked if it is CID scan\n" +
                        "S: Scan header Description\n" +
                        "M: Get MS Level\n" +
                        "Q: Quit\n" +
                        "Please select function:");
                    FunctionSwitch = Console.In.ReadLine().ToLower().Substring(0, 1);

                    if (FunctionSwitch == "q")
                    {
                        return 0;
                    }
                    else
                    {
                        Console.WriteLine("Please input file location:");
                        string _fullFilePath = Console.In.ReadLine();
                        Console.WriteLine("FIle Type(r: Raw, x:mzXML)");
                        if (Console.In.ReadLine().ToLower().StartsWith("x"))
                        {
                            Raw = new GlypID.Readers.clsRawData(_fullFilePath, GlypID.Readers.FileType.MZXMLRAWDATA);
                        }
                        else
                        {
                            Raw = new GlypID.Readers.clsRawData(_fullFilePath, GlypID.Readers.FileType.FINNIGAN);
                        }

                        //PipeClient pipeClt = new PipeClient();
                        //pipeClt.Connect("\\\\.\\pipe\\GlypIDPipe");
                        if (FunctionSwitch == "r")//Read Scan
                        {
                            Console.WriteLine("Please input scan num:");
                            int ScanNo = Convert.ToInt32(Console.In.ReadLine());

                            Console.WriteLine("Please input Single to Noise ratio (default:3.0):");
                            double parsedResult = 3.0;
                            double _singleToNoiseRatio = 3.0;
                            if (double.TryParse(Console.In.ReadLine(), out parsedResult))
                            {
                                _singleToNoiseRatio = parsedResult;
                            }

                            Console.WriteLine("Please input Peak background ratio (default:5.0):");
                            parsedResult = 5.0;
                            double _peakBackgroundRatio = 5.0;
                            if (double.TryParse(Console.In.ReadLine(), out parsedResult))
                            {
                                _peakBackgroundRatio = parsedResult;
                            }

                            Console.WriteLine("Please input Minimum background ratio (default:5.0):");
                            parsedResult = 5.0;
                            double _peptideMinBackgroundRatio = 5.0;
                            if (double.TryParse(Console.In.ReadLine(), out parsedResult))
                            {
                                _peptideMinBackgroundRatio = parsedResult;
                            }

                            Console.WriteLine("Please input Max charge (defatlt:10):");
                            short paredCharge = 10;
                            short _maxCharge = 10;
                            if (short.TryParse(Console.In.ReadLine(), out paredCharge))
                            {
                                _maxCharge = paredCharge;
                            }


                            Console.WriteLine("Reading Scan");
                            MSScan scan = GetScanFromFile(ScanNo, _singleToNoiseRatio, _peakBackgroundRatio, _peptideMinBackgroundRatio, _maxCharge , Raw);
                            string responseMsg = SendScanViaNamedPipe(scan);
                            
                            //scan = null;
                            //if (pipeClt.Connected)
                            //{
                            //    while (!pipeClt.SendMessage(memStream.ToArray())) ;
                            //    Console.WriteLine("Named pipe sent");
                            //    pipeClt.Disconnect();
                            //}
                            //else
                            //{
                            //    throw new Exception("Named Pipe not found");
                            //}
                            Console.WriteLine("ANS:Read Scan: " + ScanNo + " finish;MSScan bytes-" + responseMsg);
                        }
                        else if (FunctionSwitch == "n") //Get total scans
                        {
                            Console.WriteLine("ANS:" + Raw.GetNumScans().ToString());

                        }
                        else if (FunctionSwitch == "h") //Check HCD
                        {
                            Console.WriteLine("Please input scan num:");
                            int ScanNo = Convert.ToInt32(Console.In.ReadLine());
                            HCDInfo HCDinfo = GetHCDInfo(Raw, ScanNo);
                            if (HCDinfo != null)
                            {
                                Console.WriteLine("ANS:" + HCDinfo.ScanNum.ToString() + ";" +
                                                  HCDinfo.GlycanType.ToString() + ";" +
                                                  HCDinfo.HCDScore.ToString());


                            }
                            else
                            {
                                Console.WriteLine("ANS:" + "false");
                            }
                        }
                        else if (FunctionSwitch == "c") //Check CID
                        {
                            Console.WriteLine("Please input scan num:");
                            int ScanNo = Convert.ToInt32(Console.In.ReadLine());
                            if (Raw.IsCIDScan(ScanNo) == true)
                            {
                                Console.WriteLine("ANS:" + "true");
                            }
                            else
                            {
                                Console.WriteLine("ANS:" + "false");
                            }
                        }
                        else if (FunctionSwitch == "s") //Check description
                        {
                            Console.WriteLine("Please input scan num:");
                            int ScanNo = Convert.ToInt32(Console.In.ReadLine());
                            string description = Raw.GetScanDescription(ScanNo);
                            Console.WriteLine("ANS:" + description);

                        }
                        else if (FunctionSwitch == "m")//Get MS Level
                        {
                            Console.WriteLine("Please input scan num:");
                            int ScanNo = Convert.ToInt32(Console.In.ReadLine());
                            Console.WriteLine("ANS:" + Raw.GetMSLevel(ScanNo));
                        }
                        else
                        {
                            Console.WriteLine("Function error");
                        }
                    }
                } while (true);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return -1;
            }

                //if (args == null || args.Length == 0)
                //{
                //    Console.WriteLine("args is null"); // Check for null array
                //    return -1;
                //}
                
                //if (FunctionSwitch == "r" && args.Length != 8)
                //{
                //    Console.WriteLine("args is not enough"); // Check for null array
                //    return -1;
                //}
                //else
                //{


                //    string _fullFilePath = Console.In.ReadLine();

                //    Console.WriteLine("Init GlypID");
                //    if (args[1].ToLower() == "raw")
                //    {
                //        Raw = new GlypID.Readers.clsRawData(_fullFilePath, GlypID.Readers.FileType.FINNIGAN);
                //    }
                //    else
                //    {
                //        Raw = new GlypID.Readers.clsRawData(_fullFilePath, GlypID.Readers.FileType.MZXMLRAWDATA);
                //    }


                //    if (FunctionSwitch == "n") //Get Total scans
                //    {
                //        Console.WriteLine(Raw.GetNumScans().ToString());
                //        return Raw.GetNumScans();
                //    }
                //    else if (FunctionSwitch == "m")  //Get MS Level
                //    {
                //        int ScanNo = Convert.ToInt32(args[3]);
                //        return Raw.GetMSLevel(ScanNo);
                //    }
                //    else if (FunctionSwitch == "c") //IsCID Scan
                //    {
                //        int ScanNo = Convert.ToInt32(args[3]);
                //        if (Raw.IsCIDScan(ScanNo) == true)
                //        {
                //            return 1;
                //        }
                //        return 0;

                //    }
                //    else if (FunctionSwitch == "s") //Get Description
                //    {
                //        int ScanNo = Convert.ToInt32(args[3]);

                //        //Console.WriteLine("Get Description info");
                //        string description = Raw.GetScanDescription(ScanNo);
                //        Console.WriteLine(description);
                //        //System.Runtime.Serialization.IFormatter f = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                //        //MemoryStream memStream = new MemoryStream();
                //        //f.Serialize(memStream, description);
                //        //PipeClient pipeClt = new PipeClient();
                //        //pipeClt.Connect("\\\\.\\pipe\\GlypIDPipe");
                //        //if (pipeClt.Connected)
                //        //{
                //        //    while (!pipeClt.SendMessage(memStream.ToArray())) ;
                //        //    Console.WriteLine("Named pipe sent");
                //        //}
                //        //else
                //        //{
                //        //    throw new Exception("Named Pipe not found");
                //        //}

                //        return 1;
                //    }
                //    else if (FunctionSwitch == "h")  //Get HCD
                //    {
                //        int ScanNo = Convert.ToInt32(args[3]);
                //        HCDInfo HCDinfo = GetHCDInfo(Raw, ScanNo);

                //        //Console.WriteLine("Get HCD info");
                //        //System.Runtime.Serialization.IFormatter f = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                //        //PipeClient pipeClt = new PipeClient();
                //        //pipeClt.Connect("\\\\.\\pipe\\GlypIDPipeHCD");
                //        if (HCDinfo != null)
                //        {
                //            Console.WriteLine(HCDinfo.ScanNum.ToString() + ";" +
                //                              HCDinfo.GlycanType.ToString() + ";" +
                //                              HCDinfo.HCDScore.ToString());
                //            //MemoryStream memStream = new MemoryStream();
                //            //f.Serialize(memStream, HCDinfo);

                //            //if (pipeClt.Connected)
                //            //{
                //            //    while (!pipeClt.SendMessage(memStream.ToArray())) ;
                //            //    Console.WriteLine("Named pipe sent");
                //            //}
                //            //else
                //            //{
                //            //    throw new Exception("Named Pipe not found");
                //            //}
                //            return 1;
                //        }
                //        else
                //        {

                //            //if (pipeClt.Connected)
                //            //{

                //            //    while (!pipeClt.SendMessage(Encoding.ASCII.GetBytes("No HCD Info found"))) ;
                //            //    Console.WriteLine("Named pipe sent");
                //            //}
                //            //else
                //            //{
                //            //    throw new Exception("Named Pipe not found");
                //            //}
                //            return -1;
                //        }



                //    }
                //    else //ReadScan
                //    {
                //        int ScanNo = Convert.ToInt32(args[3]);
                //        double _singleToNoiseRatio = Convert.ToDouble(args[4]);
                //        double _peakBackgroundRatio = Convert.ToDouble(args[5]);
                //        double _peptideMinBackgroundRatio = Convert.ToDouble(args[6]);
                //        short _maxCharge = Convert.ToInt16(args[7]);


                //        Console.WriteLine("Read Scan");
                //        MSScan scan = GetScanFromFile(ScanNo, _singleToNoiseRatio, _peakBackgroundRatio, _peptideMinBackgroundRatio, _maxCharge);
                //        System.Runtime.Serialization.IFormatter f = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                //        MemoryStream memStream = new MemoryStream();
                //        f.Serialize(memStream, scan);
                //        PipeClient pipeClt = new PipeClient();
                //        pipeClt.Connect("\\\\.\\pipe\\GlypIDPipe");
                //        if (pipeClt.Connected)
                //        {
                //            while (!pipeClt.SendMessage(memStream.ToArray())) ;
                //            Console.WriteLine("Named pipe sent");
                //        }
                //        else
                //        {
                //            throw new Exception("Named Pipe not found");
                //        }


                //        /*NamedPipeClientStream PipeClient = new NamedPipeClientStream(".", "ReadMSScan", PipeDirection.Out);
                //        PipeClient.Connect();
                    
                    
                //        f.Serialize(PipeClient, scan);
                //        PipeClient.Close();*/
                //        Console.WriteLine("Read Scan: " + args[3] + " finish");

                //    }
                //    //End Read Scan

                //    return 1;
                //}


        }
       
        private static string SendScanViaNamedPipe(MSScan argMSScan)
        {
            System.Runtime.Serialization.IFormatter f = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            MemoryStream memStream = new MemoryStream();
            f.Serialize(memStream, argMSScan);
            byte[] WriteBuffer = memStream.ToArray();
            string  responseMsg="";
            CSNamedPipe.NativeNamedPipeClient.Run(@"\\.\pipe\MSScan", WriteBuffer, out responseMsg);
            return responseMsg;
            
        }
        private static float[] FindChargeAndMono(GlypID.Peaks.clsPeak[] argPeaks, float argTargetMZ, int argParentScanNo, GlypID.Readers.clsRawData Raw)
        {
            float[] MonoAndMz = new float[2];

            double interval = 9999.9;
            int ClosedIdx = 0;
            for (int i = 0; i < argPeaks.Length; i++)
            {
                if (Math.Abs(argPeaks[i].mdbl_mz - argTargetMZ) < interval)
                {
                    interval = Math.Abs(argPeaks[i].mdbl_mz - argTargetMZ);
                    ClosedIdx = i;
                }
            }


            //Charge
            float testMz = 0.0f;
            int MaxMatchedPeak = 2;

            for (int i = 1; i <= 6; i++)
            {
                double FirstMonoMz = 0.0;
                int ForwardPeak = 0;
                int BackardPeak = 0;
                //Forward Check
                testMz = argTargetMZ - 1.0f / (float)i;
                int CheckIdx = ClosedIdx - 1;
                for (int j = 1; j <= 10; j++)
                {
                    if (CheckIdx < 0)
                    {
                        break;
                    }
                    if (Math.Abs(argPeaks[CheckIdx].mdbl_mz - testMz) <= 0.03)
                    {
                        ForwardPeak++;
                        testMz = Convert.ToSingle(argPeaks[CheckIdx].mdbl_mz) - 1.0f / (float)i;
                        FirstMonoMz = argPeaks[CheckIdx].mdbl_mz;
                    }
                    CheckIdx = CheckIdx - 1;
                }



                //Backward
                testMz = argTargetMZ + 1.0f / (float)i;
                CheckIdx = ClosedIdx + 1;

                for (int j = 1; j <= 10; j++)
                {
                    if (CheckIdx >= argPeaks.Length)
                    {
                        break;
                    }
                    if (Math.Abs(argPeaks[CheckIdx].mdbl_mz - testMz) <= 0.03)
                    {
                        BackardPeak++;
                        testMz = Convert.ToSingle(argPeaks[CheckIdx].mdbl_mz) + 1.0f / (float)i;
                    }
                    CheckIdx = CheckIdx + 1;
                }

                if (ForwardPeak == 0)
                {
                    FirstMonoMz = argTargetMZ;
                }

                if (ForwardPeak + BackardPeak >= MaxMatchedPeak)
                {
                    MaxMatchedPeak = ForwardPeak + BackardPeak;
                    MonoAndMz[0] = Convert.ToSingle(FirstMonoMz);
                    MonoAndMz[1] = i;
                }
            }

            if (MonoAndMz[1] == 0)
            {
                if (interval < 0.01)
                {
                    MonoAndMz[0] = argTargetMZ;
                }
                MonoAndMz[1] = Raw.GetMonoChargeFromHeader(argParentScanNo);
            }


            return MonoAndMz;
        }
        private static MSScan GetScanFromFile(int argScanNo, double argSingleToNoise, double argPeakBackground, double argPeptideBackground, short argMaxCharge, GlypID.Readers.clsRawData Raw)
        {

            float[] _cidMzs = null;
            float[] _cidIntensities = null;
            GlypID.Peaks.clsPeak[] _cidPeaks = new GlypID.Peaks.clsPeak[1];
            GlypID.Peaks.clsPeak[] _parentPeaks = new GlypID.Peaks.clsPeak[1];

            GlypID.HornTransform.clsHornTransform mobjTransform = new GlypID.HornTransform.clsHornTransform();
            GlypID.HornTransform.clsHornTransformParameters mobjTransformParameters = new GlypID.HornTransform.clsHornTransformParameters() ;
            GlypID.HornTransform.clsHornTransformResults[] _transformResult;


            GlypID.Peaks.clsPeakProcessor cidPeakProcessor = new GlypID.Peaks.clsPeakProcessor();
            GlypID.Peaks.clsPeakProcessorParameters cidPeakParameters = new GlypID.Peaks.clsPeakProcessorParameters();

            GlypID.Peaks.clsPeakProcessor parentPeakProcessor = new GlypID.Peaks.clsPeakProcessor() ;
            GlypID.Peaks.clsPeakProcessorParameters parentPeakParameters = new GlypID.Peaks.clsPeakProcessorParameters();
            
            //Start Read Scan 
            MSScan scan = new MSScan(argScanNo);
    

            Raw.GetSpectrum(argScanNo, ref _cidMzs, ref  _cidIntensities);
            scan.MsLevel = Raw.GetMSLevel(Convert.ToInt32(argScanNo));

            double min_peptide_intensity = 0;
            scan.Time = Raw.GetScanTime(scan.ScanNo);
            scan.ScanHeader = Raw.GetScanDescription(scan.ScanNo);
            if (scan.MsLevel != 1)
            {
                float[] _parentRawMzs = null;
                float[] _parentRawIntensitys = null;

                string Header = Raw.GetScanDescription(argScanNo);
                cidPeakProcessor.ProfileType = GlypID.enmProfileType.CENTROIDED;
                if (Header.Substring(Header.IndexOf("+") + 1).Trim().StartsWith("p"))
                {
                    cidPeakProcessor.ProfileType = GlypID.enmProfileType.PROFILE;
                }

                // cidPeakProcessor.DiscoverPeaks(ref _cidMzs, ref _cidIntensities, ref _cidPeaks,
                //         Convert.ToSingle(mobjTransformParameters.MinMZ), Convert.ToSingle(mobjTransformParameters.MaxMZ), false);

                for (int chNum = 0; chNum < _cidMzs.Length; chNum++)
                {
                    scan.MSPeaks.Add(new MSPeak(
                        Convert.ToSingle(_cidMzs[chNum]),
                        Convert.ToSingle(_cidIntensities[chNum])));
                }

                //for (int chNum = 0; chNum < _cidMzs.Length; chNum++)
                //{
                //    scan.MSPeaks.Add(new MSPeak(
                //        Convert.ToSingle(_cidMzs[chNum]),
                //        Convert.ToSingle(_cidIntensities[chNum])));
                //}

                // Get parent information
                scan.ParentScanNo = Raw.GetParentScan(scan.ScanNo);

                Raw.GetSpectrum(scan.ParentScanNo, ref _parentRawMzs, ref _parentRawIntensitys);
                parentPeakProcessor.ProfileType = GlypID.enmProfileType.PROFILE;
                parentPeakProcessor.DiscoverPeaks(ref _parentRawMzs, ref _parentRawIntensitys, ref _parentPeaks, Convert.ToSingle(mobjTransformParameters.MinMZ), Convert.ToSingle(mobjTransformParameters.MaxMZ), true);
                float _parentBackgroundIntensity = (float)parentPeakProcessor.GetBackgroundIntensity(ref _parentRawIntensitys);
                _transformResult = new GlypID.HornTransform.clsHornTransformResults[1];
                bool found = false;
                if (Raw.IsFTScan(scan.ParentScanNo))
                {
                    // High resolution data
                    found = mobjTransform.FindPrecursorTransform(Convert.ToSingle(_parentBackgroundIntensity), Convert.ToSingle(min_peptide_intensity), ref _parentRawMzs, ref _parentRawIntensitys, ref _parentPeaks, Convert.ToSingle(scan.ParentMZ), ref _transformResult);
                }
                if (!found)//de-isotope fail
                {
                    // Low resolution data or bad high res spectra
                    short cs = Raw.GetMonoChargeFromHeader(scan.ScanNo);
                    double monoMZ = Raw.GetMonoMzFromHeader(scan.ScanNo);
                    List<float> ParentMzs = new List<float>(_parentRawMzs);
                    int CloseIdx = MassUtility.GetClosestMassIdx(ParentMzs, Convert.ToSingle(monoMZ));

                    if (cs > 0)
                    {
                        short[] charges = new short[1];
                        charges[0] = cs;
                        mobjTransform.AllocateValuesToTransform(Convert.ToSingle(scan.ParentMZ), Convert.ToInt32(_parentRawIntensitys[CloseIdx]), ref charges, ref _transformResult);
                    }
                    else
                    {
                        // instrument has no charge just store 2 and 3.      
                        short[] charges = new short[2];
                        charges[0] = 2;
                        charges[1] = 3;
                        mobjTransform.AllocateValuesToTransform(Convert.ToSingle(scan.ParentMZ), Convert.ToInt32(_parentRawIntensitys[CloseIdx]), ref charges, ref _transformResult);
                    }
                }

                if (_transformResult[0].mint_peak_index == -1) //De-isotope parent scan
                {
                    //Get parent info
                    MSScan _parentScan = GetScanFromFile(scan.ParentScanNo, argSingleToNoise, argPeakBackground, argPeptideBackground, argMaxCharge,Raw);
                    float[] _MSMzs = null;
                    float[] _MSIntensities = null;

                    Raw.GetSpectrum(scan.ParentScanNo, ref _MSMzs, ref  _MSIntensities);
                    // Now find peaks
                    parentPeakParameters.SignalToNoiseThreshold = 0;
                    parentPeakParameters.PeakBackgroundRatio = 0.01;
                    parentPeakProcessor.SetOptions(parentPeakParameters);
                    parentPeakProcessor.ProfileType = GlypID.enmProfileType.PROFILE;

                    parentPeakProcessor.DiscoverPeaks(ref _MSMzs, ref _MSIntensities, ref _cidPeaks,
                                            Convert.ToSingle(mobjTransformParameters.MinMZ), Convert.ToSingle(mobjTransformParameters.MaxMZ), true);



                    //Look for charge and mono.


                    float[] monoandcharge = FindChargeAndMono(_cidPeaks, Convert.ToSingle(Raw.GetParentMz(scan.ScanNo)), scan.ScanNo,Raw);
                    //scan.ParentMonoMW = _parentScan.MSPeaks[ClosedIdx].MonoMass;
                    //scan.ParentAVGMonoMW = _parentScan.MSPeaks[ClosedIdx].;
                    scan.ParentMZ = monoandcharge[0];
                    if (monoandcharge[1] == 0.0f)
                    {
                        scan.ParentCharge = Convert.ToInt32(Raw.GetMonoChargeFromHeader(scan.ParentScanNo));
                    }
                    else
                    {
                        scan.ParentCharge = Convert.ToInt32(monoandcharge[1]);
                    }

                    scan.ParentMonoMW = (monoandcharge[0] - Atoms.ProtonMass) * monoandcharge[1];

                }
                else
                {
                    scan.ParentMonoMW = (float)_transformResult[0].mdbl_mono_mw;
                    scan.ParentAVGMonoMW = (float)_transformResult[0].mdbl_average_mw;
                    scan.ParentMZ = (float)_transformResult[0].mdbl_mz;
                    scan.ParentCharge = (int)_transformResult[0].mshort_cs;
                }
                scan.IsCIDScan = Raw.IsCIDScan(argScanNo);
                scan.IsFTScan = Raw.IsFTScan(argScanNo);

                Array.Clear(_transformResult, 0, _transformResult.Length);
                Array.Clear(_cidPeaks, 0, _cidPeaks.Length);
                Array.Clear(_cidMzs, 0, _cidMzs.Length);
                Array.Clear(_cidIntensities, 0, _cidIntensities.Length);
                Array.Clear(_parentRawMzs, 0, _parentRawMzs.Length);
                Array.Clear(_parentRawIntensitys, 0, _parentRawIntensitys.Length);
            }
            else //MS Scan
            {
                scan.ParentMZ = 0.0f;
                double mdbl_current_background_intensity = 0;

                // Now find peaks
                parentPeakParameters.SignalToNoiseThreshold = argSingleToNoise;
                parentPeakParameters.PeakBackgroundRatio = argPeakBackground;
                parentPeakProcessor.SetOptions(parentPeakParameters);
                parentPeakProcessor.ProfileType = GlypID.enmProfileType.PROFILE;

                parentPeakProcessor.DiscoverPeaks(ref _cidMzs, ref _cidIntensities, ref _cidPeaks,
                                        Convert.ToSingle(mobjTransformParameters.MinMZ), Convert.ToSingle(mobjTransformParameters.MaxMZ), true);
                mdbl_current_background_intensity = parentPeakProcessor.GetBackgroundIntensity(ref _cidIntensities);

                // Settings
                min_peptide_intensity = mdbl_current_background_intensity * mobjTransformParameters.PeptideMinBackgroundRatio;
                if (mobjTransformParameters.UseAbsolutePeptideIntensity)
                {
                    if (min_peptide_intensity < mobjTransformParameters.AbsolutePeptideIntensity)
                        min_peptide_intensity = mobjTransformParameters.AbsolutePeptideIntensity;
                }
                mobjTransformParameters.PeptideMinBackgroundRatio = argPeptideBackground;
                mobjTransformParameters.MaxCharge = argMaxCharge;
                mobjTransform.TransformParameters = mobjTransformParameters;


                //  Now perform deisotoping
                _transformResult = new GlypID.HornTransform.clsHornTransformResults[1];
                mobjTransform.PerformTransform(Convert.ToSingle(mdbl_current_background_intensity), Convert.ToSingle(min_peptide_intensity), ref _cidMzs, ref _cidIntensities, ref _cidPeaks, ref _transformResult);
                // for getting results

                for (int chNum = 0; chNum < _transformResult.Length; chNum++)
                {
                    double sumintensity = 0.0;
                    double mostIntenseIntensity = 0.0;
                    for (int i = 0; i < _transformResult[chNum].marr_isotope_peak_indices.Length; i++)
                    {
                        sumintensity = sumintensity + _cidPeaks[_transformResult[chNum].marr_isotope_peak_indices[i]].mdbl_intensity;
                        if (Math.Abs(_transformResult[chNum].mdbl_most_intense_mw -
                            (_cidPeaks[_transformResult[chNum].marr_isotope_peak_indices[i]].mdbl_mz * _transformResult[chNum].mshort_cs - Atoms.ProtonMass * _transformResult[chNum].mshort_cs))
                            < 1.0 / _transformResult[chNum].mshort_cs)
                        {
                            mostIntenseIntensity = _cidPeaks[_transformResult[chNum].mint_peak_index].mdbl_intensity;
                        }
                    }
                    scan.MSPeaks.Add(new MSPeak(
                    Convert.ToSingle(_transformResult[chNum].mdbl_mono_mw),
                    _transformResult[chNum].mint_mono_intensity,
                    _transformResult[chNum].mshort_cs,
                    Convert.ToSingle(_transformResult[chNum].mdbl_mz),
                    Convert.ToSingle(_transformResult[chNum].mdbl_fit),
                    Convert.ToSingle(_transformResult[chNum].mdbl_most_intense_mw),
                    mostIntenseIntensity,
                    sumintensity
                    ));
                }
                Array.Clear(_transformResult, 0, _transformResult.Length);
                Array.Clear(_cidPeaks, 0, _cidPeaks.Length);
                Array.Clear(_cidMzs, 0, _cidMzs.Length);
                Array.Clear(_cidIntensities, 0, _cidIntensities.Length);
            }
            return scan;
        }

        /// <summary>
        /// No HCD info the return will be null
        /// </summary>
        /// <param name="argReader"></param>
        /// <param name="argScanNo"></param>
        /// <returns></returns>
        private static HCDInfo GetHCDInfo(GlypID.Readers.clsRawData argReader, int argScanNo)
        {
            if(!argReader.IsHCDScan(argScanNo))
            {
                return null;
            }
            HCDInfo HCDinfo = null;
            double _hcd_score;
            GlypID.enmGlycanType _gType; 
            int ParentScan = argReader.GetParentScan(argScanNo);
            // Scorers and transforms
            GlypID.HCDScoring.clsHCDScoring HCDScoring = new GlypID.HCDScoring.clsHCDScoring();
            GlypID.HornTransform.clsHornTransform Transform = new GlypID.HornTransform.clsHornTransform();

            // mzs , intensities
            float[] hcd_mzs = null;
            float[] hcd_intensities = null;
            float[] parent_mzs = null;
            float[] parent_intensities = null;

            // Peaks
            GlypID.Peaks.clsPeak[] parent_peaks;
            GlypID.Peaks.clsPeak[] hcd_peaks;

            // Peak Processors 
            GlypID.Peaks.clsPeakProcessor hcdPeakProcessor = new GlypID.Peaks.clsPeakProcessor();
            GlypID.Peaks.clsPeakProcessor parentPeakProcessor = new GlypID.Peaks.clsPeakProcessor();

            // Results
            GlypID.HCDScoring.clsHCDScoringScanResults[] hcd_scoring_results;
            GlypID.HornTransform.clsHornTransformResults[] transform_results;

            // Params
            GlypID.Scoring.clsScoringParameters scoring_parameters = new GlypID.Scoring.clsScoringParameters();
            GlypID.HornTransform.clsHornTransformParameters transform_parameters = new GlypID.HornTransform.clsHornTransformParameters();
            scoring_parameters.MinNumPeaksToConsider = 2;
            scoring_parameters.PPMTolerance = 10;
            scoring_parameters.UsePPM = true;

            // Init
            Transform.TransformParameters = transform_parameters;

            // Loading parent
            int parent_scan = argReader.GetParentScan(argScanNo);
            double parent_mz = argReader.GetParentMz(argScanNo);
            int scan_level = argReader.GetMSLevel(argScanNo);
            int parent_level = argReader.GetMSLevel(parent_scan);
            argReader.GetSpectrum(parent_scan, ref parent_mzs, ref parent_intensities);


            // Parent processing
            parent_peaks = new GlypID.Peaks.clsPeak[1];
            parentPeakProcessor.ProfileType = GlypID.enmProfileType.PROFILE;
            parentPeakProcessor.DiscoverPeaks(ref parent_mzs, ref parent_intensities, ref parent_peaks,
                        Convert.ToSingle(transform_parameters.MinMZ), Convert.ToSingle(transform_parameters.MaxMZ), true);
            double bkg_intensity = parentPeakProcessor.GetBackgroundIntensity(ref parent_intensities);
            double min_peptide_intensity = bkg_intensity * transform_parameters.PeptideMinBackgroundRatio;


            transform_results = new GlypID.HornTransform.clsHornTransformResults[1];
            bool found = Transform.FindPrecursorTransform(Convert.ToSingle(bkg_intensity), Convert.ToSingle(min_peptide_intensity), ref parent_mzs, ref parent_intensities, ref parent_peaks, Convert.ToSingle(parent_mz), ref transform_results);
            if (!found && (argReader.GetMonoChargeFromHeader(ParentScan) > 0))
            {
                found = true;
                double mono_mz = argReader.GetMonoMzFromHeader(ParentScan);
                if (mono_mz == 0)
                    mono_mz = parent_mz;

                short[] charges = new short[1];
                charges[0] = argReader.GetMonoChargeFromHeader(ParentScan);
                Transform.AllocateValuesToTransform(Convert.ToSingle(mono_mz), 0, ref charges, ref transform_results); // Change abundance value from 0 to parent_intensity if you wish
            }
            if (found && transform_results.Length == 1)
            {
                // Score HCD scan first
                argReader.GetSpectrum(argScanNo, ref hcd_mzs, ref hcd_intensities);
                double hcd_background_intensity = GlypID.Utils.GetAverage(ref hcd_intensities, ref  hcd_mzs, Convert.ToSingle(scoring_parameters.MinHCDMz), Convert.ToSingle(scoring_parameters.MaxHCDMz));
                hcdPeakProcessor.SetPeakIntensityThreshold(hcd_background_intensity);
                hcd_peaks = new GlypID.Peaks.clsPeak[1];

                //Check Header
                string Header = argReader.GetScanDescription(argScanNo);
                hcdPeakProcessor.ProfileType = GlypID.enmProfileType.PROFILE;
                if (Header.Substring(Header.IndexOf("+") + 1).Trim().StartsWith("c"))
                {
                    hcdPeakProcessor.ProfileType = GlypID.enmProfileType.CENTROIDED;
                }

                hcdPeakProcessor.DiscoverPeaks(ref hcd_mzs, ref hcd_intensities, ref hcd_peaks, Convert.ToSingle
                    (scoring_parameters.MinHCDMz), Convert.ToSingle(scoring_parameters.MaxHCDMz), false);
                hcdPeakProcessor.InitializeUnprocessedData();

                hcd_scoring_results = new GlypID.HCDScoring.clsHCDScoringScanResults[1];

                HCDScoring.ScoringParameters = scoring_parameters;
                _hcd_score = HCDScoring.ScoreHCDSpectra(ref hcd_peaks, ref hcd_mzs, ref hcd_intensities, ref transform_results, ref hcd_scoring_results);
                _gType = (GlypID.enmGlycanType)hcd_scoring_results[0].menm_glycan_type;

                enumGlycanType GType; //Convert from GlypID.enumGlycanType to MassLib.enumGlycanType;
                if (_gType == GlypID.enmGlycanType.CA)
                {
                    GType = enumGlycanType.CA;
                }
                else if (_gType == GlypID.enmGlycanType.CS)
                {
                    GType = enumGlycanType.CS;
                }
                else if (_gType == GlypID.enmGlycanType.HM)
                {
                    GType = enumGlycanType.HM;
                }
                else if (_gType == GlypID.enmGlycanType.HY)
                {
                    GType = enumGlycanType.HY;
                }
                else
                {
                    GType = enumGlycanType.NA;
                }
                
                HCDinfo = new HCDInfo(argScanNo, GType, _hcd_score);

            }

            return HCDinfo;

        }
    
    }
}
