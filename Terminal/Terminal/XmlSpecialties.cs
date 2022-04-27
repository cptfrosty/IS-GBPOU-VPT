using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace Terminal
{
    public class InformationSpecialties
    {
        public string nameAttribute;
        public string infoElement;
    }
    class XmlSpecialties
    {
        private static XmlSpecialties _instance;
        public static List<InformationSpecialties> specialtiesList = new List<InformationSpecialties>();
        public List<InformationSpecialties> GetSpecialtiesInfo
        {
            get => specialtiesList;
        }
        public int GetCountSpecialties
        {
            get => specialtiesList.Count;
        }

        public static XmlSpecialties Instance()
        {
            if (_instance == null)
            {
                _instance = new XmlSpecialties();
                FindDirection();
            }
            return _instance;
        }
        public static void FindDirection()
        {
            XDocument xdoc = XDocument.Load(AppDomain.CurrentDomain.BaseDirectory + "Frame/Specialties.xml");
            foreach (XElement dir in xdoc.Element("informations").Elements("dir"))
            {
                if(dir.Attribute("name").Value == "08.00.00 Техника и технологии строительства")
                {
                    foreach (var a  in dir.Elements("special"))
                    {
                        InformationSpecialties informationSpecialties = new InformationSpecialties();
                        informationSpecialties.nameAttribute = a.Attribute("name").Value;
                        informationSpecialties.infoElement = a.Element("name").Value;

                        specialtiesList.Add(informationSpecialties);
                    }
                }
            }
        }
    }
}
