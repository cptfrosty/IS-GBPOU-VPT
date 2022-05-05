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
    /// Логика взаимодействия для OpenQRWin.xaml
    /// </summary>
    public partial class OpenQRWin : Window
    {

        public OpenQRWin(Image image)
        {
            InitializeComponent();
            this.imageQR.Source = image.Source;
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
