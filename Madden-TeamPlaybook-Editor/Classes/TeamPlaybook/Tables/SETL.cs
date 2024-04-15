using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TDBAccess;

namespace Madden.TeamPlaybook
{
    [Serializable]
    public class SETL
    {
        public static readonly Dictionary<int, string> Situation = new Dictionary<int, string>
        {
            {-1, "Situation_Invalid" },
            {1, "LongYardage"},
            {2, "GeneralPurpose" },
            {3, "ShortYardage" }
        };
        public static readonly Dictionary<int, string> SetListFlag = new Dictionary<int, string>
        {
            {1, "Can Flip"},
            {2, "No Wide Align" },
            {4, "Run Oriented" },
            {8, "Pass Oriented" },
            {16, "Special Oriented" },
            {32, "Prowl Defense" },
            {64, "Prevent Defense" },
            {128, "Non Symmetrical" },
            {256, "Close" },
            {512, "Tight" },
            {1024, "Bunch" },
            {2048, "Prevent Audible" },
            {16384, "DimebacK At Pos 5" }
        };
        public static readonly Dictionary<int, string> SetType = new Dictionary<int, string>
        {
            {4, "Big"},
            {9, "Bunch"},
            {10, "Empty"},
            {6, "Four WR"},
            {8, "Goalline"},
            {-1, "Invalid"},
            {3, "Jumbo"},
            {0, "None"},
            {2, "Normal"},
            {11, "Overload"},
            {5, "Three WR"},
            {7, "Trips"},
            {12, "Wildcat"}
        };
        public static readonly Dictionary<int, string> SetClassification = new Dictionary<int, string>
        {
            {17, "1 4 6"},
            {18, "2 4 6"},
            {14, "3 2 6"},
            {15, "3 3 5"},
            {13, "3 4"},
            {16, "4 2 5"},
            {12, "4 3"},
            {30, "4 4"},
            {31, "46"},
            {32, "5 2"},
            {20, "Dime"},
            {22, "Dollar"},
            {25, "FGBlock"},
            {24, "FGReturn"},
            {7, "FieldGoal"},
            {23, "GoallineDefense"},
            {4, "GoallineOffense"},
            {-1, "Invalid"},
            {9, "Kickoff"},
            {28, "KickReturn"},
            {33, "Kneel"},
            {19, "Nickel"},
            {11, "OnsideKick"},
            {29, "OnsideReturn"},
            {3, "Pistol"},
            {5, "Punt"},
            {27, "PuntBlock"},
            {6, "PuntProtect"},
            {26, "PuntReturn"},
            {21, "Quarter"},
            {10, "SafetyKickoff"},
            {1, "Shotgun"},
            {8, "Special"},
            {34, "StopClock"},
            {0, "Under Center"},
            {2, "Wildcat"}
        };

        public int rec { get; set; }
        /// <summary>
        /// PBAI.setl
        /// </summary>
        public int setl { get; set; }
        /// <summary>
        /// FORM.form
        /// </summary>
        public int FORM { get; set; }
        public int MOTN { get; set; }
        public int CLAS { get; set; }
        public int SETT { get; set; }
        /// <summary>
        /// PLYL.SITT
        /// </summary>
        public int SITT { get; set; }
        public int SLF_ { get; set; }
        /// <summary>
        /// PBST.name
        /// </summary>
        public string name { get; set; }
        public int poso { get; set; }

        public override string ToString()
        {
            return
                "Rec#: " + rec +
                "   SETL: " + setl +
                "   FORM: " + FORM +
                "   MOTN: " + MOTN +
                "   CLAS: " + CLAS +
                "   SETT: " + SETT +
                "   SITT: " + SITT +
                "   SLF_: " + SLF_ +
                "   Name: " + name +
                "   poso: " + poso;
        }

