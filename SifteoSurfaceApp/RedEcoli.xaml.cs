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
    /// Interaction logic for RedEcoli.xaml
    /// </summary>
    public partial class RedEcoli : Grid
    {
        private double currentX;
        private double currentY;

        private double speedX;
        private double speedY;

        private RotateTransform rotateTrans;
        private TranslateTransform transTrans;

        private Random rnd;
        private DispatcherTimer _timer = new DispatcherTimer();

        private System.Windows.Media.SolidColorBrush tailColor = new SolidColorBrush(new Color { A = 200, B = (byte)0, G = (byte)0, R = (byte)255 });

        private TagVisualization1 myParent;

        public double CurrentX
        {
            get { return currentX; }
            set { currentX = value; }
        }

        public double CurrentY
        {
            get { return currentY; }
            set { currentY = value; }
        }

        public double SpeedX
        {
            get { return speedX; }
            set { speedX = value; }
        }

        public double SpeedY
        {
            get { return speedY; }
            set { speedY = value; }
        }

        public RedEcoli(TagVisualization1 t)
        {
            InitializeComponent();

            myParent = t;
            
            rnd = new Random();

            speedX = rnd.Next(0, 20);
            speedY = rnd.Next(0, 15);

            currentX = speedX;
            currentY = speedY;

            UpdateRotationBasedOnNewDirection();

            _timer = new DispatcherTimer();
            _timer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            _timer.Tick += MoveEcoliAndDrawTail;
            _timer.Start();
        }

        private void MoveEcoliAndDrawTail(object sender, EventArgs e)
        {
            CheckIfWithinWindowForBounce();
            Canvas.SetLeft(this, currentX);
            Canvas.SetTop(this, currentY);

            UpdateRotationBasedOnNewDirection();
            
        }

        private void UpdateRotationBasedOnNewDirection()
        {
            double newOrientationValue = 0;
            double orientationIncrementValue = Math.Atan(currentX / currentY) * (180 / Math.PI);

            //Based on what Cartesian quadrant : (i think)
            if (orientationIncrementValue > 90 || orientationIncrementValue < 180)
            {
                if (speedX < 0)
                {
                    newOrientationValue = orientationIncrementValue - 90;
                    //xShiftForParticles = currentX + 130;
                }
                else
                { 
                    newOrientationValue = orientationIncrementValue + 90;
                   // xShiftForParticles = currentX - 130;
                }
            }
            else if (orientationIncrementValue > 180 || orientationIncrementValue < 270)
            {
                if (speedX < 0)
                {
                    newOrientationValue = orientationIncrementValue - 180;
                    //xShiftForParticles = currentX + 20;
                }
                else
                {
                    newOrientationValue = orientationIncrementValue + 180;
                    //xShiftForParticles = currentX + 20;
                }
            }
            else if (orientationIncrementValue > 270 || orientationIncrementValue < 360)
            {
                if (speedX < 0)
                {
                    newOrientationValue = orientationIncrementValue - 270;
                    //xShiftForParticles = currentX + 130;
                }
                else
                {
                    newOrientationValue = orientationIncrementValue + 270;
                    //xShiftForParticles = currentX + 130;
                }
            }
            else
            {
                newOrientationValue = orientationIncrementValue;
            }

            rotateTrans = new RotateTransform(newOrientationValue);
            this.RenderTransform = rotateTrans;
        }

        private Point CheckIfWithinWindowForBounce()
        {

            if (currentX < -1920)//check left side
            {
                speedX = speedX * -1;
            }
            else if (currentX > myParent._myWidth)//check right side
            {
                speedX = speedX * -1;
            }

            if (currentY < -1080)//check top side
            {
                speedY = speedY * -1;
            }
            else if (currentY > myParent._myHeight)//check bottom side
            {
                speedY = speedY * -1;
            }

            currentX = currentX + speedX;
            currentY = currentY + speedY;

            return new Point(currentX, currentY);
        }


        private void Blue_TouchDown(object sender, TouchEventArgs e)
        {
            int effectpicker = rnd.Next(0, 4);
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
                    //ReverseEcoli();
                    break;
                default:
                    break;
            }

        }

       
        private void ChangeTailColor(SolidColorBrush solidColorBrush)
        {
            Tail.ChangeTailColor(solidColorBrush);
        }

        private void BlurEcoli()
        {
            this.Effect = new BlurEffect { Radius = 5 };
        }

        private void ReverseEcoli()
        {
            speedX = speedX * -1;
            speedY = speedY * -1;
        }
    }
}
