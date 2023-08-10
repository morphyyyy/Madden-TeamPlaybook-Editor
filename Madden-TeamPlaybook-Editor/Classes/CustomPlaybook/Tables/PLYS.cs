using System;
using System.Collections.Generic;
using System.Linq;
using TDBAccess;

namespace Madden.CustomPlaybook
{
    [Serializable]
    public class PLYS
    {
        public int rec { get; set; }
        public int PSAL { get; set; }
        public int ARTL { get; set; }  
        public int PLYL { get; set; }
        public int PLRR { get; set; }
        public int poso { get; set; }

        public override string ToString()
        {
            return
                "Rec#: " + rec + "\t" +
                "   PSAL: " + PSAL + "\t" +
                "   ARTL: " + ARTL + "\t" +
                "   PLYL: " + PLYL + "\t" +
                "   PLRR: " + PLRR + "\t" +
                "   poso: " + poso;
        }

        public static List<PLYS> GetPLYS(int filter = 0, int DBIndex = 0)
        {
            List<PLYS> _PLYS = new List<PLYS>();

            TdbTableProperties TableProps = new TdbTableProperties();
            TableProps.Name = new string((char)0, 5);

            // Get Tableprops based on the selected index
            if (!TDB.TDBTableGetProperties(DBIndex, TableNames.GetTables().Find(item => item.name == "PLYS").rec, ref TableProps))
                return null;

            for (int i = 0; i < TableProps.RecordCount; i++)
            {
                if (filter != 0 && (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLYS"), TDB.StrReverse("PLYL"), i) == filter)
                {
                    _PLYS.Add(new PLYS
                    {
                        rec = i,
                        PSAL = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLYS"), TDB.StrReverse("PSAL"), i),
                        ARTL = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLYS"), TDB.StrReverse("ARTL"), i),
                        PLYL = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLYS"), TDB.StrReverse("PLYL"), i),
                        PLRR = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLYS"), TDB.StrReverse("PLRR"), i),
                        poso = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLYS"), TDB.StrReverse("poso"), i)
                    });
                }
                else if (filter == 0)
                {
                    _PLYS.Add(new PLYS
                    {
                        rec = i,
                        PSAL = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLYS"), TDB.StrReverse("PSAL"), i),
                        ARTL = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLYS"), TDB.StrReverse("ARTL"), i),
                        PLYL = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLYS"), TDB.StrReverse("PLYL"), i),
                        PLRR = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLYS"), TDB.StrReverse("PLRR"), i),
                        poso = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLYS"), TDB.StrReverse("poso"), i)
                    });
                }
            }

            //_PLYS = _PLYS.ToList().OrderBy(s => s.PLYL).ThenBy(s => s.poso).Cast<PLYS>().ToList();
            //for (int i = 0; i > _PLYS.Count; i++)
            //{
            //    _PLYS[i].rec = i;
            //}

            return _PLYS;
        }

        public static void SetPLYS(List<PLYS> PLYS, int DBIndex = 0)
        {
            TdbTableProperties TableProps = new TdbTableProperties();
            TableProps.Name = new string((char)0, 5);

            // Get Tableprops based on the selected index
            TDB.TDBTableGetProperties(DBIndex, TableNames.GetTables().Find(item => item.name == "PLYS").rec, ref TableProps);

            if (PLYS.Count > TableProps.RecordCount)
                for (int i = TableProps.RecordCount - 1; i < PLYS.Count - 1; i++)
                {
                    TDB.TDBTableRecordAdd(DBIndex, TDB.StrReverse("PLYS"), true);
                }
            else if (PLYS.Count < TableProps.RecordCount)
            {
                for (int i = TableProps.RecordCount - 1; i > PLYS.Count - 1; i--)
                {
                    TDB.TDBTableRecordRemove(DBIndex, TDB.StrReverse("PLYS"), i);
                }
            }

            foreach (PLYS item in PLYS)
            {
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PLYS"), TDB.StrReverse("PSAL"), item.rec, item.PSAL);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PLYS"), TDB.StrReverse("ARTL"), item.rec, item.ARTL);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PLYS"), TDB.StrReverse("PLYL"), item.rec, item.PLYL);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PLYS"), TDB.StrReverse("PLRR"), item.rec, item.PLRR);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PLYS"), TDB.StrReverse("poso"), item.rec, item.poso);
            }
        }

        public static List<PLYS> Sort(List<PLYS> PLYS)
        {
            return PLYS.OrderBy(s => s.PLYL).ThenBy(s => s.poso).Cast<PLYS>().ToList();
        }
    }
}
