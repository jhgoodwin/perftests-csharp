using System.Diagnostics;

namespace PerfTest
{
    internal class PerfTiming
    {
        public PerfMethod PerfMethod { get; set; }
        public int Repetitions { get; set; }
        public long ElapsedTicks { get; set; }
        public long TickFrequency { get; set; }

        private const int MilliSecondsPerSecond = 1000;
        private const int MicroSecondsPerSecond = 1000 * MilliSecondsPerSecond;
        private const int NanoSecondsPerSecond = 1000 * MicroSecondsPerSecond;
        private double TicksPerNanoSecond => (double)TickFrequency / NanoSecondsPerSecond;
        private double TicksPerMicroSecond => (double)TickFrequency / MicroSecondsPerSecond;
        private double TicksPerMilliSecond => (double)TickFrequency / MilliSecondsPerSecond;
        public double GetElapsedNanoSeconds() => ElapsedTicks / TicksPerNanoSecond;
        public double GetElapsedMicroSeconds() => ElapsedTicks / TicksPerMicroSecond;
        public double GetElapsedMilliSeconds() => ElapsedTicks / TicksPerMilliSecond;
    }
}