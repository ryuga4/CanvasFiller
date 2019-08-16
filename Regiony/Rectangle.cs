using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Regiony
{
    public class Rectangle
    {
        private static int IDCounter = 0;
        public int ID = IDCounter++;



        public Rectangle(double left, double bottom, double right, double top, bool completed = false)
        {
            this.Left = left;
            this.Right = right;
            this.Top = top;
            this.Bottom = bottom;
            this._topRight = Top;
            this.Completed = completed;
            this._color = null;
        }

        public Rectangle(double left, double bottom, double right, double top, Color color)
        {
            this.Left = left;
            this.Right = right;
            this.Top = top;
            this.Bottom = bottom;
            this._topRight = Top;
            this.Completed = false;
            this._color = color;
        }

        public double Left { get; private set; }
        public double Right { get; private set; }
        public double Top { get; private set; }
        public double Bottom { get; private set; }


        public double _topRight { get; private set; }

        public bool Completed { get; set; }
        private Color? _color { get; }

        public Tuple<double,double> TopRight (List<Rectangle> rectangles)
        {
            var topRight = _topRight; 
            var filtered = rectangles.Where(x => Right >= x.Left && Right < x.Right).ToList();
            if (!filtered.Any())
                return new Tuple<double, double>(Right, topRight);
            while (true)
            {
                var containingPoint = filtered.Where(x => topRight > x.Bottom && topRight <= x.Top);
                if (!containingPoint.Any())
                {
                    if (topRight > Bottom)
                    {
                        return new Tuple<double, double>(Right, topRight); 
                    } else
                    {
                        Completed = true;
                        return null;
                    }
                } else
                {
                    topRight = containingPoint.First().Bottom;
                }
            }

        }

        public System.Windows.Shapes.Rectangle ToWPFRectangle(Random rnd)
        {
            
            var result = new System.Windows.Shapes.Rectangle();
            result.Fill = new SolidColorBrush(_color != null ? _color.Value : Color.FromRgb((byte)rnd.Next(128,255), (byte)rnd.Next(128, 255), (byte)rnd.Next(128, 255)));
            result.Width = Right - Left;
            result.Height = Top - Bottom;
            result.Stroke = new SolidColorBrush(Colors.Black);
            return result;
        }







    }
}
