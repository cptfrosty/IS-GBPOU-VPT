using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Логика взаимодействия для OpenQRWin.xaml
    /// </summary>
    public partial class OpenQRWin : Window
    {
        private DispatcherTimer timer;
        public OpenQRWin(Image image)
        {
            InitializeComponent();
            this.imageQR.Source = image.Source;

            ComponentDispatcher.ThreadIdle += new EventHandler(ComponentDispatcher_ThreadIdle);
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(360);
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

        private void Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }       
    }
}
