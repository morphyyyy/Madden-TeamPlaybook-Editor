using System;
using System.Collections.Generic;
using System.Linq;
using TDBAccess;

namespace Madden.CustomPlaybook
{
    [Serializable]
    public class PBPN
    {
        public int rec { get; set; }
        public int BOKL { get; set; }
        public int PLYL { get; set; }
        public string name { get; set; }

        public override string ToString()
        {
            return
                "Rec#: " + rec + "\t" +
                "   FORM: " + BOKL + "\t" +
                "   FTYP: " + PLYL + "\t" +
                "   Name: " + name;
        }

        public static List<PBPN> GetPBPN(int filter = 0, int DBIndex = 0)
        {
            List<PBPN> _Formations = new List<PBPN>();

            TdbTableProperties TableProps = new TdbTableProperties();
            TableProps.Name = new string((char)0, 5);

            // Get Tableprops based on the selected index
            if (!TDB.TDBTableGetProperties(DBIndex, TableNames.GetTables().Find(item => item.name == "PBPN").rec, ref TableProps))
                return null;

            List<TdbFieldProperties> tableFields = new List<TdbFieldProperties>();

            for (int i = 0; i < TableProps.FieldCount; i++)
            {
                TdbFieldProperties FieldProps = new TdbFieldProperties();
                FieldProps.Name = new string((char)0, 5);
                TDB.TDBFieldGetProperties(DBIndex, TDB.StrReverse("PBPN"), i, ref FieldProps);
                tableFields.Add(FieldProps);
            }

            for (int i = 0; i < TableProps.RecordCount; i++)
            {
                string _name = new string((char)0, (tableFields.FirstOrDefault(field => field.Name == TDB.StrReverse("name")).Size / 8) + 1);

                TDB.TDBFieldGetValueAsString(DBIndex, TDB.StrReverse("PBPN"), TDB.StrReverse("name"), i, ref _name);
                _name = _name.Replace(",", "");

                if (filter != 0 && (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PBPN"), TDB.StrReverse("FTYP"), i) == filter)
                {
                    _Formations.Add(new PBPN
                    {
                        rec = i,
                        BOKL = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PBPN"), TDB.StrReverse("BOKL"), i),
                        PLYL = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PBPN"), TDB.StrReverse("PLYL"), i),
                        name = _name
                    });
                }
                else if (filter == 0)
                {
                    _Formations.Add(new PBPN
                    {
                        rec = i,
                        BOKL = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PBPN"), TDB.StrReverse("BOKL"), i),
                        PLYL = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PBPN"), TDB.StrReverse("PLYL"), i),
                        name = _name
                    });
                }
            }

            //_Formations = _Formations.ToList().OrderBy(s => s.FTYP).ThenBy(s => s.form).Cast<FORM>().ToList();

            return _Formations;
        }

        public static void SetPBPN(List<PBPN> PBPN, int DBIndex = 0)
        {
            TdbTableProperties TableProps = new TdbTableProperties();
            TableProps.Name = new string((char)0, 5);

            // Get Tableprops based on the selected index
            TDB.TDBTableGetProperties(DBIndex, TableNames.GetTables().Find(item => item.name == "PBPN").rec, ref TableProps);

            if (PBPN.Count > TableProps.RecordCount)
                for (int i = TableProps.RecordCount - 1; i < PBPN.Count - 1; i++)
                {
                    TDB.TDBTableRecordAdd(DBIndex, TDB.StrReverse("PBPN"), true);
                }
            else if (PBPN.Count < TableProps.RecordCount)
            {
                for (int i = TableProps.RecordCount - 1; i > PBPN.Count - 1; i--)
                {
                    TDB.TDBTableRecordRemove(DBIndex, TDB.StrReverse("PBPN"), i);
                }
            }

            foreach (PBPN item in PBPN)
            {
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PBPN"), TDB.StrReverse("BOKL"), item.rec, item.BOKL);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PBPN"), TDB.StrReverse("PLYL"), item.rec, item.PLYL);
                TDB.TDBFieldSetValueAsString(DBIndex, TDB.StrReverse("PBPN"), TDB.StrReverse("name"), item.rec, item.name);
            }
        }

        public static List<PBPN> Sort(List<PBPN> PBPN)
        {
            return PBPN.OrderBy(s => s.PLYL).Cast<PBPN>().ToList();
        }
    }
}
