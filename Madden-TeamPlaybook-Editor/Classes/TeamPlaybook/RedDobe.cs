using Madden.TeamPlaybook;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using static MaddenTeamPlaybookEditor.ViewModels.TeamPlaybook;

namespace MaddenTeamPlaybookEditor.ViewModels
{
    public class RedDobe
    {
        // int is the Situation ID
        public static readonly Dictionary<string, List<KeyValuePair<int, Tendency>>> TeamTendencies = new Dictionary<string, List<KeyValuePair<int, Tendency>>>
        {
            {"cardinals", new List<KeyValuePair<int, Tendency>> // 100 WIll be the default value in the event the sit id is not found, it will be an overall tendency value
				{
                    new KeyValuePair<int, Tendency>(100, new Tendency { PA = 16, RPO = 10, Shotgun = 72, Screen = 10, Gap = 40, Zone = 58 } ), //Overall
					new KeyValuePair<int, Tendency>(35, new Tendency { PA = 22, RPO = 10, Shotgun = 57, Screen = 9, Gap = 39, Zone = 57 } ), //1st down
					new KeyValuePair<int, Tendency>(20, new Tendency { PA = 6, RPO = 10, Shotgun = 52, Screen = 10, Gap = 29, Zone = 71 } ), //2nd & short
					new KeyValuePair<int, Tendency>(17, new Tendency { PA = 28, RPO = 5, Shotgun = 55, Screen = 9, Gap = 33, Zone = 67 } ), //2nd & medium
					new KeyValuePair<int, Tendency>(6, new Tendency { PA = 18, RPO = 19, Shotgun = 80, Screen = 16, Gap = 55, Zone = 45 } ), //2nd & long
					new KeyValuePair<int, Tendency>(1, new Tendency { PA = 7, RPO = 0, Shotgun = 65, Screen = 0, Gap = 14, Zone = 86 } ), //3rd & short
					new KeyValuePair<int, Tendency>(7, new Tendency { PA = 3, RPO = 3, Shotgun = 100, Screen = 0, Gap = 67, Zone = 33 } ), //3rd & medium
					new KeyValuePair<int, Tendency>(8, new Tendency { PA = 3, RPO = 0, Shotgun = 100, Screen = 8, Gap = 100, Zone = 0 } ), //3rd & long
					new KeyValuePair<int, Tendency>(40, new Tendency { PA = 3, RPO = 6, Shotgun = 100, Screen = 20, Gap = 33, Zone = 50 } ), //3rd & extra long
					new KeyValuePair<int, Tendency>(25, new Tendency { PA = 19, RPO = 10, Shotgun = 63, Screen = 9, Gap = 41, Zone = 57 } ), //Opp 30 to 21
					new KeyValuePair<int, Tendency>(26, new Tendency { PA = 12, RPO = 12, Shotgun = 63, Screen = 14, Gap = 30, Zone = 70 } ), //Red Zone 16 to 20
					new KeyValuePair<int, Tendency>(27, new Tendency { PA = 12, RPO = 12, Shotgun = 63, Screen = 14, Gap = 30, Zone = 70 } ), //Red Zone 11 to 15
					new KeyValuePair<int, Tendency>(28, new Tendency { PA = 12, RPO = 12, Shotgun = 63, Screen = 14, Gap = 30, Zone = 70 } ), //Red Zone 6 to 10
					new KeyValuePair<int, Tendency>(3, new Tendency { PA = 0, RPO = 8, Shotgun = 50, Screen = 0, Gap = 18, Zone = 82 } ), //Goal Line
					new KeyValuePair<int, Tendency>(22, new Tendency { PA = 0, RPO = 0, Shotgun = 100, Screen = 0, Gap = 0, Zone = 0 } ), //GOal Line Pass
					new KeyValuePair<int, Tendency>(13, new Tendency { PA = 14, RPO = 5, Shotgun = 50, Screen = 8, Gap = 38, Zone = 60 } ), //Waste Time
				}
            },
            {"falcons", new List<KeyValuePair<int, Tendency>>
                {
                    new KeyValuePair<int, Tendency>(100, new Tendency { PA = 16, RPO = 9, Shotgun = 76, Screen = 8, Gap = 17, Zone = 83 } ),
                    new KeyValuePair<int, Tendency>(35, new Tendency { PA = 19, RPO = 11, Shotgun = 71, Screen = 10, Gap = 16, Zone = 84 } ),
                    new KeyValuePair<int, Tendency>(20, new Tendency { PA = 10, RPO = 21, Shotgun = 70, Screen = 8, Gap = 7, Zone = 93 } ),
                    new KeyValuePair<int, Tendency>(17, new Tendency { PA = 21, RPO = 6, Shotgun = 72, Screen = 8, Gap = 11, Zone = 89 } ),
                    new KeyValuePair<int, Tendency>(6, new Tendency { PA = 22, RPO = 8, Shotgun = 82, Screen = 8, Gap = 28, Zone = 72 } ),
                    new KeyValuePair<int, Tendency>(1, new Tendency { PA = 17, RPO = 13, Shotgun = 53, Screen = 0, Gap = 19, Zone = 81 } ),
                    new KeyValuePair<int, Tendency>(7, new Tendency { PA = 0, RPO = 0, Shotgun = 100, Screen = 0, Gap = 33, Zone = 67 } ),
                    new KeyValuePair<int, Tendency>(8, new Tendency { PA = 0, RPO = 2, Shotgun = 100, Screen = 4, Gap = 67, Zone = 33 } ),
                    new KeyValuePair<int, Tendency>(40, new Tendency { PA = 0, RPO = 0, Shotgun = 100, Screen = 18, Gap = 0, Zone = 100 } ),
                    new KeyValuePair<int, Tendency>(25, new Tendency { PA = 19, RPO = 9, Shotgun = 74, Screen = 8, Gap = 18, Zone = 82 } ),
                    new KeyValuePair<int, Tendency>(26, new Tendency { PA = 14, RPO = 14, Shotgun = 72, Screen = 17, Gap = 6, Zone = 94 } ),
                    new KeyValuePair<int, Tendency>(27, new Tendency { PA = 14, RPO = 14, Shotgun = 72, Screen = 17, Gap = 6, Zone = 94 } ),
                    new KeyValuePair<int, Tendency>(28, new Tendency { PA = 14, RPO = 14, Shotgun = 72, Screen = 17, Gap = 6, Zone = 94 } ),
                    new KeyValuePair<int, Tendency>(3, new Tendency { PA = 25, RPO = 31, Shotgun = 59, Screen = 0, Gap = 0, Zone = 100 } ),
                    new KeyValuePair<int, Tendency>(22, new Tendency { PA = 17, RPO = 33, Shotgun = 83, Screen = 25, Gap = 50, Zone = 50 } ),
                    new KeyValuePair<int, Tendency>(13, new Tendency { PA = 15, RPO = 5, Shotgun = 53, Screen = 8, Gap = 18, Zone = 82 } ),
                }
            },
            {"ravens", new List<KeyValuePair<int, Tendency>>
                {
                    new KeyValuePair<int, Tendency>(100, new Tendency { PA = 19, RPO = 13, Shotgun = 85, Screen = 10, Gap = 47, Zone = 46 } ),
                    new KeyValuePair<int, Tendency>(35, new Tendency { PA = 26, RPO = 12, Shotgun = 83, Screen = 11, Gap = 51, Zone = 47 } ),
                    new KeyValuePair<int, Tendency>(20, new Tendency { PA = 26, RPO = 32, Shotgun = 89, Screen = 22, Gap = 34, Zone = 59 } ),
                    new KeyValuePair<int, Tendency>(17, new Tendency { PA = 14, RPO = 20, Shotgun = 82, Screen = 5, Gap = 52, Zone = 39 } ),
                    new KeyValuePair<int, Tendency>(6, new Tendency { PA = 15, RPO = 11, Shotgun = 93, Screen = 9, Gap = 43, Zone = 37 } ),
                    new KeyValuePair<int, Tendency>(1, new Tendency { PA = 16, RPO = 19, Shotgun = 85, Screen = 15, Gap = 33, Zone = 67 } ),
                    new KeyValuePair<int, Tendency>(7, new Tendency { PA = 3, RPO = 0, Shotgun = 100, Screen = 6, Gap = 33, Zone = 67 } ),
                    new KeyValuePair<int, Tendency>(8, new Tendency { PA = 4, RPO = 0, Shotgun = 100, Screen = 4, Gap = 100, Zone = 0 } ),
                    new KeyValuePair<int, Tendency>(40, new Tendency { PA = 4, RPO = 13, Shotgun = 100, Screen = 10, Gap = 0, Zone = 33 } ),
                    new KeyValuePair<int, Tendency>(25, new Tendency { PA = 23, RPO = 15, Shotgun = 86, Screen = 12, Gap = 43, Zone = 52 } ),
                    new KeyValuePair<int, Tendency>(26, new Tendency { PA = 16, RPO = 9, Shotgun = 80, Screen = 13, Gap = 63, Zone = 36 } ),
                    new KeyValuePair<int, Tendency>(27, new Tendency { PA = 16, RPO = 9, Shotgun = 80, Screen = 13, Gap = 63, Zone = 36 } ),
                    new KeyValuePair<int, Tendency>(28, new Tendency { PA = 16, RPO = 9, Shotgun = 80, Screen = 13, Gap = 63, Zone = 36 } ),
                    new KeyValuePair<int, Tendency>(3, new Tendency { PA = 25, RPO = 10, Shotgun = 54, Screen = 0, Gap = 53, Zone = 47 } ),
                    new KeyValuePair<int, Tendency>(22, new Tendency { PA = 0, RPO = 0, Shotgun = 100, Screen = 0, Gap = 0, Zone = 0 } ),
                    new KeyValuePair<int, Tendency>(13, new Tendency { PA = 19, RPO = 7, Shotgun = 54, Screen = 10, Gap = 47, Zone = 47 } ),

                }
            },
            {"bills", new List<KeyValuePair<int, Tendency>>
                {
                    new KeyValuePair<int, Tendency>(100, new Tendency { PA = 16, RPO = 20, Shotgun = 74, Screen = 8, Gap = 50, Zone = 48 } ),
                    new KeyValuePair<int, Tendency>(35, new Tendency { PA = 24, RPO = 26, Shotgun = 68, Screen = 11, Gap = 54, Zone = 45 } ),
                    new KeyValuePair<int, Tendency>(20, new Tendency { PA = 9, RPO = 34, Shotgun = 73, Screen = 19, Gap = 43, Zone = 57 } ),
                    new KeyValuePair<int, Tendency>(17, new Tendency { PA = 21, RPO = 19, Shotgun = 67, Screen = 7, Gap = 21, Zone = 79 } ),
                    new KeyValuePair<int, Tendency>(6, new Tendency { PA = 13, RPO = 12, Shotgun = 90, Screen = 7, Gap = 75, Zone = 25 } ),
                    new KeyValuePair<int, Tendency>(1, new Tendency { PA = 3, RPO = 34, Shotgun = 74, Screen = 0, Gap = 56, Zone = 44 } ),
                    new KeyValuePair<int, Tendency>(7, new Tendency { PA = 0, RPO = 6, Shotgun = 100, Screen = 0, Gap = 0, Zone = 0 } ),
                    new KeyValuePair<int, Tendency>(8, new Tendency { PA = 0, RPO = 0, Shotgun = 100, Screen = 0, Gap = 0, Zone = 0 } ),
                    new KeyValuePair<int, Tendency>(40, new Tendency { PA = 0, RPO = 0, Shotgun = 100, Screen = 6, Gap = 0, Zone = 100 } ),
                    new KeyValuePair<int, Tendency>(25, new Tendency { PA = 19, RPO = 21, Shotgun = 74, Screen = 10, Gap = 48, Zone = 52 } ),
                    new KeyValuePair<int, Tendency>(26, new Tendency { PA = 16, RPO = 24, Shotgun = 78, Screen = 15, Gap = 47, Zone = 47 } ),
                    new KeyValuePair<int, Tendency>(27, new Tendency { PA = 16, RPO = 24, Shotgun = 78, Screen = 15, Gap = 47, Zone = 47 } ),
                    new KeyValuePair<int, Tendency>(28, new Tendency { PA = 16, RPO = 24, Shotgun = 78, Screen = 15, Gap = 47, Zone = 47 } ),
                    new KeyValuePair<int, Tendency>(3, new Tendency { PA = 21, RPO = 7, Shotgun = 44, Screen = 0, Gap = 40, Zone = 60 } ),
                    new KeyValuePair<int, Tendency>(22, new Tendency { PA = 14, RPO = 14, Shotgun = 100, Screen = 0, Gap = 0, Zone = 0 } ),
                    new KeyValuePair<int, Tendency>(13, new Tendency { PA = 16, RPO = 10, Shotgun = 44, Screen = 8, Gap = 48, Zone = 50 } ),


                }
            },
            {"panthers", new List<KeyValuePair<int, Tendency>>
            {
                new KeyValuePair<int, Tendency>(100, new Tendency { PA = 13, RPO = 12, Shotgun = 87, Screen = 9, Gap = 32, Zone = 68 } ),
                new KeyValuePair<int, Tendency>(35, new Tendency { PA = 20, RPO = 14, Shotgun = 82, Screen = 7, Gap = 31, Zone = 69 } ),
                new KeyValuePair<int, Tendency>(20, new Tendency { PA = 14, RPO = 25, Shotgun = 83, Screen = 0, Gap = 33, Zone = 67 } ),
                new KeyValuePair<int, Tendency>(17, new Tendency { PA = 9, RPO = 30, Shotgun = 78, Screen = 8, Gap = 39, Zone = 61 } ),
                new KeyValuePair<int, Tendency>(6, new Tendency { PA = 12, RPO = 10, Shotgun = 88, Screen = 11, Gap = 35, Zone = 63 } ),
                new KeyValuePair<int, Tendency>(1, new Tendency { PA = 7, RPO = 10, Shotgun = 91, Screen = 8, Gap = 17, Zone = 83 } ),
                new KeyValuePair<int, Tendency>(7, new Tendency { PA = 0, RPO = 0, Shotgun = 100, Screen = 4, Gap = 0, Zone = 100 } ),
                new KeyValuePair<int, Tendency>(8, new Tendency { PA = 0, RPO = 0, Shotgun = 100, Screen = 2, Gap = 0, Zone = 0 } ),
                new KeyValuePair<int, Tendency>(40, new Tendency { PA = 3, RPO = 0, Shotgun = 100, Screen = 30, Gap = 0, Zone = 100 } ),
                new KeyValuePair<int, Tendency>(25, new Tendency { PA = 14, RPO = 13, Shotgun = 83, Screen = 7, Gap = 33, Zone = 66 } ),
                new KeyValuePair<int, Tendency>(26, new Tendency { PA = 15, RPO = 17, Shotgun = 75, Screen = 4, Gap = 35, Zone = 65 } ),
                new KeyValuePair<int, Tendency>(27, new Tendency { PA = 15, RPO = 17, Shotgun = 75, Screen = 4, Gap = 35, Zone = 65 } ),
                new KeyValuePair<int, Tendency>(28, new Tendency { PA = 15, RPO = 17, Shotgun = 75, Screen = 4, Gap = 35, Zone = 65 } ),
                new KeyValuePair<int, Tendency>(3, new Tendency { PA = 18, RPO = 9, Shotgun = 82, Screen = 0, Gap = 13, Zone = 88 } ),
                new KeyValuePair<int, Tendency>(22, new Tendency { PA = 0, RPO = 0, Shotgun = 100, Screen = 0, Gap = 0, Zone = 0 } ),
                new KeyValuePair<int, Tendency>(13, new Tendency { PA = 12, RPO = 6, Shotgun = 75, Screen = 8, Gap = 32, Zone = 68 } ),

            }
            },
            {"bears", new List<KeyValuePair<int, Tendency>>
                {
                    new KeyValuePair<int, Tendency>(100, new Tendency { PA = 19, RPO = 13, Shotgun = 76, Screen = 13, Gap = 26, Zone = 72 } ),
                    new KeyValuePair<int, Tendency>(35, new Tendency { PA = 25, RPO = 13, Shotgun = 72, Screen = 15, Gap = 30, Zone = 69 } ),
                    new KeyValuePair<int, Tendency>(20, new Tendency { PA = 21, RPO = 27, Shotgun = 81, Screen = 8, Gap = 33, Zone = 67 } ),
                    new KeyValuePair<int, Tendency>(17, new Tendency { PA = 27, RPO = 14, Shotgun = 73, Screen = 17, Gap = 19, Zone = 78 } ),
                    new KeyValuePair<int, Tendency>(6, new Tendency { PA = 17, RPO = 16, Shotgun = 77, Screen = 12, Gap = 22, Zone = 78 } ),
                    new KeyValuePair<int, Tendency>(1, new Tendency { PA = 11, RPO = 19, Shotgun = 64, Screen = 18, Gap = 21, Zone = 79 } ),
                    new KeyValuePair<int, Tendency>(7, new Tendency { PA = 0, RPO = 10, Shotgun = 100, Screen = 12, Gap = 0, Zone = 80 } ),
                    new KeyValuePair<int, Tendency>(8, new Tendency { PA = 2, RPO = 5, Shotgun = 100, Screen = 8, Gap = 33, Zone = 67 } ),
                    new KeyValuePair<int, Tendency>(40, new Tendency { PA = 4, RPO = 4, Shotgun = 100, Screen = 19, Gap = 50, Zone = 50 } ),
                    new KeyValuePair<int, Tendency>(25, new Tendency { PA = 20, RPO = 14, Shotgun = 74, Screen = 13, Gap = 26, Zone = 72 } ),
                    new KeyValuePair<int, Tendency>(26, new Tendency { PA = 31, RPO = 16, Shotgun = 60, Screen = 13, Gap = 34, Zone = 66 } ),
                    new KeyValuePair<int, Tendency>(27, new Tendency { PA = 31, RPO = 16, Shotgun = 60, Screen = 13, Gap = 34, Zone = 66 } ),
                    new KeyValuePair<int, Tendency>(28, new Tendency { PA = 31, RPO = 16, Shotgun = 60, Screen = 13, Gap = 34, Zone = 66 } ),
                    new KeyValuePair<int, Tendency>(3, new Tendency { PA = 10, RPO = 20, Shotgun = 47, Screen = 0, Gap = 50, Zone = 50 } ),
                    new KeyValuePair<int, Tendency>(22, new Tendency { PA = 0, RPO = 13, Shotgun = 100, Screen = 17, Gap = 50, Zone = 50 } ),
                    new KeyValuePair<int, Tendency>(13, new Tendency { PA = 18, RPO = 7, Shotgun = 47, Screen = 13, Gap = 27, Zone = 72 } ),

                }
            },
            {"bengals", new List<KeyValuePair<int, Tendency>>
                {
                    new KeyValuePair<int, Tendency>(100, new Tendency { PA = 17, RPO = 20, Shotgun = 87, Screen = 12, Gap = 44, Zone = 54 } ),
                    new KeyValuePair<int, Tendency>(35, new Tendency { PA = 24, RPO = 27, Shotgun = 83, Screen = 16, Gap = 48, Zone = 52 } ),
                    new KeyValuePair<int, Tendency>(20, new Tendency { PA = 26, RPO = 35, Shotgun = 90, Screen = 14, Gap = 38, Zone = 62 } ),
                    new KeyValuePair<int, Tendency>(17, new Tendency { PA = 24, RPO = 22, Shotgun = 89, Screen = 8, Gap = 42, Zone = 58 } ),
                    new KeyValuePair<int, Tendency>(6, new Tendency { PA = 8, RPO = 14, Shotgun = 98, Screen = 16, Gap = 42, Zone = 50 } ),
                    new KeyValuePair<int, Tendency>(1, new Tendency { PA = 12, RPO = 9, Shotgun = 91, Screen = 15, Gap = 43, Zone = 57 } ),
                    new KeyValuePair<int, Tendency>(7, new Tendency { PA = 4, RPO = 0, Shotgun = 100, Screen = 0, Gap = 0, Zone = 0 } ),
                    new KeyValuePair<int, Tendency>(8, new Tendency { PA = 2, RPO = 2, Shotgun = 100, Screen = 0, Gap = 50, Zone = 0 } ),
                    new KeyValuePair<int, Tendency>(40, new Tendency { PA = 11, RPO = 0, Shotgun = 100, Screen = 19, Gap = 0, Zone = 50 } ),
                    new KeyValuePair<int, Tendency>(25, new Tendency { PA = 19, RPO = 22, Shotgun = 88, Screen = 15, Gap = 46, Zone = 53 } ),
                    new KeyValuePair<int, Tendency>(26, new Tendency { PA = 16, RPO = 25, Shotgun = 91, Screen = 13, Gap = 34, Zone = 66 } ),
                    new KeyValuePair<int, Tendency>(27, new Tendency { PA = 16, RPO = 25, Shotgun = 91, Screen = 13, Gap = 34, Zone = 66 } ),
                    new KeyValuePair<int, Tendency>(28, new Tendency { PA = 16, RPO = 25, Shotgun = 91, Screen = 13, Gap = 34, Zone = 66 } ),
                    new KeyValuePair<int, Tendency>(3, new Tendency { PA = 19, RPO = 38, Shotgun = 83, Screen = 0, Gap = 27, Zone = 73 } ),
                    new KeyValuePair<int, Tendency>(22, new Tendency { PA = 0, RPO = 0, Shotgun = 100, Screen = 0, Gap = 100, Zone = 0 } ),
                    new KeyValuePair<int, Tendency>(13, new Tendency { PA = 15, RPO = 10, Shotgun = 83, Screen = 12, Gap = 43, Zone = 55 } ),

                }
            },
            {"browns", new List<KeyValuePair<int, Tendency>>
                {
                    new KeyValuePair<int, Tendency>(100, new Tendency { PA = 16, RPO = 8, Shotgun = 65, Screen = 9, Gap = 49, Zone = 48 } ),
                    new KeyValuePair<int, Tendency>(35, new Tendency { PA = 24, RPO = 12, Shotgun = 57, Screen = 9, Gap = 52, Zone = 46 } ),
                    new KeyValuePair<int, Tendency>(20, new Tendency { PA = 16, RPO = 13, Shotgun = 41, Screen = 33, Gap = 42, Zone = 58 } ),
                    new KeyValuePair<int, Tendency>(17, new Tendency { PA = 24, RPO = 8, Shotgun = 51, Screen = 8, Gap = 39, Zone = 61 } ),
                    new KeyValuePair<int, Tendency>(6, new Tendency { PA = 12, RPO = 5, Shotgun = 81, Screen = 12, Gap = 38, Zone = 44 } ),
                    new KeyValuePair<int, Tendency>(1, new Tendency { PA = 5, RPO = 8, Shotgun = 62, Screen = 0, Gap = 37, Zone = 63 } ),
                    new KeyValuePair<int, Tendency>(7, new Tendency { PA = 0, RPO = 6, Shotgun = 100, Screen = 0, Gap = 60, Zone = 20 } ),
                    new KeyValuePair<int, Tendency>(8, new Tendency { PA = 2, RPO = 0, Shotgun = 100, Screen = 11, Gap = 0, Zone = 0 } ),
                    new KeyValuePair<int, Tendency>(40, new Tendency { PA = 0, RPO = 0, Shotgun = 100, Screen = 9, Gap = 100, Zone = 0 } ),
                    new KeyValuePair<int, Tendency>(25, new Tendency { PA = 18, RPO = 9, Shotgun = 62, Screen = 11, Gap = 45, Zone = 51 } ),
                    new KeyValuePair<int, Tendency>(26, new Tendency { PA = 25, RPO = 6, Shotgun = 60, Screen = 9, Gap = 60, Zone = 40 } ),
                    new KeyValuePair<int, Tendency>(27, new Tendency { PA = 25, RPO = 6, Shotgun = 60, Screen = 9, Gap = 60, Zone = 40 } ),
                    new KeyValuePair<int, Tendency>(28, new Tendency { PA = 25, RPO = 6, Shotgun = 60, Screen = 9, Gap = 60, Zone = 40 } ),
                    new KeyValuePair<int, Tendency>(3, new Tendency { PA = 23, RPO = 8, Shotgun = 27, Screen = 0, Gap = 63, Zone = 38 } ),
                    new KeyValuePair<int, Tendency>(22, new Tendency { PA = 14, RPO = 0, Shotgun = 86, Screen = 0, Gap = 100, Zone = 0 } ),
                    new KeyValuePair<int, Tendency>(13, new Tendency { PA = 15, RPO = 4, Shotgun = 27, Screen = 9, Gap = 48, Zone = 49 } ),

                }
            },
            {"cowboys", new List<KeyValuePair<int, Tendency>>
                {
                    new KeyValuePair<int, Tendency>(100, new Tendency { PA = 21, RPO = 8, Shotgun = 63, Screen = 8, Gap = 42, Zone = 55 } ),
                    new KeyValuePair<int, Tendency>(35, new Tendency { PA = 23, RPO = 12, Shotgun = 57, Screen = 8, Gap = 44, Zone = 55 } ),
                    new KeyValuePair<int, Tendency>(20, new Tendency { PA = 18, RPO = 8, Shotgun = 46, Screen = 7, Gap = 48, Zone = 52 } ),
                    new KeyValuePair<int, Tendency>(17, new Tendency { PA = 30, RPO = 7, Shotgun = 53, Screen = 11, Gap = 44, Zone = 56 } ),
                    new KeyValuePair<int, Tendency>(6, new Tendency { PA = 30, RPO = 7, Shotgun = 69, Screen = 11, Gap = 47, Zone = 41 } ),
                    new KeyValuePair<int, Tendency>(1, new Tendency { PA = 13, RPO = 9, Shotgun = 61, Screen = 5, Gap = 23, Zone = 77 } ),
                    new KeyValuePair<int, Tendency>(7, new Tendency { PA = 13, RPO = 3, Shotgun = 100, Screen = 0, Gap = 33, Zone = 67 } ),
                    new KeyValuePair<int, Tendency>(8, new Tendency { PA = 3, RPO = 0, Shotgun = 100, Screen = 0, Gap = 100, Zone = 0 } ),
                    new KeyValuePair<int, Tendency>(40, new Tendency { PA = 0, RPO = 0, Shotgun = 100, Screen = 6, Gap = 100, Zone = 0 } ),
                    new KeyValuePair<int, Tendency>(25, new Tendency { PA = 24, RPO = 9, Shotgun = 59, Screen = 10, Gap = 39, Zone = 58 } ),
                    new KeyValuePair<int, Tendency>(26, new Tendency { PA = 20, RPO = 11, Shotgun = 68, Screen = 10, Gap = 42, Zone = 53 } ),
                    new KeyValuePair<int, Tendency>(27, new Tendency { PA = 20, RPO = 11, Shotgun = 68, Screen = 10, Gap = 42, Zone = 53 } ),
                    new KeyValuePair<int, Tendency>(28, new Tendency { PA = 20, RPO = 11, Shotgun = 68, Screen = 10, Gap = 42, Zone = 53 } ),
                    new KeyValuePair<int, Tendency>(3, new Tendency { PA = 26, RPO = 5, Shotgun = 43, Screen = 0, Gap = 50, Zone = 50 } ),
                    new KeyValuePair<int, Tendency>(22, new Tendency { PA = 11, RPO = 0, Shotgun = 100, Screen = 22, Gap = 0, Zone = 0 } ),
                    new KeyValuePair<int, Tendency>(13, new Tendency { PA = 20, RPO = 4, Shotgun = 43, Screen = 9, Gap = 42, Zone = 55 } ),

                }
            },
            {"broncos", new List<KeyValuePair<int, Tendency>>
                {
                    new KeyValuePair<int, Tendency>(100, new Tendency { PA = 15, RPO = 10, Shotgun = 57, Screen = 12, Gap = 31, Zone = 66 } ),
                    new KeyValuePair<int, Tendency>(35, new Tendency { PA = 20, RPO = 12, Shotgun = 47, Screen = 8, Gap = 31, Zone = 69 } ),
                    new KeyValuePair<int, Tendency>(20, new Tendency { PA = 7, RPO = 10, Shotgun = 42, Screen = 11, Gap = 30, Zone = 70 } ),
                    new KeyValuePair<int, Tendency>(17, new Tendency { PA = 16, RPO = 13, Shotgun = 53, Screen = 0, Gap = 33, Zone = 60 } ),
                    new KeyValuePair<int, Tendency>(6, new Tendency { PA = 19, RPO = 12, Shotgun = 63, Screen = 27, Gap = 33, Zone = 57 } ),
                    new KeyValuePair<int, Tendency>(1, new Tendency { PA = 12, RPO = 15, Shotgun = 49, Screen = 0, Gap = 15, Zone = 85 } ),
                    new KeyValuePair<int, Tendency>(7, new Tendency { PA = 0, RPO = 7, Shotgun = 100, Screen = 0, Gap = 0, Zone = 75 } ),
                    new KeyValuePair<int, Tendency>(8, new Tendency { PA = 4, RPO = 0, Shotgun = 100, Screen = 15, Gap = 0, Zone = 100 } ),
                    new KeyValuePair<int, Tendency>(40, new Tendency { PA = 0, RPO = 0, Shotgun = 100, Screen = 10, Gap = 50, Zone = 0 } ),
                    new KeyValuePair<int, Tendency>(25, new Tendency { PA = 18, RPO = 12, Shotgun = 52, Screen = 13, Gap = 31, Zone = 67 } ),
                    new KeyValuePair<int, Tendency>(26, new Tendency { PA = 13, RPO = 6, Shotgun = 53, Screen = 5, Gap = 36, Zone = 64 } ),
                    new KeyValuePair<int, Tendency>(27, new Tendency { PA = 13, RPO = 6, Shotgun = 53, Screen = 5, Gap = 36, Zone = 64 } ),
                    new KeyValuePair<int, Tendency>(28, new Tendency { PA = 13, RPO = 6, Shotgun = 53, Screen = 5, Gap = 36, Zone = 64 } ),
                    new KeyValuePair<int, Tendency>(3, new Tendency { PA = 27, RPO = 0, Shotgun = 36, Screen = 0, Gap = 38, Zone = 63 } ),
                    new KeyValuePair<int, Tendency>(22, new Tendency { PA = 9, RPO = 9, Shotgun = 91, Screen = 10, Gap = 0, Zone = 0 } ),
                    new KeyValuePair<int, Tendency>(13, new Tendency { PA = 15, RPO = 5, Shotgun = 36, Screen = 11, Gap = 31, Zone = 66 } ),

                }
            },
            {"lions", new List<KeyValuePair<int, Tendency>>
                {
                    new KeyValuePair<int, Tendency>(100, new Tendency { PA = 15, RPO = 6, Shotgun = 58, Screen = 12, Gap = 45, Zone = 55 } ),
                    new KeyValuePair<int, Tendency>(35, new Tendency { PA = 22, RPO = 5, Shotgun = 38, Screen = 13, Gap = 39, Zone = 60 } ),
                    new KeyValuePair<int, Tendency>(20, new Tendency { PA = 29, RPO = 0, Shotgun = 56, Screen = 18, Gap = 40, Zone = 60 } ),
                    new KeyValuePair<int, Tendency>(17, new Tendency { PA = 20, RPO = 11, Shotgun = 43, Screen = 4, Gap = 33, Zone = 67 } ),
                    new KeyValuePair<int, Tendency>(6, new Tendency { PA = 8, RPO = 9, Shotgun = 83, Screen = 11, Gap = 68, Zone = 33 } ),
                    new KeyValuePair<int, Tendency>(1, new Tendency { PA = 7, RPO = 14, Shotgun = 77, Screen = 4, Gap = 39, Zone = 61 } ),
                    new KeyValuePair<int, Tendency>(7, new Tendency { PA = 0, RPO = 4, Shotgun = 95, Screen = 14, Gap = 0, Zone = 100 } ),
                    new KeyValuePair<int, Tendency>(8, new Tendency { PA = 0, RPO = 0, Shotgun = 100, Screen = 19, Gap = 67, Zone = 33 } ),
                    new KeyValuePair<int, Tendency>(40, new Tendency { PA = 0, RPO = 0, Shotgun = 100, Screen = 5, Gap = 0, Zone = 67 } ),
                    new KeyValuePair<int, Tendency>(25, new Tendency { PA = 17, RPO = 6, Shotgun = 51, Screen = 10, Gap = 40, Zone = 60 } ),
                    new KeyValuePair<int, Tendency>(26, new Tendency { PA = 14, RPO = 4, Shotgun = 40, Screen = 11, Gap = 52, Zone = 48 } ),
                    new KeyValuePair<int, Tendency>(27, new Tendency { PA = 14, RPO = 4, Shotgun = 40, Screen = 11, Gap = 52, Zone = 48 } ),
                    new KeyValuePair<int, Tendency>(28, new Tendency { PA = 14, RPO = 4, Shotgun = 40, Screen = 11, Gap = 52, Zone = 48 } ),
                    new KeyValuePair<int, Tendency>(3, new Tendency { PA = 13, RPO = 0, Shotgun = 29, Screen = 0, Gap = 73, Zone = 27 } ),
                    new KeyValuePair<int, Tendency>(22, new Tendency { PA = 7, RPO = 0, Shotgun = 80, Screen = 25, Gap = 33, Zone = 67 } ),
                    new KeyValuePair<int, Tendency>(13, new Tendency { PA = 14, RPO = 3, Shotgun = 29, Screen = 11, Gap = 44, Zone = 55 } ),

                }
            },
            {"packers", new List<KeyValuePair<int, Tendency>>
                {
                    new KeyValuePair<int, Tendency>(100, new Tendency { PA = 18, RPO = 17, Shotgun = 73, Screen = 12, Gap = 33, Zone = 64 } ),
                    new KeyValuePair<int, Tendency>(35, new Tendency { PA = 25, RPO = 19, Shotgun = 60, Screen = 16, Gap = 35, Zone = 63 } ),
                    new KeyValuePair<int, Tendency>(20, new Tendency { PA = 25, RPO = 36, Shotgun = 64, Screen = 12, Gap = 42, Zone = 58 } ),
                    new KeyValuePair<int, Tendency>(17, new Tendency { PA = 18, RPO = 11, Shotgun = 65, Screen = 4, Gap = 18, Zone = 82 } ),
                    new KeyValuePair<int, Tendency>(6, new Tendency { PA = 17, RPO = 16, Shotgun = 84, Screen = 12, Gap = 41, Zone = 59 } ),
                    new KeyValuePair<int, Tendency>(1, new Tendency { PA = 6, RPO = 21, Shotgun = 74, Screen = 6, Gap = 41, Zone = 59 } ),
                    new KeyValuePair<int, Tendency>(7, new Tendency { PA = 5, RPO = 9, Shotgun = 100, Screen = 5, Gap = 0, Zone = 100 } ),
                    new KeyValuePair<int, Tendency>(8, new Tendency { PA = 0, RPO = 0, Shotgun = 100, Screen = 3, Gap = 0, Zone = 0 } ),
                    new KeyValuePair<int, Tendency>(40, new Tendency { PA = 0, RPO = 4, Shotgun = 100, Screen = 24, Gap = 0, Zone = 0 } ),
                    new KeyValuePair<int, Tendency>(25, new Tendency { PA = 21, RPO = 17, Shotgun = 67, Screen = 12, Gap = 34, Zone = 66 } ),
                    new KeyValuePair<int, Tendency>(26, new Tendency { PA = 15, RPO = 32, Shotgun = 77, Screen = 22, Gap = 20, Zone = 69 } ),
                    new KeyValuePair<int, Tendency>(27, new Tendency { PA = 15, RPO = 32, Shotgun = 77, Screen = 22, Gap = 20, Zone = 69 } ),
                    new KeyValuePair<int, Tendency>(28, new Tendency { PA = 15, RPO = 32, Shotgun = 77, Screen = 22, Gap = 20, Zone = 69 } ),
                    new KeyValuePair<int, Tendency>(3, new Tendency { PA = 33, RPO = 67, Shotgun = 73, Screen = 0, Gap = 25, Zone = 75 } ),
                    new KeyValuePair<int, Tendency>(22, new Tendency { PA = 0, RPO = 0, Shotgun = 100, Screen = 9, Gap = 0, Zone = 0 } ),
                    new KeyValuePair<int, Tendency>(13, new Tendency { PA = 17, RPO = 9, Shotgun = 60, Screen = 11, Gap = 33, Zone = 65 } ),

                }
            },
            {"texans", new List<KeyValuePair<int, Tendency>>
                {
                    new KeyValuePair<int, Tendency>(100, new Tendency { PA = 16, RPO = 4, Shotgun = 61, Screen = 9, Gap = 28, Zone = 69 } ),
                    new KeyValuePair<int, Tendency>(35, new Tendency { PA = 19, RPO = 5, Shotgun = 51, Screen = 10, Gap = 31, Zone = 67 } ),
                    new KeyValuePair<int, Tendency>(20, new Tendency { PA = 22, RPO = 6, Shotgun = 47, Screen = 7, Gap = 18, Zone = 82 } ),
                    new KeyValuePair<int, Tendency>(17, new Tendency { PA = 23, RPO = 6, Shotgun = 60, Screen = 9, Gap = 47, Zone = 53 } ),
                    new KeyValuePair<int, Tendency>(6, new Tendency { PA = 19, RPO = 4, Shotgun = 67, Screen = 12, Gap = 23, Zone = 70 } ),
                    new KeyValuePair<int, Tendency>(1, new Tendency { PA = 12, RPO = 0, Shotgun = 58, Screen = 0, Gap = 0, Zone = 100 } ),
                    new KeyValuePair<int, Tendency>(7, new Tendency { PA = 0, RPO = 0, Shotgun = 100, Screen = 0, Gap = 0, Zone = 0 } ),
                    new KeyValuePair<int, Tendency>(8, new Tendency { PA = 0, RPO = 0, Shotgun = 100, Screen = 0, Gap = 0, Zone = 0 } ),
                    new KeyValuePair<int, Tendency>(40, new Tendency { PA = 3, RPO = 0, Shotgun = 100, Screen = 30, Gap = 50, Zone = 0 } ),
                    new KeyValuePair<int, Tendency>(25, new Tendency { PA = 18, RPO = 4, Shotgun = 56, Screen = 9, Gap = 31, Zone = 66 } ),
                    new KeyValuePair<int, Tendency>(26, new Tendency { PA = 19, RPO = 4, Shotgun = 41, Screen = 6, Gap = 26, Zone = 74 } ),
                    new KeyValuePair<int, Tendency>(27, new Tendency { PA = 19, RPO = 4, Shotgun = 41, Screen = 6, Gap = 26, Zone = 74 } ),
                    new KeyValuePair<int, Tendency>(28, new Tendency { PA = 19, RPO = 4, Shotgun = 41, Screen = 6, Gap = 26, Zone = 74 } ),
                    new KeyValuePair<int, Tendency>(3, new Tendency { PA = 25, RPO = 4, Shotgun = 35, Screen = 0, Gap = 19, Zone = 81 } ),
                    new KeyValuePair<int, Tendency>(22, new Tendency { PA = 0, RPO = 8, Shotgun = 100, Screen = 9, Gap = 0, Zone = 0 } ),
                    new KeyValuePair<int, Tendency>(13, new Tendency { PA = 15, RPO = 2, Shotgun = 35, Screen = 8, Gap = 30, Zone = 68 } ),

                }
            },
            {"colts", new List<KeyValuePair<int, Tendency>>
                {
                    new KeyValuePair<int, Tendency>(100, new Tendency { PA = 20, RPO = 28, Shotgun = 93, Screen = 9, Gap = 23, Zone = 77 } ),
                    new KeyValuePair<int, Tendency>(35, new Tendency { PA = 25, RPO = 37, Shotgun = 90, Screen = 9, Gap = 27, Zone = 72 } ),
                    new KeyValuePair<int, Tendency>(20, new Tendency { PA = 21, RPO = 38, Shotgun = 100, Screen = 18, Gap = 9, Zone = 91 } ),
                    new KeyValuePair<int, Tendency>(17, new Tendency { PA = 28, RPO = 40, Shotgun = 91, Screen = 18, Gap = 20, Zone = 80 } ),
                    new KeyValuePair<int, Tendency>(6, new Tendency { PA = 18, RPO = 17, Shotgun = 97, Screen = 5, Gap = 16, Zone = 84 } ),
                    new KeyValuePair<int, Tendency>(1, new Tendency { PA = 14, RPO = 26, Shotgun = 92, Screen = 6, Gap = 6, Zone = 94 } ),
                    new KeyValuePair<int, Tendency>(7, new Tendency { PA = 8, RPO = 11, Shotgun = 100, Screen = 6, Gap = 25, Zone = 75 } ),
                    new KeyValuePair<int, Tendency>(8, new Tendency { PA = 0, RPO = 0, Shotgun = 100, Screen = 3, Gap = 0, Zone = 0 } ),
                    new KeyValuePair<int, Tendency>(40, new Tendency { PA = 4, RPO = 8, Shotgun = 100, Screen = 29, Gap = 33, Zone = 33 } ),
                    new KeyValuePair<int, Tendency>(25, new Tendency { PA = 22, RPO = 28, Shotgun = 92, Screen = 8, Gap = 21, Zone = 79 } ),
                    new KeyValuePair<int, Tendency>(26, new Tendency { PA = 23, RPO = 52, Shotgun = 97, Screen = 16, Gap = 32, Zone = 68 } ),
                    new KeyValuePair<int, Tendency>(27, new Tendency { PA = 23, RPO = 52, Shotgun = 97, Screen = 16, Gap = 32, Zone = 68 } ),
                    new KeyValuePair<int, Tendency>(28, new Tendency { PA = 23, RPO = 52, Shotgun = 97, Screen = 16, Gap = 32, Zone = 68 } ),
                    new KeyValuePair<int, Tendency>(3, new Tendency { PA = 17, RPO = 39, Shotgun = 89, Screen = 0, Gap = 21, Zone = 79 } ),
                    new KeyValuePair<int, Tendency>(22, new Tendency { PA = 0, RPO = 0, Shotgun = 100, Screen = 20, Gap = 0, Zone = 0 } ),
                    new KeyValuePair<int, Tendency>(13, new Tendency { PA = 19, RPO = 14, Shotgun = 89, Screen = 9, Gap = 23, Zone = 76 } ),

                }
            },
            {"jaguars", new List<KeyValuePair<int, Tendency>>
                {
                    new KeyValuePair<int, Tendency>(100, new Tendency { PA = 18, RPO = 11, Shotgun = 79, Screen = 14, Gap = 49, Zone = 50 } ),
                    new KeyValuePair<int, Tendency>(35, new Tendency { PA = 27, RPO = 15, Shotgun = 75, Screen = 20, Gap = 48, Zone = 50 } ),
                    new KeyValuePair<int, Tendency>(20, new Tendency { PA = 20, RPO = 18, Shotgun = 76, Screen = 21, Gap = 52, Zone = 44 } ),
                    new KeyValuePair<int, Tendency>(17, new Tendency { PA = 20, RPO = 14, Shotgun = 82, Screen = 18, Gap = 56, Zone = 44 } ),
                    new KeyValuePair<int, Tendency>(6, new Tendency { PA = 14, RPO = 4, Shotgun = 96, Screen = 12, Gap = 47, Zone = 53 } ),
                    new KeyValuePair<int, Tendency>(1, new Tendency { PA = 8, RPO = 13, Shotgun = 76, Screen = 10, Gap = 53, Zone = 41 } ),
                    new KeyValuePair<int, Tendency>(7, new Tendency { PA = 3, RPO = 17, Shotgun = 100, Screen = 0, Gap = 40, Zone = 60 } ),
                    new KeyValuePair<int, Tendency>(8, new Tendency { PA = 0, RPO = 3, Shotgun = 100, Screen = 3, Gap = 0, Zone = 100 } ),
                    new KeyValuePair<int, Tendency>(40, new Tendency { PA = 0, RPO = 5, Shotgun = 100, Screen = 17, Gap = 0, Zone = 100 } ),
                    new KeyValuePair<int, Tendency>(25, new Tendency { PA = 21, RPO = 11, Shotgun = 81, Screen = 16, Gap = 48, Zone = 51 } ),
                    new KeyValuePair<int, Tendency>(26, new Tendency { PA = 18, RPO = 12, Shotgun = 69, Screen = 12, Gap = 39, Zone = 59 } ),
                    new KeyValuePair<int, Tendency>(27, new Tendency { PA = 18, RPO = 12, Shotgun = 69, Screen = 12, Gap = 39, Zone = 59 } ),
                    new KeyValuePair<int, Tendency>(28, new Tendency { PA = 18, RPO = 12, Shotgun = 69, Screen = 12, Gap = 39, Zone = 59 } ),
                    new KeyValuePair<int, Tendency>(3, new Tendency { PA = 6, RPO = 13, Shotgun = 75, Screen = 0, Gap = 0, Zone = 100 } ),
                    new KeyValuePair<int, Tendency>(22, new Tendency { PA = 0, RPO = 0, Shotgun = 100, Screen = 0, Gap = 0, Zone = 0 } ),
                    new KeyValuePair<int, Tendency>(13, new Tendency { PA = 17, RPO = 6, Shotgun = 69, Screen = 14, Gap = 46, Zone = 52 } ),

                }
            },
            {"chiefs", new List<KeyValuePair<int, Tendency>>
                {
                    new KeyValuePair<int, Tendency>(100, new Tendency { PA = 17, RPO = 18, Shotgun = 83, Screen = 12, Gap = 31, Zone = 67 } ),
                    new KeyValuePair<int, Tendency>(35, new Tendency { PA = 25, RPO = 23, Shotgun = 79, Screen = 10, Gap = 34, Zone = 66 } ),
                    new KeyValuePair<int, Tendency>(20, new Tendency { PA = 15, RPO = 41, Shotgun = 70, Screen = 21, Gap = 16, Zone = 84 } ),
                    new KeyValuePair<int, Tendency>(17, new Tendency { PA = 20, RPO = 22, Shotgun = 89, Screen = 26, Gap = 33, Zone = 67 } ),
                    new KeyValuePair<int, Tendency>(6, new Tendency { PA = 13, RPO = 10, Shotgun = 92, Screen = 16, Gap = 50, Zone = 45 } ),
                    new KeyValuePair<int, Tendency>(1, new Tendency { PA = 6, RPO = 18, Shotgun = 85, Screen = 0, Gap = 13, Zone = 87 } ),
                    new KeyValuePair<int, Tendency>(7, new Tendency { PA = 0, RPO = 0, Shotgun = 100, Screen = 5, Gap = 0, Zone = 0 } ),
                    new KeyValuePair<int, Tendency>(8, new Tendency { PA = 0, RPO = 0, Shotgun = 100, Screen = 4, Gap = 0, Zone = 0 } ),
                    new KeyValuePair<int, Tendency>(40, new Tendency { PA = 0, RPO = 0, Shotgun = 100, Screen = 11, Gap = 0, Zone = 0 } ),
                    new KeyValuePair<int, Tendency>(25, new Tendency { PA = 20, RPO = 21, Shotgun = 83, Screen = 14, Gap = 30, Zone = 70 } ),
                    new KeyValuePair<int, Tendency>(26, new Tendency { PA = 15, RPO = 21, Shotgun = 84, Screen = 18, Gap = 39, Zone = 58 } ),
                    new KeyValuePair<int, Tendency>(27, new Tendency { PA = 15, RPO = 21, Shotgun = 84, Screen = 18, Gap = 39, Zone = 58 } ),
                    new KeyValuePair<int, Tendency>(28, new Tendency { PA = 15, RPO = 21, Shotgun = 84, Screen = 18, Gap = 39, Zone = 58 } ),
                    new KeyValuePair<int, Tendency>(3, new Tendency { PA = 32, RPO = 32, Shotgun = 80, Screen = 0, Gap = 45, Zone = 55 } ),
                    new KeyValuePair<int, Tendency>(22, new Tendency { PA = 0, RPO = 50, Shotgun = 100, Screen = 0, Gap = 0, Zone = 100 } ),
                    new KeyValuePair<int, Tendency>(13, new Tendency { PA = 16, RPO = 9, Shotgun = 70, Screen = 12, Gap = 31, Zone = 68 } ),

                }
            },
            {"raiders", new List<KeyValuePair<int, Tendency>>
                {
                    new KeyValuePair<int, Tendency>(100, new Tendency { PA = 15, RPO = 4, Shotgun = 57, Screen = 9, Gap = 29, Zone = 68 } ),
                    new KeyValuePair<int, Tendency>(35, new Tendency { PA = 19, RPO = 5, Shotgun = 42, Screen = 7, Gap = 25, Zone = 71 } ),
                    new KeyValuePair<int, Tendency>(20, new Tendency { PA = 12, RPO = 4, Shotgun = 20, Screen = 0, Gap = 37, Zone = 63 } ),
                    new KeyValuePair<int, Tendency>(17, new Tendency { PA = 17, RPO = 6, Shotgun = 40, Screen = 23, Gap = 54, Zone = 46 } ),
                    new KeyValuePair<int, Tendency>(6, new Tendency { PA = 17, RPO = 6, Shotgun = 65, Screen = 10, Gap = 33, Zone = 57 } ),
                    new KeyValuePair<int, Tendency>(1, new Tendency { PA = 5, RPO = 3, Shotgun = 59, Screen = 4, Gap = 20, Zone = 80 } ),
                    new KeyValuePair<int, Tendency>(7, new Tendency { PA = 8, RPO = 3, Shotgun = 100, Screen = 8, Gap = 0, Zone = 67 } ),
                    new KeyValuePair<int, Tendency>(8, new Tendency { PA = 0, RPO = 0, Shotgun = 100, Screen = 5, Gap = 0, Zone = 100 } ),
                    new KeyValuePair<int, Tendency>(40, new Tendency { PA = 0, RPO = 0, Shotgun = 100, Screen = 6, Gap = 0, Zone = 0 } ),
                    new KeyValuePair<int, Tendency>(25, new Tendency { PA = 18, RPO = 4, Shotgun = 47, Screen = 9, Gap = 30, Zone = 66 } ),
                    new KeyValuePair<int, Tendency>(26, new Tendency { PA = 9, RPO = 9, Shotgun = 44, Screen = 19, Gap = 37, Zone = 63 } ),
                    new KeyValuePair<int, Tendency>(27, new Tendency { PA = 9, RPO = 9, Shotgun = 44, Screen = 19, Gap = 37, Zone = 63 } ),
                    new KeyValuePair<int, Tendency>(28, new Tendency { PA = 9, RPO = 9, Shotgun = 44, Screen = 19, Gap = 37, Zone = 63 } ),
                    new KeyValuePair<int, Tendency>(3, new Tendency { PA = 0, RPO = 0, Shotgun = 40, Screen = 0, Gap = 38, Zone = 63 } ),
                    new KeyValuePair<int, Tendency>(22, new Tendency { PA = 9, RPO = 0, Shotgun = 100, Screen = 0, Gap = 0, Zone = 100 } ),
                    new KeyValuePair<int, Tendency>(13, new Tendency { PA = 14, RPO = 2, Shotgun = 20, Screen = 9, Gap = 30, Zone = 67 } ),

                }
            },
            {"chargers", new List<KeyValuePair<int, Tendency>>
                {
                    new KeyValuePair<int, Tendency>(100, new Tendency { PA = 18, RPO = 16, Shotgun = 81, Screen = 11, Gap = 33, Zone = 65 } ),
                    new KeyValuePair<int, Tendency>(35, new Tendency { PA = 23, RPO = 21, Shotgun = 74, Screen = 18, Gap = 34, Zone = 65 } ),
                    new KeyValuePair<int, Tendency>(20, new Tendency { PA = 20, RPO = 38, Shotgun = 68, Screen = 15, Gap = 28, Zone = 72 } ),
                    new KeyValuePair<int, Tendency>(17, new Tendency { PA = 25, RPO = 23, Shotgun = 79, Screen = 12, Gap = 18, Zone = 73 } ),
                    new KeyValuePair<int, Tendency>(6, new Tendency { PA = 19, RPO = 7, Shotgun = 88, Screen = 7, Gap = 30, Zone = 70 } ),
                    new KeyValuePair<int, Tendency>(1, new Tendency { PA = 8, RPO = 18, Shotgun = 74, Screen = 10, Gap = 37, Zone = 63 } ),
                    new KeyValuePair<int, Tendency>(7, new Tendency { PA = 0, RPO = 3, Shotgun = 100, Screen = 0, Gap = 50, Zone = 50 } ),
                    new KeyValuePair<int, Tendency>(8, new Tendency { PA = 5, RPO = 0, Shotgun = 100, Screen = 0, Gap = 0, Zone = 0 } ),
                    new KeyValuePair<int, Tendency>(40, new Tendency { PA = 0, RPO = 0, Shotgun = 100, Screen = 26, Gap = 0, Zone = 0 } ),
                    new KeyValuePair<int, Tendency>(25, new Tendency { PA = 22, RPO = 16, Shotgun = 78, Screen = 13, Gap = 32, Zone = 67 } ),
                    new KeyValuePair<int, Tendency>(26, new Tendency { PA = 15, RPO = 29, Shotgun = 84, Screen = 16, Gap = 36, Zone = 62 } ),
                    new KeyValuePair<int, Tendency>(27, new Tendency { PA = 15, RPO = 29, Shotgun = 84, Screen = 16, Gap = 36, Zone = 62 } ),
                    new KeyValuePair<int, Tendency>(28, new Tendency { PA = 15, RPO = 29, Shotgun = 84, Screen = 16, Gap = 36, Zone = 62 } ),
                    new KeyValuePair<int, Tendency>(3, new Tendency { PA = 20, RPO = 35, Shotgun = 71, Screen = 0, Gap = 18, Zone = 82 } ),
                    new KeyValuePair<int, Tendency>(22, new Tendency { PA = 0, RPO = 0, Shotgun = 100, Screen = 0, Gap = 0, Zone = 0 } ),
                    new KeyValuePair<int, Tendency>(13, new Tendency { PA = 18, RPO = 8, Shotgun = 68, Screen = 10, Gap = 35, Zone = 64 } ),

                }
            },
            {"rams", new List<KeyValuePair<int, Tendency>>
                {
                    new KeyValuePair<int, Tendency>(100, new Tendency { PA = 15, RPO = 2, Shotgun = 62, Screen = 9, Gap = 47, Zone = 52 } ),
                    new KeyValuePair<int, Tendency>(35, new Tendency { PA = 23, RPO = 3, Shotgun = 47, Screen = 8, Gap = 43, Zone = 57 } ),
                    new KeyValuePair<int, Tendency>(20, new Tendency { PA = 6, RPO = 0, Shotgun = 46, Screen = 18, Gap = 52, Zone = 48 } ),
                    new KeyValuePair<int, Tendency>(17, new Tendency { PA = 28, RPO = 2, Shotgun = 46, Screen = 12, Gap = 52, Zone = 48 } ),
                    new KeyValuePair<int, Tendency>(6, new Tendency { PA = 7, RPO = 4, Shotgun = 86, Screen = 9, Gap = 48, Zone = 52 } ),
                    new KeyValuePair<int, Tendency>(1, new Tendency { PA = 0, RPO = 0, Shotgun = 55, Screen = 6, Gap = 57, Zone = 43 } ),
                    new KeyValuePair<int, Tendency>(7, new Tendency { PA = 0, RPO = 4, Shotgun = 100, Screen = 0, Gap = 0, Zone = 100 } ),
                    new KeyValuePair<int, Tendency>(8, new Tendency { PA = 0, RPO = 0, Shotgun = 100, Screen = 16, Gap = 0, Zone = 0 } ),
                    new KeyValuePair<int, Tendency>(40, new Tendency { PA = 0, RPO = 0, Shotgun = 100, Screen = 10, Gap = 71, Zone = 29 } ),
                    new KeyValuePair<int, Tendency>(25, new Tendency { PA = 18, RPO = 3, Shotgun = 57, Screen = 9, Gap = 46, Zone = 54 } ),
                    new KeyValuePair<int, Tendency>(26, new Tendency { PA = 10, RPO = 3, Shotgun = 51, Screen = 18, Gap = 52, Zone = 48 } ),
                    new KeyValuePair<int, Tendency>(27, new Tendency { PA = 10, RPO = 3, Shotgun = 51, Screen = 18, Gap = 52, Zone = 48 } ),
                    new KeyValuePair<int, Tendency>(28, new Tendency { PA = 10, RPO = 3, Shotgun = 51, Screen = 18, Gap = 52, Zone = 48 } ),
                    new KeyValuePair<int, Tendency>(3, new Tendency { PA = 6, RPO = 0, Shotgun = 41, Screen = 0, Gap = 46, Zone = 54 } ),
                    new KeyValuePair<int, Tendency>(22, new Tendency { PA = 0, RPO = 0, Shotgun = 91, Screen = 0, Gap = 100, Zone = 0 } ),
                    new KeyValuePair<int, Tendency>(13, new Tendency { PA = 14, RPO = 1, Shotgun = 41, Screen = 9, Gap = 47, Zone = 52 } ),

                }
            },
            {"dolphins", new List<KeyValuePair<int, Tendency>>
                {
                    new KeyValuePair<int, Tendency>(100, new Tendency { PA = 23, RPO = 7, Shotgun = 78, Screen = 12, Gap = 27, Zone = 72 } ),
                    new KeyValuePair<int, Tendency>(35, new Tendency { PA = 25, RPO = 7, Shotgun = 76, Screen = 13, Gap = 23, Zone = 77 } ),
                    new KeyValuePair<int, Tendency>(20, new Tendency { PA = 26, RPO = 18, Shotgun = 84, Screen = 5, Gap = 28, Zone = 72 } ),
                    new KeyValuePair<int, Tendency>(17, new Tendency { PA = 41, RPO = 13, Shotgun = 74, Screen = 4, Gap = 20, Zone = 80 } ),
                    new KeyValuePair<int, Tendency>(6, new Tendency { PA = 24, RPO = 4, Shotgun = 87, Screen = 12, Gap = 41, Zone = 52 } ),
                    new KeyValuePair<int, Tendency>(1, new Tendency { PA = 20, RPO = 7, Shotgun = 89, Screen = 9, Gap = 43, Zone = 57 } ),
                    new KeyValuePair<int, Tendency>(7, new Tendency { PA = 0, RPO = 0, Shotgun = 100, Screen = 0, Gap = 0, Zone = 0 } ),
                    new KeyValuePair<int, Tendency>(8, new Tendency { PA = 0, RPO = 0, Shotgun = 100, Screen = 4, Gap = 100, Zone = 0 } ),
                    new KeyValuePair<int, Tendency>(40, new Tendency { PA = 0, RPO = 0, Shotgun = 100, Screen = 36, Gap = 0, Zone = 0 } ),
                    new KeyValuePair<int, Tendency>(25, new Tendency { PA = 26, RPO = 7, Shotgun = 79, Screen = 11, Gap = 21, Zone = 78 } ),
                    new KeyValuePair<int, Tendency>(26, new Tendency { PA = 20, RPO = 8, Shotgun = 70, Screen = 17, Gap = 35, Zone = 65 } ),
                    new KeyValuePair<int, Tendency>(27, new Tendency { PA = 20, RPO = 8, Shotgun = 70, Screen = 17, Gap = 35, Zone = 65 } ),
                    new KeyValuePair<int, Tendency>(28, new Tendency { PA = 20, RPO = 8, Shotgun = 70, Screen = 17, Gap = 35, Zone = 65 } ),
                    new KeyValuePair<int, Tendency>(3, new Tendency { PA = 18, RPO = 9, Shotgun = 62, Screen = 0, Gap = 33, Zone = 67 } ),
                    new KeyValuePair<int, Tendency>(22, new Tendency { PA = 0, RPO = 0, Shotgun = 100, Screen = 0, Gap = 0, Zone = 0 } ),
                    new KeyValuePair<int, Tendency>(13, new Tendency { PA = 22, RPO = 4, Shotgun = 62, Screen = 13, Gap = 25, Zone = 74 } ),

                }
            },
            {"vikings", new List<KeyValuePair<int, Tendency>>
                {
                    new KeyValuePair<int, Tendency>(100, new Tendency { PA = 21, RPO = 8, Shotgun = 54, Screen = 8, Gap = 24, Zone = 75 } ),
                    new KeyValuePair<int, Tendency>(35, new Tendency { PA = 31, RPO = 9, Shotgun = 31, Screen = 11, Gap = 24, Zone = 75 } ),
                    new KeyValuePair<int, Tendency>(20, new Tendency { PA = 21, RPO = 6, Shotgun = 24, Screen = 4, Gap = 33, Zone = 63 } ),
                    new KeyValuePair<int, Tendency>(17, new Tendency { PA = 23, RPO = 14, Shotgun = 52, Screen = 5, Gap = 32, Zone = 68 } ),
                    new KeyValuePair<int, Tendency>(6, new Tendency { PA = 19, RPO = 10, Shotgun = 73, Screen = 10, Gap = 13, Zone = 83 } ),
                    new KeyValuePair<int, Tendency>(1, new Tendency { PA = 3, RPO = 0, Shotgun = 75, Screen = 0, Gap = 30, Zone = 70 } ),
                    new KeyValuePair<int, Tendency>(7, new Tendency { PA = 0, RPO = 0, Shotgun = 100, Screen = 0, Gap = 0, Zone = 0 } ),
                    new KeyValuePair<int, Tendency>(8, new Tendency { PA = 0, RPO = 0, Shotgun = 100, Screen = 6, Gap = 0, Zone = 100 } ),
                    new KeyValuePair<int, Tendency>(40, new Tendency { PA = 0, RPO = 4, Shotgun = 100, Screen = 13, Gap = 0, Zone = 0 } ),
                    new KeyValuePair<int, Tendency>(25, new Tendency { PA = 25, RPO = 10, Shotgun = 44, Screen = 10, Gap = 24, Zone = 75 } ),
                    new KeyValuePair<int, Tendency>(26, new Tendency { PA = 29, RPO = 7, Shotgun = 41, Screen = 16, Gap = 21, Zone = 76 } ),
                    new KeyValuePair<int, Tendency>(27, new Tendency { PA = 29, RPO = 7, Shotgun = 41, Screen = 16, Gap = 21, Zone = 76 } ),
                    new KeyValuePair<int, Tendency>(28, new Tendency { PA = 29, RPO = 7, Shotgun = 41, Screen = 16, Gap = 21, Zone = 76 } ),
                    new KeyValuePair<int, Tendency>(3, new Tendency { PA = 24, RPO = 10, Shotgun = 33, Screen = 0, Gap = 36, Zone = 64 } ),
                    new KeyValuePair<int, Tendency>(22, new Tendency { PA = 0, RPO = 0, Shotgun = 100, Screen = 0, Gap = 0, Zone = 0 } ),
                    new KeyValuePair<int, Tendency>(13, new Tendency { PA = 20, RPO = 4, Shotgun = 24, Screen = 8, Gap = 23, Zone = 75 } ),

                }
            },
            {"patriots", new List<KeyValuePair<int, Tendency>>
                {
                    new KeyValuePair<int, Tendency>(100, new Tendency { PA = 14, RPO = 13, Shotgun = 65, Screen = 14, Gap = 40, Zone = 56 } ),
                    new KeyValuePair<int, Tendency>(35, new Tendency { PA = 20, RPO = 19, Shotgun = 58, Screen = 18, Gap = 44, Zone = 53 } ),
                    new KeyValuePair<int, Tendency>(20, new Tendency { PA = 5, RPO = 20, Shotgun = 46, Screen = 7, Gap = 45, Zone = 55 } ),
                    new KeyValuePair<int, Tendency>(17, new Tendency { PA = 21, RPO = 8, Shotgun = 48, Screen = 7, Gap = 35, Zone = 55 } ),
                    new KeyValuePair<int, Tendency>(6, new Tendency { PA = 15, RPO = 9, Shotgun = 68, Screen = 18, Gap = 29, Zone = 60 } ),
                    new KeyValuePair<int, Tendency>(1, new Tendency { PA = 4, RPO = 4, Shotgun = 54, Screen = 0, Gap = 40, Zone = 60 } ),
                    new KeyValuePair<int, Tendency>(7, new Tendency { PA = 0, RPO = 0, Shotgun = 100, Screen = 6, Gap = 0, Zone = 0 } ),
                    new KeyValuePair<int, Tendency>(8, new Tendency { PA = 0, RPO = 0, Shotgun = 100, Screen = 8, Gap = 100, Zone = 0 } ),
                    new KeyValuePair<int, Tendency>(40, new Tendency { PA = 3, RPO = 0, Shotgun = 100, Screen = 24, Gap = 33, Zone = 33 } ),
                    new KeyValuePair<int, Tendency>(25, new Tendency { PA = 17, RPO = 15, Shotgun = 58, Screen = 17, Gap = 41, Zone = 54 } ),
                    new KeyValuePair<int, Tendency>(26, new Tendency { PA = 13, RPO = 20, Shotgun = 65, Screen = 0, Gap = 40, Zone = 60 } ),
                    new KeyValuePair<int, Tendency>(27, new Tendency { PA = 13, RPO = 20, Shotgun = 65, Screen = 0, Gap = 40, Zone = 60 } ),
                    new KeyValuePair<int, Tendency>(28, new Tendency { PA = 13, RPO = 20, Shotgun = 65, Screen = 0, Gap = 40, Zone = 60 } ),
                    new KeyValuePair<int, Tendency>(3, new Tendency { PA = 20, RPO = 20, Shotgun = 67, Screen = 0, Gap = 56, Zone = 44 } ),
                    new KeyValuePair<int, Tendency>(22, new Tendency { PA = 0, RPO = 20, Shotgun = 100, Screen = 0, Gap = 50, Zone = 50 } ),
                    new KeyValuePair<int, Tendency>(13, new Tendency { PA = 14, RPO = 6, Shotgun = 46, Screen = 13, Gap = 40, Zone = 56 } ),

                }
            },
            {"saints", new List<KeyValuePair<int, Tendency>>
                {
                    new KeyValuePair<int, Tendency>(100, new Tendency { PA = 10, RPO = 7, Shotgun = 65, Screen = 8, Gap = 30, Zone = 67 } ),
                    new KeyValuePair<int, Tendency>(35, new Tendency { PA = 14, RPO = 8, Shotgun = 51, Screen = 8, Gap = 33, Zone = 63 } ),
                    new KeyValuePair<int, Tendency>(20, new Tendency { PA = 6, RPO = 8, Shotgun = 47, Screen = 31, Gap = 32, Zone = 68 } ),
                    new KeyValuePair<int, Tendency>(17, new Tendency { PA = 9, RPO = 12, Shotgun = 52, Screen = 7, Gap = 20, Zone = 80 } ),
                    new KeyValuePair<int, Tendency>(6, new Tendency { PA = 12, RPO = 8, Shotgun = 78, Screen = 9, Gap = 21, Zone = 71 } ),
                    new KeyValuePair<int, Tendency>(1, new Tendency { PA = 8, RPO = 10, Shotgun = 76, Screen = 8, Gap = 21, Zone = 79 } ),
                    new KeyValuePair<int, Tendency>(7, new Tendency { PA = 0, RPO = 0, Shotgun = 100, Screen = 9, Gap = 100, Zone = 0 } ),
                    new KeyValuePair<int, Tendency>(8, new Tendency { PA = 0, RPO = 0, Shotgun = 100, Screen = 7, Gap = 100, Zone = 0 } ),
                    new KeyValuePair<int, Tendency>(40, new Tendency { PA = 0, RPO = 0, Shotgun = 100, Screen = 0, Gap = 0, Zone = 0 } ),
                    new KeyValuePair<int, Tendency>(25, new Tendency { PA = 12, RPO = 8, Shotgun = 58, Screen = 9, Gap = 29, Zone = 68 } ),
                    new KeyValuePair<int, Tendency>(26, new Tendency { PA = 11, RPO = 9, Shotgun = 62, Screen = 10, Gap = 26, Zone = 69 } ),
                    new KeyValuePair<int, Tendency>(27, new Tendency { PA = 11, RPO = 9, Shotgun = 62, Screen = 10, Gap = 26, Zone = 69 } ),
                    new KeyValuePair<int, Tendency>(28, new Tendency { PA = 11, RPO = 9, Shotgun = 62, Screen = 10, Gap = 26, Zone = 69 } ),
                    new KeyValuePair<int, Tendency>(3, new Tendency { PA = 6, RPO = 6, Shotgun = 48, Screen = 0, Gap = 33, Zone = 67 } ),
                    new KeyValuePair<int, Tendency>(22, new Tendency { PA = 0, RPO = 0, Shotgun = 88, Screen = 0, Gap = 50, Zone = 50 } ),
                    new KeyValuePair<int, Tendency>(13, new Tendency { PA = 10, RPO = 4, Shotgun = 47, Screen = 8, Gap = 31, Zone = 65 } ),

                }
            },
            {"giants", new List<KeyValuePair<int, Tendency>>
                {
                    new KeyValuePair<int, Tendency>(100, new Tendency { PA = 20, RPO = 19, Shotgun = 71, Screen = 6, Gap = 36, Zone = 62 } ),
                    new KeyValuePair<int, Tendency>(35, new Tendency { PA = 33, RPO = 22, Shotgun = 60, Screen = 6, Gap = 37, Zone = 61 } ),
                    new KeyValuePair<int, Tendency>(20, new Tendency { PA = 7, RPO = 26, Shotgun = 63, Screen = 0, Gap = 35, Zone = 60 } ),
                    new KeyValuePair<int, Tendency>(17, new Tendency { PA = 14, RPO = 41, Shotgun = 64, Screen = 8, Gap = 46, Zone = 50 } ),
                    new KeyValuePair<int, Tendency>(6, new Tendency { PA = 15, RPO = 19, Shotgun = 70, Screen = 4, Gap = 44, Zone = 56 } ),
                    new KeyValuePair<int, Tendency>(1, new Tendency { PA = 5, RPO = 16, Shotgun = 76, Screen = 0, Gap = 18, Zone = 82 } ),
                    new KeyValuePair<int, Tendency>(7, new Tendency { PA = 0, RPO = 4, Shotgun = 100, Screen = 10, Gap = 0, Zone = 100 } ),
                    new KeyValuePair<int, Tendency>(8, new Tendency { PA = 8, RPO = 5, Shotgun = 100, Screen = 3, Gap = 33, Zone = 67 } ),
                    new KeyValuePair<int, Tendency>(40, new Tendency { PA = 8, RPO = 5, Shotgun = 100, Screen = 18, Gap = 33, Zone = 67 } ),
                    new KeyValuePair<int, Tendency>(25, new Tendency { PA = 21, RPO = 20, Shotgun = 64, Screen = 7, Gap = 37, Zone = 59 } ),
                    new KeyValuePair<int, Tendency>(26, new Tendency { PA = 32, RPO = 28, Shotgun = 51, Screen = 4, Gap = 33, Zone = 64 } ),
                    new KeyValuePair<int, Tendency>(27, new Tendency { PA = 32, RPO = 28, Shotgun = 51, Screen = 4, Gap = 33, Zone = 64 } ),
                    new KeyValuePair<int, Tendency>(28, new Tendency { PA = 32, RPO = 28, Shotgun = 51, Screen = 4, Gap = 33, Zone = 64 } ),
                    new KeyValuePair<int, Tendency>(3, new Tendency { PA = 22, RPO = 11, Shotgun = 40, Screen = 0, Gap = 57, Zone = 43 } ),
                    new KeyValuePair<int, Tendency>(22, new Tendency { PA = 0, RPO = 40, Shotgun = 100, Screen = 0, Gap = 50, Zone = 50 } ),
                    new KeyValuePair<int, Tendency>(13, new Tendency { PA = 18, RPO = 9, Shotgun = 40, Screen = 7, Gap = 36, Zone = 61 } ),

                }
            },
            {"jets", new List<KeyValuePair<int, Tendency>>
                {
                    new KeyValuePair<int, Tendency>(100, new Tendency { PA = 18, RPO = 9, Shotgun = 64, Screen = 10, Gap = 32, Zone = 65 } ),
                    new KeyValuePair<int, Tendency>(35, new Tendency { PA = 24, RPO = 11, Shotgun = 43, Screen = 15, Gap = 35, Zone = 61 } ),
                    new KeyValuePair<int, Tendency>(20, new Tendency { PA = 28, RPO = 10, Shotgun = 50, Screen = 14, Gap = 20, Zone = 80 } ),
                    new KeyValuePair<int, Tendency>(17, new Tendency { PA = 18, RPO = 8, Shotgun = 44, Screen = 8, Gap = 42, Zone = 58 } ),
                    new KeyValuePair<int, Tendency>(6, new Tendency { PA = 17, RPO = 7, Shotgun = 66, Screen = 8, Gap = 21, Zone = 74 } ),
                    new KeyValuePair<int, Tendency>(1, new Tendency { PA = 15, RPO = 15, Shotgun = 83, Screen = 13, Gap = 20, Zone = 70 } ),
                    new KeyValuePair<int, Tendency>(7, new Tendency { PA = 6, RPO = 3, Shotgun = 100, Screen = 3, Gap = 0, Zone = 0 } ),
                    new KeyValuePair<int, Tendency>(8, new Tendency { PA = 0, RPO = 0, Shotgun = 100, Screen = 0, Gap = 0, Zone = 0 } ),
                    new KeyValuePair<int, Tendency>(40, new Tendency { PA = 3, RPO = 8, Shotgun = 100, Screen = 16, Gap = 0, Zone = 100 } ),
                    new KeyValuePair<int, Tendency>(25, new Tendency { PA = 20, RPO = 8, Shotgun = 50, Screen = 11, Gap = 32, Zone = 65 } ),
                    new KeyValuePair<int, Tendency>(26, new Tendency { PA = 16, RPO = 10, Shotgun = 38, Screen = 8, Gap = 42, Zone = 58 } ),
                    new KeyValuePair<int, Tendency>(27, new Tendency { PA = 16, RPO = 10, Shotgun = 38, Screen = 8, Gap = 42, Zone = 58 } ),
                    new KeyValuePair<int, Tendency>(28, new Tendency { PA = 16, RPO = 10, Shotgun = 38, Screen = 8, Gap = 42, Zone = 58 } ),
                    new KeyValuePair<int, Tendency>(3, new Tendency { PA = 17, RPO = 17, Shotgun = 33, Screen = 0, Gap = 0, Zone = 100 } ),
                    new KeyValuePair<int, Tendency>(22, new Tendency { PA = 0, RPO = 0, Shotgun = 100, Screen = 0, Gap = 0, Zone = 0 } ),
                    new KeyValuePair<int, Tendency>(13, new Tendency { PA = 16, RPO = 4, Shotgun = 33, Screen = 9, Gap = 32, Zone = 65 } ),

                }
            },
            {"eagles", new List<KeyValuePair<int, Tendency>>
                {
                    new KeyValuePair<int, Tendency>(100, new Tendency { PA = 18, RPO = 18, Shotgun = 93, Screen = 10, Gap = 30, Zone = 66 } ),
                    new KeyValuePair<int, Tendency>(35, new Tendency { PA = 22, RPO = 20, Shotgun = 95, Screen = 10, Gap = 31, Zone = 67 } ),
                    new KeyValuePair<int, Tendency>(20, new Tendency { PA = 17, RPO = 21, Shotgun = 95, Screen = 10, Gap = 21, Zone = 79 } ),
                    new KeyValuePair<int, Tendency>(17, new Tendency { PA = 17, RPO = 24, Shotgun = 100, Screen = 0, Gap = 37, Zone = 63 } ),
                    new KeyValuePair<int, Tendency>(6, new Tendency { PA = 21, RPO = 16, Shotgun = 99, Screen = 9, Gap = 45, Zone = 48 } ),
                    new KeyValuePair<int, Tendency>(1, new Tendency { PA = 18, RPO = 27, Shotgun = 75, Screen = 7, Gap = 0, Zone = 100 } ),
                    new KeyValuePair<int, Tendency>(7, new Tendency { PA = 0, RPO = 17, Shotgun = 100, Screen = 8, Gap = 67, Zone = 22 } ),
                    new KeyValuePair<int, Tendency>(8, new Tendency { PA = 2, RPO = 2, Shotgun = 100, Screen = 11, Gap = 25, Zone = 25 } ),
                    new KeyValuePair<int, Tendency>(40, new Tendency { PA = 4, RPO = 0, Shotgun = 100, Screen = 14, Gap = 100, Zone = 0 } ),
                    new KeyValuePair<int, Tendency>(25, new Tendency { PA = 21, RPO = 18, Shotgun = 97, Screen = 8, Gap = 33, Zone = 64 } ),
                    new KeyValuePair<int, Tendency>(26, new Tendency { PA = 11, RPO = 26, Shotgun = 88, Screen = 14, Gap = 28, Zone = 70 } ),
                    new KeyValuePair<int, Tendency>(27, new Tendency { PA = 11, RPO = 26, Shotgun = 88, Screen = 14, Gap = 28, Zone = 70 } ),
                    new KeyValuePair<int, Tendency>(28, new Tendency { PA = 11, RPO = 26, Shotgun = 88, Screen = 14, Gap = 28, Zone = 70 } ),
                    new KeyValuePair<int, Tendency>(3, new Tendency { PA = 11, RPO = 17, Shotgun = 63, Screen = 0, Gap = 13, Zone = 87 } ),
                    new KeyValuePair<int, Tendency>(22, new Tendency { PA = 20, RPO = 20, Shotgun = 100, Screen = 0, Gap = 100, Zone = 0 } ),
                    new KeyValuePair<int, Tendency>(13, new Tendency { PA = 17, RPO = 9, Shotgun = 63, Screen = 9, Gap = 30, Zone = 66 } ),

                }
            },
            {"steelers", new List<KeyValuePair<int, Tendency>>
                {
                    new KeyValuePair<int, Tendency>(100, new Tendency { PA = 13, RPO = 13, Shotgun = 70, Screen = 9, Gap = 28, Zone = 67 } ),
                    new KeyValuePair<int, Tendency>(35, new Tendency { PA = 18, RPO = 16, Shotgun = 64, Screen = 11, Gap = 34, Zone = 65 } ),
                    new KeyValuePair<int, Tendency>(20, new Tendency { PA = 29, RPO = 23, Shotgun = 69, Screen = 22, Gap = 15, Zone = 85 } ),
                    new KeyValuePair<int, Tendency>(17, new Tendency { PA = 11, RPO = 14, Shotgun = 63, Screen = 15, Gap = 16, Zone = 74 } ),
                    new KeyValuePair<int, Tendency>(6, new Tendency { PA = 13, RPO = 13, Shotgun = 68, Screen = 6, Gap = 28, Zone = 61 } ),
                    new KeyValuePair<int, Tendency>(1, new Tendency { PA = 3, RPO = 0, Shotgun = 67, Screen = 0, Gap = 15, Zone = 61 } ),
                    new KeyValuePair<int, Tendency>(7, new Tendency { PA = 3, RPO = 0, Shotgun = 100, Screen = 3, Gap = 0, Zone = 0 } ),
                    new KeyValuePair<int, Tendency>(8, new Tendency { PA = 0, RPO = 0, Shotgun = 100, Screen = 2, Gap = 0, Zone = 0 } ),
                    new KeyValuePair<int, Tendency>(40, new Tendency { PA = 4, RPO = 21, Shotgun = 100, Screen = 23, Gap = 17, Zone = 17 } ),
                    new KeyValuePair<int, Tendency>(25, new Tendency { PA = 16, RPO = 14, Shotgun = 65, Screen = 10, Gap = 27, Zone = 68 } ),
                    new KeyValuePair<int, Tendency>(26, new Tendency { PA = 10, RPO = 20, Shotgun = 59, Screen = 10, Gap = 39, Zone = 61 } ),
                    new KeyValuePair<int, Tendency>(27, new Tendency { PA = 10, RPO = 20, Shotgun = 59, Screen = 10, Gap = 39, Zone = 61 } ),
                    new KeyValuePair<int, Tendency>(28, new Tendency { PA = 10, RPO = 20, Shotgun = 59, Screen = 10, Gap = 39, Zone = 61 } ),
                    new KeyValuePair<int, Tendency>(3, new Tendency { PA = 0, RPO = 0, Shotgun = 55, Screen = 0, Gap = 0, Zone = 100 } ),
                    new KeyValuePair<int, Tendency>(22, new Tendency { PA = 0, RPO = 0, Shotgun = 100, Screen = 0, Gap = 0, Zone = 0 } ),
                    new KeyValuePair<int, Tendency>(13, new Tendency { PA = 12, RPO = 7, Shotgun = 55, Screen = 8, Gap = 28, Zone = 67 } ),

                }
            },
            {"49ers", new List<KeyValuePair<int, Tendency>>
                {
                    new KeyValuePair<int, Tendency>(100, new Tendency { PA = 15, RPO = 2, Shotgun = 59, Screen = 8, Gap = 25, Zone = 72 } ),
                    new KeyValuePair<int, Tendency>(35, new Tendency { PA = 21, RPO = 3, Shotgun = 60, Screen = 9, Gap = 23, Zone = 75 } ),
                    new KeyValuePair<int, Tendency>(20, new Tendency { PA = 7, RPO = 2, Shotgun = 28, Screen = 0, Gap = 30, Zone = 70 } ),
                    new KeyValuePair<int, Tendency>(17, new Tendency { PA = 13, RPO = 8, Shotgun = 67, Screen = 0, Gap = 31, Zone = 69 } ),
                    new KeyValuePair<int, Tendency>(6, new Tendency { PA = 13, RPO = 1, Shotgun = 77, Screen = 8, Gap = 35, Zone = 52 } ),
                    new KeyValuePair<int, Tendency>(1, new Tendency { PA = 7, RPO = 0, Shotgun = 49, Screen = 0, Gap = 13, Zone = 87 } ),
                    new KeyValuePair<int, Tendency>(7, new Tendency { PA = 0, RPO = 0, Shotgun = 100, Screen = 0, Gap = 100, Zone = 0 } ),
                    new KeyValuePair<int, Tendency>(8, new Tendency { PA = 0, RPO = 0, Shotgun = 100, Screen = 0, Gap = 0, Zone = 100 } ),
                    new KeyValuePair<int, Tendency>(40, new Tendency { PA = 0, RPO = 4, Shotgun = 100, Screen = 27, Gap = 20, Zone = 20 } ),
                    new KeyValuePair<int, Tendency>(25, new Tendency { PA = 16, RPO = 2, Shotgun = 62, Screen = 9, Gap = 25, Zone = 74 } ),
                    new KeyValuePair<int, Tendency>(26, new Tendency { PA = 16, RPO = 3, Shotgun = 51, Screen = 15, Gap = 23, Zone = 72 } ),
                    new KeyValuePair<int, Tendency>(27, new Tendency { PA = 16, RPO = 3, Shotgun = 51, Screen = 15, Gap = 23, Zone = 72 } ),
                    new KeyValuePair<int, Tendency>(28, new Tendency { PA = 16, RPO = 3, Shotgun = 51, Screen = 15, Gap = 23, Zone = 72 } ),
                    new KeyValuePair<int, Tendency>(3, new Tendency { PA = 9, RPO = 5, Shotgun = 35, Screen = 0, Gap = 37, Zone = 63 } ),
                    new KeyValuePair<int, Tendency>(22, new Tendency { PA = 0, RPO = 0, Shotgun = 100, Screen = 0, Gap = 0, Zone = 0 } ),
                    new KeyValuePair<int, Tendency>(13, new Tendency { PA = 14, RPO = 1, Shotgun = 28, Screen = 9, Gap = 25, Zone = 72 } ),

                }
            },
            {"seahawks", new List<KeyValuePair<int, Tendency>>
                {
                    new KeyValuePair<int, Tendency>(100, new Tendency { PA = 21, RPO = 15, Shotgun = 72, Screen = 8, Gap = 24, Zone = 74 } ),
                    new KeyValuePair<int, Tendency>(35, new Tendency { PA = 30, RPO = 14, Shotgun = 58, Screen = 6, Gap = 22, Zone = 78 } ),
                    new KeyValuePair<int, Tendency>(20, new Tendency { PA = 12, RPO = 32, Shotgun = 74, Screen = 0, Gap = 32, Zone = 64 } ),
                    new KeyValuePair<int, Tendency>(17, new Tendency { PA = 27, RPO = 15, Shotgun = 60, Screen = 0, Gap = 35, Zone = 65 } ),
                    new KeyValuePair<int, Tendency>(6, new Tendency { PA = 18, RPO = 19, Shotgun = 88, Screen = 16, Gap = 26, Zone = 63 } ),
                    new KeyValuePair<int, Tendency>(1, new Tendency { PA = 17, RPO = 10, Shotgun = 79, Screen = 4, Gap = 0, Zone = 100 } ),
                    new KeyValuePair<int, Tendency>(7, new Tendency { PA = 0, RPO = 0, Shotgun = 100, Screen = 6, Gap = 0, Zone = 0 } ),
                    new KeyValuePair<int, Tendency>(8, new Tendency { PA = 0, RPO = 3, Shotgun = 100, Screen = 6, Gap = 0, Zone = 100 } ),
                    new KeyValuePair<int, Tendency>(40, new Tendency { PA = 9, RPO = 4, Shotgun = 100, Screen = 18, Gap = 0, Zone = 100 } ),
                    new KeyValuePair<int, Tendency>(25, new Tendency { PA = 27, RPO = 15, Shotgun = 67, Screen = 9, Gap = 25, Zone = 73 } ),
                    new KeyValuePair<int, Tendency>(26, new Tendency { PA = 7, RPO = 27, Shotgun = 70, Screen = 6, Gap = 24, Zone = 73 } ),
                    new KeyValuePair<int, Tendency>(27, new Tendency { PA = 7, RPO = 27, Shotgun = 70, Screen = 6, Gap = 24, Zone = 73 } ),
                    new KeyValuePair<int, Tendency>(28, new Tendency { PA = 7, RPO = 27, Shotgun = 70, Screen = 6, Gap = 24, Zone = 73 } ),
                    new KeyValuePair<int, Tendency>(3, new Tendency { PA = 13, RPO = 33, Shotgun = 56, Screen = 0, Gap = 17, Zone = 83 } ),
                    new KeyValuePair<int, Tendency>(22, new Tendency { PA = 17, RPO = 0, Shotgun = 100, Screen = 0, Gap = 0, Zone = 0 } ),
                    new KeyValuePair<int, Tendency>(13, new Tendency { PA = 20, RPO = 8, Shotgun = 56, Screen = 8, Gap = 25, Zone = 72 } ),

                }
            },
            {"buccaneers", new List<KeyValuePair<int, Tendency>>
                {
                   new KeyValuePair<int, Tendency>(100, new Tendency { PA = 20, RPO = 20, Shotgun = 71, Screen = 8, Gap = 30, Zone = 68 } ),
                    new KeyValuePair<int, Tendency>(35, new Tendency { PA = 24, RPO = 25, Shotgun = 56, Screen = 9, Gap = 30, Zone = 69 } ),
                    new KeyValuePair<int, Tendency>(20, new Tendency { PA = 21, RPO = 48, Shotgun = 82, Screen = 8, Gap = 12, Zone = 88 } ),
                    new KeyValuePair<int, Tendency>(17, new Tendency { PA = 29, RPO = 21, Shotgun = 67, Screen = 8, Gap = 22, Zone = 78 } ),
                    new KeyValuePair<int, Tendency>(6, new Tendency { PA = 25, RPO = 21, Shotgun = 74, Screen = 11, Gap = 45, Zone = 53 } ),
                    new KeyValuePair<int, Tendency>(1, new Tendency { PA = 19, RPO = 22, Shotgun = 72, Screen = 9, Gap = 15, Zone = 77 } ),
                    new KeyValuePair<int, Tendency>(7, new Tendency { PA = 3, RPO = 0, Shotgun = 100, Screen = 12, Gap = 100, Zone = 0 } ),
                    new KeyValuePair<int, Tendency>(8, new Tendency { PA = 0, RPO = 2, Shotgun = 100, Screen = 2, Gap = 0, Zone = 100 } ),
                    new KeyValuePair<int, Tendency>(40, new Tendency { PA = 4, RPO = 0, Shotgun = 100, Screen = 4, Gap = 0, Zone = 0 } ),
                    new KeyValuePair<int, Tendency>(25, new Tendency { PA = 23, RPO = 22, Shotgun = 63, Screen = 9, Gap = 32, Zone = 67 } ),
                    new KeyValuePair<int, Tendency>(26, new Tendency { PA = 18, RPO = 35, Shotgun = 71, Screen = 6, Gap = 21, Zone = 75 } ),
                    new KeyValuePair<int, Tendency>(27, new Tendency { PA = 18, RPO = 35, Shotgun = 71, Screen = 6, Gap = 21, Zone = 75 } ),
                    new KeyValuePair<int, Tendency>(28, new Tendency { PA = 18, RPO = 35, Shotgun = 71, Screen = 6, Gap = 21, Zone = 75 } ),
                    new KeyValuePair<int, Tendency>(3, new Tendency { PA = 38, RPO = 56, Shotgun = 55, Screen = 0, Gap = 14, Zone = 86 } ),
                    new KeyValuePair<int, Tendency>(22, new Tendency { PA = 17, RPO = 17, Shotgun = 100, Screen = 0, Gap = 0, Zone = 100 } ),
                    new KeyValuePair<int, Tendency>(13, new Tendency { PA = 19, RPO = 10, Shotgun = 55, Screen = 8, Gap = 30, Zone = 68 } ),
                }
            },
            {"titans", new List<KeyValuePair<int, Tendency>>
                {
                    new KeyValuePair<int, Tendency>(100, new Tendency { PA = 20, RPO = 7, Shotgun = 63, Screen = 14, Gap = 32, Zone = 67 } ),
                    new KeyValuePair<int, Tendency>(35, new Tendency { PA = 29, RPO = 9, Shotgun = 49, Screen = 14, Gap = 35, Zone = 65 } ),
                    new KeyValuePair<int, Tendency>(20, new Tendency { PA = 17, RPO = 40, Shotgun = 40, Screen = 0, Gap = 30, Zone = 70 } ),
                    new KeyValuePair<int, Tendency>(17, new Tendency { PA = 38, RPO = 10, Shotgun = 41, Screen = 18, Gap = 30, Zone = 70 } ),
                    new KeyValuePair<int, Tendency>(6, new Tendency { PA = 12, RPO = 9, Shotgun = 73, Screen = 18, Gap = 29, Zone = 67 } ),
                    new KeyValuePair<int, Tendency>(1, new Tendency { PA = 9, RPO = 0, Shotgun = 60, Screen = 5, Gap = 0, Zone = 100 } ),
                    new KeyValuePair<int, Tendency>(7, new Tendency { PA = 6, RPO = 3, Shotgun = 100, Screen = 0, Gap = 50, Zone = 50 } ),
                    new KeyValuePair<int, Tendency>(8, new Tendency { PA = 0, RPO = 0, Shotgun = 100, Screen = 14, Gap = 0, Zone = 0 } ),
                    new KeyValuePair<int, Tendency>(40, new Tendency { PA = 0, RPO = 0, Shotgun = 100, Screen = 37, Gap = 0, Zone = 100 } ),
                    new KeyValuePair<int, Tendency>(25, new Tendency { PA = 24, RPO = 9, Shotgun = 54, Screen = 16, Gap = 32, Zone = 67 } ),
                    new KeyValuePair<int, Tendency>(26, new Tendency { PA = 11, RPO = 6, Shotgun = 59, Screen = 6, Gap = 34, Zone = 66 } ),
                    new KeyValuePair<int, Tendency>(27, new Tendency { PA = 11, RPO = 6, Shotgun = 59, Screen = 6, Gap = 34, Zone = 66 } ),
                    new KeyValuePair<int, Tendency>(28, new Tendency { PA = 11, RPO = 6, Shotgun = 59, Screen = 6, Gap = 34, Zone = 66 } ),
                    new KeyValuePair<int, Tendency>(3, new Tendency { PA = 0, RPO = 10, Shotgun = 42, Screen = 0, Gap = 38, Zone = 63 } ),
                    new KeyValuePair<int, Tendency>(22, new Tendency { PA = 22, RPO = 11, Shotgun = 100, Screen = 11, Gap = 0, Zone = 0 } ),
                    new KeyValuePair<int, Tendency>(13, new Tendency { PA = 19, RPO = 4, Shotgun = 40, Screen = 14, Gap = 33, Zone = 66 } ),
                }
            },
            {"redskins", new List<KeyValuePair<int, Tendency>>
                {
                    new KeyValuePair<int, Tendency>(100, new Tendency { PA = 16, RPO = 14, Shotgun = 83, Screen = 9, Gap = 29, Zone = 70 } ),
                    new KeyValuePair<int, Tendency>(35, new Tendency { PA = 21, RPO = 17, Shotgun = 79, Screen = 8, Gap = 34, Zone = 66 } ),
                    new KeyValuePair<int, Tendency>(20, new Tendency { PA = 10, RPO = 22, Shotgun = 71, Screen = 25, Gap = 31, Zone = 69 } ),
                    new KeyValuePair<int, Tendency>(17, new Tendency { PA = 17, RPO = 22, Shotgun = 72, Screen = 20, Gap = 24, Zone = 76 } ),
                    new KeyValuePair<int, Tendency>(6, new Tendency { PA = 21, RPO = 16, Shotgun = 89, Screen = 8, Gap = 26, Zone = 69 } ),
                    new KeyValuePair<int, Tendency>(1, new Tendency { PA = 0, RPO = 11, Shotgun = 82, Screen = 0, Gap = 8, Zone = 92 } ),
                    new KeyValuePair<int, Tendency>(7, new Tendency { PA = 0, RPO = 0, Shotgun = 100, Screen = 4, Gap = 0, Zone = 0 } ),
                    new KeyValuePair<int, Tendency>(8, new Tendency { PA = 8, RPO = 0, Shotgun = 100, Screen = 5, Gap = 0, Zone = 0 } ),
                    new KeyValuePair<int, Tendency>(40, new Tendency { PA = 0, RPO = 0, Shotgun = 100, Screen = 20, Gap = 0, Zone = 0 } ),
                    new KeyValuePair<int, Tendency>(25, new Tendency { PA = 19, RPO = 16, Shotgun = 80, Screen = 9, Gap = 31, Zone = 68 } ),
                    new KeyValuePair<int, Tendency>(26, new Tendency { PA = 11, RPO = 14, Shotgun = 79, Screen = 10, Gap = 33, Zone = 67 } ),
                    new KeyValuePair<int, Tendency>(27, new Tendency { PA = 11, RPO = 14, Shotgun = 79, Screen = 10, Gap = 33, Zone = 67 } ),
                    new KeyValuePair<int, Tendency>(28, new Tendency { PA = 11, RPO = 14, Shotgun = 79, Screen = 10, Gap = 33, Zone = 67 } ),
                    new KeyValuePair<int, Tendency>(3, new Tendency { PA = 13, RPO = 31, Shotgun = 69, Screen = 0, Gap = 33, Zone = 67 } ),
                    new KeyValuePair<int, Tendency>(22, new Tendency { PA = 17, RPO = 33, Shotgun = 100, Screen = 0, Gap = 0, Zone = 100 } ),
                    new KeyValuePair<int, Tendency>(13, new Tendency { PA = 15, RPO = 7, Shotgun = 69, Screen = 8, Gap = 29, Zone = 70 } ),
                }
            }
        };

