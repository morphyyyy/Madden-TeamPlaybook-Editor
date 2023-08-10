using System;
using System.Collections.Generic;
using System.Linq;
using TDBAccess;

namespace Madden.TeamPlaybook
{
    [Serializable]
    public class PBCC
    {
        public int rec { get; set; }
        public int CC01 { get; set; }
        public int CC02 { get; set; }
        public int CC03 { get; set; }
        public int CC04 { get; set; }
        public int CC05 { get; set; }
        public int CC06 { get; set; }
        public int CC07 { get; set; }
        public int CC08 { get; set; }
        public int CC09 { get; set; }
        public int PBPL { get; set; }
        public int PTYP { get; set; }

        public override string ToString()
        {
            return
                "Rec#: " + rec +
                "   CC01: " + CC01 +
                "   CC02: " + CC02 +
                "   CC03: " + CC03 +
                "   CC04: " + CC04 +
                "   CC05: " + CC05 +
                "   CC06: " + CC06 +
                "   CC07: " + CC07 +
                "   CC08: " + CC08 +
                "   CC09: " + CC09 +
                "   PBPL: " + PBPL +
                "   PTYP: " + PTYP;
        }

        public static List<PBCC> GetPBCC(int filter = 0, int DBIndex = 0)
        {
            List<PBCC> _PBCC = new List<PBCC>();

            TdbTableProperties TableProps = new TdbTableProperties();
            TableProps.Name = new string((char)0, 5);

            if (TableNames.GetTables().Find(item => item.name == "PBCC") != null)
            {
                // Get Tableprops based on the selected index
                if (!TDB.TDBTableGetProperties(DBIndex, TableNames.GetTables().Find(item => item.name == "PBCC").rec, ref TableProps))
                    return _PBCC;
            }
            else
            {
                return _PBCC;
            }

            for (int i = 0; i < TableProps.RecordCount; i++)
            {
                if (filter != 0 && (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PBCC"), TDB.StrReverse("PBPL"), i) == filter)
                {
                    _PBCC.Add(new PBCC
                    {
                        rec = i,
                        CC01 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PBCC"), TDB.StrReverse("CC01"), i),
                        CC02 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PBCC"), TDB.StrReverse("CC02"), i),
                        CC03 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PBCC"), TDB.StrReverse("CC03"), i),
                        CC04 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PBCC"), TDB.StrReverse("CC04"), i),
                        CC05 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PBCC"), TDB.StrReverse("CC05"), i),
                        CC06 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PBCC"), TDB.StrReverse("CC06"), i),
                        CC07 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PBCC"), TDB.StrReverse("CC07"), i),
                        CC08 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PBCC"), TDB.StrReverse("CC08"), i),
                        CC09 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PBCC"), TDB.StrReverse("CC09"), i),
                        PBPL = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PBCC"), TDB.StrReverse("PBPL"), i),
                        PTYP = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PBCC"), TDB.StrReverse("FTYP"), i)
                    });
                }
                else if (filter == 0)
                {
                    _PBCC.Add(new PBCC
                    {
                        rec = i,
                        CC01 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PBCC"), TDB.StrReverse("CC01"), i),
                        CC02 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PBCC"), TDB.StrReverse("CC02"), i),
                        CC03 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PBCC"), TDB.StrReverse("CC03"), i),
                        CC04 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PBCC"), TDB.StrReverse("CC04"), i),
                        CC05 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PBCC"), TDB.StrReverse("CC05"), i),
                        CC06 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PBCC"), TDB.StrReverse("CC06"), i),
                        CC07 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PBCC"), TDB.StrReverse("CC07"), i),
                        CC08 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PBCC"), TDB.StrReverse("CC08"), i),
                        CC09 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PBCC"), TDB.StrReverse("CC09"), i),
                        PBPL = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PBCC"), TDB.StrReverse("PBPL"), i),
                        PTYP = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PBCC"), TDB.StrReverse("PTYP"), i)
                    });
                }
            }
            return _PBCC;
        }

        public static void SetPBCC(List<PBCC> PBCC, int DBIndex = 0)
        {
            TdbTableProperties TableProps = new TdbTableProperties();
            TableProps.Name = new string((char)0, 5);

            // Get Tableprops based on the selected index
            if (TableNames.GetTables().Find(item => item.name == "PBCC") != null)
            {
                TDB.TDBTableGetProperties(DBIndex, TableNames.GetTables().Find(item => item.name == "PBCC").rec, ref TableProps);
            }
            else
            {
                return;
            }

            if (PBCC.Count > TableProps.RecordCount)
                for (int i = TableProps.RecordCount - 1; i < PBCC.Count - 1; i++)
                {
                    TDB.TDBTableRecordAdd(DBIndex, TDB.StrReverse("PBCC"), true);
                }
            else if (PBCC.Count < TableProps.RecordCount)
            {
                for (int i = TableProps.RecordCount - 1; i > PBCC.Count - 1; i--)
                {
                    TDB.TDBTableRecordRemove(DBIndex, TDB.StrReverse("PBCC"), i);
                }
            }

            foreach (PBCC item in PBCC)
            {
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PBCC"), TDB.StrReverse("CC01"), item.rec, item.CC01);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PBCC"), TDB.StrReverse("CC02"), item.rec, item.CC02);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PBCC"), TDB.StrReverse("CC03"), item.rec, item.CC03);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PBCC"), TDB.StrReverse("CC04"), item.rec, item.CC04);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PBCC"), TDB.StrReverse("CC05"), item.rec, item.CC05);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PBCC"), TDB.StrReverse("CC06"), item.rec, item.CC06);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PBCC"), TDB.StrReverse("CC07"), item.rec, item.CC07);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PBCC"), TDB.StrReverse("CC08"), item.rec, item.CC08);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PBCC"), TDB.StrReverse("CC09"), item.rec, item.CC09);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PBCC"), TDB.StrReverse("PBPL"), item.rec, item.PBPL);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PBCC"), TDB.StrReverse("PTYP"), item.rec, item.PTYP);
            }
        }

        public static List<PBCC> Sort(List<PBCC> PBCC)
        {
            return PBCC.OrderBy(s => s.PBPL).ThenBy(s => s.PTYP).Cast<PBCC>().ToList();
        }
    }
}
