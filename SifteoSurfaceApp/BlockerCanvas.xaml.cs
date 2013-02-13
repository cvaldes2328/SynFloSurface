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
using System.Windows.Media.Effects;
using System.Windows.Media.Animation;
using Artefact.Animation;

namespace SifteoSurfaceApp
{
    /// <summary>
    /// Interaction logic for BlockerCanvas.xaml
    /// </summary>
    public partial class BlockerCanvas : Canvas
    {
        private Random rnd;
        private DispatcherTimer _timer = new DispatcherTimer();

        public int _redSpeed_x = 5;
        public int _redSpeed_y = 5;

        public int _blueSpeed_x = 10;
        public int _blueSpeed_y = 8;

        public double currentX = 100;
        public double currentY = 100;

        public int _yellowSpeed_x = 2;
        public int _yellowSpeed_y = 12;

        public int _myWidth = 1300;
        public int _myHeight = 600;

        public int yShiftForParticles = 0;
        public int xShiftForParticles = 0;

        private RotateTransform rotateTrans;

        public BlockerCanvas()
        {
            InitializeComponent();

            rnd = new Random();

            _timer = new DispatcherTimer();
            _timer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            _timer.Tick += UpdateImagePositions;
            _timer.Start();

        }

        

        private void UpdateImagePositions(object sender, EventArgs e)
        {
            
            if (currentX < 0)//check left side
            {
                _blueSpeed_x = _blueSpeed_x * -1;
                xShiftForParticles = -150;
                yShiftForParticles = -35;
            }
            else if (currentX > _myWidth)//check right side
            {
                _blueSpeed_x = _blueSpeed_x * -1;

                xShiftForParticles = 150;
                yShiftForParticles = 35;
            }

            if (currentY < 0)//check top side
            {
                _blueSpeed_y = _blueSpeed_y * -1;
                xShiftForParticles = -150;
                yShiftForParticles = -35;
            }
            else if (currentY > _myHeight)//check bottom side
            {
                _blueSpeed_y = _blueSpeed_y * -1;

                xShiftForParticles = 150;
                yShiftForParticles = 25;
            }
            currentX = currentX + _blueSpeed_x;
            currentY = currentY + _blueSpeed_y;
            //Canvas.SetLeft(Blue, currentX);
            //Canvas.SetTop(Blue, currentY);

            //rotateTrans = new RotateTransform((float)Math.Atan(currentX / -currentY) * (180 / Math.PI));
            //Blue.RenderTransform = rotateTrans;

            double newOrientationValue = 0;
            double orientationIncrementValue = Math.Atan(_blueSpeed_x / _blueSpeed_y) *(180 / Math.PI);
            Console.WriteLine("rotate value: " + newOrientationValue);
            //Based on what Cartesian quadrant : (i think)
            if (orientationIncrementValue >90 || orientationIncrementValue <180){
                if (_blueSpeed_x < 0)
                    newOrientationValue = orientationIncrementValue - 90;
                else
                    newOrientationValue = orientationIncrementValue + 90;
            } else if (orientationIncrementValue >180 || orientationIncrementValue <270){
                if (_blueSpeed_x < 0)
                    newOrientationValue = orientationIncrementValue - 180;
                else
                newOrientationValue = orientationIncrementValue + 180;
            } else if (orientationIncrementValue >270|| orientationIncrementValue <360){
                if(_blueSpeed_x <0)
                    newOrientationValue = orientationIncrementValue - 270;
                else
                    newOrientationValue = orientationIncrementValue + 270;
            }else{
                newOrientationValue = orientationIncrementValue;
            }
            Console.WriteLine("rotate value: " + newOrientationValue);
            rotateTrans = new RotateTransform(newOrientationValue);
            //Blue.RenderTransform = rotateTrans;

            Point pt = new Point(currentX + xShiftForParticles, currentY + yShiftForParticles);
            var particle = CreateParticle(pt.X, pt.Y);

            pt.X += rnd.NextDouble() * 100 * (rnd.NextDouble() < .5 ? 1 : -1);
            pt.Y += rnd.NextDouble() * 100 * (rnd.NextDouble() < .5 ? 1 : -1);

            particle.AutoAlphaTo(0, .5, AnimationTransitions.CubicEaseIn, 0);
            particle.SlideTo(pt.X, pt.Y, .5, AnimationTransitions.QuadEaseOut, 0).Complete += (eo, p) => LayoutRoot.Children.Remove(particle);                    
        }


        /// <summary>
        /// update blue and make particles trail
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MoveBlue()
        {
            
        }

        public Ellipse CreateParticle(double x, double y)
        {
            //var particle = new Ellipse { Width = 10, Height = 10, Fill = (Brush)Resources["BrushWhite"], Stroke = (Brush)Resources["BrushBlack"] };
            Ellipse particle = new Ellipse();
            particle.Width = 10;
            particle.Height = 10;
            particle.Fill = Brushes.WhiteSmoke;
            Canvas.SetLeft(particle, x);
            Canvas.SetTop(particle, y);
            LayoutRoot.Children.Add(particle);
            return particle;
        }

        private void LayoutRoot_MouseMove(object sender, MouseEventArgs e)
        {
            var pt = e.GetPosition(null);
            Console.WriteLine(pt);
            var particle = CreateParticle(pt.X, pt.Y);

            pt.X += rnd.NextDouble() * 100 * (rnd.NextDouble() < .5 ? 1 : -1);
            pt.Y += rnd.NextDouble() * 100 * (rnd.NextDouble() < .5 ? 1 : -1);

            particle.AutoAlphaTo(0, .5, AnimationTransitions.CubicEaseIn, 0);
            particle.SlideTo(pt.X, pt.Y, .5, AnimationTransitions.QuadEaseOut, 0).Complete += (eo, p) => LayoutRoot.Children.Remove(particle);
        }

        private void Blue_TouchDown(object sender, TouchEventArgs e)
        {
           
        }

        private void Blue_TouchDown_1(object sender, TouchEventArgs e)
        {

        }

        private void Blue_TouchDown_2(object sender, TouchEventArgs e)
        {

        }
    }
}