        // Defines the likely number of plays that this situation will present itself in the game that are not special teams plays
        public static readonly Dictionary<int, double> SituationExpectedNumPlays = new Dictionary<int, double>
            {
                {0,0}, // 1st Play
                {35,17.76}, // 1st Down
                {20,1.36}, // "2nd & Short"},
                {17,5.01}, //"2nd & Med"},
                {6,8.16}, //"2nd & Long"},
                {1,1.48}, //3rd and short
                {7,3.53}, // "3rd & Med"},
                {8,4.07}, // "3rd & Long"},
                {40,1.20 }, // "3rd & Extra Long"},
                {31,0.72 }, // "4th & Short"},
                {32,0.36 }, // "4th & Medium"},
                {33,0.17 }, // "4th & Long"},
                {41,0.09 }, // "4rd & Extra Long"},
                {25,0 }, // "Opp 25 to 21"},// combined all with 16 to 20 in LUA offense script
                {26,6.18 }, //"Red Zone 16 to 20"},
                {27,0 }, // "Red Zone 11 to 15"},
                {28,0 }, // "Red Zone 6 to 10"},
                {39,0 }, // "Red Zone 3 to 5"},Unused - Goal line and goal line pass take precedent
                {4,0 }, // "Red Zone"}, Not Used
                {5,0.20}, // "Inside Two"}, Changed from five to two by Sabos Mod
                {3,1.50}, // "Goal Line"},
                {22,0.67 }, // "Goal Line Pass"},
                {24,3.55 }, // "4 min Offense"}, Similar to 2 minute offense when playing from behind
                {10,3.55}, // "2 min Offense"},
                {37,0 }, // "Play Action"},
                {23,0 }, // "Signiature Plays"},
                {42,0 }, // "Sudden Change"},
                {43,0},// "Last Play"
                {21,0}, // "Hail Mary"
                {44,0}, // "Max"
                {2,0}, // "Stop Clock"
                {29,0}, // "Stop Clock User"
                {30,0}, // "Stop Clock Fake User"
                {13,3.27}, // "Waste Time" Don't know when this kicks in, will just assume last 3 minutes of game - should be mostly runs
                {14,0}, // "Kneel"
                {38,0}, // "Extra Point"
                {34,0}, // "Go for 2"
                {12,0}, // "Fake FG"
                {9,0}, // "Punt"
                {19,0}, // "Punt Max Protect"
                {11,0}, // "Fake Punt"
                {15,0}, // "Kickoff"
                {36,0}, // "Squib"
                {16,0}, // "Kickoff Onside"
                {18,0} // "Kickoff Safety"
            };

