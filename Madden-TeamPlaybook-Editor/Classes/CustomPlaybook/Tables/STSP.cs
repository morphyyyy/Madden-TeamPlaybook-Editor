using System;
using System.Collections.Generic;
using System.Linq;
using TDBAccess;

namespace Madden.CustomPlaybook
{
    [Serializable]
    public class STSP
    {
        public int rec { get; set; }
        public int BOKL { get; set; }
        public int SETL { get; set; }

        public override string ToString()
        {
            return
                "Rec#: " + rec +
                "   BOKL: " + BOKL +
                "   SETL: " + SETL;
        }

        public static List<STSP> GetSTSP(int filter = 0, int DBIndex = 0)
        {
            List<STSP> _STSP = new List<STSP>();

            TdbTableProperties TableProps = new TdbTableProperties();
            TableProps.Name = new string((char)0, 5);

            // Get Tableprops based on the selected index
            if (!TDB.TDBTableGetProperties(DBIndex, TableNames.GetTables().Find(item => item.name == "STSP").rec, ref TableProps))
                return null;

            for (int i = 0; i < TableProps.RecordCount; i++)
            {
                if (filter != 0 && (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("STSP"), TDB.StrReverse("BOKL"), i) == filter)
                {
                    _STSP.Add(new STSP
                    {
                        rec = i,
                        BOKL = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("STSP"), TDB.StrReverse("BOKL"), i),
                        SETL = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("STSP"), TDB.StrReverse("SETL"), i)
                    });
                }
                else if (filter == 0)
                {
                    _STSP.Add(new STSP
                    {
                        rec = i,
                        BOKL = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("STSP"), TDB.StrReverse("BOKL"), i),
                        SETL = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("STSP"), TDB.StrReverse("SETL"), i)
                    });
                }
            }
            return _STSP;
        }

        public static void SetSTSP(List<STSP> STSP, int DBIndex = 0)
        {
            TdbTableProperties TableProps = new TdbTableProperties();
            TableProps.Name = new string((char)0, 5);

            // Get Tableprops based on the selected index
            TDB.TDBTableGetProperties(DBIndex, TableNames.GetTables().Find(item => item.name == "STSP").rec, ref TableProps);

            if (STSP.Count > TableProps.RecordCount)
            {
                for (int i = TableProps.RecordCount - 1; i < STSP.Count - 1; i++)
                {
                    TDB.TDBTableRecordAdd(DBIndex, TDB.StrReverse("STSP"), true);
                }
            }
            else if (STSP.Count < TableProps.RecordCount)
            {
                for (int i = TableProps.RecordCount - 1; i > STSP.Count - 1; i--)
                {
                    TDB.TDBTableRecordRemove(DBIndex, TDB.StrReverse("STSP"), i);
                }
            }

            foreach (STSP item in STSP)
            {
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("STSP"), TDB.StrReverse("BOKL"), item.rec, item.BOKL);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("STSP"), TDB.StrReverse("SETL"), item.rec, item.SETL);
            }
        }

        public static List<STSP> Sort(List<STSP> STSP)
        {
            return STSP.OrderBy(s => s.SETL).ThenBy(s => s.BOKL).Cast<STSP>().ToList();
        }
    }
}
