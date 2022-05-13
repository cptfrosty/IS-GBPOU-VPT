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
                btn.Width = 230;
                btn.Height = 180;
                btn.Margin = new Thickness(80, 60, 0, 0);

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

            SpecialtiesChoice specialtiesChoice = new SpecialtiesChoice(btnName);
            specialtiesChoice.Show();
        }

        //private System.Windows.Point scrollTarget;
        //private System.Windows.Point scrollStartPoint;
        //private System.Windows.Point scrollStartOffset;


        //protected override void OnPreviewMouseDown(MouseButtonEventArgs e)
        //{
        //    if (scrollViewer.IsMouseOver)
        //    {
        //        // Save starting point, used later when determining how much to scroll.
        //        scrollStartPoint = e.GetPosition(this);
        //        scrollStartOffset.X = scrollViewer.HorizontalOffset;
        //        scrollStartOffset.Y = scrollViewer.VerticalOffset;

        //        // Update the cursor if can scroll or not.
        //        this.Cursor = (scrollViewer.ExtentWidth > scrollViewer.ViewportWidth) ||
        //            (scrollViewer.ExtentHeight > scrollViewer.ViewportHeight) ?
        //            Cursors.ScrollAll : Cursors.Arrow;

        //        this.CaptureMouse();
        //    }

        //    base.OnPreviewMouseDown(e);
        //}


    //    protected override void OnPreviewMouseMove(MouseEventArgs e)
    //    {
    //        if (this.IsMouseCaptured)
    //        {
    //            System.Windows.Point currentPoint = e.GetPosition(this);

    //            // Determine the new amount to scroll.
    //            System.Windows.Point delta = new System.Windows.Point(scrollStartPoint.X -
    //                currentPoint.X, scrollStartPoint.Y - currentPoint.Y);

    //            scrollTarget.X = scrollStartOffset.X + delta.X;
    //            scrollTarget.Y = scrollStartOffset.Y + delta.Y;

    //            // Scroll to the new position.
    //            scrollViewer.ScrollToHorizontalOffset(scrollTarget.X);
    //            scrollViewer.ScrollToVerticalOffset(scrollTarget.Y);
    //        }

    //        base.OnPreviewMouseMove(e);
    //    }

    //    protected override void OnPreviewMouseUp(MouseButtonEventArgs e)
    //    {
    //        if (this.IsMouseCaptured)
    //        {
    //            this.Cursor = Cursors.Arrow;
    //            this.ReleaseMouseCapture();
    //        }

    //        base.OnPreviewMouseUp(e);
    //    }
    }
}
