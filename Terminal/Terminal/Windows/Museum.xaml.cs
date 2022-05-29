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
    /// Логика взаимодействия для Museum2.xaml
    /// </summary>
    public partial class Museum2 : Window
    {
        private DispatcherTimer timer;
        public Museum2()
        {
            InitializeComponent();

            Init();


             
            VideoBlock.Source = new Uri(AppDomain.CurrentDomain.BaseDirectory + $"Video/BackgroundVideo.mp4");
            VideoBlock.Position = new TimeSpan(0, 0, 3);
            VideoBlock.Play();
            VideoBlock.MediaEnded += VideoBlock_MediaEnded;

            ComponentDispatcher.ThreadIdle += new EventHandler(ComponentDispatcher_ThreadIdle);
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(360);
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

        private void VideoBlock_MediaEnded(object sender, RoutedEventArgs e)
        {
            VideoBlock.Position = new TimeSpan(0, 0, 1);
            VideoBlock.Play();
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Init()
        {
            TextBlock.Text = XmlMuseum2.Instance().GetMuseumInfo[0].dataElement;
        }

        private System.Windows.Point scrollTarget;
        private System.Windows.Point scrollStartPoint;
        private System.Windows.Point scrollStartOffset;
        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (scrollViewer.IsMouseOver)
            {
                // Save starting point, used later when determining how much to scroll.
                scrollStartPoint = e.GetPosition(this);
                scrollStartOffset.X = scrollViewer.HorizontalOffset;
                scrollStartOffset.Y = scrollViewer.VerticalOffset;

                // Update the cursor if can scroll or not.
                this.Cursor = (scrollViewer.ExtentWidth > scrollViewer.ViewportWidth) ||
                    (scrollViewer.ExtentHeight > scrollViewer.ViewportHeight) ?
                    Cursors.ScrollAll : Cursors.Arrow;

                this.CaptureMouse();
            }

            base.OnPreviewMouseDown(e);
        }

        protected override void OnPreviewMouseMove(MouseEventArgs e)
        {
            if (this.IsMouseCaptured)
            {
                System.Windows.Point currentPoint = e.GetPosition(this);

                // Determine the new amount to scroll.
                System.Windows.Point delta = new System.Windows.Point(scrollStartPoint.X -
                    currentPoint.X, scrollStartPoint.Y - currentPoint.Y);

                scrollTarget.X = scrollStartOffset.X + delta.X;
                scrollTarget.Y = scrollStartOffset.Y + delta.Y;

                // Scroll to the new position.
                scrollViewer.ScrollToHorizontalOffset(scrollTarget.X);
                scrollViewer.ScrollToVerticalOffset(scrollTarget.Y);
            }

            base.OnPreviewMouseMove(e);
        }

        protected override void OnPreviewMouseUp(MouseButtonEventArgs e)
        {
            if (this.IsMouseCaptured)
            {
                this.Cursor = Cursors.Arrow;
                this.ReleaseMouseCapture();
            }

            base.OnPreviewMouseUp(e);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MuseumWin museumWin = new MuseumWin();
            museumWin.ShowDialog();

        }
    }
}
