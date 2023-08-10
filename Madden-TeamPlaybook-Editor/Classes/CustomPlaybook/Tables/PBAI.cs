using System;
using System.Collections.Generic;
using System.Linq;
using TDBAccess;

namespace Madden.CustomPlaybook
{
    [Serializable]
    public class PBAI
    {
        public int rec { get; set; }
        public int BOKL { get; set; }
        public int PLYL { get; set; }
        public int AIGR { get; set; }
        public int prct { get; set; }

        public override string ToString()
        {
            return
                "Rec#: " + rec +
                "   BOKL: " + BOKL +
                "   PLYL: " + PLYL +
                "   AIGR: " + AIGR +
                "   prct: " + prct;
        }

        public static List<PBAI> GetPBAI(int filter = 0, int DBIndex = 0)
        {
            List<PBAI> _PBAI = new List<PBAI>();

            TdbTableProperties TableProps = new TdbTableProperties();
            TableProps.Name = new string((char)0, 5);

            // Get Tableprops based on the selected index
            if (!TDB.TDBTableGetProperties(DBIndex, TableNames.GetTables().Find(item => item.name == "PBAI").rec, ref TableProps))
                return null;

            for (int i = 0; i < TableProps.RecordCount; i++)
            {
                if (filter != 0 && (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PBAI"), TDB.StrReverse("PBPL"), i) == filter)
                {
                    _PBAI.Add(new PBAI
                    {
                        rec = i,
                        BOKL = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PBAI"), TDB.StrReverse("PBPL"), i),
                        PLYL = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PBAI"), TDB.StrReverse("SETL"), i),
                        AIGR = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PBAI"), TDB.StrReverse("AIGR"), i),
                        prct = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PBAI"), TDB.StrReverse("prct"), i)
                    });
                }
                else if (filter == 0)
                {
                    _PBAI.Add(new PBAI
                    {
                        rec = i,
                        BOKL = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PBAI"), TDB.StrReverse("PBPL"), i),
                        PLYL = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PBAI"), TDB.StrReverse("SETL"), i),
                        AIGR = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PBAI"), TDB.StrReverse("AIGR"), i),
                        prct = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PBAI"), TDB.StrReverse("prct"), i)
                    });
                }
            }
            return _PBAI;
        }

        public static void SetPBAI(List<PBAI> PBAI, int DBIndex = 0)
        {
            TdbTableProperties TableProps = new TdbTableProperties();
            TableProps.Name = new string((char)0, 5);

            // Get Tableprops based on the selected index
            TDB.TDBTableGetProperties(DBIndex, TableNames.GetTables().Find(item => item.name == "PBAI").rec, ref TableProps);

            if (PBAI.Count > TableProps.RecordCount)
                for (int i = TableProps.RecordCount - 1; i < PBAI.Count - 1; i++)
                {
                    TDB.TDBTableRecordAdd(DBIndex, TDB.StrReverse("PBAI"), true);
                }
            else if (PBAI.Count < TableProps.RecordCount)
            {
                for (int i = TableProps.RecordCount - 1; i > PBAI.Count - 1; i--)
                {
                    TDB.TDBTableRecordRemove(DBIndex, TDB.StrReverse("PBAI"), i);
                }
            }

            foreach (PBAI item in PBAI)
            {
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PBAI"), TDB.StrReverse("PBPL"), item.rec, item.BOKL);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PBAI"), TDB.StrReverse("SETL"), item.rec, item.PLYL);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PBAI"), TDB.StrReverse("AIGR"), item.rec, item.AIGR);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PBAI"), TDB.StrReverse("prct"), item.rec, item.prct);
            }
        }

        public static List<PBAI> Sort(List<PBAI> PBAI)
        {
            return PBAI.OrderBy(s => s.BOKL).ThenBy(s => s.AIGR).Cast<PBAI>().ToList();
        }
    }
}
