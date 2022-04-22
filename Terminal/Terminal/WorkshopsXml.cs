using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Terminal
{
    public class InformationWorkshops
    {
        public string nameAttribute;
        public string dataElement;
    }
    class WorkshopsXml
    {
        private static WorkshopsXml _instance;
        public static List<InformationWorkshops> workshopsList = new List<InformationWorkshops>();
        public List<InformationWorkshops> GetWorkshopsInfo
        {
            get => workshopsList;
        }
        public int GetCountWorkshops
        {
            get => workshopsList.Count;
        }

        public static WorkshopsXml Instance()
        {
            if (_instance == null)
            {
                _instance = new WorkshopsXml();
                Read();
            }
            return _instance;
        }
        private static void Read()
        {
            XDocument xdoc = XDocument.Load(AppDomain.CurrentDomain.BaseDirectory + "Frame/Workshops.xml");
            foreach (XElement informations in xdoc.Element("informations").Elements("info"))
            {
                InformationWorkshops informationWorkshops = new InformationWorkshops();
                informationWorkshops.nameAttribute = informations.Attribute("name").Value;
                informationWorkshops.dataElement = informations.Element("data").Value;

                workshopsList.Add(informationWorkshops);
            }
        }
    }
}
