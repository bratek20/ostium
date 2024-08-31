using System;
using System.Collections.Generic;
using B20.Ext;

namespace B20.Logic.Utils
{
    public class ListUtils
    {
        public static List<T> Of<T>(params T[] items)
        {
            return new List<T>(items);
        }
        
        public static Optional<T> Find<T>(List<T> list, Predicate<T> predicate)
        {
            foreach (var item in list)
            {
                if (predicate(item))
                {
                    return Optional<T>.Of(item);
                }
            }
            return Optional<T>.Empty();
        }
    }
}