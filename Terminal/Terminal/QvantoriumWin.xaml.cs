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
using System.Xml.Linq;

namespace Terminal
{
    /// <summary>
    /// Логика взаимодействия для QvantoriumWin.xaml
    /// </summary>
    public partial class QvantoriumWin : Window
    {
        public QvantoriumWin()
        {
            InitializeComponent();
            PlaceInfo();
          
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void PlaceInfo()
        {
            XDocument xdoc = XDocument.Load(AppDomain.CurrentDomain.BaseDirectory + "Frame/Qvantorium.xml");
            foreach (XElement info in xdoc.Element("informations").Elements("info"))
            {                                                                    
                atrText.Content = info.Attribute("name").Value;
                adressText.Text = info.Element("adress").Value;
                desText.Text = info.Element("des").Value;
            }
        }

        //TEST
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
    }
}
