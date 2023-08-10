using System;
using System.Collections.Generic;
using System.Linq;
using TDBAccess;

namespace Madden.CustomPlaybook
{
    [Serializable]
    public class PBFI
    {
        public int rec { get; set; }
        public int BOKL { get; set; }
        public string name { get; set; }

        public override string ToString()
        {
            return
                "Rec#: " + rec + "\t" +
                "   BOKL: " + BOKL + "\t" +
                "   Name: " + name;
        }

        public static List<PBFI> GetPBFI(int filter = 0, int DBIndex = 0)
        {
            List<PBFI> _Formations = new List<PBFI>();

            TdbTableProperties TableProps = new TdbTableProperties();
            TableProps.Name = new string((char)0, 5);

            // Get Tableprops based on the selected index
            if (!TDB.TDBTableGetProperties(DBIndex, TableNames.GetTables().Find(item => item.name == "PBFI").rec, ref TableProps))
                return null;

            List<TdbFieldProperties> tableFields = new List<TdbFieldProperties>();

            for (int i = 0; i < TableProps.FieldCount; i++)
            {
                TdbFieldProperties FieldProps = new TdbFieldProperties();
                FieldProps.Name = new string((char)0, 5);
                TDB.TDBFieldGetProperties(DBIndex, TDB.StrReverse("PBFI"), i, ref FieldProps);
                tableFields.Add(FieldProps);
            }

            for (int i = 0; i < TableProps.RecordCount; i++)
            {
                string _name = new string((char)0, (tableFields.Where(field => field.Name == TDB.StrReverse("name")).FirstOrDefault().Size / 8) + 1);

                TDB.TDBFieldGetValueAsString(DBIndex, TDB.StrReverse("PBFI"), TDB.StrReverse("name"), i, ref _name);
                _name = _name.Replace(",", "");

                if (filter != 0 && (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PBFI"), TDB.StrReverse("FTYP"), i) == filter)
                {
                    _Formations.Add(new PBFI
                    {
                        rec = i,
                        BOKL = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PBFI"), TDB.StrReverse("BOKL"), i),
                        name = _name
                    });
                }
                else if (filter == 0)
                {
                    _Formations.Add(new PBFI
                    {
                        rec = i,
                        BOKL = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PBFI"), TDB.StrReverse("BOKL"), i),
                        name = _name
                    });
                }
            }

            //_Formations = _Formations.ToList().OrderBy(s => s.FTYP).ThenBy(s => s.form).Cast<FORM>().ToList();

            return _Formations;
        }

        public static void SetPBFI(List<PBFI> PBFI, int DBIndex = 0)
        {
            TdbTableProperties TableProps = new TdbTableProperties();
            TableProps.Name = new string((char)0, 5);

            // Get Tableprops based on the selected index
            TDB.TDBTableGetProperties(DBIndex, TableNames.GetTables().Find(item => item.name == "PBFI").rec, ref TableProps);

            if (PBFI.Count > TableProps.RecordCount)
                for (int i = TableProps.RecordCount - 1; i < PBFI.Count - 1; i++)
                {
                    TDB.TDBTableRecordAdd(DBIndex, TDB.StrReverse("PBFI"), true);
                }
            else if (PBFI.Count < TableProps.RecordCount)
            {
                for (int i = TableProps.RecordCount - 1; i > PBFI.Count - 1; i--)
                {
                    TDB.TDBTableRecordRemove(DBIndex, TDB.StrReverse("PBFI"), i);
                }
            }

            foreach (PBFI item in PBFI)
            {
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PBFI"), TDB.StrReverse("BOKL"), item.rec, item.BOKL);
                TDB.TDBFieldSetValueAsString(DBIndex, TDB.StrReverse("PBFI"), TDB.StrReverse("name"), item.rec, item.name);
            }
        }

        public static List<PBFI> Sort(List<PBFI> PBFI)
        {
            return PBFI.OrderBy(s => s.name).Cast<PBFI>().ToList();
        }
    }
}
