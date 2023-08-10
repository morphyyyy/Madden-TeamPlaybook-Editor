using System;
using System.Collections.Generic;
using System.Linq;
using TDBAccess;

namespace Madden.CustomPlaybook
{
    [Serializable]
    public class PGPL
    {
        public int rec { get; set; }
        public int BOKL { get; set; }
        public int SETL { get; set; }
        public int PLYL { get; set; }
        public int PBST { get; set; }
        public int PLYT { get; set; }
        public int ord_ { get; set; }
        public int Flag { get; set; }

        public override string ToString()
        {
            return
                "Rec#: " + rec + "\t" +
                "   BOKL: " + BOKL + "\t" +
                "   SETL: " + SETL + "\t" +
                "   PLYL: " + PLYL + "\t" +
                "   PBST: " + PBST + "\t" +
                "   PLYT: " + PLYT + "\t" +
                "   ord_: " + ord_ + "\t" +
                "   poso: " + Flag;
        }

        public static List<PGPL> GetPGPL(int filter = 0, int DBIndex = 0)
        {
            List<PGPL> _PGPL = new List<PGPL>();

            TdbTableProperties TableProps = new TdbTableProperties();
            TableProps.Name = new string((char)0, 5);

            // Get Tableprops based on the selected index
            if (!TDB.TDBTableGetProperties(DBIndex, TableNames.GetTables().Find(item => item.name == "PGPL").rec, ref TableProps))
                return null;

            List<TdbFieldProperties> tableFields = new List<TdbFieldProperties>();

            for (int i = 0; i < TableProps.FieldCount; i++)
            {
                TdbFieldProperties FieldProps = new TdbFieldProperties();
                FieldProps.Name = new string((char)0, 5);
                TDB.TDBFieldGetProperties(DBIndex, TDB.StrReverse("PGPL"), i, ref FieldProps);
                tableFields.Add(FieldProps);
            }

            for (int i = 0; i < TableProps.RecordCount; i++)
            {
                if (filter != 0 && (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PGPL"), TDB.StrReverse("FTYP"), i) == filter)
                {
                    _PGPL.Add(new PGPL
                    {
                        rec = i,
                        SETL = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PGPL"), TDB.StrReverse("SETL"), i),
                        BOKL = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PGPL"), TDB.StrReverse("BOKL"), i),
                        PLYL = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PGPL"), TDB.StrReverse("PLYL"), i),
                        PBST = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PGPL"), TDB.StrReverse("PBST"), i),
                        PLYT = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PGPL"), TDB.StrReverse("PLYT"), i),
                        ord_ = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PGPL"), TDB.StrReverse("ord_"), i),
                        Flag = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PGPL"), TDB.StrReverse("Flag"), i)
                    });
                }
                else if (filter == 0)
                {
                    _PGPL.Add(new PGPL
                    {
                        rec = i,
                        SETL = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PGPL"), TDB.StrReverse("SETL"), i),
                        BOKL = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PGPL"), TDB.StrReverse("BOKL"), i),
                        PLYL = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PGPL"), TDB.StrReverse("PLYL"), i),
                        PBST = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PGPL"), TDB.StrReverse("PBST"), i),
                        PLYT = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PGPL"), TDB.StrReverse("PLYT"), i),
                        ord_ = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PGPL"), TDB.StrReverse("ord_"), i),
                        Flag = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PGPL"), TDB.StrReverse("Flag"), i)
                    });
                }
            }

            return _PGPL;
        }

        public static void SetPGPL(List<PGPL> PGPL, int DBIndex = 0)
        {
            TdbTableProperties TableProps = new TdbTableProperties();
            TableProps.Name = new string((char)0, 5);

            // Get Tableprops based on the selected index
            TDB.TDBTableGetProperties(DBIndex, TableNames.GetTables().Find(item => item.name == "PGPL").rec, ref TableProps);

            if (PGPL.Count > TableProps.RecordCount)
                for (int i = TableProps.RecordCount - 1; i < PGPL.Count - 1; i++)
                {
                    TDB.TDBTableRecordAdd(DBIndex, TDB.StrReverse("PGPL"), true);
                }
            else if (PGPL.Count < TableProps.RecordCount)
            {
                for (int i = TableProps.RecordCount - 1; i > PGPL.Count - 1; i--)
                {
                    TDB.TDBTableRecordRemove(DBIndex, TDB.StrReverse("PGPL"), i);
                }
            }

            foreach (PGPL item in PGPL)
            {
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PGPL"), TDB.StrReverse("BOKL"), item.rec, item.BOKL);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PGPL"), TDB.StrReverse("SETL"), item.rec, item.SETL);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PGPL"), TDB.StrReverse("PLYL"), item.rec, item.PLYL);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PGPL"), TDB.StrReverse("PBST"), item.rec, item.PBST);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PGPL"), TDB.StrReverse("PLYT"), item.rec, item.PLYT);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PGPL"), TDB.StrReverse("ord_"), item.rec, item.ord_);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PGPL"), TDB.StrReverse("Flag"), item.rec, item.Flag);
            }
        }

        public static List<PGPL> Sort(List<PGPL> PGPL)
        {
            return PGPL.OrderBy(s => s.PLYL).ThenBy(s => s.BOKL).Cast<PGPL>().ToList();
        }
    }
}
