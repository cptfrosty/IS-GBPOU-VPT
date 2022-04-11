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
using System.Xml.Linq;

namespace Terminal
{
    /// <summary>
    /// Логика взаимодействия для MuseumWin.xaml
    /// </summary>
    public partial class MuseumWin : Window
    {
        public MuseumWin()
        {
            InitializeComponent();
            Init();
        }
        private void Init()
        {
            XmlMuseum xmlMuseum = new XmlMuseum();
            int count = XmlMuseum.Instance().GetCountMuseum;
            for (int i = 1; i <= count; i++)
            {
                Button btn = new Button();
                btn.Width = 211;
                btn.Height = 160;
                btn.Margin = new Thickness(15, 100, 15, 15);

                btn.Name = "id" + i;

                btn.Click += MuseumInformation;

                Label label = new Label();
                label.Height = 40;
                label.Margin = new Thickness(0, 130, 0, 0);
                label.Content = "";
                DockPanel dp = new DockPanel();
                dp.LastChildFill = true;
                DockPanel.SetDock(label, Dock.Bottom);
                dp.Children.Add(label);

                BitmapImage bm = new BitmapImage();
                bm.BeginInit();
                bm.UriSource = new Uri(AppDomain.CurrentDomain.BaseDirectory + $"Image/Museum/{i}.jpg", UriKind.Relative);
                bm.EndInit();

                ImageBrush imageBrush = new ImageBrush(bm);
                imageBrush.Stretch = Stretch.UniformToFill;
                btn.Background = imageBrush;

                btn.Content = dp;

                panelMuseum.Children.Add(btn);
            }
        }
        // Задержка при клике
        private void MuseumInformation(object sender, RoutedEventArgs e)
        {
            string nameButton = ((Button)sender).Name;

            _ = MethodWithDelayAsync(nameButton, 400);
        }
        public async Task MethodWithDelayAsync(string btnName, int milliseconds)
        {
            await Task.Delay(milliseconds);

            btnName = btnName.Replace("id", "");
            int choice = int.Parse(btnName);

            MuseumInf museumInf = new MuseumInf(choice);
            museumInf.ShowDialog();
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
