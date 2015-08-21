using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Game28.Controls
{
    public class CircleControlBase : Control
    {
        protected double GetDegreeToRadian(int degress)
        {
            return degress * Math.PI / 180;
        }

        protected int GetSin(int degree, int len)
        {
            double r = GetDegreeToRadian(degree);
            return (int)(Math.Sin(r) * len);
        }

        protected int GetCos(int degree, int len)
        {
            double r = GetDegreeToRadian(degree);
            return (int)(Math.Cos(r) * len);
        }

        protected Point GetCenterPoint()
        {
            int originX = this.Bounds.Width / 2;
            int originY = this.Bounds.Height / 2;
            Point centerPoint = new Point(originX, originY);
            return centerPoint;
        }
    }

  
}
