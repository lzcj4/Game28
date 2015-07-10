using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game28
{
    class TextHelper
    {
        /// <summary>
        /// Get sub string exception the start and end pattern
        /// </summary>
        /// <param name="line"></param>
        /// <param name="startPattern"></param>
        /// <param name="endPattern"></param>
        /// <returns></returns>
        public static string GetSubstring(string line, string startPattern, string endPattern)
        {
            string result = string.Empty;
            int startIndex = line.IndexOf(startPattern);
            if (startIndex >= 0)
            {
                startIndex += startPattern.Length;
                int endIndex = line.IndexOf(endPattern, startIndex);
                if (endIndex > 0 && endIndex > startIndex)
                {
                    result = line.Substring(startIndex, endIndex - startIndex);
                }
            }

            return result;
        }

        /// <summary>
        /// Get sub string exception the start and end pattern from the end
        /// </summary>
        /// <param name="line"></param>
        /// <param name="startPattern"></param>
        /// <param name="endPattern"></param>
        /// <returns></returns>
        public static string GetSubstringByEnd(string line, string startPattern, string endPattern)
        {
            string result = string.Empty;
            int startIndex = line.LastIndexOf(startPattern);
            if (startIndex > 0)
            {
                startIndex += startPattern.Length;
                int endIndex = line.IndexOf(endPattern, startIndex);
                if (endIndex > 0 && endIndex > startIndex)
                {
                    result = line.Substring(startIndex, endIndex - startIndex);
                }
            }

            return result;
        }
    }
}
