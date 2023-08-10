using System;
using System.Collections.Generic;
using System.Linq;
using TDBAccess;

namespace Madden.CustomPlaybook
{
    [Serializable]
    public class SPKG
    {
        public int rec { get; set; }
        public int SPF_ { get; set; }
        public int poso { get; set; }
        public int DPos { get; set; }
        public int EPos { get; set; }

        public override string ToString()
        {
            return
                "Rec#: " + rec + "\t" +
                "   SPF_: " + SPF_ + "\t" +
                "   poso: " + poso + "\t" +
                "   DPos: " + DPos + "\t" +
                "   EPos: " + EPos;
        }

        public static List<SPKG> GetSPKG(int filter = 0, int DBIndex = 0)
        {
            List<SPKG> _SPKG = new List<SPKG>();

            TdbTableProperties TableProps = new TdbTableProperties();
            TableProps.Name = new string((char)0, 5);

            // Get Tableprops based on the selected index
            if (!TDB.TDBTableGetProperties(DBIndex, TableNames.GetTables().Find(item => item.name == "SPKG").rec, ref TableProps))
                return null;

            for (int i = 0; i < TableProps.RecordCount; i++)
            {
                if (filter != 0 && (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("SPKG"), TDB.StrReverse("SPF_"), i) == filter)
                {
                    _SPKG.Add(new SPKG
                    {
                        rec = i,
                        SPF_ = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("SPKG"), TDB.StrReverse("SPF_"), i),
                        poso = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("SPKG"), TDB.StrReverse("poso"), i),
                        DPos = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("SPKG"), TDB.StrReverse("DPos"), i),
                        EPos = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("SPKG"), TDB.StrReverse("EPos"), i)
                    });
                }
                else if (filter == 0)
                {
                    _SPKG.Add(new SPKG
                    {
                        rec = i,
                        SPF_ = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("SPKG"), TDB.StrReverse("SPF_"), i),
                        poso = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("SPKG"), TDB.StrReverse("poso"), i),
                        DPos = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("SPKG"), TDB.StrReverse("DPos"), i),
                        EPos = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("SPKG"), TDB.StrReverse("EPos"), i)
                    });
                }
            }
            return _SPKG;
        }

        public static void SetSPKG(List<SPKG> SPKG, int DBIndex = 0)
        {
            TdbTableProperties TableProps = new TdbTableProperties();
            TableProps.Name = new string((char)0, 5);

            // Get Tableprops based on the selected index
            TDB.TDBTableGetProperties(DBIndex, TableNames.GetTables().Find(item => item.name == "SPKG").rec, ref TableProps);

            if (SPKG.Count > TableProps.RecordCount)
                for (int i = TableProps.RecordCount - 1; i < SPKG.Count - 1; i++)
                {
                    TDB.TDBTableRecordAdd(DBIndex, TDB.StrReverse("SPKG"), true);
                }
            else if (SPKG.Count < TableProps.RecordCount)
            {
                for (int i = TableProps.RecordCount - 1; i > SPKG.Count - 1; i--)
                {
                    TDB.TDBTableRecordRemove(DBIndex, TDB.StrReverse("SPKG"), i);
                }
            }

            foreach (SPKG item in SPKG)
            {
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("SPKG"), TDB.StrReverse("SPF_"), item.rec, item.SPF_);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("SPKG"), TDB.StrReverse("poso"), item.rec, item.poso);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("SPKG"), TDB.StrReverse("DPos"), item.rec, item.DPos);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("SPKG"), TDB.StrReverse("EPos"), item.rec, item.EPos);
            }
        }

        public static List<SPKG> Sort(List<SPKG> SPKG)
        {
            return SPKG.OrderBy(s => s.SPF_).ThenBy(s => s.poso).Cast<SPKG>().ToList();
        }
    }
}
