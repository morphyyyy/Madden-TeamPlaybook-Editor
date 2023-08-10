using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TDBAccess;

namespace Madden.TeamPlaybook
{
    [Serializable]
    public class SGFM
    {
        public int rec { get; set; }
        public int SETL { get; set; }
        public int SGF_ { get; set; }
        public string name { get; set; }
        public int dflt { get; set; }

        public override string ToString()
        {
            return
                "Rec#: " + rec +
                "   SETL: " + SETL +
                "   SGF_: " + SGF_ +
                "   Name: " + name +
                "   dflt: " + dflt;
        }

        public static List<SGFM> GetSGFM(int filter = 0, int DBIndex = 0)
        {
            List<SGFM> _SGFM = new List<SGFM>();

            TdbTableProperties TableProps = new TdbTableProperties();
            TableProps.Name = new string((char)0, 5);

            List<TdbFieldProperties> tableFields = new List<TdbFieldProperties>();

            // Get Tableprops based on the selected index
            if (TableNames.GetTables().Find(item => item.name == "SGFM") != null)
            {
                if (!TDB.TDBTableGetProperties(DBIndex, TableNames.GetTables().Find(item => item.name == "SGFM").rec, ref TableProps))
                {
                    return null;
                }
            }
            else if (TableNames.GetTables().Find(item => item.name == "") != null)
            {
                if (!TDB.TDBTableGetProperties(DBIndex, TableNames.GetTables().Find(item => item.name == "").rec, ref TableProps))
                {
                    return null;
                }
            }

            for (int i = 0; i < TableProps.FieldCount; i++)
            {
                TdbFieldProperties FieldProps = new TdbFieldProperties();
                FieldProps.Name = new string((char)0, 5);
                TDB.TDBFieldGetProperties(DBIndex, TableProps.Name, i, ref FieldProps);
                tableFields.Add(FieldProps);
            }

            for (int i = 0; i < TableProps.RecordCount; i++)
            {
                string _name = new string((char)0, (tableFields.Where(field => field.Name == TDB.StrReverse("name")).FirstOrDefault().Size / 8) + 1);

                TDB.TDBFieldGetValueAsString(DBIndex, TableProps.Name, TDB.StrReverse("name"), i, ref _name);
                _name = _name.Replace(",", "");

                if (filter != 0 && (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TableProps.Name, TDB.StrReverse("SETL"), i) == filter)
                {
                    _SGFM.Add(new SGFM
                    {
                        rec = i,
                        SETL = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TableProps.Name, TDB.StrReverse("SETL"), i),
                        SGF_ = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TableProps.Name, TDB.StrReverse("SGF_"), i),
                        name = _name,
                        dflt = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TableProps.Name, TDB.StrReverse("dflt"), i)
                    });
                }
                else if (filter == 0)
                {
                    _SGFM.Add(new SGFM
                    {
                        rec = i,
                        SETL = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TableProps.Name, TDB.StrReverse("SETL"), i),
                        SGF_ = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TableProps.Name, TDB.StrReverse("SGF_"), i),
                        name = _name,
                        dflt = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TableProps.Name, TDB.StrReverse("dflt"), i)
                    });
                }
            }
            //_SGFM = _SGFM.OrderBy(alignment => alignment.SETL).ThenBy(alignment => alignment.SGF_).ToList();
            return _SGFM;
        }

        public static void SetSGFM(List<SGFM> SGFM, int DBIndex = 0)
        {
            TdbTableProperties TableProps = new TdbTableProperties();
            TableProps.Name = new string((char)0, 5);

            // Get Tableprops based on the selected index
            if (TableNames.GetTables().Find(item => item.name == "SGFM") != null)
            {
                TDB.TDBTableGetProperties(DBIndex, TableNames.GetTables().Find(item => item.name == "SGFM").rec, ref TableProps);
            }
            else if (TableNames.GetTables().Find(item => item.name == "") != null)
            {
                TDB.TDBTableGetProperties(DBIndex, TableNames.GetTables().Find(item => item.name == "").rec, ref TableProps);
            }

            if (SGFM.Count > TableProps.RecordCount)
                for (int i = TableProps.RecordCount - 1; i < SGFM.Count - 1; i++)
                {
                    TDB.TDBTableRecordAdd(DBIndex, TableProps.Name, true);
                }
            else if (SGFM.Count < TableProps.RecordCount)
            {
                for (int i = TableProps.RecordCount - 1; i > SGFM.Count - 1; i--)
                {
                    TDB.TDBTableRecordRemove(DBIndex, TDB.StrReverse("SGFM"), i);
                }
            }

            foreach (SGFM item in SGFM)
            {
                TDB.TDBFieldSetValueAsInteger(DBIndex, TableProps.Name, TDB.StrReverse("SETL"), item.rec, item.SETL);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TableProps.Name, TDB.StrReverse("SGF_"), item.rec, item.SGF_);
                TDB.TDBFieldSetValueAsString(DBIndex, TableProps.Name, TDB.StrReverse("name"), item.rec, item.name);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TableProps.Name, TDB.StrReverse("dflt"), item.rec, item.dflt);
            }
        }

        public static List<SGFM> Sort(List<SGFM> SGFM)
        {
            return SGFM.OrderBy(s => s.SETL).ThenBy(s => s.SGF_).Cast<SGFM>().ToList();
        }
    }
}
