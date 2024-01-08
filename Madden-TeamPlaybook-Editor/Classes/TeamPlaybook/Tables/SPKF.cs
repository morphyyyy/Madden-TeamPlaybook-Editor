using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TDBAccess;

namespace Madden.TeamPlaybook
{
    [Serializable]
    public class SPKF
    {
        public int rec { get; set; }
        /// <summary>
        /// SETL.setl
        /// </summary>
        public int SETL { get; set; }
        /// <summary>
        /// SPKG.SPF_
        /// < summary>
        public int SPF_ { get; set; }
        public string name { get; set; }

        public override string ToString()
        {
            return
                "Rec#: " + rec +
                "   SETL: " + SETL +
                "   SPF_: " + SPF_ +
                "   Name: " + name;
        }

        public static List<SPKF> GetSPKF(int filter = 0, int DBIndex = 0)
        {
            List<SPKF> _SPKF = new List<SPKF>();

            TdbTableProperties TableProps = new TdbTableProperties();
            TableProps.Name = new string((char)0, 5);

            // Get Tableprops based on the selected index
            if (!TDB.TDBTableGetProperties(DBIndex, TableNames.GetTables().Find(item => item.name == "SPKF").rec, ref TableProps))
                return null;

            List<TdbFieldProperties> tableFields = new List<TdbFieldProperties>();

            for (int i = 0; i < TableProps.FieldCount; i++)
            {
                TdbFieldProperties FieldProps = new TdbFieldProperties();
                FieldProps.Name = new string((char)0, 5);
                TDB.TDBFieldGetProperties(DBIndex, TDB.StrReverse("SPKF"), i, ref FieldProps);
                tableFields.Add(FieldProps);
            }

            for (int i = 0; i < TableProps.RecordCount; i++)
            {
                string _name = new string((char)0, (tableFields.FirstOrDefault(field => field.Name == TDB.StrReverse("name")).Size / 8) + 1);

                TDB.TDBFieldGetValueAsString(DBIndex, TDB.StrReverse("SPKF"), TDB.StrReverse("name"), i, ref _name);
                _name = _name.Replace(",", "");

                if (filter != 0 && (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("SPKF"), TDB.StrReverse("SETL"), i) == filter)
                {
                    _SPKF.Add(new SPKF
                    {
                        rec = i,
                        SETL = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("SPKF"), TDB.StrReverse("SETL"), i),
                        SPF_ = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("SPKF"), TDB.StrReverse("SPF_"), i),
                        name = _name
                    });
                }
                else if (filter == 0)
                {
                    _SPKF.Add(new SPKF
                    {
                        rec = i,
                        SETL = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("SPKF"), TDB.StrReverse("SETL"), i),
                        SPF_ = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("SPKF"), TDB.StrReverse("SPF_"), i),
                        name = _name
                    });
                }
            }
            return _SPKF;
        }

        public static void SetSPKF(List<SPKF> SPKF, int DBIndex = 0)
        {
            TdbTableProperties TableProps = new TdbTableProperties();
            TableProps.Name = new string((char)0, 5);

            // Get Tableprops based on the selected index
            TDB.TDBTableGetProperties(DBIndex, TableNames.GetTables().Find(item => item.name == "SPKF").rec, ref TableProps);

            if (SPKF.Count > TableProps.RecordCount)
                for (int i = TableProps.RecordCount - 1; i < SPKF.Count - 1; i++)
                {
                    TDB.TDBTableRecordAdd(DBIndex, TDB.StrReverse("SPKF"), true);
                }
            else if (SPKF.Count < TableProps.RecordCount)
            {
                for (int i = TableProps.RecordCount - 1; i > SPKF.Count - 1; i--)
                {
                    TDB.TDBTableRecordRemove(DBIndex, TDB.StrReverse("SPKF"), i);
                }
            }

            foreach (SPKF item in SPKF)
            {
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("SPKF"), TDB.StrReverse("SETL"), item.rec, item.SETL);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("SPKF"), TDB.StrReverse("SPF_"), item.rec, item.SPF_);
                TDB.TDBFieldSetValueAsString(DBIndex, TDB.StrReverse("SPKF"), TDB.StrReverse("name"), item.rec, item.name);
            }
        }

        public static List<SPKF> Sort(List<SPKF> SPKF)
        {
            return SPKF.OrderBy(s => s.SETL).ThenBy(s => s.SPF_).Cast<SPKF>().ToList();
        }
    }
}
