using System;
using System.Collections.Generic;
using System.Linq;
using TDBAccess;

namespace Madden.CustomPlaybook
{
    [Serializable]
    public class STID
    {
        public int rec { get; set; }
        public int BOKL { get; set; }
        public int SETL { get; set; }  
        public int PBFM { get; set; }
        public int PBST { get; set; }
        public int SPF_ { get; set; }

        public override string ToString()
        {
            return
                "Rec#: " + rec + "\t" +
                "   BOKL: " + BOKL + "\t" +
                "   SETL: " + SETL + "\t" +
                "   PBFM: " + PBFM + "\t" +
                "   PBST: " + PBST + "\t" +
                "   SPF_: " + SPF_;
        }

        public static List<STID> GetSTID(int filter = 0, int DBIndex = 0)
        {
            List<STID> _STID = new List<STID>();

            TdbTableProperties TableProps = new TdbTableProperties();
            TableProps.Name = new string((char)0, 5);

            // Get Tableprops based on the selected index
            if (!TDB.TDBTableGetProperties(DBIndex, TableNames.GetTables().Find(item => item.name == "STID").rec, ref TableProps))
                return null;

            for (int i = 0; i < TableProps.RecordCount; i++)
            {
                if (filter != 0 && (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("STID"), TDB.StrReverse("PBFM"), i) == filter)
                {
                    _STID.Add(new STID
                    {
                        rec = i,
                        BOKL = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("STID"), TDB.StrReverse("BOKL"), i),
                        SETL = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("STID"), TDB.StrReverse("SETL"), i),
                        PBFM = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("STID"), TDB.StrReverse("PBFM"), i),
                        PBST = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("STID"), TDB.StrReverse("PBST"), i),
                        SPF_ = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("STID"), TDB.StrReverse("SPF_"), i)
                    });
                }
                else if (filter == 0)
                {
                    _STID.Add(new STID
                    {
                        rec = i,
                        BOKL = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("STID"), TDB.StrReverse("BOKL"), i),
                        SETL = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("STID"), TDB.StrReverse("SETL"), i),
                        PBFM = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("STID"), TDB.StrReverse("PBFM"), i),
                        PBST = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("STID"), TDB.StrReverse("PBST"), i),
                        SPF_ = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("STID"), TDB.StrReverse("SPF_"), i)
                    });
                }
            }

            return _STID;
        }

        public static void SetSTID(List<STID> STID, int DBIndex = 0)
        {
            TdbTableProperties TableProps = new TdbTableProperties();
            TableProps.Name = new string((char)0, 5);

            // Get Tableprops based on the selected index
            TDB.TDBTableGetProperties(DBIndex, TableNames.GetTables().Find(item => item.name == "STID").rec, ref TableProps);

            if (STID.Count > TableProps.RecordCount)
                for (int i = TableProps.RecordCount - 1; i < STID.Count - 1; i++)
                {
                    TDB.TDBTableRecordAdd(DBIndex, TDB.StrReverse("STID"), true);
                }
            else if (STID.Count < TableProps.RecordCount)
            {
                for (int i = TableProps.RecordCount - 1; i > STID.Count - 1; i--)
                {
                    TDB.TDBTableRecordRemove(DBIndex, TDB.StrReverse("STID"), i);
                }
            }

            foreach (STID item in STID)
            {
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("STID"), TDB.StrReverse("BOKL"), item.rec, item.BOKL);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("STID"), TDB.StrReverse("SETL"), item.rec, item.SETL);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("STID"), TDB.StrReverse("PBFM"), item.rec, item.PBFM);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("STID"), TDB.StrReverse("PBST"), item.rec, item.PBST);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("STID"), TDB.StrReverse("SPF_"), item.rec, item.SPF_);
            }
        }

        public static List<STID> Sort(List<STID> STID)
        {
            return STID.OrderBy(s => s.PBST).Cast<STID>().ToList();
        }
    }
}
