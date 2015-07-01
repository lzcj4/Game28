using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Game28
{
    class RuleFileHelper
    {
        public const string Speed28RuleFile = "speed28rule.txt";
        public static void SaveSpeed28Rule(int roundId, int[] values)
        {
            string path = GetSpeed28RuleFilePath();
            string valueStr = string.Empty;
            for (int i = 0; i < values.Length; i++)
            {
                if (i == values.Length - 1)
                {
                    valueStr += values[i].ToString();
                }
                else
                {
                    valueStr += values[i].ToString() + ",";
                }
            }

            if (File.Exists(path))
            {
                File.Delete(path);
            }
            string result = string.Format("{0}:{1}", roundId, valueStr);
            using (Stream stream = new FileStream(path, FileMode.OpenOrCreate))
            {
                using (StreamWriter sw = new StreamWriter(stream))
                {
                    sw.Write(result);
                }
            }
        }

        public static string GetSpeed28RuleFilePath()
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Speed28RuleFile);
            return path;
        }

        public static Stream GetSpeed28RuleFileStream()
        {
            string path = GetSpeed28RuleFilePath();
            FileStream stream = new FileStream(path, FileMode.Open);
            return stream;
        }
    }
}
