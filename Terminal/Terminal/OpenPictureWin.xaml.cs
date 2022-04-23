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
    /// Логика взаимодействия для OpenPictureWin.xaml
    /// </summary>
    public partial class OpenPictureWin : Window
    {
        public OpenPictureWin(string name, int id)
        {
            InitializeComponent();

            if (name == "Button_1")
            {
                try
                {
                    FullPicture.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + $"Image/Workshops/Room/{id}.png"));
                }
                catch (System.IO.FileNotFoundException)
                {
                    FullPicture.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + $"Image/Workshops/Room/mistake.jpg"));
                }
            }               
            if (name == "Button_2")
            {
                try
                {
                    FullPicture.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + $"Image/Workshops/Room/{id+100}.png"));
                }
                catch (System.IO.FileNotFoundException)
                {
                    FullPicture.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + $"Image/Workshops/Room/mistake.jpg"));
                }
            }
            if (name == "Button_3")
            {
                try
                {
                    FullPicture.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + $"Image/Workshops/Room/{id+1000}.png"));
                }
                catch (System.IO.FileNotFoundException)
                {
                    FullPicture.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + $"Image/Workshops/Room/mistake.jpg"));
                }
            }
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
