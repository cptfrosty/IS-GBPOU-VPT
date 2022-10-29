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
using Terminal.XmlWindow;

namespace Terminal.Windows
{
    /// <summary>
    /// Логика взаимодействия для ScheduleCurentGroup.xaml
    /// </summary>
    public partial class ScheduleCurentGroup : Window
    {
        private string nameGroup { get; set; }
        private string curentDay { get; set; }

        private DispatcherTimer timer;

        public ScheduleCurentGroup(string nameGroup, string curentDay)
        {
            InitializeComponent();

            this.nameGroup = nameGroup;
            this.curentDay = curentDay;

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

        private void Init()
        {
            List<InformationSchedule> informationSchedulesList = new JsonSchedule().informationScheduleList;

            for (int i = 0; i < informationSchedulesList.Count; i++)
            {
                if (informationSchedulesList[i].nameGroup == nameGroup && informationSchedulesList[i].nameDay == curentDay)
                {
                    tb_Schedule.Text = $"   Группа: {informationSchedulesList[i].nameGroup}\n\n" +
                        $"{informationSchedulesList[i].schedule}";
                }
            }
        }

        void ComponentDispatcher_ThreadIdle(object sender, EventArgs e)
        {
            timer.Start();
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
