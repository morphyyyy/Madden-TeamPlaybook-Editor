using System;
using System.Collections.Generic;
using System.Linq;
using TDBAccess;

namespace Madden.CustomPlaybook
{
    [Serializable]
    public class CPFM
    {
        public int rec { get; set; }
        public int FORM { get; set; }
        public int FTYP { get; set; }
        public string name { get; set; }

        public override string ToString()
        {
            return
                "Rec#: " + rec + "\t" +
                "   FORM: " + FORM + "\t" +
                "   FTYP: " + FTYP + "\t" +
                "   Name: " + name;
        }

        public static List<CPFM> GetCPFM(int filter = 0, int DBIndex = 0)
        {
            List<CPFM> _Formations = new List<CPFM>();

            TdbTableProperties TableProps = new TdbTableProperties();
            TableProps.Name = new string((char)0, 5);

            // Get Tableprops based on the selected index
            if (!TDB.TDBTableGetProperties(DBIndex, TableNames.GetTables().Find(item => item.name == "CPFM").rec, ref TableProps))
                return null;

            List<TdbFieldProperties> tableFields = new List<TdbFieldProperties>();

            for (int i = 0; i < TableProps.FieldCount; i++)
            {
                TdbFieldProperties FieldProps = new TdbFieldProperties();
                FieldProps.Name = new string((char)0, 5);
                TDB.TDBFieldGetProperties(DBIndex, TDB.StrReverse("CPFM"), i, ref FieldProps);
                tableFields.Add(FieldProps);
            }

            for (int i = 0; i < TableProps.RecordCount; i++)
            {
                string _name = new string((char)0, (tableFields.Where(field => field.Name == TDB.StrReverse("name")).FirstOrDefault().Size / 8) + 1);

                TDB.TDBFieldGetValueAsString(DBIndex, TDB.StrReverse("CPFM"), TDB.StrReverse("name"), i, ref _name);
                _name = _name.Replace(",", "");

                if (filter != 0 && (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("CPFM"), TDB.StrReverse("FTYP"), i) == filter)
                {
                    _Formations.Add(new CPFM
                    {
                        rec = i,
                        FORM = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("CPFM"), TDB.StrReverse("FORM"), i),
                        FTYP = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("CPFM"), TDB.StrReverse("FTYP"), i),
                        name = _name
                    });
                }
                else if (filter == 0)
                {
                    _Formations.Add(new CPFM
                    {
                        rec = i,
                        FORM = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("CPFM"), TDB.StrReverse("FORM"), i),
                        FTYP = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("CPFM"), TDB.StrReverse("FTYP"), i),
                        name = _name
                    });
                }
            }

            //_Formations = _Formations.ToList().OrderBy(s => s.FTYP).ThenBy(s => s.form).Cast<FORM>().ToList();

            return _Formations;
        }

        public static void SetCPFM(List<CPFM> CPFM, int DBIndex = 0)
        {
            TdbTableProperties TableProps = new TdbTableProperties();
            TableProps.Name = new string((char)0, 5);

            // Get Tableprops based on the selected index
            TDB.TDBTableGetProperties(DBIndex, TableNames.GetTables().Find(item => item.name == "CPFM").rec, ref TableProps);

            if (CPFM.Count > TableProps.RecordCount)
                for (int i = TableProps.RecordCount - 1; i < CPFM.Count - 1; i++)
                {
                    TDB.TDBTableRecordAdd(DBIndex, TDB.StrReverse("CPFM"), true);
                }
            else if (CPFM.Count < TableProps.RecordCount)
            {
                for (int i = TableProps.RecordCount - 1; i > CPFM.Count - 1; i--)
                {
                    TDB.TDBTableRecordRemove(DBIndex, TDB.StrReverse("CPFM"), i);
                }
            }

            foreach (CPFM item in CPFM)
            {
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("CPFM"), TDB.StrReverse("FORM"), item.rec, item.FORM);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("CPFM"), TDB.StrReverse("FTYP"), item.rec, item.FTYP);
                TDB.TDBFieldSetValueAsString(DBIndex, TDB.StrReverse("CPFM"), TDB.StrReverse("name"), item.rec, item.name);
            }
        }

        public static List<CPFM> Sort(List<CPFM> CPFM)
        {
            return CPFM.OrderBy(s => s.FTYP).ThenBy(s => s.FORM).Cast<CPFM>().ToList();
        }
    }
}
