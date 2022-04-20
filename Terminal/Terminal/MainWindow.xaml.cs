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
using Terminal.Weather;
using Terminal.OpenWeather;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net.Sockets;

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

            CheckNetwork();

            Weather();


            //Время
            var timer = new System.Windows.Threading.DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.IsEnabled = true;
            timer.Tick += (o, t) => { Time.Content = DateTime.Now.ToShortTimeString(); };
            timer.Start();

            //Обновление погоды
            var timerWeather = new System.Windows.Threading.DispatcherTimer();
            timerWeather.Interval = new TimeSpan(0, 0, 5);
            timerWeather.IsEnabled = true;
            timerWeather.Tick += (o, t) => { Weather(); };
            timerWeather.Start();
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
            if (CheckNetwork())
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

                Translate translate = new Translate(oW.weather[0].main);

                LabelMain.Content = translate.GetWord();

                LabelCity.Content = city;

                Text_Pressure.Content = ((int)oW.main.pressure).ToString() + "мм";

                Text_Humidity.Content = oW.main.humidity.ToString() + "%";

                Text_Wind.Content = oW.wind.deg.ToString() + "°";

                Text_Direction.Content = oW.wind.speed.ToString() + "м/с";

                IconWeather.Source = oW.weather[0].Icon;

                GridWeather.Visibility = Visibility.Visible;
            }
            else
            {
                GridWeather.Visibility = Visibility.Hidden;
            }
        }

        private bool CheckNetwork()
        {
            if (!System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
            {
                return false;
            }

            bool isConnected = false;
            using (var tcpClient = new TcpClient())
            {
                tcpClient.Connect("209.85.148.138", 443); // google
                isConnected = tcpClient.Connected;
                return true;
            }
        }

        private void SpecialtiesClick(object sender, RoutedEventArgs e)
        {
            Specialties specialties = new Specialties();
            specialties.ShowDialog();
        }

        private void Workshops_Click(object sender, RoutedEventArgs e)
        {
            Workshops workshops = new Workshops();
            workshops.ShowDialog();
        }
    }
}
