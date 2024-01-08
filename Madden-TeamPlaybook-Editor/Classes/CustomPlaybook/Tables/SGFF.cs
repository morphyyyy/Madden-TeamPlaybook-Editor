using System;
using System.Collections.Generic;
using System.Linq;
using TDBAccess;

namespace Madden.CustomPlaybook
{
    [Serializable]
    public class SGFF
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

        public static List<SGFF> GetSGFF(int filter = 0, int DBIndex = 0)
        {
            List<SGFF> _SGFF = new List<SGFF>();

            TdbTableProperties TableProps = new TdbTableProperties();
            TableProps.Name = new string((char)0, 5);

            List<TdbFieldProperties> tableFields = new List<TdbFieldProperties>();

            // Get Tableprops based on the selected index
            if (TableNames.GetTables().Find(item => item.name == "SGFF") != null)
            {
                if (!TDB.TDBTableGetProperties(DBIndex, TableNames.GetTables().Find(item => item.name == "SGFF").rec, ref TableProps))
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
                string _name = new string((char)0, (tableFields.FirstOrDefault(field => field.Name == TDB.StrReverse("name")).Size / 8) + 1);

                TDB.TDBFieldGetValueAsString(DBIndex, TableProps.Name, TDB.StrReverse("name"), i, ref _name);
                _name = _name.Replace(",", "");

                if (filter != 0 && (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TableProps.Name, TDB.StrReverse("SETL"), i) == filter)
                {
                    _SGFF.Add(new SGFF
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
                    _SGFF.Add(new SGFF
                    {
                        rec = i,
                        SETL = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TableProps.Name, TDB.StrReverse("SETL"), i),
                        SGF_ = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TableProps.Name, TDB.StrReverse("SGF_"), i),
                        name = _name,
                        dflt = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TableProps.Name, TDB.StrReverse("dflt"), i)
                    });
                }
            }
            //_SGFF = _SGFF.OrderBy(alignment => alignment.SETL).ThenBy(alignment => alignment.SGF_).ToList();
            return _SGFF;
        }

        public static void SetSGFF(List<SGFF> SGFF, int DBIndex = 0)
        {
            TdbTableProperties TableProps = new TdbTableProperties();
            TableProps.Name = new string((char)0, 5);

            // Get Tableprops based on the selected index
            if (TableNames.GetTables().Find(item => item.name == "SGFF") != null)
            {
                TDB.TDBTableGetProperties(DBIndex, TableNames.GetTables().Find(item => item.name == "SGFF").rec, ref TableProps);
            }
            else if (TableNames.GetTables().Find(item => item.name == "") != null)
            {
                TDB.TDBTableGetProperties(DBIndex, TableNames.GetTables().Find(item => item.name == "").rec, ref TableProps);
            }

            if (SGFF.Count > TableProps.RecordCount)
                for (int i = TableProps.RecordCount - 1; i < SGFF.Count - 1; i++)
                {
                    TDB.TDBTableRecordAdd(DBIndex, TableProps.Name, true);
                }
            else if (SGFF.Count < TableProps.RecordCount)
            {
                for (int i = TableProps.RecordCount - 1; i > SGFF.Count - 1; i--)
                {
                    TDB.TDBTableRecordRemove(DBIndex, TDB.StrReverse("SGFF"), i);
                }
            }

            foreach (SGFF item in SGFF)
            {
                TDB.TDBFieldSetValueAsInteger(DBIndex, TableProps.Name, TDB.StrReverse("SETL"), item.rec, item.SETL);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TableProps.Name, TDB.StrReverse("SGF_"), item.rec, item.SGF_);
                TDB.TDBFieldSetValueAsString(DBIndex, TableProps.Name, TDB.StrReverse("name"), item.rec, item.name);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TableProps.Name, TDB.StrReverse("dflt"), item.rec, item.dflt);
            }
        }

        public static List<SGFF> Sort(List<SGFF> SGFF)
        {
            return SGFF.OrderBy(s => s.SETL).ThenBy(s => s.SGF_).Cast<SGFF>().ToList();
        }
    }
}
