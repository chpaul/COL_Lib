using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Markup;
using COL.MassLib;
namespace COL.GlycoLib
{
    [Serializable]
    public class GlycanCompound : IComparable
    {
   
        //private static double Na = 22.9899;
        int _Carbon;
        int _Carbon13;
        int _Hydrogen;
        int _Oxygen;
        int _Nitrogen;
        int _Sodium;
        int _Deuterium;
        int _HexNAc; //Mannose , Galactose C6H12O6 
        int _Hex; //	N-Acetylglucosamine C8H15NO6
        int _DeHex; //Fucose C6H12O5        
        int _Sia; //N-Acetylneuraminic acid 	C11H19NO9 (human)   N-glycolylneuraminic acid  C11H19NO10   (Other mammals)
        int _LCorder;
        bool _Permethylated =false;
        bool _isSodium=false;
        bool _Human=true;
        bool _isReducedReducingEnd=false;
        bool _isDeuterium=false;
        double _MonoMass;
        double _AVGMass;
        enumGlycanLabelingMethod _LabelingMethood;
        enumLabelingTag _LabelingTag;
        List<Tuple<string,float,int>> _Adducts; //Charge Proton should include in this list
        private float _LinearRegSlope;
        private float _LinearRegIntercept ;
        private bool _HasLinearRegParemeters = false;
        public GlycanCompound(int argHexNac, int argHex, int argDeHex, int argSialic)
        : this(argHexNac, argHex, argDeHex, argSialic, false, false, false, false, false) {}
        
        public GlycanCompound(int argHexNac, int argHex, int argDeHex, int argSialic, bool argIsPermethylated, bool argIsDeuterium, bool argReducedReducingEnd, bool argIsSodium, bool argIsHuman)
            : this(argHexNac, argHex, argDeHex, argSialic, argIsPermethylated, argIsDeuterium, argReducedReducingEnd, argIsSodium, argIsHuman, null, enumLabelingTag.None) { }

        public GlycanCompound(int argHexNac, int argHex, int argDeHex, int argSialic, bool argIsPermethylated, bool argIsDeuterium, bool argReducedReducingEnd, bool argIsSodium, bool argIsHuman, List<Tuple<string, float, int>> argAdducts, enumLabelingTag argLabelingTag)
        {
            _HexNAc = argHexNac;
            _Hex = argHex;
            _DeHex = argDeHex;
            _Sia = argSialic;
            _Human = argIsHuman;
            _Permethylated = argIsPermethylated;

            _isSodium = argIsSodium;
            _isReducedReducingEnd = argReducedReducingEnd;
            _isDeuterium = argIsDeuterium;
            _LabelingTag = argLabelingTag;
            _Adducts = argAdducts;
            CalcAtom();
            CalcMass();
            CalcAVGMass();
        }
        public List<Tuple<string, float, int>> Adducts
        {
            get { return _Adducts; }
        }
        private float AdductMass
        {
            get {
                float TotalMass = 0;
                foreach (Tuple<string, float, int> adduct in _Adducts)
                {
                    TotalMass = TotalMass + adduct.Item2 * adduct.Item3;
                }                
                return TotalMass;
            }
        }
        public double MZ
        {
            get
            {
                CalcAtom();
                CalcMass();
                if (Charge != 0)
                {
                    return (_MonoMass + AdductMass) / (double)Charge;
                }
                else
                {
                    return _MonoMass;
                }
            }
        }
       public int Charge
       {
           get {
               int _charge = 0;
               foreach (Tuple<string, float, int> adduct in _Adducts)
               {
                   _charge = _charge + adduct.Item3;
               }
               return _charge;
           }
           
       }
       public enumGlycanLabelingMethod LabelingMethod
       {
           get { return _LabelingMethood; }
           set { _LabelingMethood = value; }
       }
       public enumLabelingTag LabelingTag
       {
           get { return _LabelingTag; }
           set { _LabelingTag = value;
               if(_LabelingTag.ToString().Contains("DRAG_"))
               {
                   _LabelingMethood = enumGlycanLabelingMethod.DRAG;
               }
               else if (_LabelingTag.ToString().Contains("MP_"))
               {
                   _LabelingMethood = enumGlycanLabelingMethod.MultiplexPermethylated;
               }

                   CalcAtom();
                   CalcMass();
                   CalcAVGMass();
           }
       }
        public bool isHuman
        {
            get { return _Human; }
            set { _Human = value; }
        }

        public bool HasLinearRegressionParameters
        {
            get { return _HasLinearRegParemeters; }
            set { _HasLinearRegParemeters = value; }
        }
        public float LinearRegSlope
        {
            get { return _LinearRegSlope; }
            set
            {
                _HasLinearRegParemeters = true;
                _LinearRegSlope = value;
            }
        }

