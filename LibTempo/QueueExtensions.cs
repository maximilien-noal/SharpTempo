using System.Collections.Generic;
using System.Linq;

namespace LibTempo
{
    internal static class QueueExtensions
    {
        public static bool IsEmpty<T>(this Queue<T> queue) => queue.Count == 0;

        public static T Back<T>(this Queue<T> queue) => queue.Last();

        public static T Front<T>(this Queue<T> queue) => queue.First();
    }
}