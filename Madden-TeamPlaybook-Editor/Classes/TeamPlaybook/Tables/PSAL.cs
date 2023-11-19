using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using TDBAccess;

namespace Madden.TeamPlaybook
{
    [Serializable]
    public class PSAL : INotifyPropertyChanged
    {
        private int _rec { get; set; }
        public int rec { get { return _rec; } set { if (_rec == value) return; _rec = value; OnPropertyChanged("rec"); } }
        private int _val1 { get; set; }
        public int val1 { get { return _val1; } set { if (_val1 == value) return; _val1 = value; OnPropertyChanged("rec"); } }
        private int _val2 { get; set; }
        public int val2 { get { return _val2; } set { if (_val2 == value) return; _val2 = value; OnPropertyChanged("rec"); } }
        private int _val3 { get; set; }
        public int val3 { get { return _val3; } set { if (_val3 == value) return; _val3 = value; OnPropertyChanged("rec"); } }
        private int _psal { get; set; }
        public int psal { get { return _psal; } set { if (_psal == value) return; _psal = value; OnPropertyChanged("rec"); } }
        private int _code { get; set; }
        public int code { get { return _code; } set { if (_code == value) return; _code = value; OnPropertyChanged("rec"); } }
        private int _step { get; set; }
        public int step { get { return _step; } set { if (_step == value) return; _step = value; OnPropertyChanged("rec"); } }
        public static double AngleRatio = 0.35556;

        public override string ToString()
        {
            return
                "Rec#: " + rec +
                "   val1: " + val1 +
                "   val2: " + val2 +
                "   val3: " + val3 +
                "   psal: " + psal +
                "   code: " + code +
                "   step: " + step;
        }

        public static List<PSAL> GetPSAL(int filter = 0, int DBIndex = 0)
        {
            List<PSAL> _PSAL = new List<PSAL>();

            TdbTableProperties TableProps = new TdbTableProperties();
            TableProps.Name = new string((char)0, 5);

            // Get Tableprops based on the selected index
            if (!TDB.TDBTableGetProperties(DBIndex, TableNames.GetTables().Find(item => item.name == "PSAL").rec, ref TableProps))
                return null;

            for (int i = 0; i < TableProps.RecordCount; i++)
            {
                if (filter != 0 && (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PSAL"), TDB.StrReverse("PSAL"), i) == filter)
                {
                    _PSAL.Add(new PSAL
                    {
                        rec = i,
                        val1 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PSAL"), TDB.StrReverse("val1"), i),
                        val2 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PSAL"), TDB.StrReverse("val2"), i),
                        val3 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PSAL"), TDB.StrReverse("val3"), i),
                        psal = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PSAL"), TDB.StrReverse("PSAL"), i),
                        code = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PSAL"), TDB.StrReverse("code"), i),
                        step = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PSAL"), TDB.StrReverse("step"), i)
                    });
                }
                else if (filter == 0)
                {
                    _PSAL.Add(new PSAL
                    {
                        rec = i,
                        val1 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PSAL"), TDB.StrReverse("val1"), i),
                        val2 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PSAL"), TDB.StrReverse("val2"), i),
                        val3 = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PSAL"), TDB.StrReverse("val3"), i),
                        psal = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PSAL"), TDB.StrReverse("PSAL"), i),
                        code = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PSAL"), TDB.StrReverse("code"), i),
                        step = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PSAL"), TDB.StrReverse("step"), i)
                    });
                }
            }

            //_PSAL = _PSAL.OrderBy(psal => psal.psal).ThenBy(psal => psal.step).ToList();
            return _PSAL;
        }

        public static void SetPSAL(List<PSAL> PSAL, int DBIndex = 0)
        {
            TdbTableProperties TableProps = new TdbTableProperties();
            TableProps.Name = new string((char)0, 5);

            // Get Tableprops based on the selected index
            TDB.TDBTableGetProperties(DBIndex, TableNames.GetTables().Find(item => item.name == "PSAL").rec, ref TableProps);

            if (PSAL.Count > TableProps.RecordCount)
                for (int i = TableProps.RecordCount - 1; i < PSAL.Count - 1; i++)
                {
                    TDB.TDBTableRecordAdd(DBIndex, TDB.StrReverse("PSAL"), true);
                }
            else if (PSAL.Count < TableProps.RecordCount)
            {
                for (int i = TableProps.RecordCount - 1; i > PSAL.Count - 1; i--)
                {
                    TDB.TDBTableRecordRemove(DBIndex, TDB.StrReverse("PSAL"), i);
                }
            }

            for (int i = 0; i < PSAL.Count; i++)
            {
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PSAL"), TDB.StrReverse("val1"), PSAL[i].rec, PSAL[i].val1);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PSAL"), TDB.StrReverse("val2"), PSAL[i].rec, PSAL[i].val2);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PSAL"), TDB.StrReverse("val3"), PSAL[i].rec, PSAL[i].val3);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PSAL"), TDB.StrReverse("PSAL"), PSAL[i].rec, PSAL[i].psal);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PSAL"), TDB.StrReverse("code"), PSAL[i].rec, PSAL[i].code);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("PSAL"), TDB.StrReverse("step"), PSAL[i].rec, PSAL[i].step);
            }
        }

        public static List<PSAL> Sort(List<PSAL> PSAL)
        {
            return PSAL.OrderBy(s => s.psal).ThenBy(s => s.step).Cast<PSAL>().ToList();
        }

        public enum DoesPSALExist { No = 0, Yes = 1, IsIdentical = 2 }

        public bool IsIdentical(PSAL _psal)
        {
            return Convert.ToBoolean(
                ((psal == _psal.psal) ? 1 : 0) * 
                ((code == _psal.code) ? 1 : 0) * 
                ((step == _psal.step) ? 1 : 0) * 
                ((val1 == _psal.val1) ? 1 : 0) * 
                ((val2 == _psal.val2) ? 1 : 0) * 
                ((val3 == _psal.val3) ? 1 : 0)
                );
        }

        public bool IsIdentical(MaddenCustomPlaybookEditor.PSAL _psal)
        {
            return Convert.ToBoolean(
                ((psal == _psal.psal) ? 1 : 0) *
                ((code == _psal.code) ? 1 : 0) *
                ((step == _psal.step) ? 1 : 0) *
                ((val1 == _psal.val1) ? 1 : 0) *
                ((val2 == _psal.val2) ? 1 : 0) *
                ((val3 == _psal.val3) ? 1 : 0)
                );
        }

        #region INotifyPropertyChanged Members

        [field: NonSerializedAttribute()]
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion // INotifyPropertyChanged Members

    }
}
