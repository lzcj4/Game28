using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Game28UI.ViewModel;
using System.Collections.Generic;

namespace Game28UI
{
    public class UCChart : Canvas
    {
        private const double lineWidth = 1;
        private const double pointRadius = 1.5;
        private const double flagHeight = 10;
        private const double offset = 5;
        private const string fontFace = "Segoe UI";

        private double yChartPadding = 20;
        private double xChartPadding = 20;
        private int midLowerBound = 10;
        private int midUpperBound = 17;
        int[] yFlags = { };
        int[] xFlags = { };
        IDictionary<int, IList<int>> dataDic = new Dictionary<int, IList<int>>();
        Pen[] linePens = {
                          new Pen(Brushes.Green, lineWidth), new Pen(Brushes.Blue, lineWidth),
                          new Pen(Brushes.Red, lineWidth), new Pen(Brushes.Aqua, lineWidth),
                          new Pen(Brushes.BlueViolet, lineWidth), new Pen(Brushes.Chartreuse, lineWidth),
                          new Pen(Brushes.Chocolate, lineWidth), new Pen(Brushes.DarkMagenta, lineWidth),
                          new Pen(Brushes.Yellow, lineWidth), new Pen(Brushes.YellowGreen, lineWidth),
                          new Pen(Brushes.Teal, lineWidth), new Pen(Brushes.Tomato, lineWidth),
                          new Pen(Brushes.Sienna, lineWidth), new Pen(Brushes.Indigo, lineWidth),
                          new Pen(Brushes.LightPink, lineWidth), new Pen(Brushes.SlateBlue, lineWidth)
        };

        Pen blackPen = new Pen(Brushes.Black, lineWidth);
        Pen redPen = new Pen(Brushes.Red, lineWidth);
        //Pen grayPen = new Pen(Brushes.LightGray, 1);
        Pen grayPen = new Pen(new SolidColorBrush(Color.FromArgb(80, 0, 0, 0)), lineWidth);
        Pen halfRedPen = new Pen(new SolidColorBrush(Color.FromArgb(120, 255, 0, 0)), lineWidth);

        bool isDebug = true;
        private int dayCount = 960;
        public int DayCount
        {
            get { return dayCount; }
            set { dayCount = value; }
        }

        public UCChart()
        {
            this.LoadTestData();
        }

        private void LoadTestData()
        {
            int xMax = 24;
            int yMax = 28;
            xFlags = new int[xMax];
            yFlags = new int[yMax];

            for (int i = 0; i < yMax; i++)
            {
                yFlags[i] = i;
                if (i < xMax)
                {
                    xFlags[i] = i + 1;
                }
            }

            Random r = new Random();
            dataDic[1] = new List<int>();
            dataDic[15] = new List<int>();
            dataDic[31] = new List<int>();
            for (int i = 0; i < xMax; i++)
            {
                dataDic[1].Add(r.Next(yMax));
                dataDic[15].Add(r.Next(yMax));
                dataDic[31].Add(r.Next(yMax));
            }
        }

        public void SetData(IList<RoundModel> list)
        {
            isDebug = false;
            dataDic.Clear();
            var groups = list.GroupBy(item => item.Date.Day);
            foreach (var group in groups)
            {
                IList<int> groupList = new List<int>();
                dataDic[group.Key] = groupList;
                foreach (var item in group)
                {
                    groupList.Add(item.Value);
                }
            }

            this.InvalidateVisual();
        }

        private FormattedText GetFormattedText(string str)
        {
            return GetFormattedText(str, Brushes.Black);
        }

        private FormattedText GetFormattedText(string str, Brush brush)
        {
            FormattedText formatText = new FormattedText(str, CultureInfo.CurrentUICulture,
                                                         FlowDirection.LeftToRight,
                                                         new Typeface(fontFace), 16, brush);
            return formatText;
        }

        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);
            if (dataDic.IsNullOrEmpty() || linePens.IsNullOrEmpty())
            {
                return;
            }

            FormattedText xMaxFT = GetFormattedText(xFlags.Max().ToString());
            FormattedText yMaxFT = GetFormattedText(yFlags.Max().ToString());

