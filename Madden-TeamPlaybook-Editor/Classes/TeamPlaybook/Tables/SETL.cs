using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TDBAccess;

namespace Madden.TeamPlaybook
{
    [Serializable]
    public class SETL
    {
        public int rec { get; set; }
        public int setl { get; set; }
        public int FORM { get; set; }
        public int MOTN { get; set; }
        public int CLAS { get; set; }
        public int SETT { get; set; }
        public int SITT { get; set; }
        public int SLF_ { get; set; }
        public string name { get; set; }
        public int poso { get; set; }

        public override string ToString()
        {
            return
                "Rec#: " + rec +
                "   SETL: " + setl +
                "   FORM: " + FORM +
                "   MOTN: " + MOTN +
                "   CLAS: " + CLAS +
                "   SETT: " + SETT +
                "   SITT: " + SITT +
                "   SLF_: " + SLF_ +
                "   Name: " + name +
                "   poso: " + poso;
        }

        public static List<SETL> GetSETL(int filter = 0, int DBIndex = 0)
        {
            List<SETL> _SubFormations = new List<SETL>();

            TdbTableProperties TableProps = new TdbTableProperties();
            TableProps.Name = new string((char)0, 5);

            // Get Tableprops based on the selected index
            if (!TDB.TDBTableGetProperties(DBIndex, TableNames.GetTables().Find(item => item.name == "SETL").rec, ref TableProps))
                return null;

            List<TdbFieldProperties> tableFields = new List<TdbFieldProperties>();

            for (int i = 0; i < TableProps.FieldCount; i++)
            {
                TdbFieldProperties FieldProps = new TdbFieldProperties();
                FieldProps.Name = new string((char)0, 5);
                TDB.TDBFieldGetProperties(DBIndex, TDB.StrReverse("SETL"), i, ref FieldProps);
                tableFields.Add(FieldProps);
            }

            for (int i = 0; i < TableProps.RecordCount; i++)
            {
                string _name = new string((char)0, (tableFields.Where(field => field.Name == TDB.StrReverse("name")).FirstOrDefault().Size / 8) + 1);

                TDB.TDBFieldGetValueAsString(DBIndex, TDB.StrReverse("SETL"), TDB.StrReverse("name"), i, ref _name);
                _name = _name.Replace(",", "");

                if (filter != 0 && (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("SETL"), TDB.StrReverse("FORM"), i) == filter)
                {
                    _SubFormations.Add(new SETL
                    {
                        rec = i,
                        setl = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("SETL"), TDB.StrReverse("SETL"), i),
                        FORM = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("SETL"), TDB.StrReverse("FORM"), i),
                        MOTN = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("SETL"), TDB.StrReverse("MOTN"), i),
                        CLAS = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("SETL"), TDB.StrReverse("CLAS"), i),
                        SETT = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("SETL"), TDB.StrReverse("SETT"), i),
                        SITT = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("SETL"), TDB.StrReverse("SITT"), i),
                        SLF_ = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("SETL"), TDB.StrReverse("SLF_"), i),
                        name = _name,
                        poso = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("SETL"), TDB.StrReverse("poso"), i)
                    });
                }
                else if (filter == 0)
                {
                    _SubFormations.Add(new SETL
                    {
                        rec = i,
                        setl = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("SETL"), TDB.StrReverse("SETL"), i),
                        FORM = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("SETL"), TDB.StrReverse("FORM"), i),
                        MOTN = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("SETL"), TDB.StrReverse("MOTN"), i),
                        CLAS = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("SETL"), TDB.StrReverse("CLAS"), i),
                        SETT = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("SETL"), TDB.StrReverse("SETT"), i),
                        SITT = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("SETL"), TDB.StrReverse("SITT"), i),
                        SLF_ = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("SETL"), TDB.StrReverse("SLF_"), i),
                        name = _name,
                        poso = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("SETL"), TDB.StrReverse("poso"), i)
                    });
                }
            }
            return _SubFormations;
        }

        public static void SetSETL(List<SETL> SETL, int DBIndex = 0)
        {
            TdbTableProperties TableProps = new TdbTableProperties();
            TableProps.Name = new string((char)0, 5);

            // Get Tableprops based on the selected index
            TDB.TDBTableGetProperties(DBIndex, TableNames.GetTables().Find(item => item.name == "SETL").rec, ref TableProps);

            if (SETL.Count > TableProps.RecordCount)
                for (int i = TableProps.RecordCount - 1; i < SETL.Count - 1; i++)
                {
                    TDB.TDBTableRecordAdd(DBIndex, TDB.StrReverse("SETL"), true);
                }
            else if (SETL.Count < TableProps.RecordCount)
            {
                for (int i = TableProps.RecordCount - 1; i > SETL.Count - 1; i--)
                {
                    TDB.TDBTableRecordRemove(DBIndex, TDB.StrReverse("SETL"), i);
                }
            }

            foreach (SETL item in SETL)
            {
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("SETL"), TDB.StrReverse("SETL"), item.rec, item.setl);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("SETL"), TDB.StrReverse("FORM"), item.rec, item.FORM);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("SETL"), TDB.StrReverse("MOTN"), item.rec, item.MOTN);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("SETL"), TDB.StrReverse("CLAS"), item.rec, item.CLAS);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("SETL"), TDB.StrReverse("SETT"), item.rec, item.SETT);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("SETL"), TDB.StrReverse("SITT"), item.rec, item.SITT);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("SETL"), TDB.StrReverse("SLF_"), item.rec, item.SLF_);
                TDB.TDBFieldSetValueAsString(DBIndex, TDB.StrReverse("SETL"), TDB.StrReverse("name"), item.rec, item.name);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("SETL"), TDB.StrReverse("poso"), item.rec, item.poso);
            }
        }

        public static List<SETL> Sort(List<SETL> SETL)
        {
            return SETL.OrderBy(s => s.setl).Cast<SETL>().ToList();
        }
    }
}
