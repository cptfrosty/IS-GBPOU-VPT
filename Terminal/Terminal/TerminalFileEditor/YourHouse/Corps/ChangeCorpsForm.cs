using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using ComponentFactory.Krypton.Toolkit;

namespace YourHouse.Corps
{
    public partial class ChangeCorpsForm : KryptonForm
    {
        private string nameBtn;
        public ChangeCorpsForm(string nameBtn)
        {
            this.nameBtn = nameBtn;
            InitializeComponent();
        }

        private void ChangeCorpsForm_Load(object sender, EventArgs e)
        {

        }

        private void Button_Change_Click(object sender, EventArgs e)
        {
            XDocument xdoc = XDocument.Load($"..\\..\\..\\..\\bin/Debug/Frame/{nameBtn}.xml");

            foreach (XElement information in xdoc.Element("informations").Elements("info"))
            {
                if (information.Attribute("name").Value == oldNametextBox.Text)
                {
                    if(newNameTextBox.Text != "")
                        information.Attribute("name").Value = newNameTextBox.Text;
                    if (newAdressTextBox.Text != "")
                        information.Element("adress").Value = newAdressTextBox.Text;
                    if (newAdressTextBox.Text != "")
                        information.Element("telephone").Value = newAdressTextBox.Text;
                    if (newNumberTextBox.Text != "")
                        information.Element("email").Value = newNumberTextBox.Text;
                    if (newEmailTextBox.Text != "")
                        information.Element("graphic").Value = newEmailTextBox.Text;
                    if (newHistoryRichTextBox.Text != "")
                        information.Element("history").Value = newHistoryRichTextBox.Text;
                }
                xdoc.Save($"..\\..\\..\\..\\bin/Debug/Frame/{nameBtn}.xml");
            }
            KryptonMessageBox.Show("Изменено!");
            this.Close();
        }
    }
}
