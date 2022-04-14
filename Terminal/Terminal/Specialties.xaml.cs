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
    /// Interaction logic for Specialties.xaml
    /// </summary>
    public partial class Specialties : Window
    {
        public Specialties()
        {
            InitializeComponent();
            Init();
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Init()
        {
            XmlSpecialties xmlSpecialties = new XmlSpecialties();
            int count = XmlSpecialties.Instance().GetCountSpecialties;

            for (int i = 1; i <= count; i++)
            {
                Button btn = new Button();
                btn.Width = 211;
                btn.Height = 160;
                btn.Margin = new Thickness(15, 100, 15, 15);

                btn.Name = "id" + i;

                btn.Click += SpecialtiesInformation;

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
                bm.UriSource = new Uri(AppDomain.CurrentDomain.BaseDirectory + $"Image/Specialties/{i}.jpg", UriKind.Relative);
                bm.EndInit();


                btn.BorderBrush = Brushes.Black;
                btn.BorderThickness = new Thickness(2);


                ImageBrush imageBrush = new ImageBrush(bm);
                imageBrush.Stretch = Stretch.UniformToFill;
                btn.Background = imageBrush;

                btn.Content = dp;

                panelSpecialties.Children.Add(btn);
            }
        }
        private void SpecialtiesInformation(object sender, RoutedEventArgs e)
        {
            string nameButton = ((Button)sender).Name;

            _ = MethodWithDelayAsync(nameButton, 400);
        }
        public async Task MethodWithDelayAsync(string btnName, int milliseconds)
        {
            await Task.Delay(milliseconds);

            btnName = btnName.Replace("id", "");
            int choice = int.Parse(btnName);

            SpecialtiesInf specialtiesInf = new SpecialtiesInf(choice);
            specialtiesInf.ShowDialog();
        }
    }
}
