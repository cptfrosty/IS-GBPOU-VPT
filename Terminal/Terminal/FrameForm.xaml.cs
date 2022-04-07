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
            //Picture.Source = new BitmapImage(new Uri($"pack://application:,,,/{id}.jpg"));
            Picture.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + $"Image/Corp/{id}.jpg"));
            


            ShowInfo(id);

            
            
        }

        public void ShowInfo(int id)
        {
            XDocument xdoc = XDocument.Load(AppDomain.CurrentDomain.BaseDirectory + "Frame/Corps.xml");
            foreach (XElement informations in xdoc.Element("informations").Elements("info"))
            {
                XAttribute nameAttribute = informations.Attribute("name");
                XElement adressElement = informations.Element("adress");
                XElement telephoneElement = informations.Element("telephone");
                XElement emailElement = informations.Element("email");
                XElement graphicElement = informations.Element("graphic");

                

                switch (id)
                {
                    default: break;
                    case 1:
                        if(nameAttribute.Value == "Корпус 1")
                        {
                            PlaceInfo(nameAttribute, adressElement, telephoneElement, emailElement, graphicElement);
                        }
                        break;
                    case 2:
                        if (nameAttribute.Value == "Корпус 2")
                        {
                            PlaceInfo(nameAttribute, adressElement, telephoneElement, emailElement, graphicElement);
                        }
                        break;
                    case 3:
                        if (nameAttribute.Value == "Корпус 3")
                        {
                            PlaceInfo(nameAttribute, adressElement, telephoneElement, emailElement, graphicElement);
                        }
                        break;
                    case 4:
                        if (nameAttribute.Value == "Корпус 4")
                        {
                            PlaceInfo(nameAttribute, adressElement, telephoneElement, emailElement, graphicElement);
                        }
                        break;
                }
            }
        }


        /// <summary>
        /// Информация о корпусе
        /// </summary>
        /// <param name="nameAttribute"></param>
        /// <param name="adressElement"></param>
        /// <param name="telephoneElement"></param>
        /// <param name="emailElement"></param>
        /// <param name="graphicElement"></param>
        public void PlaceInfo(XAttribute nameAttribute,XElement adressElement, XElement telephoneElement, XElement emailElement, XElement graphicElement)
        {
            CorpLabel.Content = nameAttribute.Value;
            AdressLabel.Text = adressElement.Value;
            TelephoneLabel.Content = telephoneElement.Value;
            EmailLabel.Content = emailElement.Value;
            GraphicLabel.Content = graphicElement.Value;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