        // Defines the target pass depth per situation, this will give an average pass depth for the gameplan close to the nfl ~ 7.75
        public static readonly Dictionary<int, double> SituationTargetPassDepth = new Dictionary<int, double>
            {
                {35,7.95}, // 1st Down
                {20,7.85}, // "2nd & Short"},
                {17,7.40}, //"2nd & Med"},
                {6,6.78}, //"2nd & Long"},
                {1,7.10}, //3rd and short
                {7,9.06}, // "3rd & Med"},
                {8,10.36}, // "3rd & Long"},
                {40,8.21}, // "3rd & Extra Long"},
                {31,6.22}, // "4th & Short"},
                {32,7.80}, // "4th & Medium"},
                {33,13.80}, // "4th & Long"},
                {41,20.11}, // "4rd & Extra Long"},
                {25,6.06}, // "Red Zone Fringe"},
                {26,6.89}, //"Red Zone 16 to 20"},
                {27,7.63}, // "Red Zone 11 to 15"},
                {28,7.47}, // "Red Zone 6 to 10"},
                {39,7.52}, // "Red Zone 3 to 5"},
                {5,8.0}, // "Inside Two"}, Changed from five to two by Sabos Mod
                {24,8.13}, // "4 min Offense"}, Similar to 2 minute offense when playing from behind
                {10,9.72}, // "2 min Offense"},
                {3,6.44}, // "Goal Line"}
                {13,3.20 }, // "Waste Time"}
                {22,7.52 } // "Goal Line Pass"}
            };

