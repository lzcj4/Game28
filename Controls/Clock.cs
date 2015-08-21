using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Game28.Controls
{
    public class Clock : CircleControlBase
    {
        const int timeInterval = 60, hourInterval = 12, degreeInterval = 6;

        int hourWidth = 0, minuteWidth = 0, secondWidth = 0, radius = 0;

        /// <summary>
        /// SCALE ITEM WIDTH AND HEIGHT
        /// </summary>
        int scaleWidth = 10, scaleHeight = 10;
        int hour = 0, minute = 0, second = 0;
        Timer timer;

        public Clock()
        {
        }

        protected override void InitLayout()
        {
            DateTime dtNow = DateTime.Now;
            hour = dtNow.Hour;
            minute = dtNow.Minute;
            second = dtNow.Second;

            int range = 20;
            radius = this.Width / 2 - range * 2;
            secondWidth = radius - range;
            minuteWidth = secondWidth - range * 2;
            hourWidth = minuteWidth - range * 2;

            StartTimer();

            base.InitLayout();
        }

        private void StartTimer()
        {
            timer = new Timer();
            timer.Interval = 1 * 1000;
            timer.Tick += timer_Tick;
            timer.Start();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            if (++second % timeInterval == 0)
            {
                second = 0;
                if (++minute % timeInterval == 0)
                {
                    minute = 0;
                    if (++hour % hourInterval == 0)
                    {
                        hour = 0;
                    }
                }
            }
            this.Invalidate();
        }

        private void StopTimer()
        {
            if (null != timer)
            {
                timer.Stop();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            Graphics graphic = e.Graphics;

            DrawEdge(graphic);
            DrawHour(graphic);
            DrawMinute(graphic);
            DrawSecond(graphic);

            base.OnPaint(e);

            sw.Stop();
            Console.WriteLine(string.Format("/*****  Current clock draw elapsed:{0} ms *******/", sw.ElapsedMilliseconds));
        }


        private void DrawEdge(Graphics graphic)
        {
            Point centerPoint = GetCenterPoint();

            Pen pen = new Pen(Brushes.Black);
            graphic.DrawEllipse(pen, centerPoint.X - scaleWidth / 2, centerPoint.Y - scaleHeight / 2, scaleHeight, scaleHeight);
            graphic.FillEllipse(Brushes.Black, centerPoint.X - scaleWidth / 2, centerPoint.Y - scaleHeight / 2, scaleHeight, scaleHeight);

            Console.WriteLine(string.Format("/*  current point origin X:{0} , Y:{1} */", centerPoint.X, centerPoint.Y));
            pen = new Pen(Brushes.Black);

            for (int i = 0; i < timeInterval; i++)
            {
                int degree = i * degreeInterval;
                int x = centerPoint.X + GetSin(degree, radius);
                int y = centerPoint.Y - GetCos(degree, radius);

                Console.WriteLine(string.Format("current point location X:{0} , Y:{1}", x, y));
                Point newPoint = new Point(x, y);

                scaleWidth = scaleHeight = i % 5 == 0 ? 15 : 2;
                Point lastPoint = new Point(x + GetSin(degree, scaleWidth), y - GetCos(degree, scaleHeight));

                graphic.DrawLine(pen, lastPoint, newPoint);
            }
            Console.WriteLine(string.Format("/====  current point origin X:{0} , Y:{1} ====/", centerPoint.X, centerPoint.Y));
        }

        private void DrawPointer(Graphics graphic, int degree, int width, Pen pen)
        {
            Point centerPoint = GetCenterPoint();
            int x = centerPoint.X + GetSin(degree, width);
            int y = centerPoint.Y - GetCos(degree, width);
            Point newPoint = new Point(x, y);
            graphic.DrawLine(pen, centerPoint, newPoint);
        }

        private void DrawHour(Graphics graphic)
        {
            Pen pen = new Pen(Brushes.Black);
            int degree = hour * degreeInterval * 5 + 30 * minute / timeInterval;
            DrawPointer(graphic, degree, hourWidth, pen);
        }

        private void DrawMinute(Graphics graphic)
        {
            Pen pen = new Pen(Brushes.Blue);
            int degree = minute * degreeInterval;
            DrawPointer(graphic, degree, minuteWidth, pen);
        }

        private void DrawSecond(Graphics graphic)
        {
            Pen pen = new Pen(Brushes.Green);
            int degree = second * degreeInterval;
            DrawPointer(graphic, degree, secondWidth, pen);
        }
    }
}
