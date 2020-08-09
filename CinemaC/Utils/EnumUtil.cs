using System;
using System.Collections.Generic;
using System.Linq;

namespace CinemaC.Utils
{
    public static class EnumUtil
    {
        public static IEnumerable<T> GetValue<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }
    }
}