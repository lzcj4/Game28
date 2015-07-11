using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game28.Model
{
    public class HistoryInfo
    {
        /// <summary>
        /// 期号
        /// </summary>
        public string RoundId { get; set; }

        /// <summary>
        /// 当前结果值
        /// </summary>
        public int Result { get; set; }
        /// <summary>
        /// 赌注
        /// </summary>
        public long Stake { get; set; }

        /// <summary>
        /// 当前竞后结果: 赌注+利润
        /// </summary>
        public long Amount { get; set; }

        /// <summary>
        /// 日期
        /// </summary>
        public string Date { get; set; }
        public string Time { get { return Date.Replace(DateTime.Now.Year.ToString() + "-", ""); } }

        /// <summary>
        /// 总下注U豆
        /// </summary>
        public long TotalAmount { get; set; }
        public string TotalAmountStr { get { return this.TotalAmount.ToString("N0"); } }

        /// <summary>
        /// 赢的人数
        /// </summary>
        public int WinnerNum { get; set; }

        public string WinAmountStr { get { return (this.Amount - this.Stake).ToString("N0"); } }

        public string OddStr
        {
            get
            {
                return (Result % 2 == 1) ? "单" : string.Empty;
            }
        }
        public string EvenStr
        {
            get
            {
                return (Result % 2 == 0) ? "双" : string.Empty;
            }
        }
        public string MiddleStr
        {
            get
            {
                return (Result >= 10 && Result <= 17) ? "中" : string.Empty;
            }
        }
        public string EdgeStr
        {
            get
            {
                return !(Result >= 10 && Result <= 17) ? "边" : string.Empty;
            }
        }
        public string BigStr
        {
            get
            {
                return (Result >= 14) ? "大" : string.Empty;
            }
        }

        public string SmallStr
        {
            get
            {
                return (Result < 14) ? "小" : string.Empty;
            }
        }


        public override string ToString()
        {
            string result = string.Format("期号:{0},开奖时间:{1},竞猜结果:{2},U豆总数:{3},中奖人数:{4},收入:{5},投入:{6},净赚:{7}",
                this.RoundId, this.Date, this.Result, this.TotalAmount.ToString("N0"),
                this.WinnerNum.ToString("N0"), this.Amount.ToString("N0"), this.Stake.ToString("N0"),
                (this.Amount - this.Stake).ToString("N0"));
            return result;
        }
    }
}
