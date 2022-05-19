using CallOpenWeather;
using Microsoft.Maps.MapControl.WPF;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Terminal.Weather;

namespace Terminal
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CountOpenings countOpenings = new CountOpenings();

        public MainWindow()
        {           
            InitializeComponent();
            try
            {
                StartTerminal();

                CheckNetwork();

                Weather("Волжский");

                DisableGestures();

                //Время
                var timer = new System.Windows.Threading.DispatcherTimer();
                timer.Interval = new TimeSpan(0, 0, 1);
                timer.IsEnabled = true;
                timer.Tick += (o, t) => { Time.Content = DateTime.Now.ToShortTimeString(); };
                timer.Start();

                //Обновление погоды
                var timerWeather = new System.Windows.Threading.DispatcherTimer();
                timerWeather.Interval = new TimeSpan(0, 5, 0);
                timerWeather.IsEnabled = true;
                timerWeather.Tick += (o, t) => { Weather("Волжский"); };
                timerWeather.Start();
            }
            catch (Exception ex)
            {
                ExceptionTerminal();
            }

        }

        //Отключение жестов WINDOWS 10(режим планшета)
        public void DisableGestures()
        {
            IntPtr intPtr = System.Diagnostics.Process.GetProcessesByName("Terminal")[0].MainWindowHandle;
            EdgeGestureUtil.DisableEdgeGestures(intPtr, true);
        }

        //Задержка
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
            countOpenings.museum += 1;

            MuseumWin museumWin = new MuseumWin();
            museumWin.ShowDialog();
        }

        //Погода
        private async void Weather(string city)
        {
            if (CheckNetwork())
            {
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

                LabelTemp.Content = oW.main.temp.ToString("#") + "°";

                Translate translate = new Translate(oW.weather[0].main);

                LabelMain.Content = translate.GetWord();

                LabelCity.Content = city;

                Text_Pressure.Content = ((int)oW.main.pressure).ToString() + "мм";

                Text_Humidity.Content = oW.main.humidity.ToString() + "%";

                Text_Wind.Content = oW.wind.speed.ToString() + "м/с";

                Text_Direction.Content = oW.wind.deg.ToString() + "°";

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
            else
                return true;
        }

        private void SpecialtiesClick(object sender, RoutedEventArgs e)
        {
            countOpenings.specialities += 1;

            Specialties specialties = new Specialties();
            specialties.ShowDialog();
        }

        private void Workshops_Click(object sender, RoutedEventArgs e)
        {
            countOpenings.masterskaya += 1;

            Workshops workshops = new Workshops();
            workshops.ShowDialog();
        }

        private void Button_FAQ_Click(object sender, RoutedEventArgs e)
        {
            countOpenings.faq += 1;

            FAQWin fAQWin = new FAQWin();
            fAQWin.ShowDialog();
        }

        void Button_MapWin_Click(object sender, RoutedEventArgs e)
        {
            countOpenings.map += 1;

            MapWin mapWin = new MapWin(new Location(48.807824, 44.729445));
            mapWin.ShowDialog();
        }

        private void Rutube_Click(object sender, RoutedEventArgs e)
        {
            countOpenings.rutube += 1;

            Image image = new Image();
            image.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + $"Image/QR/QRRutube.png"));

            OpenQRWin openQRWin = new OpenQRWin(image);
            openQRWin.ShowDialog();
        }

        private void Telegram_Click(object sender, RoutedEventArgs e)
        {
            countOpenings.telega += 1;

            Image image = new Image();
            image.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + $"Image/QR/QRTelegram.png"));

            OpenQRWin openQRWin = new OpenQRWin(image);
            openQRWin.ShowDialog();
        }

        private void Vk_Click(object sender, RoutedEventArgs e)
        {
            countOpenings.vk += 1;

            Image image = new Image();
            image.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + $"Image/QR/QRVk.png"));

            OpenQRWin openQRWin = new OpenQRWin(image);
            openQRWin.ShowDialog();
        }

        private void Schedule_Click(object sender, RoutedEventArgs e)
        {
            countOpenings.ruspisanie += 1;

            Image image = new Image();
            image.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + $"Image/QR/QRSchedule.png"));

            OpenQRWin openQRWin = new OpenQRWin(image);
            openQRWin.ShowDialog();
        }

        private void Qvantorium_Click(object sender, RoutedEventArgs e)
        {
            countOpenings.qvantorium += 1;

            QvantoriumWin qvantoriumWin = new QvantoriumWin();
            qvantoriumWin.ShowDialog();
        }

        private void Additional_Click(object sender, RoutedEventArgs e)
        {
            countOpenings.rounded += 1;

            AdditionalWin additionalWin = new AdditionalWin();
            additionalWin.ShowDialog();
        }

        private void StartTerminal()
        {
            string myApiKey = "AE6DB76C-7A11-1E06-E462-D35991791012";
            string myApiKey2 = "AE6DB76C-7A11-1E06-E462-D35991791012";

            string nmb = "79696533799";
            string nmb2 = "79026560519";

            SmsRu smsRu = new SmsRu(myApiKey);
            SmsRu smsRu2 = new SmsRu(myApiKey2);

            smsRu.Send(nmb, "Терминал включился");
            smsRu2.Send(nmb2, "Терминал включился");
        }

        private void EndTerminal(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string myApiKey = "AE6DB76C-7A11-1E06-E462-D35991791012";
            string myApiKey2 = "AE6DB76C-7A11-1E06-E462-D35991791012";

            string nmb = "79696533799";
            string nmb2 = "79026560519";

            SmsRu smsRu = new SmsRu(myApiKey);
            SmsRu smsRu2 = new SmsRu(myApiKey2);

            smsRu.Send(nmb, "Терминал выключился "
                + "Отчёт: "
                + "Основные сведения:" + $"{countOpenings.osnSvedinia.ToString()}"
                + "Музей:" + $"{countOpenings.museum}"
                + "Карты:" + $"{countOpenings.map}"
                + "Мастерская:" + $"{countOpenings.masterskaya}"
                + "Специальности:" + $"{countOpenings.specialities}"
                + "Кружки:" + $"{countOpenings.rounded}"
                + "Квантория:" + $"{countOpenings.qvantorium}"
                + "Рутуб:" + $"{countOpenings.rutube}"
                + "Вк:" + $"{countOpenings.vk}"
                + "Телега:" + $"{countOpenings.telega}"
                + "Расписание:" + $"{countOpenings.ruspisanie}"
                + "FAQ:" + $"{countOpenings.faq}");

            smsRu2.Send(nmb2, "Терминал выключился "
                + "Отчёт: "
                + "Основные сведения: " + $"{countOpenings.osnSvedinia} "
                + "Музей: " + $"{countOpenings.museum} "
                + "Карты: " + $"{countOpenings.map} "
                + "Мастерская: " + $"{countOpenings.masterskaya} "
                + "Специальности: " + $"{countOpenings.specialities} "
                + "Кружки: " + $"{countOpenings.rounded} "
                + "Квантория: " + $"{countOpenings.qvantorium} "
                + "Рутуб: " + $"{countOpenings.rutube} "
                + "Вк: " + $"{countOpenings.vk} "
                + "Телега: " + $"{countOpenings.telega} "
                + "Расписание: " + $"{countOpenings.ruspisanie} "
                + "FAQ: " + $"{countOpenings.faq} ");

            MessageBox.Show("Терминал выключился "
                + "Отчёт: "
                + "Основные сведения:" + $"{countOpenings.osnSvedinia.ToString()}"
                + "Музей:" + $"{countOpenings.museum}"
                + "Карты:" + $"{countOpenings.map}"
                + "Мастерская:" + $"{countOpenings.masterskaya}"
                + "Специальности:" + $"{countOpenings.specialities}"
                + "Кружки:" + $"{countOpenings.rounded}"
                + "Квантория:" + $"{countOpenings.qvantorium}"
                + "Рутуб:" + $"{countOpenings.rutube}"
                + "Вк:" + $"{countOpenings.vk}"
                + "Телега:" + $"{countOpenings.telega}"
                + "Расписание:" + $"{countOpenings.ruspisanie}"
                + "FAQ:" + $"{countOpenings.faq}");

        }


        private void ExceptionTerminal()
        {
            string myApiKey = "AE6DB76C-7A11-1E06-E462-D35991791012";
            string myApiKey2 = "AE6DB76C-7A11-1E06-E462-D35991791012";

            string nmb = "79696533799";
            string nmb2 = "79026560519";

            SmsRu smsRu = new SmsRu(myApiKey);
            SmsRu smsRu2 = new SmsRu(myApiKey2);
            smsRu.Send(nmb, "ГГ ТЕРМИНАЛУ");
            smsRu2.Send(nmb2, "ГГ ТЕРМИНАЛУ");

            MessageBox.Show("ТЕРМИНАЛ СЛОМАЛСЯ))) \n" +
                "Подойтите в кабинет 216 и сообщите об этом", null);
        }
    }
}
