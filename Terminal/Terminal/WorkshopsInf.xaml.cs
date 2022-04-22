using System;
using System.Collections.Generic;
using System.IO;
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

        public bool isCheck = false;
        public WorkshopsInf(int id)
        {
            choice = id;
            InitializeComponent();

            string path1 = $"Image/Workshops/Room/{id}.png";
            string path2 = $"Image/Workshops/Room/{id + 100}.png";
            string path3 = $"Image/Workshops/Room/{id + 1000}.png";

            FileInfo fileInfo1 = new FileInfo(path1);
            FileInfo fileInfo2 = new FileInfo(path2);
            FileInfo fileInfo3 = new FileInfo(path3);

            if (fileInfo1.Exists)
                Picture_1.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + $"Image/Workshops/Room/{id}.png"));
            if (fileInfo2.Exists)
                Picture_2.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + $"Image/Workshops/Room/{id+100}.png"));
            if (fileInfo3.Exists)
                Picture_3.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + $"Image/Workshops/Room/{id+1000}.png"));

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



        //Расширить картинку
        private void Button_Test(object sender, RoutedEventArgs e)
        {
            if (!isCheck)
            {
                Border_1.Visibility = Visibility.Hidden;

                Picture_1.Margin = new Thickness(Top);
                Button_1.Margin = new Thickness(Top);

                Picture_1.Width = 910;
                Picture_1.Height = 1000;
                Button_1.Width = 910;
                Button_1.Height = 1000;
                isCheck = true;
            }
            else
            {
                Border_1.Visibility = Visibility.Visible;

                Picture_1.Width = 310;
                Picture_1.Height = 488;
                Button_1.Width = 310;
                Button_1.Height = 310;
                isCheck = false;
            }
        }
    }
}
