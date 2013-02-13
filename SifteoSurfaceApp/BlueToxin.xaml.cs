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
using System.Windows.Media.Animation;

namespace SifteoSurfaceApp
{
    /// <summary>
    /// Interaction logic for BlueToxin.xaml
    /// </summary>
    public partial class BlueToxin : TagVisualization
    {
        private int enterCount;

        /// <summary>
        /// Constructor.
        /// </summary>
        public BlueToxin()
        {
            InitializeComponent();

            this.RenderTransform = new ScaleTransform(1, 1);
        }

        /// <summary>
        /// Call to indicate that the visualization has entered another UI element.
        /// </summary>
        public void Enter()
        {
            ++enterCount;
            if (enterCount == 1)
            {
                Animate(0.5, 1.0, 0.4, 0.0, 1.0);
            }
        }

        /// <summary>
        /// Call to indicate that the visualization has left another UI element.
        /// </summary>
        public void Leave()
        {
            //Debug.Assert(enterCount > 0);
            --enterCount;
            if (enterCount < 1)
            {
                Animate(0.2, 0.4, 1.0, 1.0, 0.0);
            }
        }

        /// <summary>
        /// Change the logical highlight "level" of the visualization (0 = totally
        /// un-highlighted, 1 = totally highlighted).
        /// </summary>
        /// <remarks>
        /// The implementation here is to affect transparency and size. "Highlighted"
        /// = opaque and large, "un-highlighted" = transparent and small.
        /// </remarks>
        /// <param name="seconds"></param>
        /// <param name="fromLevel"></param>
        /// <param name="toLevel"></param>
        /// <param name="accelerationRatio"></param>
        /// <param name="decelerationRatio"></param>
        private void Animate(
            double seconds,
            double fromLevel,
            double toLevel,
            double accelerationRatio,
            double decelerationRatio)
        {
            DoubleAnimation animation = new DoubleAnimation(
                fromLevel,
                toLevel,
                new Duration(TimeSpan.FromSeconds(seconds)));
            animation.AccelerationRatio = accelerationRatio;
            animation.DecelerationRatio = decelerationRatio;
            ScaleTransform transform = (ScaleTransform)this.RenderTransform;
            transform.BeginAnimation(ScaleTransform.ScaleXProperty, animation);
            transform.BeginAnimation(ScaleTransform.ScaleYProperty, animation);
            this.BeginAnimation(OpacityProperty, animation);
        }
    }
}
