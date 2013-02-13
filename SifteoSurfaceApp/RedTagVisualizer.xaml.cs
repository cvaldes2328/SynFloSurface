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
using System.Windows.Media.Effects;

namespace SifteoSurfaceApp
{
    /// <summary>
    /// Interaction logic for RedTagVisualizer.xaml
    /// </summary>
    public partial class RedTagVisualizer : TagVisualization
    {
        public SurfaceWindow1 _mySurfaceWindow1;
        private Random _myRandom;

        public RedTagVisualizer()
        {
            InitializeComponent();
            _myRandom = new Random();
        }

        private void TagVisualization_TouchDown(object sender, TouchEventArgs e)
        {
            Point actualTouchPoint = e.Device.GetCenterPosition(_mySurfaceWindow1);

            int effectpicker = _myRandom.Next(0, 3);

            BodyToFill.Fill = Brushes.Lavender;

            switch (effectpicker)
            {
                case 0:
                    BlurEcoli();
                    break;
                case 1:
                    MoveEcoli(actualTouchPoint);
                    break;
                case 2:
                    ReverseEcoli();
                    break;
                default:
                    break;
            }

        }

        private void BlurEcoli()
        {
            this.Effect = new BlurEffect { Radius = 5 };
        }

        private void MoveEcoli(Point actualTouchPoint)
        {
            TranslateTransform myTranslateTransform;

            //double x = 0;
            //double y = 0;

            //if (actualTouchPoint.X < 960)
            //    x = actualTouchPoint.X - 100;
            //else
            //    x = actualTouchPoint.X + 50;

            //if (actualTouchPoint.Y < 540)
            //    y = actualTouchPoint.Y - 20;
            //else
            //    y = actualTouchPoint.Y + 20;

            myTranslateTransform = new TranslateTransform(actualTouchPoint.X - 150, actualTouchPoint.Y - 200);
            this.RenderTransform = myTranslateTransform;
        }

        private void ReverseEcoli()
        {
            if (this.FlowDirection == FlowDirection.RightToLeft)
                this.FlowDirection = System.Windows.FlowDirection.LeftToRight;
            else
                this.FlowDirection = System.Windows.FlowDirection.RightToLeft;
        }
    }
}
