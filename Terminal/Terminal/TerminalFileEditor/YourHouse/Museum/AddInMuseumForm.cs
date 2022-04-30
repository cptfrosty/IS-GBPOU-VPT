using System;
using System.Windows.Forms;
using System.Xml;
using ComponentFactory.Krypton.Toolkit;

namespace YourHouse.Museum
{
    public partial class AddInMuseumForm : KryptonForm
    {
        private string nameBtn;
        public AddInMuseumForm(string nameBtn)
        {
            InitializeComponent();
            this.nameBtn = nameBtn;
        }

        private void AddInMuseumForm_Load(object sender, EventArgs e)
        {

        }

        private void Button_AddCorp_Click(object sender, EventArgs e)
        {
            if (nameTextBox.Text != "" &&
                dataRichTextBox.Text != "")
            {
                XmlDocument xDoc = new XmlDocument();

                xDoc.Load($"..\\..\\..\\..\\bin/Debug/Frame/{nameBtn}.xml");
                XmlElement xRoot = xDoc.DocumentElement;

                // создаем новый элемент employee
                XmlElement infoElem = xDoc.CreateElement("info");

                // создаем атрибут name
                XmlAttribute nameAttr = xDoc.CreateAttribute("name");

                // создаем элементы 
                XmlElement dataElem = xDoc.CreateElement("data");

                //создаем текстовые значения для элементов и атрибута
                XmlText nameText = xDoc.CreateTextNode($"{nameTextBox.Text}");
                XmlText dataText = xDoc.CreateTextNode($"{dataRichTextBox.Text}");

                //добавляем узлы
                nameAttr.AppendChild(nameText);
                dataElem.AppendChild(dataText);

                infoElem.Attributes.Append(nameAttr);
                infoElem.AppendChild(dataElem);

                xRoot.AppendChild(infoElem);
                xDoc.Save($"..\\..\\..\\..\\bin/Debug/Frame/{nameBtn}.xml");

                KryptonMessageBox.Show("Сохранено!");
                Close();
            }
            else
                KryptonMessageBox.Show("Заполните все поля!");
        }
    }
}
