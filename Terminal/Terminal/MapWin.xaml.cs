using Microsoft.Maps.MapControl.WPF;
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


namespace Terminal
{
    /// <summary>
    /// Логика взаимодействия для MapWin.xaml
    /// </summary>
    public partial class MapWin : Window
    {
        //Выбранная локация
        private Location location;

        public MapWin(Location location)
        {           
            InitializeComponent();

            this.location = location;

            SetLocation();
            SetMurker();
        }

        //Выход
        private void Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        //Устанавливаем локацию
        public void SetLocation()
        {
            Map.Center = location;
        }
        //Приблежаем картинку
        private void ZoomPlus(object sender, RoutedEventArgs e)
        {
            Map.ZoomLevel += 1;
        }
        //Отдаляем картинку
        private void ZoomMinus(object sender, RoutedEventArgs e)
        {
            Map.ZoomLevel -= 1;
        }
        //Ставим маркеры на карте
        private void SetMurker()
        {
            var locations = new[] {
                //Корпус 1
            new Location(48.807824, 44.729445),
                //Корпус 2
            new Location(48.786934, 44.772160),
                //Корпус 3
            new Location(48.789219, 44.767156),
                //Корпус 4
            new Location(48.782049, 44.771755)};
            for (int i = 0; i < locations.Length; i++)
            {
                var pushpin = new Pushpin() { Location = locations[i], Content = i + 1 };
                this.Map.Children.Add(pushpin);
            }
        }
    }
}
