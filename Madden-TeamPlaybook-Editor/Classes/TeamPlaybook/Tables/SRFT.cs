using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TDBAccess;

namespace Madden.TeamPlaybook
{
    [Serializable]
    public class SRFT
    {
        public int rec { get; set; }
        public int SIDE { get; set; }
        public int YOFF { get; set; }
        public int TECH { get; set; }
        /// <summary>
        /// PLYL.plyl
        /// </summary>
        public int PLYL { get; set; }
        public int STAN { get; set; }
        /// <summary>
        /// PLYS.poso
        /// </summary>
        public int PLYR { get; set; }
        public int PRIS { get; set; }
        public int GAPS { get; set; }
        public int ASSS { get; set; }
        public int PRIW { get; set; }
        public int GAPW { get; set; }
        public int ASSW { get; set; }

        public override string ToString()
        {
            return
                "rec: " + rec +
                "   SIDE: " + SIDE +
                "   YOFF: " + YOFF +
                "   TECH: " + TECH +
                "   PLYL: " + PLYL +
                "   STAN: " + STAN +
                "   PLYR: " + PLYR +
                "   PRIS: " + PRIS +
                "   GAPS: " + GAPS +
                "   ASSS: " + ASSS +
                "   PRIW: " + PRIW +
                "   GAPW: " + GAPW +
                "   ASSW: " + ASSW;
        }

        public static List<SRFT> GetSRFT(int filter = 0, int DBIndex = 0)
        {
            List<SRFT> _SRFT = new List<SRFT>();

            TdbTableProperties TableProps = new TdbTableProperties();
            TableProps.Name = new string((char)0, 5);

            if (TableNames.GetTables().Find(item => item.name == "SRFT") != null)
            {
                // Get Tableprops based on the selected index
                if (!TDB.TDBTableGetProperties(DBIndex, TableNames.GetTables().Find(item => item.name == "SRFT").rec, ref TableProps))
                    return _SRFT;
            }
            else
            {
                return _SRFT;
            }

            for (int i = 0; i < TableProps.RecordCount; i++)
            {
                if (filter != 0 && (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("SRFT"), TDB.StrReverse("PLYL"), i) == filter)
                {
                    _SRFT.Add(new SRFT
                    {
                        rec = i,
                        SIDE = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("SRFT"), TDB.StrReverse("SIDE"), i),
                        YOFF = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("SRFT"), TDB.StrReverse("YOFF"), i),
                        TECH = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("SRFT"), TDB.StrReverse("TECH"), i),
                        PLYL = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("SRFT"), TDB.StrReverse("PLYL"), i),
                        STAN = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("SRFT"), TDB.StrReverse("STAN"), i),
                        PLYR = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("SRFT"), TDB.StrReverse("PLYR"), i),
                        PRIS = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("SRFT"), TDB.StrReverse("PRIS"), i),
                        GAPS = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("SRFT"), TDB.StrReverse("GAPS"), i),
                        ASSS = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("SRFT"), TDB.StrReverse("ASSS"), i),
                        PRIW = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("SRFT"), TDB.StrReverse("PRIW"), i),
                        GAPW = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("SRFT"), TDB.StrReverse("GAPW"), i),
                        ASSW = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("SRFT"), TDB.StrReverse("ASSW"), i)
                    });
                }
                else if (filter == 0)
                {
                    _SRFT.Add(new SRFT
                    {
                        rec = i,
                        SIDE = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("SRFT"), TDB.StrReverse("SIDE"), i),
                        YOFF = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("SRFT"), TDB.StrReverse("YOFF"), i),
                        TECH = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("SRFT"), TDB.StrReverse("TECH"), i),
                        PLYL = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("SRFT"), TDB.StrReverse("PLYL"), i),
                        STAN = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("SRFT"), TDB.StrReverse("STAN"), i),
                        PLYR = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("SRFT"), TDB.StrReverse("PLYR"), i),
                        PRIS = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("SRFT"), TDB.StrReverse("PRIS"), i),
                        GAPS = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("SRFT"), TDB.StrReverse("GAPS"), i),
                        ASSS = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("SRFT"), TDB.StrReverse("ASSS"), i),
                        PRIW = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("SRFT"), TDB.StrReverse("PRIW"), i),
                        GAPW = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("SRFT"), TDB.StrReverse("GAPW"), i),
                        ASSW = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("SRFT"), TDB.StrReverse("ASSW"), i)
                    });
                }
            }
            return _SRFT;
        }

        public static void SetSRFT(List<SRFT> SRFT, int DBIndex = 0)
        {
            TdbTableProperties TableProps = new TdbTableProperties();
            TableProps.Name = new string((char)0, 5);

            // Get Tableprops based on the selected index
            if (TableNames.GetTables().Find(item => item.name == "SRFT") != null)
            {
                TDB.TDBTableGetProperties(DBIndex, TableNames.GetTables().Find(item => item.name == "SRFT").rec, ref TableProps);
            }
            else
            {
                return;
            }

            if (SRFT.Count > TableProps.RecordCount)
                for (int i = TableProps.RecordCount - 1; i < SRFT.Count - 1; i++)
                {
                    TDB.TDBTableRecordAdd(DBIndex, TDB.StrReverse("SRFT"), true);
                }
            else if (SRFT.Count < TableProps.RecordCount)
            {
                for (int i = TableProps.RecordCount - 1; i > SRFT.Count - 1; i--)
                {
                    TDB.TDBTableRecordRemove(DBIndex, TDB.StrReverse("SRFT"), i);
                }
            }

            foreach (SRFT item in SRFT)
            {
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("SRFT"), TDB.StrReverse("SIDE"), item.rec, item.SIDE);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("SRFT"), TDB.StrReverse("YOFF"), item.rec, item.YOFF);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("SRFT"), TDB.StrReverse("TECH"), item.rec, item.TECH);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("SRFT"), TDB.StrReverse("PLYL"), item.rec, item.PLYL);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("SRFT"), TDB.StrReverse("STAN"), item.rec, item.STAN);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("SRFT"), TDB.StrReverse("PLYR"), item.rec, item.PLYR);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("SRFT"), TDB.StrReverse("PRIS"), item.rec, item.PRIS);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("SRFT"), TDB.StrReverse("GAPS"), item.rec, item.GAPS);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("SRFT"), TDB.StrReverse("ASSS"), item.rec, item.ASSS);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("SRFT"), TDB.StrReverse("PRIW"), item.rec, item.PRIW);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("SRFT"), TDB.StrReverse("GAPW"), item.rec, item.GAPW);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("SRFT"), TDB.StrReverse("ASSW"), item.rec, item.ASSW);
            }
        }

        public static List<SRFT> Sort(List<SRFT> SRFT)
        {
            return SRFT.OrderBy(s => s.PLYL).ThenBy(s => s.PLYR).Cast<SRFT>().ToList();
        }
    }
}
