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
    /// Логика взаимодействия для ScheduleGroupsWin.xaml
    /// </summary>
    public partial class ScheduleGroupsWin : Window
    {
        private DispatcherTimer timer;

        private string curentDay { get; set; }

        public ScheduleGroupsWin(string nameBtn)
        {
            InitializeComponent();

            curentDay = nameBtn;

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
            List<InformationSchedule> informationScheduleList = new JsonSchedule().informationScheduleList;

            for (int i = 0; i < informationScheduleList.Count; i++)
            {
                if (informationScheduleList[i].nameDay == curentDay)
                {
                    Button btn = new Button
                    {
                        Width = 120,
                        Height = 70,
                        Margin = new Thickness(20,20,20,20),
                        Tag = $"{informationScheduleList[i].nameGroup}"
                    };

                    btn.Click += ScheduleCurentGroup_Click;

                    //конвертер для foreground
                    var bc = new BrushConverter();

                    Label label = new Label
                    {
                        Height = 40,
                        FontSize = 12,
                        FontWeight = FontWeights.Bold,
                        Foreground = (Brush)bc.ConvertFrom("#1C2631"),
                        Margin = new Thickness(0, 10, 0, 0),
                        VerticalAlignment = VerticalAlignment.Center,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        Content = $"{informationScheduleList[i].nameGroup}"
                    };
                    DockPanel dp = new DockPanel
                    {
                        LastChildFill = true
                    };
                    DockPanel.SetDock(label, Dock.Bottom);
                    dp.Children.Add(label);

                    btn.BorderBrush = Brushes.Black;
                    btn.BorderThickness = new Thickness(2);

                    btn.Background = Brushes.Transparent;

                    btn.Content = dp;

                    panelGroups.Children.Add(btn);
                }
            }
        }

        private void ScheduleCurentGroup_Click(object sender, RoutedEventArgs e)
        {
            string nameButton = ((Button)sender).Tag.ToString();

            ScheduleCurentGroup scheduleCurentGroup = new ScheduleCurentGroup(nameButton, curentDay);
            scheduleCurentGroup.Show();
        }
    }
}
