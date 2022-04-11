using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Terminal
{
    public class MuseumInformation
    {
        public string nameAttribute;
        public string dataElement;
    }
    class XmlMuseum
    {
        private static XmlMuseum _instance;
        private static List<MuseumInformation> museumList = new List<MuseumInformation>();
        public List<MuseumInformation> GetMuseumInfo
        {
            get => museumList;
        }
        public int GetCountMuseum
        {
            get => museumList.Count;
        }
        public static XmlMuseum Instance()
        {
            if (_instance == null)
            {
                _instance = new XmlMuseum();
                Read();
            }
            return _instance;
        }
        /// <summary>
        /// Считывание данных
        /// </summary>
        private static void Read()
        {
            XDocument xdoc = XDocument.Load(AppDomain.CurrentDomain.BaseDirectory + "Frame/Museum.xml");
            foreach (XElement informations in xdoc.Element("informations").Elements("info"))
            {
                MuseumInformation museumInformation = new MuseumInformation();
                museumInformation.nameAttribute = informations.Attribute("name").Value;
                museumInformation.dataElement = informations.Element("data").Value;
                
                museumList.Add(museumInformation);
            }
        }
    }
}
