using System;
using System.Collections.Generic;
using System.Linq;
using TDBAccess;

namespace Madden.CustomPlaybook
{
    [Serializable]
    public class PBPL
    {
        public int rec { get; set; }
        public int COMF { get; set; }
        public int SETL { get; set; }
        public int PLYL { get; set; }
        public int SRMM { get; set; }
        public int SITT { get; set; }
        public int PLTY { get; set; }
        public int PLF_ { get; set; }
        public string name { get; set; }
        public int risk { get; set; }
        public int motn { get; set; }
        public int vpos { get; set; }

        public override string ToString()
        {
            return
                "Rec#: " + rec +
                "   COMF: " + COMF +
                "   SETL: " + SETL +
                "   PLYL: " + PLYL +
                "   SRMM: " + SRMM +
                "   SITT: " + SITT +
                "   PLTY: " + PLTY +
                "   PLF_: " + PLF_ +
                "   name: " + name +
                "   risk: " + risk +
                "   motn: " + motn +
                "   vpos: " + vpos;
        }

        public static List<PBPL> GetPBPL(int filter = 0, int DBIndex = 0)
        {
            List<PBPL> _PBPL = new List<PBPL>();

            TdbTableProperties TableProps = new TdbTableProperties();
            TableProps.Name = new string((char)0, 5);

            // Get Tableprops based on the selected index
            if (!TDB.TDBTableGetProperties(DBIndex, TableNames.GetTables().Find(item => item.name == "PBPL").rec, ref TableProps))
                return null;

            List<TdbFieldProperties> tableFields = new List<TdbFieldProperties>();

            for (int i = 0; i < TableProps.FieldCount; i++)
            {
                TdbFieldProperties FieldProps = new TdbFieldProperties();
                FieldProps.Name = new string((char)0, 5);
                TDB.TDBFieldGetProperties(DBIndex, TDB.StrReverse("PBPL"), i, ref FieldProps);
                tableFields.Add(FieldProps);
            }

            for (int i = 0; i < TableProps.RecordCount; i++)
            {
                string _name = new string((char)0, (tableFields.Where(field => field.Name == TDB.StrReverse("name")).FirstOrDefault().Size / 8) + 1);

                TDB.TDBFieldGetValueAsString(DBIndex, TDB.StrReverse("PBPL"), TDB.StrReverse("name"), i, ref _name);
                _name = _name.Replace(",", "");

                if (filter != 0 && (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PBPL"), TDB.StrReverse("PBST"), i) == filter)
                {
                    _PBPL.Add(new PBPL
                    {
                        rec = i,
                        COMF = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PBPL"), TDB.StrReverse("COMF"), i),
                        SETL = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PBPL"), TDB.StrReverse("SETL"), i),
                        SRMM = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PBPL"), TDB.StrReverse("SRMM"), i),
                        SITT = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PBPL"), TDB.StrReverse("SITT"), i),
                        PLYL = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PBPL"), TDB.StrReverse("PLYL"), i),
                        PLTY = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PBPL"), TDB.StrReverse("PLTY"), i),
                        PLF_ = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PBPL"), TDB.StrReverse("PLF_"), i),
                        name = _name,
                        risk = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PBPL"), TDB.StrReverse("risk"), i),
                        motn = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PBPL"), TDB.StrReverse("motn"), i),
                        vpos = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PBPL"), TDB.StrReverse("vpos"), i)
                    });
                }
                else if (filter == 0)
                {
                    _PBPL.Add(new PBPL
                    {
                        rec = i,
                        COMF = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PBPL"), TDB.StrReverse("COMF"), i),
                        SETL = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PBPL"), TDB.StrReverse("SETL"), i),
                        SRMM = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PBPL"), TDB.StrReverse("SRMM"), i),
                        SITT = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PBPL"), TDB.StrReverse("SITT"), i),
                        PLYL = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PBPL"), TDB.StrReverse("PLYL"), i),
                        PLTY = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PBPL"), TDB.StrReverse("PLTY"), i),
                        PLF_ = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PBPL"), TDB.StrReverse("PLF_"), i),
                        name = _name,
                        risk = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PBPL"), TDB.StrReverse("risk"), i),
                        motn = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PBPL"), TDB.StrReverse("motn"), i),
                        vpos = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PBPL"), TDB.StrReverse("vpos"), i)
                    });
                }
            }

            //_PBPL = _PBPL.ToList().OrderBy(s => s.PBST).ThenBy(s => s.ord_).Cast<PBPL>().ToList();
            //for (int i = 0; i > _PBPL.Count; i++)
            //{
            //    _PBPL[i].rec = i;
            //}

            return _PBPL;
        }

        public static void SetPBPL(List<PBPL> PBPL, int DBIndex = 0)
        {
            TdbTableProperties TableProps = new TdbTableProperties();
            TableProps.Name = new string((char)0, 5);

            // Get Tableprops based on the selected index
            TDB.TDBTableGetProperties(DBIndex, TableNames.GetTables().Find(item => item.name == "PBPL").rec, ref TableProps);

            if (PBPL.Count > TableProps.RecordCount)
                for (int i = TableProps.RecordCount - 1; i < PBPL.Count - 1; i++)
                {
                    TDB.TDBTableRecordAdd(DBIndex, TDB.StrReverse("PBPL"), true);
                }
            else if (PBPL.Count < TableProps.RecordCount)
            {
                for (int i = TableProps.RecordCount - 1; i > PBPL.Count - 1; i--)
                {
                    TDB.TDBTableRecordRemove(DBIndex, TDB.StrReverse("PBPL"), i);
                }
            }

            foreach (PBPL item in PBPL)
            {
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PBPL"), TDB.StrReverse("COMF"), item.rec, item.COMF);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PBPL"), TDB.StrReverse("SETL"), item.rec, item.SETL);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PBPL"), TDB.StrReverse("SRMM"), item.rec, item.SRMM);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PBPL"), TDB.StrReverse("SITT"), item.rec, item.SITT);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PBPL"), TDB.StrReverse("PLYL"), item.rec, item.PLYL);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PBPL"), TDB.StrReverse("PLTY"), item.rec, item.PLTY);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PBPL"), TDB.StrReverse("PLF_"), item.rec, item.PLF_);
                TDB.TDBFieldSetValueAsString(DBIndex, TDB.StrReverse("PBPL"), TDB.StrReverse("name"), item.rec, item.name);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PBPL"), TDB.StrReverse("risk"), item.rec, item.risk);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PBPL"), TDB.StrReverse("motn"), item.rec, item.motn);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PBPL"), TDB.StrReverse("vpos"), item.rec, item.vpos);
            }
        }

        public static List<PBPL> Sort(List<PBPL> PBPL)
        {
            return PBPL.OrderBy(s => s.PLTY).ThenBy(s => s.PLF_).Cast<PBPL>().ToList();
        }
    }
}
