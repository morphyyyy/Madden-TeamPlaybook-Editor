using System;
using System.Collections.Generic;
using System.Linq;
using TDBAccess;

namespace Madden.TeamPlaybook
{
    [Serializable]
    public class FORM
    {
        public int rec { get; set; }
        /// <summary>
        /// SETL.FORM
        /// </summary>
        public int form { get; set; }
        /// <summary>
        /// PBFM.FTYP, PBAU.FTYP
        /// </summary>
        public int FTYP { get; set; }
        /// <summary>
        /// PBFM.name
        /// </summary>
        public string name { get; set; }

        public override string ToString()
        {
            return
                "Rec#: " + rec + "\t" +
                "   FORM: " + form + "\t" +
                "   FTYP: " + FTYP + "\t" +
                "   Name: " + name;
        }

        public static List<FORM> GetFORM(int filter = 0, int DBIndex = 0)
        {
            List<FORM> _Formations = new List<FORM>();

            TdbTableProperties TableProps = new TdbTableProperties();
            TableProps.Name = new string((char)0, 5);

            // Get Tableprops based on the selected index
            if (!TDB.TDBTableGetProperties(DBIndex, TableNames.GetTables().Find(item => item.name == "FORM").rec, ref TableProps))
                return null;

            List<TdbFieldProperties> tableFields = new List<TdbFieldProperties>();

            for (int i = 0; i < TableProps.FieldCount; i++)
            {
                TdbFieldProperties FieldProps = new TdbFieldProperties();
                FieldProps.Name = new string((char)0, 5);
                TDB.TDBFieldGetProperties(DBIndex, TDB.StrReverse("FORM"), i, ref FieldProps);
                tableFields.Add(FieldProps);
            }

            for (int i = 0; i < TableProps.RecordCount; i++)
            {
                string _name = new string((char)0, (tableFields.FirstOrDefault(field => field.Name == TDB.StrReverse("name")).Size / 8) + 1);

                TDB.TDBFieldGetValueAsString(DBIndex, TDB.StrReverse("FORM"), TDB.StrReverse("name"), i, ref _name);
                _name = _name.Replace(",", "");

                if (filter != 0 && (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("FORM"), TDB.StrReverse("FTYP"), i) == filter)
                {
                    _Formations.Add(new FORM
                    {
                        rec = i,
                        form = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("FORM"), TDB.StrReverse("FORM"), i),
                        FTYP = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("FORM"), TDB.StrReverse("FTYP"), i),
                        name = _name
                    });
                }
                else if (filter == 0)
                {
                    _Formations.Add(new FORM
                    {
                        rec = i,
                        form = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("FORM"), TDB.StrReverse("FORM"), i),
                        FTYP = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("FORM"), TDB.StrReverse("FTYP"), i),
                        name = _name
                    });
                }
            }

            //_Formations = _Formations.ToList().OrderBy(s => s.FTYP).ThenBy(s => s.form).Cast<FORM>().ToList();

            return _Formations;
        }

        public static void SetFORM(List<FORM> FORM, int DBIndex = 0)
        {
            TdbTableProperties TableProps = new TdbTableProperties();
            TableProps.Name = new string((char)0, 5);

            // Get Tableprops based on the selected index
            TDB.TDBTableGetProperties(DBIndex, TableNames.GetTables().Find(item => item.name == "FORM").rec, ref TableProps);

            if (FORM.Count > TableProps.RecordCount)
                for (int i = TableProps.RecordCount - 1; i < FORM.Count - 1; i++)
                {
                    TDB.TDBTableRecordAdd(DBIndex, TDB.StrReverse("FORM"), true);
                }
            else if (FORM.Count < TableProps.RecordCount)
            {
                for (int i = TableProps.RecordCount - 1; i > FORM.Count - 1; i--)
                {
                    TDB.TDBTableRecordRemove(DBIndex, TDB.StrReverse("FORM"), i);
                }
            }

            foreach (FORM item in FORM)
            {
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("FORM"), TDB.StrReverse("FORM"), item.rec, item.form);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("FORM"), TDB.StrReverse("FTYP"), item.rec, item.FTYP);
                TDB.TDBFieldSetValueAsString(DBIndex, TDB.StrReverse("FORM"), TDB.StrReverse("name"), item.rec, item.name);
            }
        }

        public static List<FORM> Sort(List<FORM> FORM)
        {
            return FORM.OrderBy(s => s.FTYP).ThenBy(s => s.form).Cast<FORM>().ToList();
        }
    }
}
