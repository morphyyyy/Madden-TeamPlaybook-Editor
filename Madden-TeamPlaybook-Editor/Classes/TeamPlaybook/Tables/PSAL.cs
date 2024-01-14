using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using TDBAccess;

namespace Madden.TeamPlaybook
{
    [Serializable]
    public class PSAL
    {
        public static readonly Dictionary<KeyValuePair<int, string>, Dictionary<KeyValuePair<string, string>, Dictionary<int, string>>> CodeDefinition = new Dictionary<KeyValuePair<int, string>, Dictionary<KeyValuePair<string, string>, Dictionary<int, string>>>
        {
            { 
                new KeyValuePair<int, string>(26, "Initial Animation"),
                new Dictionary<KeyValuePair<string, string>, Dictionary<int, string>>
                {
                    { 
                        new KeyValuePair<string, string>("val1", "Animation Type"),
                        new Dictionary<int, string>
                        {
                            { 0, "Shuffle" },
                            { 1, "Backstep" },
                            { 2, "Hopstep" },
                            { 3, "Counterstep" },
                            { 4, "Power Pull" },
                            { 5, "WR Start" },
                            { 6, "HB Rec Pitch" },
                            { 7, "QB Option" },
                            { 8, "QB Option Counter" },
                            { 9, "QB Speed Option" },
                            { 10, "FB Triple Option" },
                            { 11, "Def Flatzone Left" },
                            { 12, "Def Drop Hookzone" },
                            { 13, "Def Blitz" },
                            { 14, "Def Blitz Drop" },
                            { 15, "Def DL Backpedal" },
                            { 16, "Def DL Turn & Run" },
                            { 17, "Def DL Stunt" },
                            { 18, "Def DL Stunt Loop" },
                            { 19, "Def DL Fireoff" },
                            { 20, "FB Receive Handoff" },
                            { 21, "FB Lead Block" },
                            { 22, "HB Lateral Step" },
                            { 23, "Skip Pull" },
                            { 24, "Truck Pull" },
                            { 25, "Trap Pull" },
                            { 26, "Screen Pull" },
                            { 27, "Counter Pull" },
                            { 28, "FB Counter 2pt" },
                            { 29, "FB Counter 3pt" },
                            { 30, "FB Counter 3pt Left" },
                            { 31, "WR Start Quick" },
                            { 32, "Run Block" },
                            { 33, "2pt Pass Block Passive" },
                            { 34, "2pt Pass Block Aggressive" },
                            { 35, "2pt Pass Block Slide" },
                            { 36, "3pt Pass Block Passive" },
                            { 37, "3pt Pass Block Aggressive" },
                            { 38, "3pt Pass Block Slide" },
                            { 39, "2pt FG Block" },
                            { 40, "DL Pass Rush" },
                            { 41, "DL Drop" },
                            { 42, "HB Run Route" },
                            { 43, "Lateral Run Block" },
                            { 44, "Kickoff Coverage" },
                            { 45, "Def DL Stand" },
                            { 46, "Def Backside Contain" },
                            { 47, "Def DL Option Read" },
                            { 48, "RB Outside Right" },
                            { 49, "RB Outside Left" },
                            { 50, "RB Right to Middle" },
                            { 51, "RB Left to Middle" },
                            { 52, "RB Forward Fast" },
                            { 53, "RB Shotgun Right" },
                            { 54, "RB Shotgun Left" },
                            { 55, "RB Offset Right" },
                            { 56, "RB Offset Left" },
                            { 57, "FB Outside Right" },
                            { 58, "FB Outside Left" },
                            { 59, "FB Inside Right" },
                            { 60, "FB Inside Left" },
                            { 61, "FB Forward"}
                        }
                    }
                }
            },
            { 
                new KeyValuePair<int, string>(57, "Defensive Alignment"),
                new Dictionary<KeyValuePair<string, string>, Dictionary<int, string>>
                {
                    { 
                        new KeyValuePair<string, string>("val1", "Receiver SubPosition"),
                        new Dictionary<int, string>
                        {
                            { -1, "Invalid" },
                            { 0, "First" },
                            { 1, "WR" },
                            { 2, "TE" },
                            { 3, "RB" },
                            { 4, "WR Reduced Split" },
                            { 5, "WR Slot" },
                            { 6, "WR Tight Outside" },
                            { 7, "WR Tight Slot" },
                            { 8, "TE Twin Inside" },
                            { 9, "TE Twin Outside" },
                            { 10, "Bunch Outside" },
                            { 11, "Bunch Inside" },
                            { 12, "Bunch Middle" },
                            { 13, "Nub TE" },
                            { 14, "Trips Outside" },
                            { 15, "Trips Inside" },
                            { 16, "Trips Middle" },
                            { 17, "Stack Top" },
                            { 18, "Stack Bottom" },
                            { 19, "Stack Middle" },
                            { 20, "FB" },
                            { 21, "Num Positions"}
                        }
                    },
                    { 
                        new KeyValuePair<string, string>("val2", "Alignment Technique"),
                        new Dictionary<int, string>
                        {
                            {0, "No Align" },
                            {1, "Head Up" },
                            {2, "Inside 1" },
                            {3, "Outside 1" },
                            {4, "Outside 2" },
                            {5, "Outside 3" },
                            {6, "Outside 4" },
                            {7, "Outside 5" },
                            {8, "Split" },
                            {9, "Divider Rule" },
                            {10, "Inside Shoulder" },
                            {11, "Outside Shoulder"}
                        }
                    }
                }
            }
        };

