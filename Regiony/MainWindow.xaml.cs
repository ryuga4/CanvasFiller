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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Regiony
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var rnd = new Random();

            var initRectangles = new List<Rectangle>()
            {
                new Rectangle(100,100,350,150, Colors.Black),
                new Rectangle(300,150,350,400, Colors.Black),
                new Rectangle(100,350,300,400, Colors.Black),
                new Rectangle(100,250,150,350, Colors.Black),
                new Rectangle(100,200,250,250, Colors.Black),
                new Rectangle(200,250,250,300, Colors.Black),
            };


            var cont = new Container(initRectangles, MainCanvas.Width, MainCanvas.Height);
            
            var rectangles = cont.GetRectangles();
            foreach (var item in rectangles)
            {
                var wpf = item.ToWPFRectangle(rnd);
                MainCanvas.Children.Add(wpf);
                Canvas.SetTop(wpf, Height - item.Top);
                Canvas.SetLeft(wpf, item.Left);
            }
        }
    }
}
