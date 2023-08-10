using System;
using System.Collections.Generic;
using System.Linq;
using TDBAccess;

namespace Madden.CustomPlaybook
{
    [Serializable]
    public class PIDX
    {
        public int rec { get; set; }
        public int COMF { get; set; }
        public int BOKL { get; set; }
        public int SETL { get; set; }
        public int PLYL { get; set; }
        public int PBFM { get; set; }
        public int SRMM { get; set; }
        public int PBST { get; set; }
        public int SITT { get; set; }
        public int PLYT { get; set; }
        public int ORD_ { get; set; }
        public int PLF_ { get; set; }
        public string name { get; set; }
        public int Flag { get; set; }
        public int risk { get; set; }
        public int motn { get; set; }
        public int vpos { get; set; }

        public override string ToString()
        {
            return
                "Rec#: " + rec +
                "   COMF: " + COMF +
                "   BOKL: " + BOKL +
                "   SETL: " + SETL +
                "   PLYL: " + PLYL +
                "   PBFM: " + PBFM +
                "   SRMM: " + SRMM +
                "   PBST: " + PBST +
                "   SITT: " + SITT +
                "   PLYT: " + PLYT +
                "   ORD_: " + ORD_ +
                "   PLF_: " + PLF_ +
                "   name: " + name +
                "   Flag: " + Flag +
                "   risk: " + risk +
                "   motn: " + motn +
                "   vpos: " + vpos;
        }

