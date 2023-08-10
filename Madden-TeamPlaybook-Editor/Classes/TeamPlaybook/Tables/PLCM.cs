using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TDBAccess;

namespace Madden.TeamPlaybook
{
    [Serializable]
    public class PLCM
    {
        public int rec { get; set; }
        public int per1 { get; set; }
        public int dir1 { get; set; }
        public int ply1 { get; set; }
        public int per2 { get; set; }
        public int dir2 { get; set; }
        public int ply2 { get; set; }
        public int per3 { get; set; }
        public int dir3 { get; set; }
        public int ply3 { get; set; }
        public int per4 { get; set; }
        public int dir4 { get; set; }
        public int ply4 { get; set; }
        public int per5 { get; set; }
        public int dir5 { get; set; }
        public int ply5 { get; set; }
        public int PLYL { get; set; }

        public override string ToString()
        {
            return
                "Rec#: " + rec +
                "   per1: " + per1 +
                "   dir1: " + dir1 +
                "   ply1: " + ply1 +
                "   per2: " + per2 +
                "   dir2: " + dir2 +
                "   ply2: " + ply2 +
                "   per3: " + per3 +
                "   dir3: " + dir3 +
                "   ply3: " + ply3 +
                "   per4: " + per4 +
                "   dir4: " + dir4 +
                "   ply4: " + ply4 +
                "   per5: " + per5 +
                "   dir5: " + dir5 +
                "   ply5: " + ply5 +
                "   PLYL: " + PLYL;
        }

        public static List<PLCM> GetPLCM(int filter = 0, int DBIndex = 0)
        {
            List<PLCM> _PLCM = new List<PLCM>();

            TdbTableProperties TableProps = new TdbTableProperties();
            TableProps.Name = new string((char)0, 5);

            // Get Tableprops based on the selected index
            if (!TDB.TDBTableGetProperties(DBIndex, TableNames.GetTables().Find(item => item.name == "PLCM").rec, ref TableProps))
                return null;

            for (int i = 0; i < TableProps.RecordCount; i++)
            {
                if (filter != 0 && (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLCM"), TDB.StrReverse("PLYL"), i) == filter)
                {
                    _PLCM.Add(new PLCM
                    {
                        rec = i,
                        per1 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLCM"), TDB.StrReverse("per1"), i),
                        dir1 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLCM"), TDB.StrReverse("dir1"), i),
                        ply1 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLCM"), TDB.StrReverse("ply1"), i),
                        per2 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLCM"), TDB.StrReverse("per2"), i),
                        dir2 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLCM"), TDB.StrReverse("dir2"), i),
                        ply2 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLCM"), TDB.StrReverse("ply2"), i),
                        per3 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLCM"), TDB.StrReverse("per3"), i),
                        dir3 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLCM"), TDB.StrReverse("dir3"), i),
                        ply3 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLCM"), TDB.StrReverse("ply3"), i),
                        per4 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLCM"), TDB.StrReverse("per4"), i),
                        dir4 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLCM"), TDB.StrReverse("dir4"), i),
                        ply4 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLCM"), TDB.StrReverse("ply4"), i),
                        per5 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLCM"), TDB.StrReverse("per5"), i),
                        dir5 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLCM"), TDB.StrReverse("dir5"), i),
                        ply5 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLCM"), TDB.StrReverse("ply5"), i),
                        PLYL = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLCM"), TDB.StrReverse("PLYL"), i)
                    });
                }
                else if (filter == 0)
                {
                    _PLCM.Add(new PLCM
                    {
                        rec = i,
                        per1 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLCM"), TDB.StrReverse("per1"), i),
                        dir1 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLCM"), TDB.StrReverse("dir1"), i),
                        ply1 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLCM"), TDB.StrReverse("ply1"), i),
                        per2 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLCM"), TDB.StrReverse("per2"), i),
                        dir2 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLCM"), TDB.StrReverse("dir2"), i),
                        ply2 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLCM"), TDB.StrReverse("ply2"), i),
                        per3 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLCM"), TDB.StrReverse("per3"), i),
                        dir3 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLCM"), TDB.StrReverse("dir3"), i),
                        ply3 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLCM"), TDB.StrReverse("ply3"), i),
                        per4 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLCM"), TDB.StrReverse("per4"), i),
                        dir4 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLCM"), TDB.StrReverse("dir4"), i),
                        ply4 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLCM"), TDB.StrReverse("ply4"), i),
                        per5 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLCM"), TDB.StrReverse("per5"), i),
                        dir5 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLCM"), TDB.StrReverse("dir5"), i),
                        ply5 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLCM"), TDB.StrReverse("ply5"), i),
                        PLYL = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLCM"), TDB.StrReverse("PLYL"), i)
                    });
                }
            }
            return _PLCM;
        }

        public static void SetPLCM(List<PLCM> PLCM, int DBIndex = 0)
        {
            TdbTableProperties TableProps = new TdbTableProperties();
            TableProps.Name = new string((char)0, 5);

            // Get Tableprops based on the selected index
            TDB.TDBTableGetProperties(DBIndex, TableNames.GetTables().Find(item => item.name == "PLCM").rec, ref TableProps);

            if (PLCM.Count > TableProps.RecordCount)
                for (int i = TableProps.RecordCount - 1; i < PLCM.Count - 1; i++)
                {
                    TDB.TDBTableRecordAdd(DBIndex, TDB.StrReverse("PLCM"), true);
                }
            else if (PLCM.Count < TableProps.RecordCount)
            {
                for (int i = TableProps.RecordCount - 1; i > PLCM.Count - 1; i--)
                {
                    TDB.TDBTableRecordRemove(DBIndex, TDB.StrReverse("PLCM"), i);
                }
            }

            foreach (PLCM item in PLCM)
            {
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PLCM"), TDB.StrReverse("per1"), item.rec, item.per1);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PLCM"), TDB.StrReverse("dir1"), item.rec, item.dir1);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PLCM"), TDB.StrReverse("ply1"), item.rec, item.ply1);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PLCM"), TDB.StrReverse("per2"), item.rec, item.per2);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PLCM"), TDB.StrReverse("dir2"), item.rec, item.dir2);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PLCM"), TDB.StrReverse("ply2"), item.rec, item.ply2);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PLCM"), TDB.StrReverse("per3"), item.rec, item.per3);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PLCM"), TDB.StrReverse("dir3"), item.rec, item.dir3);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PLCM"), TDB.StrReverse("ply3"), item.rec, item.ply3);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PLCM"), TDB.StrReverse("per4"), item.rec, item.per4);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PLCM"), TDB.StrReverse("dir4"), item.rec, item.dir4);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PLCM"), TDB.StrReverse("ply4"), item.rec, item.ply4);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PLCM"), TDB.StrReverse("per5"), item.rec, item.per5);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PLCM"), TDB.StrReverse("dir5"), item.rec, item.dir5);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PLCM"), TDB.StrReverse("ply5"), item.rec, item.ply5);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PLCM"), TDB.StrReverse("PLYL"), item.rec, item.PLYL);
            }
        }

        public static List<PLCM> Sort(List<PLCM> PLCM)
        {
            return PLCM.OrderBy(s => s.PLYL).Cast<PLCM>().ToList();
        }
    }
}
