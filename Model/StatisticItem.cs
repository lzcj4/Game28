
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game28.Model
{
    public class StatisticItem
    {
        public StatisticItem(string name, bool isNeedPercent = true)
        {
            this.Name = name;
            this.IsNeedPercent = isNeedPercent;
        }

        private bool IsNeedPercent { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }

        public double Percent { get; set; }
        public string PercentStr
        {
            get
            {
                return Count == 0 || !IsNeedPercent ? string.Empty : string.Format("{0}%", Percent * 100);
            }
        }
    }
}
