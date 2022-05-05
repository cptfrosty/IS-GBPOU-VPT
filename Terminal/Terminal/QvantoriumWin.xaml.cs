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
                atrText.Text = info.Attribute("name").Value;
                adressText.Text = info.Element("adress").Value;
                desText.Text = info.Element("des").Value;
            }
        }
    }
}
