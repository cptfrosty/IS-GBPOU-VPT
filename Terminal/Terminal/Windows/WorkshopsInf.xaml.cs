﻿using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace Terminal
{
    /// <summary>
    /// Логика взаимодействия для WorkshopsInf.xaml
    /// </summary>
    public partial class WorkshopsInf : Window
    {
        public int choice;
        int id;

        public bool isCheck = false;

        private DispatcherTimer timer;

        public WorkshopsInf(int id)
        {
            InitializeComponent();

            this.id = id;

            choice = id;

            string path1 = $"Image/Workshops/Room/{id}.png";
            string path2 = $"Image/Workshops/Room/{id + 100}.png";
            string path3 = $"Image/Workshops/Room/{id + 1000}.png";

            FileInfo fileInfo1 = new FileInfo(path1);
            FileInfo fileInfo2 = new FileInfo(path2);
            FileInfo fileInfo3 = new FileInfo(path3);

            if (fileInfo1.Exists)
                Picture_1.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + $"Image/Workshops/Room/{id}.png"));
            if (fileInfo2.Exists)
                Picture_2.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + $"Image/Workshops/Room/{id + 100}.png"));
            if (fileInfo3.Exists)
                Picture_3.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + $"Image/Workshops/Room/{id + 1000}.png"));

            InitButton(id);

            //Закрытие окна из-за бездейстивия
            ComponentDispatcher.ThreadIdle += new EventHandler(ComponentDispatcher_ThreadIdle);
            timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(180)
            };
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

        public void InitButton(int id)
        {
            InformationWorkshops informationWorkshops = WorkshopsXml.Instance().GetWorkshopsInfo[id - 1];
            PlaceInfo(informationWorkshops);
        }

        /// <param name="nameAttribute"></param>
        /// <param name="dataElement"></param>
        public void PlaceInfo(InformationWorkshops wor)
        {
            NameLabel.Content = wor.nameAttribute;
            Data.Text = wor.dataElement;
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //Расширить картинку
        private void Button_OpenPicture(object sender, RoutedEventArgs e)
        {
            Image image = new Image();

            string nameButton = ((Button)sender).Name;

            if (nameButton == "Button_1")
            {
                try
                {
                    image.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + $"Image/Workshops/Room/{id}.png"));
                }
                catch (FileNotFoundException)
                {
                    image.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + $"Image/Workshops/Room/mistake.jpg"));
                }
            }
            if (nameButton == "Button_2")
            {
                try
                {
                    image.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + $"Image/Workshops/Room/{id + 100}.png"));
                }
                catch (FileNotFoundException)
                {
                    image.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + $"Image/Workshops/Room/mistake.jpg"));
                }
            }
            if (nameButton == "Button_3")
            {
                try
                {
                    image.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + $"Image/Workshops/Room/{id + 1000}.png"));
                }
                catch (FileNotFoundException)
                {
                    image.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + $"Image/Workshops/Room/mistake.jpg"));
                }
            }
            OpenPictureWin openPictureWin = new OpenPictureWin(image);
            openPictureWin.Show();
        }
    }
}