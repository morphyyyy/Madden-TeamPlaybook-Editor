using System;
using System.Collections.Generic;
using System.Linq;
using TDBAccess;

namespace Madden.CustomPlaybook
{
    [Serializable]
    public class BSMP
    {
        public int rec { get; set; }
        public int SETL { get; set; }
        public int PBST { get; set; }

        public override string ToString()
        {
            return
                "Rec#: " + rec + "\t" +
                "   SETL: " + SETL + "\t" +
                "   PBST: " + PBST;
        }

        public static List<BSMP> GetBSMP(int filter = 0, int DBIndex = 0)
        {
            List<BSMP> _BSMP = new List<BSMP>();

            TdbTableProperties TableProps = new TdbTableProperties();
            TableProps.Name = new string((char)0, 5);

            // Get Tableprops based on the selected index
            if (!TDB.TDBTableGetProperties(DBIndex, TableNames.GetTables().Find(item => item.name == "BSMP").rec, ref TableProps))
                return null;

            List<TdbFieldProperties> tableFields = new List<TdbFieldProperties>();

            for (int i = 0; i < TableProps.FieldCount; i++)
            {
                TdbFieldProperties FieldProps = new TdbFieldProperties();
                FieldProps.Name = new string((char)0, 5);
                TDB.TDBFieldGetProperties(DBIndex, TDB.StrReverse("BSMP"), i, ref FieldProps);
                tableFields.Add(FieldProps);
            }

            for (int i = 0; i < TableProps.RecordCount; i++)
            {
                string _name = new string((char)0, (tableFields.FirstOrDefault(field => field.Name == TDB.StrReverse("name")).Size / 8) + 1);

                TDB.TDBFieldGetValueAsString(DBIndex, TDB.StrReverse("BSMP"), TDB.StrReverse("name"), i, ref _name);
                _name = _name.Replace(",", "");

                if (filter != 0 && (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("BSMP"), TDB.StrReverse("FTYP"), i) == filter)
                {
                    _BSMP.Add(new BSMP
                    {
                        rec = i,
                        SETL = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("BSMP"), TDB.StrReverse("SETL"), i),
                        PBST = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("BSMP"), TDB.StrReverse("PBST"), i)
                    });
                }
                else if (filter == 0)
                {
                    _BSMP.Add(new BSMP
                    {
                        rec = i,
                        SETL = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("BSMP"), TDB.StrReverse("SETL"), i),
                        PBST = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("BSMP"), TDB.StrReverse("PBST"), i)
                    });
                }
            }

            return _BSMP;
        }

        public static void SetBSMP(List<BSMP> BSMP, int DBIndex = 0)
        {
            TdbTableProperties TableProps = new TdbTableProperties();
            TableProps.Name = new string((char)0, 5);

            // Get Tableprops based on the selected index
            TDB.TDBTableGetProperties(DBIndex, TableNames.GetTables().Find(item => item.name == "BSMP").rec, ref TableProps);

            if (BSMP.Count > TableProps.RecordCount)
                for (int i = TableProps.RecordCount - 1; i < BSMP.Count - 1; i++)
                {
                    TDB.TDBTableRecordAdd(DBIndex, TDB.StrReverse("BSMP"), true);
                }
            else if (BSMP.Count < TableProps.RecordCount)
            {
                for (int i = TableProps.RecordCount - 1; i > BSMP.Count - 1; i--)
                {
                    TDB.TDBTableRecordRemove(DBIndex, TDB.StrReverse("BSMP"), i);
                }
            }

            foreach (BSMP item in BSMP)
            {
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("BSMP"), TDB.StrReverse("SETL"), item.rec, item.SETL);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("BSMP"), TDB.StrReverse("PBST"), item.rec, item.PBST);
            }
        }

        public static List<BSMP> Sort(List<BSMP> BSMP)
        {
            return BSMP.OrderBy(s => s.SETL).Cast<BSMP>().ToList();
        }
    }
}
