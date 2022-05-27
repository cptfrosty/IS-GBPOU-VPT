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
using System.Xml.Linq;

namespace Terminal
{
    /// <summary>
    /// Логика взаимодействия для AdditionalWin.xaml
    /// </summary>
    public partial class AdditionalWin : Window
    {
        private DispatcherTimer timer;
        public AdditionalWin()
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


        private System.Windows.Point scrollTarget;
        private System.Windows.Point scrollStartPoint;
        private System.Windows.Point scrollStartOffset;


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

        private void Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Init()
        {
            XmlAdditional xmlAdditional = new XmlAdditional();
            xmlAdditional.FindAdditional();
            int count = xmlAdditional.GetCountinformationAdditionalList;

            for (int i = 0; i < count; i++)
            {
                Grid grid = new Grid();

                /* Контейнер */

                //Бордер
                Border border = new Border();
                border.Width = 700;
                border.Height = 200;
                border.Margin = new Thickness(170, 80, 0, 0);
                border.CornerRadius = new CornerRadius(5, 5, 5, 5);

                border.BorderBrush = Brushes.Black;
                border.BorderThickness = new Thickness(3);

                /* Размер шрифта и цвет */

                //Название кружка
                TextBlock atrText = new TextBlock();
                atrText.FontFamily = new FontFamily("Arial");
                atrText.FontWeight = FontWeights.Bold;
                atrText.Foreground = Brushes.Black;
                atrText.FontSize = 28;

                //График работы
                TextBlock graphicText = new TextBlock();
                graphicText.Foreground = Brushes.Black;
                graphicText.FontSize = 20;

                //ФИО преподавателя
                TextBlock fullnameText = new TextBlock();
                fullnameText.Foreground = Brushes.Black;
                fullnameText.FontSize = 18;

                /* Размер, растояние, путь */

                atrText.Text = xmlAdditional.informationAdditionalList[i].nameAttribute.ToString();
                atrText.Margin = new Thickness(20, 10, 0, 0);
                atrText.Width = 660;
                atrText.TextWrapping = TextWrapping.Wrap;

                graphicText.Text = xmlAdditional.informationAdditionalList[i].graphicElement.ToString();
                graphicText.Margin = new Thickness(0, 60, 340, 0);
                graphicText.Height = 90;
                graphicText.Width = 300;
                graphicText.TextWrapping = TextWrapping.Wrap;

                fullnameText.Text = xmlAdditional.informationAdditionalList[i].fullNameElement.ToString();
                fullnameText.Margin = new Thickness(500, 150, 0, 0);
                fullnameText.Height = 30;
                fullnameText.Width = 160;
                fullnameText.TextWrapping = TextWrapping.Wrap;

                grid.Children.Add(atrText);
                grid.Children.Add(graphicText);
                grid.Children.Add(fullnameText);

                border.Child = grid;

                panelAdd.Children.Add(border);

            }
        }
    }
}
