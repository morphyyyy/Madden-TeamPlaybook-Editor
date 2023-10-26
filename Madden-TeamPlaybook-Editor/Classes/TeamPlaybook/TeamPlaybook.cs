using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using TDBAccess;
using Madden.TeamPlaybook;
using Madden.Team;
using System.Windows;
using System.Reflection;
using System.Windows.Media;
using Madden.CustomPlaybook;

namespace MaddenTeamPlaybookEditor.ViewModels
{
    [Serializable]
    public class TeamPlaybook : INotifyPropertyChanged
    {
        public override string ToString()
        {
            return
                Path.GetFileNameWithoutExtension(filePath);
        }

        public static readonly Dictionary<int, string> Tables = new Dictionary<int, string>
        {
            {0, "SETP"},
            {1, "SPKG"},
            {2, "SGFM"},
            {3, "PLYS"},
            {4, "PLPD"},
            {5, "PBPL"},
            {6, "PPCT"},
            {7, "PBAI"},
            {8, "PLYL"},
            {9, "SRFT"},
            {10, "SPKF"},
            {11, "SETG"},
            {12, "SDEF"},
            {13, "SETL"},
            {14, "PSAL"},
            {15, "PLRD"},
            {16, "PBST"},
            {17, "PBFM"},
            {18, "PBCC"},
            {19, "PBAU"},
            {20, "FORM"},
            {21, "PLCM"},
            {22, "ARTL"}
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

        public static readonly Dictionary<int, string> SituationOff = new Dictionary<int, string>
        {
            {35,"1st Down"},
            {20,"2nd & Short"},
            {17,"2nd & Med"},
            {6,"2nd & Long"},
            {1,"3rd & Short"},
            {7,"3rd & Med"},
            {8,"3rd & Long"},
            {31,"4th & Short"},
            {32,"4th & Medium"},
            {33,"4th & Long"},
            {37,"Play Action"},
            {4,"Redzone"},
            {5,"Inside Five"},
            {3,"Goal Line"},
            {22,"Goal Line Pass"},
            {34,"Go for 2"},
            {24,"4 Minute Offense"},
            {10,"2 Minute Offense"},
            {21,"Hail Mary"},
            {23,"All Go" },
            {2,"Stop Clock"},
            {29,"Stop Clock"},
            {30,"Stop Clock"},
            {13,"Chew Clock"},
            {14,"QB Kneel"},
            {0,"Field Goal"},
            {38,"Field Goal"},
            {12,"Fake FG"},
            {9,"Punt"},
            {19,"Punt Tight"},
            {11,"Fake Punt"},
            {15,"Kickoff"},
            {36,"Kickoff"},
            {16,"Onside"},
            {18,"Safety Kickoff"}
        };

        public static readonly Dictionary<int, string> SituationDef = new Dictionary<int, string>
        {
            {0,"Normal"},
            {18,"Run"},
            {2,"Pass"},
            {20,"Nickle"},
            {6,"Nickle (Run)"},
            {13,"Nickle (Pass)"},
            {21,"Dime (Bunch & Empty)"},
            {27,"Dime (Pass)"},
            {12,"Trips"},
            {1,"Trips (Run)"},
            {11,"Trips (Pass)"},
            {22,"Goal Line"},
            {5,"Goal Line (Run)"},
            {7,"Goal Line (Pass)"},
            {24,"Wildcat"},
            {25,"Overload"},
            {29,"Clock, Time"},
            {19,"?"},
            {10,"?"}
        };

        public string filePath { get; set; }

        public ObservableCollection<FormationVM> Formations { get; set; }

        public static Point LOS { get { return new Point(266.5, 600); } }

        public string Type { get; set; }
        public Dictionary<int, string> Situations { get; set; }
        public List<TableNames> TableNames { get; set; }
        public List<Madden.TeamPlaybook.ARTL> ARTL { get; set; }
        public List<Madden.TeamPlaybook.FORM> FORM { get; set; }
        public List<Madden.TeamPlaybook.PBAI> PBAI { get; set; }
        public List<Madden.TeamPlaybook.PBAU> PBAU { get; set; }
        public List<Madden.TeamPlaybook.PBCC> PBCC { get; set; }
        public List<Madden.TeamPlaybook.PBFM> PBFM { get; set; }
        public List<Madden.TeamPlaybook.PBPL> PBPL { get; set; }
        public List<Madden.TeamPlaybook.PBST> PBST { get; set; }
        public List<Madden.TeamPlaybook.PLCM> PLCM { get; set; }
        public List<Madden.TeamPlaybook.PLPD> PLPD { get; set; }
        public List<Madden.TeamPlaybook.PLRD> PLRD { get; set; }
        public List<Madden.TeamPlaybook.PLYL> PLYL { get; set; }
        public List<Madden.TeamPlaybook.PLYS> PLYS { get; set; }
        public List<Madden.TeamPlaybook.PPCT> PPCT { get; set; }
        public List<Madden.TeamPlaybook.PSAL> PSAL { get; set; }
        public List<Madden.TeamPlaybook.SDEF> SDEF { get; set; }
        public List<Madden.TeamPlaybook.SETG> SETG { get; set; }
        public List<Madden.TeamPlaybook.SETL> SETL { get; set; }
        public List<Madden.TeamPlaybook.SETP> SETP { get; set; }
        public List<Madden.TeamPlaybook.SGFM> SGFM { get; set; }
        public List<Madden.TeamPlaybook.SPKF> SPKF { get; set; }
        public List<Madden.TeamPlaybook.SPKG> SPKG { get; set; }
        public List<Madden.TeamPlaybook.SRFT> SRFT { get; set; }
        public List<DCHT> DCHT { get; set; }
        public List<PLAY> PLAY { get; set; }

        public TeamPlaybook()
        {
        }

        public TeamPlaybook(string filepath)
        {
            filePath = filepath;
            GetTables();
            ReIndexTables();
            BuildPlaybook();
            GetType();
            BuildSituations();
        }

        public void RemoveFormation(FormationVM Formation, bool dbOnly = false)
        {
            for (int i = Formation.SubFormations.Count - 1; i >= 0; i--)
            {
                Formation.RemoveSubFormation(Formation.SubFormations[i]);
            }

            int ftyp = Formation.PBFM.FTYP;
            foreach (Madden.TeamPlaybook.PBFM formation in PBFM.Where(form => form.FTYP == ftyp).Cast<Madden.TeamPlaybook.PBFM>().ToList())
            {
                PBFM[PBFM.IndexOf(PBFM.Where(form => form.rec == formation.rec).FirstOrDefault())].ord_--;
            }
            PBFM.RemoveAt(PBFM.IndexOf(PBFM.Where(form => form.rec == Formation.PBFM.rec).FirstOrDefault()));

            try
            {
                FORM.RemoveAt(FORM.IndexOf(FORM.Where(form => form.rec == Formation.FORM.rec).FirstOrDefault()));
            }
            catch
            {

            }

            Formations.Remove(Formation);
        }

        public void AddFormation(FormationVM Formation, int ord = 0, bool dbOnly = false)
        {
            FORM existingFORM = FORM.Where(formation => formation.form == Formation.FORM.form).FirstOrDefault();
            if (existingFORM == null)
            {
                int ftyp = Formation.PBFM.FTYP;
                foreach (Madden.TeamPlaybook.PBFM formation in PBFM.Where(form => form.FTYP == ftyp).Cast<Madden.TeamPlaybook.PBFM>().ToList())
                {
                    if (formation.ord_ >= ord)
                    {
                        PBFM[PBFM.IndexOf(PBFM.Where(form => form.rec == formation.rec).FirstOrDefault())].ord_++;
                    }
                }

                PBFM.Add(Formation.PBFM);
                if (ord != 0)
                {
                    PBFM[PBFM.Count - 1].ord_ = ord;
                }
                else
                {
                    PBFM[PBFM.Count - 1].ord_ = Formations.Count + 1;
                }

                if (Formation.FORM != null)
                {
                    FORM.Add(Formation.FORM);
                }

                if (!dbOnly)
                {
                    Formations.Insert(ord - 1, new FormationVM(PBFM[PBFM.Count - 1], this, Formation.Playbook));
                }

                foreach (SubFormationVM subFormation in Formation.SubFormations)
                {
                    FormationVM newFormation = Formations.Where(formation => formation.PBFM.pbfm == Formation.PBFM.pbfm).FirstOrDefault();
                    newFormation.AddSubFormation(subFormation, dbOnly: false);
                    newFormation.SubFormations[newFormation.SubFormations.Count - 1].IsVisible = false;
                }
                //else
                //{
                //    foreach (SubFormationVM subFormation in Formation.SubFormations)
                //    {
                //        FormationVM existingFormation = Formations.Where(formation => formation.FORM.form == Formation.FORM.form).FirstOrDefault();
                //        subFormation.Formation.PBFM.pbfm = existingFormation.PBFM.pbfm;
                //        existingFormation.AddSubFormation(subFormation, dbOnly: false);
                //    }
                //}
            }
            else
            {
                MessageBox.Show(Formation.PBFM.name, "Formation Already Exists!");
            }
        }

        public void AddFormation(MaddenCustomPlaybookEditor.CustomPlaybookFormation Formation, int ord = 0)
        {
            FORM existingFORM = FORM.Where(formation => formation.form == Formation.CPFM.FORM).FirstOrDefault();
            if (existingFORM == null)
            {
                #region PBFM

                if (PBFM.Where(form => form.pbfm == Formation.PBFM.pbfm).FirstOrDefault() != null)
                {
                    Formation.PBFM.pbfm = NextAvailableID((from pbfm in PBFM select pbfm.pbfm).ToList());
                }

                if (ord == 0)
                {
                    ord = Formations.Count + 1;
                }

                Madden.TeamPlaybook.PBFM CPBpbfm = new Madden.TeamPlaybook.PBFM
                {
                    rec = PBST.Count,
                    FAU1 = -1,
                    FAU2 = -1,
                    FAU3 = -1,
                    FAU4 = -1,
                    pbfm = Formation.PBFM.pbfm,
                    FTYP = Formation.PBFM.FTYP,
                    ord_ = ord,
                    grid = Formation.PBFM.grid,
                    name = Formation.PBFM.name
                };

                PBFM.Add(CPBpbfm);

                #endregion

                #region FORM

                FORM.Add(new FORM
                {
                    rec = FORM.Count,
                    form = Formation.CPFM.FORM,
                    FTYP = Formation.CPFM.FTYP,
                    name = Formation.CPFM.name
                });

                #endregion

                #region Add Formation

                FormationVM formation = new FormationVM(CPBpbfm, this);

                int count = 0;

                for (int i = 0; i < Formation.SubFormations.Count; i = Formation.SubFormations.Count > 64 ? i + 2 : i++)
                {
                    Formation.SubFormations[i].PGPL[0].ord_ = count;
                    if (Formation.SubFormations.Count > 64)
                    {
                        Formation.SubFormations[i + 1].PGPL[0].ord_ = count;
                    }
                    count++;
                }

                foreach (MaddenCustomPlaybookEditor.CustomPlaybookSubFormation subFormation in Formation.SubFormations)
                {
                    formation.AddSubFormation(subFormation);
                    formation.SubFormations[formation.SubFormations.Count - 1].IsVisible = false;
                }

                if (ord <= Formations.Count && ord != 0)
                {
                    for (int i = ord - 1; i < Formations.Count; i++)
                    {
                        if (Formations[i].PBFM.FTYP == Formation.PBFM.FTYP)
                        {
                            Formations[i].PBFM.ord_ += 1;
                        }
                    }

                    Formations.Insert(ord - 1, formation);
                }
                else
                {
                    Formations.Add(formation);
                }
                Formations[ord - 1].IsVisible = true;
                Formations[ord - 1].IsExpanded = false;

                #endregion

            }
            else
            {
                MessageBox.Show(Formation.PBFM.name, "Formation Already Exists!");
            }
        }

        public void GetTables()
        {
            ARTL = Madden.TeamPlaybook.ARTL.GetARTL();    //ARTO ARTD
            FORM = Madden.TeamPlaybook.FORM.GetFORM();    //CPFM
            PBAI = Madden.TeamPlaybook.PBAI.GetPBAI();    //PBAI
            PBAU = Madden.TeamPlaybook.PBAU.GetPBAU();    //PBAU
            PBCC = Madden.TeamPlaybook.PBCC.GetPBCC();    
            PBFM = Madden.TeamPlaybook.PBFM.GetPBFM();    //PBFM
            PBPL = Madden.TeamPlaybook.PBPL.GetPBPL();    //PBPL
            PBST = Madden.TeamPlaybook.PBST.GetPBST();    
            PLCM = Madden.TeamPlaybook.PLCM.GetPLCM();
            PLPD = Madden.TeamPlaybook.PLPD.GetPLPD();
            PLRD = Madden.TeamPlaybook.PLRD.GetPLRD();
            PLYL = Madden.TeamPlaybook.PLYL.GetPLYL();
            PLYS = Madden.TeamPlaybook.PLYS.GetPLYS();
            PPCT = Madden.TeamPlaybook.PPCT.GetPPCT();
            PSAL = Madden.TeamPlaybook.PSAL.GetPSAL();
            SDEF = Madden.TeamPlaybook.SDEF.GetSDEF();
            SETG = Madden.TeamPlaybook.SETG.GetSETG();
            SETL = Madden.TeamPlaybook.SETL.GetSETL();
            SETP = Madden.TeamPlaybook.SETP.GetSETP();
            SGFM = Madden.TeamPlaybook.SGFM.GetSGFM();
            SPKF = Madden.TeamPlaybook.SPKF.GetSPKF();
            SPKG = Madden.TeamPlaybook.SPKG.GetSPKG();
            SRFT = Madden.TeamPlaybook.SRFT.GetSRFT();
        }

        public void GetRoster()
        {
            DCHT = Madden.Team.DCHT.GetDCHT(DBIndex: 1);
            PLAY = Madden.Team.PLAY.GetPLAY(DBIndex: 1);
        }

        public List<SubFormationVM> GetPSALlist()
        {
            List<PlayVM> psals = new List<PlayVM>();
            foreach (Madden.TeamPlaybook.PSAL psal in PSAL.Distinct())
            {
                psals.Add(new PlayVM());
                psals[psals.Count() - 1].Players = new ObservableCollection<PlayerVM>
                {
                    new PlayerVM
                    {
                        PSAL = PSAL.Where(_psal => _psal.psal == psal.psal).OrderBy(s => s.step).ToList(),
                        PLYS = PLYS.Where(plys => plys.PSAL == psal.psal).FirstOrDefault(),
                        ARTL = ARTL.Where(_psal => _psal.artl == PLYS.Where(plys => plys.PSAL == psal.psal).FirstOrDefault().ARTL).FirstOrDefault(),
                        artlColor = ARTLColor.BaseRoute,
                        SETG = new Madden.TeamPlaybook.SETG
                        {
                            x___ = 0,
                            y___ = 0,
                            fx__ = 0,
                            fy__ = 0
                        },
                        SETP = new Madden.TeamPlaybook.SETP
                        {
                            artx = 90,
                            arty = 80
                        },
                        Icon = new EllipseGeometry(new Point(0, 0), 4, 4).GetFlattenedPathGeometry(),
                        Play = psals[psals.Count() - 1]
                    }
                };
                //psals[psals.Count() - 1].Players[0].PSALpath = psals[psals.Count() - 1].Players[0].ConvertPSAL(psals[psals.Count() - 1].Players[0].PSAL);
                psals[psals.Count() - 1].Players[0].ARTLpath = psals[psals.Count() - 1].Players[0].ConvertARTL(psals[psals.Count() - 1].Players[0].ARTL);
                psals[psals.Count() - 1].Players[0].GetRouteCap();
            }
            List<SubFormationVM> routeTypes = new List<SubFormationVM>();
            foreach (int PLRR in PLYS.Select(x => x.PLRR).Distinct())
            {
                routeTypes.Add(
                    new SubFormationVM
                    {
                        PBST = new PBST { name = "PLRR: " + PLRR.ToString() },
                        Plays = new ObservableCollection<PlayVM>(psals.Where(play => play.Players[0].PLYS.PLRR == PLRR).ToList())
                    });
                routeTypes[routeTypes.Count()-1].IsVisible = true;
            }
                
            return routeTypes.OrderBy(s => s.PBST.name).ToList();
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

            foreach (Madden.TeamPlaybook.PBFM formation in PBFM)
            {
                Formations.Add(new FormationVM(formation, this));
            }
        }

        public void GetType()
        {
            Type =
                PBFM != null ?
                PBFM.Exists(form => form.FTYP == 1 || form.FTYP == 2 || form.FTYP == 3) ?
                "Offense" :
                "Defense" :
                "";
        }

        public void BuildSituations()
        {
            if (Type == "Offense")
            {
                Situations = SituationOff;
            }
            else if (Type == "Defense")
            {
                Situations = SituationDef;
            }
        }

        public void SaveTDBTables(int OpenIndex)
        {
            Madden.TeamPlaybook.ARTL.SetARTL(ARTL, OpenIndex);
            Madden.TeamPlaybook.FORM.SetFORM(FORM, OpenIndex);
            Madden.TeamPlaybook.PBAI.SetPBAI(PBAI, OpenIndex);
            Madden.TeamPlaybook.PBAU.SetPBAU(PBAU, OpenIndex);
            Madden.TeamPlaybook.PBCC.SetPBCC(PBCC, OpenIndex);
            Madden.TeamPlaybook.PBFM.SetPBFM(PBFM, OpenIndex);
            Madden.TeamPlaybook.PBPL.SetPBPL(PBPL, OpenIndex);
            Madden.TeamPlaybook.PBST.SetPBST(PBST, OpenIndex);
            Madden.TeamPlaybook.PLCM.SetPLCM(PLCM, OpenIndex);
            Madden.TeamPlaybook.PLPD.SetPLPD(PLPD, OpenIndex);
            Madden.TeamPlaybook.PLRD.SetPLRD(PLRD, OpenIndex);
            Madden.TeamPlaybook.PLYL.SetPLYL(PLYL, OpenIndex);
            Madden.TeamPlaybook.PLYS.SetPLYS(PLYS, OpenIndex);
            Madden.TeamPlaybook.PPCT.SetPPCT(PPCT, OpenIndex);
            Madden.TeamPlaybook.PSAL.SetPSAL(PSAL, OpenIndex);
            Madden.TeamPlaybook.SDEF.SetSDEF(SDEF, OpenIndex);
            Madden.TeamPlaybook.SETG.SetSETG(SETG, OpenIndex);
            Madden.TeamPlaybook.SETL.SetSETL(SETL, OpenIndex);
            Madden.TeamPlaybook.SETP.SetSETP(SETP, OpenIndex);
            Madden.TeamPlaybook.SGFM.SetSGFM(SGFM, OpenIndex);
            Madden.TeamPlaybook.SPKF.SetSPKF(SPKF, OpenIndex);
            Madden.TeamPlaybook.SPKG.SetSPKG(SPKG, OpenIndex);
            Madden.TeamPlaybook.SRFT.SetSRFT(SRFT, OpenIndex);
        }

        public void ReIndexTables(bool sort = true)
        {
            if (sort) ARTL = Madden.TeamPlaybook.ARTL.Sort(ARTL);
            for (int i = 0; i < ARTL.Count; i++)
            {
                ARTL[i].rec = i;
            }
            if (sort) FORM = Madden.TeamPlaybook.FORM.Sort(FORM);
            for (int i = 0; i < FORM.Count; i++)
            {
                FORM[i].rec = i;
            }
            if (sort) PBAI = Madden.TeamPlaybook.PBAI.Sort(PBAI);
            for (int i = 0; i < PBAI.Count; i++)
            {
                PBAI[i].rec = i;
            }
            if (sort) PBAU = Madden.TeamPlaybook.PBAU.Sort(PBAU);
            for (int i = 0; i < PBAU.Count; i++)
            {
                PBAU[i].rec = i;
            }
            if (sort) PBCC = Madden.TeamPlaybook.PBCC.Sort(PBCC);
            for (int i = 0; i < PBCC.Count; i++)
            {
                PBCC[i].rec = i;
            }
            if (sort) PBFM = Madden.TeamPlaybook.PBFM.Sort(PBFM);
            for (int i = 0; i < PBFM.Count; i++)
            {
                PBFM[i].rec = i;
            }
            if (sort) PBPL = Madden.TeamPlaybook.PBPL.Sort(PBPL);
            for (int i = 0; i < PBPL.Count; i++)
            {
                PBPL[i].rec = i;
            }
            if (sort) PBST = Madden.TeamPlaybook.PBST.Sort(PBST);
            for (int i = 0; i < PBST.Count; i++)
            {
                PBST[i].rec = i;
            }
            if (sort) PLCM = Madden.TeamPlaybook.PLCM.Sort(PLCM);
            for (int i = 0; i < PLCM.Count; i++)
            {
                PLCM[i].rec = i;
            }
            if (sort) PLPD = Madden.TeamPlaybook.PLPD.Sort(PLPD);
            for (int i = 0; i < PLPD.Count; i++)
            {
                PLPD[i].rec = i;
            }
            if (sort) PLRD = Madden.TeamPlaybook.PLRD.Sort(PLRD);
            for (int i = 0; i < PLRD.Count; i++)
            {
                PLRD[i].rec = i;
            }
            if (sort) PLYL = Madden.TeamPlaybook.PLYL.Sort(PLYL);
            for (int i = 0; i < PLYL.Count; i++)
            {
                PLYL[i].rec = i;
            }
            if (sort) PLYS = Madden.TeamPlaybook.PLYS.Sort(PLYS);
            for (int i = 0; i < PLYS.Count; i++)
            {
                PLYS[i].rec = i;
            }
            if (sort) PPCT = Madden.TeamPlaybook.PPCT.Sort(PPCT);
            for (int i = 0; i < PPCT.Count; i++)
            {
                PPCT[i].rec = i;
            }
            if (sort) PSAL = Madden.TeamPlaybook.PSAL.Sort(PSAL);
            for (int i = 0; i < PSAL.Count; i++)
            {
                PSAL[i].rec = i;
            }
            if (sort) SDEF = Madden.TeamPlaybook.SDEF.Sort(SDEF);
            for (int i = 0; i < SDEF.Count; i++)
            {
                SDEF[i].rec = i;
            }
            if (sort) SETG = Madden.TeamPlaybook.SETG.Sort(SETG);
            for (int i = 0; i < SETG.Count; i++)
            {
                SETG[i].rec = i;
            }
            if (sort) SETL = Madden.TeamPlaybook.SETL.Sort(SETL);
            for (int i = 0; i < SETL.Count; i++)
            {
                SETL[i].rec = i;
            }
            if (sort) SETP = Madden.TeamPlaybook.SETP.Sort(SETP);
            for (int i = 0; i < SETP.Count; i++)
            {
                SETP[i].rec = i;
            }
            if (sort) SGFM = Madden.TeamPlaybook.SGFM.Sort(SGFM);
            for (int i = 0; i < SGFM.Count; i++)
            {
                SGFM[i].rec = i;
            }
            if (sort) SPKF = Madden.TeamPlaybook.SPKF.Sort(SPKF);
            for (int i = 0; i < SPKF.Count; i++)
            {
                SPKF[i].rec = i;
            }
            if (sort) SPKG = Madden.TeamPlaybook.SPKG.Sort(SPKG);
            for (int i = 0; i < SPKG.Count; i++)
            {
                SPKG[i].rec = i;
            }
            if (sort) SRFT = Madden.TeamPlaybook.SRFT.Sort(SRFT);
            for (int i = 0; i < SRFT.Count; i++)
            {
                SRFT[i].rec = i;
            }
        }

        public static int NextAvailableID(List<int> IDs, bool insert = false, int buffer = 0)
        {
            if (IDs == null || IDs.Count == 0) return 1;
            int ID = IDs.Max() + 1;
            if (insert)
            {
                for (int i = 1; i <= ID; i++)
                {
                    if (!IDs.Contains(i))
                    {
                        ID = i;
                        break;
                    }
                }
            }
            return ID + buffer;
        }

        #region IsUsing

        public bool IsUsing(Madden.TeamPlaybook.PSAL psal)
        {
            bool Using = false;
            foreach (Madden.TeamPlaybook.PLYS assignment in PLYS)
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
            foreach (Madden.TeamPlaybook.PLYS assignment in PLYS)
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