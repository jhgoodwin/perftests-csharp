using System;
using System.Reflection;

namespace PerfTest
{
    internal class PerfMethod
    {
        public string Name { get; set; }
        public MethodInfo Method { get; set; }
    }
}