using System;
using System.Collections.Generic;
using System.Linq;
using TDBAccess;

namespace Madden.CustomPlaybook
{
    [Serializable]
    public class PBFM
    {
        public int rec { get; set; }
        public int FAU1 { get; set; }
        public int FAU2 { get; set; }
        public int FAU3 { get; set; }
        public int FAU4 { get; set; }
        public int pbfm { get; set; }
        public int FTYP { get; set; }
        public int ord_ { get; set; }
        public string name { get; set; }

        public override string ToString()
        {
            return
                "Rec#: " + rec + "\t" +
                "   FAU1: " + FAU1 + "\t" +
                "   FAU2: " + FAU2 + "\t" +
                "   FAU3: " + FAU3 + "\t" +
                "   FAU4: " + FAU4 + "\t" +
                "   pbfm: " + pbfm + "\t" +
                "   FTYP: " + FTYP + "\t" +
                "   ord_: " + ord_ + "\t" +
                "   Name: " + name;
        }

        public static List<PBFM> GetPBFM(int filter = 0, int DBIndex = 0)
        {
            List<PBFM> _Formations = new List<PBFM>();

            TdbTableProperties TableProps = new TdbTableProperties();
            TableProps.Name = new string((char)0, 5);

            // Get Tableprops based on the selected index
            if (!TDB.TDBTableGetProperties(DBIndex, TableNames.GetTables().Find(item => item.name == "PBFM").rec, ref TableProps))
                return null;

            List<TdbFieldProperties> tableFields = new List<TdbFieldProperties>();

            for (int i = 0; i < TableProps.FieldCount; i++)
            {
                TdbFieldProperties FieldProps = new TdbFieldProperties();
                FieldProps.Name = new string((char)0, 5);
                TDB.TDBFieldGetProperties(DBIndex, TDB.StrReverse("PBFM"), i, ref FieldProps);
                tableFields.Add(FieldProps);
            }

            for (int i = 0; i < TableProps.RecordCount; i++)
            {
                string _name = new string((char)0, (tableFields.FirstOrDefault(field => field.Name == TDB.StrReverse("name")).Size / 8) + 1);

                TDB.TDBFieldGetValueAsString(DBIndex, TDB.StrReverse("PBFM"), TDB.StrReverse("name"), i, ref _name);
                _name = _name.Replace(",", "");

                if (filter != 0 && (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PBFM"), TDB.StrReverse("FTYP"), i) == filter)
                {
                    _Formations.Add(new PBFM
                    {
                        rec = i,
                        FAU1 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PBFM"), TDB.StrReverse("FAU1"), i),
                        FAU2 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PBFM"), TDB.StrReverse("FAU2"), i),
                        FAU3 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PBFM"), TDB.StrReverse("FAU3"), i),
                        FAU4 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PBFM"), TDB.StrReverse("FAU4"), i),
                        pbfm = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PBFM"), TDB.StrReverse("PBFM"), i),
                        FTYP = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PBFM"), TDB.StrReverse("FTYP"), i),
                        ord_ = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PBFM"), TDB.StrReverse("ord_"), i),
                        name = _name
                    });
                }
                else if (filter == 0)
                {
                    _Formations.Add(new PBFM
                    {
                        rec = i,
                        FAU1 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PBFM"), TDB.StrReverse("FAU1"), i),
                        FAU2 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PBFM"), TDB.StrReverse("FAU2"), i),
                        FAU3 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PBFM"), TDB.StrReverse("FAU3"), i),
                        FAU4 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PBFM"), TDB.StrReverse("FAU4"), i),
                        pbfm = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PBFM"), TDB.StrReverse("PBFM"), i),
                        FTYP = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PBFM"), TDB.StrReverse("FTYP"), i),
                        ord_ = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PBFM"), TDB.StrReverse("ord_"), i),
                        name = _name
                    });
                }

            }
            return _Formations;
        }

        public static void SetPBFM(List<PBFM> PBFM, int DBIndex = 0)
        {
            TdbTableProperties TableProps = new TdbTableProperties();
            TableProps.Name = new string((char)0, 5);

            // Get Tableprops based on the selected index
            TDB.TDBTableGetProperties(DBIndex, TableNames.GetTables().Find(item => item.name == "PBFM").rec, ref TableProps);

            if (PBFM.Count > TableProps.RecordCount)
                for (int i = TableProps.RecordCount - 1; i < PBFM.Count - 1; i++)
                {
                    TDB.TDBTableRecordAdd(DBIndex, TDB.StrReverse("PBFM"), true);
                }
            else if (PBFM.Count < TableProps.RecordCount)
            {
                for (int i = TableProps.RecordCount - 1; i > PBFM.Count - 1; i--)
                {
                    TDB.TDBTableRecordRemove(DBIndex, TDB.StrReverse("PBFM"), i);
                }

            }

            foreach (PBFM item in PBFM)
            {
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PBFM"), TDB.StrReverse("FAU1"), item.rec, item.FAU1);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PBFM"), TDB.StrReverse("FAU2"), item.rec, item.FAU2);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PBFM"), TDB.StrReverse("FAU3"), item.rec, item.FAU3);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PBFM"), TDB.StrReverse("FAU4"), item.rec, item.FAU4);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PBFM"), TDB.StrReverse("PBFM"), item.rec, item.pbfm);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PBFM"), TDB.StrReverse("FTYP"), item.rec, item.FTYP);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PBFM"), TDB.StrReverse("ord_"), item.rec, item.ord_);
                TDB.TDBFieldSetValueAsString(DBIndex, TDB.StrReverse("PBFM"), TDB.StrReverse("name"), item.rec, item.name);
            }
        }

        public static List<PBFM> Sort(List<PBFM> PBFM)
        {
            return PBFM.OrderBy(s => s.FTYP).ThenBy(s => s.ord_).Cast<PBFM>().ToList();
        }
    }
}
