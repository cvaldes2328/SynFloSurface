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
using Microsoft.Surface;
using Microsoft.Surface.Presentation;
using Microsoft.Surface.Presentation.Controls;
using Artefact.Animation;

namespace SifteoSurfaceApp
{
    /// <summary>
    /// Interaction logic for TagVisualization1.xaml
    /// </summary>
    public partial class TagVisualization1 : TagVisualization
    {
        //REMEMBER TO CHANGE THIS IN THE XAML
        public int _myWidth = 1920;
        public int _myHeight = 1080;
        private Random rnd;

        public TagVisualization1()
        {
            InitializeComponent();
            rnd = new Random();
            //get sizeof dynmically later
            //int deskHeight = Screen.PrimaryScreen.Bounds.Height;
            //int deskWidth = Screen.PrimaryScreen.Bounds.Width;
        }

        private void TagVisualization1_Loaded(object sender, RoutedEventArgs e)
        {
            //TODO: customize TagVisualization1's UI based on this.VisualizedTag here
            //BlueEcoli b = new BlueEcoli(this);
            //CanvasToAddEcoliTo.Children.Add(b);
        }

        private void TagVisualization_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //BlueEcoli b = new BlueEcoli(this);
            //CanvasToAddEcoliTo.Children.Add(b);
        }

        public void Animate(Grid wy)
        {
            wy.SlideTo(600, 600, 2, AnimationTransitions.QuadEaseOut, 0).Complete += (eo, p) => SlideAnimation();
        }

        private void SlideAnimation()
        {
            double x = rnd.Next(0, 600);
            double y = rnd.Next(0, 600);
            this.SlideTo(x, y, 2, AnimationTransitions.QuadEaseOut, 0);//.Complete += (eo, p) => SlideAnimation();
            Console.WriteLine("in tagviz slide..." + x + ", " + y);
        }
    }
}
