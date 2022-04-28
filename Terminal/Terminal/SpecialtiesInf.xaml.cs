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
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Terminal
{
    /// <summary>
    /// Interaction logic for SpecialtiesInf.xaml
    /// </summary>
    public partial class SpecialtiesInf : Window
    {
        private string nameBtn;
        private DispatcherTimer timer;

        public SpecialtiesInf(string nameBtn)
        {
            this.nameBtn = nameBtn;
            InitializeComponent();

            //Picture.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + $"Image/Specialties/{id}.jpg"));

            //Закрытие окна из-за бездейстивия
            ComponentDispatcher.ThreadIdle += new EventHandler(ComponentDispatcher_ThreadIdle);
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(180);
            timer.Tick += new EventHandler(timer_Tick);
        }

        void timer_Tick(object sender, EventArgs e)
        {
            this.Close();
            timer.Stop();
        }

        void ComponentDispatcher_ThreadIdle(object sender, EventArgs e)
        {
            timer.Start();
        }

        private void PlaceInfo()
        {

        }
       
        
        private void Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_OpenPicture(object sender, RoutedEventArgs e)
        {
            Image image = new Image();

            string nameButton = ((Button)sender).Name;

            if (nameButton == "Button_1")
            {
                try
                {
                    //image.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + $"Image/Specialties/{id}.jpg"));
                }
                catch (System.IO.FileNotFoundException)
                {
                    image.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + $"Image/Workshops/Room/mistake.jpg"));
                }
            }
            OpenPictureWin openPictureWin = new OpenPictureWin(image);
            openPictureWin.Show();
        }
    }
}
