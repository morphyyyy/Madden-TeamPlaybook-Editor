using MaddenTeamPlaybookEditor.User_Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using TDBAccess;

namespace Madden.Team
{
    [Serializable]
    public class DCHT
    {
        public int rec { get; set; }
        public int PGID { get; set; }
        public int TGID { get; set; }
        public int PPOS { get; set; }
        public int ddep { get; set; }

        public override string ToString()
        {
            return
                "Rec#: " + rec + "\t" +
                "   PGID: " + PGID + "\t" +
                "   TGID: " + TGID + "\t" +
                "   PGID: " + PPOS + "\t" +
                "   PGID: " + ddep;
        }

        public static List<DCHT> GetDCHT(int filter = 0, int DBIndex = 0)
        {
            List<DCHT> _DCHT = new List<DCHT>();

            TdbTableProperties TableProps = new TdbTableProperties();
            TableProps.Name = new string((char)0, 5);

            // Get Tableprops based on the selected index
            if (!TDB.TDBTableGetProperties(DBIndex, TableNames.GetTables(DBIndex: DBIndex).Find(item => item.name == "DCHT").rec, ref TableProps))
                return null;

            List<TdbFieldProperties> tableFields = new List<TdbFieldProperties>();

            for (int i = 0; i < TableProps.FieldCount; i++)
            {
                TdbFieldProperties FieldProps = new TdbFieldProperties();
                FieldProps.Name = new string((char)0, 5);
                TDB.TDBFieldGetProperties(DBIndex, TDB.StrReverse("DCHT"), i, ref FieldProps);
                tableFields.Add(FieldProps);
            }

            for (int i = 0; i < TableProps.RecordCount; i++)
            {
                _DCHT.Add(new DCHT
                {
                    rec = i,
                    PGID = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("DCHT"), TDB.StrReverse("PGID"), i),
                    TGID = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("DCHT"), TDB.StrReverse("TGID"), i),
                    PPOS = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("DCHT"), TDB.StrReverse("PPOS"), i),
                    ddep = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("DCHT"), TDB.StrReverse("ddep"), i),
                });
            }

            return _DCHT;
        }
    }
}
