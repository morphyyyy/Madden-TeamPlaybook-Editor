using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using TDBAccess;

namespace Madden.TeamPlaybook
{
    [Serializable]
    public class PSAL
    {
        public static readonly Dictionary<KeyValuePair<int, string>, Dictionary<KeyValuePair<string, string>, Dictionary<int, string>>> CodeDefinition = new Dictionary<KeyValuePair<int, string>, Dictionary<KeyValuePair<string, string>, Dictionary<int, string>>>
        {
            {
                new KeyValuePair<int, string>(-1, "None Value"),
                new Dictionary<KeyValuePair<string, string>, Dictionary<int, string>>{ }
            },
            {
                new KeyValuePair<int, string>(0, "None"),
                new Dictionary<KeyValuePair<string, string>, Dictionary<int, string>>{ }
            },
            {
                new KeyValuePair<int, string>(1, "Run Easy"),
                new Dictionary<KeyValuePair<string, string>, Dictionary<int, string>>{ }
            },
            {
                new KeyValuePair<int, string>(2, "Chase Ball"),
                new Dictionary<KeyValuePair<string, string>, Dictionary<int, string>>{ }
            },
            {
                new KeyValuePair<int, string>(3, "Move Direction Distance"),
                new Dictionary<KeyValuePair<string, string>, Dictionary<int, string>>{ }
            },
            {
                new KeyValuePair<int, string>(4, "Move Direction Distance Constant"),
                new Dictionary<KeyValuePair<string, string>, Dictionary<int, string>>{ }
            },
            {
                new KeyValuePair<int, string>(5, "Face Direction"),
                new Dictionary<KeyValuePair<string, string>, Dictionary<int, string>>{ }
            },
            {
                new KeyValuePair<int, string>(6, "Ball Carrier Dive"),
                new Dictionary<KeyValuePair<string, string>, Dictionary<int, string>>{ }
            },
            {
                new KeyValuePair<int, string>(7, "Scramble"),
                new Dictionary<KeyValuePair<string, string>, Dictionary<int, string>>{ }
            },
            {
                new KeyValuePair<int, string>(8, "Run Route"),
                new Dictionary<KeyValuePair<string, string>, Dictionary<int, string>>{ }
            },
            {
                new KeyValuePair<int, string>(9, "Receiver Cut"),
                new Dictionary<KeyValuePair<string, string>, Dictionary<int, string>>{ }
            },
            {
                new KeyValuePair<int, string>(10, "Receiver Get Open"),
                new Dictionary<KeyValuePair<string, string>, Dictionary<int, string>>{ }
            },
            {
                new KeyValuePair<int, string>(11, "Pitch"),
                new Dictionary<KeyValuePair<string, string>, Dictionary<int, string>>{ }
            },
            {
                new KeyValuePair<int, string>(12, "Handoff Option"),
                new Dictionary<KeyValuePair<string, string>, Dictionary<int, string>>{ }
            },
            {
                new KeyValuePair<int, string>(13, "Receive Handoff"),
                new Dictionary<KeyValuePair<string, string>, Dictionary<int, string>>{ }
            },
            {
                new KeyValuePair<int, string>(14, "Pass Block"),
                new Dictionary<KeyValuePair<string, string>, Dictionary<int, string>>{ }
            },
            {
                new KeyValuePair<int, string>(15, "Run Block"),
                new Dictionary<KeyValuePair<string, string>, Dictionary<int, string>>{ }
            },
            {
                new KeyValuePair<int, string>(16, "Kickoff"),
                new Dictionary<KeyValuePair<string, string>, Dictionary<int, string>>{ }
            },
            {
                new KeyValuePair<int, string>(17, "Kick Return"),
                new Dictionary<KeyValuePair<string, string>, Dictionary<int, string>>{ }
            },
            {
                new KeyValuePair<int, string>(18, "Lead Block"),
                new Dictionary<KeyValuePair<string, string>, Dictionary<int, string>>{ }
            },
            {
                new KeyValuePair<int, string>(19, "Man Coverage"),
                new Dictionary<KeyValuePair<string, string>, Dictionary<int, string>>{ }
            },
            {
                new KeyValuePair<int, string>(20, "Flat Zone"),
                new Dictionary<KeyValuePair<string, string>, Dictionary<int, string>>{ }
            },
            {
                new KeyValuePair<int, string>(21, "Hook Zone"),
                new Dictionary<KeyValuePair<string, string>, Dictionary<int, string>>{ }
            },
            {
                new KeyValuePair<int, string>(22, "Curl Flat Zone"),
                new Dictionary<KeyValuePair<string, string>, Dictionary<int, string>>{ }
            },
            {
                new KeyValuePair<int, string>(23, "Deep Zone"),
                new Dictionary<KeyValuePair<string, string>, Dictionary<int, string>>{ }
            },
            {
                new KeyValuePair<int, string>(24, "Show Blitz"),
                new Dictionary<KeyValuePair<string, string>, Dictionary<int, string>>{ }
            },
            {
                new KeyValuePair<int, string>(25, "Delay"),
                new Dictionary<KeyValuePair<string, string>, Dictionary<int, string>>{ }
            },
            {
                new KeyValuePair<int, string>(26, "Initial Move"),
                new Dictionary<KeyValuePair<string, string>, Dictionary<int, string>>
                {
                    { 
                        new KeyValuePair<string, string>("val1", "Animation Type"),
                        new Dictionary<int, string>
                        {
                            { 0, "Shuffle" },
                            { 1, "Backstep" },
                            { 2, "Hopstep" },
                            { 3, "Counterstep" },
                            { 4, "Power Pull" },
                            { 5, "WR Start" },
                            { 6, "HB Rec Pitch" },
                            { 7, "QB Option" },
                            { 8, "QB Option Counter" },
                            { 9, "QB Speed Option" },
                            { 10, "FB Triple Option" },
                            { 11, "Def Flatzone Left" },
                            { 12, "Def Drop Hookzone" },
                            { 13, "Def Blitz" },
                            { 14, "Def Blitz Drop" },
                            { 15, "Def DL Backpedal" },
                            { 16, "Def DL Turn & Run" },
                            { 17, "Def DL Stunt" },
                            { 18, "Def DL Stunt Loop" },
                            { 19, "Def DL Fireoff" },
                            { 20, "FB Receive Handoff" },
                            { 21, "FB Lead Block" },
                            { 22, "HB Lateral Step" },
                            { 23, "Skip Pull" },
                            { 24, "Truck Pull" },
                            { 25, "Trap Pull" },
                            { 26, "Screen Pull" },
                            { 27, "Counter Pull" },
                            { 28, "FB Counter 2pt" },
                            { 29, "FB Counter 3pt" },
                            { 30, "FB Counter 3pt Left" },
                            { 31, "WR Start Quick" },
                            { 32, "Run Block" },
                            { 33, "2pt Pass Block Passive" },
                            { 34, "2pt Pass Block Aggressive" },
                            { 35, "2pt Pass Block Slide" },
                            { 36, "3pt Pass Block Passive" },
                            { 37, "3pt Pass Block Aggressive" },
                            { 38, "3pt Pass Block Slide" },
                            { 39, "2pt FG Block" },
                            { 40, "DL Pass Rush" },
                            { 41, "DL Drop" },
                            { 42, "HB Run Route" },
                            { 43, "Lateral Run Block" },
                            { 44, "Kickoff Coverage" },
                            { 45, "Def DL Stand" },
                            { 46, "Def Backside Contain" },
                            { 47, "Def DL Option Read" },
                            { 48, "RB Outside Right" },
                            { 49, "RB Outside Left" },
                            { 50, "RB Right to Middle" },
                            { 51, "RB Left to Middle" },
                            { 52, "RB Forward Fast" },
                            { 53, "RB Shotgun Right" },
                            { 54, "RB Shotgun Left" },
                            { 55, "RB Offset Right" },
                            { 56, "RB Offset Left" },
                            { 57, "FB Outside Right" },
                            { 58, "FB Outside Left" },
                            { 59, "FB Inside Right" },
                            { 60, "FB Inside Left" },
                            { 61, "FB Forward"}
                        }
                    }
                }
            },
            {
                new KeyValuePair<int, string>(27, "FG Spot"),
                new Dictionary<KeyValuePair<string, string>, Dictionary<int, string>>{ }
            },
            {
                new KeyValuePair<int, string>(28, "FG Kick"),
                new Dictionary<KeyValuePair<string, string>, Dictionary<int, string>>{ }
            },
            {
                new KeyValuePair<int, string>(29, "Punt"),
                new Dictionary<KeyValuePair<string, string>, Dictionary<int, string>>{ }
            },
            {
                new KeyValuePair<int, string>(30, "Stop Clock"),
                new Dictionary<KeyValuePair<string, string>, Dictionary<int, string>>{ }
            },
            {
                new KeyValuePair<int, string>(31, "Kneel"),
                new Dictionary<KeyValuePair<string, string>, Dictionary<int, string>>{ }
            },
            {
                new KeyValuePair<int, string>(32, "Receive Pitch"),
                new Dictionary<KeyValuePair<string, string>, Dictionary<int, string>>{ }
            },
            {
                new KeyValuePair<int, string>(33, "Pull Block"),
                new Dictionary<KeyValuePair<string, string>, Dictionary<int, string>>{ }
            },
            {
                new KeyValuePair<int, string>(34, "Read and Blitz"),
                new Dictionary<KeyValuePair<string, string>, Dictionary<int, string>>{ }
            },
            {
                new KeyValuePair<int, string>(35, "Head Turn Run Route"),
                new Dictionary<KeyValuePair<string, string>, Dictionary<int, string>>{ }
            },
            {
                new KeyValuePair<int, string>(36, "Option Route"),
                new Dictionary<KeyValuePair<string, string>, Dictionary<int, string>>{ }
            },
            {
                new KeyValuePair<int, string>(37, "Option Route Extra Info"),
                new Dictionary<KeyValuePair<string, string>, Dictionary<int, string>>{ }
            },
            {
                new KeyValuePair<int, string>(38, "Handoff Turn"),
                new Dictionary<KeyValuePair<string, string>, Dictionary<int, string>>{ }
            },
            {
                new KeyValuePair<int, string>(39, "Handoff Give"),
                new Dictionary<KeyValuePair<string, string>, Dictionary<int, string>>{ }
            },
            {
                new KeyValuePair<int, string>(40, "Handoff Fake"),
                new Dictionary<KeyValuePair<string, string>, Dictionary<int, string>>{ }
            },
            {
                new KeyValuePair<int, string>(41, "Pass Rush"),
                new Dictionary<KeyValuePair<string, string>, Dictionary<int, string>>{ }
            },
            {
                new KeyValuePair<int, string>(42, "Option Run"),
                new Dictionary<KeyValuePair<string, string>, Dictionary<int, string>>{ }
            },
            {
                new KeyValuePair<int, string>(43, "Option Follow"),
                new Dictionary<KeyValuePair<string, string>, Dictionary<int, string>>{ }
            },
            {
                new KeyValuePair<int, string>(44, "Wedge Block"),
                new Dictionary<KeyValuePair<string, string>, Dictionary<int, string>>{ }
            },
            {
                new KeyValuePair<int, string>(45, "Auto Motion"),
                new Dictionary<KeyValuePair<string, string>, Dictionary<int, string>>{ }
            },
            {
                new KeyValuePair<int, string>(46, "Auto Motion Snap"),
                new Dictionary<KeyValuePair<string, string>, Dictionary<int, string>>{ }
            },
            {
                new KeyValuePair<int, string>(47, "Defensive Movement"),
                new Dictionary<KeyValuePair<string, string>, Dictionary<int, string>>{ }
            },
            {
                new KeyValuePair<int, string>(48, "Override Formation Position"),
                new Dictionary<KeyValuePair<string, string>, Dictionary<int, string>>{ }
            },
            {
                new KeyValuePair<int, string>(49, "Call for Ball"),
                new Dictionary<KeyValuePair<string, string>, Dictionary<int, string>>{ }
            },
            {
                new KeyValuePair<int, string>(50, "Run Route Turbo"),
                new Dictionary<KeyValuePair<string, string>, Dictionary<int, string>>{ }
            },
            {
                new KeyValuePair<int, string>(51, "Run Route Fake Out"),
                new Dictionary<KeyValuePair<string, string>, Dictionary<int, string>>{ }
            },
            {
                new KeyValuePair<int, string>(52, "Defensive Fake Out"),
                new Dictionary<KeyValuePair<string, string>, Dictionary<int, string>>{ }
            },
            {
                new KeyValuePair<int, string>(53, "Defensive Fake Out Extra Info"),
                new Dictionary<KeyValuePair<string, string>, Dictionary<int, string>>{ }
            },
            {
                new KeyValuePair<int, string>(54, "Move Defender"),
                new Dictionary<KeyValuePair<string, string>, Dictionary<int, string>>{ }
            },
            {
                new KeyValuePair<int, string>(55, "Delay Block"),
                new Dictionary<KeyValuePair<string, string>, Dictionary<int, string>>{ }
            },
            {
                new KeyValuePair<int, string>(56, "Disabled"),
                new Dictionary<KeyValuePair<string, string>, Dictionary<int, string>>{ }
            },
            {
                new KeyValuePair<int, string>(57, "Defensive Alignment"),
                new Dictionary<KeyValuePair<string, string>, Dictionary<int, string>>
                {
                    {
                        new KeyValuePair<string, string>("val1", "Alignment"),
                        new Dictionary<int, string>()
                    },
                    {
                        new KeyValuePair<string, string>("val2", "Receiver SubPosition"),
                        new Dictionary<int, string>
                        {
                            { -1, "Invalid" },
                            { 0, "First" },
                            { 1, "WR" },
                            { 2, "TE" },
                            { 3, "RB" },
                            { 4, "WR Reduced Split" },
                            { 5, "WR Slot" },
                            { 6, "WR Tight Outside" },
                            { 7, "WR Tight Slot" },
                            { 8, "TE Twin Inside" },
                            { 9, "TE Twin Outside" },
                            { 10, "Bunch Outside" },
                            { 11, "Bunch Inside" },
                            { 12, "Bunch Middle" },
                            { 13, "Nub TE" },
                            { 14, "Trips Outside" },
                            { 15, "Trips Inside" },
                            { 16, "Trips Middle" },
                            { 17, "Stack Top" },
                            { 18, "Stack Bottom" },
                            { 19, "Stack Middle" },
                            { 20, "FB" },
                            { 21, "Num Positions"}
                        }
                    },
                    { 
                        new KeyValuePair<string, string>("val3", "Alignment Technique"),
                        new Dictionary<int, string>
                        {
                            {0, "No Align" },
                            {1, "Head Up" },
                            {2, "Inside 1" },
                            {3, "Outside 1" },
                            {4, "Outside 2" },
                            {5, "Outside 3" },
                            {6, "Outside 4" },
                            {7, "Outside 5" },
                            {8, "Split" },
                            {9, "Divider Rule" },
                            {10, "Inside Shoulder" },
                            {11, "Outside Shoulder"}
                        }
                    }
                }
            },
            {
                new KeyValuePair<int, string>(58, "Canned Handoff"),
                new Dictionary<KeyValuePair<string, string>, Dictionary<int, string>>
                {
                    {
                        new KeyValuePair<string, string>("val1", "Player Number"),
                        new Dictionary<int, string>()
                    },
                    {
                        new KeyValuePair<string, string>("val2", "Handoff Animation"),
                        new Dictionary<int, string>()
                    },
                    {
                        new KeyValuePair<string, string>("val3", "Flipped Handoff Animation"),
                        new Dictionary<int, string>()
                    }
                }
            },
            {
                new KeyValuePair<int, string>(59, "Get Ball"),
                new Dictionary<KeyValuePair<string, string>, Dictionary<int, string>>{ }
            },
            {
                new KeyValuePair<int, string>(60, "Jump"),
                new Dictionary<KeyValuePair<string, string>, Dictionary<int, string>>{ }
            },
            {
                new KeyValuePair<int, string>(61, "Unused 4"),
                new Dictionary<KeyValuePair<string, string>, Dictionary<int, string>>{ }
            },
            {
                new KeyValuePair<int, string>(62, "Set Animation"),
                new Dictionary<KeyValuePair<string, string>, Dictionary<int, string>>{ }
            },
            {
                new KeyValuePair<int, string>(63, "Fall"),
                new Dictionary<KeyValuePair<string, string>, Dictionary<int, string>>{ }
            },
            {
                new KeyValuePair<int, string>(64, "Get Up"),
                new Dictionary<KeyValuePair<string, string>, Dictionary<int, string>>{ }
            },
            {
                new KeyValuePair<int, string>(65, "Move Offense Huddle"),
                new Dictionary<KeyValuePair<string, string>, Dictionary<int, string>>{ }
            },
            {
                new KeyValuePair<int, string>(66, "Move Defense Huddle"),
                new Dictionary<KeyValuePair<string, string>, Dictionary<int, string>>{ }
            },
            {
                new KeyValuePair<int, string>(67, "Pass"),
                new Dictionary<KeyValuePair<string, string>, Dictionary<int, string>>{ }
            },
            {
                new KeyValuePair<int, string>(68, "Wrap Tackle Offense"),
                new Dictionary<KeyValuePair<string, string>, Dictionary<int, string>>{ }
            },
            {
                new KeyValuePair<int, string>(69, "Wrap Tackle Defense"),
                new Dictionary<KeyValuePair<string, string>, Dictionary<int, string>>{ }
            },
            {
                new KeyValuePair<int, string>(70, "Move To Catch"),
                new Dictionary<KeyValuePair<string, string>, Dictionary<int, string>>{ }
            },
            {
                new KeyValuePair<int, string>(71, "Play Ball"),
                new Dictionary<KeyValuePair<string, string>, Dictionary<int, string>>{ }
            },
            {
                new KeyValuePair<int, string>(72, "Catch"),
                new Dictionary<KeyValuePair<string, string>, Dictionary<int, string>>{ }
            },
            {
                new KeyValuePair<int, string>(73, "Timeout"),
                new Dictionary<KeyValuePair<string, string>, Dictionary<int, string>>{ }
            },
            {
                new KeyValuePair<int, string>(74, "Wrap Block"),
                new Dictionary<KeyValuePair<string, string>, Dictionary<int, string>>{ }
            },
            {
                new KeyValuePair<int, string>(75, "Juke"),
                new Dictionary<KeyValuePair<string, string>, Dictionary<int, string>>{ }
            },
            {
                new KeyValuePair<int, string>(76, "Catch"),
                new Dictionary<KeyValuePair<string, string>, Dictionary<int, string>>{ }
            },
            {
                new KeyValuePair<int, string>(77, "Hurdle"),
                new Dictionary<KeyValuePair<string, string>, Dictionary<int, string>>{ }
            },
            {
                new KeyValuePair<int, string>(78, "Chuck"),
                new Dictionary<KeyValuePair<string, string>, Dictionary<int, string>>{ }
            },
            {
                new KeyValuePair<int, string>(79, "Defense Preplay"),
                new Dictionary<KeyValuePair<string, string>, Dictionary<int, string>>{ }
            },
            {
                new KeyValuePair<int, string>(80, "Stiffarm"),
                new Dictionary<KeyValuePair<string, string>, Dictionary<int, string>>{ }
            },
            {
                new KeyValuePair<int, string>(81, "Unused 7"),
                new Dictionary<KeyValuePair<string, string>, Dictionary<int, string>>{ }
            },
            {
                new KeyValuePair<int, string>(82, "Receive Snap"),
                new Dictionary<KeyValuePair<string, string>, Dictionary<int, string>>{ }
            },
            {
                new KeyValuePair<int, string>(83, "Stumble"),
                new Dictionary<KeyValuePair<string, string>, Dictionary<int, string>>{ }
            },
            {
                new KeyValuePair<int, string>(84, "Tight Rope"),
                new Dictionary<KeyValuePair<string, string>, Dictionary<int, string>>{ }
            },
            {
                new KeyValuePair<int, string>(85, "Unused 2"),
                new Dictionary<KeyValuePair<string, string>, Dictionary<int, string>>{ }
            },
            {
                new KeyValuePair<int, string>(86, "Unused 3"),
                new Dictionary<KeyValuePair<string, string>, Dictionary<int, string>>{ }
            },
            {
                new KeyValuePair<int, string>(87, "Play Over"),
                new Dictionary<KeyValuePair<string, string>, Dictionary<int, string>>{ }
            },
            {
                new KeyValuePair<int, string>(88, "Move To Point"),
                new Dictionary<KeyValuePair<string, string>, Dictionary<int, string>>{ }
            },
            {
                new KeyValuePair<int, string>(89, "Set Flag"),
                new Dictionary<KeyValuePair<string, string>, Dictionary<int, string>>{ }
            },
            {
                new KeyValuePair<int, string>(90, "Unused 6"),
                new Dictionary<KeyValuePair<string, string>, Dictionary<int, string>>{ }
            },
            {
                new KeyValuePair<int, string>(91, "Unused 5"),
                new Dictionary<KeyValuePair<string, string>, Dictionary<int, string>>{ }
            },
            {
                new KeyValuePair<int, string>(92, "Huddle Check"),
                new Dictionary<KeyValuePair<string, string>, Dictionary<int, string>>{ }
            },
            {
                new KeyValuePair<int, string>(93, "Injury"),
                new Dictionary<KeyValuePair<string, string>, Dictionary<int, string>>{ }
            },
            {
                new KeyValuePair<int, string>(94, "Slide"),
                new Dictionary<KeyValuePair<string, string>, Dictionary<int, string>>{ }
            },
            {
                new KeyValuePair<int, string>(95, "Celebration"),
                new Dictionary<KeyValuePair<string, string>, Dictionary<int, string>>{ }
            },
            {
                new KeyValuePair<int, string>(96, "Kickoff Spot Ball"),
                new Dictionary<KeyValuePair<string, string>, Dictionary<int, string>>{ }
            },
            {
                new KeyValuePair<int, string>(97, "Wait For Flag"),
                new Dictionary<KeyValuePair<string, string>, Dictionary<int, string>>{ }
            },
            {
                new KeyValuePair<int, string>(98, "Preplay"),
                new Dictionary<KeyValuePair<string, string>, Dictionary<int, string>>{ }
            },
            {
                new KeyValuePair<int, string>(99, "Set Animation Face Direction"),
                new Dictionary<KeyValuePair<string, string>, Dictionary<int, string>>{ }
            },
            {
                new KeyValuePair<int, string>(100, "Sty In Pose"),
                new Dictionary<KeyValuePair<string, string>, Dictionary<int, string>>{ }
            },
            {
                new KeyValuePair<int, string>(101, "Defensive Cut"),
                new Dictionary<KeyValuePair<string, string>, Dictionary<int, string>>{ }
            },
            {
                new KeyValuePair<int, string>(102, "Hit Stick"),
                new Dictionary<KeyValuePair<string, string>, Dictionary<int, string>>{ }
            },
            {
                new KeyValuePair<int, string>(103, "Snap"),
                new Dictionary<KeyValuePair<string, string>, Dictionary<int, string>>{ }
            },
            {
                new KeyValuePair<int, string>(104, "Option Transition"),
                new Dictionary<KeyValuePair<string, string>, Dictionary<int, string>>{ }
            },
            {
                new KeyValuePair<int, string>(105, "Strip Ball"),
                new Dictionary<KeyValuePair<string, string>, Dictionary<int, string>>{ }
            },
            {
                new KeyValuePair<int, string>(106, "Coach"),
                new Dictionary<KeyValuePair<string, string>, Dictionary<int, string>>{ }
            },
            {
                new KeyValuePair<int, string>(107, "Wrap Celebration Teammate"),
                new Dictionary<KeyValuePair<string, string>, Dictionary<int, string>>{ }
            },
            {
                new KeyValuePair<int, string>(108, "Wrap Celebration Key Player"),
                new Dictionary<KeyValuePair<string, string>, Dictionary<int, string>>{ }
            },
            {
                new KeyValuePair<int, string>(109, "Kick Return Assist"),
                new Dictionary<KeyValuePair<string, string>, Dictionary<int, string>>{ }
            },
            {
                new KeyValuePair<int, string>(110, "Prevent"),
                new Dictionary<KeyValuePair<string, string>, Dictionary<int, string>>{ }
            },
            {
                new KeyValuePair<int, string>(111, "Read and React"),
                new Dictionary<KeyValuePair<string, string>, Dictionary<int, string>>{ }
            },
            {
                new KeyValuePair<int, string>(112, "Give Up"),
                new Dictionary<KeyValuePair<string, string>, Dictionary<int, string>>{ }
            },
            {
                new KeyValuePair<int, string>(113, "Delay Get Up"),
                new Dictionary<KeyValuePair<string, string>, Dictionary<int, string>>{ }
            },
            {
                new KeyValuePair<int, string>(114, "Pitch Key"),
                new Dictionary<KeyValuePair<string, string>, Dictionary<int, string>>{ }
            },
            {
                new KeyValuePair<int, string>(115, "Defensive Strafe"),
                new Dictionary<KeyValuePair<string, string>, Dictionary<int, string>>{ }
            },
            {
                new KeyValuePair<int, string>(116, "Defensive Skill Dive"),
                new Dictionary<KeyValuePair<string, string>, Dictionary<int, string>>{ }
            },
            {
                new KeyValuePair<int, string>(117, "Defensive Skill Tractor Beam"),
                new Dictionary<KeyValuePair<string, string>, Dictionary<int, string>>{ }
            },
            {
                new KeyValuePair<int, string>(118, "Non Ball Carrier Dive"),
                new Dictionary<KeyValuePair<string, string>, Dictionary<int, string>>{ }
            },
            {
                new KeyValuePair<int, string>(119, "On Field Refs"),
                new Dictionary<KeyValuePair<string, string>, Dictionary<int, string>>{ }
            },
            {
                new KeyValuePair<int, string>(120, "On Field Refs Animation"),
                new Dictionary<KeyValuePair<string, string>, Dictionary<int, string>>{ }
            },
            {
                new KeyValuePair<int, string>(121, "Multi Actor Interaction Offense"),
                new Dictionary<KeyValuePair<string, string>, Dictionary<int, string>>{ }
            },
            {
                new KeyValuePair<int, string>(122, "Multi Actor Interaction Defense"),
                new Dictionary<KeyValuePair<string, string>, Dictionary<int, string>>{ }
            },
            {
                new KeyValuePair<int, string>(123, "Multi Actor Get Into Position"),
                new Dictionary<KeyValuePair<string, string>, Dictionary<int, string>>{ }
            },
            {
                new KeyValuePair<int, string>(124, "Canned Animation"),
                new Dictionary<KeyValuePair<string, string>, Dictionary<int, string>>{ }
            },
            {
                new KeyValuePair<int, string>(125, "Canned Chuck"),
                new Dictionary<KeyValuePair<string, string>, Dictionary<int, string>>{ }
            },
            {
                new KeyValuePair<int, string>(126, "Run Fit Movement"),
                new Dictionary<KeyValuePair<string, string>, Dictionary<int, string>>{ }
            },
            {
                new KeyValuePair<int, string>(127, "Press"),
                new Dictionary<KeyValuePair<string, string>, Dictionary<int, string>>{ }
            },
            {
                new KeyValuePair<int, string>(128, "Block Stick Attempt"),
                new Dictionary<KeyValuePair<string, string>, Dictionary<int, string>>{ }
            },
            {
                new KeyValuePair<int, string>(129, "Animation Script NonScriptable"),
                new Dictionary<KeyValuePair<string, string>, Dictionary<int, string>>{ }
            },
            {
                new KeyValuePair<int, string>(130, "Scramble Test"),
                new Dictionary<KeyValuePair<string, string>, Dictionary<int, string>>{ }
            },
            {
                new KeyValuePair<int, string>(131, "Animation Script"),
                new Dictionary<KeyValuePair<string, string>, Dictionary<int, string>>{ }
            },
            {
                new KeyValuePair<int, string>(132, "Max"),
                new Dictionary<KeyValuePair<string, string>, Dictionary<int, string>>{ }
            }
        };

        public int rec { get; set; }
        public int val1 { get; set; }
        public int val2 { get; set; }
        public int val3 { get; set; }
        /// <summary>
        /// PLYS.PSAL
        /// </summary>
        public int psal { get; set; }
        public int code { get; set; }
        public int step { get; set; }
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
    }
}
