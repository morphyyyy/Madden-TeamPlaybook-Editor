using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TDBAccess;

namespace Madden.TeamPlaybook
{
    [Serializable]
    public class PLYL
    {
        public int rec { get; set; }
        public int SETL { get; set; }
        public int plyl { get; set; }
        public int SRMM { get; set; }
        public int SITT { get; set; }
        public int PLYT { get; set; }
        public int PLF_ { get; set; }
        public string name { get; set; }
        public int risk { get; set; }
        public int motn { get; set; }
        public int phlp { get; set; }
        public int vpos { get; set; }

        public override string ToString()
        {
            return
                "Rec#: " + rec +
                "   SETL: " + SETL +
                "   PLYL: " + plyl +
                "   SRMM: " + SRMM +
                "   SITT: " + SITT +
                "   PLYT: " + PLYT +
                "   PLF_: " + PLF_ +
                "   Name: " + name +
                "   risk: " + risk +
                "   motn: " + motn +
                "   phlp: " + phlp +
                "   vpos: " + vpos;
        }

        public static List<PLYL> GetPLYL(int filter = 0, int DBIndex = 0)
        {
            List<PLYL> _PLYL = new List<PLYL>();

            TdbTableProperties TableProps = new TdbTableProperties();
            TableProps.Name = new string((char)0, 5);

            // Get Tableprops based on the selected index
            if (!TDB.TDBTableGetProperties(DBIndex, TableNames.GetTables().Find(item => item.name == "PLYL").rec, ref TableProps))
                return null;

            List<TdbFieldProperties> tableFields = new List<TdbFieldProperties>();

            for (int i = 0; i < TableProps.FieldCount; i++)
            {
                TdbFieldProperties FieldProps = new TdbFieldProperties();
                FieldProps.Name = new string((char)0, 5);
                TDB.TDBFieldGetProperties(DBIndex, TDB.StrReverse("PLYL"), i, ref FieldProps);
                tableFields.Add(FieldProps);
            }

            for (int i = 0; i < TableProps.RecordCount; i++)
            {
                string _name = new string((char)0, (tableFields.Where(field => field.Name == TDB.StrReverse("name")).FirstOrDefault().Size / 8) + 1);

                TDB.TDBFieldGetValueAsString(DBIndex, TDB.StrReverse("PLYL"), TDB.StrReverse("name"), i, ref _name);
                _name = _name.Replace(",", "");

                _PLYL.Add(new PLYL
                {
                    rec = i,
                    SETL = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLYL"), TDB.StrReverse("SETL"), i),
                    plyl = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLYL"), TDB.StrReverse("PLYL"), i),
                    SRMM = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLYL"), TDB.StrReverse("SRMM"), i),
                    SITT = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLYL"), TDB.StrReverse("SITT"), i),
                    PLYT = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLYL"), TDB.StrReverse("PLYT"), i),
                    PLF_ = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLYL"), TDB.StrReverse("PLF_"), i),
                    name = _name,
                    risk = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLYL"), TDB.StrReverse("risk"), i),
                    motn = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLYL"), TDB.StrReverse("motn"), i),
                    phlp = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLYL"), TDB.StrReverse("phlp"), i),
                    vpos = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLYL"), TDB.StrReverse("vpos"), i)
                });
            }
            return _PLYL;
        }

        public static void SetPLYL(List<PLYL> PLYL, int DBIndex = 0)
        {
            TdbTableProperties TableProps = new TdbTableProperties();
            TableProps.Name = new string((char)0, 5);

            // Get Tableprops based on the selected index
            TDB.TDBTableGetProperties(DBIndex, TableNames.GetTables().Find(item => item.name == "PLYL").rec, ref TableProps);

            if (PLYL.Count > TableProps.RecordCount)
                for (int i = TableProps.RecordCount - 1; i < PLYL.Count - 1; i++)
                {
                    TDB.TDBTableRecordAdd(DBIndex, TDB.StrReverse("PLYL"), true);
                }
            else if (PLYL.Count < TableProps.RecordCount)
            {
                for (int i = TableProps.RecordCount - 1; i > PLYL.Count - 1; i--)
                {
                    TDB.TDBTableRecordRemove(DBIndex, TDB.StrReverse("PLYL"), i);
                }
            }

            foreach (PLYL item in PLYL)
            {
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PLYL"), TDB.StrReverse("SETL"), item.rec, item.SETL);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PLYL"), TDB.StrReverse("PLYL"), item.rec, item.plyl);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PLYL"), TDB.StrReverse("SRMM"), item.rec, item.SRMM);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PLYL"), TDB.StrReverse("SITT"), item.rec, item.SITT);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PLYL"), TDB.StrReverse("PLYT"), item.rec, item.PLYT);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PLYL"), TDB.StrReverse("PLF_"), item.rec, item.PLF_);
                TDB.TDBFieldSetValueAsString(DBIndex, TDB.StrReverse("PLYL"), TDB.StrReverse("name"), item.rec, item.name);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PLYL"), TDB.StrReverse("risk"), item.rec, item.risk);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PLYL"), TDB.StrReverse("motn"), item.rec, item.motn);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PLYL"), TDB.StrReverse("phlp"), item.rec, item.phlp);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PLYL"), TDB.StrReverse("vpos"), item.rec, item.vpos);
            }
        }

        public static List<PLYL> Sort(List<PLYL> PLYL)
        {
            return PLYL.OrderBy(s => s.plyl).Cast<PLYL>().ToList();
        }
    }
}
