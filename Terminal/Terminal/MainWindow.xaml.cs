using CallOpenWeather;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Terminal
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Weather();

            var timer = new System.Windows.Threading.DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.IsEnabled = true;
            timer.Tick += (o, t) => { Time.Content = DateTime.Now.ToShortTimeString(); };
            timer.Start();
        }

        private void MainInformation(object sender, RoutedEventArgs e)
        {
            _ = MethodWithDelayAsync(400);
        }
        public async Task MethodWithDelayAsync(int milliseconds)
        {
            await Task.Delay(milliseconds);

            WindowInf windowInf = new WindowInf();
            windowInf.ShowDialog();
        }
        

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MuseumWin museumWin = new MuseumWin();
            museumWin.ShowDialog();
        }

        //Погода
        private async void Weather()
        {
            string city = "Волжский";
            WebRequest request = null;
            WebResponse response = await Call.CallWeather(city, request).GetResponseAsync();

            string answer = string.Empty;

            using (Stream s = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(s))
                {
                    answer = await reader.ReadToEndAsync();
                }
            }

            response.Close();

            OpenWeather.OpenWeather oW = JsonConvert.DeserializeObject<OpenWeather.OpenWeather>(answer);

            //string icon = oW.weather[0].icon;

            LabelTemp.Content = oW.main.temp.ToString("#") + "°";

            LabelMain.Content = oW.weather[0].main;

            LabelCity.Content = city;
        } 
    }
}
