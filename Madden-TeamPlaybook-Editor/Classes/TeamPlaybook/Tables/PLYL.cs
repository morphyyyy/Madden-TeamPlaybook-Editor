using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TDBAccess;

namespace Madden.TeamPlaybook
{
    [Serializable]
    public class PLYL
    {
        public static readonly Dictionary<int, string> BlockingScheme = new Dictionary<int, string>
        {
            {-1, "Invalid"},
            {0, "Code Determined"},
            {1, "Inside Zone"},
            {2, "Outside Zone"},
            {3, "Misdirection"},
            {4, "Pitch"},
            {5, "Iso"},
            {6, "Trap"},
            {7, "Counter"},
            {8, "Power"},
            {9, "Pass"},
            {10, "Zone Read"},
            {11, "Outside Zone 2 TE"},
            {12, "Zone Read TE"},
            {13, "Speed Option"},
            {14, "Speed Option TE"},
            {15, "Power One Back"},
            {16, "Power 2 TE"},
            {17, "Speed Option 2 TE"},
            {18, "Zone Read 2 TE"},
            {19, "Inside Zone 2 TE"},
            {20, "Power Sweep TE"},
            {21, "Inside Zone One Back"},
            {22, "FB Dive"},
            {23, "Mid Line"},
            {24, "Inside Zone Read"},
            {25, "Inside Veer"},
            {26, "QB Wrap"},
            {27, "Gun Power"},
            {28, "Fold"},
            {29, "PA Screen"},
            {30, "Reverse Blocking"},
            {31, "Dart"},
            {32, "Gun Mid Line"},
            {33, "Strong Power"},
            {34, "Power 2 Back"},
            {35, "Inside Zone Insert"},
            {36, "Inside Zone Easy"},
            {37, "Wham"},
            {38, "Counter Easy"},
            {39, "Counter Easy TE"},
            {40, "Power G"},
            {41, "Stretch Arc"},
            {42, "Fake Field Goal Run"},
            {43, "Fake Punt Run"},
            {44, "PA Inside Zone"},
            {45, "Inside Zone Split"},
            {46, "Inside Zone Willy"},
            {47, "Strong Counter"},
            {48, "Duo"},
            {49, "Code Outside Zone"},
            {50, "End Around Lead"},
            {51, "Outside Zone Backside Lock"},
            {52, "Pin Pull"},
            {53, "Crack Toss"}
        };
        public static readonly Dictionary<int, string> PlayListFlag = new Dictionary<int, string>
        {
            {1, "CanFlip"},
            {2, "RunLeft"},
            {4, "RunMiddle"},
            {8, "RunRight"},
            {16, "PassShort"},
            {32, "PassMedium"},
            {64, "PassLong"},
            {128, "PassLeft"},
            {256, "PassMiddle"},
            {512, "PassRight"},
            {1024, "QBScrambleStop"},
            {2048, "NoPlaymaker"},
            {4096, "DisableLockOn"},
            {8192, "Cover2Defense"},
            {16384, "PuntRight"},
            {32768, "PuntLeft"},
            {65536, "PuntSky"},
            {131072, "SnapToVIP"},
            {262144, "OptionStop"},
            {524288, "ContainLeft"},
            {1048576, "ContainRight"},
            {2097152, "OutsideRunStop"},
            {4194304, "BlockAndRelease"},
            {8388608, "CoverageShell"},
            {16777216, "BlockLeft"},
            {33554432, "BlockRight"},
            {67108864, "IsBoosted"},
            {134217728, "StartComplement"},
            {268435456, "EndComplement"},
            {536870912, "NoWideAlign"}
        };

        public int rec { get; set; }
        /// <summary>
        /// SETL.setl
        /// </summary>
        public int SETL { get; set; }
        /// <summary>
        /// PBPL.PLYL, PLPD.PLYL, PLRD.PLYL, PLYS.PLYL, PLCM.PLYL, PPCT.plyl, SDEF.plyl, SRFT.PLYL
        /// </summary>
        public int plyl { get; set; }
        public int SRMM { get; set; }
        /// <summary>
        /// SETL.SITT
        /// </summary>
        public int SITT { get; set; }
        /// <summary>
        /// PBCC.PTYP
        /// </summary>
        public int PLYT { get; set; }
        /// <summary>
        /// PBAI.PLF_
        /// </summary>
        public int PLF_ { get; set; }
        /// <summary>
        /// PBPL.name
        /// </summary>
        public string name { get; set; }
        public int risk { get; set; }
        public int motn { get; set; }
        public int phlp { get; set; }
        /// <summary>
        /// PBAI.vpos
        /// </summary>
        public int vpos { get; set; }

