using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Graphic
{
    class SWSleep
    {
        long freq = 0;
        public SWSleep()
        {
            this.freq = Stopwatch.Frequency;
            USleep(0);
        }

        internal void USleep(int us)
        {
            double sec = (double)us / 1000000;
            Stopwatch sw = Stopwatch.StartNew();
            while (sw.ElapsedTicks / (double)freq < sec)
            {
            }
        }

        internal void MSleep(int ms)
        {
            double sec = (double)ms / 1000;
            Stopwatch sw = Stopwatch.StartNew();
            while (sw.ElapsedTicks / (double)freq < sec)
            {
            }
        }
    }
}