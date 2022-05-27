using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Microsoft.Maps.MapControl.WPF;

namespace Terminal
{
    /// <summary>
    /// Логика взаимодействия для FrameForm.xaml
    /// </summary>
    public partial class FrameForm : Window
    {
        public int Choice;
        public Location curentLocation;
        /// <summary>
        /// Конструктор формы
        /// </summary>
        /// <param name="id">id позиции</param>
        public FrameForm(int id)
        {
            InitializeComponent();
            this.Choice = id;

            //PS ссылка на картинку в XML файле должна быть
            Picture.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + $"Image/Corp/{id}.jpg"));

            PictureMapping.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + $"Image/Corp/{id + 100}.jpg"));

            InitButton(id);

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

        public void InitButton(int id)
        {
            CollegeBuilding currentCorp = XmlCollegeBuilding.Instance().GetCollegeBuilding[id-1];
            PlaceInfo(currentCorp);
        }

        /// <summary>
        /// Информация о корпусе
        /// </summary>
        /// <param name="nameAttribute"></param>
        /// <param name="adressElement"></param>
        /// <param name="telephoneElement"></param>
        /// <param name="emailElement"></param>
        /// <param name="graphicElement"></param>
        public void PlaceInfo(CollegeBuilding corp)
        {
            CorpLabel.Content = corp.nameAttribute;
            AdressLabel.Text = corp.adressElement;
            TelephoneLabel.Content = corp.telephoneElement;
            EmailLabel.Content = corp.emailElement;
            GraphicLabel.Content = corp.workSchedule;
            HistoryLabel.Text = corp.history;
            curentLocation = corp.location;
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        //TEST
        private System.Windows.Point scrollTarget;
        private System.Windows.Point scrollStartPoint;
        private System.Windows.Point scrollStartOffset;
        private DispatcherTimer timer;

        protected override void OnPreviewMouseDown(MouseButtonEventArgs e)
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

        private void Button_OpenPicture(object sender, RoutedEventArgs e)
        {
            Image image = new Image();

            string nameButton = ((Button)sender).Name;

            if (nameButton == "Button_1")
            {
                try
                {
                    image.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + $"Image/Corp/{Choice}.jpg"));
                }
                catch (System.IO.FileNotFoundException)
                {
                    image.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + $"Image/Workshops/Room/mistake.jpg"));
                }
            }
            if (nameButton == "Button_2")
            {
                try
                {
                    image.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + $"Image/Corp/{Choice + 100}.jpg"));
                }
                catch (System.IO.FileNotFoundException)
                {
                    image.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + $"Image/Workshops/Room/mistake.jpg"));
                }
            }

            OpenPictureWin openPictureWin = new OpenPictureWin(image);
            openPictureWin.Show();
        }

        private void Button_OpenMap(object sender, RoutedEventArgs e)
        {
            MapWin mapWin = new MapWin(curentLocation);
            mapWin.ShowDialog();
        }
    }
}
