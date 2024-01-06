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
using System.Security.Cryptography;

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

        [Serializable]
        public class Gameplan
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

            public static readonly List<int> Percentages = new List<int>
            {
                0, 1, 5, 10, 15, 20, 30, 45, 60, 75, 90
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
                101, 102, 103, 159, 208
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
                15, 16, 17, 151, 161, 163, 165, 169, 195, 196, 203, 209
            };

            public static readonly List<int> Offense = new List<int>
            {
                1, 2, 3, 4, 5, 6, 11, 13, 14, 15, 16, 17, 18, 19, 21, 22, 23, 24, 25, 37, 38, 100, 101, 102, 103, 104, 105, 106, 107, 151, 152, 153, 154, 155, 157, 159, 161, 163, 165, 169, 172, 195, 196, 197, 198, 201, 202, 203, 204, 205, 207, 208, 209
            };

            public static readonly List<int> Defense = new List<int>
            {
                32, 35, 42, 43, 44, 45, 46, 47, 49, 51, 174, 175, 176, 177, 178, 179, 180, 182, 183, 185, 186, 187, 188, 189, 190, 191, 192, 194, 199, 210, 211
            };
        }

        [Serializable]
        public class Tendency
        {
            public double PA { get; set; } // Playaction
            public double RPO { get; set; }
            public double Shotgun { get; set; } // Shotgun formation 
            public double Screen { get; set; }
            public double Gap { get; set; } // Gap Run
            public double Zone { get; set; } // Zone Run
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
            {25,"Opp 21 to 25"},
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

        public ObservableCollection<SubFormationVM> SubFormations
        {
            get 
            {
                ObservableCollection<SubFormationVM> _subFormations = new ObservableCollection<SubFormationVM>();
                foreach (FormationVM _Formation in Formations) foreach (SubFormationVM _subFormation in _Formation.SubFormations) _subFormations.Add(_subFormation);
                return _subFormations;
            }
        }

        public ObservableCollection<PlayVM> Plays
        {
            get
            {
                ObservableCollection<PlayVM> _Plays = new ObservableCollection<PlayVM>();
                foreach (SubFormationVM _subFormation in SubFormations) foreach (PlayVM _play in _subFormation.Plays) _Plays.Add(_play);
                return _Plays;
            }
        }

        public ObservableCollection<PlayerVM> Players
        {
            get
            {
                ObservableCollection<PlayerVM> _Players = new ObservableCollection<PlayerVM>();
                foreach (PlayVM _play in Plays) foreach (PlayerVM _player in _play.Players) _Players.Add(_player);
                return _Players;
            }
        }

        public List<KeyValuePair<int, Tendency>> Tendencies { get; set; }

        public Point LOS { get; set; } = new Point(266.5, 600);

        public string Type { get; set; }
        public Dictionary<int, string> Situations { get; set; }
        public List<TableNames> TableNames { get; set; }
        private List<Madden.TeamPlaybook.ARTL> _ARTL;
        public List<Madden.TeamPlaybook.ARTL> ARTL
        {
            get { return _ARTL; }
            set
            {
                if (_ARTL == value)
                    return;
                _ARTL = value;
                OnPropertyChanged("ARTL");
            }
        }
        private List<Madden.TeamPlaybook.FORM> _FORM;
        public List<Madden.TeamPlaybook.FORM> FORM
        {
            get { return _FORM; }
            set
            {
                if (_FORM == value)
                    return;
                _FORM = value;
                OnPropertyChanged("FORM");
            }
        }
        private List<Madden.TeamPlaybook.PBAI> _PBAI;
        public List<Madden.TeamPlaybook.PBAI> PBAI
        {
            get { return _PBAI; }
            set
            {
                if (_PBAI == value)
                    return;
                _PBAI = value;
                OnPropertyChanged("PBAI");
            }
        }
        private List<Madden.TeamPlaybook.PBAU> _PBAU;
        public List<Madden.TeamPlaybook.PBAU> PBAU
        {
            get { return _PBAU; }
            set
            {
                if (_PBAU == value)
                    return;
                _PBAU = value;
                OnPropertyChanged("PBAU");
            }
        }
        private List<Madden.TeamPlaybook.PBCC> _PBCC;
        public List<Madden.TeamPlaybook.PBCC> PBCC
        {
            get { return _PBCC; }
            set
            {
                if (_PBCC == value)
                    return;
                _PBCC = value;
                OnPropertyChanged("PBCC");
            }
        }
        private List<Madden.TeamPlaybook.PBFM> _PBFM;
        public List<Madden.TeamPlaybook.PBFM> PBFM
        {
            get { return _PBFM; }
            set
            {
                if (_PBFM == value)
                    return;
                _PBFM = value;
                OnPropertyChanged("PBFM");
            }
        }
        private List<Madden.TeamPlaybook.PBPL> _PBPL;
        public List<Madden.TeamPlaybook.PBPL> PBPL
        {
            get { return _PBPL; }
            set
            {
                if (_PBPL == value)
                    return;
                _PBPL = value;
                OnPropertyChanged("PBPL");
            }
        }
        private List<Madden.TeamPlaybook.PBST> _PBST;
        public List<Madden.TeamPlaybook.PBST> PBST
        {
            get { return _PBST; }
            set
            {
                if (_PBST == value)
                    return;
                _PBST = value;
                OnPropertyChanged("PBST");
            }
        }
        private List<Madden.TeamPlaybook.PLCM> _PLCM;
        public List<Madden.TeamPlaybook.PLCM> PLCM
        {
            get { return _PLCM; }
            set
            {
                if (_PLCM == value)
                    return;
                _PLCM = value;
                OnPropertyChanged("PLCM");
            }
        }
        private List<Madden.TeamPlaybook.PLPD> _PLPD;
        public List<Madden.TeamPlaybook.PLPD> PLPD
        {
            get { return _PLPD; }
            set
            {
                if (_PLPD == value)
                    return;
                _PLPD = value;
                OnPropertyChanged("PLPD");
            }
        }
        private List<Madden.TeamPlaybook.PLRD> _PLRD;
        public List<Madden.TeamPlaybook.PLRD> PLRD
        {
            get { return _PLRD; }
            set
            {
                if (_PLRD == value)
                    return;
                _PLRD = value;
                OnPropertyChanged("PLRD");
            }
        }
        private List<Madden.TeamPlaybook.PLYL> _PLYL;
        public List<Madden.TeamPlaybook.PLYL> PLYL
        {
            get { return _PLYL; }
            set
            {
                if (_PLYL == value)
                    return;
                _PLYL = value;
                OnPropertyChanged("PLYL");
            }
        }
        private List<Madden.TeamPlaybook.PLYS> _PLYS;
        public List<Madden.TeamPlaybook.PLYS> PLYS
        {
            get { return _PLYS; }
            set
            {
                if (_PLYS == value)
                    return;
                _PLYS = value;
                OnPropertyChanged("PLYS");
            }
        }
        private List<Madden.TeamPlaybook.PPCT> _PPCT;
        public List<Madden.TeamPlaybook.PPCT> PPCT
        {
            get { return _PPCT; }
            set
            {
                if (_PPCT == value)
                    return;
                _PPCT = value;
                OnPropertyChanged("PPCT");
            }
        }
        private List<Madden.TeamPlaybook.PSAL> _PSAL;
        public List<Madden.TeamPlaybook.PSAL> PSAL
        {
            get { return _PSAL; }
            set
            {
                if (_PSAL == value)
                    return;
                _PSAL = value;
                OnPropertyChanged("PSAL");
            }
        }
        private List<Madden.TeamPlaybook.SDEF> _SDEF;
        public List<Madden.TeamPlaybook.SDEF> SDEF
        {
            get { return _SDEF; }
            set
            {
                if (_SDEF == value)
                    return;
                _SDEF = value;
                OnPropertyChanged("SDEF");
            }
        }
        private List<Madden.TeamPlaybook.SETG> _SETG;
        public List<Madden.TeamPlaybook.SETG> SETG
        {
            get { return _SETG; }
            set
            {
                if (_SETG == value)
                    return;
                _SETG = value;
                OnPropertyChanged("SETG");
            }
        }
        private List<Madden.TeamPlaybook.SETL> _SETL;
        public List<Madden.TeamPlaybook.SETL> SETL
        {
            get { return _SETL; }
            set
            {
                if (_SETL == value)
                    return;
                _SETL = value;
                OnPropertyChanged("SETL");
            }
        }
        private List<Madden.TeamPlaybook.SETP> _SETP;
        public List<Madden.TeamPlaybook.SETP> SETP
        {
            get { return _SETP; }
            set
            {
                if (_SETP == value)
                    return;
                _SETP = value;
                OnPropertyChanged("SETP");
            }
        }
        private List<Madden.TeamPlaybook.SGFM> _SGFM;
        public List<Madden.TeamPlaybook.SGFM> SGFM
        {
            get { return _SGFM; }
            set
            {
                if (_SGFM == value)
                    return;
                _SGFM = value;
                OnPropertyChanged("SGFM");
            }
        }
        private List<Madden.TeamPlaybook.SPKF> _SPKF;
        public List<Madden.TeamPlaybook.SPKF> SPKF
        {
            get { return _SPKF; }
            set
            {
                if (_SPKF == value)
                    return;
                _SPKF = value;
                OnPropertyChanged("SPKF");
            }
        }
        private List<Madden.TeamPlaybook.SPKG> _SPKG;
        public List<Madden.TeamPlaybook.SPKG> SPKG
        {
            get { return _SPKG; }
            set
            {
                if (_SPKG == value)
                    return;
                _SPKG = value;
                OnPropertyChanged("SPKG");
            }
        }
        private List<Madden.TeamPlaybook.SRFT> _SRFT;
        public List<Madden.TeamPlaybook.SRFT> SRFT
        {
            get { return _SRFT; }
            set
            {
                if (_SRFT == value)
                    return;
                _SRFT = value;
                OnPropertyChanged("SRFT");
            }
        }
        private List<DCHT> _DCHT;
        public List<DCHT> DCHT
        {
            get { return _DCHT; }
            set
            {
                if (_DCHT == value)
                    return;
                _DCHT = value;
                OnPropertyChanged("DCHT");
            }
        }
        private List<PLAY> _PLAY;
        public List<PLAY> PLAY
        {
            get { return _PLAY; }
            set
            {
                if (_PLAY == value)
                    return;
                _PLAY = value;
                OnPropertyChanged("PLAY");
            }
        }

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
            GetTendencies();
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

        public void AddFormation(MaddenCustomPlaybookEditor.ViewModels.FormationVM Formation, int ord = 0)
        {
            //FORM existingFORM = FORM.Where(formation => formation.form == Formation.CPFM.FORM).FirstOrDefault();
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

        public void GetTendencies()
        {
            Tendencies = RedDobe.TeamTendencies.FirstOrDefault(tt => this.ToString().IndexOf(tt.Key, StringComparison.OrdinalIgnoreCase) >= 0 && Type == "Offense").Value;
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
            List<int> _form = FORM.Where(p => Gameplan.KeyFormations.Contains(p.name)).Select(p => p.form).ToList();
            List<int> _setl = SETL.Where(p => _form.Contains(p.FORM)).Select(p => p.setl).ToList();
            List<Madden.TeamPlaybook.PLYL> _plyl = PLYL.Where(p => !_setl.Contains(p.SETL)).ToList();

            List<Madden.TeamPlaybook.PBAI> _pbai = PBAI.Where(p => Gameplan.KeySituations1.Contains(p.AIGR)).ToList();
            PBAI.RemoveAll(p => _pbai.Contains(p));
            foreach (Madden.TeamPlaybook.PLYL play in _plyl)
            {
                foreach (int airg in Gameplan.KeySituations1.Where(p => p != 4).ToList())
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
            _pbai = PBAI.Where(p => Gameplan.IgnoreFormations1.Contains(p.SETL) && Gameplan.IgnoreSituations1.Contains(p.AIGR)).ToList();
            PBAI.RemoveAll(p => _pbai.Contains(p));
            _pbai = PBAI.Where(p => Gameplan.IgnoreFormations2.Contains(p.SETL) && Gameplan.IgnoreSituations2.Contains(p.AIGR)).ToList();
            PBAI.RemoveAll(p => _pbai.Contains(p));

            _pbai = PBAI.Where(p => Gameplan.KeySituations2.Contains(p.AIGR) && Gameplan.KeyPlayTypes.Contains(p.PLYT)).ToList();
            PBAI.RemoveAll(p => _pbai.Contains(p));
            _plyl = PLYL.Where(p => Gameplan.KeyPlays.Contains(p.plyl) && !_setl.Contains(p.SETL)).ToList();
            foreach (Madden.TeamPlaybook.PLYL play in _plyl)
            {
                foreach (int airg in Gameplan.KeySituations2)
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
            _pbai = PBAI.Where(p => Gameplan.IgnoreFormations1.Contains(p.SETL) && Gameplan.IgnoreSituations1.Contains(p.AIGR)).ToList();
            PBAI.RemoveAll(p => _pbai.Contains(p));
            _pbai = PBAI.Where(p => Gameplan.IgnoreFormations2.Contains(p.SETL) && Gameplan.IgnoreSituations2.Contains(p.AIGR)).ToList();
            PBAI.RemoveAll(p => _pbai.Contains(p));
            _pbai = PBAI.Where(p => new List<int> { 7, 8 }.Contains(p.AIGR) && new List<int> { 101, 102, 159 }.Contains(p.PLYT)).ToList();
            PBAI.RemoveAll(p => _pbai.Contains(p));
            _pbai = PBAI.Where(p => p.prct == 0).ToList();
            PBAI.RemoveAll(p => _pbai.Contains(p));

            foreach (Madden.TeamPlaybook.PBAI play in PBAI) play.prct = 10;

            foreach (Madden.TeamPlaybook.PBAI play in PBAI.Where(p => p.AIGR == 35))
            {
                if (Gameplan.ZoneRun.Contains(play.PLYT)) play.prct = 75;
                if (Gameplan.RPO.Contains(play.PLYT)) play.prct = 45;
                if (Gameplan.GapRun.Contains(play.PLYT)) play.prct = 45;
                if (play.PLYT == 4 && play.vpos != 5)
                {
                    play.prct = 30;
                }
                else if (play.PLYT == 4 && play.vpos == 5)
                {
                    play.prct = 15;
                }
                if (Gameplan.Pass.Contains(play.PLYT) && play.vpos != 5)
                {
                    play.prct = 30;
                }
                else if(Gameplan.Pass.Contains(play.PLYT) && play.vpos == 5)
                {
                    play.prct = 15;
                }
                if (Gameplan.Screen.Contains(play.PLYT)) play.prct = 10;
            }
            foreach (Madden.TeamPlaybook.PBAI play in PBAI.Where(p => p.AIGR == 6))
            {
                if (Gameplan.ZoneRun.Contains(play.PLYT)) play.prct = 40;
                if (Gameplan.Pass.Contains(play.PLYT) && play.vpos != 5)
                {
                    play.prct = 30;
                }
                else if (Gameplan.Pass.Contains(play.PLYT) && play.vpos == 5)
                {
                    play.prct = 15;
                }
                if (play.PLYT == 4 && play.vpos == 5)
                {
                    play.prct = 15;
                }
                if (Gameplan.GapRun.Contains(play.PLYT)) play.prct = 10;
                if (Gameplan.Screen.Contains(play.PLYT)) play.prct = 10;
            }
            foreach (Madden.TeamPlaybook.PBAI play in PBAI.Where(p => p.AIGR == 17))
            {
                if (Gameplan.ZoneRun.Contains(play.PLYT)) play.prct = 60;
                if (Gameplan.RPO.Contains(play.PLYT)) play.prct = 45;
                if (Gameplan.GapRun.Contains(play.PLYT)) play.prct = 45;
                if (play.PLYT == 4 && play.vpos != 5)
                {
                    play.prct = 30;
                }
                else if (play.PLYT == 4 && play.vpos == 5)
                {
                    play.prct = 15;
                }
                if (Gameplan.Pass.Contains(play.PLYT) && play.vpos != 5)
                {
                    play.prct = 30;
                }
                else if (Gameplan.Pass.Contains(play.PLYT) && play.vpos == 5)
                {
                    play.prct = 15;
                }
                if (Gameplan.Screen.Contains(play.PLYT)) play.prct = 10;
            }
            foreach (Madden.TeamPlaybook.PBAI play in PBAI.Where(p => p.AIGR == 3 || p.AIGR == 4 || p.AIGR == 20 || p.AIGR == 25 || p.AIGR == 26 || p.AIGR == 27 || p.AIGR == 28 || p.AIGR == 39))
            {
                if (Gameplan.Run.Contains(play.PLYT)) play.prct = 30;
                else
                {
                    play.prct = 10;
                }
            }
            foreach (Madden.TeamPlaybook.PBAI play in PBAI.Where(p => p.AIGR == 7))
            {
                if (Gameplan.Pass.Contains(play.PLYT)) play.prct = 10;
                if (Gameplan.Screen.Contains(play.PLYT)) play.prct = 1;
                if (Gameplan.GapRun.Contains(play.PLYT)) play.prct = 1;
                if (Gameplan.Run.Contains(play.PLYT)) play.prct = 0;
            }
            foreach (Madden.TeamPlaybook.PBAI play in PBAI.Where(p => p.AIGR == 8))
            {
                if (Gameplan.Pass.Contains(play.PLYT)) play.prct = 10;
                if (play.PLYT == 4) play.prct = 1;
                if (Gameplan.Screen.Contains(play.PLYT)) play.prct = 1;
                if (Gameplan.GapRun.Contains(play.PLYT)) play.prct = 1;
                if (Gameplan.Run.Contains(play.PLYT)) play.prct = 0;
            }
            foreach (Madden.TeamPlaybook.PBAI play in PBAI.Where(p => p.AIGR == 1))
            {
                if (Gameplan.Run.Contains(play.PLYT)) play.prct = 20;
                else
                {
                    play.prct = 10;
                }
            }

            if (PBAI.Count > 2000)
            {
                int threshold = 1;
                _pbai = PBAI.Where(p => p.prct == threshold && PBAI.Select(n => n.AIGR > threshold) != null).ToList();
                if (MessageBox.Show("There are " + (2000 - PBAI.Count).ToString() + " too many PBAI records and the game will crash.\nWould you like to remove " + _pbai.Count + " records with a 1 prct?", "Warning", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    PBAI.RemoveAll(p => _pbai.Contains(p));
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

        public void AddPlaysToGameplan(int airg, List<Madden.TeamPlaybook.PLYL> _plyl, int Amount, bool Random)
        {
            int idx;
            for (int i = Math.Min(Amount, _plyl.Count()); i > 0; i--)
            {
                if (Random)
                {
                    idx = RedDobe.RandomInt(0, _plyl.Count() - 1);
                }
                else
                {
                    idx = i - 1;
                }


                PBAI.Add(new Madden.TeamPlaybook.PBAI
                {
                    rec = NextAvailableID(PBAI.Select(p => p.rec).ToList()),
                    PBPL = PBPL.Where(p => p.PLYL == _plyl[idx].plyl).FirstOrDefault().pbpl,
                    SETL = _plyl[idx].SETL,
                    AIGR = airg,
                    PLYT = _plyl[idx].PLYT,
                    PLF_ = _plyl[idx].PLF_,
                    Flag = PBPL.Where(p => p.PLYL == _plyl[idx].plyl).FirstOrDefault().Flag,
                    vpos = _plyl[idx].vpos,
                    prct = 10
                });
                // Remove the already used play so it doesn't get picked again
                _plyl.Remove(_plyl[idx]);
                if (_plyl.Count() == 0)
                    break;
            }
        }

        public void RedDobeRevampGameplan()
        {
            // Now lets add some play types that are missing in certain situations
            List<int> _form = FORM.Where(p => Gameplan.KeyFormations.Contains(p.name)).Select(p => p.form).ToList();
            List<int> _setl = SETL.Where(p => _form.Contains(p.FORM)).Select(p => p.setl).ToList();
            List<int> _setlShotgun = SETL.Where(p => p.FORM == 1 || p.FORM == 103).Select(p => p.setl).ToList();
            List<Madden.TeamPlaybook.PLYL> _plyl = PLYL.Where(p => !_setl.Contains(p.SETL)).ToList();
            List<Madden.TeamPlaybook.PLYL> _plylShotgun = _plyl.Where(p => _setlShotgun.Contains(p.SETL)).ToList();
            List<Madden.TeamPlaybook.PLYL> _plylNonShotgun = _plyl.Where(p => !_setlShotgun.Contains(p.SETL)).ToList();

            List<Madden.TeamPlaybook.PLYL> _rpo = _plyl.Where(p => Gameplan.RPO.Contains(p.PLYT)).ToList();
            List<Madden.TeamPlaybook.PLYL> _screenHB = _plyl.Where(p => Gameplan.Screen.Contains(p.PLYT) && p.vpos == 1).ToList();
            List<Madden.TeamPlaybook.PLYL> _screenWRTE = _plyl.Where(p => Gameplan.Screen.Contains(p.PLYT) && p.vpos != 1).ToList();
            List<Madden.TeamPlaybook.PLYL> _pa = _plyl.Where(p => p.PLYT == 4).ToList();
            List<Madden.TeamPlaybook.PLYL> _draw = _plyl.Where(p => p.PLYT == 14).ToList();
            List<Madden.TeamPlaybook.PLYL> _gap = _plyl.Where(p => Gameplan.GapRun.Contains(p.PLYT)).ToList();
            List<Madden.TeamPlaybook.PLYL> _zone = _plyl.Where(p => Gameplan.ZoneRun.Contains(p.PLYT)).ToList();
            List<Madden.TeamPlaybook.PLYL> _gapShotgun = _plylShotgun.Where(p => Gameplan.GapRun.Contains(p.PLYT)).ToList();
            List<Madden.TeamPlaybook.PLYL> _gapNonShotgun = _plylNonShotgun.Where(p => Gameplan.GapRun.Contains(p.PLYT)).ToList();
            List<Madden.TeamPlaybook.PLYL> _zoneNonShotgun = _plylNonShotgun.Where(p => Gameplan.ZoneRun.Contains(p.PLYT)).ToList();
            List<Madden.TeamPlaybook.PLYL> _zoneShotgun = _plylShotgun.Where(p => Gameplan.ZoneRun.Contains(p.PLYT)).ToList();
            List<Madden.TeamPlaybook.PLYL> _passNonShotgun = _plylNonShotgun.Where(p => Gameplan.Pass.Contains(p.PLYT)).ToList();
            List<Madden.TeamPlaybook.PLYL> _passShotgun = _plylShotgun.Where(p => Gameplan.Pass.Contains(p.PLYT)).ToList();

            List<Madden.TeamPlaybook.PLYL> _plylCopy;
            // RPOs
            foreach (int airg in RedDobe.SituationsToAddRPOs.ToList())
            {
                _plylCopy = new List<Madden.TeamPlaybook.PLYL>(_rpo);
                AddPlaysToGameplan(airg, _plylCopy, 12, true);
            }
            // Screens
            foreach (int airg in RedDobe.SituationsToAddHBScreens.ToList())
            {
                _plylCopy = new List<Madden.TeamPlaybook.PLYL>(_screenHB);
                AddPlaysToGameplan(airg, _plylCopy, 5, true);
            }
            foreach (int airg in RedDobe.SituationsToAddWRTEScreens.ToList())
            {
                _plylCopy = new List<Madden.TeamPlaybook.PLYL>(_screenWRTE);
                AddPlaysToGameplan(airg, _plylCopy, 5, true);
            }
            // Playaction passes
            foreach (int airg in RedDobe.SituationsToAddPlayAction.ToList())
            {
                _plylCopy = new List<Madden.TeamPlaybook.PLYL>(_pa);
                AddPlaysToGameplan(airg, _plylCopy, 20, true);
            }
            // Draws
            foreach (int airg in RedDobe.SituationsToAddDraws.ToList())
            {
                _plylCopy = new List<Madden.TeamPlaybook.PLYL>(_draw);
                AddPlaysToGameplan(airg, _plylCopy, 3, false);
            }
            // all gap and zone runs
            foreach (int airg in RedDobe.SituationsToAddGapZoneRuns.ToList())
            {
                _plylCopy = new List<Madden.TeamPlaybook.PLYL>(_gap);
                AddPlaysToGameplan(airg, _plylCopy, 4, true);
                _plylCopy = new List<Madden.TeamPlaybook.PLYL>(_zone);
                AddPlaysToGameplan(airg, _plylCopy, 6, true);
            }
            // Non-Shotgun gap and zone runs
            foreach (int airg in RedDobe.SituationsToAddNonShotgunGapZoneRuns.ToList())
            {
                _plylCopy = new List<Madden.TeamPlaybook.PLYL>(_gapNonShotgun);
                AddPlaysToGameplan(airg, _plylCopy, 4, true);
                _plylCopy = new List<Madden.TeamPlaybook.PLYL>(_zoneNonShotgun);
                AddPlaysToGameplan(airg, _plylCopy, 6, true);
            }
            // Shotgun gap and zone runs
            foreach (int airg in RedDobe.SituationsToAddShotgunGapZoneRuns.ToList())
            {
                _plylCopy = new List<Madden.TeamPlaybook.PLYL>(_gapShotgun);
                AddPlaysToGameplan(airg, _plylCopy, 4, true);
                _plylCopy = new List<Madden.TeamPlaybook.PLYL>(_zoneShotgun);
                AddPlaysToGameplan(airg, _plylCopy, 6, true);
            }
            //shotgun passes
            foreach (int airg in RedDobe.SituationsToAddShotgunPasses.ToList())
            {
                _plylCopy = new List<Madden.TeamPlaybook.PLYL>(_passShotgun);
                AddPlaysToGameplan(airg, _plylCopy, 20, true);
            }
            // Non shotgun passes
            foreach (int airg in RedDobe.SituationsToAddNonShotgunPasses.ToList())
            {
                _plylCopy = new List<Madden.TeamPlaybook.PLYL>(_passNonShotgun);
                AddPlaysToGameplan(airg, _plylCopy, 10, true);
            }
            // All Plays
            List<Madden.TeamPlaybook.PBAI> _pbai;

            foreach (int airg in RedDobe.SituationsToAddAllPlays.ToList())
            {
                _pbai = PBAI.Where(p => p.AIGR == airg).ToList();
                PBAI.RemoveAll(p => _pbai.Contains(p));
                _plylCopy = new List<Madden.TeamPlaybook.PLYL>(_plyl);
                AddPlaysToGameplan(airg, _plylCopy, 500, false);
            }

            // Count the number records to see if we need to trim down PBAI to get under the 2000 play cap, removing entries from situations that aren't used very often
            int PBAIRecordCount = PBAI.Count();
            Console.WriteLine("AI Play calling records after adding entries: " + PBAIRecordCount);

            // Add up the depth of all pass plays in this PB
            double TotalRouteDepth = Plays.Where(p => (Gameplan.Pass.Contains(p.PLYL.PLYT) || Gameplan.RPO.Contains(p.PLYL.PLYT) ||
            Gameplan.Screen.Contains(p.PLYL.PLYT) || p.PLYL.PLYT == 4)).Sum(p => p.AverageRouteDepth);
            int NumPassPlays = Plays.Count(p => Gameplan.Pass.Contains(p.PLYL.PLYT));
            int NumPlayActionPlays = Plays.Count(p => p.PLYL.PLYT == 4);
            int NumScreenPlays = Plays.Count(p => Gameplan.Screen.Contains(p.PLYL.PLYT));
            int NumWRTEScreenPlays = Plays.Count(p => Gameplan.Screen.Contains(p.PLYL.PLYT) && p.PLYL.vpos != 1);
            int NumRPOPlays = Plays.Count(p => Gameplan.RPO.Contains(p.PLYL.PLYT));
            int NumRunPlays = Plays.Count(p => Gameplan.Run.Contains(p.PLYL.PLYT));
            // Need to get number of jet passes separately since they don't have a route depth value and figure out the new average depth with those passes
            int NumJetPasses = Plays.Count(p => p.PLYL.PLYT == 208);
            int NumTrickPasses = Plays.Count(p => p.PLYL.PLYT == 157 || p.PLYL.PLYT == 158);
            int TotPassPlays = NumPassPlays + NumPlayActionPlays + NumScreenPlays + NumRPOPlays + NumTrickPasses;
            // FOR INFORMATIONAL PURPOSES ONLY
            Console.WriteLine("Num Reg Pass Plays: " + NumPassPlays);
            Console.WriteLine("Num PlayAction Pass Plays: " + NumPlayActionPlays);
            Console.WriteLine("Num Jet Pass Plays: " + NumJetPasses);
            Console.WriteLine("Num Trick Pass Plays: " + NumTrickPasses);
            Console.WriteLine("Num Screen Plays: " + NumScreenPlays);
            Console.WriteLine("Num RPO Plays: " + NumRPOPlays);
            Console.WriteLine("Total Pass Plays: " + TotPassPlays);
            Console.WriteLine("Total Run Plays: " + NumRunPlays);
            Console.WriteLine("Avg Pass Depth including jet passes: " + Math.Round(TotalRouteDepth / TotPassPlays, 2));

            // Vars to target when revamping this entire gameplan
            double GameplanTargetRunPercentage = RedDobe.GetTeamRunPercentage(filePath); // This will be the run percent that we are trying to target for this gameplan ~ .5%
            double GameplanTargetPassDepth = RedDobe.GetTeamPassDepth(filePath); // This will be the run percent that we are trying to target for this gameplan ~ .5%
            double NFLAverageRunPercentage = 39.67; // THis is the 2023 nfl average of run plays called (QB scrambles are counted as passes)
            double NFLAveragePassDepth = 7.75; // THis is the 2023 nfl average pass depth
            // Variance above or below the nfl average run percentage (for adjusting each situation)
            double GameplanRunPercentageVariance = (GameplanTargetRunPercentage - NFLAverageRunPercentage) / NFLAverageRunPercentage;
            if (GameplanRunPercentageVariance > 0)
            {
                GameplanRunPercentageVariance *= 1; // needs to be increased for higher than average gameplans to account for all of the 100% passing downs
            }
            // Variance above or below the nfl average pass depth (for adjusting each situation)
            double GameplanPassDepthVariance = (GameplanTargetPassDepth - NFLAveragePassDepth) / NFLAveragePassDepth;

            // Vars for cumilative PB totals for this gameplan
            double TotalPassWeight = 0; // The sum of all of the prct for pass plays
            double TotalWeightedRouteDepth = 0; // play route depth times pass weight
            double TotalAvgRouteDepth = 0; // Sum of (SitExpectedPassPlays * SitAvgRouteDepth) - divid this by TotalExpectedPassPlays to get the gameplan's average route depth
            double TotalRunWeight = 0; // The sum of all of the prct for run plays
            double TotalScreenWeight = 0; //For tracking the total wieght of all screens
            double TotalRPOWeight = 0;//For tracking the total wieght of all RPOs
            double TotalWeight = 0; // The sum of all of the prct for all plays
            double TotalRunPercentage = 0; // Total expected runs / total expected plays
            double TotalGapRunPercentage = 0; // Total expected gap runs / total expected runs
            double TotalZoneRunPercentage = 0; // Total expected zone runs / total expected runs
            double TotalShotgunPercentage = 0; // Total shotgun pass plays / total expected plays
            double TotalScreenPassPercentage = 0; // Total expected screen plays / total expected pass plays
            double TotalWRTEScreenPercentage = 0; // Total expected WR/TE screen plays / total expected screen plays
            double TotalRPOPercentage = 0; // Total expected RPO's / total expected plays
            double TotalPlayActionPercentage = 0; // Total expected Play Action passes / total expected plays
            double TotalExpectedRunPlays = 0; // Expected number of run plays
            double TotalExpectedGapRunPlays = 0; // Expected number of gap run plays
            double TotalExpectedZoneRunPlays = 0; // Expected number of zone run plays
            double TotalExpectedPassPlays = 0; // Expected number of pass plays
            double TotalExpectedRPOPlays = 0; // expected number of RPO plays
            double TotalExpectedShotgunPlays = 0; // Expected number of shotgun pass plays
            double TotalExpectedScreenPlays = 0; // expected number of screen pass play
            double TotalExpectedWRTEScreenPlays = 0; // expected number of WR and TE screens
            double TotalExpectedPlayActionPlays = 0; // expected number of playaction passes
            double TotalExpectedPlays = 0; // Expected total number of runs and passes

            var groupedPBAI = from s in PBAI
                              group s by s.AIGR;

            //iterate each Situation group        
            foreach (var Situation in groupedPBAI)
            {
                if (RedDobe.SituationExpectedNumPlays[Situation.Key] == 0) // Skip if no plays are expected for this situation
                    continue;
                string SituationName = SituationOff[Situation.Key];
                Console.WriteLine("Situation ID: {0}", Situation.Key + " Name: " + SituationName); //Each group has a key 
                // Target Values
                var SitTendency = RedDobe.GetTeamSituationTendency(filePath, Situation.Key);
                // This will be the run percent that we are trying to target for this situation ~ 5%
                double SitTargetRunPercentage = Math.Min(Math.Max(RedDobe.GetSituationRunPercentage(Situation.Key) * (1 + GameplanRunPercentageVariance), 0), 100);
                // THis will be the target pass depth that we are tying to target for this situation ~ 10%
                double SitTargetRouteDepth = Math.Min(Math.Max(RedDobe.GetSituationRouteDepth(Situation.Key) * (1 + GameplanPassDepthVariance), 0), 100);
                double SitTargetShotgunPercentage = SitTendency.Shotgun; // The percent of all plays that should be shotgun formation ~ 10%
                double SitTargetScreenPercentage = SitTendency.Screen; // The percent of all PASS plays that should be screens ~ 10%
                double SitTargetWRTEScreenPercentage = 58; // The percent of SCREEN plays that should be WR or TE screens ~ 20%
                if (SitTargetScreenPercentage == 0)
                {
                    SitTargetWRTEScreenPercentage = 0;
                }
                double SitTargetRPOPercentage = SitTendency.RPO; // The percent of ALL plays called that should be RPOs ~ 10%
                double SitTargetPlayActionPercentage = SitTendency.PA; // The percentage of all plays called that should be Play action passes ~10%
                double SitTargetGapRunPercentage = SitTendency.Gap; // The percentage of all RUN plays called that should be gap runs ~10%
                double SitTargetZoneRunPercentage = SitTendency.Zone; // The percentage of all RUN plays called that should be zone runs ~10%

                // vars for this situation
                double SitPassWeight = 0;
                double SitWeightedRouteDepth = 0;
                double SitAvgRouteDepth = 0;
                double SitRunWeight = 0;
                double SitGapRunWeight = 0; // The sum of all of the prct for gap run plays
                double SitZoneRunWeight = 0; // The sum of all of the prct for zone run plays
                double SitShotgunWeight = 0; // The sum of all of the prct for shotgun plays
                double SitScreenWeight = 0; //For tracking the total wieght of all screens in this situation
                double SitWRTEScreenWeight = 0;
                double SitRPOWeight = 0;//For tracking the total wieght of all RPOs in this situation
                double SitPlayActionWeight = 0;
                double SitWeight = 0;
                double SitRunPercentage = 0;
                double SitGapRunPercentage = 0; // Sit expected gap runs / total expected runs
                double SitZoneRunPercentage = 0; // Sit expected zone runs / total expected runs
                double SitShotgunPercentage = 0;
                double SitScreenPercentage = 0;
                double SitWRTEScreenPercentage = 0; // Used the track the percent of all Screen passes that are WR or TE screens
                double SitRPOPercentage = 0;
                double SitPlayActionPercentage = 0;
                double SitRouteDepth = 0;
                double SitExpectedRunPlays = 0;
                double SitExpectedGapRunPlays = 0; // Expected number of gap run plays
                double SitExpectedZoneRunPlays = 0; // Expected number of zone run plays
                double SitExpectedPassPlays = 0;
                double SitExpectedRPOPlays = 0;
                double SitExpectedShotgunPlays = 0; // Expected number of shotgun plays
                double SitExpectedScreenPlays = 0;
                double SitExpectedWRTEScreenPlays = 0;
                double SitExpectedPlayActionPlays = 0;
                double SitExpectedTotPlays = 0;

                bool SituationComplete = false; // Stop loop and adjusting situation when this is true
                bool ShotgunsComplete = false; // Stop adjusting shotgun passes when true
                bool ScreensComplete = false; // Stop adjusting screens when true
                bool WRTEScreensComplete = false; // Stop adjusting WR and TE screens when true
                bool RPOsComplete = false; // Stop adjusting RPOs when true
                bool RouteDepthComplete = false; // Sopt adjusting Route depth when true
                bool RunPercentComplete = false; // stop adjusting run/pass percentage when true
                bool GapRunsComplete = false; // Stop adjusting gap runs when true
                bool ZoneRunsComplete = false; // Stop adjusting zone runs when true
                bool PlayActionPercentComplete = false; // stop adjusting Play action percentage when true
                double ScreenPercentageAdjustment = 0; // How much the situation screen ratio is off from the target
                double WRTEScreenPercentageAdjustment = 0; // How much the situation WR TE screen ratio is off from the target
                double RPOPercentageAdjustment = 0; // How much the situation RPO ratio is off from the target
                double RouteDepthAdjustment = 0; // How much the situation average route depth is off from the target
                double RunPercentAdjustment = 0; // How much the situation run percent is off from the target
                double PlayActionPercentAdjustment = 0; // How much the situation Play action percent is off from the target
                double ShotgunPercentAdjustment = 0; // How much the situation Shotgun pass percent is off from the target
                double GapRunPercentAdjustment = 0; // How much the situation gap run percent is off from the target
                double ZoneRunPercentAdjustment = 0; // How much the situation zone run percent is off from the target
                // The following situation tendency adjustments will be ignored when these are set to true
                bool IgnoreSomeTendencies = false;
                bool IgnoreRouteDepth = false;

                if (RedDobe.SituationsToIgnore.Contains(Situation.Key))
                {
                    IgnoreSomeTendencies = true;
                }

                if (RedDobe.SituationsToIgnoreRouteDepth.Contains(Situation.Key))
                {
                    IgnoreRouteDepth = true;
                }
                double AcceptableDistanceToTarget = 2.50;
                double AcceptableDistanceRouteDepth = .50;
                double AcceptableDistanceToRunPercentTarget = 1.25;
                double RPORunPercent = 0;
                int x = 1; // counter for do while loop
                // Start looping until we get the target values
                do
                {
                    // 1/2 of RPO is added to the run weight, RPO's are a mixture of zone and gap runs and need to be subtracted from the target
                    // Otherwise it would impossible to acheive the target gap and zone run percentages
                    RPORunPercent = SitTargetRPOPercentage / 4 / SitTargetRunPercentage * 100;
                    SitTargetGapRunPercentage = Math.Max(SitTendency.Gap - RPORunPercent, 0);
                    SitTargetZoneRunPercentage = Math.Max(SitTendency.Zone - RPORunPercent, 0);
                    // Vars for this situation, Reset totals to 0 before we restart the loop and try reaching our target value again
                    SitPassWeight = SitWeightedRouteDepth = SitAvgRouteDepth = SitRunWeight = SitWeight = SitRunPercentage = SitGapRunPercentage =
                        SitZoneRunPercentage = SitScreenPercentage = SitWRTEScreenPercentage = SitRPOPercentage = SitShotgunPercentage =
                        SitPlayActionPercentage = SitRouteDepth = SitExpectedRunPlays = SitExpectedGapRunPlays = SitExpectedZoneRunPlays =
                        SitExpectedPassPlays = SitExpectedRPOPlays = SitExpectedScreenPlays = SitExpectedShotgunPlays = SitShotgunWeight =
                        SitExpectedWRTEScreenPlays = SitExpectedPlayActionPlays = SitExpectedTotPlays = SitScreenWeight =
                        SitWRTEScreenWeight = SitRPOWeight = SitPlayActionWeight = SitGapRunWeight = SitZoneRunWeight = 0;

                    // Let's collect all of the weighting for runs and passes along with everything else needed to make this work
                    foreach (Madden.TeamPlaybook.PBAI s in Situation) // Each group has inner collection
                    {
                        // Find this play
                        var ThisPlay = Plays.FirstOrDefault(p => s.PBPL == p.PBPL.pbpl);
                        if (ThisPlay != null)
                        {
                            // Set all plays to be 10 weighting on first run - this allows better balancing
                            if (x == 1)
                            {
                                s.prct = 50;
                            }
                            //Console.WriteLine("Playbook Play ID Found: {0}", s.PBPL);
                            // Passes to include RPOs - 208 is a jet pass
                            bool IsPass = Gameplan.Pass.Contains(s.PLYT) || s.PLYT == 104; // 104 is hail mary, they are in the 4th down gameplan sometimes
                            bool IsPlayAction = s.PLYT == 4;
                            bool IsShotgun = ThisPlay.SubFormation.SETL.FORM == 1 || ThisPlay.SubFormation.SETL.FORM == 103;
                            bool IsScreen = Gameplan.Screen.Contains(s.PLYT);
                            bool IsWRTEScreen = false;
                            bool IsRun = Gameplan.Run.Contains(s.PLYT);
                            bool IsGapRun = Gameplan.GapRun.Contains(s.PLYT);
                            bool IsZoneRun = Gameplan.ZoneRun.Contains(s.PLYT);
                            bool IsTrickPass = s.PLYT == 157 || s.PLYT == 158;

                            if (IsScreen)
                            {
                                if (ThisPlay.PLYL.vpos != 1)
                                {
                                    IsWRTEScreen = true;
                                }
                            }
                            bool IsJetPass = s.PLYT == 208;
                            bool IsRPO = Gameplan.RPO.Contains(s.PLYT);
                            int WeightAdjustment = 0;

                            // First clear out all plays that are not in the gameplan unless the situation ignores certain tendencies
                            if (!IgnoreSomeTendencies)
                            {
                                if ((IsShotgun && SitTargetShotgunPercentage < AcceptableDistanceToTarget) ||
                                    (!IsShotgun && SitTargetShotgunPercentage > 100 - AcceptableDistanceToTarget))
                                {
                                    WeightAdjustment = -200;
                                }
                                if (IsRPO && SitTargetRPOPercentage < AcceptableDistanceToTarget)
                                {
                                    WeightAdjustment = -200;
                                }
                                if (IsScreen && SitTargetScreenPercentage < AcceptableDistanceToTarget)
                                {
                                    WeightAdjustment = -200;
                                }
                                if (IsPlayAction && SitTargetPlayActionPercentage < AcceptableDistanceToTarget)
                                {
                                    WeightAdjustment = -200;
                                }
                                if (IsRun)
                                {
                                    if (!IsGapRun && !IsZoneRun && SitTargetGapRunPercentage +
                                        SitTargetZoneRunPercentage >
                                        100 - AcceptableDistanceToTarget) // for other runs that need to be removed
                                    {
                                        WeightAdjustment = -200;
                                    }
                                    if ((IsZoneRun && SitTargetZoneRunPercentage < AcceptableDistanceToTarget) ||
                                        (!IsZoneRun && SitTargetZoneRunPercentage > 100 - AcceptableDistanceToTarget))
                                    {
                                        WeightAdjustment = -200;
                                    }
                                    else if ((IsGapRun && SitTargetGapRunPercentage < AcceptableDistanceToTarget)
                                        || (!IsGapRun && SitTargetGapRunPercentage > 100 - AcceptableDistanceToTarget))
                                    {
                                        WeightAdjustment = -200;
                                    }
                                }
                            }
                            if ((IsRun && SitTargetRunPercentage < AcceptableDistanceToRunPercentTarget) ||
                                (!IsRun && SitTargetRunPercentage > 100 - AcceptableDistanceToRunPercentTarget))
                            {
                                WeightAdjustment = -200;
                            }

                            //Adjust all play weighting
                            if (x > 1 && !ShotgunsComplete) // Need to process this first so that the weight is adjusted since shotguns can be runs and passes
                            {
                                if (IsShotgun)
                                {
                                    WeightAdjustment += RedDobe.GetWeightAdjustment(ShotgunPercentAdjustment);
                                }
                                else // Need to adjust all other plays when Shotgun percent is below or above the target
                                {
                                    WeightAdjustment += RedDobe.GetWeightAdjustment(-ShotgunPercentAdjustment);
                                }
                            }
                            if ((x > 1 && x <= 31 && !RunPercentComplete) || (x > 1 && !RunPercentComplete))
                            { // Weighting adjustments done after first loop
                                if (IsRun)
                                {
                                    WeightAdjustment += RedDobe.GetWeightAdjustment(RunPercentAdjustment);
                                }
                                else if (!IsRPO) // Any passes need to be adjusted the other way when run percent is not met
                                {
                                    // Weighting adjustments done after first loop
                                    WeightAdjustment += RedDobe.GetWeightAdjustment(-RunPercentAdjustment);
                                }
                            }
                            if ((x > 31 && x <= 61 && !RPOsComplete) || (x > 1 && !RPOsComplete))
                            {
                                if (IsRPO)
                                {
                                    // Weighting adjustments done after first loop
                                    WeightAdjustment += RedDobe.GetWeightAdjustment(RPOPercentageAdjustment);
                                }
                                else
                                {
                                    WeightAdjustment += RedDobe.GetWeightAdjustment(-RPOPercentageAdjustment);
                                }
                            }

                            if ((x > 121 && x <= 151 && !ScreensComplete) || (x > 1 && !ScreensComplete))
                            {
                                if (IsScreen)
                                {
                                    // Weighting adjustments done after first loop
                                    WeightAdjustment += RedDobe.GetWeightAdjustment(ScreenPercentageAdjustment);
                                }
                                else if (IsPass || IsJetPass || IsPlayAction || IsTrickPass) // Since screen is a percent of pass
                                {
                                    WeightAdjustment += RedDobe.GetWeightAdjustment(-ScreenPercentageAdjustment);
                                }
                            }
                            if ((x > 121 && x <= 151 && !WRTEScreensComplete) || (x > 1 && !WRTEScreensComplete)) // for handling WR or TE screens
                            {
                                if (IsWRTEScreen)
                                {
                                    WeightAdjustment += RedDobe.GetWeightAdjustment(WRTEScreenPercentageAdjustment);
                                }
                                else if (IsScreen) // Since these are a percent of all screens
                                {
                                    WeightAdjustment += RedDobe.GetWeightAdjustment(-WRTEScreenPercentageAdjustment);
                                }
                            }

                            if ((x > 91 && x <= 121 && !PlayActionPercentComplete) || (x > 1 && !PlayActionPercentComplete))
                            {
                                if (IsPlayAction)
                                {
                                    // Weighting adjustments done after first loop
                                    WeightAdjustment += RedDobe.GetWeightAdjustment(PlayActionPercentAdjustment);
                                }
                                else if (IsPass || IsJetPass || IsScreen || IsTrickPass)
                                {
                                    WeightAdjustment += RedDobe.GetWeightAdjustment(-PlayActionPercentAdjustment);
                                }
                            }
                            if ((x > 61 && x <= 91 && !RouteDepthComplete) || (x > 1 && !RouteDepthComplete))
                            {
                                if (IsJetPass)
                                {
                                    // Weighting adjustments done after first loop
                                    WeightAdjustment += RedDobe.GetWeightAdjustment(-RouteDepthAdjustment);
                                }
                                else if ((IsPass || IsPlayAction || IsTrickPass) && !IsRPO && !IsScreen)
                                {
                                    // Weighting adjustments done after first loop, don't want to adjust screens or RPOs here, they are already handled
                                    double LastSitRouteDepth = SitTargetRouteDepth - RouteDepthAdjustment;
                                    if ((RouteDepthAdjustment > 0 && LastSitRouteDepth < ThisPlay.AverageRouteDepth) ||
                                               (RouteDepthAdjustment < 0 && LastSitRouteDepth >= ThisPlay.AverageRouteDepth))
                                    {
                                        WeightAdjustment += RedDobe.GetWeightAdjustment(1);
                                    }
                                    else if ((RouteDepthAdjustment > 0 && LastSitRouteDepth - 5 < ThisPlay.AverageRouteDepth) ||
                                               (RouteDepthAdjustment < 0 && LastSitRouteDepth + 5 >= ThisPlay.AverageRouteDepth))
                                    {
                                        // do nothing
                                    }
                                    else
                                    {
                                        WeightAdjustment += RedDobe.GetWeightAdjustment(-1);
                                    }
                                }
                            }
                            if (((IsRun || IsRPO) && x > 1 && x <= 31) || ((IsRun || IsRPO) && x > 1))
                            {
                                if (IsGapRun)
                                {
                                    if (!GapRunsComplete)
                                    {
                                        WeightAdjustment += RedDobe.GetWeightAdjustment(GapRunPercentAdjustment);
                                    }
                                }
                                else if (IsZoneRun)
                                {
                                    if (!ZoneRunsComplete)
                                    {
                                        WeightAdjustment += RedDobe.GetWeightAdjustment(ZoneRunPercentAdjustment);
                                    }
                                } // Need to adjust all other run plays and rpo's when gap or run percent are below or above the target
                                else
                                {
                                    if (!ZoneRunsComplete && !GapRunsComplete && (GapRunPercentAdjustment > 0 && ZoneRunPercentAdjustment > 0))
                                    {
                                        WeightAdjustment += RedDobe.GetWeightAdjustment(-1);
                                    }
                                    else if (!ZoneRunsComplete && GapRunsComplete && ZoneRunPercentAdjustment > 0)
                                    {
                                        WeightAdjustment += RedDobe.GetWeightAdjustment(-1);
                                    }
                                    else if (ZoneRunsComplete && !GapRunsComplete && GapRunPercentAdjustment > 0)
                                    {
                                        WeightAdjustment += RedDobe.GetWeightAdjustment(-1);
                                    }
                                }
                            }
                            s.prct = Math.Max(Math.Min(s.prct + WeightAdjustment, 99), 0);
                            // Now add the new adjusted weighting to the Sit Vars
                            if (IsPass || IsScreen || IsRPO || IsJetPass || IsPlayAction || IsTrickPass)
                            {
                                if (IsRPO)
                                {
                                    // Assume RPO is a 50% chance to be a run or pass
                                    SitRPOWeight += s.prct;
                                    SitPassWeight += (double)s.prct / 2.0;
                                    SitWeightedRouteDepth += ThisPlay.AverageRouteDepth * (double)s.prct / 2.0;
                                    SitRunWeight += (double)s.prct / 2.0;
                                    SitRouteDepth += ThisPlay.AverageRouteDepth;
                                }
                                else if (IsJetPass)
                                {
                                    SitRouteDepth += -2.0; // Assign a depth to jet pass since they don't have one
                                    SitPassWeight += (double)s.prct;
                                    SitWeightedRouteDepth += -2.0 * (double)s.prct;
                                }
                                else // this is just a screen or pass or playaction
                                {
                                    if (IsScreen)
                                    {
                                        if (IsWRTEScreen)
                                        {
                                            SitWRTEScreenWeight += (double)s.prct;
                                        }
                                        SitScreenWeight += (double)s.prct;
                                    }
                                    if (IsPlayAction)
                                    {
                                        SitPlayActionWeight += (double)s.prct;
                                    }
                                    SitRouteDepth += ThisPlay.AverageRouteDepth;
                                    SitPassWeight += (double)s.prct;
                                    SitWeightedRouteDepth += ThisPlay.AverageRouteDepth * (double)s.prct;
                                }
                                SitWeight += (double)s.prct;
                            }

                            // Runs     
                            if (IsRun)
                            {
                                if (IsGapRun)
                                {
                                    SitGapRunWeight += (double)s.prct;
                                }
                                else if (IsZoneRun)
                                {
                                    SitZoneRunWeight += (double)s.prct;
                                } // Need to adjust all other run plays when gap or run percent are below or above the target                            
                                SitRunWeight += (double)s.prct;
                                SitWeight += (double)s.prct;
                            }
                            if (IsShotgun) // add to shotgun weight at the end since the weight can be adjusted by playtype
                            {
                                SitShotgunWeight += (double)s.prct;
                            }

                        }
                        else
                        {
                            Console.WriteLine("Could Not Find Playbook Play ID: {0}", s.PBPL);
                        }
                    }
                    //Console.WriteLine("Summary for Situation ID: {0}", Situation.Key + " Name: " + SituationName); //Each group has a key 
                    //Console.WriteLine("Sit Pass Weight: {0}", SitPassWeight);
                    //Console.WriteLine("Sit Route Depth: {0}", SitWeightedRouteDepth);
                    //Console.WriteLine("Average Route Depth: {0}", Math.Round(SitAvgRouteDepth, 2));
                    //Console.WriteLine("Sit Run Weight: {0}", SitRunWeight);
                    //Console.WriteLine("Sit Screen Weight: {0}", SitScreenWeight);
                    //Console.WriteLine("Sit RPO Weight: {0}", SitRPOWeight);

                    if (SitPassWeight > 0)
                    {
                        SitAvgRouteDepth = SitWeightedRouteDepth / SitPassWeight;
                    }
                    SitExpectedRunPlays = RedDobe.SituationExpectedNumPlays[Situation.Key] * SitRunWeight / SitWeight;
                    SitExpectedPassPlays = RedDobe.SituationExpectedNumPlays[Situation.Key] * SitPassWeight / SitWeight;
                    SitExpectedTotPlays = SitExpectedRunPlays + SitExpectedPassPlays;
                    SitExpectedGapRunPlays = RedDobe.SituationExpectedNumPlays[Situation.Key] * SitGapRunWeight / SitWeight;
                    SitExpectedZoneRunPlays = RedDobe.SituationExpectedNumPlays[Situation.Key] * SitZoneRunWeight / SitWeight;
                    SitExpectedRPOPlays = RedDobe.SituationExpectedNumPlays[Situation.Key] * SitRPOWeight / SitWeight;
                    SitExpectedShotgunPlays = RedDobe.SituationExpectedNumPlays[Situation.Key] * SitShotgunWeight / SitWeight;
                    SitExpectedScreenPlays = RedDobe.SituationExpectedNumPlays[Situation.Key] * SitScreenWeight / SitWeight;
                    SitExpectedWRTEScreenPlays = RedDobe.SituationExpectedNumPlays[Situation.Key] * SitWRTEScreenWeight / SitWeight;
                    SitExpectedPlayActionPlays = RedDobe.SituationExpectedNumPlays[Situation.Key] * SitPlayActionWeight / SitWeight;
                    SitRunPercentage = SitRunWeight / SitWeight * 100.0;
                    SitShotgunPercentage = SitShotgunWeight / SitWeight * 100.0;
                    if (SitPassWeight > 0)
                    {
                        SitScreenPercentage = SitScreenWeight / SitPassWeight * 100.0;
                    }

                    if (SitScreenWeight > 0)
                    {
                        SitWRTEScreenPercentage = SitWRTEScreenWeight / SitScreenWeight * 100.0;
                    }

                    if (SitRunWeight > 0)
                    {
                        SitGapRunPercentage = SitGapRunWeight / SitRunWeight * 100.0;
                        SitZoneRunPercentage = SitZoneRunWeight / SitRunWeight * 100.0;
                    }

                    SitRPOPercentage = SitRPOWeight / SitWeight * 100.0;

                    SitPlayActionPercentage = SitPlayActionWeight / SitWeight * 100;
                    //Console.WriteLine("Sit expected num of runs: {0}", Math.Round(SitExpectedRunPlays, 1));
                    //Console.WriteLine("Sit expected num of passes: {0}", Math.Round(SitExpectedPassPlays, 2));
                    //Console.WriteLine("Sit Run/Pass Ratio: {0}", Math.Round(SitRunPercentage, 1));
                    //Console.WriteLine("Sit Screen/Pass Ratio: {0}", Math.Round(SitScreenPercentage, 1));
                    //Console.WriteLine("Sit RPO Ratio: {0}", Math.Round(SitRPOPercentage, 1));


                    // Final checks to see the target values are met before repeating another loop
                    if (SitScreenPercentage < SitTargetScreenPercentage - AcceptableDistanceToTarget ||
                        SitScreenPercentage > SitTargetScreenPercentage + AcceptableDistanceToTarget)
                    {
                        ScreenPercentageAdjustment = SitTargetScreenPercentage - SitScreenPercentage;
                        ScreensComplete = false;
                    }
                    else
                    {
                        ScreensComplete = true;
                        ScreenPercentageAdjustment = 0;
                    }

                    if (SitShotgunPercentage < SitTargetShotgunPercentage - AcceptableDistanceToTarget ||
                        SitShotgunPercentage > SitTargetShotgunPercentage + AcceptableDistanceToTarget)
                    {
                        ShotgunPercentAdjustment = SitTargetShotgunPercentage - SitShotgunPercentage;
                        ShotgunsComplete = false;
                    }
                    else
                    {
                        ShotgunsComplete = true;
                        ShotgunPercentAdjustment = 0;
                    }

                    if (NumWRTEScreenPlays > 0 && (SitWRTEScreenPercentage < SitTargetWRTEScreenPercentage - AcceptableDistanceToTarget ||
                        SitWRTEScreenPercentage > SitTargetWRTEScreenPercentage + AcceptableDistanceToTarget))
                    {
                        WRTEScreenPercentageAdjustment = SitTargetWRTEScreenPercentage - SitWRTEScreenPercentage;
                        WRTEScreensComplete = false;
                    }
                    else
                    {
                        WRTEScreensComplete = true;
                        WRTEScreenPercentageAdjustment = 0;
                    }

                    if (SitRPOPercentage < SitTargetRPOPercentage - AcceptableDistanceToTarget ||
                        SitRPOPercentage > SitTargetRPOPercentage + AcceptableDistanceToTarget)
                    {
                        RPOPercentageAdjustment = SitTargetRPOPercentage - SitRPOPercentage;
                        RPOsComplete = false;
                    }
                    else
                    {
                        RPOsComplete = true;
                        RPOPercentageAdjustment = 0;
                    }

                    if (SitTargetRunPercentage < 100 && SitAvgRouteDepth < SitTargetRouteDepth - AcceptableDistanceRouteDepth ||
                        SitAvgRouteDepth > SitTargetRouteDepth + AcceptableDistanceRouteDepth)
                    {
                        RouteDepthAdjustment = SitTargetRouteDepth - SitAvgRouteDepth;
                        RouteDepthComplete = false;
                    }
                    else
                    {
                        RouteDepthComplete = true;
                        RouteDepthAdjustment = 0;
                    }

                    if (SitPlayActionPercentage < SitTargetPlayActionPercentage - AcceptableDistanceToTarget ||
                        SitPlayActionPercentage > SitTargetPlayActionPercentage + AcceptableDistanceToTarget)
                    {
                        PlayActionPercentAdjustment = SitTargetPlayActionPercentage - SitPlayActionPercentage;
                        PlayActionPercentComplete = false;
                    }
                    else
                    {
                        PlayActionPercentComplete = true;
                        PlayActionPercentAdjustment = 0;
                    }

                    if (SitRunPercentage < SitTargetRunPercentage - AcceptableDistanceToRunPercentTarget ||
                        SitRunPercentage > SitTargetRunPercentage + AcceptableDistanceToRunPercentTarget)
                    {
                        RunPercentAdjustment = SitTargetRunPercentage - SitRunPercentage;
                        RunPercentComplete = false;
                    }
                    else
                    {
                        RunPercentComplete = true;
                        RunPercentAdjustment = 0;
                    }

                    if (SitGapRunPercentage < SitTargetGapRunPercentage - AcceptableDistanceToTarget ||
                        SitGapRunPercentage > SitTargetGapRunPercentage + AcceptableDistanceToTarget)
                    {
                        GapRunPercentAdjustment = SitTargetGapRunPercentage - SitGapRunPercentage;
                        GapRunsComplete = false;
                    }
                    else
                    {
                        GapRunsComplete = true;
                        GapRunPercentAdjustment = 0;
                    }

                    if (SitZoneRunPercentage < SitTargetZoneRunPercentage - AcceptableDistanceToTarget ||
                        SitZoneRunPercentage > SitTargetZoneRunPercentage + AcceptableDistanceToTarget)
                    {
                        ZoneRunPercentAdjustment = SitTargetZoneRunPercentage - SitZoneRunPercentage;
                        ZoneRunsComplete = false;
                    }
                    else
                    {
                        ZoneRunsComplete = true;
                        ZoneRunPercentAdjustment = 0;
                    }

                    // Final override to ignore certain tendencies for situations that we don't want to adjust those tendencies
                    if (IgnoreSomeTendencies)
                    {
                        ShotgunsComplete = ScreensComplete = WRTEScreensComplete = RPOsComplete = PlayActionPercentComplete =
                            GapRunsComplete = ZoneRunsComplete = true;
                        ShotgunPercentAdjustment = ScreenPercentageAdjustment = WRTEScreenPercentageAdjustment = RPOPercentageAdjustment
                            = PlayActionPercentAdjustment = GapRunPercentAdjustment = ZoneRunPercentAdjustment = 0;
                    }
                    if (IgnoreRouteDepth)
                    {
                        RouteDepthComplete = true;
                        RouteDepthAdjustment = 0;
                    }

                    if (ShotgunsComplete && ScreensComplete && WRTEScreensComplete && RPOsComplete && RouteDepthComplete && PlayActionPercentComplete &&
                        RunPercentComplete && GapRunsComplete && ZoneRunsComplete)
                    {
                        SituationComplete = true;
                    }
                    x++;
                } while (!SituationComplete & x < 200);

                Console.WriteLine("SUMMARY FOR SITUATION ID: {0}", Situation.Key + " NAME: " + SituationName); //Each group has a key 
                Console.WriteLine("Sit Pass Weight: {0}", SitPassWeight);
                Console.WriteLine("Sit Run Weight: {0}", SitRunWeight);
                Console.WriteLine("Sit Shotgun Weight: {0}", SitShotgunWeight);
                Console.WriteLine("Sit Screen Weight: {0}", SitScreenWeight);
                Console.WriteLine("Sit WR/TE Screen Weight: {0}", SitWRTEScreenWeight);
                Console.WriteLine("Sit RPO Weight: {0}", SitRPOWeight);
                Console.WriteLine("Sit Play Action Weight: {0}", SitPlayActionWeight);
                Console.WriteLine("Sit Route Depth: {0}", Math.Round(SitWeightedRouteDepth, 1));

                Console.WriteLine("Situation Target Average Route Depth: {0}", Math.Round(SitTargetRouteDepth, 2));
                Console.WriteLine("Sit Actual Average Route Depth: {0}", Math.Round(SitAvgRouteDepth, 2));

                Console.WriteLine("Situation Target Run Percentage: {0}", Math.Round(SitTargetRunPercentage, 2));
                Console.WriteLine("Sit Actual Run Percentage: {0}", Math.Round(SitRunPercentage, 1));

                Console.WriteLine("Situation Target Gap Run Percentage: {0}", Math.Round(SitTargetGapRunPercentage, 2));
                Console.WriteLine("Sit Actual Gap Run Percentage: {0}", Math.Round(SitGapRunPercentage, 1));

                Console.WriteLine("Situation Target Zone Run Percentage: {0}", Math.Round(SitTargetZoneRunPercentage, 2));
                Console.WriteLine("Sit Actual Zone Run Percentage: {0}", Math.Round(SitZoneRunPercentage, 1));

                Console.WriteLine("Situation Target Shotgun Percentage: {0}", Math.Round(SitTargetShotgunPercentage, 2));
                Console.WriteLine("Sit Actual Shotgun Percentage: {0}", Math.Round(SitShotgunPercentage, 1));

                Console.WriteLine("Situation Target Screen Percentage: {0}", Math.Round(SitTargetScreenPercentage, 2));
                Console.WriteLine("Sit Actual Screen Percentage: {0}", Math.Round(SitScreenPercentage, 1));

                Console.WriteLine("Situation Target WR/TE Screen Percentage of all Screens: {0}", Math.Round(SitTargetWRTEScreenPercentage, 2));
                Console.WriteLine("Sit actual WR TE Screen Percentage of all screens: {0}", Math.Round(SitWRTEScreenPercentage, 1));

                Console.WriteLine("Situation Target RPO Percentage: {0}", Math.Round(SitTargetRPOPercentage, 2));
                Console.WriteLine("Sit actual RPO Percentage: {0}", Math.Round(SitRPOPercentage, 1));

                Console.WriteLine("Situation Target Play Action Percentage: {0}", Math.Round(SitTargetPlayActionPercentage, 2));
                Console.WriteLine("Sit actual Playaction Percentage: {0}", Math.Round(SitPlayActionPercentage, 1));

                Console.WriteLine("Sit expected num of runs: {0}", Math.Round(SitExpectedRunPlays, 2));
                Console.WriteLine("Sit expected num of passes: {0}", Math.Round(SitExpectedPassPlays, 2));

                // Add to cumilative PB values before moving on to next situation
                TotalPassWeight += SitPassWeight;
                TotalWeightedRouteDepth += SitWeightedRouteDepth;
                TotalAvgRouteDepth += SitExpectedPassPlays * SitAvgRouteDepth;
                TotalRunWeight += SitRunWeight;
                TotalWeight += SitWeight;
                TotalScreenWeight += SitScreenWeight;
                TotalRPOWeight += SitRPOWeight;
                TotalExpectedRunPlays += SitExpectedRunPlays;
                TotalExpectedGapRunPlays += SitExpectedGapRunPlays;
                TotalExpectedZoneRunPlays += SitExpectedZoneRunPlays;
                TotalExpectedPassPlays += SitExpectedPassPlays;
                TotalExpectedPlays = TotalExpectedRunPlays + TotalExpectedPassPlays;
                TotalExpectedShotgunPlays += SitExpectedShotgunPlays;
                TotalExpectedScreenPlays += SitExpectedScreenPlays;
                TotalExpectedWRTEScreenPlays += SitExpectedWRTEScreenPlays;
                TotalExpectedRPOPlays += SitExpectedRPOPlays;
                TotalExpectedPlayActionPlays += SitExpectedPlayActionPlays;
                TotalRunPercentage = Math.Round(TotalExpectedRunPlays / TotalExpectedPlays * 100.0, 1);
                TotalGapRunPercentage = Math.Round(TotalExpectedGapRunPlays / TotalExpectedRunPlays * 100.0, 1);
                TotalZoneRunPercentage = Math.Round(TotalExpectedZoneRunPlays / TotalExpectedRunPlays * 100.0, 1);
                TotalShotgunPercentage = Math.Round(TotalExpectedShotgunPlays / TotalExpectedPlays * 100.0, 1);
                TotalScreenPassPercentage = Math.Round(TotalExpectedScreenPlays / TotalExpectedPassPlays * 100.0, 1);
                TotalWRTEScreenPercentage = Math.Round(TotalExpectedWRTEScreenPlays / TotalExpectedScreenPlays * 100.0, 1);
                TotalRPOPercentage = Math.Round(TotalExpectedRPOPlays / TotalExpectedPlays * 100.0, 1);
                TotalPlayActionPercentage = Math.Round(TotalExpectedPlayActionPlays / TotalExpectedPlays * 100.0, 1);

                Console.WriteLine("expected run percentage of gameplan: {0}", TotalRunPercentage);
                Console.WriteLine("expected gap run percentage of run plays in gameplan: {0}", TotalGapRunPercentage);
                Console.WriteLine("expected zone run percentage of run plays in gameplan: {0}", TotalZoneRunPercentage);
                Console.WriteLine("average route depth of entire gameplan: {0}", Math.Round(TotalAvgRouteDepth / TotalExpectedPassPlays, 2));
                Console.WriteLine("expected shotgun percentage of plays in gameplan: {0}", TotalShotgunPercentage);
                Console.WriteLine("expected screen pass percentage of pass plays in gameplan: {0}", TotalScreenPassPercentage);
                Console.WriteLine("expected WR TE screen percentage of screen passes in gameplan: {0}", TotalWRTEScreenPercentage);
                Console.WriteLine("expected RPO percentage of gameplan: {0}", TotalRPOPercentage);
                Console.WriteLine("expected Playaction percentage of gameplan: {0}", TotalPlayActionPercentage);

                Console.WriteLine("total expected run plays in gameplan: {0}", Math.Round(TotalExpectedRunPlays, 2));
                Console.WriteLine("total expected pass plays in gameplan: {0}", Math.Round(TotalExpectedPassPlays, 2));
                Console.WriteLine("total expected plays in gameplan: {0}", Math.Round(TotalExpectedPlays, 2));
                Console.WriteLine("total expected shotgun plays in gameplan: {0}", Math.Round(TotalExpectedShotgunPlays, 2));
                Console.WriteLine("total expected screen plays in gameplan: {0}", Math.Round(TotalExpectedScreenPlays, 2));
                Console.WriteLine("total expected WR/TE screen plays in gameplan: {0}", Math.Round(TotalExpectedWRTEScreenPlays, 2));
                Console.WriteLine("total expected RPO plays in gameplan: {0}", Math.Round(TotalExpectedRPOPlays, 2));
                Console.WriteLine("total expected PlayAction plays in gameplan: {0}", Math.Round(TotalExpectedPlayActionPlays, 2));
                Console.WriteLine("total expected gap run plays in gameplan: {0}", Math.Round(TotalExpectedGapRunPlays, 2));
                Console.WriteLine("total expected zone run plays in gameplan: {0}", Math.Round(TotalExpectedZoneRunPlays, 2));
            }
            // remove the entries with a 0 weight, this should get us below 2000
            PBAIRecordCount = PBAI.Where(p => p.prct == 0).Count();
            _pbai = PBAI.Where(p => p.prct == 0).ToList();
            PBAI.RemoveAll(p => _pbai.Contains(p));

            Console.WriteLine("AI Play calling records with 0 weight: " + PBAIRecordCount);
            Console.WriteLine("AI Play calling records remaining: " + PBAI.Count());
            if (PBAI.Count() > 2000)
            {
                // Remove any situation entries that we don't 
                _pbai = PBAI.Where(p => RedDobe.SituationsToTrim.Contains(p.AIGR)).ToList();
                PBAI.RemoveAll(p => _pbai.Contains(p));
                Console.WriteLine("AI Play calling records in the situations to remove: " + _pbai.Count());
                Console.WriteLine("AI Play calling records remaining: " + PBAI.Count());

                if (PBAI.Count() > 2000)
                {
                    int NumRecordsToRemove = PBAI.Count() - 2000;
                    Console.WriteLine("Trimming more entries: " + NumRecordsToRemove);
                    // Remove 1 prct entries first
                    _pbai = PBAI.Where(p => ((Gameplan.KeySituations1.Contains(p.AIGR) || Gameplan.KeySituations2.Contains(p.AIGR)) ||
                    RedDobe.SituationsToAddAllPlays.Contains(p.AIGR)) && p.prct == 1).ToList();
                    // Add 2 prct entries to the list so that they get removed last
                    _pbai.AddRange(PBAI.Where(p => ((Gameplan.KeySituations1.Contains(p.AIGR) || Gameplan.KeySituations2.Contains(p.AIGR)) ||
                    RedDobe.SituationsToAddAllPlays.Contains(p.AIGR)) && p.prct == 2).ToList());

                    foreach (Madden.TeamPlaybook.PBAI pbai in _pbai)
                    {
                        if (NumRecordsToRemove > 0)
                        {
                            PBAI.Remove(pbai);
                        }
                        else
                            break;
                        NumRecordsToRemove--;
                    }
                    Console.WriteLine("AI Play calling records remaining after final removal: " + PBAI.Count());
                }
            }

            PBAI = Madden.TeamPlaybook.PBAI.Sort(PBAI);
            for (int i = 0; i < PBAI.Count(); i++)
            {
                PBAI[i].rec = i;
            }

            foreach (PlayVM _play in Plays)
            {
                _play.GetSituations();
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