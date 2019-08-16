using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Regiony
{
    public class Container
    {
        public Container(List<Rectangle> rectangles, double width, double height)
        {
            this.Rectangles = (new List<Rectangle>() {
                new Rectangle(-1, 0, 0, height,false),
                new Rectangle(width, 0, width+1, height,true),
                new Rectangle(0, -1, width, 0,true)
            }).Concat(rectangles).ToList();
            Width = width;
            Height = height;
            
        }
        private double Width { get; set; }
        private double Height { get; set; }


        private void Fill()
        {
            while (true)
            {
                
                var rect = Rectangles.FirstOrDefault(x => !x.Completed);
               
                if (rect == null)
                    return;

                var others = Rectangles.Where(x => x.ID != rect.ID).ToList();

                var topRight = rect.TopRight(others);
                if (topRight == null)
                    continue;

                var top = topRight.Item2;
                var left = topRight.Item1;
                
                var colidingRight = others.Where(x => x.Left > left && x.Top >= top && x.Bottom < top);
                var leftMost = colidingRight.OrderBy(x => x.Left).First();
                var right = leftMost.Left;

                var colidingBottom = others.Where(x => x.Top < top && ((x.Left < right && x.Left > left) || (x.Right < right && x.Right > left) || (right < x.Right && right > x.Left) || (left < x.Right && left > x.Left)));
                var highest = colidingBottom.OrderByDescending(x => x.Top).First();
                var bottom = highest.Top;
                Rectangles.Add(new Rectangle(left,bottom,right,top));

            }
        }


        private List<Rectangle> Rectangles { get; set; }

        public List<Rectangle> GetRectangles()
        {
            Fill();
            return Rectangles.Where(x => x.Left >= 0 && x.Right <= Width && x.Bottom >= 0 && x.Top <= Height).ToList();
        }
    }
}