        // Defines the target run percentage per situation, this will give an average run percent for the gameplan close to the nfl ~ 42.5
        public static readonly Dictionary<int, double> SituationTargetRunPercentage = new Dictionary<int, double>
            {
                {35,50.84}, // 1st Down
                {20,66.74}, // "2nd & Short"},
                {17,49.55}, //"2nd & Med"},
                {6,29.86}, //"2nd & Long"},
                {1,53.00}, //3rd and short
                {7,14.31}, // "3rd & Med"},
                {8,9.39}, // "3rd & Long"},
                {40,9.39}, // "3rd & Extra Long"},
                {31,53.99}, // "4th & Short"},
                {32,0}, // "4th & Medium"},
                {33,0}, // "4th & Long"},
                {41,0}, // "4rd & Extra Long"},
                {25,50.87}, // "Opp 30 to 21"},
                {26,54.05}, //"Red Zone 16 to 20"},
                {27,53.22}, // "Red Zone 11 to 15"},
                {28,57.64}, // "Red Zone 6 to 10"},
                {5,51.28}, // "Inside Two"}, Changed from five to two by Sabos Mod
                {24,19.45}, // "4 min Offense"}, Similar to 2 minute offense when playing from behind
                {10,0}, // "2 min Offense"},
                {13,95.00}, // "Waste Time"
                {3,66.09}, // "Goal Line"},
                {22,20.00} // "Goal Line Pass"},
            };

