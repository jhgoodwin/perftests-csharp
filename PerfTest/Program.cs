using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace PerfTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var methods = GetPerfMethods();
            var timings = methods.Select(GetPerfTiming);
            Print(timings);
        }

        private static void Print(IEnumerable<PerfTiming> timings)
        {
            foreach(var timing in timings)
            {
                PrintTiming(timing);
            }
        }

        private static void PrintTiming(PerfTiming timing)
        {
            Console.WriteLine("---");
            Console.WriteLine($"Test Name: {timing.PerfMethod.Name}");
            double time = timing.GetElapsedNanoSeconds() / timing.Repetitions;
            Console.WriteLine($"Average Time (ns): {time}");
        }

        private static PerfTiming GetPerfTiming(PerfMethod method)
        {
            var iterations = GetPerfTestMethodAttribute(method.Method).Iterations;
            var timer = new Stopwatch();
            // warmup method
            method.Method.Invoke(null, null);
            timer.Start();
            for (var i = 0; i < iterations; i++)
            {
                method.Method.Invoke(null, null);
            }
            timer.Stop();
            return new PerfTiming
            {
                ElapsedTicks = timer.ElapsedTicks,
                PerfMethod = method,
                Repetitions = iterations,
                TickFrequency = Stopwatch.Frequency
            };
        }

        private static PerfTestMethodAttribute GetPerfTestMethodAttribute(MethodInfo method)
            => method.GetCustomAttributes(typeof(PerfTestMethodAttribute), false)
                .Cast<PerfTestMethodAttribute>()
                .FirstOrDefault();

        private static IEnumerable<PerfMethod> GetPerfMethods()
            => AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .SelectMany(t => t.GetMethods())
                .Where(m => GetPerfTestMethodAttribute(m) != null)
                .Select(m => {
                    var attr = GetPerfTestMethodAttribute(m);
                    return new PerfMethod {Name = attr.Name, Method = m };
                });
    }
}
