using System;

namespace PerfTest
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false)]
    public class PerfTestMethodAttribute : Attribute
    {
        public string Name { get; private set; }
        public int Iterations { get; private set; }
        public PerfTestMethodAttribute (string name, int iterations)
        {
            Name = name;
            Iterations = iterations;
        }
    }
}