using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace Counter
{
    // Calculate time per iteration in nanoseconds
    public class QueryPerfCounter
    {
        [DllImport("KERNEL32")]
        private static extern bool QueryPerformanceCounter(
          out long lpPerformanceCount);

        [DllImport("Kernel32.dll")]
        private static extern bool QueryPerformanceFrequency(out long lpFrequency);

        private double fps;
        private double period;
        private long start;
        private long stop;
        private long frequency;
        Decimal multiplier = new Decimal(1.0e9);

        public string FPSstring { get { return fps.ToString("0.00")+" FPS"; } }
        public double DurationInMS { get { return period/1000000.0; } } //in mseconds

        public QueryPerfCounter()
        {
            if (QueryPerformanceFrequency(out frequency) == false)
            {
                // Frequency not supported
                throw new Win32Exception();
            }
        }

        public void Start()
        {
            QueryPerformanceCounter(out start);
        }

        public void Stop()
        {
            QueryPerformanceCounter(out stop);
        }

        public double Duration(int iterations)
        {
            period = ((((double)(stop - start) * (double)multiplier) / (double)frequency) / iterations);
            fps = 1000000000 / period;
            return period;
        }

        public double DurationPerIteration()
        {
            period = (((double)(stop - start) * (double)multiplier) / (double)frequency);
            fps = 1000000000 / period;
            return period;
        }
    }
}