using System;
using System.Collections.Generic;
using System.Drawing;
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
using System.Security.Policy;
using Microsoft.Win32;
using System.Xml.Linq;
using System.IO;

namespace Terminal
{
    /// <summary>
    /// Логика взаимодействия для FrameForm.xaml
    /// </summary>
    public partial class FrameForm : Window
    {
        public int Choice;
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
            
            InitButton(id);
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
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
