using System;
using System.Collections.Generic;
using System.Linq;
using TDBAccess;

namespace Madden.CustomPlaybook
{
    [Serializable]
    public class CPFT
    {
        public int rec { get; set; }
        public int FMOF { get; set; }
        public int FTYP { get; set; }
        public string name { get; set; }

        public override string ToString()
        {
            return
                "Rec#: " + rec + "\t" +
                "   FMOF: " + FMOF + "\t" +
                "   FTYP: " + FTYP + "\t" +
                "   Name: " + name;
        }

        public static List<CPFT> GetCPFT(int filter = 0, int DBIndex = 0)
        {
            List<CPFT> _Formations = new List<CPFT>();

            TdbTableProperties TableProps = new TdbTableProperties();
            TableProps.Name = new string((char)0, 5);

            // Get Tableprops based on the selected index
            if (!TDB.TDBTableGetProperties(DBIndex, TableNames.GetTables().Find(item => item.name == "CPFT").rec, ref TableProps))
                return null;

            List<TdbFieldProperties> tableFields = new List<TdbFieldProperties>();

            for (int i = 0; i < TableProps.FieldCount; i++)
            {
                TdbFieldProperties FieldProps = new TdbFieldProperties();
                FieldProps.Name = new string((char)0, 5);
                TDB.TDBFieldGetProperties(DBIndex, TDB.StrReverse("CPFT"), i, ref FieldProps);
                tableFields.Add(FieldProps);
            }

            for (int i = 0; i < TableProps.RecordCount; i++)
            {
                string _name = new string((char)0, (tableFields.Where(field => field.Name == TDB.StrReverse("name")).FirstOrDefault().Size / 8) + 1);

                TDB.TDBFieldGetValueAsString(DBIndex, TDB.StrReverse("CPFT"), TDB.StrReverse("name"), i, ref _name);
                _name = _name.Replace(",", "");

                if (filter != 0 && (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("CPFT"), TDB.StrReverse("FTYP"), i) == filter)
                {
                    _Formations.Add(new CPFT
                    {
                        rec = i,
                        FMOF = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("CPFT"), TDB.StrReverse("FMOF"), i),
                        FTYP = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("CPFT"), TDB.StrReverse("FTYP"), i),
                        name = _name
                    });
                }
                else if (filter == 0)
                {
                    _Formations.Add(new CPFT
                    {
                        rec = i,
                        FMOF = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("CPFT"), TDB.StrReverse("FMOF"), i),
                        FTYP = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("CPFT"), TDB.StrReverse("FTYP"), i),
                        name = _name
                    });
                }
            }

            //_Formations = _Formations.ToList().OrderBy(s => s.FTYP).ThenBy(s => s.form).Cast<FORM>().ToList();

            return _Formations;
        }

        public static void SetCPFT(List<CPFT> CPFT, int DBIndex = 0)
        {
            TdbTableProperties TableProps = new TdbTableProperties();
            TableProps.Name = new string((char)0, 5);

            // Get Tableprops based on the selected index
            TDB.TDBTableGetProperties(DBIndex, TableNames.GetTables().Find(item => item.name == "CPFT").rec, ref TableProps);

            if (CPFT.Count > TableProps.RecordCount)
                for (int i = TableProps.RecordCount - 1; i < CPFT.Count - 1; i++)
                {
                    TDB.TDBTableRecordAdd(DBIndex, TDB.StrReverse("CPFT"), true);
                }
            else if (CPFT.Count < TableProps.RecordCount)
            {
                for (int i = TableProps.RecordCount - 1; i > CPFT.Count - 1; i--)
                {
                    TDB.TDBTableRecordRemove(DBIndex, TDB.StrReverse("CPFT"), i);
                }
            }

            foreach (CPFT item in CPFT)
            {
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("CPFT"), TDB.StrReverse("FMOF"), item.rec, item.FMOF);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("CPFT"), TDB.StrReverse("FTYP"), item.rec, item.FTYP);
                TDB.TDBFieldSetValueAsString(DBIndex, TDB.StrReverse("CPFT"), TDB.StrReverse("name"), item.rec, item.name);
            }
        }

        public static List<CPFT> Sort(List<CPFT> CPFT)
        {
            return CPFT.OrderBy(s => s.FTYP).Cast<CPFT>().ToList();
        }
    }
}
