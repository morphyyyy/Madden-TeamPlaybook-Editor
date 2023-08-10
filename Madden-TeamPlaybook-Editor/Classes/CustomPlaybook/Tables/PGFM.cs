using System;
using System.Collections.Generic;
using System.Linq;
using TDBAccess;

namespace Madden.CustomPlaybook
{
    [Serializable]
    public class PGFM
    {
        public int rec { get; set; }
        public int BOKL { get; set; }
        public int PBFM { get; set; }
        public int SRFM { get; set; }

        public override string ToString()
        {
            return
                "Rec#: " + rec + "\t" +
                "   BOKL: " + BOKL + "\t" +
                "   PBFM: " + PBFM + "\t" +
                "   SRFM: " + SRFM;
        }

        public static List<PGFM> GetPGFM(int filter = 0, int DBIndex = 0)
        {
            List<PGFM> _Formations = new List<PGFM>();

            TdbTableProperties TableProps = new TdbTableProperties();
            TableProps.Name = new string((char)0, 5);

            // Get Tableprops based on the selected index
            if (!TDB.TDBTableGetProperties(DBIndex, TableNames.GetTables().Find(item => item.name == "PGFM").rec, ref TableProps))
                return null;

            List<TdbFieldProperties> tableFields = new List<TdbFieldProperties>();

            for (int i = 0; i < TableProps.FieldCount; i++)
            {
                TdbFieldProperties FieldProps = new TdbFieldProperties();
                FieldProps.Name = new string((char)0, 5);
                TDB.TDBFieldGetProperties(DBIndex, TDB.StrReverse("PGFM"), i, ref FieldProps);
                tableFields.Add(FieldProps);
            }

            for (int i = 0; i < TableProps.RecordCount; i++)
            {
                if (filter != 0 && (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PGFM"), TDB.StrReverse("FTYP"), i) == filter)
                {
                    _Formations.Add(new PGFM
                    {
                        rec = i,
                        BOKL = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PGFM"), TDB.StrReverse("BOKL"), i),
                        PBFM = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PGFM"), TDB.StrReverse("PBFM"), i),
                        SRFM = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PGFM"), TDB.StrReverse("SRFM"), i)
                    });
                }
                else if (filter == 0)
                {
                    _Formations.Add(new PGFM
                    {
                        rec = i,
                        BOKL = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PGFM"), TDB.StrReverse("BOKL"), i),
                        PBFM = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PGFM"), TDB.StrReverse("PBFM"), i),
                        SRFM = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PGFM"), TDB.StrReverse("SRFM"), i)
                    });
                }
            }

            //_Formations = _Formations.ToList().OrderBy(s => s.FTYP).ThenBy(s => s.form).Cast<FORM>().ToList();

            return _Formations;
        }

        public static void SetPGFM(List<PGFM> PGFM, int DBIndex = 0)
        {
            TdbTableProperties TableProps = new TdbTableProperties();
            TableProps.Name = new string((char)0, 5);

            // Get Tableprops based on the selected index
            TDB.TDBTableGetProperties(DBIndex, TableNames.GetTables().Find(item => item.name == "PGFM").rec, ref TableProps);

            if (PGFM.Count > TableProps.RecordCount)
                for (int i = TableProps.RecordCount - 1; i < PGFM.Count - 1; i++)
                {
                    TDB.TDBTableRecordAdd(DBIndex, TDB.StrReverse("PGFM"), true);
                }
            else if (PGFM.Count < TableProps.RecordCount)
            {
                for (int i = TableProps.RecordCount - 1; i > PGFM.Count - 1; i--)
                {
                    TDB.TDBTableRecordRemove(DBIndex, TDB.StrReverse("PGFM"), i);
                }
            }

            foreach (PGFM item in PGFM)
            {
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PGFM"), TDB.StrReverse("BOKL"), item.rec, item.BOKL);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PGFM"), TDB.StrReverse("PBFM"), item.rec, item.PBFM);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PGFM"), TDB.StrReverse("SRFM"), item.rec, item.SRFM);
            }
        }

        public static List<PGFM> Sort(List<PGFM> PGFM)
        {
            return PGFM.OrderBy(s => s.PBFM).Cast<PGFM>().ToList();
        }
    }
}
