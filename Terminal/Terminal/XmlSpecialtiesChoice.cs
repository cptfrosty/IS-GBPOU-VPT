using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Terminal
{
      public class InformationSpecialtiesDir
      {
          public string nameDir;
          public string nameAttribute;
          public string infoElement;
      }

    class XmlSpecialtiesChoice
    {

        private string nameBtn;
        public XmlSpecialtiesChoice(string nameBtn)
        {
            this.nameBtn = nameBtn;
        }



        private XmlSpecialtiesChoice _instance;
        public List<InformationSpecialtiesDir> specialtiesList = new List<InformationSpecialtiesDir>();

        public List<InformationSpecialtiesDir> GetSpecialtiesInfo
        {
            get => specialtiesList;
        }
        public int GetCountSpecialties
        {
            get => specialtiesList.Count;
        }


        public XmlSpecialtiesChoice Instance()
        {
            if (_instance == null)
            {
                _instance = new XmlSpecialtiesChoice(nameBtn);
                FindDirection();
            }
            return _instance;
        }
        public void FindDirection()
        {
            XDocument xdoc = XDocument.Load(AppDomain.CurrentDomain.BaseDirectory + "Frame/Specialties.xml");
            foreach (XElement dir in xdoc.Element("informations").Elements("dir"))
            {
                string nameDir = dir.Attributes().ToList().Where(p => (p.Name == "nameForBtn")).FirstOrDefault().Value;
                if (nameDir == nameBtn)
                {
                    foreach (var special in dir.Elements("special"))
                    {

                        //TODO...
                        InformationSpecialtiesDir informationSpecialtiesDir = new InformationSpecialtiesDir();
                        informationSpecialtiesDir.nameDir = nameDir;

                        informationSpecialtiesDir.nameAttribute = special.Attributes().ToList().Where(p => (p.Name == "nameForBtn")).FirstOrDefault().Value;
                        informationSpecialtiesDir.infoElement = special.Element("info").Value;

                        specialtiesList.Add(informationSpecialtiesDir);
                        //...TODO
                    }
                }
       
            }
        }
    }
}
