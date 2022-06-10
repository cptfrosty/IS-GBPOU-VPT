using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace Terminal
{
    /// <summary>
    /// Логика взаимодействия для WindowInf.xaml
    /// </summary>
    public partial class WindowInf : Window
    {
        public int countCorp;

        private DispatcherTimer timer;

        public WindowInf()
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

        // Кнопка выхода
        private void Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        // Создание кнопок
        private void Init()
        {
            XmlCollegeBuilding xmlCorps = new XmlCollegeBuilding();
            int count = XmlCollegeBuilding.Instance().GetCountCorp;

            for (int i = 1; i <= count; i++)
            {
                Button btn = new Button
                {
                    Width = 261,
                    Height = 210,
                    Margin = new Thickness(15, 100, 15, 15),

                    Name = "id" + i
                };

                btn.Click += CollegeBuildingInformation;

                Label label = new Label
                {
                    FontSize = 22,
                    Height = 40,
                    Margin = new Thickness(0, 160, 0, 0),
                    Content = $"Корпус {i}"
                };
                DockPanel dp = new DockPanel
                {
                    LastChildFill = true
                };
                DockPanel.SetDock(label, Dock.Bottom);
                dp.Children.Add(label);
               
                BitmapImage bm = new BitmapImage();
                bm.BeginInit();
                bm.UriSource = new Uri("Icon/logo2.png", UriKind.Relative);
                bm.EndInit();
             
                btn.BorderBrush = Brushes.Black;
                btn.BorderThickness = new Thickness(2);

                ImageBrush imageBrush = new ImageBrush(bm)
                {
                    Stretch = Stretch.UniformToFill
                };
                btn.Background = imageBrush;

                btn.Content = dp;

                panelKorpus.Children.Add(btn);
            }
        }
        // Задержка при клике
        private void CollegeBuildingInformation(object sender, RoutedEventArgs e)
        {
            string nameButton = ((Button)sender).Name;

            _ = MethodWithDelayAsync(nameButton, 400);
        }
        public async Task MethodWithDelayAsync(string btnName,int milliseconds)
        {
            await Task.Delay(milliseconds);

            btnName = btnName.Replace("id", "");
            int choice = int.Parse(btnName);

            FrameForm frameForm = new FrameForm(choice);
            frameForm.ShowDialog();
        }
    }
}