        // List of low percentage situations in the PBAI that can have long pass records removed in order to get under the 2000 record limit of that table
        public static readonly List<int> SituationsToTrim = new List<int>
            {
                24, 25, 27, 28, 37, 39
            };

        // List of situations to ignore for the purposes of getting rpo, shotgun, playaction, gap, zone and screen passes adjusted to the team's tendency
        // I don't have data on these and am just going to adjust these for route depth and run/pass ratio only
        public static readonly List<int> SituationsToIgnore = new List<int>
            {
                31, 32, 33, 41, 10, 5, 13, 22, 3, 24
            };

        // These sitatuations do not have a lot of passes so can't really adjust route depth
        public static readonly List<int> SituationsToIgnoreRouteDepth = new List<int>
            {
                41
            };

        // Situations that are missing screen plays
        public static readonly List<int> SituationsToAddHBScreens = new List<int>
            {
                20, 1, 40, 13
            };

        // Situations that are missing WR and TE screen plays
        public static readonly List<int> SituationsToAddWRTEScreens = new List<int>
            {
                20, 1, 40, 13
            };

        // Situations that are missing RPO plays
        public static readonly List<int> SituationsToAddRPOs = new List<int>
            {
                1, 40, 13
            };

        // Situations that are missing playaction plays
        public static readonly List<int> SituationsToAddPlayAction = new List<int>
            {
                20, 40, 1, 13
            };

