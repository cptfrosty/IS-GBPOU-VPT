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
        private XmlSpecialties _instance;
        public List<InformationSpecialties> specialtiesList = new List<InformationSpecialties>();
        public List<InformationSpecialties> GetSpecialtiesInfo
        {
            get => specialtiesList;
        }
        public int GetCountSpecialties
        {
            get => specialtiesList.Count;
        }

        public XmlSpecialties Instance()
        {
            if (_instance == null)
            {
                _instance = new XmlSpecialties();
                FindDirection();
            }
            return _instance;
        }
        public void FindDirection()
        {
            XDocument xdoc = XDocument.Load(AppDomain.CurrentDomain.BaseDirectory + "Frame/Specialties.xml");
            foreach (XElement dir in xdoc.Element("informations").Elements("dir"))
            {
                 foreach (var special in dir.Elements("special"))
                 {
                    InformationSpecialties informationSpecialties = new InformationSpecialties();
                    //TODO РЕШИТЬ ПРОБЛЕМУ ...
                    var a = special.Attributes().ToList().Where(p => (p.Name == "nameForBtn"));
                    
                    informationSpecialties.nameAttribute = a.ElementAt(0).Value;
                    //... TODO РЕШИТЬ ПРОБЛЕМУ
                    informationSpecialties.infoElement = special.Element("info").Value;

                    specialtiesList.Add(informationSpecialties);
                 }
                
            }
        }

        public int GetCountDir()
        {
            int i = 0;
            XDocument xdoc = XDocument.Load(AppDomain.CurrentDomain.BaseDirectory + "Frame/Specialties.xml");
            foreach (XElement dir in xdoc.Element("informations").Elements("dir"))
            {
                i = +1;
            }
            return i;
        }
    }
}
