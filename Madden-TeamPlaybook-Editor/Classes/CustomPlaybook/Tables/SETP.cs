using System;
using System.Collections.Generic;
using System.Linq;
using TDBAccess;

namespace Madden.CustomPlaybook
{
    [Serializable]
    public class SETP
    {
        public int rec { get; set; }
        public int SETL { get; set; }
        public int setp { get; set; }
        public int SGT_ { get; set; }
        public int arti { get; set; }
        public int opnm { get; set; }
        public int tabo { get; set; }
        public int poso { get; set; }
        public int odep { get; set; }
        public int flas { get; set; }
        public int DPos { get; set; }
        public int EPos { get; set; }
        public int fmtx { get; set; }
        public int artx { get; set; }
        public int fmty { get; set; }
        public int arty { get; set; }

        public override string ToString()
        {
            return
                "Rec#: " + rec + "\t" +
                "   SETL: " + SETL + "\t" +
                "   setp: " + setp + "\t" +
                "   SGT_: " + SGT_ + "\t" +
                "   arti: " + arti + "\t" +
                "   opnm: " + opnm + "\t" +
                "   tabo: " + tabo + "\t" +
                "   poso: " + poso + "\t" +
                "   odep: " + odep + "\t" +
                "   flas: " + flas + "\t" +
                "   DPos: " + DPos + "\t" +
                "   EPos: " + EPos + "\t" +
                "   fmtx: " + fmtx + "\t" +
                "   artx: " + artx + "\t" +
                "   fmty: " + fmty + "\t" +
                "   arty: " + arty;
        }

        public static List<SETP> GetSETP(int filter = 0, int DBIndex = 0)
        {
            List<SETP> _SETP = new List<SETP>();

            TdbTableProperties TableProps = new TdbTableProperties();
            TableProps.Name = new string((char)0, 5);

            // Get Tableprops based on the selected index
            if (!TDB.TDBTableGetProperties(DBIndex, TableNames.GetTables().Find(item => item.name == "SETP").rec, ref TableProps))
                return null;

            for (int i = 0; i < TableProps.RecordCount; i++)
            {
                if (filter != 0 && (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("SETP"), TDB.StrReverse("SETL"), i) == filter)
                {
                    _SETP.Add(new SETP
                    {
                        rec = i,
                        SETL = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("SETP"), TDB.StrReverse("SETL"), i),
                        setp = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("SETP"), TDB.StrReverse("SETP"), i),
                        SGT_ = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("SETP"), TDB.StrReverse("SGT_"), i),
                        arti = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("SETP"), TDB.StrReverse("arti"), i),
                        opnm = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("SETP"), TDB.StrReverse("opnm"), i),
                        tabo = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("SETP"), TDB.StrReverse("tabo"), i),
                        poso = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("SETP"), TDB.StrReverse("poso"), i),
                        flas = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("SETP"), TDB.StrReverse("flas"), i),
                        DPos = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("SETP"), TDB.StrReverse("DPos"), i),
                        EPos = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("SETP"), TDB.StrReverse("EPos"), i),
                        fmtx = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("SETP"), TDB.StrReverse("fmtx"), i),
                        artx = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("SETP"), TDB.StrReverse("artx"), i),
                        fmty = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("SETP"), TDB.StrReverse("fmty"), i),
                        arty = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("SETP"), TDB.StrReverse("arty"), i)
                    });
                }
                else if (filter == 0)
                {
                    _SETP.Add(new SETP
                    {
                        rec = i,
                        SETL = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("SETP"), TDB.StrReverse("SETL"), i),
                        setp = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("SETP"), TDB.StrReverse("SETP"), i),
                        SGT_ = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("SETP"), TDB.StrReverse("SGT_"), i),
                        arti = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("SETP"), TDB.StrReverse("arti"), i),
                        opnm = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("SETP"), TDB.StrReverse("opnm"), i),
                        tabo = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("SETP"), TDB.StrReverse("tabo"), i),
                        poso = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("SETP"), TDB.StrReverse("poso"), i),
                        odep = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("SETP"), TDB.StrReverse("odep"), i),
                        flas = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("SETP"), TDB.StrReverse("flas"), i),
                        DPos = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("SETP"), TDB.StrReverse("DPos"), i),
                        EPos = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("SETP"), TDB.StrReverse("EPos"), i),
                        fmtx = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("SETP"), TDB.StrReverse("fmtx"), i),
                        artx = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("SETP"), TDB.StrReverse("artx"), i),
                        fmty = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("SETP"), TDB.StrReverse("fmty"), i),
                        arty = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("SETP"), TDB.StrReverse("arty"), i)
                    });
                }
            }
            return _SETP;
        }

        public static void SetSETP(List<SETP> SETP, int DBIndex = 0)
        {
            TdbTableProperties TableProps = new TdbTableProperties();
            TableProps.Name = new string((char)0, 5);

            // Get Tableprops based on the selected index
            TDB.TDBTableGetProperties(DBIndex, TableNames.GetTables().Find(item => item.name == "SETP").rec, ref TableProps);

            if (SETP.Count > TableProps.RecordCount)
                for (int i = TableProps.RecordCount - 1; i < SETP.Count - 1; i++)
                {
                    TDB.TDBTableRecordAdd(DBIndex, TDB.StrReverse("SETP"), true);
                }
            else if (SETP.Count < TableProps.RecordCount)
            {
                for (int i = TableProps.RecordCount - 1; i > SETP.Count - 1; i--)
                {
                    TDB.TDBTableRecordRemove(DBIndex, TDB.StrReverse("SETP"), i);
                }
            }

            foreach (SETP item in SETP)
            {
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("SETP"), TDB.StrReverse("SETL"), item.rec, item.SETL);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("SETP"), TDB.StrReverse("SETP"), item.rec, item.setp);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("SETP"), TDB.StrReverse("SGT_"), item.rec, item.SGT_);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("SETP"), TDB.StrReverse("arti"), item.rec, item.arti);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("SETP"), TDB.StrReverse("opnm"), item.rec, item.arti);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("SETP"), TDB.StrReverse("tabo"), item.rec, item.tabo);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("SETP"), TDB.StrReverse("poso"), item.rec, item.poso);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("SETP"), TDB.StrReverse("odep"), item.rec, item.arti);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("SETP"), TDB.StrReverse("flas"), item.rec, item.flas);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("SETP"), TDB.StrReverse("DPos"), item.rec, item.DPos);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("SETP"), TDB.StrReverse("EPos"), item.rec, item.EPos);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("SETP"), TDB.StrReverse("fmtx"), item.rec, item.fmtx);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("SETP"), TDB.StrReverse("artx"), item.rec, item.artx);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("SETP"), TDB.StrReverse("fmty"), item.rec, item.fmty);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("SETP"), TDB.StrReverse("arty"), item.rec, item.arty);
            }
        }

        public static List<SETP> Sort(List<SETP> SETP)
        {
            return SETP.OrderBy(s => s.SETL).ThenBy(s => s.poso).Cast<SETP>().ToList();
        }
    }
}
