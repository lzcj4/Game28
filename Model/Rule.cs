using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game28
{
    class Rule
    {
        public string Name { get; set; }
        public int[] Values { get; set; }

        public string ValuesStr
        {
            get
            {
                string valueStr = string.Empty;
                for (int i = 0; i < Values.Length; i++)
                {
                    if (i == Values.Length - 1)
                    {
                        valueStr += Values[i].ToString();
                    }
                    else
                    {
                        valueStr += Values[i].ToString() + ",";
                    }
                }
                return valueStr;
            }
        }

        public Rule(string name, int[] values)
        {
            if (string.IsNullOrEmpty(name) || values == null || values.Length != Speed28.MaxLen)
            {
                throw new ArgumentException();
            }

            Name = name;
            Values = values;
        }

        public override string ToString()
        {
            string valueStr = string.Empty;
            for (int i = 0; i < Values.Length; i++)
            {
                if (i == Values.Length - 1)
                {
                    valueStr += Values[i].ToString();
                }
                else
                {
                    valueStr += Values[i].ToString() + ",";
                }
            }

            string result = string.Format("{0}:{1};", Name, valueStr);
            return result;
        }
    }
}