        public float LinearRegIntercept
        {
            get { return _LinearRegIntercept; }
            set
            {
                _HasLinearRegParemeters = true;
                _LinearRegIntercept = value;
            }
        }
        public int NumOfPermethlationSites
        {
            get
            {
                if (!isHuman) //NeuGc has 6 permeth sites
                {
                    return NoOfHex * 3 +NoOfHexNAc * 3 +NoOfDeHex * 2 +NoOfSia * 6;
                }
                else
                {
                    return NoOfHex * 3 +NoOfHexNAc * 3 +NoOfDeHex * 2 +NoOfSia * 5;
                }
            }
        }
        public double MonoMass
        {
            get
            {
                if (_MonoMass == 0.0)
                {
                    CalcMass();
                }
                return _MonoMass;
            }
        }
        public double AVGMass
        {
            get
            {
                if (_AVGMass == 0.0)
                {
                    CalcAVGMass();
                }
                return _AVGMass;
            }
        }
        private void CalcMass()
        {
            _MonoMass = _Carbon * Atoms.CarbonMass +
                                    _Carbon13 * Atoms.Carbon13Mass + 
                                    _Hydrogen * Atoms.HydrogenMass +
                                    _Nitrogen * Atoms.NitrogenMass +
                                    _Oxygen * Atoms.OxygenMass +
                                    _Sodium * Atoms.SodiumMass +
                                    _Deuterium * Atoms.DeuteriumMass;
                                    
         }
        private void CalcAVGMass()
        {
            _AVGMass = _Carbon * Atoms.CarbonAVGMass + 
                                   _Carbon13 * Atoms.Carbon13AVGMass + 
                                   _Hydrogen * Atoms.HydrogenAVGMass +
                                   _Nitrogen * Atoms.NitrogenAVGMass + 
                                   _Oxygen * Atoms.OxygenAVGMass + 
                                   _Sodium * Atoms.SodiumMass + 
                                   _Deuterium * Atoms.DeuteriumMass;
        }

        public double MassWithoutWater
        {
            get { return _MonoMass - Atoms.HydrogenMass*2 - Atoms.OxygenMass; }
        }
        public bool isPermethylated
        {
            get { return _Permethylated; }
        }
        public bool isReducedReducingEnd
        {
            get { return _isReducedReducingEnd; }
        }
        public bool isSodium
        {
            get { return _isSodium; }
        }
        public int NoOfHexNAc
        {
            get { return _HexNAc; }
        }
        public int NoOfHex
        {
            get { return _Hex; }
        }
        public int NoOfSia
        {
            get{ return _Sia; }
        }
        public int NoOfDeHex
        {
            get { return _DeHex; }
         }
        public int Sodium
        {
            get{return _Sodium;}
        }
        public int Nitrogen
        {
            get{return _Nitrogen;}
        }
        public int Carbon
        {
            get{return _Carbon;}
        }
        public int Carbon13
        {
            get { return _Carbon13; }
        }
        public int Hydrogen
        {
            get{return _Hydrogen;}
        }
        public int Oxygen
        {
            get{return _Oxygen;}
        }
        public int Deuterium
        {
            get { return _Deuterium; }            
        }
        public int GlycanLCorder
        {
            get { return _LCorder;}
            set { _LCorder = value; }
        }
        public string GlycanKey
        {
            get
            {
                if (_Human)
                {
                    return _HexNAc.ToString() + "-" +
               _Hex.ToString() + "-" +
               _DeHex.ToString() + "-" +
               _Sia.ToString()+"-0";
                }
                else
                {
                    return _HexNAc.ToString() + "-" +
                   _Hex.ToString() + "-" +
                   _DeHex.ToString() + "-0-" +
                   _Sia.ToString();
                }
 
            }
        }

