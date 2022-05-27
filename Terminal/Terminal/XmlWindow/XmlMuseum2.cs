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
    class XmlMuseum2
    {
        private static XmlMuseum2 _instance;
        private static List<MuseumInformation> museumList = new List<MuseumInformation>();
        public List<MuseumInformation> GetMuseumInfo
        {
            get => museumList;
        }
        public int GetCountMuseum
        {
            get => museumList.Count;
        }
        public static XmlMuseum2 Instance()
        {
            if (_instance == null)
            {
                _instance = new XmlMuseum2();
                Read();
            }
            return _instance;
        }
        /// <summary>
        /// Считывание данных
        /// </summary>
        private static void Read()
        {
            XDocument xdoc = XDocument.Load(AppDomain.CurrentDomain.BaseDirectory + "Frame/Museum2.xml");
            foreach (XElement informations in xdoc.Element("informations").Elements("info"))
            {
                MuseumInformation museumInformation = new MuseumInformation();
                museumInformation.dataElement = informations.Value;

                museumList.Add(museumInformation);
            }
        }
    }
}
