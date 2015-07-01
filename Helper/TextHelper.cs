using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game28
{
    class TextHelper
    {
        public static string GetSubstring(string line, string startPattern, string endPattern)
        {
            string result = string.Empty;
            int startIndex = line.IndexOf(startPattern);
            if (startIndex > 0)
            {
                startIndex += startPattern.Length;
                int endIndex = line.IndexOf(endPattern);
                if (endIndex > 0 && endIndex > startIndex)
                {
                    result = line.Substring(startIndex, endIndex - startIndex);
                }
            }

            return result;
        }
    }
}
