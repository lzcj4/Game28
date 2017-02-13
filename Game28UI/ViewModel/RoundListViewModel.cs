using Game28UI.Http;
using Newtonsoft.Json.Linq;
using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace Game28UI.ViewModel
{
    public class RoundListViewModel : ViewModelBase
    {
        #region Properties 
        
        public ICollectionView GameView { get; set; }
        public GameItem CurrentGame
        {
            get { return this.GameView.CurrentItem as GameItem; }
        }

        private ICollectionView roundView;
        public ICollectionView RoundView
        {
            get { return roundView; }
            set { this.SetProperty(ref roundView, value, "RoundView"); }
        }

        private DateTime startDate = DateTime.Now;
        public DateTime StartDate
        {
            get { return this.startDate; }
            set
            {
                this.SetProperty(ref this.startDate, value, "StartDate");
            }
        }

        private int countDan = 0;
        public int CountDan
        {
            get { return this.countDan; }
            set
            {
                this.SetProperty(ref this.countDan, value, "CountDan");
            }
        }

        private int countShuang = 0;
        public int CountShuang
        {
            get { return this.countShuang; }
            set
            {
                this.SetProperty(ref this.countShuang, value, "CountShuang");
            }
        }

        private int countZhong = 0;
        public int CountZhong
        {
            get { return this.countZhong; }
            set
            {
                this.SetProperty(ref this.countZhong, value, "CountZhong");
            }
        }

        private int countBian = 0;
        public int CountBian
        {
            get { return this.countBian; }
            set
            {
                this.SetProperty(ref this.countBian, value, "CountBian");
            }
        }

        private int countDa = 0;
        public int CountDa
        {
            get { return this.countDa; }
            set
            {
                this.SetProperty(ref this.countDa, value, "CountDa");
            }
        }

        private int countXiao = 0;
        public int CountXiao
        {
            get { return this.countXiao; }
            set
            {
                this.SetProperty(ref this.countXiao, value, "CountXiao");
            }
        }

        #endregion

        public ICommand QueryCommand
        {
            get
            {
                return new GenericCommand()
                {
                    CanExecuteCallback = (obj) => { return true; },
                    ExecuteCallback = (obj) => { Task.Run(() => this.GetRounds()); }
                };
            }
        }
        
        public RoundListViewModel()
        {
            this.GameView = CollectionViewSource.GetDefaultView(GameItem.GetAllGames());
        }

        private void ResetCount()
        {
            this.CountDan = 0;
            this.CountShuang = 0;
            this.CountBian = 0;
            this.CountZhong = 0;
            this.CountDa = 0;
            this.CountXiao = 0;
        }

        private void GetRounds()
        {
            JObject json = new JObject();
            DateTime dtS = this.StartDate.Date;
            DateTime dtE = this.StartDate.Date;
            string dateFormat = "yyyy-MM-dd HH:mm:ss";
            dtE = dtE.AddHours(23).AddMinutes(59).AddSeconds(59);

            json["game"] = this.CurrentGame.Name;
            json["startdate"] = dtS.ToString(dateFormat);
            json["enddate"] = dtE.ToString(dateFormat);

            GameHttp http = new GameHttp();
            var list = http.GetGameRounds(json).OrderBy(item => item.ID);

            this.RoundView = CollectionViewSource.GetDefaultView(list);
            foreach (var item in list)
            {
                this.CountDan = item.IsDan ? ++this.countDan : this.countDan;
                this.CountShuang = !item.IsDan ? ++this.countShuang : this.countShuang;
                this.CountZhong = item.IsZhong ? ++this.countZhong : this.countZhong;
                this.CountBian = !item.IsZhong ? ++this.countBian : this.countBian;
                this.CountXiao = item.IsXiao ? ++this.countXiao : this.countXiao;
                this.CountDa = !item.IsXiao ? ++this.countDa : this.countDa;
            }
        }
    }
}