        // Situations that are missing draw plays
        public static readonly List<int> SituationsToAddDraws = new List<int>
            {
                24, 40
            };

        // Situations that are missing all run plays
        public static readonly List<int> SituationsToAddGapZoneRuns = new List<int>
        {

        };

        // Situations that are missing non-shotgun run plays
        public static readonly List<int> SituationsToAddNonShotgunGapZoneRuns = new List<int>
        {

        };

        // Situations that are missing shotgun zone and gap run plays
        public static readonly List<int> SituationsToAddShotgunGapZoneRuns = new List<int>
            {
                22, 40, 1
            };

        // Situations that are missing pass plays non shotgun
        public static readonly List<int> SituationsToAddNonShotgunPasses = new List<int>
            {
                5
            };

        // Situations that are missing shotgun pass plays
        public static readonly List<int> SituationsToAddShotgunPasses = new List<int>
            {
                33, 10, 32, 31, 24, 40
            };

        // Situations to add all plays
        public static readonly List<int> SituationsToAddAllPlays = new List<int>
            {
                6, 7, 8, 26
            };

        // Defines the average throw depth for each team when calculating the revised gameplan
        // default is 2023 nfl values
        public static Dictionary<string, double> TeamAverageThrowDepth = new Dictionary<string, double>
            {
                {"cardinals",8.1},
                {"falcons",8.3},
                {"ravens",8.6},
                {"bills",8.0},
                {"panthers",7.3},
                {"bears",6.8},
                {"bengals",6.1},
                {"browns",8.4},
                {"cowboys",7.9},
                {"broncos",7.0},
                {"lions",6.9},
                {"packers",8.4},
                {"texans",9.2},
                {"colts",7.3},
                {"jaguars",7.8},
                {"chiefs",6.8},
                {"raiders",7.4},
                {"chargers",7.9},
                {"rams",8.1},
                {"dolphins",7.7},
                {"vikings",7.1},
                {"patriots",7.2},
                {"saints",8.5},
                {"giants",7.1},
                {"jets",7.4},
                {"eagles",8.4},
                {"steelers",7.4},
                {"49ers",7.9},
                {"seahawks",7.9},
                {"buccaneers",8.9},
                {"titans",9.7},
                {"redskins",7.4}
            };