        public override string ToString()
        {
            return
                "Rec#: " + rec +
                "   SETL: " + SETL +
                "   PLYL: " + plyl +
                "   SRMM: " + SRMM +
                "   SITT: " + SITT +
                "   PLYT: " + PLYT +
                "   PLF_: " + PLF_ +
                "   Name: " + name +
                "   risk: " + risk +
                "   motn: " + motn +
                "   phlp: " + phlp +
                "   vpos: " + vpos;
        }

        public static List<PLYL> GetPLYL(int filter = 0, int DBIndex = 0)
        {
            List<PLYL> _PLYL = new List<PLYL>();

            TdbTableProperties TableProps = new TdbTableProperties();
            TableProps.Name = new string((char)0, 5);

            // Get Tableprops based on the selected index
            if (!TDB.TDBTableGetProperties(DBIndex, TableNames.GetTables().Find(item => item.name == "PLYL").rec, ref TableProps))
                return null;

            List<TdbFieldProperties> tableFields = new List<TdbFieldProperties>();

            for (int i = 0; i < TableProps.FieldCount; i++)
            {
                TdbFieldProperties FieldProps = new TdbFieldProperties();
                FieldProps.Name = new string((char)0, 5);
                TDB.TDBFieldGetProperties(DBIndex, TDB.StrReverse("PLYL"), i, ref FieldProps);
                tableFields.Add(FieldProps);
            }

            for (int i = 0; i < TableProps.RecordCount; i++)
            {
                string _name = new string((char)0, (tableFields.FirstOrDefault(field => field.Name == TDB.StrReverse("name")).Size / 8) + 1);

                TDB.TDBFieldGetValueAsString(DBIndex, TDB.StrReverse("PLYL"), TDB.StrReverse("name"), i, ref _name);
                _name = _name.Replace(",", "");

                _PLYL.Add(new PLYL
                {
                    rec = i,
                    SETL = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLYL"), TDB.StrReverse("SETL"), i),
                    plyl = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLYL"), TDB.StrReverse("PLYL"), i),
                    SRMM = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLYL"), TDB.StrReverse("SRMM"), i),
                    SITT = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLYL"), TDB.StrReverse("SITT"), i),
                    PLYT = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLYL"), TDB.StrReverse("PLYT"), i),
                    PLF_ = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLYL"), TDB.StrReverse("PLF_"), i),
                    name = _name,
                    risk = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLYL"), TDB.StrReverse("risk"), i),
                    motn = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLYL"), TDB.StrReverse("motn"), i),
                    phlp = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLYL"), TDB.StrReverse("phlp"), i),
                    vpos = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLYL"), TDB.StrReverse("vpos"), i)
                });
            }
            return _PLYL;
        }

        public static void SetPLYL(List<PLYL> PLYL, int DBIndex = 0)
        {
            TdbTableProperties TableProps = new TdbTableProperties();
            TableProps.Name = new string((char)0, 5);

            // Get Tableprops based on the selected index
            TDB.TDBTableGetProperties(DBIndex, TableNames.GetTables().Find(item => item.name == "PLYL").rec, ref TableProps);

            if (PLYL.Count > TableProps.RecordCount)
                for (int i = TableProps.RecordCount - 1; i < PLYL.Count - 1; i++)
                {
                    TDB.TDBTableRecordAdd(DBIndex, TDB.StrReverse("PLYL"), true);
                }
            else if (PLYL.Count < TableProps.RecordCount)
            {
                for (int i = TableProps.RecordCount - 1; i > PLYL.Count - 1; i--)
                {
                    TDB.TDBTableRecordRemove(DBIndex, TDB.StrReverse("PLYL"), i);
                }
            }

            foreach (PLYL item in PLYL)
            {
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PLYL"), TDB.StrReverse("SETL"), item.rec, item.SETL);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PLYL"), TDB.StrReverse("PLYL"), item.rec, item.plyl);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PLYL"), TDB.StrReverse("SRMM"), item.rec, item.SRMM);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PLYL"), TDB.StrReverse("SITT"), item.rec, item.SITT);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PLYL"), TDB.StrReverse("PLYT"), item.rec, item.PLYT);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PLYL"), TDB.StrReverse("PLF_"), item.rec, item.PLF_);
                TDB.TDBFieldSetValueAsString(DBIndex, TDB.StrReverse("PLYL"), TDB.StrReverse("name"), item.rec, item.name);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PLYL"), TDB.StrReverse("risk"), item.rec, item.risk);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PLYL"), TDB.StrReverse("motn"), item.rec, item.motn);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PLYL"), TDB.StrReverse("phlp"), item.rec, item.phlp);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PLYL"), TDB.StrReverse("vpos"), item.rec, item.vpos);
            }
        }

        public static List<PLYL> Sort(List<PLYL> PLYL)
        {
            return PLYL.OrderBy(s => s.plyl).Cast<PLYL>().ToList();
        }
    }
}
