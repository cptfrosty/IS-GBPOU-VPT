using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.Maps.MapControl.WPF;

namespace Terminal
{ 

    //Объект корпуса
    public class CollegeBuilding
    {
        public string nameAttribute;
        public string adressElement;
        public string telephoneElement;
        public string emailElement;
        public string workSchedule;
        public string history;
        public Location location;
    }
    
    /*
     * Информация о XML документе корпусов
     */

    class XmlCollegeBuilding
    {
        private static XmlCollegeBuilding _instance;
        private static List<CollegeBuilding> collegeBuilding = new List<CollegeBuilding>();

        public List<CollegeBuilding> GetCollegeBuilding
        {
            get => collegeBuilding;
        }

        public int GetCountCorp
        {
            get => collegeBuilding.Count;
        }

        public static XmlCollegeBuilding Instance()
        {
            if (_instance == null)
            {
                _instance = new XmlCollegeBuilding();
                Read();
            }

            return _instance;
        }

        /// <summary>
        /// Считывание данных
        /// </summary>
        private static void Read()
        {
            XDocument xdoc = XDocument.Load(AppDomain.CurrentDomain.BaseDirectory + "Frame/Corps.xml");
            foreach (XElement informations in xdoc.Element("informations").Elements("info"))
            {
                CollegeBuilding infoCorp = new CollegeBuilding();
                infoCorp.nameAttribute = informations.Attribute("name").Value;
                infoCorp.adressElement = informations.Element("adress").Value;
                infoCorp.telephoneElement = informations.Element("telephone").Value;
                infoCorp.emailElement = informations.Element("email").Value;
                infoCorp.workSchedule = informations.Element("graphic").Value;
                infoCorp.history = informations.Element("history").Value;
                //infoCorp.location = new Location(Convert.ToInt32(informations.Element("longitude").Value), Convert.ToInt32(informations.Element("width").Value));
                infoCorp.location = new Location(1, 1);

                collegeBuilding.Add(infoCorp);
            }
        }
    }
}
