using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using TDBAccess;

namespace Madden.TeamPlaybook
{
    [Serializable]
    public class ARTL
    {
        public override string ToString()
        {
            string ARTL_string = "rec: " + rec +
                "   artl: " + artl +
                "   acnt: " + acnt + "\n";
            for (int i = 0; i < ARTList.Count; i++) ARTL_string += "\t" + i.ToString() + "\t" + ARTList[i] + "\n";
            return ARTL_string;
        }

        public int rec { get; set; }
        public int artl { get; set; }
        public int acnt { get; set; }

        public List<PlayArt> ARTList { get; set; }

        public static PathGeometry Block
        {
            get
            {
                return new RectangleGeometry(new Rect(new Point(0, -4.5), new Size(3, 9))).GetFlattenedPathGeometry();
            }
        }

        public static PathGeometry Arrow
        {
            get
            {
                PathGeometry RouteCap = new PathGeometry();
                PathFigure CapFigure = new PathFigure();
                PathSegmentCollection CapSegments = new PathSegmentCollection();
                PointCollection CapPoints = new PointCollection();
                PolyLineSegment line_segment = new PolyLineSegment();

                CapFigure.Segments = CapSegments;
                RouteCap.Figures.Add(CapFigure);

                CapFigure.StartPoint = new System.Windows.Point { X = 10, Y = 0 };
                CapPoints.Add(new System.Windows.Point { X = 0, Y = -5 });
                CapPoints.Add(new System.Windows.Point { X = 0, Y = 5 });

                line_segment.Points = CapPoints;
                CapSegments.Add(line_segment);

                return RouteCap;
            }
        }

        public static PathGeometry QuarterHookFlat
        {
            get
            {
                return new EllipseGeometry(new Rect(new System.Windows.Point(-16, -6.8555), new System.Windows.Size(32, 13.711f))).GetFlattenedPathGeometry();
            }
        }

        public static PathGeometry DeepThird
        {
            get
            {
                return new EllipseGeometry(new Rect(new System.Windows.Point(-29.53125, -9.66796875f), new System.Windows.Size(59.0625f, 19.3359375f))).GetFlattenedPathGeometry();
            }
        }

        public static PathGeometry DeepHalf
        {
            get
            {
                return new EllipseGeometry(new Rect(new System.Windows.Point(-39.0234375, -10.546875), new System.Windows.Size(78.046875f, 21.09375f))).GetFlattenedPathGeometry();
            }
        }

        public static PathGeometry QBSpy
        {
            get
            {
                return new EllipseGeometry(new Rect(new System.Windows.Point(-10.546875, -6.6796875), new System.Windows.Size(21.09375f, 13.359375f))).GetFlattenedPathGeometry();
            }
        }

