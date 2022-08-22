using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Threading;

namespace Terminal
{
    /// <summary>
    /// Логика взаимодействия для FAQWin.xaml
    /// </summary>
    public partial class FAQWin : Window
    {
        private DispatcherTimer timer;

        private Point scrollTarget;
        private Point scrollStartPoint;
        private Point scrollStartOffset;

        public FAQWin()
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
                Point currentPoint = e.GetPosition(this);

                // Determine the new amount to scroll.
                Point delta = new Point(scrollStartPoint.X -
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

        private void Init()
        {
            XmlFaq xmlFaq = new XmlFaq();
            xmlFaq.FindFaq();
            int count = xmlFaq.GetCountinformationinformationXmlFaqList;         

            for (int i = 0; i < count; i++)
            {
                Grid grid = new Grid();

                /* Контейнер */

                //Бордер
                Border border = new Border();
                border.Width = 1750;
                border.Margin = new Thickness(65, 40, 0, 0);
                border.CornerRadius = new CornerRadius(5, 5, 5, 5);

                border.BorderBrush = Brushes.Black;
                border.BorderThickness = new Thickness(3);

                /* Размер шрифта и цвет */

                //Вопрос
                TextBlock atrText = new TextBlock
                {
                    FontFamily = new FontFamily("Arial"),
                    FontWeight = FontWeights.Bold,
                    Foreground = Brushes.Black,
                    FontSize = 28
                };

                //Ответ на вопрос
                TextBlock answerText = new TextBlock
                {
                    Foreground = Brushes.Black,
                    FontSize = 22
                };

                /* Размер, растояние, путь */

                atrText.Text = xmlFaq.informationXmlFaqList[i].nameAttribute.ToString();
                atrText.Margin = new Thickness(35, 5, 0, 0);
                atrText.VerticalAlignment = VerticalAlignment.Top;
                atrText.Width = 1600;
                atrText.MaxHeight = 40;
                atrText.TextWrapping = TextWrapping.Wrap;

                answerText.Text = xmlFaq.informationXmlFaqList[i].answerElement.ToString();
                answerText.Width = 1600;
                answerText.Margin = new Thickness(0, 50, 0, 0);
                answerText.MaxHeight = 500;
                //Бордер завист от ответа
                border.Height = answerText.Height + 100;
                answerText.TextWrapping = TextWrapping.Wrap;

                //Установка размера wrapPanel
                panelFaq.Height += border.Height;

                grid.Children.Add(atrText);
                grid.Children.Add(answerText);

                border.Child = grid;

                panelFaq.Children.Add(border);
            }
        }
    }
}
