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
    /// Interaction logic for Specialties.xaml
    /// </summary>
    public partial class Specialties : Window
    {
        private DispatcherTimer timer;

        public Specialties()
        {
            InitializeComponent();
            Init();

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

        private void Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Init()
        {
            XmlSpecialties xmlSpecialties = new XmlSpecialties();
            xmlSpecialties.FindDirection();
            int count = xmlSpecialties.GetCountDirButton;

            for (int i = 0; i < count; i++)
            {
                Button btn = new Button();
                btn.Width = 300;
                btn.Height = 250;
                btn.Margin = new Thickness(70, 40, 0, 0);

                btn.Name = xmlSpecialties.dirButtonList[i].nameAttribute.ToString();

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
                bm.UriSource = new Uri(AppDomain.CurrentDomain.BaseDirectory + $"Image/Specialties/SpecialtyDirection/{btn.Name}.png", UriKind.Relative);
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

            //btnName = btnName.Replace("id","");
            //int choice = int.Parse(btnName);

            SpecialtiesChoice specialtiesChoice = new SpecialtiesChoice(btnName);
            specialtiesChoice.Show();
        }
    }
}
