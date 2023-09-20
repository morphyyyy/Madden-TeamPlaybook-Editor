using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using TDBAccess;
using Madden.CustomPlaybook;
using System.Windows;

namespace Madden20CustomPlaybookEditor.ViewModels
{
    [Serializable]
    public class CustomPlaybook : INotifyPropertyChanged
    {
        public override string ToString()
        {
            return
                Path.GetFileNameWithoutExtension(filePath);
        }

        public static readonly Dictionary<int, string> Tables = new Dictionary<int, string>
        {
            {0, "SRFT"},
            {1, "SETP"},
            {2, "SPKG"},
            {3, "SPKF"},
            {4, "STLS"},
            {5, "STSP"},
            {6, "STID"},
            {7, "SLEP"},
            {8, "SETG"},
            {9, "SGFF"},
            {10, "SDEF"},
            {11, "PSLO"},
            {12, "PSLD"},
            {13, "PLYS"},
            {14, "PLRD"},
            {15, "PLPD"},
            {16, "SETL"},
            {17, "PBPL"},
            {18, "PIDX"},
            {19, "PBFM"},
            {20, "PBFI"},
            {21, "PBAU"},
            {22, "PGPL"},
            {23, "PBPN"},
            {24, "PPCT"},
            {25, "CPFM"},
            {26, "CPFT"},
            {27, "PGFM"},
            {28, "PLCM"},
            {29, "BSMO"},
            {30, "ARTO"},
            {31, "ARTD"},
            {32, "PBAI"},
            {33, "BSMP"}
        };

        public static readonly Dictionary<int, string> Positions = new Dictionary<int, string>
        {
            {0, "QB"},
            {1, "HB"},
            {2, "FB"},
            {3, "WR"},
            {4, "TE"},
            {5, "LT"},
            {6, "LG"},
            {7, "C"},
            {8, "RG"},
            {9, "RT"},
            {10, "LE"},
            {11, "RE"},
            {12, "DT"},
            {13, "LOLB"},
            {14, "MLB"},
            {15, "ROLB"},
            {16, "CB"},
            {17, "FS"},
            {18, "SS"},
            {19, "K"},
            {20, "P"},
            {21, "KR"},
            {22, "PR"},
            {23, "KOS"},
            {24, "LS"},
            {25, "3RB"},
            {26, "PRB"},
            {27, "SWR"},
            {28, "RLE"},
            {29, "RRE"},
            {30, "RDT"},
            {31, "SLB"},
            {32, "SCB"}
        };

        public static readonly Dictionary<int, string> PlayType = new Dictionary<int, string>
        {
            {4, "Play Action"},
            {5, "WR/HB Slip Screen"},
            {6, "Jailbreak Screen"},
            {7, "Rollout Smash"},
            {11, "QB Power"},
            {13," Counter"},
            {14, "Draw"},
            {15, "Toss"},
            {16, "Reverse"},
            {17, "Power Option"},
            {19, "QB Sneak"},
            {21, "Field Goal"},
            {22, "Punt"},
            {23," Kickoff"},
            {24, "Punt"},
            {25, "Onside Kick"},
            {37, "QB Kneel"},
            {38, "Spike Ball"},
            {41, "Cover 2/Tampa 2"},
            {42, "Cover 2 Flat"},
            {43, "Cover 3 Match"},
            {44, "Cover 4 Palms"},
            {45, "Cover 4 Quarters"},
            {46, "Goalline"},
            {47, "Engage Eight"},
            {48, "Blitz"},
            {100, "Bubble Screen/Fake Screen Go"},
            {101, "Quick Pass"},
            {102, "Deep Pass"},
            {103, "Pass"},
            {104, "Hail Mary"},
            {105, "Fake FG"},
            {106, "Fake Punt"},
            {107, "Fake Spike"},
            {151, "Dive"},
            {152, "Buck Sweep"},
            {153, "FB Dive"},
            {154, "Fake FG"},
            {157, "Trick Play"},
            {159, "Pass"},
            {161, "Zone Fake Jet"},
            {163, "Triple Option"},
            {165, "Shovel Option"},
            {169, "Read Option"},
            {174, "Cover 0 Blitz"},
            {175, "Cover 1 Blitz"},
            {176, "Cover 2 Blitz"},
            {177, "Cover 3 Blitz"},
            {178, "Cover 1"},
            {179, "Cover 1"},
            {180, "Cover 1"},
            {182, "Cover 2 Man"},
            {183, "Quarters Deep"},
            {185, "Cover 2 /Tampa 2"},
            {186, "Cover 2 Invert"},
            {187, "Cover 2 Hard Flat"},
            {188, "Cover 3 Sky/Flat"},
            {189, "Cover 3 Buzz Press"},
            {191,  "Cover 3 Sky"},
            {192, "Cover 6/9"},
            {194, "Cover 4 Drop"},
            {195, "Inside Zone/HB Blast"},
            {196, "Outside Zone/Off Tackle"},
            {197, "Power O/HB Base"},
            {198, "Iso"},
            {199, "Prevent"},
            {200, "Buck Sweep"},
            {201, "Trap"},
            {203, "End Around"},
            {204, "RPO Read"},
            {205, "RPO Peek"},
            {207, "RPO Lookie/Alert"},
            {208, "Jet Pass"},
            {209, "Jet Sweep"}
       };

        public string filePath { get; set; }

        public ObservableCollection<FormationVM> Formations { get; set; }

