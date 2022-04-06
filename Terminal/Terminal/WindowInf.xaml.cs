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

            nameButton = nameButton.Replace("id", "");

            int choice = int.Parse(nameButton);
 
            FrameForm frameForm = new FrameForm(choice);
            
            frameForm.ShowDialog();
        }

        private void Init()
        {
            for (int i = 1; i <= 4; i++)
            {
                Button btn = new Button();
                btn.Width = 211;
                btn.Height = 160;
                btn.Margin = new Thickness(15, 100, 15, 15);
                btn.Background = Brushes.White;
                btn.Name = "id" + i;

                btn.Click += Button_Click_1;

                StackPanel sp = new StackPanel();

                BitmapImage bm = new BitmapImage();
                bm.BeginInit();
                bm.UriSource = new Uri("Icon/logo2.png", UriKind.Relative);
                bm.EndInit();

                Image img = new Image();
                img.Source = bm;
                img.Width = 70;
                img.Height = 70;

                Label label = new Label();
                label.Content = $"Корпус {i}";

                btn.Content = sp;
                sp.Children.Add(img);
                sp.Children.Add(label);

                panelKorpus.Children.Add(btn);

            }
        }
    }
}
