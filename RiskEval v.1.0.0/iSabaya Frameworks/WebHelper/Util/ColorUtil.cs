using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace WebHelper.Util
{
    public class ColorUtil
    {
        public static IList<Color> GenerateGradientColor(int size)
        {
            var colorList = new List<Color>();

            int rMax = Color.FromArgb(255, 36, 0, 3).R;
            int rMin = Color.Red.R;
            int gMax = Color.FromArgb(255, 36, 0, 3).G;
            int gMin = Color.Red.G;
            int bMax = Color.FromArgb(255, 36, 0, 3).B;
            int bMin = Color.Red.B;

            for (var i = 0; i < size; i++)
            {
                var rAverage = rMin + (rMax - rMin) * i / size;
                var gAverage = gMin + (gMax - gMin) * i / size;
                var bAverage = bMin + (bMax - bMin) * i / size;

                colorList.Add(Color.FromArgb(rAverage, gAverage, bAverage));
            }

            return colorList;
        }
    }
}