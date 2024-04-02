using System;
using System.Drawing;

namespace Madden.Team
{
    [Serializable]
    public class TeamColors
    {
        public string Color1 { get; set; }
        public string Color2 { get; set; }
        public string Color3 { get; set; }
        public string Color4 { get; set; }
        public string Color5 { get; set; }

        public TeamColors(string color1)
        {
            Color1 = color1;
        }

        public TeamColors(string color1, string color2)
        {
            Color1 = color1;
            Color2 = color2;
        }

        public TeamColors(string color1, string color2, string color3)
        {
            Color1 = color1;
            Color2 = color2;
            Color3 = color3;
        }

        public TeamColors(string color1, string color2, string color3, string color4)
        {
            Color1 = color1;
            Color2 = color2;
            Color3 = color3;
            Color4 = color4;
        }

        public TeamColors(string color1, string color2, string color3, string color4, string color5)
        {
            Color1 = color1;
            Color2 = color2;
            Color3 = color3;
            Color4 = color4;
            Color5 = color5;
        }
    }
}
