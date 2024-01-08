using MaddenTeamPlaybookEditor.User_Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using TDBAccess;

namespace Madden.Team
{
    [Serializable]
    public class PLAY
    {
        public int rec { get; set; }
        public int PJEN { get; set; }
        public string PFNA { get; set; }
        public string PLNA { get; set; }
        public int PGID { get; set; }

        public override string ToString()
        {
            return
                "Rec#: " + rec + "\t" +
                "   PJEN: " + PJEN + "\t" +
                "   Name: " + PFNA + " " + PLNA + "\t" +
                "   PGID: " + PGID;
        }

        public static List<PLAY> GetPLAY(int filter = 0, int DBIndex = 0)
        {
            List<PLAY> _TEAM = new List<PLAY>();

            TdbTableProperties TableProps = new TdbTableProperties();
            TableProps.Name = new string((char)0, 5);

            // Get Tableprops based on the selected index
            if (!TDB.TDBTableGetProperties(DBIndex, TableNames.GetTables(DBIndex: DBIndex).Find(item => item.name == "PLAY").rec, ref TableProps))
                return null;

            List<TdbFieldProperties> tableFields = new List<TdbFieldProperties>();

            for (int i = 0; i < TableProps.FieldCount; i++)
            {
                TdbFieldProperties FieldProps = new TdbFieldProperties();
                FieldProps.Name = new string((char)0, 5);
                TDB.TDBFieldGetProperties(DBIndex, TDB.StrReverse("PLAY"), i, ref FieldProps);
                tableFields.Add(FieldProps);
            }

            for (int i = 0; i < TableProps.RecordCount; i++)
            {
                string firstName = new string((char)0, (tableFields.FirstOrDefault(field => field.Name == TDB.StrReverse("PLAY")).Size / 8) + 1);
                string lastName = new string((char)0, (tableFields.FirstOrDefault(field => field.Name == TDB.StrReverse("PLAY")).Size / 8) + 1);

                TDB.TDBFieldGetValueAsString(DBIndex, TDB.StrReverse("PLAY"), TDB.StrReverse("PFNA"), i, ref firstName);
                TDB.TDBFieldGetValueAsString(DBIndex, TDB.StrReverse("PLAY"), TDB.StrReverse("PLNA"), i, ref lastName);

                _TEAM.Add(new PLAY
                {
                    rec = i,
                    PJEN = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLAY"), TDB.StrReverse("PJEN"), i),
                    PFNA = firstName,
                    PLNA = lastName,
                    PGID = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLAY"), TDB.StrReverse("PGID"), i),
                });
            }

            return _TEAM;
        }
    }
}
