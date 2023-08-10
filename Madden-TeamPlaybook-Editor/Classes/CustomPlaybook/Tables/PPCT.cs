using System;
using System.Collections.Generic;
using System.Linq;
using TDBAccess;

namespace Madden.CustomPlaybook
{
    [Serializable]
    public class PPCT
    {
        public int rec { get; set; }
        public int plyl { get; set; }
        public int conp { get; set; }
        public int recr { get; set; }

        public override string ToString()
        {
            return
                "Rec#: " + rec +
                "   plyl: " + plyl +
                "   conp: " + conp +
                "   recr: " + recr;
        }

        public static List<PPCT> GetPPCT(int filter = 0, int DBIndex = 0)
        {
            List<PPCT> _PPCT = new List<PPCT>();

            TdbTableProperties TableProps = new TdbTableProperties();
            TableProps.Name = new string((char)0, 5);

            // Get Tableprops based on the selected index
            if (!TDB.TDBTableGetProperties(DBIndex, TableNames.GetTables().Find(item => item.name == "PPCT").rec, ref TableProps))
                return null;

            for (int i = 0; i < TableProps.RecordCount; i++)
            {
                if (filter != 0 && (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PPCT"), TDB.StrReverse("plyl"), i) == filter)
                {
                    _PPCT.Add(new PPCT
                    {
                        rec = i,
                        plyl = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PPCT"), TDB.StrReverse("plyl"), i),
                        conp = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PPCT"), TDB.StrReverse("conp"), i),
                        recr = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PPCT"), TDB.StrReverse("recr"), i)
                    });
                }
                else if (filter == 0)
                {
                    _PPCT.Add(new PPCT
                    {
                        rec = i,
                        plyl = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PPCT"), TDB.StrReverse("plyl"), i),
                        conp = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PPCT"), TDB.StrReverse("conp"), i),
                        recr = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PPCT"), TDB.StrReverse("recr"), i)
                    });
                }
            }
            return _PPCT;
        }

        public static void SetPPCT(List<PPCT> PPCT, int DBIndex = 0)
        {
            TdbTableProperties TableProps = new TdbTableProperties();
            TableProps.Name = new string((char)0, 5);

            // Get Tableprops based on the selected index
            TDB.TDBTableGetProperties(DBIndex, TableNames.GetTables().Find(item => item.name == "PPCT").rec, ref TableProps);

            if (PPCT.Count > TableProps.RecordCount)
                for (int i = TableProps.RecordCount - 1; i < PPCT.Count - 1; i++)
                {
                    TDB.TDBTableRecordAdd(DBIndex, TDB.StrReverse("PPCT"), true);
                }
            else if (PPCT.Count < TableProps.RecordCount)
            {
                for (int i = TableProps.RecordCount - 1; i > PPCT.Count - 1; i--)
                {
                    TDB.TDBTableRecordRemove(DBIndex, TDB.StrReverse("PPCT"), i);
                }
            }

            foreach (PPCT item in PPCT)
            {
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PPCT"), TDB.StrReverse("plyl"), item.rec, item.plyl);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PPCT"), TDB.StrReverse("conp"), item.rec, item.conp);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PPCT"), TDB.StrReverse("recr"), item.rec, item.recr);
            }
        }

        public static List<PPCT> Sort(List<PPCT> PPCT)
        {
            return PPCT.OrderBy(s => s.plyl).Cast<PPCT>().ToList();
        }
    }
}
