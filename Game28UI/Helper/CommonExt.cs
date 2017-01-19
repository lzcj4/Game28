﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game28UI
{
    static class CommonExt
    {
        public static bool IsNull(this object obj)
        {
            bool result = obj == null;
            return result;
        }

        public static bool IsNullOrEmpty(this string str)
        {
            bool result = String.IsNullOrEmpty(str);
            return result;
        }

        public static bool IsNullOrEmpty<T>(this IEnumerable<T> list)
        {
            bool result = list == null || !list.Any();
            return result;
        }


        /// <summary>
        /// same as string.StartsWith, IgnoreCase
        /// </summary>
        /// <param name="strA"></param>
        /// <param name="strB"></param>
        /// <returns></returns>
        public static bool IsStartsWith(this string strA, string strB)
        {
            if (string.IsNullOrEmpty(strA) || string.IsNullOrEmpty(strB)) return false;
            return strA.StartsWith(strB, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// same as string.contains, IgnoreCase
        /// </summary>
        /// <param name="sourceValue"></param>
        /// <param name="testValue"></param>
        /// <returns></returns>
        public static bool IsContains(this string sourceValue, string testValue)
        {
            if (string.IsNullOrEmpty(sourceValue) || string.IsNullOrEmpty(testValue)) return false;
            return sourceValue.ToUpperInvariant().Contains(testValue.ToUpperInvariant());
        }

        public static bool EqualIgnoreCase(this string stringA, string stringB)
        {
            bool result = false;
            if (stringA.IsNullOrEmpty() && stringB.IsNullOrEmpty())
            {
                result = true;
            }

            if (!stringA.IsNullOrEmpty() && !stringB.IsNullOrEmpty())
            {
                result = stringA.Equals(stringB, StringComparison.InvariantCultureIgnoreCase);
            }
            return result;
        }

        public static bool StartWithIgnoreCase(this string stringA, string stringB)
        {
            bool result = false;
            if (stringA.IsNullOrEmpty() && stringB.IsNullOrEmpty())
            {
                result = true;
            }

            if (!stringA.IsNullOrEmpty() && !stringB.IsNullOrEmpty())
            {
                result = stringA.StartsWith(stringB, StringComparison.InvariantCultureIgnoreCase);
            }
            return result;
        }
    }
}
