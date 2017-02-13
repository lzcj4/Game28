using Game28UI.Http;
using Game28UI.ViewModel;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Windows;

namespace Game28UI
{

    /// <summary>
    /// Interaction logic for WinAnalyze.xaml
    /// </summary>
    public partial class WinAnalyze : Window
    {
        private AnalyzeViewModel ViewModel { get; set; }

        public WinAnalyze()
        {
            InitializeComponent();

            this.btnZoomIn.Click += BtnZoomIn_Click;
            this.btnZoomOut.Click += BtnZoomOut_Click;
            this.ucChart.Rounds = 40 * 24;

            this.ViewModel = new AnalyzeViewModel();
            this.DataContext = this.ViewModel;
            this.ViewModel.LoadEvent += (sender, e) => { this.ucChart.SetData(e.Args); };
            //this.ucChart.Rounds = 12 * 15;
            //this.ucChart.TotalHours = 24 - 9;
            //this.ucChart.StartHour = 9;

            this.Loaded += (sender, e) => { this.ViewModel.Query(); };
        }


        float currentZoom = 1.0f;
        float zoominRate = 1.1f;
        float zoomoutRate = 0.9f;

        private void BtnZoomOut_Click(object sender, RoutedEventArgs e)
        {
            this.currentZoom *= zoomoutRate;
            this.Zoom(zoomoutRate);
        }

        private void BtnZoomIn_Click(object sender, RoutedEventArgs e)
        {
            this.currentZoom *= zoominRate;
            this.Zoom(zoominRate);
        }

        private void Zoom(float rate)
        {
            this.ucChart.Width = this.ucChart.ActualWidth * rate;
            this.ucChart.Height = this.ucChart.ActualHeight * rate;
            this.txtZoom.Text = string.Format("当前倍率:{0:f2}", this.currentZoom);
        }

    }
}
