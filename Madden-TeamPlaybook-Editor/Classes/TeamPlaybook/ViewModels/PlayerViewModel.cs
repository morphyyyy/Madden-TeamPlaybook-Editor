using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Shapes;
using TDBAccess;
using Madden.TeamPlaybook;
using Madden.Team;
using System.Windows;
using System.Windows.Media;
using System.Globalization;
using MaddenTeamPlaybookEditor.User_Controls;
using System.Windows.Markup;
using static MaddenTeamPlaybookEditor.ViewModels.SubFormationVM;

namespace MaddenTeamPlaybookEditor.ViewModels
{
    [Serializable]
    public class PlayerVM : INotifyPropertyChanged
    {
        public override string ToString()
        {
            string PlayerVM_string = EPos + " " + DPos + "\n";
            PlayerVM_string += SETP.ToString() + "\n";
            PlayerVM_string += SETG.ToString() + "\n";
            foreach (PSAL psal in PSAL) PlayerVM_string += psal.ToString() + "\n";
            //PlayerVM_string += "Rec#: " + ARTL.rec + "\tartl: " + ARTL.artl + "\tacnt: " + ARTL.acnt + "\n";
            PlayerVM_string += ARTL.ToString();
            return PlayerVM_string;
        }

        public PlayVM Play { get; set; }

        private PLYS _PLYS { get; set; }
        public PLYS PLYS
        {
            get { return _PLYS; }
            set
            {
                if (_PLYS == value)
                    return;
                _PLYS = value;
                if (_PSAL != null)
                {
                    ConvertPSAL(_PSAL);
                    GetRouteCap();
                }
            }
        }
        public SETP SETP { get; set; }
        public SETG SETG { get; set; }
        public SRFT SRFT { get; set; }
        private List<PSAL> _PSAL { get; set; }
        public List<PSAL> PSAL
        {
            get { return _PSAL; }
            set
            {
                if (_PSAL == value)
                    return;
                _PSAL = value;
                if (_PSAL != null)
                {
                    ConvertPSAL(_PSAL);
                    GetRouteCap();
                }
            }
        }
        public ARTL ARTL { get; set; }
        private Point _XY { get; set; }
        public Point XY
        {
            get { return _XY; }
            set
            {
                if (_XY == value)
                    return;
                _XY = value;
                OnPropertyChanged("XY");
            }
        }
        public Progression progression { get; set; }
        public double RouteDepth { get; set; }
        public double RouteDepthPLPDnormalized
        {
            get
            {
                int totalWeight = 
                    this.Play.PLPD != null ? 
                    this.Play.PLPD.progressions.Where(p => p.per > 1).Select(p => p.per).Sum() :
                    1;
                double routeMultiplier = 
                    progression != null ? 
                    progression.per : 
                    1;
                return (RouteDepth * routeMultiplier) / totalWeight;
            }
        }
        public DCHT DCHT { get; set; }
        public PLAY Player { get; set; }

