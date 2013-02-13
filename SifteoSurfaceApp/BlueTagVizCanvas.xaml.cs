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
using Microsoft.Surface.Presentation.Input;
using System.Windows.Threading;
using Artefact.Animation;
using System.Windows.Media.Effects;

namespace SifteoSurfaceApp
{
    /// <summary>
    /// Interaction logic for BlueTagVizCanvas.xaml
    /// </summary>
    public partial class BlueTagVizCanvas : TagVisualization
    {      

        private Random rnd;
        private DispatcherTimer _timer = new DispatcherTimer();

        private List<Grid> ecoliList;

        public int _myWidth = 1300;
        public int _myHeight = 600;

        private System.Windows.Media.SolidColorBrush tailColor = Brushes.PowderBlue;

        public BlueTagVizCanvas()
        {           
            InitializeComponent();

            rnd = new Random();

            ecoliList = new List<Grid>();

            //Blue.SpeedX = 10;
            //Blue.SpeedY = 8;

            //Blue1.SpeedX = 20;
            //Blue1.SpeedY = 18;

            //ecoliList.Add(Blue);
            //ecoliList.Add(Blue1);

            _timer = new DispatcherTimer();
            _timer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            _timer.Tick += MoveEcoliAndDrawTail;
            _timer.Start();

        }

        

        private void MoveEcoliAndDrawTail(object sender, EventArgs e)
        {
            foreach (BlueEcoli g in ecoliList)
            {            
                Canvas.SetLeft(g, g.CurrentX);
                Canvas.SetTop(g, g.CurrentY); 

                UpdateRotationBasedOnNewDirection(g);
                CreateTail();
            }                 
        }

        private void CreateTail()
        {
            //Point pt = new Point(curr + xShiftForParticles, ecoliPoint.Y + yShiftForParticles);
            //var particle = CreateParticle(pt.X, pt.Y);

            //pt.X += rnd.NextDouble() * 100 * (rnd.NextDouble() < .5 ? 1 : -1);
            //pt.Y += rnd.NextDouble() * 100 * (rnd.NextDouble() < .5 ? 1 : -1);

            //particle.AutoAlphaTo(0, .5, AnimationTransitions.CubicEaseIn, 0);
            //particle.SlideTo(pt.X, pt.Y, .5, AnimationTransitions.QuadEaseOut, 0).Complete += (eo, p) => LayoutRoot.Children.Remove(particle); 
        }

        private void UpdateRotationBasedOnNewDirection(BlueEcoli g)
        {
            double newOrientationValue = 0;
            double orientationIncrementValue = Math.Atan(g.CurrentX / g.CurrentY) * (180 / Math.PI);
           
            //Based on what Cartesian quadrant : (i think)
            if (orientationIncrementValue > 90 || orientationIncrementValue < 180)
            {
                if (g.SpeedX < 0)
                    newOrientationValue = orientationIncrementValue - 90;
                else
                    newOrientationValue = orientationIncrementValue + 90;
            }
            else if (orientationIncrementValue > 180 || orientationIncrementValue < 270)
            {
                if (g.SpeedX < 0)
                    newOrientationValue = orientationIncrementValue - 180;
                else
                    newOrientationValue = orientationIncrementValue + 180;
            }
            else if (orientationIncrementValue > 270 || orientationIncrementValue < 360)
            {
                if (g.SpeedX < 0)
                    newOrientationValue = orientationIncrementValue - 270;
                else
                    newOrientationValue = orientationIncrementValue + 270;
            }
            else
            {
                newOrientationValue = orientationIncrementValue;
            }

            //rotateTrans = new RotateTransform(newOrientationValue);
            //g.RenderTransform = rotateTrans;
        }
        
        private Point CheckIfWithinWindowForBounce(BlueEcoli g)
        {

            if (g.CurrentX < 0)//check left side
            {
                g.SpeedX = g.SpeedX * -1;
            }
            else if (g.CurrentX > _myWidth)//check right side
            {
                g.SpeedX = g.SpeedX * -1;
            }

            if (g.CurrentY < 0)//check top side
            {
                g.SpeedY = g.SpeedY * -1;
            }
            else if (g.CurrentY > _myHeight)//check bottom side
            {
                g.SpeedY = g.SpeedY * -1;
            }

            g.CurrentX = g.CurrentX + g.SpeedX;
            g.CurrentY = g.CurrentY + g.SpeedY;

            return new Point(g.CurrentX, g.CurrentY);
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
            LayoutRoot.Children.Add(particle);
            return particle;
        }        

        private void Blue_TouchDown(object sender, TouchEventArgs e)
        {
           
        }

        private void TagVisualization_PreviewTouchDown(object sender, TouchEventArgs e)
        {
            Rect touchBounds = e.Device.GetBounds(null);
            //check if x overlaps with touch bounding box
            //if ((currentX > touchBounds.BottomLeft.X) || (currentX < touchBounds.BottomRight.X))
            //{
            //    ReverseEcoli();
            //}
        }

        private void TagVisualization_TouchDown(object sender, TouchEventArgs e)
        {

            int effectpicker = rnd.Next(0, 5);
            switch (effectpicker)
            {
                case 0:
                    //BlurEcoli();
                    ChangeTailColor(Brushes.DarkGray);
                    break;
                case 1://red
                    ChangeTailColor(Brushes.Red);
                    break;
                case 2://red
                    ChangeTailColor(Brushes.GreenYellow);
                    break;
                case 3://red
                    ChangeTailColor(Brushes.Thistle);
                    break;
                case 4://red
                    ChangeTailColor(Brushes.Salmon);
                    break;
                case 5:
                    ReverseEcoli();
                    break;
                default:
                    break;
            }

        }

        private void ChangeTailColor(SolidColorBrush solidColorBrush)
        {
            tailColor = solidColorBrush;
        }

        private void BlurEcoli()
        {
            this.Effect = new BlurEffect { Radius = 5 };
        }        

        private void ReverseEcoli()
        {
            //_blueSpeed_x = _blueSpeed_x * -1;
            //_blueSpeed_y = _blueSpeed_y * -1;
        }

    }
}
