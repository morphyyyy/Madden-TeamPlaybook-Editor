using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TDBAccess;

namespace Madden.TeamPlaybook
{
    [Serializable]
    public class PLPD
    {
        public int rec { get; set; }
        public int com1 { get; set; }
        public int con1 { get; set; }
        public int per1 { get; set; }
        public int rcv1 { get; set; }
        public int icx1 { get; set; }
        public int icy1 { get; set; }
        public int com2 { get; set; }
        public int con2 { get; set; }
        public int per2 { get; set; }
        public int rcv2 { get; set; }
        public int icx2 { get; set; }
        public int icy2 { get; set; }
        public int com3 { get; set; }
        public int con3 { get; set; }
        public int per3 { get; set; }
        public int rcv3 { get; set; }
        public int icx3 { get; set; }
        public int icy3 { get; set; }
        public int com4 { get; set; }
        public int con4 { get; set; }
        public int per4 { get; set; }
        public int rcv4 { get; set; }
        public int icx4 { get; set; }
        public int icy4 { get; set; }
        public int com5 { get; set; }
        public int con5 { get; set; }
        public int per5 { get; set; }
        public int rcv5 { get; set; }
        public int icx5 { get; set; }
        public int icy5 { get; set; }
        public int PLYL { get; set; }

        public override string ToString()
        {
            return
                "Rec#: " + rec +
                "   com1: " + com1 +
                "   con1: " + con1 +
                "   per1: " + per1 +
                "   rcv1: " + rcv1 +
                "   icx1: " + rcv1 +
                "   icy1: " + rcv1 +
                "   com2: " + com2 +
                "   con2: " + con2 +
                "   per2: " + per2 +
                "   rcv2: " + rcv2 +
                "   icx2: " + rcv1 +
                "   icy2: " + rcv1 +
                "   com3: " + com3 +
                "   con3: " + con3 +
                "   per3: " + per3 +
                "   rcv3: " + rcv3 +
                "   icx3: " + rcv1 +
                "   icy3: " + rcv1 +
                "   com4: " + com4 +
                "   con4: " + con4 +
                "   per4: " + per4 +
                "   rcv4: " + rcv4 +
                "   icx4: " + rcv1 +
                "   icy4: " + rcv1 +
                "   com5: " + com5 +
                "   con5: " + con5 +
                "   per5: " + per5 +
                "   rcv5: " + rcv5 +
                "   icx5: " + rcv1 +
                "   icy5: " + rcv1 +
                "   PLYL: " + PLYL;
        }

