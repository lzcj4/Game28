using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Game28.Controls
{
    public class Ring : CircleControlBase
    {
        int outerWidth, innerWidth;
        protected override void InitLayout()
        {
            outerWidth = this.Width - 20;
            innerWidth = outerWidth - 20;
            base.InitLayout();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            Graphics graphic = e.Graphics;
            Point centerPoint = GetCenterPoint();
            Pen pen = new Pen(Brushes.Black);
            Pen penR = new Pen(Brushes.Red);
            Pen penG = new Pen(Brushes.Green);
            Pen penB = new Pen(Brushes.Blue);

            graphic.DrawEllipse(penR, centerPoint.X - 10 - outerWidth / 2, centerPoint.Y - 10 - outerWidth / 2, outerWidth, outerWidth);
            graphic.DrawEllipse(penB, centerPoint.X - innerWidth / 2, centerPoint.Y - innerWidth / 2, innerWidth, innerWidth);

            base.OnPaint(e);

            sw.Stop();
            Console.WriteLine(string.Format("/*****  Current clock draw elapsed:{0} ms *******/", sw.ElapsedMilliseconds));
        }
    }
}
