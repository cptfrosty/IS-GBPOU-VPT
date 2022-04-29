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
            if (NameTextBox.Text != "" &&
                AdressTextBox.Text != "" &&
                NubmerTextBox.Text != "" &&
                EmailTextBox.Text != "" &&
                GraphicTextBox.Text != "" &&
                HistoryRichTextBox.Text != "")
            {
                XmlDocument xDoc = new XmlDocument();

                xDoc.Load($"..\\..\\..\\..\\bin/Debug/Frame/{nameBtn}.xml");
                XmlElement xRoot = xDoc.DocumentElement;

                // создаем новый элемент employee
                XmlElement infoElem = xDoc.CreateElement("info");

                // создаем атрибут name
                XmlAttribute nameAttr = xDoc.CreateAttribute("name");

                // создаем элементы 
                XmlElement adressElem = xDoc.CreateElement("adress");
                XmlElement numberPhoneElem = xDoc.CreateElement("telephone");
                XmlElement emailElem = xDoc.CreateElement("email");
                XmlElement graphicElem = xDoc.CreateElement("graphic");
                XmlElement historyElem = xDoc.CreateElement("history");

                //создаем текстовые значения для элементов и атрибута
                XmlText nameText = xDoc.CreateTextNode($"{NameTextBox.Text}");
                XmlText adressText = xDoc.CreateTextNode($"{AdressTextBox.Text}");
                XmlText telephoneText = xDoc.CreateTextNode($"{NubmerTextBox.Text}");
                XmlText emailText = xDoc.CreateTextNode($"{EmailTextBox.Text}");
                XmlText graphicText = xDoc.CreateTextNode($"{GraphicTextBox.Text}");
                XmlText historyText = xDoc.CreateTextNode($"{HistoryRichTextBox.Text}");

                //добавляем узлы
                nameAttr.AppendChild(nameText);
                adressElem.AppendChild(adressText);
                numberPhoneElem.AppendChild(telephoneText);
                emailElem.AppendChild(emailText);
                graphicElem.AppendChild(graphicText);
                historyElem.AppendChild(historyText);

                infoElem.Attributes.Append(nameAttr);
                infoElem.AppendChild(adressElem);
                infoElem.AppendChild(numberPhoneElem);
                infoElem.AppendChild(emailElem);
                infoElem.AppendChild(graphicElem);
                infoElem.AppendChild(historyElem);

                xRoot.AppendChild(infoElem);
                xDoc.Save($"..\\..\\..\\..\\bin/Debug/Frame/{nameBtn}.xml");

                KryptonMessageBox.Show("Сохранено!");
                this.Close();
            }
            else
                KryptonMessageBox.Show("Заполните все поля!");
        }
    }
}