        public int rec { get; set; }
        public int val1 { get; set; }
        public int val2 { get; set; }
        public int val3 { get; set; }
        /// <summary>
        /// PLYS.PSAL
        /// </summary>
        public int psal { get; set; }
        public int code { get; set; }
        public int step { get; set; }
        public static double AngleRatio = 0.35556;

        public override string ToString()
        {
            return
                "Rec#: " + rec +
                "   val1: " + val1 +
                "   val2: " + val2 +
                "   val3: " + val3 +
                "   psal: " + psal +
                "   code: " + code +
                "   step: " + step;
        }

        public static List<PSAL> GetPSAL(int filter = 0, int DBIndex = 0)
        {
            List<PSAL> _PSAL = new List<PSAL>();

            TdbTableProperties TableProps = new TdbTableProperties();
            TableProps.Name = new string((char)0, 5);

            // Get Tableprops based on the selected index
            if (!TDB.TDBTableGetProperties(DBIndex, TableNames.GetTables().Find(item => item.name == "PSAL").rec, ref TableProps))
                return null;

            for (int i = 0; i < TableProps.RecordCount; i++)
            {
                if (filter != 0 && (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PSAL"), TDB.StrReverse("PSAL"), i) == filter)
                {
                    _PSAL.Add(new PSAL
                    {
                        rec = i,
                        val1 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PSAL"), TDB.StrReverse("val1"), i),
                        val2 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PSAL"), TDB.StrReverse("val2"), i),
                        val3 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PSAL"), TDB.StrReverse("val3"), i),
                        psal = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PSAL"), TDB.StrReverse("PSAL"), i),
                        code = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PSAL"), TDB.StrReverse("code"), i),
                        step = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PSAL"), TDB.StrReverse("step"), i)
                    });
                }
                else if (filter == 0)
                {
                    _PSAL.Add(new PSAL
                    {
                        rec = i,
                        val1 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PSAL"), TDB.StrReverse("val1"), i),
                        val2 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PSAL"), TDB.StrReverse("val2"), i),
                        val3 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PSAL"), TDB.StrReverse("val3"), i),
                        psal = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PSAL"), TDB.StrReverse("PSAL"), i),
                        code = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PSAL"), TDB.StrReverse("code"), i),
                        step = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PSAL"), TDB.StrReverse("step"), i)
                    });
                }
            }

            //_PSAL = _PSAL.OrderBy(psal => psal.psal).ThenBy(psal => psal.step).ToList();
            return _PSAL;
        }

        public static void SetPSAL(List<PSAL> PSAL, int DBIndex = 0)
        {
            TdbTableProperties TableProps = new TdbTableProperties();
            TableProps.Name = new string((char)0, 5);

            // Get Tableprops based on the selected index
            TDB.TDBTableGetProperties(DBIndex, TableNames.GetTables().Find(item => item.name == "PSAL").rec, ref TableProps);

            if (PSAL.Count > TableProps.RecordCount)
                for (int i = TableProps.RecordCount - 1; i < PSAL.Count - 1; i++)
                {
                    TDB.TDBTableRecordAdd(DBIndex, TDB.StrReverse("PSAL"), true);
                }
            else if (PSAL.Count < TableProps.RecordCount)
            {
                for (int i = TableProps.RecordCount - 1; i > PSAL.Count - 1; i--)
                {
                    TDB.TDBTableRecordRemove(DBIndex, TDB.StrReverse("PSAL"), i);
                }
            }

            for (int i = 0; i < PSAL.Count; i++)
            {
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PSAL"), TDB.StrReverse("val1"), PSAL[i].rec, PSAL[i].val1);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PSAL"), TDB.StrReverse("val2"), PSAL[i].rec, PSAL[i].val2);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PSAL"), TDB.StrReverse("val3"), PSAL[i].rec, PSAL[i].val3);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PSAL"), TDB.StrReverse("PSAL"), PSAL[i].rec, PSAL[i].psal);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PSAL"), TDB.StrReverse("code"), PSAL[i].rec, PSAL[i].code);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PSAL"), TDB.StrReverse("step"), PSAL[i].rec, PSAL[i].step);
            }
        }

        public static List<PSAL> Sort(List<PSAL> PSAL)
        {
            return PSAL.OrderBy(s => s.psal).ThenBy(s => s.step).Cast<PSAL>().ToList();
        }

        public enum DoesPSALExist { No = 0, Yes = 1, IsIdentical = 2 }

        public bool IsIdentical(PSAL _psal)
        {
            return Convert.ToBoolean(
                ((psal == _psal.psal) ? 1 : 0) * 
                ((code == _psal.code) ? 1 : 0) * 
                ((step == _psal.step) ? 1 : 0) * 
                ((val1 == _psal.val1) ? 1 : 0) * 
                ((val2 == _psal.val2) ? 1 : 0) * 
                ((val3 == _psal.val3) ? 1 : 0)
                );
        }

        public bool IsIdentical(MaddenCustomPlaybookEditor.PSAL _psal)
        {
            return Convert.ToBoolean(
                ((psal == _psal.psal) ? 1 : 0) *
                ((code == _psal.code) ? 1 : 0) *
                ((step == _psal.step) ? 1 : 0) *
                ((val1 == _psal.val1) ? 1 : 0) *
                ((val2 == _psal.val2) ? 1 : 0) *
                ((val3 == _psal.val3) ? 1 : 0)
                );
        }
    }
}
