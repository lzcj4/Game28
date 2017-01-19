using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Game28UI.Http;
using Newtonsoft.Json.Linq;
using Game28UI.ViewModel;

namespace Game28UI
{
    /// <summary>
    /// Interaction logic for WinAnalyze.xaml
    /// </summary>
    public partial class WinAnalyze : Window
    {
        public WinAnalyze()
        {
            InitializeComponent();
            this.dtStart.SelectedDate = DateTime.Now;
            this.dtEnd.SelectedDate = DateTime.Now;

            this.Loaded += WinAnalyze_Loaded;
            this.btnSearch.Click += BtnSearch_Click;
        }
        private void WinAnalyze_Loaded(object sender, RoutedEventArgs e)
        {
            JObject json = new JObject();
            json["game"] = "crazy28";
            json["startdate"] = "2017-01-17 00:00:00";
            json["enddate"] = "2017-01-17 23:59:59";

            //GameHttp http = new GameHttp();
            //IList<RoundModel> list = http.GetGameRounds(json);
            //this.ucChart.SetData(list);
        }



        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            JObject json = new JObject();
            DateTime dtS = dtStart.SelectedDate.Value;
            DateTime dtE = dtEnd.SelectedDate.Value;

            dtE = dtE.AddHours(23);
            dtE = dtE.AddMinutes(59);
            dtE = dtE.AddSeconds(59);

            json["game"] = "crazy28";
            json["startdate"] = dtS.ToString();
            json["enddate"] = dtE.ToString();

            GameHttp http = new GameHttp();
            IList<RoundModel> list = http.GetGameRounds(json);
            this.ucChart.SetData(list);
        }

    }
}
