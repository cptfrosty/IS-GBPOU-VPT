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
    /// Interaction logic for SpecialtiesInf.xaml
    /// </summary>
    public partial class SpecialtiesInf : Window
    {
        public int choice;
        public SpecialtiesInf(int id)
        {
            choice = id;
            InitializeComponent();

            Picture.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + $"Image/Specialties/{id}.jpg"));

            InitButton(id);
        }
        public void InitButton(int id)
        {
            InformationSpecialties informationSpecialties = XmlSpecialties.Instance().GetSpecialtiesInfo[id - 1];
            PlaceInfo(informationSpecialties);
        }
        public void PlaceInfo(InformationSpecialties spec)
        {
            NameLabel.Content = spec.nameAttribute;
            Data.Text = spec.dataElement;
        }
        private void Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
