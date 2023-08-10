using System;
using System.Collections.Generic;
using System.Linq;
using TDBAccess;

namespace Madden.CustomPlaybook
{
    [Serializable]
    public class PLRD
    {
        public int rec { get; set; }
        public int PLYL { get; set; }
        public int hole { get; set; }

        public override string ToString()
        {
            return
                "Rec#: " + rec +
                "   PLYL: " + PLYL +
                "   hole: " + hole;
        }

        public static List<PLRD> GetPLRD(int filter = 0, int DBIndex = 0)
        {
            List<PLRD> _PLRD = new List<PLRD>();

            TdbTableProperties TableProps = new TdbTableProperties();
            TableProps.Name = new string((char)0, 5);

            // Get Tableprops based on the selected index
            if (!TDB.TDBTableGetProperties(DBIndex, TableNames.GetTables().Find(item => item.name == "PLRD").rec, ref TableProps))
                return null;

            for (int i = 0; i < TableProps.RecordCount; i++)
            {
                if (filter != 0 && (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLRD"), TDB.StrReverse("PLYL"), i) == filter)
                {
                    _PLRD.Add(new PLRD
                    {
                        rec = i,
                        PLYL = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLRD"), TDB.StrReverse("PLYL"), i),
                        hole = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLRD"), TDB.StrReverse("hole"), i)
                    });
                }
                else if (filter == 0)
                {
                    _PLRD.Add(new PLRD
                    {
                        rec = i,
                        PLYL = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLRD"), TDB.StrReverse("PLYL"), i),
                        hole = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLRD"), TDB.StrReverse("hole"), i)
                    });
                }
            }
            return _PLRD;
        }

        public static void SetPLRD(List<PLRD> PLRD, int DBIndex = 0)
        {
            TdbTableProperties TableProps = new TdbTableProperties();
            TableProps.Name = new string((char)0, 5);

            // Get Tableprops based on the selected index
            TDB.TDBTableGetProperties(DBIndex, TableNames.GetTables().Find(item => item.name == "PLRD").rec, ref TableProps);

            if (PLRD.Count > TableProps.RecordCount)
            {
                for (int i = TableProps.RecordCount - 1; i < PLRD.Count - 1; i++)
                {
                    TDB.TDBTableRecordAdd(DBIndex, TDB.StrReverse("PLRD"), true);
                }
            }
            else if (PLRD.Count < TableProps.RecordCount)
            {
                for (int i = TableProps.RecordCount - 1; i > PLRD.Count - 1; i--)
                {
                    TDB.TDBTableRecordRemove(DBIndex, TDB.StrReverse("PLRD"), i);
                }
            }

            foreach (PLRD item in PLRD)
            {
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PLRD"), TDB.StrReverse("PLYL"), item.rec, item.PLYL);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PLRD"), TDB.StrReverse("hole"), item.rec, item.hole);
            }
        }

        public static List<PLRD> Sort(List<PLRD> PLRD)
        {
            return PLRD.OrderBy(s => s.PLYL).Cast<PLRD>().ToList();
        }
    }
}
