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

namespace SifteoSurfaceApp
{
    /// <summary>
    /// Interaction logic for WhiteEcoliWithColorPlasmid.xaml
    /// </summary>
    public partial class WhiteEcoliWithColorPlasmid : Grid
    {
        public WhiteEcoliWithColorPlasmid(Brushes color)
        {
            InitializeComponent();

            //Plasmid.Stroke = color;
        }

        
    }
}
