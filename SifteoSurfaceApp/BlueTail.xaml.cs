using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Artefact.Animation;
using System.Windows.Media.Effects;

namespace SifteoSurfaceApp
{
    /// <summary>
    /// Interaction logic for BlueTail.xaml
    /// </summary>
    public partial class BlueTail : Canvas
    {


        private Random rnd;
        private DispatcherTimer _timer = new DispatcherTimer();

        private System.Windows.Media.SolidColorBrush tailColor = new SolidColorBrush(new Color { A = 255, B = (byte)247, G = (byte)202, R = (byte)9 });

        public TagVisualization1 myParent;
        public SurfaceWindow1 sw1;

        public BlueTail()
        {
            InitializeComponent();

            rnd = new Random();

            _timer = new DispatcherTimer();
            _timer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            _timer.Tick += CreateTail;
            _timer.Start();
        }

        private void CreateTail(object sender, EventArgs e)
        {
            Point pt = new Point(22, 75);
            var particle = CreateParticle(pt.X, pt.Y);

            pt.X += rnd.NextDouble() * 100 * (rnd.NextDouble() < .5 ? 1 : -1);
            pt.Y += rnd.NextDouble() * 100 * (rnd.NextDouble() < .5 ? 1 : -1);

            particle.AutoAlphaTo(0, .5, AnimationTransitions.CubicEaseIn, 0);
            particle.SlideTo(pt.X, pt.Y, .5, AnimationTransitions.QuadEaseOut, 0).Complete += (eo, p) => this.Children.Remove(particle);
        }

        public Ellipse CreateParticle(double x, double y)
        {
            //var particle = new Ellipse { Width = 10, Height = 10, Fill = (Brush)Resources["BrushWhite"], Stroke = (Brush)Resources["BrushBlack"] };
            Ellipse particle = new Ellipse();
            particle.Width = 10;
            particle.Height = 10;
            particle.Fill = tailColor;
            Canvas.SetLeft(particle, x);
            Canvas.SetTop(particle, y);
            this.Children.Add(particle);
            return particle;
        }



        public void ChangeTailColor(SolidColorBrush solidColorBrush)
        {
            tailColor = solidColorBrush;
        }

    }
}