        public static List<PIDX> GetPIDX(int filter = 0, int DBIndex = 0)
        {
            List<PIDX> _PIDX = new List<PIDX>();

            TdbTableProperties TableProps = new TdbTableProperties();
            TableProps.Name = new string((char)0, 5);

            // Get Tableprops based on the selected index
            if (!TDB.TDBTableGetProperties(DBIndex, TableNames.GetTables().Find(item => item.name == "PIDX").rec, ref TableProps))
                return null;

            List<TdbFieldProperties> tableFields = new List<TdbFieldProperties>();

            for (int i = 0; i < TableProps.FieldCount; i++)
            {
                TdbFieldProperties FieldProps = new TdbFieldProperties();
                FieldProps.Name = new string((char)0, 5);
                TDB.TDBFieldGetProperties(DBIndex, TDB.StrReverse("PIDX"), i, ref FieldProps);
                tableFields.Add(FieldProps);
            }

            for (int i = 0; i < TableProps.RecordCount; i++)
            {
                string _name = new string((char)0, (tableFields.Where(field => field.Name == TDB.StrReverse("name")).FirstOrDefault().Size / 8) + 1);

                TDB.TDBFieldGetValueAsString(DBIndex, TDB.StrReverse("PBFM"), TDB.StrReverse("name"), i, ref _name);
                _name = _name.Replace(",", "");

                if (filter != 0 && (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PIDX"), TDB.StrReverse("PLYL"), i) == filter)
                {
                    _PIDX.Add(new PIDX
                    {
                        rec = i,
                        COMF = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PIDX"), TDB.StrReverse("COMF"), i),
                        BOKL = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PIDX"), TDB.StrReverse("BOKL"), i),
                        SETL = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PIDX"), TDB.StrReverse("SETL"), i),
                        PLYL = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PIDX"), TDB.StrReverse("PLYL"), i),
                        PBFM = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PIDX"), TDB.StrReverse("PBFM"), i),
                        SRMM = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PIDX"), TDB.StrReverse("SRMM"), i),
                        PBST = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PIDX"), TDB.StrReverse("PBST"), i),
                        SITT = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PIDX"), TDB.StrReverse("SITT"), i),
                        PLYT = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PIDX"), TDB.StrReverse("PLYT"), i),
                        ORD_ = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PIDX"), TDB.StrReverse("ORD_"), i),
                        PLF_ = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PIDX"), TDB.StrReverse("PLF_"), i),
                        name = _name,
                        Flag = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PIDX"), TDB.StrReverse("Flag"), i),
                        risk = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PIDX"), TDB.StrReverse("risk"), i),
                        motn = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PIDX"), TDB.StrReverse("motn"), i),
                        vpos = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PIDX"), TDB.StrReverse("vpos"), i)
                    });
                }
                else if (filter == 0)
                {
                    _PIDX.Add(new PIDX
                    {
                        rec = i,
                        COMF = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PIDX"), TDB.StrReverse("COMF"), i),
                        BOKL = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PIDX"), TDB.StrReverse("BOKL"), i),
                        SETL = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PIDX"), TDB.StrReverse("SETL"), i),
                        PLYL = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PIDX"), TDB.StrReverse("PLYL"), i),
                        PBFM = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PIDX"), TDB.StrReverse("PBFM"), i),
                        SRMM = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PIDX"), TDB.StrReverse("SRMM"), i),
                        PBST = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PIDX"), TDB.StrReverse("PBST"), i),
                        SITT = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PIDX"), TDB.StrReverse("SITT"), i),
                        PLYT = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PIDX"), TDB.StrReverse("PLYT"), i),
                        ORD_ = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PIDX"), TDB.StrReverse("ORD_"), i),
                        PLF_ = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PIDX"), TDB.StrReverse("PLF_"), i),
                        name = _name,
                        Flag = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PIDX"), TDB.StrReverse("Flag"), i),
                        risk = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PIDX"), TDB.StrReverse("risk"), i),
                        motn = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PIDX"), TDB.StrReverse("motn"), i),
                        vpos = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PIDX"), TDB.StrReverse("vpos"), i)
                    });
                }
            }
            return _PIDX;
        }

        public static void SetPIDX(List<PIDX> PIDX, int DBIndex = 0)
        {
            TdbTableProperties TableProps = new TdbTableProperties();
            TableProps.Name = new string((char)0, 5);

            // Get Tableprops based on the selected index
            TDB.TDBTableGetProperties(DBIndex, TableNames.GetTables().Find(item => item.name == "PIDX").rec, ref TableProps);

            if (PIDX.Count > TableProps.RecordCount)
                for (int i = TableProps.RecordCount - 1; i < PIDX.Count - 1; i++)
                {
                    TDB.TDBTableRecordAdd(DBIndex, TDB.StrReverse("PIDX"), true);
                }
            else if (PIDX.Count < TableProps.RecordCount)
            {
                for (int i = TableProps.RecordCount - 1; i > PIDX.Count - 1; i--)
                {
                    TDB.TDBTableRecordRemove(DBIndex, TDB.StrReverse("PIDX"), i);
                }
            }

            foreach (PIDX item in PIDX)
            {
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PIDX"), TDB.StrReverse("COMF"), item.rec, item.COMF);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PIDX"), TDB.StrReverse("BOKL"), item.rec, item.BOKL);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PIDX"), TDB.StrReverse("SETL"), item.rec, item.SETL);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PIDX"), TDB.StrReverse("PLYL"), item.rec, item.PLYL);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PIDX"), TDB.StrReverse("PBFM"), item.rec, item.PBFM);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PIDX"), TDB.StrReverse("SRMM"), item.rec, item.SRMM);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PIDX"), TDB.StrReverse("PBST"), item.rec, item.PBST);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PIDX"), TDB.StrReverse("SITT"), item.rec, item.SITT);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PIDX"), TDB.StrReverse("PLYT"), item.rec, item.PLYT);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PIDX"), TDB.StrReverse("ORD_"), item.rec, item.ORD_);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PIDX"), TDB.StrReverse("PLF_"), item.rec, item.PLF_);
                TDB.TDBFieldSetValueAsString(DBIndex, TDB.StrReverse("PIDX"), TDB.StrReverse("name"), item.rec, item.name);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PIDX"), TDB.StrReverse("Flag"), item.rec, item.Flag);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PIDX"), TDB.StrReverse("risk"), item.rec, item.risk);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PIDX"), TDB.StrReverse("motn"), item.rec, item.motn);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PIDX"), TDB.StrReverse("vpos"), item.rec, item.vpos);
            }
        }

        public static List<PIDX> Sort(List<PIDX> PIDX)
        {
            return PIDX.OrderBy(s => s.PLYL).Cast<PIDX>().ToList();
        }
    }
}