        // Defines the run percentages for each team when calculating the revised gameplan, considers QB scrambles as pass att ~ 2 per game,
        // default is 2023 nfl values
        public static Dictionary<string, double> TeamRunPercentage = new Dictionary<string, double>
            {
                {"cardinals",37.98},
                {"falcons",45.24},
                {"ravens",44.68},
                {"bills",38.08},
                {"panthers",33.94},
                {"bears",43.56},
                {"bengals",30.44},
                {"browns",42.98},
                {"cowboys",40.18},
                {"broncos",40.44},
                {"lions",42.57},
                {"packers",37.97},
                {"texans",38.54},
                {"colts",40.81},
                {"jaguars",40.30},
                {"chiefs",34.23},
                {"raiders",39.46},
                {"chargers",35.79},
                {"rams",40.09},
                {"dolphins",41.01},
                {"vikings",34.47},
                {"patriots",38.69},
                {"saints",39.07},
                {"giants",36.85},
                {"jets",31.28},
                {"eagles",42.93},
                {"steelers",41.52},
                {"49ers",48.50},
                {"seahawks",36.53},
                {"buccaneers",36.00},
                {"titans",40.78},
                {"redskins",28.95}
            };

        public static int RandomInt(int min, int max)
        {
            var rng = RandomNumberGenerator.Create();
            var buffer = new byte[4];

            rng.GetBytes(buffer);
            int result = BitConverter.ToInt32(buffer, 0);

            return new Random(result).Next(min, max);
        }

