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
        public int countCorp;

        //public List<InfoCorp> ListInfoCorp = new List<InfoCorp>();
        public WindowInf()
        {
            InitializeComponent();
            Init();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Init()
        {
            XmlCollegeBuilding xmlCorps = new XmlCollegeBuilding();
            int count = XmlCollegeBuilding.Instance().GetCountCorp;

            for (int i = 1; i <= count; i++)
            {
                Button btn = new Button();               
                btn.Width = 211;
                btn.Height = 160;
                btn.Margin = new Thickness(15, 100, 15, 15);
                
                btn.Name = "id" + i;

                btn.Click += CollegeBuildingInformation;

                Label label = new Label();
                label.Height = 40;
                label.Margin = new Thickness(0, 130, 0, 0);
                label.Content = $"Корпус {i}";
                DockPanel dp = new DockPanel();
                dp.LastChildFill = true;
                DockPanel.SetDock(label, Dock.Bottom);
                dp.Children.Add(label);

                BitmapImage bm = new BitmapImage();
                bm.BeginInit();
                bm.UriSource = new Uri("Icon/logo2.png", UriKind.Relative);
                bm.EndInit();
                
                ImageBrush imageBrush = new ImageBrush(bm);
                imageBrush.Stretch = Stretch.UniformToFill;
                btn.Background = imageBrush;

                btn.Content = dp;

                panelKorpus.Children.Add(btn);
            }
        }

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
