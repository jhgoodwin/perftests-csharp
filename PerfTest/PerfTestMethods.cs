using System.Runtime.CompilerServices;
namespace PerfTest
{
    internal static class PerfTestMethods
    {
        [PerfTestMethod("NonInlined", 1_000_000)]
        public static void NonInlined ()
        {
            var sum = 0;
            for (var i=0; i < 10; i++)
            {
                sum += NonInlinedInternal();
            }
        }
        private static int NonInlinedInternal()
        {
            return "one".Length + "two".Length + "three".Length +
                "four".Length + "five".Length + "six".Length +
                "seven".Length + "eight".Length + "nine".Length +
                "ten".Length;
        }
        [PerfTestMethod("AggressiveInlined", 1_000_000)]
        public static void AggressiveInlined ()
        {
            var sum = 0;
            for (var i=0; i < 10; i++)
            {
                sum += AggressiveInlinedInternal();
            }
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int AggressiveInlinedInternal()
        {
            return "one".Length + "two".Length + "three".Length +
                "four".Length + "five".Length + "six".Length +
                "seven".Length + "eight".Length + "nine".Length +
                "ten".Length;
        }    }    
}