using Game28UI.Http;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace Game28UI.ViewModel
{
    public class GameItem
    {
        public string Title { get; set; }
        public string Name { get; set; }

        public GameItem(string title, string name)
        {
            this.Title = title;
            this.Name = name;
        }

        public static IList<GameItem> GetAllGames()
        {
            IList<GameItem> list = new List<GameItem>();
            list.Add(new GameItem("疯狂28", "crazy28"));
            list.Add(new GameItem("韩国28", "korea28"));
            list.Add(new GameItem("PC28", "PC28"));
            list.Add(new GameItem("极速16", "speed16"));
            return list;
        }
    }

    public class RoundEventArgs<T> : EventArgs
    {
        public T Args { get; private set; }
        public RoundEventArgs(T args)
        {
            this.Args = args;
        }
    }

    public class AnalyzeViewModel : ViewModelBase
    {
        public event EventHandler<RoundEventArgs<IList<RoundModel>>> LoadEvent;
        
        public ICollectionView GameView { get; set; }
        public GameItem CurrentGame
        {
            get { return this.GameView.CurrentItem as GameItem; }
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

        private DateTime endDate = DateTime.Now;
        public DateTime EndDate
        {
            get { return this.endDate; }
            set
            {
                this.SetProperty(ref this.endDate, value, "EndDate");
            }
        }

        public ICommand QueryCommand
        {
            get
            {
                return new GenericCommand()
                {
                    CanExecuteCallback = (obj) => { return true; },
                    ExecuteCallback = (obj) => { this.Query(); }
                };
            }
        }
        public AnalyzeViewModel()
        {
            this.GameView = CollectionViewSource.GetDefaultView(GameItem.GetAllGames());
        }

        public void Query()
        {
            JObject json = new JObject();
            DateTime dtE = this.EndDate.Date.AddHours(23).AddMinutes(59).AddSeconds(59);
            string dateFormat = "yyyy-MM-dd HH:mm:ss";
            json["game"] = CurrentGame.Name;
            json["startdate"] = this.StartDate.Date.ToString(dateFormat);
            json["enddate"] = dtE.ToString(dateFormat);

            Task.Run(() =>
            {
                GameHttp http = new GameHttp();
                IList<RoundModel> list = http.GetGameRounds(json);
                if (LoadEvent != null)
                {
                    this.RunOnUIThreadAsync(() =>
                    {
                        this.LoadEvent(this, new RoundEventArgs<IList<RoundModel>>(list));
                    });
                }
            });

        }
    }
}
