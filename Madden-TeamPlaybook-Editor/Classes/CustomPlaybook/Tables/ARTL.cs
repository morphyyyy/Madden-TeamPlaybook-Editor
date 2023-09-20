using System;
using System.Collections.Generic;
using System.Linq;
using TDBAccess;

namespace Madden.CustomPlaybook
{
    [Serializable]
    public class ARTL
    {
        public override string ToString()
        {
            string ARTL_string = "rec: " + rec +
                "   artl: " + artl +
                "   acnt: " + acnt + "\n";
            for (int i = 0; i < 12; i++) ARTL_string += "\t" + i.ToString() + "\t" + ARTList[i] + "\n";
            return ARTL_string;
        }

        public int rec { get; set; }
        public int artl { get; set; }
        public int acnt { get; set; }

        public List<PlayArt> ARTList { get; set; }

        public static List<ARTL> GetARTL(string Type, int filter = 0, int DBIndex = 0)
        {
            List<ARTL> _ARTL = new List<ARTL>();

            TdbTableProperties TableProps = new TdbTableProperties();
            TableProps.Name = new string((char)0, 5);

            // Get Tableprops based on the selected index
            if (!TDB.TDBTableGetProperties(DBIndex, TableNames.GetTables().Find(item => item.name == Type).rec, ref TableProps))
                return null;

            for (int i = 0; i < TableProps.RecordCount; i++)
            {
                if (filter != 0 && (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLPD"), TDB.StrReverse("PLYL"), i) == filter)
                {
                    _ARTL.Add(new ARTL
                    {
                        rec = i,
                        artl = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse(Type), TDB.StrReverse("ARTL"), i),
                        acnt = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse(Type), TDB.StrReverse("acnt"), i)
                    });
                    _ARTL[i].ARTList = new List<PlayArt>();
                    for (int x=1; x <= 12; x++)
                    {
                        string ARTLstep;
                        if (x < 10) ARTLstep = "0" + x.ToString();
                        else ARTLstep = x.ToString();
                        _ARTL[i].ARTList.Add(new PlayArt
                        {
                            sp = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse(Type), TDB.StrReverse("sp" + ARTLstep), i),
                            ls = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse(Type), TDB.StrReverse("ls" + ARTLstep), i),
                            at = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse(Type), TDB.StrReverse("at" + ARTLstep), i),
                            ct = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse(Type), TDB.StrReverse("ct" + ARTLstep), i),
                            lt = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse(Type), TDB.StrReverse("lt" + ARTLstep), i),
                            au = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse(Type), TDB.StrReverse("au" + ARTLstep), i),
                            av = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse(Type), TDB.StrReverse("av" + ARTLstep), i),
                            ax = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse(Type), TDB.StrReverse("ax" + ARTLstep), i),
                            ay = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse(Type), TDB.StrReverse("ay" + ARTLstep), i),
                        });
                    }
                }
                else if (filter == 0)
                {
                    _ARTL.Add(new ARTL
                    {
                        rec = i,
                        artl = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse(Type), TDB.StrReverse("ARTL"), i),
                        acnt = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse(Type), TDB.StrReverse("acnt"), i)
                    });
                    _ARTL[i].ARTList = new List<PlayArt>();
                    for (int x = 1; x <= 12; x++)
                    {
                        string ARTLstep;
                        if (x < 10) ARTLstep = "0" + x.ToString();
                        else ARTLstep = x.ToString();
                        _ARTL[i].ARTList.Add(new PlayArt
                        {
                            sp = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse(Type), TDB.StrReverse("sp" + ARTLstep), i),
                            ls = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse(Type), TDB.StrReverse("ls" + ARTLstep), i),
                            at = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse(Type), TDB.StrReverse("at" + ARTLstep), i),
                            ct = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse(Type), TDB.StrReverse("ct" + ARTLstep), i),
                            lt = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse(Type), TDB.StrReverse("lt" + ARTLstep), i),
                            au = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse(Type), TDB.StrReverse("au" + ARTLstep), i),
                            av = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse(Type), TDB.StrReverse("av" + ARTLstep), i),
                            ax = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse(Type), TDB.StrReverse("ax" + ARTLstep), i),
                            ay = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse(Type), TDB.StrReverse("ay" + ARTLstep), i),
                        });
                    }
                }
            }
            return _ARTL;
        }

        public static void SetARTL(string Type, List<ARTL> ARTL, int DBIndex = 0)
        {
            TdbTableProperties TableProps = new TdbTableProperties();
            TableProps.Name = new string((char)0, 5);

            // Get Tableprops based on the selected index
            TDB.TDBTableGetProperties(DBIndex, TableNames.GetTables().Find(item => item.name == Type).rec, ref TableProps);

            if (ARTL.Count > TableProps.RecordCount)
            {
                for (int i = TableProps.RecordCount - 1; i < ARTL.Count - 1; i++)
                {
                    TDB.TDBTableRecordAdd(DBIndex, TDB.StrReverse(Type), true);
                }
            }
            else if (ARTL.Count < TableProps.RecordCount)
            {
                for (int i = TableProps.RecordCount - 1; i > ARTL.Count - 1; i--)
                {
                    TDB.TDBTableRecordRemove(DBIndex, TDB.StrReverse(Type), i);
                }
            }

            foreach (ARTL item in ARTL)
            {
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse(Type), TDB.StrReverse("ARTL"), item.rec, item.artl);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse(Type), TDB.StrReverse("acnt"), item.rec, item.acnt);
                for (int x = 1; x <= 12; x++)
                {
                    string ARTLstep;
                    if (x < 10)
                    {
                        ARTLstep = "0" + x.ToString();
                    }
                    else
                    {
                        ARTLstep = x.ToString();
                    }
                    TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse(Type), TDB.StrReverse("sp" + ARTLstep), item.rec, item.ARTList[x - 1].sp);
                    TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse(Type), TDB.StrReverse("ls" + ARTLstep), item.rec, item.ARTList[x - 1].ls);
                    TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse(Type), TDB.StrReverse("at" + ARTLstep), item.rec, item.ARTList[x - 1].at);
                    TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse(Type), TDB.StrReverse("ct" + ARTLstep), item.rec, item.ARTList[x - 1].ct);
                    TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse(Type), TDB.StrReverse("lt" + ARTLstep), item.rec, item.ARTList[x - 1].lt);
                    TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse(Type), TDB.StrReverse("au" + ARTLstep), item.rec, item.ARTList[x - 1].au);
                    TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse(Type), TDB.StrReverse("av" + ARTLstep), item.rec, item.ARTList[x - 1].av);
                    TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse(Type), TDB.StrReverse("ax" + ARTLstep), item.rec, item.ARTList[x - 1].ax);
                    TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse(Type), TDB.StrReverse("ay" + ARTLstep), item.rec, item.ARTList[x - 1].ay);
                }
            }
        }

        public static List<ARTL> Sort(List<ARTL> ARTL)
        {
            return ARTL.OrderBy(s => s.artl).Cast<ARTL>().ToList();
        }

        public bool IsIdentical(ARTL _artl)
        {
            int same =
                ((rec == _artl.rec) ? 1 : 0) *
                ((artl == _artl.artl) ? 1 : 0) *
                ((acnt == _artl.acnt) ? 1 : 0);
            for (int i = 0; i < 12; i++)
            {
                same = same *
                ((_artl.ARTList[i].sp == ARTList[i].sp) ? 1 : 0) *
                ((_artl.ARTList[i].ls == ARTList[i].ls) ? 1 : 0) *
                ((_artl.ARTList[i].at == ARTList[i].at) ? 1 : 0) *
                ((_artl.ARTList[i].ct == ARTList[i].ct) ? 1 : 0) *
                ((_artl.ARTList[i].lt == ARTList[i].lt) ? 1 : 0) *
                ((_artl.ARTList[i].au == ARTList[i].au) ? 1 : 0) *
                ((_artl.ARTList[i].av == ARTList[i].av) ? 1 : 0) *
                ((_artl.ARTList[i].ax == ARTList[i].ax) ? 1 : 0) *
                ((_artl.ARTList[i].ay == ARTList[i].ay) ? 1 : 0);
            }
            return Convert.ToBoolean(same);
        }

        public bool IsIdentical(MaddenCustomPlaybookEditor.ARTL _artl)
        {
            int same =
                ((rec == _artl.rec) ? 1 : 0) *
                ((artl == _artl.artl) ? 1 : 0) *
                ((acnt == _artl.acnt) ? 1 : 0);
            for (int i = 0; i < 12; i++)
            {
                same = same *
                ((_artl.sp[i] == ARTList[i].sp) ? 1 : 0) *
                ((_artl.ls[i] == ARTList[i].ls) ? 1 : 0) *
                ((_artl.at[i] == ARTList[i].at) ? 1 : 0) *
                ((_artl.ct[i] == ARTList[i].ct) ? 1 : 0) *
                ((_artl.lt[i] == ARTList[i].lt) ? 1 : 0) *
                ((_artl.au[i] == ARTList[i].au) ? 1 : 0) *
                ((_artl.av[i] == ARTList[i].av) ? 1 : 0) *
                ((_artl.ax[i] == ARTList[i].ax) ? 1 : 0) *
                ((_artl.ay[i] == ARTList[i].ay) ? 1 : 0);
            }
            return Convert.ToBoolean(same);
        }
    }

    [Serializable]
    public class PlayArt
    {
        public override string ToString()
        {
            return
                "sp: " + "[" + string.Join(", ", sp) + "]\t" +
                "ls: " + "[" + string.Join(", ", ls) + "]\t" +
                "at: " + "[" + string.Join(", ", at) + "]\t" +
                "ct: " + "[" + string.Join(", ", ct) + "]\t" +
                "lt: " + "[" + string.Join(", ", lt) + "]\t" +
                "au: " + "[" + string.Join(", ", au) + "]\t" +
                "av: " + "[" + string.Join(", ", av) + "]\t" +
                "ax: " + "[" + string.Join(", ", ax) + "]\t" +
                "ay: " + "[" + string.Join(", ", ay) + "]";
        }

        public int sp;
        public int ls;
        public int at;
        public int ct;
        public int lt;
        public int au;
        public int av;
        public int ax;
        public int ay;
    }
}
