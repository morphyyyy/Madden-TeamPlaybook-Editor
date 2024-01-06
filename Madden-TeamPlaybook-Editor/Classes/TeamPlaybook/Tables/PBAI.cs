using MaddenTeamPlaybookEditor.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using TDBAccess;

namespace Madden.TeamPlaybook
{
    [Serializable]
    public class PBAI
    {
        public int rec { get; set; }
        public int PBPL { get; set; }
        public int SETL { get; set; }
        public int AIGR { get; set; }
        public int PLYT { get; set; }
        public int PLF_ { get; set; }
        public int Flag { get; set; }
        public int vpos { get; set; }
        public int prct { get; set; }
        public string Name
        {
            get
            {
                return MaddenTeamPlaybookEditor.ViewModels.TeamPlaybook.Gameplan.Offense.Contains(PLYT) ?
                    MaddenTeamPlaybookEditor.ViewModels.TeamPlaybook.SituationOff.FirstOrDefault(x => x.Key == AIGR).Value :
                    MaddenTeamPlaybookEditor.ViewModels.TeamPlaybook.SituationDef.FirstOrDefault(x => x.Key == AIGR).Value;
            }
        }
        public string Type
        { 
            get
            {
                return MaddenTeamPlaybookEditor.ViewModels.TeamPlaybook.PlayType.FirstOrDefault(x => x.Key == PLYT).Value;
            }
        }

        public override string ToString()
        {
            return
                "Rec#: " + rec +
                "   PBPL: " + PBPL +
                "   SETL: " + SETL +
                "   AIGR: " + AIGR +
                "   PLYT: " + PLYT +
                "   PLF_: " + PLF_ +
                "   Flag: " + Flag +
                "   vpos: " + vpos +
                "   prct: " + prct;
        }

        public static List<PBAI> GetPBAI(int filter = 0, int DBIndex = 0)
        {
            List<PBAI> _PBAI = new List<PBAI>();

            TdbTableProperties TableProps = new TdbTableProperties();
            TableProps.Name = new string((char)0, 5);

            // Get Tableprops based on the selected index
            if (!TDB.TDBTableGetProperties(DBIndex, TableNames.GetTables().Find(item => item.name == "PBAI").rec, ref TableProps))
                return null;

            for (int i = 0; i < TableProps.RecordCount; i++)
            {
                if (filter != 0 && (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PBAI"), TDB.StrReverse("PBPL"), i) == filter)
                {
                    _PBAI.Add(new PBAI
                    {
                        rec = i,
                        PBPL = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PBAI"), TDB.StrReverse("PBPL"), i),
                        SETL = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PBAI"), TDB.StrReverse("SETL"), i),
                        AIGR = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PBAI"), TDB.StrReverse("AIGR"), i),
                        PLYT = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PBAI"), TDB.StrReverse("PLYT"), i),
                        PLF_ = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PBAI"), TDB.StrReverse("PLF_"), i),
                        Flag = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PBAI"), TDB.StrReverse("Flag"), i),
                        vpos = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PBAI"), TDB.StrReverse("vpos"), i),
                        prct = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PBAI"), TDB.StrReverse("prct"), i)
                    });
                }
                else if (filter == 0)
                {
                    _PBAI.Add(new PBAI
                    {
                        rec = i,
                        PBPL = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PBAI"), TDB.StrReverse("PBPL"), i),
                        SETL = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PBAI"), TDB.StrReverse("SETL"), i),
                        AIGR = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PBAI"), TDB.StrReverse("AIGR"), i),
                        PLYT = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PBAI"), TDB.StrReverse("PLYT"), i),
                        PLF_ = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PBAI"), TDB.StrReverse("PLF_"), i),
                        Flag = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PBAI"), TDB.StrReverse("Flag"), i),
                        vpos = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PBAI"), TDB.StrReverse("vpos"), i),
                        prct = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PBAI"), TDB.StrReverse("prct"), i)
                    });
                }
            }
            return _PBAI;
        }

        public static void SetPBAI(List<PBAI> PBAI, int DBIndex = 0)
        {
            TdbTableProperties TableProps = new TdbTableProperties();
            TableProps.Name = new string((char)0, 5);

            // Get Tableprops based on the selected index
            TDB.TDBTableGetProperties(DBIndex, TableNames.GetTables().Find(item => item.name == "PBAI").rec, ref TableProps);

            if (PBAI.Count > TableProps.RecordCount)
                for (int i = TableProps.RecordCount - 1; i < PBAI.Count - 1; i++)
                {
                    TDB.TDBTableRecordAdd(DBIndex, TDB.StrReverse("PBAI"), true);
                }
            else if (PBAI.Count < TableProps.RecordCount)
            {
                for (int i = TableProps.RecordCount - 1; i > PBAI.Count - 1; i--)
                {
                    TDB.TDBTableRecordRemove(DBIndex, TDB.StrReverse("PBAI"), i);
                }
            }

            foreach (PBAI item in PBAI)
            {
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PBAI"), TDB.StrReverse("PBPL"), item.rec, item.PBPL);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PBAI"), TDB.StrReverse("SETL"), item.rec, item.SETL);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PBAI"), TDB.StrReverse("AIGR"), item.rec, item.AIGR);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PBAI"), TDB.StrReverse("PLYT"), item.rec, item.PLYT);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PBAI"), TDB.StrReverse("PLF_"), item.rec, item.PLF_);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PBAI"), TDB.StrReverse("Flag"), item.rec, item.Flag);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PBAI"), TDB.StrReverse("vpos"), item.rec, item.vpos);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PBAI"), TDB.StrReverse("prct"), item.rec, item.prct);
            }
        }

        public static List<PBAI> Sort(List<PBAI> PBAI)
        {
            List<PBAI> _PBAI = PBAI.OrderByDescending(s => s.prct).ThenBy(s => s.PBPL).Cast<PBAI>().ToList();
            for (int i = 0; i < _PBAI.Count(); i++)
            {
                _PBAI[i].rec = i;
            }
            return _PBAI;
        }
    }
}
