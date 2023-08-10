using System;
using System.Collections.Generic;
using System.Linq;
using TDBAccess;

namespace Madden.CustomPlaybook
{
    [Serializable]
    public class BSMO
    {
        public int rec { get; set; }
        public int SETL { get; set; }
        public int FORM { get; set; }
        public int MOTN { get; set; }
        public int SETT { get; set; }
        public int SITT { get; set; }
        public int SLF_ { get; set; }
        public string name { get; set; }
        public int poso { get; set; }

        public override string ToString()
        {
            return
                "Rec#: " + rec + "\t" +
                "   SETL: " + SETL + "\t" +
                "   FORM: " + FORM + "\t" +
                "   MOTN: " + MOTN + "\t" +
                "   SETT: " + SETT + "\t" +
                "   SITT: " + SITT + "\t" +
                "   SLF_: " + SLF_ + "\t" +
                "   name: " + name + "\t" +
                "   poso: " + poso;
        }

        public static List<BSMO> GetBSMO(int filter = 0, int DBIndex = 0)
        {
            List<BSMO> _BSMO = new List<BSMO>();

            TdbTableProperties TableProps = new TdbTableProperties();
            TableProps.Name = new string((char)0, 5);

            // Get Tableprops based on the selected index
            if (!TDB.TDBTableGetProperties(DBIndex, TableNames.GetTables().Find(item => item.name == "BSMO").rec, ref TableProps))
                return null;

            List<TdbFieldProperties> tableFields = new List<TdbFieldProperties>();

            for (int i = 0; i < TableProps.FieldCount; i++)
            {
                TdbFieldProperties FieldProps = new TdbFieldProperties();
                FieldProps.Name = new string((char)0, 5);
                TDB.TDBFieldGetProperties(DBIndex, TDB.StrReverse("BSMO"), i, ref FieldProps);
                tableFields.Add(FieldProps);
            }

            for (int i = 0; i < TableProps.RecordCount; i++)
            {
                string _name = new string((char)0, (tableFields.Where(field => field.Name == TDB.StrReverse("name")).FirstOrDefault().Size / 8) + 1);

                TDB.TDBFieldGetValueAsString(DBIndex, TDB.StrReverse("BSMO"), TDB.StrReverse("name"), i, ref _name);
                _name = _name.Replace(",", "");

                if (filter != 0 && (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("BSMO"), TDB.StrReverse("FTYP"), i) == filter)
                {
                    _BSMO.Add(new BSMO
                    {
                        rec = i,
                        SETL = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("BSMO"), TDB.StrReverse("SETL"), i),
                        FORM = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("BSMO"), TDB.StrReverse("FORM"), i),
                        MOTN = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("BSMO"), TDB.StrReverse("MOTN"), i),
                        SETT = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("BSMO"), TDB.StrReverse("SETT"), i),
                        SITT = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("BSMO"), TDB.StrReverse("SITT"), i),
                        SLF_ = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("BSMO"), TDB.StrReverse("SLF_"), i),
                        name = _name,
                        poso = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("BSMO"), TDB.StrReverse("poso"), i)
                    });
                }
                else if (filter == 0)
                {
                    _BSMO.Add(new BSMO
                    {
                        rec = i,
                        SETL = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("BSMO"), TDB.StrReverse("SETL"), i),
                        FORM = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("BSMO"), TDB.StrReverse("FORM"), i),
                        MOTN = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("BSMO"), TDB.StrReverse("MOTN"), i),
                        SETT = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("BSMO"), TDB.StrReverse("SETT"), i),
                        SITT = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("BSMO"), TDB.StrReverse("SITT"), i),
                        SLF_ = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("BSMO"), TDB.StrReverse("SLF_"), i),
                        name = _name,
                        poso = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("BSMO"), TDB.StrReverse("poso"), i)
                    });
                }
            }

            return _BSMO;
        }

        public static void SetBSMO(List<BSMO> BSMO, int DBIndex = 0)
        {
            TdbTableProperties TableProps = new TdbTableProperties();
            TableProps.Name = new string((char)0, 5);

            // Get Tableprops based on the selected index
            TDB.TDBTableGetProperties(DBIndex, TableNames.GetTables().Find(item => item.name == "BSMO").rec, ref TableProps);

            if (BSMO.Count > TableProps.RecordCount)
                for (int i = TableProps.RecordCount - 1; i < BSMO.Count - 1; i++)
                {
                    TDB.TDBTableRecordAdd(DBIndex, TDB.StrReverse("BSMO"), true);
                }
            else if (BSMO.Count < TableProps.RecordCount)
            {
                for (int i = TableProps.RecordCount - 1; i > BSMO.Count - 1; i--)
                {
                    TDB.TDBTableRecordRemove(DBIndex, TDB.StrReverse("BSMO"), i);
                }
            }

            foreach (BSMO item in BSMO)
            {
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("BSMO"), TDB.StrReverse("SETL"), item.rec, item.SETL);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("BSMO"), TDB.StrReverse("FORM"), item.rec, item.FORM);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("BSMO"), TDB.StrReverse("FTYP"), item.rec, item.MOTN);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("BSMO"), TDB.StrReverse("SETT"), item.rec, item.SETT);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("BSMO"), TDB.StrReverse("SITT"), item.rec, item.SITT);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("BSMO"), TDB.StrReverse("SLF_"), item.rec, item.SLF_);
                TDB.TDBFieldSetValueAsString(DBIndex, TDB.StrReverse("BSMO"), TDB.StrReverse("name"), item.rec, item.name);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("BSMO"), TDB.StrReverse("poso"), item.rec, item.poso);
            }
        }

        public static List<BSMO> Sort(List<BSMO> BSMO)
        {
            return BSMO.OrderBy(s => s.FORM).Cast<BSMO>().ToList();
        }
    }
}
