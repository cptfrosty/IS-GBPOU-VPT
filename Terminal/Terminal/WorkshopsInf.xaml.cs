﻿using System;
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
    /// Логика взаимодействия для WorkshopsInf.xaml
    /// </summary>
    public partial class WorkshopsInf : Window
    {
        public int choice;
        public WorkshopsInf(int id)
        {
            choice = id;
            InitializeComponent();

            Picture_1.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + $"Image/Workshops/Room/{id}.png"));
            
            //Добавить картинки

            //Picture_2.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + $"Image/Workshops/Room/{id+100}.png"));
            //Picture_3.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + $"Image/Workshops/Room/{id+200}.png"));

            InitButton(id);
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
    }
}
