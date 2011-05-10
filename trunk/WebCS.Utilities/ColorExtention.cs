using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace WebCS.Utilities
{
    public static class ColorExtention
    {
        public static Color RandomColor()
        {
            Random rand = new Random();
            int randRed = rand.Next(255);
            int randGreen = rand.Next(255);
            int randBlue = rand.Next(255);
            return Color.FromArgb(randRed, randGreen, randBlue);
        }
    }
}
