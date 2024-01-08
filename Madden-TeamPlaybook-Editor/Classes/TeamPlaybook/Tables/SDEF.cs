using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TDBAccess;

namespace Madden.TeamPlaybook
{
    [Serializable]
    public class SDEF
    {
        public int rec { get; set; }
        public int ATCA { get; set; }
        /// <summary>
        /// PLYL.plyl
        /// </summary>
        public int PLYL { get; set; }
        public int DFLP { get; set; }
        public int STRP { get; set; }
        public int STRR { get; set; }

        public override string ToString()
        {
            return
                "Rec#: " + rec +
                "   ATCA: " + ATCA +
                "   PLYL: " + PLYL +
                "   DFLP: " + DFLP +
                "   STRP: " + STRP +
                "   STRR: " + STRR;
        }

        public static List<SDEF> GetSDEF(int filter = 0, int DBIndex = 0)
        {
            List<SDEF> _SDEF = new List<SDEF>();

            TdbTableProperties TableProps = new TdbTableProperties();
            TableProps.Name = new string((char)0, 5);

            if (TableNames.GetTables().Find(item => item.name == "SDEF") != null)
            {
                // Get Tableprops based on the selected index
                if (!TDB.TDBTableGetProperties(DBIndex, TableNames.GetTables().Find(item => item.name == "SDEF").rec, ref TableProps))
                    return _SDEF;
            }
            else
            {
                return _SDEF;
            }

            for (int i = 0; i < TableProps.RecordCount; i++)
            {
                if (filter != 0 && (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("SDEF"), TDB.StrReverse("PLYL"), i) == filter)
                {
                    _SDEF.Add(new SDEF
                    {
                        rec = i,
                        ATCA = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("SDEF"), TDB.StrReverse("ATCA"), i),
                        PLYL = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("SDEF"), TDB.StrReverse("PLYL"), i),
                        DFLP = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("SDEF"), TDB.StrReverse("DFLP"), i),
                        STRP = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("SDEF"), TDB.StrReverse("STRP"), i),
                        STRR = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("SDEF"), TDB.StrReverse("STRR"), i)
                    });
                }
                else if (filter == 0)
                {
                    _SDEF.Add(new SDEF
                    {
                        rec = i,
                        ATCA = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("SDEF"), TDB.StrReverse("ATCA"), i),
                        PLYL = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("SDEF"), TDB.StrReverse("PLYL"), i),
                        DFLP = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("SDEF"), TDB.StrReverse("DFLP"), i),
                        STRP = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("SDEF"), TDB.StrReverse("STRP"), i),
                        STRR = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("SDEF"), TDB.StrReverse("STRR"), i)
                    });
                }
            }
            return _SDEF;
        }

        public static void SetSDEF(List<SDEF> SDEF, int DBIndex = 0)
        {
            TdbTableProperties TableProps = new TdbTableProperties();
            TableProps.Name = new string((char)0, 5);

            // Get Tableprops based on the selected index
            if (TableNames.GetTables().Find(item => item.name == "SDEF") != null)
            {
                TDB.TDBTableGetProperties(DBIndex, TableNames.GetTables().Find(item => item.name == "SDEF").rec, ref TableProps);
            }
            else
            {
                return;
            }

            if (SDEF.Count > TableProps.RecordCount)
                for (int i = TableProps.RecordCount - 1; i < SDEF.Count - 1; i++)
                {
                    TDB.TDBTableRecordAdd(DBIndex, TDB.StrReverse("SDEF"), true);
                }
            else if (SDEF.Count < TableProps.RecordCount)
            {
                for (int i = TableProps.RecordCount - 1; i > SDEF.Count - 1; i--)
                {
                    TDB.TDBTableRecordRemove(DBIndex, TDB.StrReverse("SDEF"), i);
                }
            }

            foreach (SDEF item in SDEF)
            {
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("SDEF"), TDB.StrReverse("ATCA"), item.rec, item.ATCA);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("SDEF"), TDB.StrReverse("PLYL"), item.rec, item.PLYL);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("SDEF"), TDB.StrReverse("DFLP"), item.rec, item.DFLP);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("SDEF"), TDB.StrReverse("STRP"), item.rec, item.STRP);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("SDEF"), TDB.StrReverse("STRR"), item.rec, item.STRR);
            }
        }

        public static List<SDEF> Sort(List<SDEF> SDEF)
        {
            return SDEF.OrderBy(s => s.PLYL).Cast<SDEF>().ToList();
        }
    }
}
