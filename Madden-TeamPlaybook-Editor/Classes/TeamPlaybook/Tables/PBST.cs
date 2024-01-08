using System;
using System.Collections.Generic;
using System.Linq;
using TDBAccess;

namespace Madden.TeamPlaybook
{
    [Serializable]
    public class PBST
    {
        public int rec { get; set; }
        /// <summary>
        /// SETL.setl
        /// </summary>
        public int SETL { get; set; }
        /// <summary>
        /// PBFM.pbfm
        /// </summary>
        public int PBFM { get; set; }
        /// <summary>
        /// PBPL.pbst
        /// </summary>
        public int pbst { get; set; }       
        public int SPF_ { get; set; }
        public int ord_ { get; set; }
        /// <summary>
        /// SETL.name
        /// </summary>
        public string name { get; set; }

        public override string ToString()
        {
            return
                "Rec#: " + rec +
                "   SETL: " + SETL +
                "   PBFM: " + PBFM +
                "   PBST: " + pbst +
                "   SPF_: " + SPF_ +
                "   ord_: " + ord_ +
                "   Name: " + name;
        }

        public static List<PBST> GetPBST(int filter = 0, int DBIndex = 0)
        {
            List<PBST> _SubFormations = new List<PBST>();

            TdbTableProperties TableProps = new TdbTableProperties();
            TableProps.Name = new string((char)0, 5);

            // Get Tableprops based on the selected index
            if (!TDB.TDBTableGetProperties(DBIndex, TableNames.GetTables().Find(item => item.name == "PBST").rec, ref TableProps))
                return null;

            List<TdbFieldProperties> tableFields = new List<TdbFieldProperties>();

            for (int i = 0; i < TableProps.FieldCount; i++)
            {
                TdbFieldProperties FieldProps = new TdbFieldProperties();
                FieldProps.Name = new string((char)0, 5);
                TDB.TDBFieldGetProperties(DBIndex, TDB.StrReverse("PBST"), i, ref FieldProps);
                tableFields.Add(FieldProps);
            }

            for (int i = 0; i < TableProps.RecordCount; i++)
            {
                string _name = new string((char)0, (tableFields.FirstOrDefault(field => field.Name == TDB.StrReverse("name")).Size / 8) + 1);

                TDB.TDBFieldGetValueAsString(DBIndex, TDB.StrReverse("PBST"), TDB.StrReverse("name"), i, ref _name);
                _name = _name.Replace(",", "");

                if (filter != 0 && (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PBST"), TDB.StrReverse("PBFM"), i) == filter)
                {
                    _SubFormations.Add(new PBST
                    {
                        rec = i,
                        SETL = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PBST"), TDB.StrReverse("SETL"), i),
                        PBFM = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PBST"), TDB.StrReverse("PBFM"), i),
                        pbst = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PBST"), TDB.StrReverse("PBST"), i),
                        SPF_ = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PBST"), TDB.StrReverse("SPF_"), i),
                        ord_ = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PBST"), TDB.StrReverse("ord_"), i),
                        name = _name
                    });
                }
                else if (filter == 0)
                {
                    _SubFormations.Add(new PBST
                    {
                        rec = i,
                        SETL = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PBST"), TDB.StrReverse("SETL"), i),
                        PBFM = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PBST"), TDB.StrReverse("PBFM"), i),
                        pbst = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PBST"), TDB.StrReverse("PBST"), i),
                        SPF_ = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PBST"), TDB.StrReverse("SPF_"), i),
                        ord_ = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PBST"), TDB.StrReverse("ord_"), i),
                        name = _name
                    });
                }
            }

            //_SubFormations = _SubFormations.ToList().OrderBy(s => s.PBFM).ThenBy(s => s.ord_).Cast<PBST>().ToList();

            return _SubFormations;
        }

        public static void SetPBST(List<PBST> PBST, int DBIndex = 0)
        {
            TdbTableProperties TableProps = new TdbTableProperties();
            TableProps.Name = new string((char)0, 5);

            // Get Tableprops based on the selected index
            TDB.TDBTableGetProperties(DBIndex, TableNames.GetTables().Find(item => item.name == "PBST").rec, ref TableProps);

            if (PBST.Count > TableProps.RecordCount)
                for (int i = TableProps.RecordCount - 1; i < PBST.Count - 1; i++)
                {
                    TDB.TDBTableRecordAdd(DBIndex, TDB.StrReverse("PBST"), true);
                }
            else if (PBST.Count < TableProps.RecordCount)
            {
                for (int i = TableProps.RecordCount - 1; i > PBST.Count - 1; i--)
                {
                    TDB.TDBTableRecordRemove(DBIndex, TDB.StrReverse("PBST"), i);
                }
            }

            foreach (PBST item in PBST)
            {
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PBST"), TDB.StrReverse("SETL"), item.rec, item.SETL);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PBST"), TDB.StrReverse("PBFM"), item.rec, item.PBFM);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PBST"), TDB.StrReverse("PBST"), item.rec, item.pbst);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PBST"), TDB.StrReverse("SPF_"), item.rec, item.SPF_);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PBST"), TDB.StrReverse("ord_"), item.rec, item.ord_);
                TDB.TDBFieldSetValueAsString(DBIndex, TDB.StrReverse("PBST"), TDB.StrReverse("name"), item.rec, item.name);
            }
        }

        public static List<PBST> Sort(List<PBST> PBST)
        {
            return PBST.OrderBy(s => s.pbst).Cast<PBST>().ToList();
        }
    }
}