        public static List<SETL> GetSETL(int filter = 0, int DBIndex = 0)
        {
            List<SETL> _SubFormations = new List<SETL>();

            TdbTableProperties TableProps = new TdbTableProperties();
            TableProps.Name = new string((char)0, 5);

            // Get Tableprops based on the selected index
            if (!TDB.TDBTableGetProperties(DBIndex, TableNames.GetTables().Find(item => item.name == "SETL").rec, ref TableProps))
                return null;

            List<TdbFieldProperties> tableFields = new List<TdbFieldProperties>();

            for (int i = 0; i < TableProps.FieldCount; i++)
            {
                TdbFieldProperties FieldProps = new TdbFieldProperties();
                FieldProps.Name = new string((char)0, 5);
                TDB.TDBFieldGetProperties(DBIndex, TDB.StrReverse("SETL"), i, ref FieldProps);
                tableFields.Add(FieldProps);
            }

            for (int i = 0; i < TableProps.RecordCount; i++)
            {
                string _name = new string((char)0, (tableFields.FirstOrDefault(field => field.Name == TDB.StrReverse("name")).Size / 8) + 1);

                TDB.TDBFieldGetValueAsString(DBIndex, TDB.StrReverse("SETL"), TDB.StrReverse("name"), i, ref _name);
                _name = _name.Replace(",", "");

                if (filter != 0 && (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("SETL"), TDB.StrReverse("FORM"), i) == filter)
                {
                    _SubFormations.Add(new SETL
                    {
                        rec = i,
                        setl = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("SETL"), TDB.StrReverse("SETL"), i),
                        FORM = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("SETL"), TDB.StrReverse("FORM"), i),
                        MOTN = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("SETL"), TDB.StrReverse("MOTN"), i),
                        CLAS = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("SETL"), TDB.StrReverse("CLAS"), i),
                        SETT = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("SETL"), TDB.StrReverse("SETT"), i),
                        SITT = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("SETL"), TDB.StrReverse("SITT"), i),
                        SLF_ = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("SETL"), TDB.StrReverse("SLF_"), i),
                        name = _name,
                        poso = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("SETL"), TDB.StrReverse("poso"), i)
                    });
                }
                else if (filter == 0)
                {
                    _SubFormations.Add(new SETL
                    {
                        rec = i,
                        setl = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("SETL"), TDB.StrReverse("SETL"), i),
                        FORM = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("SETL"), TDB.StrReverse("FORM"), i),
                        MOTN = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("SETL"), TDB.StrReverse("MOTN"), i),
                        CLAS = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("SETL"), TDB.StrReverse("CLAS"), i),
                        SETT = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("SETL"), TDB.StrReverse("SETT"), i),
                        SITT = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("SETL"), TDB.StrReverse("SITT"), i),
                        SLF_ = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("SETL"), TDB.StrReverse("SLF_"), i),
                        name = _name,
                        poso = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("SETL"), TDB.StrReverse("poso"), i)
                    });
                }
            }
            return _SubFormations;
        }

        public static void SetSETL(List<SETL> SETL, int DBIndex = 0)
        {
            TdbTableProperties TableProps = new TdbTableProperties();
            TableProps.Name = new string((char)0, 5);

            // Get Tableprops based on the selected index
            TDB.TDBTableGetProperties(DBIndex, TableNames.GetTables().Find(item => item.name == "SETL").rec, ref TableProps);

            if (SETL.Count > TableProps.RecordCount)
                for (int i = TableProps.RecordCount - 1; i < SETL.Count - 1; i++)
                {
                    TDB.TDBTableRecordAdd(DBIndex, TDB.StrReverse("SETL"), true);
                }
            else if (SETL.Count < TableProps.RecordCount)
            {
                for (int i = TableProps.RecordCount - 1; i > SETL.Count - 1; i--)
                {
                    TDB.TDBTableRecordRemove(DBIndex, TDB.StrReverse("SETL"), i);
                }
            }

            foreach (SETL item in SETL)
            {
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("SETL"), TDB.StrReverse("SETL"), item.rec, item.setl);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("SETL"), TDB.StrReverse("FORM"), item.rec, item.FORM);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("SETL"), TDB.StrReverse("MOTN"), item.rec, item.MOTN);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("SETL"), TDB.StrReverse("CLAS"), item.rec, item.CLAS);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("SETL"), TDB.StrReverse("SETT"), item.rec, item.SETT);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("SETL"), TDB.StrReverse("SITT"), item.rec, item.SITT);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("SETL"), TDB.StrReverse("SLF_"), item.rec, item.SLF_);
                TDB.TDBFieldSetValueAsString(DBIndex, TDB.StrReverse("SETL"), TDB.StrReverse("name"), item.rec, item.name);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("SETL"), TDB.StrReverse("poso"), item.rec, item.poso);
            }
        }

        public static List<SETL> Sort(List<SETL> SETL)
        {
            return SETL.OrderBy(s => s.setl).Cast<SETL>().ToList();
        }
    }
}