        public static Point LOS { get { return new Point(266.5, 400); } }

        public List<TableNames> TableNames { get; set; }
        public List<Madden.CustomPlaybook.ARTL> ARTO { get; set; }
        public List<Madden.CustomPlaybook.ARTL> ARTD { get; set; }
        public List<Madden.CustomPlaybook.CPFM> CPFM { get; set; }
        public List<Madden.CustomPlaybook.PBFI> PBFI { get; set; }
        public List<Madden.CustomPlaybook.PBFM> PBFM { get; set; }
        public List<Madden.CustomPlaybook.PBPL> PBPL { get; set; }
        public List<Madden.CustomPlaybook.PBPN> PBPN { get; set; }
        public List<Madden.CustomPlaybook.PIDX> PIDX { get; set; }
        public List<Madden.CustomPlaybook.PLCM> PLCM { get; set; }
        public List<Madden.CustomPlaybook.PLPD> PLPD { get; set; }
        public List<Madden.CustomPlaybook.PLRD> PLRD { get; set; }
        public List<Madden.CustomPlaybook.PLYS> PLYS { get; set; }
        public List<Madden.CustomPlaybook.PPCT> PPCT { get; set; }
        public List<Madden.CustomPlaybook.PSAL> PSLO { get; set; }
        public List<Madden.CustomPlaybook.PSAL> PSLD { get; set; }
        public List<Madden.CustomPlaybook.SDEF> SDEF { get; set; }
        public List<Madden.CustomPlaybook.SETG> SETG { get; set; }
        public List<Madden.CustomPlaybook.SETL> SETL { get; set; }
        public List<Madden.CustomPlaybook.SETP> SETP { get; set; }
        public List<Madden.CustomPlaybook.SGFF> SGFF { get; set; }
        public List<Madden.CustomPlaybook.SPKF> SPKF { get; set; }
        public List<Madden.CustomPlaybook.SPKG> SPKG { get; set; }
        public List<Madden.CustomPlaybook.SRFT> SRFT { get; set; }
        public List<Madden.CustomPlaybook.STID> STID { get; set; }

        public CustomPlaybook(string filepath)
        {
            filePath = filepath;
            GetTables();
            BuildPlaybook();
        }

        public void GetTables()
        {
            ARTO = Madden.CustomPlaybook.ARTL.GetARTL("ARTO");
            ARTD = Madden.CustomPlaybook.ARTL.GetARTL("ARTD");
            CPFM = Madden.CustomPlaybook.CPFM.Sort(Madden.CustomPlaybook.CPFM.GetCPFM());
            PBFI = Madden.CustomPlaybook.PBFI.GetPBFI();
            PBFM = Madden.CustomPlaybook.PBFM.GetPBFM();
            PBPL = Madden.CustomPlaybook.PBPL.GetPBPL();
            PBPN = Madden.CustomPlaybook.PBPN.GetPBPN();
            PIDX = Madden.CustomPlaybook.PIDX.GetPIDX();
            PLCM = Madden.CustomPlaybook.PLCM.GetPLCM();
            PLPD = Madden.CustomPlaybook.PLPD.GetPLPD();
            PLRD = Madden.CustomPlaybook.PLRD.GetPLRD();
            PLYS = Madden.CustomPlaybook.PLYS.GetPLYS();
            PPCT = Madden.CustomPlaybook.PPCT.GetPPCT();
            PSLO = Madden.CustomPlaybook.PSAL.GetPSAL("PSLO");
            PSLD = Madden.CustomPlaybook.PSAL.GetPSAL("PSLD");
            SDEF = Madden.CustomPlaybook.SDEF.GetSDEF();
            SETG = Madden.CustomPlaybook.SETG.GetSETG();
            SETL = Madden.CustomPlaybook.SETL.GetSETL();
            SETP = Madden.CustomPlaybook.SETP.GetSETP();
            SGFF = Madden.CustomPlaybook.SGFF.GetSGFF();
            SPKF = Madden.CustomPlaybook.SPKF.GetSPKF();
            SPKG = Madden.CustomPlaybook.SPKG.GetSPKG();
            SRFT = Madden.CustomPlaybook.SRFT.GetSRFT();
            STID = Madden.CustomPlaybook.STID.GetSTID();
        }

        public void BuildPlaybook()
        {
            if (Formations == null)
            {
                Formations = new ObservableCollection<FormationVM>();
            }
            else
            {
                Formations.Clear();
            }

            foreach (Madden.CustomPlaybook.CPFM formation in CPFM)
            {
                Formations.Add(new FormationVM(formation, this));
            }
        }

        #region IsUsing

        public bool IsUsing(PSAL psal)
        {
            bool Using = false;
            foreach (Madden.CustomPlaybook.PLYS assignment in PLYS)
            {
                if (assignment.PSAL == psal.psal)
                {
                    Using = true;
                    break;
                }
            }
            return Using;
        }

        public bool IsUsing(Madden.TeamPlaybook.ARTL artl)
        {
            bool Using = false;
            foreach (Madden.CustomPlaybook.PLYS assignment in PLYS)
            {
                if (assignment.ARTL == artl.artl)
                {
                    Using = true;
                    break;
                }
            }
            return Using;
        }

        #endregion

        #region INotifyPropertyChanged Members

        [field: NonSerializedAttribute()]
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion // INotifyPropertyChanged Members
    }
}
