using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Threading;

namespace Terminal
{
    /// <summary>
    /// Логика взаимодействия для Museum2.xaml
    /// </summary>
    public partial class Museum2 : Window
    {
        private DispatcherTimer timer;

        private Point scrollTarget;
        private Point scrollStartPoint;
        private Point scrollStartOffset;
        public Museum2()
        {
            InitializeComponent();

            Init();

            //Запуск видео с 3 секунды
            VideoBlock.Source = new Uri(AppDomain.CurrentDomain.BaseDirectory + $"Video/BackgroundVideo.mp4");
            VideoBlock.Position = new TimeSpan(0, 0, 3);
            VideoBlock.Play();
            VideoBlock.MediaEnded += VideoBlock_MediaEnded;

            //Закрытие окна из-за бездействия 
            ComponentDispatcher.ThreadIdle += new EventHandler(ComponentDispatcher_ThreadIdle);
            timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(360)
            };
            timer.Tick += new EventHandler(Timer_Tick);
        }
        void Timer_Tick(object sender, EventArgs e)
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
                Point currentPoint = e.GetPosition(this);

                // Determine the new amount to scroll.
                Point delta = new System.Windows.Point(scrollStartPoint.X -
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
