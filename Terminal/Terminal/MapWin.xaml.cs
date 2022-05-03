using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Maps.MapControl.WPF;

namespace Terminal
{
    /// <summary>
    /// Логика взаимодействия для MapWin.xaml
    /// </summary>
    public partial class MapWin : Window
    {
        
        public MapWin(Location location)
        {
            
            InitializeComponent();           
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public void SetLocation()
        {

        }

        private void ZoomPlus(object sender, RoutedEventArgs e)
        {
            Map.ZoomLevel += 1;
        }
        private void ZoomMinus(object sender, RoutedEventArgs e)
        {
            Map.ZoomLevel -= 1;
        }
    }
}
