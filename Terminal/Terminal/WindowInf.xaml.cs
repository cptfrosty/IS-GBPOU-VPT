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
    /// Логика взаимодействия для WindowInf.xaml
    /// </summary>
    public partial class WindowInf : Window
    {
        private int clickID = 0;

        public WindowInf()
        {
            InitializeComponent();
            Init();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string nameButton = ((Button)sender).Name;

            nameButton = nameButton.Replace("Line", "");

            int choice = int.Parse(nameButton);
 
            FrameForm frameForm = new FrameForm(choice);
            
            frameForm.ShowDialog();
        }

        private void Init()
        {
            for (int i = 0; i < 5; i++)
            {
                Button button = new Button();

                button.Height = 100;
                button.Width = 100;
                button.Margin = new Thickness(12, 12, 12, 12);
                button.Name = "Line" + (i+1).ToString(); //Исправить
                button.Click += Button_Click_1;
                //button.Background = new BitmapImage(new Uri("pack://application:,,,/img/1.jpg"));
                


                panelKorpus.Children.Add(button);
            }
        }
    }
}
