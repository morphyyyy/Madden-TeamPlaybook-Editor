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
        public int PLYL { get; set; }

        public List<Progression> progressions { get; set; }

        public override string ToString()
        {
            string PLPDtoString = "";
            PLPDtoString += "Rec#: " + rec + "   PLYL: " + PLYL + "\n";
            for (int x = 0; x < progressions.Count(); x++)
            {
                PLPDtoString +=
                    "   com" + (x + 1) + ": " + progressions[x].com +
                    "   con" + (x + 1) + ": " + progressions[x].con +
                    "   per" + (x + 1) + ": " + progressions[x].per +
                    "   rcv" + (x + 1) + ": " + progressions[x].rcv +
                    "   icx" + (x + 1) + ": " + progressions[x].icx +
                    "   icy" + (x + 1) + ": " + progressions[x].icy + "\n";
            }
            return PLPDtoString;
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
                        PLYL = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLPD"), TDB.StrReverse("PLYL"), i)
                    });
                    _PLPD[i].progressions = new List<Progression>();
                    for (int x = 1; x <= 5; x++)
                    {
                        _PLPD[i].progressions.Add(new Progression
                        {
                            com = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLPD"), TDB.StrReverse("com" + x.ToString()), i),
                            con = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLPD"), TDB.StrReverse("con" + x.ToString()), i),
                            per = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLPD"), TDB.StrReverse("per" + x.ToString()), i),
                            rcv = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLPD"), TDB.StrReverse("rcv" + x.ToString()), i),
                            icx = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLPD"), TDB.StrReverse("icx" + x.ToString()), i),
                            icy = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLPD"), TDB.StrReverse("icy" + x.ToString()), i)
                        });
                    }
                }
                else if (filter == 0)
                {
                    _PLPD.Add(new PLPD
                    {
                        rec = i,
                        PLYL = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLPD"), TDB.StrReverse("PLYL"), i)
                    });
                    _PLPD[i].progressions = new List<Progression>();
                    for (int x = 1; x <= 5; x++)
                    {
                        _PLPD[i].progressions.Add(new Progression
                        {
                            com = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLPD"), TDB.StrReverse("com" + x.ToString()), i),
                            con = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLPD"), TDB.StrReverse("con" + x.ToString()), i),
                            per = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLPD"), TDB.StrReverse("per" + x.ToString()), i),
                            rcv = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLPD"), TDB.StrReverse("rcv" + x.ToString()), i),
                            icx = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLPD"), TDB.StrReverse("icx" + x.ToString()), i),
                            icy = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLPD"), TDB.StrReverse("icy" + x.ToString()), i)
                        });
                    }
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
                for (int x = 1; x <= 5; x++)
                {
                    TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PLPD"), TDB.StrReverse("com" + x.ToString()), item.rec, item.progressions[x - 1].com);
                    TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PLPD"), TDB.StrReverse("con" + x.ToString()), item.rec, item.progressions[x - 1].con);
                    TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PLPD"), TDB.StrReverse("per" + x.ToString()), item.rec, item.progressions[x - 1].per);
                    TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PLPD"), TDB.StrReverse("rcv" + x.ToString()), item.rec, item.progressions[x - 1].rcv);
                    TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PLPD"), TDB.StrReverse("icx" + x.ToString()), item.rec, item.progressions[x - 1].icx);
                    TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PLPD"), TDB.StrReverse("icy" + x.ToString()), item.rec, item.progressions[x - 1].icy);
                }
            }
        }

        public static List<PLPD> Sort(List<PLPD> PLPD)
        {
            return PLPD.OrderBy(s => s.PLYL).Cast<PLPD>().ToList();
        }
    }

    [Serializable]
    public class Progression
    {
        public override string ToString()
        {
            return
                "com: " + "[" + string.Join(", ", com) + "]\t" +
                "con: " + "[" + string.Join(", ", con) + "]\t" +
                "per: " + "[" + string.Join(", ", per) + "]\t" +
                "rcv: " + "[" + string.Join(", ", rcv) + "]\t" +
                "icx: " + "[" + string.Join(", ", icx) + "]\t" +
                "icy: " + "[" + string.Join(", ", icy) + "]\t";
        }

        public int com;
        public int con;
        public int per;
        public int rcv;
        public int icx;
        public int icy;
    }
}
