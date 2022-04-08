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

            for (int i = 1; i <= XmlCollegeBuilding.Instance().GetCountCorp; i++)
            {

                Button btn = new Button();               
                btn.Width = 211;
                btn.Height = 160;
                btn.Margin = new Thickness(15, 100, 15, 15);
                
                btn.Name = "id" + i;

                Grid grid = new Grid();
                RowDefinition rowImage = new RowDefinition();
                RowDefinition rowText = new RowDefinition();
                rowImage.Height = new GridLength(120);
                rowText.Height = new GridLength(60);
                grid.RowDefinitions.Add(rowImage);
                grid.RowDefinitions.Add(rowText);

                btn.Click += CollegeBuildingInformation;

                //StackPanel sp = new StackPanel();

                BitmapImage bm = new BitmapImage();
                bm.BeginInit();
                bm.UriSource = new Uri("Icon/logo2copy.png", UriKind.Relative);
                bm.EndInit();
                

                Image img = new Image();
                img.Source = bm;
                img.Width = 140;
                img.Height = 140;

                ImageBrush imageBrush = new ImageBrush(bm);
                btn.Background = imageBrush;

                Label label = new Label();
                label.Content = $"Корпус {i}";

                btn.Content = grid;
                Grid.SetRow(img, 0);
                Grid.SetRow(label, 1);

                grid.Children.Add(img);
                grid.Children.Add(label);
                grid.Children.Add(img);
                //sp.Children.Add(label);

                panelKorpus.Children.Add(btn);

            }
        }


        private void CollegeBuildingInformation(object sender, RoutedEventArgs e)
        {
            string nameButton = ((Button)sender).Name;

            _ = MethodWithDelayAsync(nameButton,400);
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
