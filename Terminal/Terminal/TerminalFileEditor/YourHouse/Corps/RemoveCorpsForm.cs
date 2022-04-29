using System;
using System.Xml.Linq;
using ComponentFactory.Krypton.Toolkit;

namespace YourHouse.Corps
{
    public partial class RemoveCorpsForm : KryptonForm
    {
        private string nameBtn;
        public RemoveCorpsForm(string nameBtn)
        {
            this.nameBtn = nameBtn;
            InitializeComponent();
        }

        private void RemoveCorpsForm_Load(object sender, EventArgs e)
        {

        }

        private void Button_Remove_Click(object sender, EventArgs e)
        {
            XDocument xdoc = XDocument.Load($"..\\..\\..\\..\\bin/Debug/Frame/{nameBtn}.xml");
            foreach (XElement corps in xdoc.Element("informations").Elements("info"))
            {
                if (corps.Attribute("name").Value == textBox.Text)
                {
                    corps.Remove();
                }
                xdoc.Save($"..\\..\\..\\..\\bin/Debug/Frame/{nameBtn}.xml");
            }

            KryptonMessageBox.Show("Удалено!");
            this.Close();
        }
    }
}
