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
    /// A scatterview that makes an interactive ecoli on touch
    /// </summary>
    public partial class TrailOnTouch : ScatterView
    {
        public Random _random = new Random();
        private Random rnd;
        public DispatcherTimer _timer = new DispatcherTimer();
        public int _redSpeed_x = 5;
        public int _redSpeed_y = 5;

        public int _blueSpeed_x = 10;
        public int _blueSpeed_y = 8;
        
        public int _yellowSpeed_x = 2;
        public int _yellowSpeed_y = 12;
        
        public int _myWidth = 1920;
        public int _myHeight = 1080;


        public int yShiftForParticles = 0;
        public int xShiftForParticles = 0;

        private RotateTransform rotateTrans;

        public TrailOnTouch()
        {
            InitializeComponent();

            //blue.Center = new Point(100, 100);

            //_myStoryboard = this.FindResource("myStoryboard") as Storyboard;
            //_myStoryboard.Completed += new EventHandler(myStoryboard_Completed);

            //_myPointAnimation = _myStoryboard.Children.ElementAt(0) as PointAnimation;

            _timer = new DispatcherTimer();
            _timer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            _timer.Tick += UpdateImagePositions;
            _timer.Start();

            StartTimer();

            rnd = new Random();

            //Point pt = blue.Center; // args.GetPosition(null);

            //// transition info
            //double time = .8;
            //double delay = 0;
            //PercentHandler ease = AnimationTransitions.CubicEaseOut;

            //// ease
            //ArtefactAnimator.AddEase(blue, AnimationTypes.X, pt.X + 300, time, ease, delay);
            //ArtefactAnimator.AddEase(blue, AnimationTypes.Y, pt.Y + 300, time, ease, delay);

        }

        public void MakeDynamicEcoli(string color, Point myPoint)
        {
            //TODO - diff Ecoli by string color
            //BlueEcoliScatterViewItem blueSVI = new BlueEcoliScatterViewItem();
            //blueSVI.ContainerManipulationDelta += new ContainerManipulationDeltaEventHandler(blueSVI_ContainerManipulationDelta);
            //LayoutRoot.Items.Add(blueSVI);
            //blueSVI.Center = myPoint;

            //EcoliWithTail blueSVI = new EcoliWithTail();
            //blueSVI.ContainerManipulationDelta += new ContainerManipulationDeltaEventHandler(blueSVI_ContainerManipulationDelta);
            //LayoutRoot.Items.Add(blueSVI);
            //blueSVI.Center = myPoint;

            //BlueEcoli e = new BlueEcoli();            
            //LayoutRoot.Items.Add(e);
            //e.Orientation = 180;
            //e.Center = myPoint;


        }

        //public Ellipse CreateParticle(double x, double y)
        //{
        //    var particle = new Ellipse { Width = 10, Height = 10, Fill = (Brush)Resources["BrushWhite"], Stroke = (Brush)Resources["BrushBlack"] };
        //    Canvas.SetLeft(particle, x);
        //    Canvas.SetTop(particle, y);
        //    LayoutRoot.Children.Add(particle);
        //    return particle;
        //}

        void blueSVI_ContainerManipulationDelta(object sender, ContainerManipulationDeltaEventArgs e)
        {
            ScatterViewItem s = sender as ScatterViewItem;
            // Retrieve X and Y position of the mouse
            double xPos = s.Center.X;
            double yPos = s.Center.Y;

            //BlueTrail temp = new BlueTrail(); //TODO - update based on color from sender
            
            //temp.Opacity = 1;

            //// Add the newly created image to the layout
            //LayoutRoot.Items.Add(temp);

            //temp.Center = s.Center;
        }

        void myStoryboard_Completed(object sender, EventArgs e)
        {
            _timer.Start();
        }

        private void UpdateImagePositions(object sender, EventArgs e)
        {
            foreach (ScatterViewItem s in LayoutRoot.Items)//for all the SVIs
            {
                if (s.Name.Contains("Red"))
                {
                    Point newPos = s.Center;

                    if (newPos.X < 0)//check left side
                    {
                        _redSpeed_x = _redSpeed_x * -1;
                    }
                    else if (newPos.X > _myWidth)//check right side
                    {
                        _redSpeed_x = _redSpeed_x * -1;
                    }

                    if (newPos.Y < 0)//check top side
                    {
                        _redSpeed_y = _redSpeed_y * -1;
                    }
                    else if (newPos.Y > _myHeight)//check bottom side
                    {
                        _redSpeed_y = _redSpeed_y * -1;
                    }

                    s.Center = new Point(newPos.X + _redSpeed_x, newPos.Y + _redSpeed_y);
                }else
                if (s.Name.Contains("blue"))
                {
                    Point newPos = s.Center;

                    if (s.Center.X < 0)//check left side
                    {
                        _blueSpeed_x = _blueSpeed_x * -1;
                        xShiftForParticles = -150;
                        yShiftForParticles = -35;
                    }
                    else if (s.Center.X > _myWidth)//check right side
                    {
                        _blueSpeed_x = _blueSpeed_x * -1;

                        xShiftForParticles = 150;
                        yShiftForParticles = 35;
                    }

                    if (s.Center.Y < 0)//check top side
                    {
                        _blueSpeed_y = _blueSpeed_y * -1;
                        xShiftForParticles = -150;
                        yShiftForParticles = -35;
                    }
                    else if (s.Center.Y > _myHeight)//check bottom side
                    {
                        _blueSpeed_y = _blueSpeed_y * -1;

                        xShiftForParticles = 150;
                        yShiftForParticles = 25;
                    }
                    newPos.X = s.Center.X + _blueSpeed_x;
                    newPos.Y = s.Center.Y + _blueSpeed_y;
                    s.Center = newPos;

                    //rotateTrans = new RotateTransform((float)Math.Atan(currentX / -currentY) * (180 / Math.PI));
                    //Blue.RenderTransform = rotateTrans;

                    double newOrientationValue = 0;
                    double orientationIncrementValue = Math.Atan(_blueSpeed_x / _blueSpeed_y) * (180 / Math.PI);
                    Console.WriteLine("rotate value: " + newOrientationValue);
                    //Based on what Cartesian quadrant : (i think)
                    if (orientationIncrementValue > 90 || orientationIncrementValue < 180)
                    {
                        if (_blueSpeed_x < 0)
                            newOrientationValue = orientationIncrementValue - 90;
                        else
                            newOrientationValue = orientationIncrementValue + 90;
                    }
                    else if (orientationIncrementValue > 180 || orientationIncrementValue < 270)
                    {
                        if (_blueSpeed_x < 0)
                            newOrientationValue = orientationIncrementValue - 180;
                        else
                            newOrientationValue = orientationIncrementValue + 180;
                    }
                    else if (orientationIncrementValue > 270 || orientationIncrementValue < 360)
                    {
                        if (_blueSpeed_x < 0)
                            newOrientationValue = orientationIncrementValue - 270;
                        else
                            newOrientationValue = orientationIncrementValue + 270;
                    }
                    else
                    {
                        newOrientationValue = orientationIncrementValue;
                    }
                    Console.WriteLine("rotate value: " + newOrientationValue);
                    rotateTrans = new RotateTransform(newOrientationValue);
                    s.RenderTransform = rotateTrans;

                    Point pt = new Point(newPos.X + xShiftForParticles, newPos.Y + yShiftForParticles);
                    var particle = CreateParticle(pt.X, pt.Y);

                    pt.X += rnd.NextDouble() * 100 * (rnd.NextDouble() < .5 ? 1 : -1);
                    pt.Y += rnd.NextDouble() * 100 * (rnd.NextDouble() < .5 ? 1 : -1);

                    particle.AutoAlphaTo(0, .5, AnimationTransitions.CubicEaseIn, 0);
                    particle.SlideTo(pt.X, pt.Y, .5, AnimationTransitions.QuadEaseOut, 0).Complete += (eo, p) => MainCanvas.Children.Remove(particle); 
                } else
                if (s.Name.Contains("Yellow"))
                {
                    Point newPos = s.Center;

                    if (newPos.X < 0)//check left side
                        _yellowSpeed_x = _yellowSpeed_x * -1;
                    else if (newPos.X > _myWidth)//check right side
                        _yellowSpeed_x = _yellowSpeed_x * -1;

                    if (newPos.Y < 0)//check top side
                        _yellowSpeed_y = _yellowSpeed_y * -1;
                    else if (newPos.Y > _myHeight)//check bottom side
                        _yellowSpeed_y = _yellowSpeed_y * -1;

                    s.Center = new Point(newPos.X + _yellowSpeed_y, newPos.Y + _yellowSpeed_y);
                }
            }
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
            MainCanvas.Children.Add(particle);
            return particle;
        }

        private void Canvas_TouchDown(object sender, TouchEventArgs e)
        {
            _timer.Stop();

            // Retrieve current mouse coordinates.
            double newX = e.GetTouchPoint(this).Position.X;
            double newY = e.GetTouchPoint(this).Position.Y;
            Point myPoint = new Point();
            myPoint.X = newX;
            myPoint.Y = newY;
        }

        #region Trail Animation

        /// <summary>
        /// Timer to added to the page to use a delegate.
        /// </summary>
        public void StartTimer()
        {
            DispatcherTimer myDispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            myDispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 1000); // 100 Milliseconds
            myDispatcherTimer.Tick += new EventHandler(FadeTrail); // Every 'tick' (100 Milliseconds), the "FadeTrail" function is called
            myDispatcherTimer.Start();
        }

        /// <summary>
        /// Delegate to be called once the timer is called to fade out the cursor trail.
        /// </summary>
        /// <param name="o"></param>
        /// <param name="sender"></param>
        public void FadeTrail(object o, EventArgs sender)
        {
            
            // position info
           
            //// Loop through all images in the trail and change the dimensions + opacity
            //foreach (ScatterViewItem trailimg in LayoutRoot.Items)
            //{
            //    if (trailimg.Opacity == 0 && trailimg.Name.Contains("Trail"))
            //        LayoutRoot.Items.Remove(trailimg);
            //    else if (trailimg.Name.Contains("Trail"))
            //    {
            //        trailimg.Opacity -= 0.1;
            //    }
            //}
        }

        private void ScatterViewItem_ContainerManipulationDelta(object sender, ContainerManipulationDeltaEventArgs e)
        {
            //ScatterViewItem s = sender as ScatterViewItem;
            //// Retrieve X and Y position of the mouse
            //double xPos = s.Center.X;
            //double yPos = s.Center.Y;

            //BlueTrail temp = new BlueTrail(); //TODO - update based on color from sender
            //temp.Effect = new BlurEffect { Radius = 5 };
            //temp.Opacity = 1;

            //// Add the newly created image to the layout
            //LayoutRoot.Items.Add(temp);

            //temp.Center = s.Center;
        }

        #endregion

        
    }
}
