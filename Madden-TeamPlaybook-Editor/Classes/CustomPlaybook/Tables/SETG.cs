using System;
using System.Collections.Generic;
using System.Linq;
using TDBAccess;

namespace Madden.CustomPlaybook
{
    [Serializable]
    public class SETG
    {
        public int rec { get; set; }
        public int SETP { get; set; }
        public int SGF_ { get; set; }
        public int SF__ { get; set; }
        public float x___ { get; set; }
        public float y___ { get; set; }
        public float fx__ { get; set; }
        public float fy__ { get; set; }
        public int anm_ { get; set; }
        public int dir_ { get; set; }
        public int fanm { get; set; }
        public int fdir { get; set; }

        public override string ToString()
        {
            return
                "Rec#: " + rec + "\t" +
                "   SETP: " + SETP + "\t" +
                "   SGF_: " + SGF_ + "\t" +
                "   SF__: " + SF__ + "\t" +
                "   x___: " + x___ + "\t" +
                "   y___: " + y___ + "\t" +
                "   fx__: " + fx__ + "\t" +
                "   fy__: " + fy__ + "\t" +
                "   anm_: " + anm_ + "\t" +
                "   dir_: " + dir_ + "\t" +
                "   fanm: " + fanm + "\t" +
                "   fdir: " + fdir;
        }

        public static List<SETG> GetSETG(int filter = 0, int DBIndex = 0)
        {
            List<SETG> _SETG = new List<SETG>();

            TdbTableProperties TableProps = new TdbTableProperties();
            TableProps.Name = new string((char)0, 5);

            // Get Tableprops based on the selected index
            if (!TDB.TDBTableGetProperties(DBIndex, TableNames.GetTables().Find(item => item.name == "SETG").rec, ref TableProps))
                return null;

            for (int i = 0; i < TableProps.RecordCount; i++)
            {
                if (filter != 0 && (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("SETG"), TDB.StrReverse("SETP"), i) == filter)
                {
                    _SETG.Add(new SETG
                    {
                        rec = i,
                        SETP = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("SETG"), TDB.StrReverse("SETP"), i),
                        SGF_ = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("SETG"), TDB.StrReverse("SGF_"), i),
                        SF__ = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("SETG"), TDB.StrReverse("SF__"), i),
                        x___ = (float)TDB.TDBFieldGetValueAsFloat(DBIndex, TDB.StrReverse("SETG"), TDB.StrReverse("x___"), i),
                        y___ = (float)TDB.TDBFieldGetValueAsFloat(DBIndex, TDB.StrReverse("SETG"), TDB.StrReverse("y___"), i),
                        fx__ = (float)TDB.TDBFieldGetValueAsFloat(DBIndex, TDB.StrReverse("SETG"), TDB.StrReverse("fx__"), i),
                        fy__ = (float)TDB.TDBFieldGetValueAsFloat(DBIndex, TDB.StrReverse("SETG"), TDB.StrReverse("fy__"), i),
                        anm_ = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("SETG"), TDB.StrReverse("anm_"), i),
                        dir_ = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("SETG"), TDB.StrReverse("dir_"), i),
                        fanm = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("SETG"), TDB.StrReverse("fanm"), i),
                        fdir = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("SETG"), TDB.StrReverse("fdir"), i)
                    });
                }
                else if (filter == 0)
                {
                    _SETG.Add(new SETG
                    {
                        rec = i,
                        SETP = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("SETG"), TDB.StrReverse("SETP"), i),
                        SGF_ = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("SETG"), TDB.StrReverse("SGF_"), i),
                        SF__ = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("SETG"), TDB.StrReverse("SF__"), i),
                        x___ = (float)TDB.TDBFieldGetValueAsFloat(DBIndex, TDB.StrReverse("SETG"), TDB.StrReverse("x___"), i),
                        y___ = (float)TDB.TDBFieldGetValueAsFloat(DBIndex, TDB.StrReverse("SETG"), TDB.StrReverse("y___"), i),
                        fx__ = (float)TDB.TDBFieldGetValueAsFloat(DBIndex, TDB.StrReverse("SETG"), TDB.StrReverse("fx__"), i),
                        fy__ = (float)TDB.TDBFieldGetValueAsFloat(DBIndex, TDB.StrReverse("SETG"), TDB.StrReverse("fy__"), i),
                        anm_ = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("SETG"), TDB.StrReverse("anm_"), i),
                        dir_ = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("SETG"), TDB.StrReverse("dir_"), i),
                        fanm = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("SETG"), TDB.StrReverse("fanm"), i),
                        fdir = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("SETG"), TDB.StrReverse("fdir"), i)
                    });
                }
            }
            return _SETG;
        }

        public static void SetSETG(List<SETG> SETG, int DBIndex = 0)
        {
            TdbTableProperties TableProps = new TdbTableProperties();
            TableProps.Name = new string((char)0, 5);

            // Get Tableprops based on the selected index
            TDB.TDBTableGetProperties(DBIndex, TableNames.GetTables().Find(item => item.name == "SETG").rec, ref TableProps);

            if (SETG.Count > TableProps.RecordCount)
                for (int i = TableProps.RecordCount - 1; i < SETG.Count - 1; i++)
                {
                    TDB.TDBTableRecordAdd(DBIndex, TDB.StrReverse("SETG"), true);
                }
            else if (SETG.Count < TableProps.RecordCount)
            {
                for (int i = TableProps.RecordCount - 1; i > SETG.Count - 1; i--)
                {
                    TDB.TDBTableRecordRemove(DBIndex, TDB.StrReverse("SETG"), i);
                }
            }

            foreach (SETG item in SETG)
            {
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("SETG"), TDB.StrReverse("SETP"), item.rec, item.SETP);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("SETG"), TDB.StrReverse("SGF_"), item.rec, item.SGF_);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("SETG"), TDB.StrReverse("SF__"), item.rec, item.SF__);
                TDB.TDBFieldSetValueAsFloat(DBIndex, TDB.StrReverse("SETG"), TDB.StrReverse("x___"), item.rec, item.x___);
                TDB.TDBFieldSetValueAsFloat(DBIndex, TDB.StrReverse("SETG"), TDB.StrReverse("y___"), item.rec, item.y___);
                TDB.TDBFieldSetValueAsFloat(DBIndex, TDB.StrReverse("SETG"), TDB.StrReverse("fx__"), item.rec, item.fx__);
                TDB.TDBFieldSetValueAsFloat(DBIndex, TDB.StrReverse("SETG"), TDB.StrReverse("fy__"), item.rec, item.fy__);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("SETG"), TDB.StrReverse("anm_"), item.rec, item.anm_);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("SETG"), TDB.StrReverse("dir_"), item.rec, item.dir_);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("SETG"), TDB.StrReverse("fanm"), item.rec, item.fanm);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("SETG"), TDB.StrReverse("fdir"), item.rec, item.fdir);
            }
        }

        public static List<SETG> Sort(List<SETG> SETG)
        {
            return SETG.OrderBy(s => s.SETP).ThenBy(s => s.SGF_).Cast<SETG>().ToList();
        }
    }
}
