using System;
using System.Collections.Generic;
using System.Text;
using Game28.Properties;

namespace Game28
{
    class AppSetting
    {
        static Settings appSetting;
        static AppSetting()
        {
            appSetting = Properties.Settings.Default;
        }

        public static void Save()
        {
            appSetting.Save();
        }

        public static string User
        {
            get { return appSetting.user; }
            set { appSetting.user = value; }
        }

        public static string Pwd
        {
            get { return appSetting.pwd; }
            set { appSetting.pwd = value; }
        }

        public static string Interval
        {
            get { return appSetting.interval; }
            set { appSetting.interval = value; }
        }

        public static int MaxLimit
        {
            get { return appSetting.MaxLimit; }
            set { appSetting.MaxLimit = value; }
        }

        public static string Rules
        {
            get { return appSetting.rules; }
            set { appSetting.rules = value; }
        }

        public static IList<Rule> GetRules()
        {
            IList<Rule> result = new List<Rule>();

            string rules = Rules;
            if (string.IsNullOrEmpty(rules))
            {
                return result;
            }

            string[] items = rules.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var item in items)
            {
                string[] r = item.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                if (r.Length == 2)
                {
                    string[] numStrs = r[1].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    if (numStrs.Length != Speed28.MaxLen)
                    {
                        continue;
                    }

                    string name = r[0];
                    int[] values = new int[Speed28.MaxLen];
                    for (int i = 0; i < Speed28.MaxLen; i++)
                    {
                        values[i] = int.Parse(numStrs[i]);
                    }

                    Rule rule = new Rule(name, values);
                    result.Add(rule);
                }
            }
            return result;
        }

        public static void SaveRules(IList<Rule> rules)
        {
            if (rules == null)
            {
                return;
            }
            StringBuilder sb = new StringBuilder();
            foreach (var item in rules)
            {
                sb.Append(item.ToString());
            }
            Rules = sb.ToString();
        }
    }
}