        public bool IsGlycanWithInLinearRegLCTime(float argTotalLCTime,float argTolrenaceTime, float argIdentifiedTime)
        {
            if (_LinearRegIntercept == null || LinearRegSlope == null)
            {
                return false;
            }
            float expectedTime = _LinearRegSlope*argTotalLCTime + _LinearRegIntercept;
            if (Math.Abs(expectedTime - argIdentifiedTime)/argTotalLCTime <= argTolrenaceTime)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        ///http://www.expasy.ch/tools/glycomod/glycomod_masses.html
        ///                                                 Native                  Permethylated
        ///Hexose (Hex) Mannose                    Man      C6H10O5  	162.0528    C9H16O5     204.0998    - 3 sites can be Permethylated
        ///Hexose (Hex) Galactose                  Gal      C6H10O5  	162.0528    C9H16O5     204.0998
        ///HexNAc       N-Acetylglucosamine 	   GlcNAC   C8H13NO5    203.0794    C11H19NO5   245.1263   - 3 sites can be Permethylated
        ///Deoxyhexose  Fucose                     Fuc      C6H10O4     146.0579    C8H14O4     174.0892   - 2 sites can be premethylated
        ///NeuAc        N-Acetylneuraminic acid    NeuNAc   C11H17NO8   291.0954    C16H27NO8   361.1737  - 5 sites can be Permethylated
        ///NeuGc        N-glycolylneuraminic acid  NeuNGc   C11H17NO9   307.0903    C17H29NO9   391.1842  - 6 sites can be Permethylated
        ///Permethylated -H ->CH3;
        /// </summary>
        private void CalcAtom()
        {    
            if (_Permethylated)
            {
                _Carbon = 9 * _Hex + 11 * _HexNAc + 8 * _DeHex;
                _Hydrogen = 16 * _Hex + 19 * _HexNAc + 14 * _DeHex;
                _Oxygen = 5 * _Hex + 5 * _HexNAc + 4 * _DeHex;
                _Nitrogen = 1 * _HexNAc + 1 * _Sia;

                if (_isDeuterium) //Replace permethlation hydrogen to Deutrtium
                {
                    _Hydrogen = 10 * _Hex + 13 * _HexNAc + 10 * _DeHex;
                    _Deuterium = 6 * _Hex + 6 * _HexNAc + 4 * _DeHex;
                }

                if (_Human)
                {
                    _Carbon = _Carbon + 16 * _Sia;
                    _Hydrogen = _Hydrogen + 27 * _Sia;
                    _Oxygen = _Oxygen + 8 * _Sia;
                }
                else
                {
                    _Carbon = _Carbon + 17 * _Sia;
                    _Hydrogen = _Hydrogen + 29 * _Sia;
                    _Oxygen = _Oxygen + 9 * _Sia;
                }
                //Nonreducing end  -CH3
                _Carbon = _Carbon + 1;
                _Hydrogen = _Hydrogen + 3;
                if (_isReducedReducingEnd) //C2OH7
                {
                    _Carbon = _Carbon + 2;
                    _Oxygen = _Oxygen + 1;
                    _Hydrogen = _Hydrogen + 7;
                }
                else //COH3
                {
                    _Carbon = _Carbon + 1;
                    _Oxygen = _Oxygen + 1;
                    _Hydrogen = _Hydrogen + 3;
                }
                //Labeling
                switch (_LabelingTag)
                {
                    case enumLabelingTag.MP_CH2D:
                        if (_Human)
                        {
                            _Deuterium = _Hex * 3 + _HexNAc * 3 + _DeHex * 2 + _Sia * 5 + 3; //3: 1 NonReducing End  + 2 ReducedReducing End
                            _Hydrogen = _Hydrogen - _Deuterium;                            
                        }
                        else
                        {
                            _Deuterium = _Hex * 3 + _HexNAc * 3 + _DeHex * 2 + _Sia * 6 +3;
                            _Hydrogen = _Hydrogen - _Deuterium;
                        }
                        break;
                    case enumLabelingTag.MP_CHD2:
                        if (_Human)
                        {
                            _Deuterium = (_Hex * 3 + _HexNAc * 3 + _DeHex * 2 + _Sia * 5 + 3) * 2;
                            _Hydrogen = _Hydrogen - _Deuterium;
                        }
                        else
                        {
                            _Deuterium = (_Hex * 3 + _HexNAc * 3 + _DeHex * 2 + _Sia * 6 +3)*2;
                            _Hydrogen = _Hydrogen - _Deuterium;
                        }
                        break;
                    case enumLabelingTag.MP_CD3:
                        if (_Human)
                        {
                            _Deuterium = (_Hex * 3 + _HexNAc * 3 + _DeHex * 2 + _Sia * 5 +3) * 3;
                            _Hydrogen = _Hydrogen - _Deuterium;
                        }
                        else
                        {
                            _Deuterium = (_Hex * 3 + _HexNAc * 3 + _DeHex * 2 + _Sia * 6+3) * 3;
                            _Hydrogen = _Hydrogen - _Deuterium;
                        }
                        break;
                    case enumLabelingTag.MP_13CH3:
                        if (_Human)
                        {
                            _Deuterium = 0;
                            _Carbon13 = _Hex * 3 + _HexNAc * 3 + _DeHex * 2 + _Sia * 5 +3;
                            _Carbon = _Carbon - _Carbon13;
                        }
                        else
                        {
                            _Deuterium = 0;
                            _Carbon13 = _Hex * 3 + _HexNAc * 3 + _DeHex * 2 + _Sia * 6 +3;
                            _Carbon = _Carbon - _Carbon13;
                        }
                        break;
                    case enumLabelingTag.MP_13CHD2:
                        if (_Human)
                        {
                            _Deuterium = (_Hex * 3 + _HexNAc * 3 + _DeHex * 2 + _Sia * 5 +3)*2;
                            _Hydrogen = _Hydrogen - _Deuterium;         
                            _Carbon13 = _Hex * 3 + _HexNAc * 3 + _DeHex * 2 + _Sia * 5+3;
                            _Carbon = _Carbon - _Carbon13;
                        }
                        else
                        {
                            _Deuterium = (_Hex * 3 + _HexNAc * 3 + _DeHex * 2 + _Sia * 6 +3)*2;
                            _Hydrogen = _Hydrogen - _Deuterium;
                            _Carbon13 = _Hex * 3 + _HexNAc * 3 + _DeHex * 2 + _Sia * 6 +3;
                            _Carbon = _Carbon - _Carbon13;
                        }
                        break;
                    case enumLabelingTag.MP_13CD3:
                        if (_Human)
                        {
                            _Deuterium = (_Hex * 3 + _HexNAc * 3 + _DeHex * 2 + _Sia * 5 + 3)*3;
                            _Hydrogen = _Hydrogen - _Deuterium;
                            _Carbon13 = _Hex * 3 + _HexNAc * 3 + _DeHex * 2 + _Sia * 5 +3;
                            _Carbon = _Carbon - _Carbon13;
                        }
                        else
                        {
                            _Deuterium = (_Hex * 3 + _HexNAc * 3 + _DeHex * 2 + _Sia * 6 +3)*3;
                            _Hydrogen = _Hydrogen - _Deuterium;
                            _Carbon13 = _Hex * 3 + _HexNAc * 3 + _DeHex * 2 + _Sia * 6 +3;
                            _Carbon = _Carbon - _Carbon13;
                        }
                        break;
                }
            }
            else
            {
                _Carbon = 6 * _Hex + 8 * _HexNAc + 6 * _DeHex;
                _Hydrogen = 10 * _Hex + 13 * _HexNAc + 10 * _DeHex;
                _Oxygen = 5 * _Hex + 5 * _HexNAc + 4 * _DeHex;
                _Nitrogen = 1 * _HexNAc + 1 * _Sia;

                if (_Human)
                {
                    _Carbon = _Carbon + 11 * _Sia;
                    _Hydrogen = _Hydrogen + 17 * _Sia;
                    _Oxygen = _Oxygen + 8 * _Sia;
                }
                else
                {
                    _Carbon = _Carbon + 11 * _Sia;
                    _Hydrogen = _Hydrogen + 17 * _Sia;
                    _Oxygen = _Oxygen + 9 * _Sia;
                }
                //Nonreducing end -H
                _Hydrogen = _Hydrogen + 1;
                if (_isReducedReducingEnd) //OH3
                {
                    _Oxygen = _Oxygen + 1;
                    _Hydrogen = _Hydrogen + 3;
                }
                else //OH
                {
                    _Oxygen = _Oxygen + 1;
                    _Hydrogen = _Hydrogen + 1;
                }
                switch (_LabelingTag)
                {
                    /*DRAG
                     *   Light TAG: C8H10N2 
                     *   Heavy TAG: 13C6  C2H10N2
                     *   NeuAC: +CH3N1   -O                        
                     */
                    case enumLabelingTag.DRAG_Heavy:
                        _Carbon = _Carbon + 2 +( _Sia * 1);
                        _Hydrogen = _Hydrogen + 10 + (_Sia * 3);
                        _Nitrogen = _Nitrogen + 2 + (_Sia * 1);
                        _Oxygen = _Oxygen - (_Sia * 1);
                        _Carbon13 = 6;
                        break;
                    case enumLabelingTag.DRAG_Light:
                         _Carbon = _Carbon + 8 + (_Sia * 1);
                        _Hydrogen = _Hydrogen + 10 + (_Sia * 3);
                        _Nitrogen = _Nitrogen + 2 +( _Sia * 1);
                        _Oxygen = _Oxygen - (_Sia * 1);
                        break;
                }
            }
            if (_isSodium)
            {
                _Sodium = 1;
            }            
        }
        public int CompareTo(object obj)
        {
            if (obj is GlycanCompound)
            {
                GlycanCompound p2 = (GlycanCompound)obj;
                return _MonoMass.CompareTo(p2.MonoMass);
            }
            else
                throw new ArgumentException("Object is not a Compound.");
        }
        public object Clone()
        {
            MemoryStream ms = new MemoryStream();
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(ms, this);
            ms.Position = 0;
            object obj = bf.Deserialize(ms);
            ms.Close();
            return obj;
        }
    }
    
}