            xChartPadding = yMaxFT.Width > xChartPadding ? yMaxFT.Width + offset : xChartPadding;
            yChartPadding = xMaxFT.Height > yChartPadding ? xMaxFT.Height : yChartPadding;

            double canvasWidth = this.ActualWidth - xChartPadding * 2;
            double canvasHeight = this.ActualHeight - yChartPadding * 2 * 2;
            //2 yChartingPadding for Title

            double yTop = 3 * yChartPadding;
            double startXPos = xChartPadding;
            double startYPos = canvasHeight + yTop;

            FormattedText ftStart = GetFormattedText("0");
            dc.DrawText(ftStart, new Point(yChartPadding, startYPos - flagHeight / 4));

            //X-order flag 1-24小时
            dc.DrawLine(blackPen, new Point(xChartPadding, startYPos),
                                  new Point(xChartPadding + canvasWidth, startYPos));

            double xStep = canvasWidth / xFlags.Length;
            for (int i = 0; i < xFlags.Length; i++)
            {
                double startX = xChartPadding + (i + 1) * xStep;
                int h = xFlags[i];
                FormattedText ft = GetFormattedText(h.ToString(), Brushes.Black);
                dc.DrawLine(grayPen, new Point(startX, startYPos), new Point(startX, yTop));
                dc.DrawText(ft, new Point(startX - ft.Width / 2, startYPos - flagHeight / 4));
            }

            //Y-order flag 1-27, and 0 is excepted
            dc.DrawLine(blackPen, new Point(xChartPadding, startYPos), new Point(xChartPadding, yTop));
            double yStep = canvasHeight / (yFlags.Length - 1);
            for (int i = 1; i < yFlags.Length; i++)
            {
                double startY = i * yStep;
                Pen linePen = i >= midLowerBound && i <= midUpperBound ? halfRedPen : grayPen;
                Pen txtPen = i >= midLowerBound && i <= midUpperBound ? redPen : blackPen;
                FormattedText ft = GetFormattedText(yFlags[i].ToString(), txtPen.Brush);
                dc.DrawLine(linePen, new Point(xChartPadding, startYPos - startY),
                    new Point(canvasWidth + xChartPadding, startYPos - startY));
                dc.DrawText(ft, new Point(0, startYPos - startY - ft.Height / 2));
            }

            int j = 0;
            int yMax = yFlags.Max();
            //divide to the every value step
            yStep = canvasHeight / yMax;

            int l = 0;
            xStep = canvasWidth / (isDebug ? xFlags.Length : DayCount);
            foreach (int key in dataDic.Keys)
            {
                //Line
                j = 1;
                Pen currentPen = linePens[l++];
                Point lastPoint = new Point(0, 0);

                IList<int> dayList = dataDic[key];
                for (int z = 0; z < dayList.Count; z++, j++)
                {
                    Point newPoint = new Point(xChartPadding + j * xStep,
                                               startYPos - dayList[z] * yStep);
                    if (lastPoint.X == 0 && lastPoint.Y == 0)
                    {
                        lastPoint = newPoint;
                    }

                    dc.DrawLine(currentPen, lastPoint, newPoint);
                    dc.DrawEllipse(currentPen.Brush, currentPen, newPoint, pointRadius, pointRadius);

                    lastPoint = newPoint;
                }
            }

            if (!dataDic.IsNullOrEmpty() && !linePens.IsNullOrEmpty())
            {
                double xPos = canvasWidth / 3;
                double yPos = 1 * yChartPadding;
                int rect_len = 10;
                int title_margin = 5;
                l = 0;
                foreach (int key in dataDic.Keys)
                {
                    FormattedText ft = GetFormattedText(string.Format("{0}日", key), linePens[l].Brush);
                    dc.DrawText(ft, new Point(xPos, yPos));
                    xPos += ft.Width + 2;
                    dc.DrawRectangle(linePens[l].Brush, linePens[l], new Rect(xPos, yPos + 1.5 * offset, rect_len, rect_len));
                    xPos += rect_len + title_margin;
                    l++;
                }
            }
        }
    }
}
