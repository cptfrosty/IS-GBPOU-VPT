using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Threading;
using Terminal.XmlWindow;

namespace Terminal.Windows
{
    /// <summary>
    /// Логика взаимодействия для Schedule.xaml
    /// </summary>
    public partial class Schedule : Window
    {
        private DispatcherTimer timer;

        public Schedule()
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
            JsonSchedule jsonSchedule = new JsonSchedule();

            List<InformationSchedule> informationScheduleList = jsonSchedule.GetList();

            List<InformationDay> informationDaysList = jsonSchedule.GetDays();

            for (int i = 0; i < informationDaysList.Count; i++)
            {
                Button btn = new Button
                {
                    Width = 261,
                    Height = 210,
                    Margin = new Thickness(90, 170, 15, 15),
                    Tag = $"{informationDaysList[i].nameDay}"
                };

                btn.Click += ScheduleGroups_Click;

                //конвертер для foreground
                var bc = new BrushConverter();

                Label label = new Label
                {
                    Height = 40,
                    FontSize = 18,
                    FontWeight = FontWeights.Bold,
                    Foreground = (Brush)bc.ConvertFrom("#1C2631"),
                    Margin = new Thickness(0, 10, 0, 0),
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Content = $"{informationDaysList[i].nameDay}"
                };
                DockPanel dp = new DockPanel
                {
                    LastChildFill = true
                };
                DockPanel.SetDock(label, Dock.Bottom);
                dp.Children.Add(label);

                //BitmapImage bm = new BitmapImage();
                //bm.BeginInit();
                //bm.UriSource = new Uri(AppDomain.CurrentDomain.BaseDirectory + $"Image/Workshops/{i}.png", UriKind.Relative);
                //bm.EndInit();

                btn.BorderBrush = Brushes.Black;
                btn.BorderThickness = new Thickness(2);

                //ImageBrush imageBrush = new ImageBrush(bm)
                //{
                //    Stretch = Stretch.UniformToFill
                //};

                btn.Background = Brushes.Transparent;

                btn.Content = dp;

                panelDay.Children.Add(btn);
            }
        }

        private void ScheduleGroups_Click(object sender, RoutedEventArgs e)
        {
            string nameButton = ((Button)sender).Tag.ToString();

            ScheduleGroupsWin scheduleGroupsWin = new ScheduleGroupsWin(nameButton);
            scheduleGroupsWin.Show();
        }
    }
}
