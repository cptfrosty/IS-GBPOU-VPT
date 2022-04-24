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
    /// Логика взаимодействия для Workshops.xaml
    /// </summary>
    public partial class Workshops : Window
    {
        private DispatcherTimer timer;

        public Workshops()
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

        private void Init()
        {
            WorkshopsXml workshopsXml = new WorkshopsXml();
            int count = WorkshopsXml.Instance().GetCountWorkshops;
            for (int i = 1; i <= count; i++)
            {
                Button btn = new Button();
                btn.Width = 261;
                btn.Height = 210;
                btn.Margin = new Thickness(90, 600, 15, 15);

                btn.Name = "id" + i;

                btn.Click += WorkshopsInformation;

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
                bm.UriSource = new Uri(AppDomain.CurrentDomain.BaseDirectory + $"Image/Workshops/{i}.png", UriKind.Relative);
                bm.EndInit();

                btn.BorderBrush = Brushes.Black;
                btn.BorderThickness = new Thickness(2);

                ImageBrush imageBrush = new ImageBrush(bm);
                imageBrush.Stretch = Stretch.UniformToFill;
                btn.Background = imageBrush;

                btn.Content = dp;

                panelWorkshop.Children.Add(btn);
            }
        }
        private void WorkshopsInformation(object sender, RoutedEventArgs e)
        {
            string nameButton = ((Button)sender).Name;

            _ = MethodWithDelayAsync(nameButton, 400);
        }
        public async Task MethodWithDelayAsync(string btnName, int milliseconds)
        {
            await Task.Delay(milliseconds);

            btnName = btnName.Replace("id", "");
            int choice = int.Parse(btnName);

            WorkshopsInf workshopsInf = new WorkshopsInf(choice);
            workshopsInf.ShowDialog();
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
