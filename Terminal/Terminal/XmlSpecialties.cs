using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Terminal
{
    public class InformationSpecialties
    {
        public string nameAttribute;
        public string dataElement;
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
                Read();
            }
            return _instance;
        }
        private static void Read()
        {
            XDocument xdoc = XDocument.Load(AppDomain.CurrentDomain.BaseDirectory + "Frame/Specialties.xml");
            foreach (XElement informations in xdoc.Element("informations").Elements("info"))
            {
                InformationSpecialties informationSpecialties = new InformationSpecialties();
                informationSpecialties.nameAttribute = informations.Attribute("name").Value;
                informationSpecialties.dataElement = informations.Element("data").Value;

                specialtiesList.Add(informationSpecialties);
            }
        }
    }
}
