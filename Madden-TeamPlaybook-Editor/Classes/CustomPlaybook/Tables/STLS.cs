using System;
using System.Collections.Generic;
using System.Linq;
using TDBAccess;

namespace Madden.CustomPlaybook
{
    [Serializable]
    public class STLS
    {
        public int rec { get; set; }
        public int SETL { get; set; }

        public override string ToString()
        {
            return
                "Rec#: " + rec +
                "   SETL: " + SETL;
        }

        public static List<STLS> GetSTLS(int filter = 0, int DBIndex = 0)
        {
            List<STLS> _STLS = new List<STLS>();

            TdbTableProperties TableProps = new TdbTableProperties();
            TableProps.Name = new string((char)0, 5);

            // Get Tableprops based on the selected index
            if (!TDB.TDBTableGetProperties(DBIndex, TableNames.GetTables().Find(item => item.name == "STLS").rec, ref TableProps))
                return null;

            for (int i = 0; i < TableProps.RecordCount; i++)
            {
                if (filter != 0 && (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("STLS"), TDB.StrReverse("PLYL"), i) == filter)
                {
                    _STLS.Add(new STLS
                    {
                        rec = i,
                        SETL = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("STLS"), TDB.StrReverse("SETL"), i)
                    });
                }
                else if (filter == 0)
                {
                    _STLS.Add(new STLS
                    {
                        rec = i,
                        SETL = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("STLS"), TDB.StrReverse("SETL"), i)
                    });
                }
            }
            return _STLS;
        }

        public static void SetSTLS(List<STLS> STLS, int DBIndex = 0)
        {
            TdbTableProperties TableProps = new TdbTableProperties();
            TableProps.Name = new string((char)0, 5);

            // Get Tableprops based on the selected index
            TDB.TDBTableGetProperties(DBIndex, TableNames.GetTables().Find(item => item.name == "STLS").rec, ref TableProps);

            if (STLS.Count > TableProps.RecordCount)
            {
                for (int i = TableProps.RecordCount - 1; i < STLS.Count - 1; i++)
                {
                    TDB.TDBTableRecordAdd(DBIndex, TDB.StrReverse("STLS"), true);
                }
            }
            else if (STLS.Count < TableProps.RecordCount)
            {
                for (int i = TableProps.RecordCount - 1; i > STLS.Count - 1; i--)
                {
                    TDB.TDBTableRecordRemove(DBIndex, TDB.StrReverse("STLS"), i);
                }
            }

            foreach (STLS item in STLS)
            {
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("STLS"), TDB.StrReverse("SETL"), item.rec, item.SETL);
            }
        }

        public static List<STLS> Sort(List<STLS> STLS)
        {
            return STLS.OrderBy(s => s.SETL).Cast<STLS>().ToList();
        }
    }
}
