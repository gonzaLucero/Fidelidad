using System;
using System.Collections.Generic;

namespace Hexacta.Core.Tools.Utilities
{
    /// <summary>
    /// Extensions for <see cref="System.Collections.Generic.IEnumerable"/>.
    /// </summary>
    public static class IEnumerableOfTExtensions
    {
        public static void ForEachInEnumerable<TEnumerable>(this IEnumerable<TEnumerable> enumerable, Action<TEnumerable> action)
        {
            foreach (var item in enumerable)
            {
                action(item);
            }
        }
    }
}