        public static List<PLPD> GetPLPD(int filter = 0, int DBIndex = 0)
        {
            List<PLPD> _PLPD = new List<PLPD>();

            TdbTableProperties TableProps = new TdbTableProperties();
            TableProps.Name = new string((char)0, 5);

            // Get Tableprops based on the selected index
            if (!TDB.TDBTableGetProperties(DBIndex, TableNames.GetTables().Find(item => item.name == "PLPD").rec, ref TableProps))
                return null;

            for (int i = 0; i < TableProps.RecordCount; i++)
            {
                if (filter != 0 && (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLPD"), TDB.StrReverse("PLYL"), i) == filter)
                {
                    _PLPD.Add(new PLPD
                    {
                        rec = i,
                        com1 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLPD"), TDB.StrReverse("com1"), i),
                        con1 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLPD"), TDB.StrReverse("con1"), i),
                        per1 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLPD"), TDB.StrReverse("per1"), i),
                        rcv1 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLPD"), TDB.StrReverse("rcv1"), i),
                        icx1 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLPD"), TDB.StrReverse("icx1"), i),
                        icy1 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLPD"), TDB.StrReverse("icy1"), i),
                        com2 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLPD"), TDB.StrReverse("com2"), i),
                        con2 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLPD"), TDB.StrReverse("con2"), i),
                        per2 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLPD"), TDB.StrReverse("per2"), i),
                        rcv2 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLPD"), TDB.StrReverse("rcv2"), i),
                        icx2 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLPD"), TDB.StrReverse("icx2"), i),
                        icy2 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLPD"), TDB.StrReverse("icy2"), i),
                        com3 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLPD"), TDB.StrReverse("com3"), i),
                        con3 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLPD"), TDB.StrReverse("con3"), i),
                        per3 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLPD"), TDB.StrReverse("per3"), i),
                        rcv3 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLPD"), TDB.StrReverse("rcv3"), i),
                        icx3 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLPD"), TDB.StrReverse("icx3"), i),
                        icy3 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLPD"), TDB.StrReverse("icy3"), i),
                        com4 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLPD"), TDB.StrReverse("com4"), i),
                        con4 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLPD"), TDB.StrReverse("con4"), i),
                        per4 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLPD"), TDB.StrReverse("per4"), i),
                        rcv4 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLPD"), TDB.StrReverse("rcv4"), i),
                        icx4 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLPD"), TDB.StrReverse("icx4"), i),
                        icy4 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLPD"), TDB.StrReverse("icy4"), i),
                        com5 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLPD"), TDB.StrReverse("com5"), i),
                        con5 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLPD"), TDB.StrReverse("con5"), i),
                        per5 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLPD"), TDB.StrReverse("per5"), i),
                        rcv5 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLPD"), TDB.StrReverse("rcv5"), i),
                        icx5 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLPD"), TDB.StrReverse("icx5"), i),
                        icy5 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLPD"), TDB.StrReverse("icy5"), i),
                        PLYL = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLPD"), TDB.StrReverse("PLYL"), i)
                    });
                }
                else if (filter == 0)
                {
                    _PLPD.Add(new PLPD
                    {
                        rec = i,
                        com1 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLPD"), TDB.StrReverse("com1"), i),
                        con1 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLPD"), TDB.StrReverse("con1"), i),
                        per1 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLPD"), TDB.StrReverse("per1"), i),
                        rcv1 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLPD"), TDB.StrReverse("rcv1"), i),
                        icx1 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLPD"), TDB.StrReverse("icx1"), i),
                        icy1 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLPD"), TDB.StrReverse("icy1"), i),
                        com2 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLPD"), TDB.StrReverse("com2"), i),
                        con2 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLPD"), TDB.StrReverse("con2"), i),
                        per2 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLPD"), TDB.StrReverse("per2"), i),
                        rcv2 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLPD"), TDB.StrReverse("rcv2"), i),
                        icx2 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLPD"), TDB.StrReverse("icx2"), i),
                        icy2 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLPD"), TDB.StrReverse("icy2"), i),
                        com3 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLPD"), TDB.StrReverse("com3"), i),
                        con3 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLPD"), TDB.StrReverse("con3"), i),
                        per3 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLPD"), TDB.StrReverse("per3"), i),
                        rcv3 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLPD"), TDB.StrReverse("rcv3"), i),
                        icx3 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLPD"), TDB.StrReverse("icx3"), i),
                        icy3 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLPD"), TDB.StrReverse("icy3"), i),
                        com4 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLPD"), TDB.StrReverse("com4"), i),
                        con4 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLPD"), TDB.StrReverse("con4"), i),
                        per4 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLPD"), TDB.StrReverse("per4"), i),
                        rcv4 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLPD"), TDB.StrReverse("rcv4"), i),
                        icx4 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLPD"), TDB.StrReverse("icx4"), i),
                        icy4 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLPD"), TDB.StrReverse("icy4"), i),
                        com5 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLPD"), TDB.StrReverse("com5"), i),
                        con5 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLPD"), TDB.StrReverse("con5"), i),
                        per5 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLPD"), TDB.StrReverse("per5"), i),
                        rcv5 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLPD"), TDB.StrReverse("rcv5"), i),
                        icx5 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLPD"), TDB.StrReverse("icx5"), i),
                        icy5 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLPD"), TDB.StrReverse("icy5"), i),
                        PLYL = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLPD"), TDB.StrReverse("PLYL"), i)
                    });
                }
            }
            return _PLPD;
        }

        public static void SetPLPD(List<PLPD> PLPD, int DBIndex = 0)
        {
            TdbTableProperties TableProps = new TdbTableProperties();
            TableProps.Name = new string((char)0, 5);

            // Get Tableprops based on the selected index
            TDB.TDBTableGetProperties(DBIndex, TableNames.GetTables().Find(item => item.name == "PLPD").rec, ref TableProps);

            if (PLPD.Count > TableProps.RecordCount)
            {
                for (int i = TableProps.RecordCount - 1; i < PLPD.Count - 1; i++)
                {
                    TDB.TDBTableRecordAdd(DBIndex, TDB.StrReverse("PLPD"), true);
                }
            }
            else if (PLPD.Count < TableProps.RecordCount)
            {
                for (int i = TableProps.RecordCount - 1; i > PLPD.Count - 1; i--)
                {
                    TDB.TDBTableRecordRemove(DBIndex, TDB.StrReverse("PLPD"), i);
                }
            }

            foreach (PLPD item in PLPD)
            {
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PLPD"), TDB.StrReverse("com1"), item.rec, item.com1);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PLPD"), TDB.StrReverse("con1"), item.rec, item.con1);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PLPD"), TDB.StrReverse("per1"), item.rec, item.per1);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PLPD"), TDB.StrReverse("rcv1"), item.rec, item.rcv1);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PLPD"), TDB.StrReverse("icx1"), item.rec, item.icx1);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PLPD"), TDB.StrReverse("icy1"), item.rec, item.icy1);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PLPD"), TDB.StrReverse("com2"), item.rec, item.com2);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PLPD"), TDB.StrReverse("con2"), item.rec, item.con2);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PLPD"), TDB.StrReverse("per2"), item.rec, item.per2);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PLPD"), TDB.StrReverse("rcv2"), item.rec, item.rcv2);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PLPD"), TDB.StrReverse("icx2"), item.rec, item.icx2);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PLPD"), TDB.StrReverse("icy2"), item.rec, item.icy2);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PLPD"), TDB.StrReverse("com3"), item.rec, item.com3);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PLPD"), TDB.StrReverse("con3"), item.rec, item.con3);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PLPD"), TDB.StrReverse("per3"), item.rec, item.per3);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PLPD"), TDB.StrReverse("rcv3"), item.rec, item.rcv3);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PLPD"), TDB.StrReverse("icx3"), item.rec, item.icx3);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PLPD"), TDB.StrReverse("icy3"), item.rec, item.icy3);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PLPD"), TDB.StrReverse("com4"), item.rec, item.com4);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PLPD"), TDB.StrReverse("con4"), item.rec, item.con4);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PLPD"), TDB.StrReverse("per4"), item.rec, item.per4);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PLPD"), TDB.StrReverse("rcv4"), item.rec, item.rcv4);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PLPD"), TDB.StrReverse("icx4"), item.rec, item.icx4);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PLPD"), TDB.StrReverse("icy4"), item.rec, item.icy4);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PLPD"), TDB.StrReverse("com5"), item.rec, item.com5);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PLPD"), TDB.StrReverse("con5"), item.rec, item.con5);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PLPD"), TDB.StrReverse("per5"), item.rec, item.per5);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PLPD"), TDB.StrReverse("rcv5"), item.rec, item.rcv5);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PLPD"), TDB.StrReverse("icx5"), item.rec, item.icx5);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PLPD"), TDB.StrReverse("icy5"), item.rec, item.icy5);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PLPD"), TDB.StrReverse("PLYL"), item.rec, item.PLYL);
            }
        }

        public static List<PLPD> Sort(List<PLPD> PLPD)
        {
            return PLPD.OrderBy(s => s.PLYL).Cast<PLPD>().ToList();
        }
    }
}