        public static List<ARTL> GetARTL(int filter = 0, int DBIndex = 0)
        {
            List<ARTL> _ARTL = new List<ARTL>();

            TdbTableProperties TableProps = new TdbTableProperties();
            TableProps.Name = new string((char)0, 5);

            // Get Tableprops based on the selected index
            if (!TDB.TDBTableGetProperties(DBIndex, TableNames.GetTables().Find(item => item.name == "ARTL").rec, ref TableProps))
                return null;

            for (int i = 0; i < TableProps.RecordCount; i++)
            {
                if (filter != 0 && (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("PLPD"), TDB.StrReverse("PLYL"), i) == filter)
                {
                    _ARTL.Add(new ARTL
                    {
                        rec = i,
                        artl = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("ARTL"), TDB.StrReverse("ARTL"), i),
                        acnt = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("ARTL"), TDB.StrReverse("acnt"), i)
                    });
                    _ARTL[i].ARTList = new List<PlayArt>();
                    for (int x = 1; x <= 12; x++)
                    {
                        string ARTLstep;
                        if (x < 10) ARTLstep = "0" + x.ToString();
                        else ARTLstep = x.ToString();
                        _ARTL[i].ARTList.Add(new PlayArt
                        {
                            sp = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("ARTL"), TDB.StrReverse("sp" + ARTLstep), i),
                            ls = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("ARTL"), TDB.StrReverse("ls" + ARTLstep), i),
                            at = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("ARTL"), TDB.StrReverse("at" + ARTLstep), i),
                            ct = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("ARTL"), TDB.StrReverse("ct" + ARTLstep), i),
                            lt = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("ARTL"), TDB.StrReverse("lt" + ARTLstep), i),
                            au = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("ARTL"), TDB.StrReverse("au" + ARTLstep), i),
                            av = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("ARTL"), TDB.StrReverse("av" + ARTLstep), i),
                            ax = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("ARTL"), TDB.StrReverse("ax" + ARTLstep), i),
                            ay = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("ARTL"), TDB.StrReverse("ay" + ARTLstep), i),
                        });
                    }
                }
                else if (filter == 0)
                {
                    _ARTL.Add(new ARTL
                    {
                        rec = i,
                        artl = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("ARTL"), TDB.StrReverse("ARTL"), i),
                        acnt = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("ARTL"), TDB.StrReverse("acnt"), i)
                    });
                    _ARTL[i].ARTList = new List<PlayArt>();
                    for (int x = 1; x <= 12; x++)
                    {
                        string ARTLstep;
                        if (x < 10) ARTLstep = "0" + x.ToString();
                        else ARTLstep = x.ToString();
                        _ARTL[i].ARTList.Add(new PlayArt
                        {
                            sp = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("ARTL"), TDB.StrReverse("sp" + ARTLstep), i),
                            ls = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("ARTL"), TDB.StrReverse("ls" + ARTLstep), i),
                            at = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("ARTL"), TDB.StrReverse("at" + ARTLstep), i),
                            ct = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("ARTL"), TDB.StrReverse("ct" + ARTLstep), i),
                            lt = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("ARTL"), TDB.StrReverse("lt" + ARTLstep), i),
                            au = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("ARTL"), TDB.StrReverse("au" + ARTLstep), i),
                            av = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("ARTL"), TDB.StrReverse("av" + ARTLstep), i),
                            ax = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("ARTL"), TDB.StrReverse("ax" + ARTLstep), i),
                            ay = (int)(UInt32)TDB.TDBFieldGetValueAsInteger(DBIndex, TDB.StrReverse("ARTL"), TDB.StrReverse("ay" + ARTLstep), i),
                        });
                    }
                }
            }

            return _ARTL;
        }

        public static void SetARTL(List<ARTL> ARTL, int DBIndex = 0)
        {
            TdbTableProperties TableProps = new TdbTableProperties();
            TableProps.Name = new string((char)0, 5);

            // Get Tableprops based on the selected index
            TDB.TDBTableGetProperties(DBIndex, TableNames.GetTables().Find(item => item.name == "ARTL").rec, ref TableProps);

            if (ARTL.Count > TableProps.RecordCount)
            {
                for (int i = TableProps.RecordCount - 1; i < ARTL.Count - 1; i++)
                {
                    TDB.TDBTableRecordAdd(DBIndex, TDB.StrReverse("ARTL"), true);
                }
            }
            else if (ARTL.Count < TableProps.RecordCount)
            {
                for (int i = TableProps.RecordCount - 1; i > ARTL.Count - 1; i--)
                {
                    TDB.TDBTableRecordRemove(DBIndex, TDB.StrReverse("ARTL"), i);
                }
            }

            foreach (ARTL item in ARTL)
            {
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("ARTL"), TDB.StrReverse("ARTL"), item.rec, item.artl);
                TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("ARTL"), TDB.StrReverse("acnt"), item.rec, item.acnt);
                for (int x = 1; x <= 12; x++)
                {
                    string ARTLstep;
                    if (x < 10)
                    {
                        ARTLstep = "0" + x.ToString();
                    }
                    else
                    {
                        ARTLstep = x.ToString();
                    }
                    TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("ARTL"), TDB.StrReverse("sp" + ARTLstep), item.rec, item.ARTList[x - 1].sp);
                    TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("ARTL"), TDB.StrReverse("ls" + ARTLstep), item.rec, item.ARTList[x - 1].ls);
                    TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("ARTL"), TDB.StrReverse("at" + ARTLstep), item.rec, item.ARTList[x - 1].at);
                    TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("ARTL"), TDB.StrReverse("ct" + ARTLstep), item.rec, item.ARTList[x - 1].ct);
                    TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("ARTL"), TDB.StrReverse("lt" + ARTLstep), item.rec, item.ARTList[x - 1].lt);
                    TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("ARTL"), TDB.StrReverse("au" + ARTLstep), item.rec, item.ARTList[x - 1].au);
                    TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("ARTL"), TDB.StrReverse("av" + ARTLstep), item.rec, item.ARTList[x - 1].av);
                    TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("ARTL"), TDB.StrReverse("ax" + ARTLstep), item.rec, item.ARTList[x - 1].ax);
                    TDB.TDBFieldSetValueAsInteger(DBIndex, TDB.StrReverse("ARTL"), TDB.StrReverse("ay" + ARTLstep), item.rec, item.ARTList[x - 1].ay);
                }
            }
        }

        public static List<ARTL> Sort(List<ARTL> ARTL)
        {
            return ARTL.OrderBy(s => s.artl).Cast<ARTL>().ToList();
        }

        public bool IsIdentical(ARTL _artl)
        {
            int same =
                ((rec == _artl.rec) ? 1 : 0) *
                ((artl == _artl.artl) ? 1 : 0) *
                ((acnt == _artl.acnt) ? 1 : 0);
            for (int i = 0; i < 12; i++)
            {
                same = same *
                ((_artl.ARTList[i].sp == ARTList[i].sp) ? 1 : 0) *
                ((_artl.ARTList[i].ls == ARTList[i].ls) ? 1 : 0) *
                ((_artl.ARTList[i].at == ARTList[i].at) ? 1 : 0) *
                ((_artl.ARTList[i].ct == ARTList[i].ct) ? 1 : 0) *
                ((_artl.ARTList[i].lt == ARTList[i].lt) ? 1 : 0) *
                ((_artl.ARTList[i].au == ARTList[i].au) ? 1 : 0) *
                ((_artl.ARTList[i].av == ARTList[i].av) ? 1 : 0) *
                ((_artl.ARTList[i].ax == ARTList[i].ax) ? 1 : 0) *
                ((_artl.ARTList[i].ay == ARTList[i].ay) ? 1 : 0);
            }
            return Convert.ToBoolean(same);
        }

        public bool IsIdentical(MaddenCustomPlaybookEditor.ARTL _artl)
        {
            int same =
                ((rec == _artl.rec) ? 1 : 0) *
                ((artl == _artl.artl) ? 1 : 0) *
                ((acnt == _artl.acnt) ? 1 : 0);
            for (int i = 0; i < 12; i++)
            {
                same = same *
                ((_artl.sp[i] == ARTList[i].sp) ? 1 : 0) *
                ((_artl.ls[i] == ARTList[i].ls) ? 1 : 0) *
                ((_artl.at[i] == ARTList[i].at) ? 1 : 0) *
                ((_artl.ct[i] == ARTList[i].ct) ? 1 : 0) *
                ((_artl.lt[i] == ARTList[i].lt) ? 1 : 0) *
                ((_artl.au[i] == ARTList[i].au) ? 1 : 0) *
                ((_artl.av[i] == ARTList[i].av) ? 1 : 0) *
                ((_artl.ax[i] == ARTList[i].ax) ? 1 : 0) *
                ((_artl.ay[i] == ARTList[i].ay) ? 1 : 0);
            }
            return Convert.ToBoolean(same);
        }
    }

    [Serializable]
    public class PlayArt
    {
        public override string ToString()
        {
            return
                "sp: " + "[" + string.Join(", ", sp) + "]\t" +
                "ls: " + "[" + string.Join(", ", ls) + "]\t" +
                "at: " + "[" + string.Join(", ", at) + "]\t" +
                "ct: " + "[" + string.Join(", ", ct) + "]\t" +
                "lt: " + "[" + string.Join(", ", lt) + "]\t" +
                "au: " + "[" + string.Join(", ", au) + "]\t" +
                "av: " + "[" + string.Join(", ", av) + "]\t" +
                "ax: " + "[" + string.Join(", ", ax) + "]\t" +
                "ay: " + "[" + string.Join(", ", ay) + "]";
        }

        public int sp;
        public int ls;
        public int at;
        public int ct;
        public int lt;
        public int au;
        public int av;
        public int ax;
        public int ay;
    }

    [Serializable]
    public class ARTLColor
    {
        public bool Equals(ARTLColor other)
        {
            if (other == null) return false;

            return Type == other.Type;
        }

        public int Order { get; set; }
        public string Type { get; set; }
        public System.Windows.Media.Color Color { get; set; }

        public static System.Windows.Media.Color PlayerIconColor = Colors.Ivory;
        public static System.Windows.Media.Color PlayerHighlightColor = System.Windows.Media.Color.FromArgb(255, 64, 0, 255);

        public ARTLColor()
        {

        }

        public ARTLColor(System.Windows.Media.Color color)
        {
            Color = color;
        }

        public static int Brightness(System.Windows.Media.Color c)
        {
            return (int)Math.Sqrt(
               c.R * c.R * .241 +
               c.G * c.G * .691 +
               c.B * c.B * .068);
        }

        public static ARTLColor Undefined
        {
            get
            {
                return new ARTLColor() { Order = 20, Type = "Undefined", Color = Colors.Transparent };
            }
        }

        public static ARTLColor Block
        {
            get
            {
                return new ARTLColor() { Order = 1, Type = "Block", Color = System.Windows.Media.Color.FromArgb(255, 200, 200, 200) };
            }
        }

        public static ARTLColor BaseRoute
        {
            get
            {
                return new ARTLColor() { Order = 4, Type = "Base Route", Color = System.Windows.Media.Color.FromArgb(255, 195, 177, 82) };
            }
        }

        public static ARTLColor PrimaryRoute
        {
            get
            {
                return new ARTLColor() { Order = 5, Type = "Primary Route", Color = System.Windows.Media.Color.FromArgb(255, 219, 65, 85) };
            }
        }

        public static ARTLColor DelayRoute
        {
            get
            {
                return new ARTLColor() { Order = 6, Type = "Delay Route", Color = System.Windows.Media.Color.FromArgb(255, 52, 111, 247) };
            }
        }

        public static ARTLColor MotionRoute
        {
            get
            {
                return new ARTLColor() { Order = 7, Type = "Motion Route", Color = System.Windows.Media.Color.FromArgb(255, 156, 227, 241) };
            }
        }

        public static ARTLColor Run
        {
            get
            {
                return new ARTLColor() { Order = 8, Type = "Run", Color = System.Windows.Media.Color.FromArgb(255, 219, 65, 85) };
            }
        }

        public static ARTLColor QBScramble
        {
            get
            {
                return new ARTLColor() { Order = 2, Type = "QB Scramble", Color = System.Windows.Media.Color.FromArgb(255, 19, 232, 132) };
            }
        }

        public static ARTLColor QBHandoff
        {
            get
            {
                return new ARTLColor() { Order = 3, Type = "QB Handoff", Color = System.Windows.Media.Color.FromArgb(255, 219, 65, 85) };
            }
        }

        public static ARTLColor Kickoff
        {
            get
            {
                return new ARTLColor() { Order = 9, Type = "Kickoff", Color = System.Windows.Media.Color.FromArgb(255, 219, 65, 85) };
            }
        }

        public static ARTLColor CloudFlat
        {
            get
            {
                return new ARTLColor() { Order = 3, Type = "Cloud Flat", Color = System.Windows.Media.Color.FromArgb(255, 8, 194, 219) };
            }
        }

        public static ARTLColor HardFlat
        {
            get
            {
                return new ARTLColor() { Order = 4, Type = "Hard Flat", Color = System.Windows.Media.Color.FromArgb(255, 90, 163, 170) };
            }
        }

        public static ARTLColor SoftSquat
        {
            get
            {
                return new ARTLColor() { Order = 5, Type = "Soft Squat", Color = System.Windows.Media.Color.FromArgb(255, 153, 234, 244) };
            }
        }

        public static ARTLColor MidRead
        {
            get
            {
                return new ARTLColor() { Order = 12, Type = "Mid Read", Color = System.Windows.Media.Color.FromArgb(255, 165, 189, 107) };
            }
        }

        public static ARTLColor ThreeReceiverHook
        {
            get
            {
                return new ARTLColor() { Order = 11, Type = "Three Receiver", Color = System.Windows.Media.Color.FromArgb(255, 219, 243, 159) };
            }
        }

        public static ARTLColor HookCurl
        {
            get
            {
                return new ARTLColor() { Order = 9, Type = "Hook Curl", Color = System.Windows.Media.Color.FromArgb(255, 231, 239, 192) };
            }
        }

        public static ARTLColor VertHook
        {
            get
            {
                return new ARTLColor() { Order = 10, Type = "Vert Hook", Color = System.Windows.Media.Color.FromArgb(255, 214, 227, 148) };
            }
        }

        public static ARTLColor CurlFlat
        {
            get
            {
                return new ARTLColor() { Order = 6, Type = "Curl Flat", Color = System.Windows.Media.Color.FromArgb(255, 145, 62, 222) };
            }
        }

        public static ARTLColor SeamFlat
        {
            get
            {
                return new ARTLColor() { Order = 7, Type = "Seam Flat", Color = System.Windows.Media.Color.FromArgb(255, 189, 128, 245) };
            }
        }

        public static ARTLColor QuarterFlat
        {
            get
            {
                return new ARTLColor() { Order = 8, Type = "Quarter Flat", Color = System.Windows.Media.Color.FromArgb(255, 255, 182, 247) };
            }
        }

        public static ARTLColor DeepZone
        {
            get
            {
                return new ARTLColor() { Order = 2, Type = "Deep Zone", Color = System.Windows.Media.Color.FromArgb(255, 49, 109, 247) };
            }
        }

        public static ARTLColor QBSpy
        {
            get
            {
                return new ARTLColor() { Order = 13, Type = "QB Spy", Color = System.Windows.Media.Color.FromArgb(255, 200, 120, 16) };
            }
        }

        public static ARTLColor RushQB
        {
            get
            {
                return new ARTLColor() { Order = 1, Type = "Rush QB", Color = System.Windows.Media.Color.FromArgb(255, 219, 65, 85) };
            }
        }
    }
}