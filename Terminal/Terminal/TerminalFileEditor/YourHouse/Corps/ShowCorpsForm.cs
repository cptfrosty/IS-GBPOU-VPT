using ComponentFactory.Krypton.Toolkit;
using System;
using System.Xml.Linq;

namespace YourHouse.Corps
{
    public partial class ShowCorpsForm : KryptonForm
    {
        private string nameBtn;
        public ShowCorpsForm(string nameBtn)
        {
            this.nameBtn = nameBtn;
            InitializeComponent();

            ShowCorps();
        }

        private void ShowCorpsForm_Load(object sender, EventArgs e)
        {

        }


        private void ShowCorps()
        {
            XDocument xdoc = XDocument.Load($"..\\..\\..\\..\\bin/Debug/Frame/{nameBtn}.xml");
            foreach (XElement corps in xdoc.Element("informations").Elements("info"))
            {             
                InfoRichTextBox.Text += "\t" + corps.Attribute("name").Value + "\n\n" + corps.Element("adress").Value + "\n" + corps.Element("telephone").Value + "\n" + corps.Element("email").Value + "\n" + corps.Element("graphic").Value + "\n" + corps.Element("history").Value + "\n" + "\n";
            }
        }
    }
}
