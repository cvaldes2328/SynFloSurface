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

namespace SifteoSurfaceApp
{
    /// <summary>
    /// Interaction logic for BlueEcoliScatterViewItem.xaml
    /// </summary>
    public partial class BlueEcoliScatterViewItem : ScatterViewItem
    {
        public BlueEcoliScatterViewItem()
        {
            InitializeComponent();

            //Remove shadow from control
            this.ApplyTemplate();
            this.Background = new SolidColorBrush(Colors.Transparent);
            this.ShowsActivationEffects = false;
            this.BorderBrush = System.Windows.Media.Brushes.Transparent;
            Microsoft.Surface.Presentation.Generic.SurfaceShadowChrome ssc;
            ssc = this.Template.FindName("shadow", this) as Microsoft.Surface.Presentation.Generic.SurfaceShadowChrome;
            ssc.Visibility = Visibility.Hidden;
        }
    }
}
