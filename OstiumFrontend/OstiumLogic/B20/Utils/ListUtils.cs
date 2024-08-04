using System.Collections.Generic;

namespace B20.Logic.Utils
{
    public class ListUtils
    {
        public static List<T> ListOf<T>(params T[] items)
        {
            return new List<T>(items);
        }
    }
}