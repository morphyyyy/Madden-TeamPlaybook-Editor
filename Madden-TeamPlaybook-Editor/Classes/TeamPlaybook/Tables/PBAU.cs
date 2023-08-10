using System;
using System.Collections.Generic;
using System.Linq;
using TDBAccess;

namespace Madden.TeamPlaybook
{
    [Serializable]
    public class PBAU
    {
        public int rec { get; set; }
        public int PBPL { get; set; }
        public int FTYP { get; set; }
        public int pbau { get; set; }

        public override string ToString()
        {
            return
                "Rec#: " + rec +
                "   PBPL: " + PBPL +
                "   FTYP: " + FTYP +
                "   PBAU: " + pbau;
        }

        public static List<PBAU> GetPBAU(int filter = 0, int DBIndex = 0)
        {
            List<PBAU> _PBAU = new List<PBAU>();

            TdbTableProperties TableProps = new TdbTableProperties();
            TableProps.Name = new string((char)0, 5);

            // Get Tableprops based on the selected index
            if (!TDB.TDBTableGetProperties(DBIndex, TableNames.GetTables().Find(item => item.name == "PBAU").rec, ref TableProps))
                return null;

            for (int i = 0; i < TableProps.RecordCount; i++)
            {
                if (filter != 0 && (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PBAU"), TDB.StrReverse("PBPL"), i) == filter)
                {
                    _PBAU.Add(new PBAU
                    {
                        rec = i,
                        PBPL = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PBAU"), TDB.StrReverse("PBPL"), i),
                        FTYP = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PBAU"), TDB.StrReverse("FTYP"), i),
                        pbau = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PBAU"), TDB.StrReverse("PBAU"), i)
                    });
                }
                else if (filter == 0)
                {
                    _PBAU.Add(new PBAU
                    {
                        rec = i,
                        PBPL = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PBAU"), TDB.StrReverse("PBPL"), i),
                        FTYP = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PBAU"), TDB.StrReverse("FTYP"), i),
                        pbau = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PBAU"), TDB.StrReverse("PBAU"), i)
                    });
                }
            }
            return _PBAU;
        }

        public static void SetPBAU(List<PBAU> PBAU, int DBIndex = 0)
        {
            TdbTableProperties TableProps = new TdbTableProperties();
            TableProps.Name = new string((char)0, 5);

            // Get Tableprops based on the selected index
            TDB.TDBTableGetProperties(DBIndex, TableNames.GetTables().Find(item => item.name == "PBAU").rec, ref TableProps);

            if (PBAU.Count > TableProps.RecordCount)
                for (int i = TableProps.RecordCount - 1; i < PBAU.Count - 1; i++)
                {
                    TDB.TDBTableRecordAdd(DBIndex, TDB.StrReverse("PBAU"), true);
                }
            else if (PBAU.Count < TableProps.RecordCount)
            {
                for (int i = TableProps.RecordCount - 1; i > PBAU.Count - 1; i--)
                {
                    TDB.TDBTableRecordRemove(DBIndex, TDB.StrReverse("PBAU"), i);
                }
            }

            foreach (PBAU item in PBAU)
            {
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PBAU"), TDB.StrReverse("PBPL"), item.rec, item.PBPL);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PBAU"), TDB.StrReverse("FTYP"), item.rec, item.FTYP);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PBAU"), TDB.StrReverse("PBAU"), item.rec, item.pbau);
            }
        }

        public static List<PBAU> Sort(List<PBAU> PBAU)
        {
            return PBAU.OrderBy(s => s.FTYP).ThenBy(form => form.PBPL).Cast<PBAU>().ToList();
        }
    }
}
