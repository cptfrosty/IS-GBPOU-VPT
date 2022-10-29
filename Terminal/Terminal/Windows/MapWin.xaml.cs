﻿using Microsoft.Maps.MapControl.WPF;
using System;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Threading;

namespace Terminal
{
    /// <summary>
    /// Логика взаимодействия для MapWin.xaml
    /// </summary>
    public partial class MapWin : Window
    {
        //Выбранная локация
        private Location location;

        private DispatcherTimer timer;

        public MapWin(Location location)
        {
            InitializeComponent();

            this.location = location;

            SetLocation();
            SetMurker();

            //Закрытие окна из-за бездейстивия
            ComponentDispatcher.ThreadIdle += new EventHandler(ComponentDispatcher_ThreadIdle);
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(180);
            timer.Tick += new EventHandler(Timer_Tick);
        }

        void Timer_Tick(object sender, EventArgs e)
        {
            this.Close();
            timer.Stop();
        }

        void ComponentDispatcher_ThreadIdle(object sender, EventArgs e)
        {
            timer.Start();
        }

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

        private void Road_Button_Click(object sender, RoutedEventArgs e)
        {
            Map.Mode = new RoadMode();
        }

        private void AerialWithLabels_Button_Click(object sender, RoutedEventArgs e)
        {
            Map.Mode = new AerialMode(true);
        }
    }
}