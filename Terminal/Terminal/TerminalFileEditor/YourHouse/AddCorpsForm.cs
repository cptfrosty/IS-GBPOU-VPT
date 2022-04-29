using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using ComponentFactory.Krypton.Toolkit;

namespace YourHouse
{
    public partial class AddCorpsForm : KryptonForm
    {
        private string nameBtn;
        public AddCorpsForm(string nameBtn)
        {
            InitializeComponent();
            this.nameBtn = nameBtn;
        }

        private void AddFormForCorps_Load(object sender, EventArgs e)
        {

        }

        private void Button_AddCorp_Click(object sender, EventArgs e)
        {
            if  (NameTextBox.Text != "" && 
                AdressTextBox.Text != "" && 
                NubmerTextBox.Text != "" && 
                EmailTextBox.Text != "" && 
                GraphicTextBox.Text != "" && 
                HistoryRichTextBox.Text != "")
            {
                XmlDocument xDoc = new XmlDocument();

                xDoc.Load($"C:/");
                XmlElement xRoot = xDoc.DocumentElement;

                // создаем новый элемент employee
                XmlElement userElem = xDoc.CreateElement("employee");

                // создаем атрибут name
                XmlAttribute nameAttr = xDoc.CreateAttribute("name");

                // создаем элементы company и birthday
                XmlElement birthdayElem = xDoc.CreateElement("birthday");
                XmlElement numberPhoneElem = xDoc.CreateElement("numberphone");
                XmlElement cityElem = xDoc.CreateElement("city");
                XmlElement pasportElem = xDoc.CreateElement("pasport");
                XmlElement positionElem = xDoc.CreateElement("position");

                // создаем текстовые значения для элементов и атрибута
                XmlText nameText = xDoc.CreateTextNode($"{fullname}");
                XmlText birthdayText = xDoc.CreateTextNode($"{birthday}");
                XmlText numberPhoneText = xDoc.CreateTextNode($"{numberPhone}");
                XmlText cityText = xDoc.CreateTextNode($"{city}");
                XmlText pasportText = xDoc.CreateTextNode($"{pasport}");
                XmlText positionText = xDoc.CreateTextNode($"{position}");

                //добавляем узлы
                nameAttr.AppendChild(nameText);
                birthdayElem.AppendChild(birthdayText);
                numberPhoneElem.AppendChild(numberPhoneText);
                cityElem.AppendChild(cityText);
                pasportElem.AppendChild(pasportText);
                positionElem.AppendChild(positionText);

                userElem.Attributes.Append(nameAttr);
                userElem.AppendChild(birthdayElem);
                userElem.AppendChild(numberPhoneElem);
                userElem.AppendChild(cityElem);
                userElem.AppendChild(pasportElem);
                userElem.AppendChild(positionElem);

                xRoot.AppendChild(userElem);
                xDoc.Save(@"C:\employees.xml");

            }
        }
    }
}