        public static double GetTeamRunPercentage(string filePath)
        {
            return TeamRunPercentage.FirstOrDefault(p => Path.GetFileName(filePath).Contains(p.Key)).Value;
        }

        public static double GetTeamPassDepth(string filePath)
        {
            return TeamAverageThrowDepth.FirstOrDefault(p => Path.GetFileName(filePath).Contains(p.Key)).Value;
        }

        public static double GetSituationRunPercentage(int SituationID)
        {
            return SituationTargetRunPercentage.FirstOrDefault(p => p.Key == SituationID).Value;
        }

        public static double GetSituationRouteDepth(int SituationID)
        {
            return SituationTargetPassDepth.FirstOrDefault(p => p.Key == SituationID).Value;
        }

        public static Tendency GetTeamSituationTendency(string filePath, int SituationID)
        {
            return TeamTendencies.FirstOrDefault(p => Path.GetFileName(filePath).Contains(p.Key)).Value.FirstOrDefault(p => p.Key == SituationID).Value ??
                 TeamTendencies.FirstOrDefault(p => Path.GetFileName(filePath).Contains(p.Key)).Value.FirstOrDefault(p => p.Key == 100).Value;
        }

        public static bool IsDivisible(int x, int n)
        {
            return (x % n) == 0;
        }

        // Used to determine how much to adjust the prct or weight for each play, factor increases the significance of the adjustment
        public static int GetWeightAdjustment(double Adjustment, double Factor = 1.0)
        {
            int WeightAdjustment;
            if (Adjustment > 0)
            {
                WeightAdjustment = Convert.ToInt32(1 * Factor);
            }
            else
            {
                WeightAdjustment = -1;
            }
            //NewWeight = Math.Max(Math.Min(NewWeight, 99), 0);
            return WeightAdjustment;
        }
    }
}
