using System;
using System.Collections.Generic;
using System.Linq;
using TDBAccess;

namespace Madden.TeamPlaybook
{
    [Serializable]
    public class PBPL
    {
        public int rec { get; set; }
        public int PKGE { get; set; }
        public int COMF { get; set; }
        /// <summary>
        /// PBAI.pbpl, PBAU.pbpl, PBCC.pbpl
        /// </summary>
        public int pbpl { get; set; }
        /// <summary>
        /// PLYL.plyl
        /// </summary>
        public int PLYL { get; set; }
        /// <summary>
        /// PBST.pbst
        /// </summary>
        public int PBST { get; set; }
        public int ord_ { get; set; }
        /// <summary>
        /// PLYL.name
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// PBAI.Flag
        /// </summary>
        public int Flag { get; set; }

        public override string ToString()
        {
            return
                "Rec#: " + rec +
                "   PKGE: " + PKGE +
                "   COMF: " + COMF +
                "   pbpl: " + pbpl +
                "   PLYL: " + PLYL +
                "   PBST: " + PBST +
                "   ord_: " + ord_ +
                "   name: " + name +
                "   Flag: " + Flag;
        }

        public static List<PBPL> GetPBPL(int filter = 0, int DBIndex = 0)
        {
            List<PBPL> _PBPL = new List<PBPL>();

            TdbTableProperties TableProps = new TdbTableProperties();
            TableProps.Name = new string((char)0, 5);

            // Get Tableprops based on the selected index
            if (!TDB.TDBTableGetProperties(DBIndex, TableNames.GetTables().Find(item => item.name == "PBPL").rec, ref TableProps))
                return null;

            List<TdbFieldProperties> tableFields = new List<TdbFieldProperties>();

            for (int i = 0; i < TableProps.FieldCount; i++)
            {
                TdbFieldProperties FieldProps = new TdbFieldProperties();
                FieldProps.Name = new string((char)0, 5);
                TDB.TDBFieldGetProperties(DBIndex, TDB.StrReverse("PBPL"), i, ref FieldProps);
                tableFields.Add(FieldProps);
            }

            for (int i = 0; i < TableProps.RecordCount; i++)
            {
                string _name = new string((char)0, (tableFields.FirstOrDefault(field => field.Name == TDB.StrReverse("name")).Size / 8) + 1);

                TDB.TDBFieldGetValueAsString(DBIndex, TDB.StrReverse("PBPL"), TDB.StrReverse("name"), i, ref _name);
                _name = _name.Replace(",", "");

                if (filter != 0 && (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PBPL"), TDB.StrReverse("PBST"), i) == filter)
                {
                    _PBPL.Add(new PBPL
                    {
                        rec = i,
                        PKGE = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PBPL"), TDB.StrReverse("PKGE"), i),
                        COMF = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PBPL"), TDB.StrReverse("COMF"), i),
                        pbpl = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PBPL"), TDB.StrReverse("PBPL"), i),
                        PLYL = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PBPL"), TDB.StrReverse("PLYL"), i),
                        PBST = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PBPL"), TDB.StrReverse("PBST"), i),
                        ord_ = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PBPL"), TDB.StrReverse("ord_"), i),
                        name = _name,
                        Flag = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PBPL"), TDB.StrReverse("Flag"), i)
                    });
                }
                else if (filter == 0)
                {
                    _PBPL.Add(new PBPL
                    {
                        rec = i,
                        PKGE = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PBPL"), TDB.StrReverse("PKGE"), i),
                        COMF = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PBPL"), TDB.StrReverse("COMF"), i),
                        pbpl = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PBPL"), TDB.StrReverse("PBPL"), i),
                        PLYL = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PBPL"), TDB.StrReverse("PLYL"), i),
                        PBST = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PBPL"), TDB.StrReverse("PBST"), i),
                        ord_ = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PBPL"), TDB.StrReverse("ord_"), i),
                        name = _name,
                        Flag = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PBPL"), TDB.StrReverse("Flag"), i)
                    });
                }
            }

            //_PBPL = _PBPL.ToList().OrderBy(s => s.PBST).ThenBy(s => s.ord_).Cast<PBPL>().ToList();
            //for (int i = 0; i > _PBPL.Count; i++)
            //{
            //    _PBPL[i].rec = i;
            //}

            return _PBPL;
        }

        public static void SetPBPL(List<PBPL> PBPL, int DBIndex = 0)
        {
            TdbTableProperties TableProps = new TdbTableProperties();
            TableProps.Name = new string((char)0, 5);

            // Get Tableprops based on the selected index
            TDB.TDBTableGetProperties(DBIndex, TableNames.GetTables().Find(item => item.name == "PBPL").rec, ref TableProps);

            if (PBPL.Count > TableProps.RecordCount)
                for (int i = TableProps.RecordCount - 1; i < PBPL.Count - 1; i++)
                {
                    TDB.TDBTableRecordAdd(DBIndex, TDB.StrReverse("PBPL"), true);
                }
            else if (PBPL.Count < TableProps.RecordCount)
            {
                for (int i = TableProps.RecordCount - 1; i > PBPL.Count - 1; i--)
                {
                    TDB.TDBTableRecordRemove(DBIndex, TDB.StrReverse("PBPL"), i);
                }
            }

            foreach (PBPL item in PBPL)
            {
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PBPL"), TDB.StrReverse("PKGE"), item.rec, item.PKGE);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PBPL"), TDB.StrReverse("COMF"), item.rec, item.COMF);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PBPL"), TDB.StrReverse("PBPL"), item.rec, item.pbpl);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PBPL"), TDB.StrReverse("PLYL"), item.rec, item.PLYL);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PBPL"), TDB.StrReverse("PBST"), item.rec, item.PBST);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PBPL"), TDB.StrReverse("ord_"), item.rec, item.ord_);
                TDB.TDBFieldSetValueAsString(DBIndex, TDB.StrReverse("PBPL"), TDB.StrReverse("name"), item.rec, item.name);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PBPL"), TDB.StrReverse("Flag"), item.rec, item.Flag);
            }
        }

        public static List<PBPL> Sort(List<PBPL> PBPL)
        {
            return PBPL.OrderBy(s => s.PBST).ThenBy(s => s.ord_).Cast<PBPL>().ToList();
        }
    }
}