        public string DPos { get; set; }
        public string EPos { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Number { get; set; }
        public int Speed { get; set; }

        [field: NonSerialized] 
        public PathGeometry Icon { get; set; }
        [field: NonSerialized]
        public ARTLColor artlColor { get; set; }
        [field: NonSerialized] 
        public List<Path> ARTLpath { get; set; }
        [field: NonSerialized] 
        public List<Path> PSALpath { get; set; }
        [field: NonSerialized] 
        public PathGeometry RouteCap { get; set; }

        private bool _isSelected;

        public PlayerVM()
        {

        }

        public PlayerVM(PLYS plys, PlayVM _Play)
        {
            Play = _Play;
            PLYS = Play.PLYS.FirstOrDefault(player => player.poso == plys.poso);
            UpdatePlayer();
        }

        public void UpdatePlayer()
        {
            GetAssignment();
            GetARTL();
            GetPSAL();
            GetRouteCap();
            if (Play.PLPD != null) GetIcxIcy();
            GetAttributes();
        }

        /// <summary>
        /// Returns the absolute value of RoutePoints.Min minus Player.XY.Y represented in pixels
        /// </summary>
        public void GetRouteDepth(PointCollection Points)
        {
            RouteDepth = TeamPlaybook.RouteType
                .Where(p => p.Value[0] == "RR")
                .Select(p => p.Key)
                .Contains(PLYS.PLRR) ?
                Math.Abs(Points.Min(p => p.Y)) - XY.Y :
                0;
        }

        public void GetAssignment()
        {
            SETP = Play.SubFormation.CurrentPackage.FirstOrDefault(set => set.poso == PLYS.poso);
            SETG = Play.SubFormation.CurrentAlignment.SETG.FirstOrDefault(set => set.SETP == SETP.setp);
            SRFT = Play.SRFT.FirstOrDefault(assignment => assignment.PLYR == PLYS.poso);
            EPos = TeamPlaybook.Positions[Play.SubFormation.CurrentPackage.FirstOrDefault(poso => poso.poso == PLYS.poso).EPos];
            DPos = Play.SubFormation.CurrentPackage.FirstOrDefault(poso => poso.poso == PLYS.poso).DPos.ToString();
            XY = new Point { X = SETG.x___ * 11.4286, Y = SETG.y___ * -10 };
        }

        public void GetPSAL()
        {
            PSAL = Play.SubFormation.Formation.Playbook.PSAL.Where(step => step.psal == PLYS.PSAL).OrderBy(step => step.step).ToList();
            ConvertPSAL(PSAL);

            //if (PSAL.Count == 0)
            //{
            //    MessageBox.Show(
            //        Play.SubFormation.Formation.PBFM.name + " " +
            //        Play.SubFormation.PBST.name + " - " +
            //        Play.PBPL.name + "\n\n" +
            //        DPos + EPos +
            //        "\nPSAL: " + PLYS.PSAL +
            //        "\nARTL: " + PLYS.ARTL,
            //        "Missing PSAL"
            //        );
            //}
        }

        public void GetIcxIcy()
        {
            switch (PLYS.poso)
            {
                case 1:
                    progression = Play.PLPD.progressions.FirstOrDefault(progression => progression.rcv == 1);
                    break;
                case 2:
                    progression = Play.PLPD.progressions.FirstOrDefault(progression => progression.rcv == 2);
                    break;
                case 3:
                    progression = Play.PLPD.progressions.FirstOrDefault(progression => progression.rcv == 3);
                    break;
                case 4:
                    progression = Play.PLPD.progressions.FirstOrDefault(progression => progression.rcv == 4);
                    break;
                case 5:
                    progression = Play.PLPD.progressions.FirstOrDefault(progression => progression.rcv == 5);
                    break;
                default:
                    break;
            }

        }

        public void UpdateXY(Point point)
        {
            PSAL psal = this.PSAL.LastOrDefault(step => step.code == 48);
            if (psal != null)
            {
                //X to val1 = 51/90 = .5667
                //Y to val2 = 3/8 = .375
                psal.val1 = (int)(point.X * .5667);
                psal.val2 = (int)(point.Y * -.375);
            }
            //float xRatio = (float)(point.X * .0875) / this.SETG.x___;
            //float yRatio = (float)(point.Y * -.1) / this.SETG.y___;
            //this.SETP.fmtx = (int)(this.SETP.fmtx * xRatio);
            //this.SETP.fmty = (int)(this.SETP.fmty * yRatio);
            //this.SETP.artx = (int)(this.SETP.artx * xRatio);
            //this.SETP.arty = (int)(this.SETP.arty * yRatio);
            this.XY = point;
            if (this.SETG.SGF_ != this.Play?.SubFormation?.CurrentAlignment?.SGFM?.SGF_)
            {
                if (!Play.SubFormation.Formation.Playbook.SGFM.Contains(this.Play.SubFormation.CurrentAlignment.SGFM))
                {
                    this.Play.SubFormation.CurrentAlignment.SGFM.SGF_ = Play.SubFormation.Formation.Playbook.SGFM.Max(m => m.SGF_) + 1;
                    this.Play.SubFormation.CurrentAlignment.SGFM.rec = Play.SubFormation.Formation.Playbook.SGFM.Max(m => m.rec) + 1;
                    Play.SubFormation.Formation.Playbook.SGFM.Add(this.Play.SubFormation.CurrentAlignment.SGFM);
                }
                SETG newSETG = new SETG
                {
                    rec = this.Play.SubFormation.Formation.Playbook.SETG.Max(x => x.rec) + 1,
                    setg = this.Play.SubFormation.Formation.Playbook.SETG.Max(setg => setg.setg) + 1,
                    SETP = this.SETG.SETP,
                    SGF_ = this.Play.SubFormation.CurrentAlignment.SGFM.SGF_,
                    SF__ = this.SETG.SF__,
                    x___ = (float)(_XY.X * .0875),
                    y___ = (float)(_XY.Y * -.1),
                    fx__ = this.SETG.fx__,
                    fy__ = this.SETG.fy__,
                    anm_ = this.SETG.anm_,
                    dir_ = this.SETG.dir_,
                    fanm = this.SETG.fanm,
                    fdir = this.SETG.fdir
                };
                SETG = newSETG;
                int poso = this.Play.SubFormation.CurrentAlignment.SETG.FindIndex(setp => setp.SETP == this.SETG.SETP);
                this.Play.SubFormation.CurrentAlignment.SETG[poso] = newSETG;
                Alignment alignment = this.Play.SubFormation.Alignments.FirstOrDefault(a => a.SGFM == this.Play?.SubFormation?.CurrentAlignment?.SGFM);
                alignment.SETG.Add(SETG);
                alignment.SETG = alignment.SETG.OrderBy(s => s.SETP).ToList();
                Play.SubFormation.Formation.Playbook.SETG.Add(newSETG);
            }
            else
            {
                this.SETG.x___ = (float)(_XY.X * .0875);
                this.SETG.y___ = (float)(_XY.Y * -.1);
            }
        }

        public void UpdateAlignment()
        {

        }

        public void UpdatePSAL()
        {
            ConvertPSAL(PSAL);
            foreach (Path path in PSALpath) ((PathGeometry)path.Data).Freeze();
        }

        public void UpdatePSAL(PSAL psal, Point point)
        {
            switch (psal.code)
            {
                default:
                    break;
            }
            ConvertPSAL(PSAL);
            foreach (Path path in PSALpath) ((PathGeometry)path.Data).Freeze();
        }

        public void GetARTL()
        {
            #region ARTL

            ARTL = Play.SubFormation.Formation.Playbook.ARTL.FirstOrDefault(step => step.artl == PLYS.ARTL);
            ConvertARTL(ARTL);

            #endregion

            #region Icon

            switch (Play.SubFormation.CurrentPackage.FirstOrDefault(set => set.poso == PLYS.poso).arti)
            {
                case 0://X
                    FormattedText X = new FormattedText(
                        "x",
                        CultureInfo.GetCultureInfo("en-us"),
                        FlowDirection.LeftToRight,
                        new Typeface(new FontFamily("Tahoma"), FontStyles.Normal, FontWeights.Black, FontStretches.Normal),
                        16,
                        new SolidColorBrush(Madden.TeamPlaybook.ARTLColor.PlayerIconColor)
                        );
                    Icon = X.BuildGeometry(new Point(-4, -12)).GetFlattenedPathGeometry(.05, ToleranceType.Absolute);
                    break;
                case 1://Circle
                    Icon = new EllipseGeometry(new Point(0, 0), 4, 4).GetFlattenedPathGeometry(.05, ToleranceType.Absolute);
                    break;
                case 2://Square
                    Icon = new RectangleGeometry(new Rect(new Point(-4, -4), new Size(8, 8))).GetFlattenedPathGeometry();
                    break;
                case 4://Circle
                    Icon = new EllipseGeometry(new Point(0, 0), 4, 4).GetFlattenedPathGeometry(.05, ToleranceType.Absolute);
                    break;
                case 5://Square
                    Icon = new RectangleGeometry(new Rect(new Point(-4, -4), new Size(8, 8))).GetFlattenedPathGeometry();
                    break;
                default:
                    Icon = new EllipseGeometry(new Point(0, 0), 4, 4).GetFlattenedPathGeometry(.1, ToleranceType.Absolute);
                    break;
            }

            #endregion

            #region Color

            GetARTLcolor();

            #endregion
        }

        public void GetARTLcolor()
        {
            artlColor =
                PLYS.poso != 0 ?
                Play.PLYL.vpos == PLYS.poso ?
                artlColor = ARTLColor.PrimaryRoute :
                artlColor = ARTLColor.Undefined :
                artlColor = ARTLColor.Undefined
                ;

            //switch (ARTL.acnt)
            //{
            //    default:
            //        artlColor = new ARTLColor(Colors.BlueViolet);
            //        break;
            //}
        }

        public void GetRouteCap()
        {
            if (artlColor.Equals(ARTLColor.Block))
            {
                RouteCap = ARTL.Block;
            }
            else if (ARTL != null )
            {
                if (ARTL.ARTList != null)
                {
                    //Get Zone Size
                    int EndOfList = ARTL.ARTList.FindIndex(playart => playart.ct != 0);
                    if (EndOfList < 0) EndOfList = 0;
                    if (ARTL.ARTList.Count > 0)
                    {
                        switch (ARTL.ARTList[EndOfList].ct)
                        {
                            case 3: //Deep quarter, hook, flat
                                RouteCap = ARTL.QuarterHookFlat;
                                break;

                            case 4: //Deep third
                                RouteCap = ARTL.DeepThird;
                                break;

                            case 5: //Deep half
                                RouteCap = ARTL.DeepHalf;
                                break;

                            case 6: //QB spy
                                RouteCap = ARTL.QBSpy;
                                break;

                            default: //Arrow
                                RouteCap = ARTL.Arrow;
                                break;
                        }
                    }
                }
            }
        }

        public void GetAttributes()
        {
            switch (EPos)
            {
                case "QB"://X
                    Speed = 65;
                    break;
                case "HB"://X
                    Speed = 89;
                    break;
                case "FB"://X
                    Speed = 70;
                    break;
                case "WR"://X
                    Speed = 92;
                    break;
                case "TE"://X
                    Speed = 82;
                    break;
                case "LT"://X
                    Speed = 65;
                    break;
                case "LG"://X
                    Speed = 65;
                    break;
                case "C"://X
                    Speed = 65;
                    break;
                case "RG"://X
                    Speed = 65;
                    break;
                case "RT"://X
                    Speed = 65;
                    break;
                case "LE"://X
                    Speed = 75;
                    break;
                case "RE"://X
                    Speed = 75;
                    break;
                case "DT"://X
                    Speed = 65;
                    break;
                case "LOLB"://X
                    Speed = 80;
                    break;
                case "MLB"://X
                    Speed = 80;
                    break;
                case "ROLB"://X
                    Speed = 80;
                    break;
                case "CB"://X
                    Speed = 90;
                    break;
                case "FS"://X
                    Speed = 90;
                    break;
                case "SS"://X
                    Speed = 85;
                    break;
                case "K"://X
                    Speed = 65;
                    break;
                case "P"://X
                    Speed = 65;
                    break;
                case "KR"://X
                    Speed = 9905;
                    break;
                case "PR"://X
                    Speed = 90;
                    break;
                case "KOS"://X
                    Speed = 65;
                    break;
                case "LS"://X
                    Speed = 65;
                    break;
                case "3RB"://X
                    Speed = 89;
                    break;
                case "PRB"://X
                    Speed = 85;
                    break;
                case "SWR"://X
                    Speed = 90;
                    break;
                case "RLE"://X
                    Speed = 80;
                    break;
                case "RRE"://X
                    Speed = 80;
                    break;
                case "RDT"://X
                    Speed = 75;
                    break;
                case "SLB"://X
                    Speed = 80;
                    break;
                case "SCB"://X
                    Speed = 90;
                    break;
                default:
                    Speed = 70;
                    break;
            }

            if (Play.SubFormation.Formation.Playbook.DCHT != null)
            {
                int _epos = Play.SubFormation.CurrentPackage.FirstOrDefault(poso => poso.poso == PLYS.poso).EPos;
                DCHT = Play.SubFormation.Formation.Playbook.DCHT.FirstOrDefault(player => player.PPOS == SETP.EPos && player.ddep == SETP.DPos - 1);
                if (DCHT != null)
                {
                    Number = Play.SubFormation.Formation.Playbook.PLAY.FirstOrDefault(player => player.PGID == DCHT.PGID).PJEN;
                    FirstName = Play.SubFormation.Formation.Playbook.PLAY.FirstOrDefault(player => player.PGID == DCHT.PGID).PFNA;
                    LastName = Play.SubFormation.Formation.Playbook.PLAY.FirstOrDefault(player => player.PGID == DCHT.PGID).PLNA;
                }
            }
        }

        public void ConvertPSAL(List<PSAL> PSAL, Point LOS = new Point(), bool flipPSAL = false)
        {
            List<Path> PSALpath = new List<Path>();
            PathGeometry RouteGeo = new PathGeometry();
            PathFigure RouteFigure = new PathFigure();
            PathSegmentCollection RouteSegments = new PathSegmentCollection();
            RouteFigure.Segments = RouteSegments;
            RouteGeo.Figures.Add(RouteFigure);
            PointCollection RoutePoints = new PointCollection();
            PSALpath.Add(new Path());
            PSALpath[PSALpath.Count - 1].Data = RouteGeo;
            RouteFigure.StartPoint = new Point { X = 0, Y = 0 };
            PolyLineSegment line_segment = new PolyLineSegment();
            line_segment.IsSmoothJoin = true;
            RouteSegments.Add(line_segment);
            line_segment.Points = RoutePoints;
            Point Offset = new Point { X = 0, Y = 0 };

            for (int i = 0; i < PSAL.Count; i++)
            {
                switch (PSAL[i].code)
                {
                    case 1:
                        #region Run To End Zone

                        break;

                    #endregion

                    case 2:
                        #region Chase Ball

                        break;

                    #endregion

                    case 3:
                        #region MoveDirDist

                        if (RoutePoints.Count > 0) Offset = RoutePoints[RoutePoints.Count - 1];
                        RoutePoints.Add(MoveDistDirToXY(PSAL[i].val1, PSAL[i].val2, Offset, flipPSAL));
                        if (artlColor.Equals(ARTLColor.Undefined)) artlColor = ARTLColor.BaseRoute;
                        break;

                    #endregion

                    case 4:
                        #region MoveDirDistConst

                        if (RoutePoints.Count > 0) Offset = RoutePoints[RoutePoints.Count - 1];
                        RoutePoints.Add(MoveDistDirToXY(PSAL[i].val1, PSAL[i].val2, Offset, flipPSAL));
                        if (artlColor.Equals(ARTLColor.Undefined)) artlColor = ARTLColor.BaseRoute;
                        break;

                    #endregion

                    case 5:
                        #region Face Direction

                        break;

                    #endregion

                    case 7:
                        #region QB Scramble

                        artlColor = ARTLColor.QBScramble;
                        break;

                    #endregion

                    case 8:
                        #region Receiver Run Route

                        //convert to offest
                        if (RoutePoints.Count > 0) Offset = RoutePoints[RoutePoints.Count - 1];
                        RoutePoints.Add(MoveDistDirToXY(PSAL[i].val1, PSAL[i].val2, Offset, flipPSAL));

                        if (artlColor.Equals(ARTLColor.Undefined)) artlColor = ARTLColor.BaseRoute;
                        break;

                    #endregion

                    case 9:
                        #region Receiver Cut Move

                        if (RoutePoints.Count > 0) Offset = RoutePoints[RoutePoints.Count - 1];

                        //1 = 45 degrees
                        if (PSAL[i].val2 == 1)
                        {
                            if (PSAL[i].val1 == 1)
                            {
                                RoutePoints.Add(MoveDistDirToXY(24, (int)(PSAL[i - 1].val2 - (45 * Madden.TeamPlaybook.PSAL.AngleRatio)), Offset, flipPSAL));
                            }
                            else if (PSAL[i].val1 == 2)
                            {
                                RoutePoints.Add(MoveDistDirToXY(24, (int)(PSAL[i - 1].val2 + (45 * Madden.TeamPlaybook.PSAL.AngleRatio)), Offset, flipPSAL));
                            }
                        }

                        //2 = 90 degrees
                        if (PSAL[i].val2 == 2)
                        {
                            if (PSAL[i].val1 == 1)
                            {
                                RoutePoints.Add(MoveDistDirToXY(24, (int)(PSAL[i - 1].val2 - (90 * Madden.TeamPlaybook.PSAL.AngleRatio)), Offset, flipPSAL));
                            }
                            else if (PSAL[i].val1 == 2)
                            {
                                RoutePoints.Add(MoveDistDirToXY(24, (int)(PSAL[i - 1].val2 + (90 * Madden.TeamPlaybook.PSAL.AngleRatio)), Offset, flipPSAL));
                            }
                        }

                        //3 = 22 degrees
                        if (PSAL[i].val2 == 3)
                        {
                            if (PSAL[i].val1 == 1)
                            {
                                RoutePoints.Add(MoveDistDirToXY(24, (int)(PSAL[i - 1].val2 - (22 * Madden.TeamPlaybook.PSAL.AngleRatio)), Offset, flipPSAL));
                            }
                            else if (PSAL[i].val1 == 2)
                            {
                                RoutePoints.Add(MoveDistDirToXY(24, (int)(PSAL[i - 1].val2 + (22 * Madden.TeamPlaybook.PSAL.AngleRatio)), Offset, flipPSAL));
                            }
                        }

                        //4 = 67 degrees
                        if (PSAL[i].val2 == 4)
                        {
                            if (PSAL[i].val1 == 1)
                            {
                                RoutePoints.Add(MoveDistDirToXY(24, (int)(PSAL[i - 1].val2 - (67 * Madden.TeamPlaybook.PSAL.AngleRatio)), Offset, flipPSAL));
                            }
                            else if (PSAL[i].val1 == 2)
                            {
                                RoutePoints.Add(MoveDistDirToXY(24, (int)(PSAL[i - 1].val2 + (67 * Madden.TeamPlaybook.PSAL.AngleRatio)), Offset, flipPSAL));
                            }
                        }

                        //5 = Curl
                        if (PSAL[i].val2 == 5)
                        {
                            if (PSAL[i].val1 == 1)
                            {
                                RoutePoints.Add(MoveDistDirToXY(24, (int)(PSAL[i - 1].val2 - (135 * Madden.TeamPlaybook.PSAL.AngleRatio)), Offset, flipPSAL));
                            }
                            else if (PSAL[i].val1 == 2)
                            {
                                RoutePoints.Add(MoveDistDirToXY(24, (int)(PSAL[i - 1].val2 + (135 * Madden.TeamPlaybook.PSAL.AngleRatio)), Offset, flipPSAL));
                            }
                        }

                        //7 = HitchComback, 8 = HitchGoIn and 9 = HitchGoOut
                        if (PSAL[i].val2 == 7 || PSAL[i].val2 == 8 || PSAL[i].val2 == 9)
                        {
                            if (PSAL[i].val1 == 1)
                            {
                                RoutePoints.Add(MoveDistDirToXY(8, (int)(PSAL[i - 1].val2 - (105 * Madden.TeamPlaybook.PSAL.AngleRatio)), Offset, flipPSAL));
                            }
                            else if (PSAL[i].val1 == 2)
                            {
                                RoutePoints.Add(MoveDistDirToXY(8, (int)(PSAL[i - 1].val2 + (105 * Madden.TeamPlaybook.PSAL.AngleRatio)), Offset, flipPSAL));
                            }
                        }

                        //10 = OutAndUp
                        if (PSAL[i].val2 == 10)
                        {
                            if (PSAL[i].val1 == 1)
                            {
                                RoutePoints.Add(MoveDistDirToXY(24, (int)(PSAL[i - 1].val2 - (105 * Madden.TeamPlaybook.PSAL.AngleRatio)), Offset, flipPSAL));
                            }
                            else if (PSAL[i].val1 == 2)
                            {
                                RoutePoints.Add(MoveDistDirToXY(24, (int)(PSAL[i - 1].val2 + (105 * Madden.TeamPlaybook.PSAL.AngleRatio)), Offset, flipPSAL));
                            }
                        }

                        //11 = Smash and 17 = SmashQuick
                        if (PSAL[i].val2 == 11 || PSAL[i].val2 == 17)
                        {
                            if (PSAL[i].val1 == 1)
                            {
                                RoutePoints.Add(MoveDistDirToXY(16, (int)(-10 * Madden.TeamPlaybook.PSAL.AngleRatio), Offset, flipPSAL));
                            }
                            else if (PSAL[i].val1 == 2)
                            {
                                RoutePoints.Add(MoveDistDirToXY(16, (int)(190 * Madden.TeamPlaybook.PSAL.AngleRatio), Offset, flipPSAL));
                            }
                        }

                        //13 = WRScrn
                        if (PSAL[i].val2 == 13)
                        {
                            if (PSAL[i].val1 == 1)
                            {
                                RoutePoints.Add(MoveDistDirToXY(24, (int)(-15 * Madden.TeamPlaybook.PSAL.AngleRatio), Offset, flipPSAL));
                            }
                            else if (PSAL[i].val1 == 2)
                            {
                                RoutePoints.Add(MoveDistDirToXY(24, (int)(195 * Madden.TeamPlaybook.PSAL.AngleRatio), Offset, flipPSAL));
                            }

                            //assign route color
                            if (artlColor.Equals(ARTLColor.Undefined))
                            {
                                artlColor = ARTLColor.BaseRoute;
                            }
                        }

                        //14 = 90Inside
                        if (PSAL[i].val2 == 14)
                        {
                            if (PSAL[i].val1 == 1)
                            {
                                RoutePoints.Add(MoveDistDirToXY(24, (int)(0 * Madden.TeamPlaybook.PSAL.AngleRatio), Offset, flipPSAL));
                            }
                            else if (PSAL[i].val1 == 2)
                            {
                                RoutePoints.Add(MoveDistDirToXY(24, (int)(180 * Madden.TeamPlaybook.PSAL.AngleRatio), Offset, flipPSAL));
                            }
                        }

                        //16 = 180Partial
                        if (PSAL[i].val2 == 16)
                        {
                            RoutePoints.Add(MoveDistDirToXY(16, (int)(270 * Madden.TeamPlaybook.PSAL.AngleRatio), Offset, flipPSAL));
                        }

                        //18 = Hitch
                        if (PSAL[i].val2 == 18)
                        {
                            if (PSAL[i].val1 == 1)
                            {
                                RoutePoints.Add(MoveDistDirToXY(16, (int)(PSAL[i - 1].val2 - (105 * Madden.TeamPlaybook.PSAL.AngleRatio)), Offset, flipPSAL));
                            }
                            else if (PSAL[i].val1 == 2)
                            {
                                RoutePoints.Add(MoveDistDirToXY(16, (int)(PSAL[i - 1].val2 + (105 * Madden.TeamPlaybook.PSAL.AngleRatio)), Offset, flipPSAL));
                            }
                        }

                        //20 = ShakeCut
                        if (PSAL[i].val2 == 20)
                        {
                            if (PSAL[i].val1 == 1)
                            {
                                RoutePoints.Add(MoveDistDirToXY(8, (int)(PSAL[i - 1].val2 + (67 * Madden.TeamPlaybook.PSAL.AngleRatio)), Offset, flipPSAL));
                            }
                            else if (PSAL[i].val1 == 2)
                            {
                                RoutePoints.Add(MoveDistDirToXY(8, (int)(PSAL[i - 1].val2 - (67 * Madden.TeamPlaybook.PSAL.AngleRatio)), Offset, flipPSAL));
                            }
                        }

                        //21 = StutterCut
                        if (PSAL[i].val2 == 21)
                        {
                            if (PSAL[i].val1 == 1)
                            {
                                RoutePoints.Add(MoveDistDirToXY(8, (int)(PSAL[i - 1].val2 - (67 * Madden.TeamPlaybook.PSAL.AngleRatio)), Offset, flipPSAL));
                            }
                            else if (PSAL[i].val1 == 2)
                            {
                                RoutePoints.Add(MoveDistDirToXY(8, (int)(PSAL[i - 1].val2 + (67 * Madden.TeamPlaybook.PSAL.AngleRatio)), Offset, flipPSAL));
                            }
                        }

                        //22 = HingeCut
                        if (PSAL[i].val2 == 22)
                        {
                            if (PSAL[i].val1 == 1)
                            {
                                RoutePoints.Add(MoveDistDirToXY(16, (int)(145 * Madden.TeamPlaybook.PSAL.AngleRatio), Offset, flipPSAL));
                            }
                            else if (PSAL[i].val1 == 2)
                            {
                                RoutePoints.Add(MoveDistDirToXY(16, (int)(35 * Madden.TeamPlaybook.PSAL.AngleRatio), Offset, flipPSAL));
                            }
                        }

                        //23 = PostCorner
                        if (PSAL[i].val2 == 23)
                        {
                            if (PSAL[i].val1 == 1)
                            {
                                RoutePoints.Add(MoveDistDirToXY(48, (int)(PSAL[i - 1].val2 + (45 * Madden.TeamPlaybook.PSAL.AngleRatio)), Offset, flipPSAL));
                            }
                            else if (PSAL[i].val1 == 2)
                            {
                                RoutePoints.Add(MoveDistDirToXY(48, (int)(PSAL[i - 1].val2 - (45 * Madden.TeamPlaybook.PSAL.AngleRatio)), Offset, flipPSAL));
                            }
                        }

                        //25 = StutterStreak
                        if (PSAL[i].val2 == 25)
                        {
                            if (PSAL[i].val1 == 1)
                            {
                                RoutePoints.Add(MoveDistDirToXY(4, (int)(PSAL[i - 1].val2 + (90 * Madden.TeamPlaybook.PSAL.AngleRatio)), Offset, flipPSAL));
                            }
                            else if (PSAL[i].val1 == 2)
                            {
                                RoutePoints.Add(MoveDistDirToXY(4, (int)(PSAL[i - 1].val2 - (90 * Madden.TeamPlaybook.PSAL.AngleRatio)), Offset, flipPSAL));
                            }
                        }

                        //26 = WR Swing
                        if (PSAL[i].val2 == 26)
                        {
                            if (PSAL[i].val1 == 1)
                            {
                                RoutePoints.Add(MoveDistDirToXY(24, (int)(-35 * Madden.TeamPlaybook.PSAL.AngleRatio), Offset, flipPSAL));
                                Offset = RoutePoints[RoutePoints.Count - 1];
                                RoutePoints.Add(MoveDistDirToXY(24, (int)(0 * Madden.TeamPlaybook.PSAL.AngleRatio), Offset, flipPSAL));
                            }
                            else if (PSAL[i].val1 == 2)
                            {
                                RoutePoints.Add(MoveDistDirToXY(24, (int)(215 * Madden.TeamPlaybook.PSAL.AngleRatio), Offset, flipPSAL));
                                Offset = RoutePoints[RoutePoints.Count - 1];
                                RoutePoints.Add(MoveDistDirToXY(24, (int)(180 * Madden.TeamPlaybook.PSAL.AngleRatio), Offset, flipPSAL));
                            }
                        }

                        //28 = Sluggo
                        if (PSAL[i].val2 == 28)
                        {
                            if (PSAL[i].val1 == 1)
                            {
                                RoutePoints.Add(MoveDistDirToXY(24, (int)(22 * Madden.TeamPlaybook.PSAL.AngleRatio), Offset, flipPSAL));
                            }
                            else if (PSAL[i].val1 == 2)
                            {
                                RoutePoints.Add(MoveDistDirToXY(24, (int)(158 * Madden.TeamPlaybook.PSAL.AngleRatio), Offset, flipPSAL));
                            }
                        }

                        //29 = Out n Up
                        if (PSAL[i].val2 == 29)
                        {
                            if (PSAL[i].val1 == 1)
                            {
                                RoutePoints.Add(MoveDistDirToXY(24, (int)(180 * Madden.TeamPlaybook.PSAL.AngleRatio), Offset, flipPSAL));
                            }
                            else if (PSAL[i].val1 == 2)
                            {
                                RoutePoints.Add(MoveDistDirToXY(24, (int)(0 * Madden.TeamPlaybook.PSAL.AngleRatio), Offset, flipPSAL));
                            }
                        }

                        if (artlColor.Equals(ARTLColor.Undefined)) artlColor = ARTLColor.BaseRoute;

                        break;

                    #endregion

                    case 10:
                        #region Receiver Get Open

                        break;

                    #endregion

                    case 11:
                        #region Pitch Ball?

                        artlColor = ARTLColor.QBHandoff;
                        break;

                    #endregion

                    case 12:
                        #region Option Handoff

                        artlColor = ARTLColor.QBHandoff;
                        break;

                    #endregion

                    case 13:
                        #region Receive Hand Off

                        if (RoutePoints.Count > 0) Offset = RoutePoints[RoutePoints.Count - 1];
                        RoutePoints.Add(MoveDistDirToXY(PSAL[i].val1, PSAL[i].val2, Offset, flipPSAL));
                        artlColor = ARTLColor.Run;
                        break;

                    #endregion

                    case 14:
                        #region PassBlock

                        if (PSAL[i].val1 == 0)
                        {
                            //assign route color
                            artlColor = ARTLColor.Block;

                            //manual offset of 2 yards back
                            if (i == 0)
                            {
                                RoutePoints.Add(MoveDistDirToXY(12, 96, Offset, flipPSAL));
                            }
                        }
                        else
                        {
                            //assign route color
                            artlColor = 
                                PSAL[i + 1].code != 255 ?
                                ARTLColor.DelayRoute :
                                artlColor = ARTLColor.Block;
                        }

                        break;

                    #endregion

                    case 15:
                        #region RunBlock

                        if (PSAL[i].val1 == 0)
                        {
                            //assign route color
                            artlColor = ARTLColor.Block;

                            //manual offset of 2 yards forward
                            if (i == 0)
                            {
                                RoutePoints.Add(MoveDistDirToXY(12, 32, Offset, flipPSAL));
                            }
                        }
                        else
                        {
                            //assign route color
                            artlColor =
                                PSAL[i + 1].code != 255 ?
                                ARTLColor.DelayRoute :
                                artlColor = ARTLColor.Block;
                        }

                        break;

                    #endregion

                    case 16:
                        #region Kickoff?

                        artlColor = ARTLColor.Kickoff;
                        break;

                    #endregion

                    case 18:
                        #region LeadBlock

                        artlColor = ARTLColor.Block;
                        break;

                    #endregion

                    case 19:
                        #region Man Coverage

                        break;

                    #endregion

                    case 20:
                        #region Cloud flat, Hard Flat, Soft Squat 

                        if (PSAL[i].val1 == 9) RoutePoints.Add(new Point { X = (int)((533 * .167) - 266.5 - XY.X), Y = -50 - XY.Y });
                        else if (PSAL[i].val1 == 10) RoutePoints.Add(new Point { X = (int)((533 * .833) - 266.5 - XY.X), Y = -50 - XY.Y });

                        switch (PSAL[i].val2)
                        {
                            case 0:
                                artlColor = ARTLColor.CloudFlat;
                                break;
                            case 1:
                                artlColor = ARTLColor.HardFlat;
                                break;
                            case 2:
                                artlColor = ARTLColor.SoftSquat;
                                break;
                            default:
                                artlColor = ARTLColor.CloudFlat;
                                break;
                        }
                        break;

                    #endregion

                    case 21:
                        #region Mid Read, 3 Rec Hook, Hook Curl, Vert Hook  

                        //convert to offest
                        if (RoutePoints.Count > 0) Offset = RoutePoints[RoutePoints.Count - 1];
                        RoutePoints.Add(MoveDistDirToXY(20, PSAL[i].val2, Offset, flipPSAL));

                        switch (PSAL[i].val1)
                        {
                            case 0:
                                RoutePoints.Add(new Point { X = (int)((533 * .5) - 266.5 - XY.X), Y = -200 - XY.Y });
                                artlColor = ARTLColor.MidRead;
                                break;
                            case 1:
                                RoutePoints.Add(new Point { X = (int)((533 * .5) - 266.5 - XY.X), Y = -150 - XY.Y });
                                artlColor = ARTLColor.ThreeReceiverHook;
                                break;
                            case 2:
                                RoutePoints.Add(new Point { X = (int)((533 * .333) - 266.5 - XY.X), Y = -100 - XY.Y });
                                artlColor = ARTLColor.HookCurl;
                                break;
                            case 3:
                                RoutePoints.Add(new Point { X = (int)((533 * .667) - 266.5 - XY.X), Y = -100 - XY.Y });
                                artlColor = ARTLColor.HookCurl;
                                break;
                            case 4:
                                RoutePoints.Add(new Point { X = (int)((533 * .333) - 266.5 - XY.X), Y = -100 - XY.Y });
                                artlColor = ARTLColor.VertHook;
                                break;
                            case 5:
                                RoutePoints.Add(new Point { X = (int)((533 * .667) - 266.5 - XY.X), Y = -100 - XY.Y });
                                artlColor = ARTLColor.VertHook;
                                break;
                            default:
                                artlColor = ARTLColor.HookCurl;
                                break;
                        }
                        break;

                    #endregion

                    case 22:
                        #region Curl Flat, Seam Flat, Quarter Flat  

                        if (PSAL[i].val1 == 11) RoutePoints.Add(new Point { X = (int)((533 * .167) - 266.5 - XY.X), Y = -100 - XY.Y });
                        else if (PSAL[i].val1 == 12) RoutePoints.Add(new Point { X = (int)((533 * .833) - 266.5 - XY.X), Y = -100 - XY.Y });

                        switch (PSAL[i].val2)
                        {
                            case 0:
                                artlColor = ARTLColor.CurlFlat;
                                break;
                            case 1:
                                artlColor = ARTLColor.SeamFlat;
                                break;
                            case 2:
                                artlColor = ARTLColor.QuarterFlat;
                                break;
                            default:
                                artlColor = ARTLColor.SeamFlat;
                                break;
                        }
                        break;

                    #endregion

                    case 23:
                        #region Deep Zone  

                        switch (PSAL[i].val1)
                        {
                            case 0:
                                RoutePoints.Add(new Point { X = (int)((533 * .25) - 266.5 - XY.X), Y = -300 - XY.Y });
                                break;
                            case 1:
                                RoutePoints.Add(new Point { X = (int)((533 * .75) - 266.5 - XY.X), Y = -300 - XY.Y });
                                break;
                            case 2:
                                RoutePoints.Add(new Point { X = (int)((533 * .167) - 266.5 - XY.X), Y = -300 - XY.Y });
                                break;
                            case 3:
                                RoutePoints.Add(new Point { X = (int)((533 * .5) - 266.5 - XY.X), Y = -300 - XY.Y });
                                break;
                            case 4:
                                RoutePoints.Add(new Point { X = (int)((533 * .833) - 266.5 - XY.X), Y = -300 - XY.Y });
                                break;
                            case 5:
                                RoutePoints.Add(new Point { X = (int)((533 * .125) - 266.5 - XY.X), Y = -300 - XY.Y });
                                break;
                            case 6:
                                RoutePoints.Add(new Point { X = (int)((533 * .375) - 266.5 - XY.X), Y = -300 - XY.Y });
                                break;
                            case 7:
                                RoutePoints.Add(new Point { X = (int)((533 * .625) - 266.5 - XY.X), Y = -300 - XY.Y });
                                break;
                            case 8:
                                RoutePoints.Add(new Point { X = (int)((533 * .875) - 266.5 - XY.X), Y = -300 - XY.Y });
                                break;
                            default:
                                ;
                                break;
                        }

                        artlColor = ARTLColor.DeepZone;

                        break;

                    #endregion

                    case 24:
                        #region Pass Rush  

                        if (RoutePoints.Count > 0) Offset = RoutePoints[RoutePoints.Count - 1];
                        //else Offset = new Point(-266.5 - XY.Y, -400 - XY.Y);
                        RoutePoints.Add(MoveDistDirToXY(PSAL[i].val2, PSAL[i].val1, Offset, flipPSAL));
                        artlColor = ARTLColor.RushQB;

                        break;

                    #endregion

                    case 25:
                        #region Delay

                        break;

                    #endregion

                    case 26:
                        #region Initial Anim

                        if (RoutePoints.Count > 0) Offset = RoutePoints[RoutePoints.Count - 1];

                        if (PSAL[i].val1 == 1 || PSAL[i].val1 == 4 || PSAL[i].val1 == 5 || PSAL[i].val1 == 6)
                        {
                            RoutePoints.Add(MoveDistDirToXY(4, PSAL[i].val2, Offset, flipPSAL));
                        }
                        else if (PSAL[i].val1 == 25)
                        {
                            RoutePoints.Add(MoveDistDirToXY(32, PSAL[i].val2, Offset, flipPSAL));
                        }
                        else if (PSAL[i].val1 == 15)
                        {
                            RoutePoints.Add(MoveDistDirToXY(PSAL[i].val2, 96, Offset, flipPSAL));
                        }

                        //if (artlColor.Equals(ARTLColor.Undefined)) artlColor = ARTLColor.BaseRoute;
                        break;

                    #endregion

                    case 27:
                        #region Punt?

                        break;

                    #endregion

                    case 28:
                        #region FG Spot?

                        break;

                    #endregion

                    case 29:
                        #region FG Kick?

                        break;

                    #endregion

                    case 30:
                        #region Stop Clock?

                        break;

                    #endregion

                    case 31:
                        #region Kneel?

                        break;

                    #endregion

                    case 32:
                        #region Receive Pitch

                        if (RoutePoints.Count > 0) Offset = RoutePoints[RoutePoints.Count - 1];
                        RoutePoints.Add(MoveDistDirToXY(PSAL[i].val1, PSAL[i].val2, Offset, flipPSAL));
                        artlColor = ARTLColor.Run;
                        break;

                    #endregion

                    case 34:
                        #region QB Spy

                        artlColor = ARTLColor.QBSpy;

                        break;

                    #endregion

                    case 35:
                        #region Head Turn Run Route

                        if (RoutePoints.Count > 0) Offset = RoutePoints[RoutePoints.Count - 1];
                        RoutePoints.Add(MoveDistDirToXY(PSAL[i].val1, PSAL[i].val2, Offset, flipPSAL));
                        break;

                    #endregion

                    case 36:
                        #region Option Route

                        //Option Route Base
                        if (RoutePoints.Count > 0) Offset = RoutePoints[RoutePoints.Count - 1];
                        RoutePoints.Add(GetOptionOffset(PSAL[i].val1, Offset, flipPSAL));

                        //Option route 1
                        PathGeometry option1Geo = new PathGeometry();
                        PathFigure option1Figure = new PathFigure();
                        PathSegmentCollection Option1Segments = new PathSegmentCollection();
                        option1Figure.Segments = Option1Segments;
                        option1Geo.Figures.Add(option1Figure);
                        PointCollection Option1Points = new PointCollection();
                        PSALpath.Add(new Path());
                        PSALpath[PSALpath.Count - 1].Data = option1Geo;
                        option1Figure.StartPoint = RoutePoints.Count > 1 ? RoutePoints[RoutePoints.Count - 2] : RouteFigure.StartPoint;
                        PolyLineSegment option1_segment = new PolyLineSegment();
                        option1_segment.IsSmoothJoin = true;
                        Option1Segments.Add(option1_segment);
                        option1_segment.Points = Option1Points;
                        Option1Points.Add(GetOptionOffset(PSAL[i].val2, Offset, flipPSAL));

                        //Option route 2
                        if (PSAL[i].val3 != 255)
                        {
                            PathGeometry option2Geo = new PathGeometry();
                            PathFigure option2Figure = new PathFigure();
                            PathSegmentCollection Option2Segments = new PathSegmentCollection();
                            option2Figure.Segments = Option2Segments;
                            option2Geo.Figures.Add(option2Figure);
                            PointCollection Option2Points = new PointCollection();
                            PSALpath.Add(new Path());
                            PSALpath[PSALpath.Count - 1].Data = option2Geo;
                            option2Figure.StartPoint = option1Figure.StartPoint;
                            PolyLineSegment option2_segment = new PolyLineSegment();
                            option2_segment.IsSmoothJoin = true;
                            Option2Segments.Add(option2_segment);
                            option2_segment.Points = Option2Points;
                            Option2Points.Add(GetOptionOffset(PSAL[i].val3, Offset, flipPSAL));
                        }

                        break;

                    #endregion

                    case 37:
                        #region Option Route Extra Info

                        break;

                    #endregion

                    case 38:
                        #region Handoff Turn?

                        artlColor = ARTLColor.QBHandoff;
                        break;

                    #endregion

                    case 39:
                        #region Handoff Give?

                        artlColor = ARTLColor.QBHandoff;
                        break;

                    #endregion

                    case 40:
                        #region Option Run?

                        artlColor = ARTLColor.Run;
                        break;

                    #endregion

                    case 41:
                        #region Rush QB

                        if (SRFT != null)
                        {
                            switch (SRFT.GAPS)
                            {
                                case 1:
                                    RoutePoints.Add(new Point { X = (16.5 * .5) - XY.X, Y = 15 - XY.Y });
                                    break;
                                case 2:
                                    RoutePoints.Add(new Point { X = (-16.5 * .5) - XY.X, Y = 15 - XY.Y });
                                    break;
                                case 3:
                                    RoutePoints.Add(new Point { X = -XY.X, Y = 15 - XY.Y });
                                    break;
                                case 4:
                                    RoutePoints.Add(new Point { X = (16.5 * 1.5) - XY.X, Y = 15 - XY.Y });
                                    break;
                                case 5:
                                    RoutePoints.Add(new Point { X = 16.5 - XY.X, Y = 15 - XY.Y });
                                    break;
                                case 8:
                                    RoutePoints.Add(new Point { X = (-16.5 * 1.5) - XY.X, Y = 15 - XY.Y });
                                    break;
                                case 10:
                                    RoutePoints.Add(new Point { X = 16.5 - XY.X, Y = 15 - XY.Y });
                                    break;
                                case 16:
                                    RoutePoints.Add(new Point { X = (16.5 * 2.5) - XY.X, Y = 15 - XY.Y });
                                    break;
                                case 20:
                                    RoutePoints.Add(new Point { X = (16.5 * 2) - XY.X, Y = 15 - XY.Y });
                                    break;
                                case 32:
                                    RoutePoints.Add(new Point { X = (-16.5 * 2.5) - XY.X, Y = 15 - XY.Y });
                                    break;
                                case 40:
                                    RoutePoints.Add(new Point { X = (-16.5 * 2) - XY.X, Y = 15 - XY.Y });
                                    break;
                                case 64:
                                    RoutePoints.Add(new Point { X = (16.5 * 3.5) - XY.X, Y = 15 - XY.Y });
                                    break;
                                case 128:
                                    RoutePoints.Add(new Point { X = (-16.5 * 3.5) - XY.X, Y = 15 - XY.Y });
                                    break;
                                case 256:
                                    RoutePoints.Add(new Point { X = (16.5 * 4.5) - XY.X, Y = 15 - XY.Y });
                                    break;
                                case 512:
                                    RoutePoints.Add(new Point { X = (-16.5 * 4.5) - XY.X, Y = 15 - XY.Y });
                                    break;
                                default:
                                    break;
                            }

                            if (artlColor.Equals(ARTLColor.Undefined)) artlColor = ARTLColor.RushQB;
                        }
                        else
                        {
                            if (RoutePoints.Count > 0) Offset = RoutePoints[RoutePoints.Count - 1];
                            RoutePoints.Add(MoveDistDirToXY(PSAL[i].val1, PSAL[i].val2, Offset, flipPSAL));
                            artlColor = ARTLColor.Run;
                        }

                        break;

                    #endregion

                    case 42:
                        #region Hand Off Fake?

                        artlColor = ARTLColor.QBScramble;
                        break;

                    #endregion

                    case 43:
                        #region Option Follow

                        artlColor = ARTLColor.Run;
                        break;

                    #endregion

                    case 44:
                        #region Wedge Block

                        artlColor = ARTLColor.Block;

                        break;

                    #endregion

                    case 45:
                        #region Auto Motion Offense

                        if (flipPSAL)
                        {
                            RoutePoints.Add(
                                new Point
                                {
                                    X = ((PSAL[i].val1 / 5.6667) - SETG.fx__) * 10 * -2,
                                    Y = (Math.Abs(PSAL[i].val2 / 5.6667) + SETG.fy__) * 10
                                });
                        }
                        else
                        {
                            RoutePoints.Add(
                                new Point
                                {
                                    X = ((PSAL[i].val1 / 5.6667) - SETG.x___) * 10,
                                    Y = (Math.Abs(PSAL[i].val2 / 5.6667) + SETG.y___) * 10
                                });
                        }
                        artlColor = ARTLColor.MotionRoute;
                        break;

                    #endregion

                    case 46:
                        #region Auto Motion Snap

                        break;

                    #endregion

                    case 47:
                        #region Auto Motion Defense

                        if (flipPSAL)
                        {
                            RoutePoints.Add(
                                new Point
                                {
                                    X = (PSAL[i].val1 * -3.5294) - XY.X,
                                    Y = (PSAL[i].val2 * -1.7647) - XY.Y
                                });
                        }
                        else
                        {
                            RoutePoints.Add(
                                new Point
                                {
                                    X = (PSAL[i].val1 * 1.7647) - XY.X,
                                    Y = (PSAL[i].val2 * -1.7647) - XY.Y
                                });
                        }
                        artlColor = ARTLColor.MotionRoute;
                        break;

                    #endregion

                    case 48:
                        #region Player offset

                        //val1 to X = 9/51 = 1.7647
                        //val2 to Y = .8/3 = 2.6667
                        //XY = new Point(PSAL[i].val1 * 1.7647, PSAL[i].val2 * -2.6667);
                        //if (flipPSAL) RoutePoints[RoutePoints.Count - 1].Offset(RoutePoints[RoutePoints.Count - 1].X * -2, RoutePoints[RoutePoints.Count - 1].Y);

                        if (flipPSAL)
                        {
                            RoutePoints.Add(
                                new Point
                                {
                                    X = (PSAL[i].val1 * -1.7647) - XY.X,
                                    Y = (PSAL[i].val2 * -2.6667) - XY.Y
                                });
                        }
                        else
                        {
                            RoutePoints.Add(
                                new Point
                                {
                                    X = (PSAL[i].val1 * 1.7647) - XY.X,
                                    Y = (PSAL[i].val2 * -2.6667) - XY.Y
                                });
                        }

                        break;

                    #endregion

                    case 57:
                        #region Animation Defense

                        break;

                    #endregion

                    case 58:
                        #region Animation Offense

                        break;

                    #endregion

                    case 255:
                        #region End of Route

                        break;

                        #endregion
                }
            }

            //foreach (Path path in PSALpath) ((PathGeometry)path.Data).Freeze();

            GetRouteDepth(RoutePoints);

            this.PSALpath = PSALpath;
        }

        public void ConvertARTL(ARTL ARTL)
        {
            List<Path> _ARTLpath = new List<Path>();
            if (ARTL == null)
            {
                //MessageBox.Show(
                //    Play.SubFormation.Formation.PBFM.name + " " +
                //    Play.SubFormation.PBST.name + " - " +
                //    Play.PBPL.name + "\n\n" +
                //    DPos + EPos +
                //    "\nPSAL: " + PLYS.PSAL +
                //    "\nARTL: " + PLYS.ARTL,
                //    "Missing ARTL"
                //);

                ARTL = new ARTL
                {
                    ARTList = new List<PlayArt>()
                };
            }
            else
            {
                List<int> routeIndices = new List<int>();
                for (int i = 0; i <= 11; i++)
                {
                    if (ARTL.ARTList[i].ct != 0)
                    {
                        routeIndices.Add(i);
                    }
                }
                int optionStartIndex =
                    routeIndices.Count != 0 ?
                    routeIndices[0] == 0 ?
                    0 :
                    routeIndices[0] - 1 :
                    0;

                foreach (int step in routeIndices)
                {
                    PathGeometry RouteGeo = new PathGeometry();
                    PathFigure RouteFigure = new PathFigure();
                    PathSegmentCollection RouteSegments = new PathSegmentCollection();
                    RouteFigure.Segments = RouteSegments;
                    RouteGeo.Figures.Add(RouteFigure);
                    PointCollection RoutePoints = new PointCollection();
                    _ARTLpath.Add(new Path());
                    _ARTLpath[_ARTLpath.Count - 1].Data = RouteGeo;

                    if (routeIndices.IndexOf(step) == 0)
                    {
                        RouteFigure.StartPoint = new Point { X = 0, Y = 0 };
                    }
                    else
                    {
                        RouteFigure.StartPoint =
                            ARTL.ARTList[optionStartIndex].au == 0 && ARTL.ARTList[optionStartIndex].av == 0 ?
                            new Point { X = ARTL.ARTList[optionStartIndex].ax, Y = ARTL.ARTList[optionStartIndex].ay } :
                            new Point { X = ARTL.ARTList[optionStartIndex].au, Y = ARTL.ARTList[optionStartIndex].av };
                    }

                    for (int i = routeIndices.IndexOf(step) == 0 ? 0 : routeIndices[routeIndices.IndexOf(step) - 1] + 1; i <= step; i++)
                    {
                        if (ARTL.ARTList[i].au == 0 && ARTL.ARTList[i].av == 0)
                        {
                            RoutePoints.Add(new Point { X = ARTL.ARTList[i].ax, Y = ARTL.ARTList[i].ay });
                            PolyLineSegment line_segment = new PolyLineSegment();
                            line_segment.IsSmoothJoin = true;
                            RouteSegments.Add(line_segment);

                            if (ARTL.ARTList[i].ct != 0 || ARTL.ARTList[i + 1].au != 0 || ARTL.ARTList[i + 1].av != 0)
                            {
                                line_segment.Points = RoutePoints.Clone();
                                RoutePoints.Clear();
                            }
                        }
                        else
                        {
                            if (i == 0)
                            {
                                RoutePoints.Add(new Point { X = 0, Y = 0 });
                            }
                            else
                            {
                                if (ARTL.ARTList[i - 1].au == 0 && ARTL.ARTList[i - 1].av == 0)
                                {
                                    RoutePoints.Add(new Point { X = ARTL.ARTList[i - 1].ax, Y = ARTL.ARTList[i - 1].ay });
                                }
                                else
                                {
                                    RoutePoints.Add(new Point { X = ARTL.ARTList[i - 1].au, Y = ARTL.ARTList[i - 1].av });
                                }
                            }

                            RoutePoints.Add(new Point { X = ARTL.ARTList[i].ax, Y = ARTL.ARTList[i].ay });
                            RoutePoints.Add(new Point { X = ARTL.ARTList[i].au, Y = ARTL.ARTList[i].av });

                            PolyBezierSegment bezier_segment = new PolyBezierSegment();
                            bezier_segment.IsSmoothJoin = true;
                            RouteSegments.Add(bezier_segment);

                            if (ARTL.ARTList[i].ct != 0)
                            {
                                bezier_segment.Points = RoutePoints.Clone();
                                RoutePoints.Clear();
                            }
                        }
                    }
                }
            }

            foreach (Path path in _ARTLpath) ((PathGeometry)path.Data).Freeze();

            ARTLpath = _ARTLpath;
        }

        public static Point MoveDistDirToXY(int dist, int dir, Point Offset, bool flipPSAL)
        {
            double angle = Math.PI * (dir / Madden.TeamPlaybook.PSAL.AngleRatio) / 180.0;
            double sinAngle = Math.Sin(angle);
            double cosAngle = Math.Cos(angle);
            Point XY = new Point();

            //if (dist > 128)
            //{
            //    dist = (int)(dist * .5);
            //}

            XY.X = (int)(cosAngle * (dist / .8));
            XY.Y = (int)(sinAngle * (dist / .8)) * -1;

            XY.Offset(Offset.X, Offset.Y);

            if (flipPSAL) XY.Offset(XY.X * -2, Offset.Y);

            return XY;
        }

        public static PSAL XYtoMoveDistDir(Point XY)
        {
            PSAL PSAL = new PSAL();
            PSAL.val1 = (int)((Math.Sqrt(Math.Pow(XY.X, 2) + Math.Pow(XY.Y, 2))) * .8);
            PSAL.val2 = (int)(Math.Atan2(XY.Y, XY.X) / Math.PI * 180 * Madden.TeamPlaybook.PSAL.AngleRatio);
            if (PSAL.val2 < 0)
            {
                PSAL.val2 = 128 + PSAL.val2;
            }

            return PSAL;
        }

        public static Point GetOptionOffset(int code, Point Offset, bool flipPSAL)
        {
            if (code == 0)     //Curl left
            {
                return MoveDistDirToXY(16, (int)(225 * Madden.TeamPlaybook.PSAL.AngleRatio), Offset, flipPSAL);
            }

            if (code == 1)    //Curl right
            {
                return MoveDistDirToXY(16, (int)(-45 * Madden.TeamPlaybook.PSAL.AngleRatio), Offset, flipPSAL);
            }

            if (code == 2)    //Post right
            {
                return MoveDistDirToXY(90, (int)(45 * Madden.TeamPlaybook.PSAL.AngleRatio), Offset, flipPSAL);
            }

            if (code == 3)    //Corner left
            {
                return MoveDistDirToXY(90, (int)(135 * Madden.TeamPlaybook.PSAL.AngleRatio), Offset, flipPSAL);
            }

            if (code == 5)     //Slant right
            {
                return MoveDistDirToXY(90, (int)(33 * Madden.TeamPlaybook.PSAL.AngleRatio), Offset, flipPSAL);
            }

            if (code == 6)    //Fade/Streak left
            {
                return MoveDistDirToXY(90, (int)(93 * Madden.TeamPlaybook.PSAL.AngleRatio), Offset, flipPSAL);
            }

            if (code == 7)    //Slant left
            {
                return MoveDistDirToXY(90, (int)(147 * Madden.TeamPlaybook.PSAL.AngleRatio), Offset, flipPSAL);
            }

            if (code == 8)    //Fade/Streak right
            {
                return MoveDistDirToXY(90, (int)(87 * Madden.TeamPlaybook.PSAL.AngleRatio), Offset, flipPSAL);
            }

            if (code == 9)     //In/Out right
            {
                return MoveDistDirToXY(90, (int)(0 * Madden.TeamPlaybook.PSAL.AngleRatio), Offset, flipPSAL);
            }

            if (code == 10)    //In/Out left
            {
                return MoveDistDirToXY(90, (int)(180 * Madden.TeamPlaybook.PSAL.AngleRatio), Offset, flipPSAL);
            }

            if (code == 11)   //Fade left
            {
                return MoveDistDirToXY(90, (int)(93 * Madden.TeamPlaybook.PSAL.AngleRatio), Offset, flipPSAL);
            }

            if (code == 12)    //Hitch left
            {
                return MoveDistDirToXY(16, (int)(225 * Madden.TeamPlaybook.PSAL.AngleRatio), Offset, flipPSAL);
            }

            if (code == 13)    //Hitch right
            {
                return MoveDistDirToXY(16, (int)(-45 * Madden.TeamPlaybook.PSAL.AngleRatio), Offset, flipPSAL);
            }

            if (code == 15)   //180 Partial
            {
                return MoveDistDirToXY(16, (int)(270 * Madden.TeamPlaybook.PSAL.AngleRatio), Offset, flipPSAL);
            }

            if (code == 16)   //180 Partial
            {
                return MoveDistDirToXY(16, (int)(270 * Madden.TeamPlaybook.PSAL.AngleRatio), Offset, flipPSAL);
            }

            if (code == 17)   //Drag right
            {
                return MoveDistDirToXY(90, (int)(3 * Madden.TeamPlaybook.PSAL.AngleRatio), Offset, flipPSAL);
            }

            if (code == 18)   //Drag left
            {
                return MoveDistDirToXY(90, (int)(177 * Madden.TeamPlaybook.PSAL.AngleRatio), Offset, flipPSAL);
            }

            if (code == 19)   //Hitch right
            {
                return MoveDistDirToXY(16, (int)(-45 * Madden.TeamPlaybook.PSAL.AngleRatio), Offset, flipPSAL);
            }

            if (code == 20)   //Hitch left
            {
                return MoveDistDirToXY(16, (int)(225 * Madden.TeamPlaybook.PSAL.AngleRatio), Offset, flipPSAL);
            }

            return new Point { X = 0, Y = 0 };
        }

        public static Point[] XYtoPoint(List<Point> xy)
        {
            Point[] points = new Point[xy.Count];

            for (int i = 0; i < xy.Count; i++)
            {
                points[i].X = xy[i].X;
                points[i].Y = xy[i].Y;
            }

            return points;
        }

        #region IsSelected

        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if (value != _isSelected)
                {
                    _isSelected = value;
                    foreach (PlayVM play in Play.SubFormation.Plays)
                    {
                        play.Players.FirstOrDefault(poso => poso.PLYS.poso == PLYS.poso).IsSelected = value;
                    }
                    this.OnPropertyChanged("IsSelected");
                }
            }
        }

        #endregion // IsSelected

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