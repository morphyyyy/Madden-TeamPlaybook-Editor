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
using MaddenCustomPlaybookEditor;
using MaddenTeamPlaybookEditor.User_Controls;

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
            {-1, "Any"},
            {1, "Flea Flicker"},
            {2, "Option Pass"},
            {3, "Pass"},
            {4, "Play Action Pass"},
            {5, "WR/HB Slip Screen"},
            {6, "Jailbreak Screen"},
            {7, "Rollout Smash"},
            {9, "Pass (Unused)"},
            {10, "Pass (Unused)"},
            {11, "QB Power"},
            {12, "Run"},
            {13, "Counter"},
            {14, "Draw"},
            {15, "Pitch"},
            {16, "Reverse"},
            {17, "Speed Option"},
            {18, "Play Action Run"},
            {19, "QB Sneak"},
            {20, "Statue"},
            {21, "Field Goal"},
            {22, "Punt"},
            {23, "Kickoff"},
            {24, "Safety Punt"},
            {25, "Onside Kick"},
            {31, "Blitz"},
            {32, "Block"},
            {33, "Man"},
            {34, "Man Zone"},
            {35, "Return"},
            {36, "Zone"},
            {37, "QB Kneel"},
            {38, "Spike Ball"},
            {41, "QB PA Run"},
            {42, "Cover 2"},
            {43, "Cover 3"},
            {44, "Cover 4 Palms"},
            {45, "Cover 4 Quarters"},
            {46, "Man Blitz"},
            {47, "Zone Blitz"},
            {48, "Combo Blitz"},
            {49, "FG Safe Man"},
            {50, "FG Safe Zone"},
            {51, "FG Block"},
            {100, "1 Step Drop"},
            {101, "3 Step Drop"},
            {102, "5 Step Drop"},
            {103, "Shotgun"},
            {104, "Hail Mary"},
            {105, "Fake FG"},
            {106, "Fake Punt"},
            {107, "Fake Spike"},
            {151, "Inside Run"},
            {152, "Outside Run"},
            {153, "FB Run"},
            {154, "Fake FG Run"},
            {155, "Fake Punt Run"},
            {157, "Trick Pass"},
            {158, "Double Pass"},
            {159, "7 Step Drop"},
            {161, "Zone Fake Jet"},
            {162, "Mid Read Option"},
            {163, "Triple Zone Read Option"},
            {164, "Veer Option"},
            {165, "Triple Veer Option"},
            {166, "Triple Mid Read Option"},
            {167, "Gun Mid Read Option"},
            {169, "Zone Read Option"},
            {170, "Zone Read WR Triple Option"},
            {171, "Zone Option"},
            {172, "Inverted Veer Option"},
            {174, "Man 0 Blitz"},
            {175, "Man 1 Blitz"},
            {176, "Zone 2 Blitz"},
            {177, "Zone 3 Blitz"},
            {178, "Man 1"},
            {179, "Man 1 Press"},
            {180, "Man 1 Robber"},
            {181, "Man Double"},
            {182, "Man 2 Deep"},
            {183, "Man 3 Deep"},
            {184, "Zone Goalline"},
            {185, "Cover 2 Sink"},
            {186, "Cover 2 Invert"},
            {187, "Cover 2 Drop"},
            {188, "Cover 3 Sky"},
            {189, "Cover 3 Buzz"},
            {190, "Cover 3 Cloud"},
            {191, "Cover 3 Drop"},
            {192, "Cover 6"},
            {194, "Cover 4 Drop"},
            {195, "Inside Zone"},
            {196, "Outside Zone"},
            {197, "HB Power"},
            {198, "HB Iso"},
            {199, "Prevent"},
            {200, "Sweep"},
            {201, "Trap"},
            {202, "Wham"},
            {203, "End Around"},
            {204, "RPO Read"},
            {205, "RPO Peek"},
            {207, "RPO Lookie/Alert"},
            {208, "Jet Pass"},
            {209, "Jet Sweep"}
       };

        public class Situation
        { 
            public static readonly List<int> KeyPlays = new List<int>
            {
                78, 2675, 2750, 2850, 2883, 3202, 3591, 3753, 3868, 3936, 4078, 4108, 4259, 4263, 4617, 4958, 5381, 5407, 5413, 5981, 6356, 6402, 6403, 6523, 6525, 6528, 6927, 7113,
                7509, 7595, 7596, 7597, 7610, 7618, 7620, 7654, 7656, 7658, 7660, 7669, 7698, 7717, 7721, 7722, 7727, 7852, 7853, 7943, 7981, 7989, 8092, 8096, 8192, 8207, 8330, 8354,
                8431, 8476, 8477, 8612, 8658, 8729, 8737, 8745, 8785, 8841, 8842, 8859, 8869, 8890, 8914, 8915, 8916, 8936, 8982, 8986, 9027, 9040, 9046, 9147, 9156, 9159, 9174, 9270,
                9272, 9482, 9495, 9499, 9802, 9804, 9806, 9812, 9816, 9842, 9844, 9845, 9846, 10148, 10264, 10283, 10342, 10344, 10346, 10347, 10447, 10458, 10459, 10493, 10706, 10720,
                10807, 11088, 11090, 11091, 11096, 11100, 11101, 11102, 11104, 11105, 11106, 11107, 11110, 11122, 11266, 11280, 11398, 11535, 11686, 11689, 11770, 11841, 11918, 11921,
                11926, 11933, 11937, 11939, 11948, 12128, 12139, 12222, 12245, 12250, 12304, 12312, 12313, 12354, 12727, 13030, 13104, 13165, 13171, 13176, 13184, 13285, 13309, 13325,
                13350, 13399, 13400, 13514, 13515, 13517, 13520, 13565, 13566, 13568, 13570, 13580, 13581, 13583, 13595, 13693, 13704, 13745, 13880, 13913, 13926, 13934, 14075, 14091,
                14188, 14211, 14236, 14252, 14294, 14300, 14332, 14872, 14898, 14938, 14988, 15227, 15240, 15293, 15303, 15319, 15320, 15328, 15529, 15582, 15643, 15652, 15718, 15737,
                15741, 15763, 15767, 15768, 15769, 15779, 15897, 15909, 15916, 15925, 16092, 16103, 16164, 16298, 16322, 16330, 16337, 16344, 16347, 16386, 16393, 16407, 16426, 16453,
                16477, 16478, 17308, 17497, 17499, 17740, 17741, 17746, 17783, 17786, 17806, 17840, 17937, 18419, 18760, 18787, 19428, 19440, 19476, 19755, 19758, 19760, 19761, 19847,
                19905, 19906, 19916, 19984, 19986, 19990, 19996, 20000, 20058, 20099, 20108, 20159, 20163, 20167, 20477, 20585, 20588, 20803, 20809, 20865, 20892, 20897, 20900, 20913,
                21060, 21079, 21092, 21098, 21103, 21127, 21141, 21178, 21181, 21185, 21190, 21201, 21255, 21379, 21386, 21466, 21686, 21701, 21711, 21725, 21730, 22072, 22402, 22415,
                22607, 22626, 22634, 22636, 22686, 22690, 22691, 22703, 22704, 22705, 22706, 22709, 22717, 22811, 22843, 22854, 22856, 22864, 22874, 22885, 22913, 22919, 23027, 23028,
                23033, 23045, 23073, 23074, 23189, 23191, 23193, 23307, 23537, 23541, 23544, 23565, 23624, 23640, 23658, 23665, 23735, 23749, 23754, 23773, 23774, 23788, 23789, 24007,
                24045, 24055, 24139, 24140, 24141, 24142, 24143, 24145, 24146, 24152, 24159, 24162, 24174, 24175, 24179, 24191, 24199, 24200, 24204, 24211, 24212, 24213, 24214, 24215,
                24224, 24225, 24244, 24266, 24268, 24269, 24271, 24272, 24274, 24288, 24328, 24334, 24341, 24343, 24346, 24384, 24387, 24392, 24393, 24395, 24396, 24397, 24438, 24562,
                24671, 24673, 24674, 24677, 24678, 24679, 24680, 24681, 24683, 24684, 24687, 24695, 24699, 24728, 24792, 24798, 24800, 24850, 24862, 24906, 25106, 25136, 25155, 25162,
                25163, 25164, 25165, 25204, 25293, 25364, 25419, 25518, 25553, 25718, 25806, 25818, 25838, 25880, 25915, 25918, 25930, 25938, 25940, 25944, 25962, 25963, 25991, 26003,
                26007, 26026, 26041, 26082, 26086, 26087, 26090, 26091, 26122, 26124, 26152, 26210, 26301, 26303, 26324, 26338, 26360, 26370, 26824, 26825, 26826, 26828, 26917, 26955,
                26967, 27057, 27074, 27211, 27237, 27252, 27253, 27286, 27305, 27320, 27352, 27372, 27401, 27414, 27415, 27416, 27424, 27431, 27441, 27449, 27450, 27451, 27455, 27462,
                27500, 27504, 27509, 27511, 27527, 27534, 27542, 27578, 27583, 27588, 27590, 27595, 27599, 27606, 27607, 27608, 27609, 27612, 27613, 27615, 27617, 27619, 27620, 27624,
                27626, 27633, 27635, 27662, 27665, 27666, 27687, 27709, 27717, 27719, 27720, 27730, 27741, 27742, 27772, 27779, 27782, 27788, 27791, 27793, 27794, 27799, 27800, 27803,
                27804, 27817, 27820, 27828, 27831, 27833, 27841, 27842, 27844, 27846, 27849, 27850, 27880, 27882, 27914, 27939, 27977, 27978, 27979, 27982, 27983, 27992, 28094, 28095,
                28096, 28170, 28172, 28176, 28178, 28181, 28183, 28184, 28185, 28187, 28188, 28189, 28191, 28195, 28200, 28201, 28202, 28204, 28213, 28222, 28232, 28234, 28237, 28240,
                28244, 28255, 28269, 28272, 28273, 28291, 28305, 28308, 28417, 28487, 28631, 28709, 28712, 28722, 28751, 28769, 28777, 28798, 28808, 28828, 28836, 28850, 28858, 28862,
                28869, 28870, 28871, 28873, 28891, 28892, 28898, 28899, 28903, 28905, 28912, 28913, 28918, 28919, 28933, 28936, 28943, 28944, 28953, 28957, 28958, 28962, 28963, 28969,
                28983, 28986, 28988, 28999, 29001, 29002, 29011, 29019, 29036, 29037, 29042, 29046, 29054, 29058, 29072, 29080, 29085, 29090, 29096, 29124, 29125, 29128, 29131, 29132,
                29133, 29135, 29136, 29137, 29138, 29140, 29142, 29143, 29146, 29156, 29163, 29174, 29197, 29202, 29217, 29221, 29222, 29237, 29238, 29256, 29263, 29264, 29275, 29303,
                29304, 29305, 29306, 29307, 29312, 29313, 29315, 29318, 29333, 29334, 29338, 29339, 29345, 29351, 29352, 29361, 29364, 29367, 29379, 29384, 29391, 29403, 29416, 29419,
                29420, 29421, 29424, 29431, 29433, 29438, 29466, 29481, 29482, 29485, 29487, 29494, 29515, 29520, 29523, 29525, 29527, 29528, 29529, 29550, 29557, 29558, 29561, 29563,
                29565, 29566, 29567, 29572, 29587, 29589, 29596, 29597, 29599, 29604, 29605, 29607, 29615, 29616, 29620, 29625, 29629, 29630, 29660, 29661, 29664, 29669, 29680, 29690,
                29691, 29692, 29696, 29699, 29701, 29704, 29706, 29728, 29733, 29734, 29737, 29738, 29742, 29744, 29745, 29747, 29752, 29753, 29754, 29755, 29757, 29780, 29799, 29801,
                29804, 29806, 29812, 29818, 29841
            };

            public static readonly List<int> KeyPlayTypes = new List<int>
            {
                4, 101, 102, 103, 159
            };

            public static readonly List<int> KeySituations1 = new List<int>
            {
                35, 17, 20, 6, 4
            };

            public static readonly List<int> KeySituations2 = new List<int>
            {
                1, 3, 6, 20, 22, 39
            };

            public static readonly List<int> IgnoreSituations1 = new List<int>
            {
                40, 8, 7, 41, 33, 32, 24, 10, 22
            };

            public static readonly List<int> IgnoreSituations2 = new List<int>
            {
                35, 6, 17
            };

            public static readonly List<int> IgnoreFormations1 = new List<int>
            {
                1186, 844, 813, 1382, 1149, 1416, 1143, 380, 1147, 576, 1014, 1148, 388, 1282, 1432, 364, 1427,
                44, 363, 1407, 1289, 1299, 1391, 428, 430, 473, 474, 476, 525, 537, 542, 544, 908, 909, 910, 911,
                913, 949, 978, 996, 997, 1118, 1152, 1179, 1185, 1283, 1287, 1333, 1334, 1335, 1336, 1337, 1338,
                1399, 1420, 1421, 1422, 30, 50, 51, 55, 68, 90, 140, 182, 189, 256, 310, 323, 335, 340, 485, 492,
                595, 672, 734, 825, 1018, 1121, 1171, 1172, 1176, 1181, 1182, 1210, 1319, 1320, 1350, 1383, 1393,
                1398, 1414, 1430, 1431
            };

            public static readonly List<int> IgnoreFormations2 = new List<int>
            {
                241, 394, 23, 654, 653, 1411, 176, 95, 22, 238, 1383, 1181, 340, 45, 672,
                323, 1398, 182
            };

            public static readonly List<string> KeyFormations = new List<string>
            {
                "Goal Line Offense", "Hail Mary", "Kickoff", "Onside", "Safety Kickoff", "Special", "Direct Snaps", "Wildcat"
            };

            public static readonly List<int> Run = new List<int>
            {
                11, 12, 13, 14, 15, 16, 17, 18, 19, 37, 41, 151, 152, 153, 154, 155, 161, 162, 163, 164, 165, 166, 167, 169, 170, 171, 172, 195, 196, 197, 198, 200, 201, 202, 203, 209
            };

            public static readonly List<int> RPO = new List<int>
            {
                204, 205, 207
            };

            public static readonly List<int> Pass = new List<int>
            {
                101, 102, 103, 159
            };

            public static readonly List<int> Screen = new List<int>
            {
                5, 6, 7, 100
            };

            public static readonly List<int> GapRun = new List<int>
            {
                197, 198, 200, 152, 13, 201
            };

            public static readonly List<int> ZoneRun = new List<int>
            {
                15, 16, 17, 151, 161, 163, 165, 169, 195, 196, 203, 208, 209
            };
        }

        public static readonly Dictionary<int, List<string>> RouteType = new Dictionary<int, List<string>>
        {
            {0, new List<string> { "Invalid", "Invalid" } },
            {1, new List<string> { "Block", "Pass" } },
            {2, new List<string> { "Block", "Run" } },
            {3, new List<string> { "Def", "Blitz" } },
            {4, new List<string> { "Def", "Man Coverage" } },
            {5, new List<string> { "Def", "Pass Rush" } },
            {6, new List<string> { "Def", "QB Spy" } },
            {7, new List<string> { "Def", "Zone Curl Flat Lt" } },
            {8, new List<string> { "Def", "Zone Curl Flat Rt" } },
            {9, new List<string> { "Def", "Zone Deep 2 Lt Half" } },
            {10, new List<string> { "Def", "Zone Deep 2 Rt Half" } },
            {11, new List<string> { "Def", "Zone Deep 4 In Lt" } },
            {12, new List<string> { "Def", "Zone Deep 4 In Rt" } },
            {13, new List<string> { "Def", "Zone Deep 4 Out Lt" } },
            {14, new List<string> { "Def", "Zone Deep 4 Out Rt" } },
            {15, new List<string> { "Def", "Zone Deep Lt 3rd" } },
            {16, new List<string> { "Def", "Zone Deep Mid 3rd" } },
            {17, new List<string> { "Def", "Zone Deep Rt 3rd" } },
            {18, new List<string> { "Def", "Zone Flat Lt" } },
            {19, new List<string> { "Def", "Zone Flat Rt" } },
            {20, new List<string> { "Def", "Zone Hook Lt" } },
            {21, new List<string> { "Def", "Zone Hook Mid" } },
            {22, new List<string> { "Def", "Zone Hook Rt" } },
            {23, new List<string> { "Def", "Zone Prevent" } },
            {24, new List<string> { "K", "FG" } },
            {25, new List<string> { "K", "FG Fake" } },
            {26, new List<string> { "K", "Kickoff" } },
            {27, new List<string> { "K", "Onside Kick" } },
            {28, new List<string> { "K", "Squib Kick" } },
            {29, new List<string> { "P", "Punt" } },
            {30, new List<string> { "P", "Punt Fake Pass" } },
            {31, new List<string> { "P", "Punt Fake Run" } },
            {32, new List<string> { "QB", "Draw" } },
            {33, new List<string> { "QB", "Fake FG Pitch" } },
            {34, new List<string> { "QB", "Fake FG Run" } },
            {35, new List<string> { "QB", "Fake Spike" } },
            {36, new List<string> { "QB", "FG Fake Pass" } },
            {37, new List<string> { "QB", "FG Hold" } },
            {38, new List<string> { "QB", "Kneel" } },
            {39, new List<string> { "QB", "Option Give" } },
            {40, new List<string> { "QB", "Pass" } },
            {41, new List<string> { "QB", "Play Action" } },
            {42, new List<string> { "QB", "Power Option" } },
            {43, new List<string> { "QB", "Shotgun Read Option" } },
            {44, new List<string> { "QB", "Speed Option" } },
            {45, new List<string> { "QB", "Spike" } },
            {46, new List<string> { "QB", "Triple Option" } },
            {47, new List<string> { "RB", "Counter" } },
            {48, new List<string> { "RB", "Dive" } },
            {49, new List<string> { "RB", "Draw" } },
            {50, new List<string> { "RB", "Fake Punt Option" } },
            {51, new List<string> { "RB", "Fake Punt Option Pass" } },
            {52, new List<string> { "RB", "Fake Punt Run" } },
            {53, new List<string> { "RB", "Option Follow" } },
            {54, new List<string> { "RB", "Option Receive" } },
            {55, new List<string> { "RB", "Pass" } },
            {56, new List<string> { "RB", "Slam" } },
            {57, new List<string> { "RB", "Stretch" } },
            {58, new List<string> { "RB", "Sweep" } },
            {59, new List<string> { "RR", "Block and Release" } },
            {60, new List<string> { "RR", "Clown" } },
            {61, new List<string> { "RR", "Comeback" } },
            {62, new List<string> { "RR", "Corner Deep" } },
            {63, new List<string> { "RR", "Corner Middle" } },
            {64, new List<string> { "RR", "Corner Post" } },
            {65, new List<string> { "RR", "Corner Stop" } },
            {66, new List<string> { "RR", "Cover 2 Corner" } },
            {67, new List<string> { "RR", "Cross" } },
            {68, new List<string> { "RR", "Curl Long" } },
            {69, new List<string> { "RR", "Curl Medium" } },
            {70, new List<string> { "RR", "Curl n Go" } },
            {71, new List<string> { "RR", "Drag" } },
            {72, new List<string> { "RR", "Flat Lt" } },
            {73, new List<string> { "RR", "Flat Rt" } },
            {74, new List<string> { "RR", "Hitch" } },
            {75, new List<string> { "RR", "Hitch N Go" } },
            {76, new List<string> { "RR", "In Deep" } },
            {77, new List<string> { "RR", "In Middle" } },
            {78, new List<string> { "RR", "In N Up" } },
            {79, new List<string> { "RR", "In Short" } },
            {80, new List<string> { "RR", "Option Route" } },
            {81, new List<string> { "RR", "Out Deep" } },
            {82, new List<string> { "RR", "Out Middle" } },
            {83, new List<string> { "RR", "Out N Up" } },
            {84, new List<string> { "RR", "Out N Up Comeback" } },
            {85, new List<string> { "RR", "Out Short" } },
            {86, new List<string> { "RR", "Post Corner" } },
            {87, new List<string> { "RR", "Post Deep" } },
            {88, new List<string> { "RR", "Post Middle" } },
            {89, new List<string> { "RR", "Post Stop" } },
            {90, new List<string> { "RR", "RB Angle" } },
            {91, new List<string> { "RR", "RB Flat Lt" } },
            {92, new List<string> { "RR", "RB Flat Rt" } },
            {93, new List<string> { "RR", "WR Screen" } },
            {94, new List<string> { "RR", "RB Screen Left" } },
            {95, new List<string> { "RR", "RB Screen Rt" } },
            {96, new List<string> { "RR", "Slant" } },
            {97, new List<string> { "RR", "Slant N Go" } },
            {98, new List<string> { "RR", "Streak" } },
            {99, new List<string> { "RR", "Swing Lt" } },
            {100, new List<string> { "RR", "Swing Rt" } },
            {101, new List<string> { "RR", "Wheel Lt" } },
            {102, new List<string> { "RR", "Wheel Rt" } },
            {103, new List<string> { "RR", "Whip In" } },
            {104, new List<string> { "RR", "Whip Out" } },
            {105, new List<string> { "ST", "Kickoff Block" } },
            {106, new List<string> { "ST", "Kickoff Cover" } },
            {107, new List<string> { "ST", "Kickoff Return" } },
            {108, new List<string> { "ST", "Punt Block" } },
            {109, new List<string> { "ST", "Punt Return" } },
            {110, new List<string> { "WR", "Pass" } },
            {111, new List<string> { "QB", "Handoff" } },
            {112, new List<string> { "QB", "Run" } },
            {113, new List<string> { "QB", "Pitch" } },
            {114, new List<string> { "QB", "Option Pass" } },
            {115, new List<string> { "RR", "Block Flats Lt" } },
            {116, new List<string> { "RR", "Block Flats Rt" } },
            {117, new List<string> { "RR", "Block Hook Mid" } },
            {118, new List<string> { "RR", "Block TE Dump" } },
            {119, new List<string> { "RR", "Block TE Cross" } },
            {120, new List<string> { "RR", "Block TE In" } },
            {121, new List<string> { "RR", "Block TE Out" } },
            {122, new List<string> { "RR", "Block Streak" } },
            {123, new List<string> { "RR", "Block Hook Lt" } },
            {124, new List<string> { "RR", "Block Hook Rt" } },
            {125, new List<string> { "RR", "Block Circle" } },
            {126, new List<string> { "RR", "Slant Hook" } },
            {127, new List<string> { "ST", "Punt Cover" } },
            {128, new List<string> { "ST", "Fake Punt Rooskie" } },
            {129, new List<string> { "ST", "Fake Punt Rooskie Pass" } },
            {130, new List<string> { "WR", "Reverse" } },
            {131, new List<string> { "WR", "Fake Reverse" } },
            {132, new List<string> { "RB", "Handoff Reverse" } },
            {133, new List<string> { "Def", "Man 1" } },
            {134, new List<string> { "Def", "Man 2" } },
            {135, new List<string> { "Def", "Man 3" } },
            {136, new List<string> { "Def", "Man 4" } },
            {137, new List<string> { "Def", "Man 5" } },
            {138, new List<string> { "ST", "Onside Cover" } },
            {139, new List<string> { "ST", "Onside Recover" } },
            {140, new List<string> { "QB", "Fake Pass" } },
            {141, new List<string> { "RR", "Fade" } },
            {142, new List<string> { "RR", "Option Comeback Corner" } },
            {143, new List<string> { "RR", "Option Comeback Fade" } },
            {144, new List<string> { "RR", "Option Curl Corner" } },
            {145, new List<string> { "RR", "Option Curl Dig" } },
            {146, new List<string> { "RR", "Option Curl Fade" } },
            {147, new List<string> { "RR", "Option Curl Post Seam" } },
            {148, new List<string> { "RR", "Option Curl Seam" } },
            {149, new List<string> { "RR", "Option Dig Post" } },
            {150, new List<string> { "RR", "Option Drag Hitch" } },
            {151, new List<string> { "RR", "Option HB Choice In" } },
            {152, new List<string> { "RR", "Option HB Choice In Out" } },
            {153, new List<string> { "RR", "Option HB Choice Out" } },
            {154, new List<string> { "RR", "Option Hitch Fade" } },
            {155, new List<string> { "RR", "Option Hitch Fade Slant" } },
            {156, new List<string> { "RR", "Option Hitch In Out" } },
            {157, new List<string> { "RR", "Option Hitch Out" } },
            {158, new List<string> { "RR", "Option Juke" } },
            {159, new List<string> { "RR", "Option Out Fade" } },
            {160, new List<string> { "RR", "Option Out Fade Slant" } },
            {161, new List<string> { "RR", "Option Seam Bender" } },
            {162, new List<string> { "RR", "Option Post Corner" } },
            {163, new List<string> { "Max", "Max" } }
        };

        public static readonly Dictionary<int, string> SituationOff = new Dictionary<int, string>
        {
            {0,"1st Play"},
            {35,"1st Down"},
            {20,"2nd & Short"},
            {17,"2nd & Med"},
            {6,"2nd & Long"},
            {1,"3rd & Short"},
            {7,"3rd & Med"},
            {8,"3rd & Long"},
            {40,"3rd & Extra Long"},
            {31,"4th & Short"},
            {32,"4th & Medium"},
            {33,"4th & Long"},
            {41,"4rd & Extra Long"},
            {25,"Red Zone Fringe"},
            {26,"Red Zone 16 to 20"},
            {27,"Red Zone 11 to 15"},
            {28,"Red Zone 6 to 10"},
            {39,"Red Zone 3 to 5"},
            {4,"Red Zone"},
            {5,"Inside Five"},
            {3,"Goal Line"},
            {22,"Goal Line Pass"},
            {24,"4 min Offense"},
            {10,"2 min Offense"},
            {37,"Play Action"},
            {23,"Signiature Plays"},
            {42,"Sudden Change"},
            {43,"Last Play"},
            {21,"Hail Mary"},
            {44,"Max"},
            {2,"Stop Clock"},
            {29,"Stop Clock User"},
            {30,"Stop Clock Fake User"},
            {13,"Waste Time"},
            {14,"Kneel"},
            {38,"Extra Point"},
            {34,"Go for 2"},
            {12,"Fake FG"},
            {9,"Punt"},
            {19,"Punt Max Protect"},
            {11,"Fake Punt"},
            {15,"Kickoff"},
            {36,"Squib"},
            {16,"Kickoff Onside"},
            {18,"Kickoff Safety"}
        };

        public static readonly Dictionary<int, string> SituationDef = new Dictionary<int, string>
        {
            {0,"Normal"},
            {18,"Run"},
            {2,"Pass"},
            {20,"Nickle"},
            {6,"Nickle Run"},
            {13,"Nickle Pass"},
            {21,"Dime"},
            {27,"Dime Pass"},
            {12,"Trips"},
            {1,"Trips Run"},
            {11,"Trips Pass"},
            {10,"Bunch"},
            {19,"Empty"},
            {25,"Overload"},
            {24,"Wildcat"},
            {29,"Conserve Time"},
            {28,"Prevent"},
            {26,"Hail Mary"},
            {22,"Goal Line"},
            {5,"Goal Line Run"},
            {7,"Goal Line Pass"},
            {3,"Kickoff Return"},
            {4,"Kickoff Return Onsides"},
            {8,"Kickoff Return Safety"},
            {14,"Punt Return"},
            {15,"Punt Return Safe"},
            {9,"Punt Block"},
            {17,"FG Safe"},
            {16,"FG Block"},
            {23,"FG Return"},
            {30,"Max"}
        };

        public string filePath { get; set; }

        public ObservableCollection<FormationVM> Formations { get; set; }

        public Point LOS { get; set; } = new Point(266.5, 600);

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

        public ObservableCollection<FormationVM> GetPSALlist()
        {
            ObservableCollection<FormationVM> routes = new ObservableCollection<FormationVM>();
            TeamPlaybook routeTypes = new TeamPlaybook { Formations = routes };
            var routePositions = RouteType.GroupBy(type => type.Value[0]).ToDictionary(type => type.Key, type => type.ToList());

            foreach (var _position in routePositions)
            {
                ObservableCollection<SubFormationVM> positions = new ObservableCollection<SubFormationVM>();
                FormationVM route = new FormationVM
                {
                    PBFM = new Madden.TeamPlaybook.PBFM { name = _position.Key },
                    SubFormations = positions,
                    Playbook = routeTypes,
                    IsVisible = true
                };
                foreach (var _type in _position.Value)
                {
                    ObservableCollection<PlayVM> types = new ObservableCollection<PlayVM>();
                    SubFormationVM position = new SubFormationVM
                    {
                        PBST = new PBST { name = _type.Key.ToString() + ": " + _type.Value[1] },
                        Plays = types,
                        Formation = route,
                        CurrentPackage = new List<Madden.TeamPlaybook.SETP>(),
                        Packages = new List<SubFormationVM.Package>
                        {
                            new SubFormationVM.Package(new Madden.TeamPlaybook.SPKF(), new List<Madden.TeamPlaybook.SPKG>())
                        },
                        Alignments = new List<SubFormationVM.Alignment>
                        {
                            new SubFormationVM.Alignment(new Madden.TeamPlaybook.SGFM(), new List<Madden.TeamPlaybook.SETG>())
                        }
                    };
                    foreach (var _route in PLYS.Select(x => new { x.PSAL, x.PLRR }).Where(x => x.PLRR == _type.Key).Distinct().OrderBy(x => x.PSAL))
                    {
                        PlayerVM player = new PlayerVM
                        {
                            PLYS = PLYS.Where(plys => plys.PSAL == _route.PSAL).FirstOrDefault(),
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
                            ARTL = ARTL.Where(_psal => _psal.artl == PLYS.Where(plys => plys.PSAL == _route.PSAL).FirstOrDefault().ARTL).FirstOrDefault(),
                            artlColor = ARTLColor.Undefined,
                            PSAL = PSAL.Where(_psal => _psal.psal == _route.PSAL).OrderBy(s => s.step).ToList(),
                            Icon = new EllipseGeometry(new Point(0, 0), 4, 4).GetFlattenedPathGeometry(),
                            EPos = "",
                            DPos = "1"
                        };
                        PlayVM type = new PlayVM
                        {
                            PBPL = new Madden.TeamPlaybook.PBPL { name = "PSAL: " + _route.PSAL.ToString() },
                            PLYL = new PLYL { vpos = 0 },
                            PLYS = new List<Madden.TeamPlaybook.PLYS>(),
                            Players = new ObservableCollection<PlayerVM>
                            {
                                player
                            },
                            SubFormation = position
                        };
                        player.Play = type;
                        player.ConvertARTL(player.ARTL);
                        player.GetARTLcolor();
                        player.ConvertPSAL(player.PSAL);
                        player.GetRouteCap();
                        type.GetPlayerPlayartViewList();
                        types.Add(type);
                    }
                    if (position.Plays.Count() > 0)
                    {
                        positions.Add(position);
                    }
                }
                if (route.SubFormations.Count() > 0)
                {
                    routes.Add(route);
                }
            }

            return routes;
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

        public void RevampGameplan()
        {
            List<int> _form = FORM.Where(p => Situation.KeyFormations.Contains(p.name)).Select(p => p.form).ToList();
            List<int> _setl = SETL.Where(p => _form.Contains(p.FORM)).Select(p => p.setl).ToList();
            List<Madden.TeamPlaybook.PLYL> _plyl = PLYL.Where(p => !_setl.Contains(p.SETL)).ToList();

            List<Madden.TeamPlaybook.PBAI> _pbai = PBAI.Where(p => Situation.KeySituations1.Contains(p.AIGR)).ToList();
            PBAI.RemoveAll(p => _pbai.Contains(p));
            foreach (Madden.TeamPlaybook.PLYL play in _plyl)
            {
                foreach (int airg in Situation.KeySituations1.Where(p => p != 4).ToList())
                {
                    PBAI.Add(new Madden.TeamPlaybook.PBAI
                    {
                        rec = NextAvailableID(PBAI.Select(p => p.rec).ToList()),
                        PBPL = PBPL.Where(p => p.PLYL == play.plyl).FirstOrDefault().pbpl,
                        SETL = play.SETL,
                        AIGR = airg,
                        PLYT = play.PLYT,
                        PLF_ = play.PLF_,
                        Flag = PBPL.Where(p => p.PLYL == play.plyl).FirstOrDefault().Flag,
                        vpos = play.vpos,
                        prct = 10
                    });
                }
            }
            _pbai = PBAI.Where(p => Situation.IgnoreFormations1.Contains(p.SETL) && Situation.IgnoreSituations1.Contains(p.AIGR)).ToList();
            PBAI.RemoveAll(p => _pbai.Contains(p));
            _pbai = PBAI.Where(p => Situation.IgnoreFormations2.Contains(p.SETL) && Situation.IgnoreSituations2.Contains(p.AIGR)).ToList();
            PBAI.RemoveAll(p => _pbai.Contains(p));

            _pbai = PBAI.Where(p => Situation.KeySituations2.Contains(p.AIGR) && Situation.KeyPlayTypes.Contains(p.PLYT)).ToList();
            PBAI.RemoveAll(p => _pbai.Contains(p));
            _plyl = PLYL.Where(p => Situation.KeyPlays.Contains(p.plyl) && !_setl.Contains(p.SETL)).ToList();
            foreach (Madden.TeamPlaybook.PLYL play in _plyl)
            {
                foreach (int airg in Situation.KeySituations2)
                {
                    PBAI.Add(new Madden.TeamPlaybook.PBAI
                    {
                        rec = NextAvailableID(PBAI.Select(p => p.rec).ToList()),
                        PBPL = PBPL.Where(p => p.PLYL == play.plyl).FirstOrDefault().pbpl,
                        SETL = play.SETL,
                        AIGR = airg,
                        PLYT = play.PLYT,
                        PLF_ = play.PLF_,
                        Flag = PBPL.Where(p => p.PLYL == play.plyl).FirstOrDefault().Flag,
                        vpos = play.vpos,
                        prct = 10
                    });
                }
            }
            _pbai = PBAI.Where(p => Situation.IgnoreFormations1.Contains(p.SETL) && Situation.IgnoreSituations1.Contains(p.AIGR)).ToList();
            PBAI.RemoveAll(p => _pbai.Contains(p));
            _pbai = PBAI.Where(p => Situation.IgnoreFormations2.Contains(p.SETL) && Situation.IgnoreSituations2.Contains(p.AIGR)).ToList();
            PBAI.RemoveAll(p => _pbai.Contains(p));
            _pbai = PBAI.Where(p => new List<int> { 7, 8 }.Contains(p.AIGR) && new List<int> { 101, 102, 159 }.Contains(p.PLYT)).ToList();
            PBAI.RemoveAll(p => _pbai.Contains(p));
            _pbai = PBAI.Where(p => p.prct == 0).ToList();
            PBAI.RemoveAll(p => _pbai.Contains(p));

            foreach (Madden.TeamPlaybook.PBAI play in PBAI.Where(p => p.AIGR == 35))
            {
                if (Situation.ZoneRun.Contains(play.PLYT)) play.prct = 75;
                if (Situation.RPO.Contains(play.PLYT)) play.prct = 45;
                if (Situation.GapRun.Contains(play.PLYT)) play.prct = 45;
                if (play.PLYT == 4 && play.vpos != 5)
                {
                    play.prct = 30;
                }
                else if (play.PLYT == 4 && play.vpos == 5)
                {
                    play.prct = 15;
                }
                if (Situation.Pass.Contains(play.PLYT)) play.prct = 30;
                if (Situation.Screen.Contains(play.PLYT)) play.prct = 10;
            }
            foreach (Madden.TeamPlaybook.PBAI play in PBAI.Where(p => p.AIGR == 6))
            {
                if (Situation.ZoneRun.Contains(play.PLYT)) play.prct = 40;
                if (play.PLYT == 4 && play.vpos != 5)
                {
                    play.prct = 30;
                }
                else if (play.PLYT == 4 && play.vpos == 5)
                {
                    play.prct = 15;
                }
                if (Situation.GapRun.Contains(play.PLYT)) play.prct = 10;
                if (Situation.Screen.Contains(play.PLYT)) play.prct = 10;
            }
            foreach (Madden.TeamPlaybook.PBAI play in PBAI.Where(p => p.AIGR == 17))
            {
                if (Situation.ZoneRun.Contains(play.PLYT)) play.prct = 60;
                if (Situation.RPO.Contains(play.PLYT)) play.prct = 45;
                if (Situation.GapRun.Contains(play.PLYT)) play.prct = 45;
                if (play.PLYT == 4 && play.vpos != 5)
                {
                    play.prct = 30;
                }
                else if (play.PLYT == 4 && play.vpos == 5)
                {
                    play.prct = 15;
                }
                if (Situation.Pass.Contains(play.PLYT)) play.prct = 30;
                if (Situation.Screen.Contains(play.PLYT)) play.prct = 10;
            }
            foreach (Madden.TeamPlaybook.PBAI play in PBAI.Where(p => p.AIGR == 3 || p.AIGR == 4 || p.AIGR == 20 || p.AIGR == 25 || p.AIGR == 26 || p.AIGR == 27 || p.AIGR == 28 || p.AIGR == 39))
            {
                if (Situation.Run.Contains(play.PLYT)) play.prct = 30;
                else
                {
                    play.prct = 10;
                }
            }
            foreach (Madden.TeamPlaybook.PBAI play in PBAI.Where(p => p.AIGR == 8 || p.AIGR == 7))
            {
                if (Situation.Pass.Contains(play.PLYT)) play.prct = 10;
                if (Situation.Screen.Contains(play.PLYT)) play.prct = 1;
                if (Situation.GapRun.Contains(play.PLYT)) play.prct = 1;
                if (Situation.Run.Contains(play.PLYT)) play.prct = 0;
            }
            foreach (Madden.TeamPlaybook.PBAI play in PBAI.Where(p => p.AIGR == 1))
            {
                if (Situation.Run.Contains(play.PLYT)) play.prct = 20;
                else
                {
                    play.prct = 10;
                }
            }

            PBAI = Madden.TeamPlaybook.PBAI.Sort(PBAI);
            for (int i = 0; i < PBAI.Count(); i++)
            {
                PBAI[i].rec = i;
            }

            foreach (FormationVM _formation in Formations)
            {
                foreach (SubFormationVM _subFormation in _formation.SubFormations)
                {
                    foreach (PlayVM _play in _subFormation.Plays)
                    {
                        _play.GetSituations();
                    }
                }
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

        public static int NextAvailableID(List<int> IDs, bool insert = false, int buffer = 0, int start = 1)
        {
            if (IDs == null || IDs.Count == 0) return 1;
            int ID = IDs.Max() + 1;
            if (insert)
            {
                for (int i = start; i <= ID; i++)
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