using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Terminal
    { 

    class InfoCorps
    {        
        public string nameAttribute { get; set; }
        public string adressElement  { get; set; }
        public string telephoneElement  { get; set; }
        public string emailElement { get; set; }
        public string graphicElement { get; set; }
    }
    class XmlCorps
    {
        static public List<InfoCorps> ListInfoCorps = new List<InfoCorps>();

        public List<InfoCorps> ShowInfo(int id)
        {
            

            XDocument xdoc = XDocument.Load(AppDomain.CurrentDomain.BaseDirectory + "Frame/Corps.xml");
            foreach (XElement informations in xdoc.Element("informations").Elements("info"))
            {
                XAttribute nameAttribute = informations.Attribute("name");
                XElement adressElement = informations.Element("adress");
                XElement telephoneElement = informations.Element("telephone");
                XElement emailElement = informations.Element("email");
                XElement graphicElement = informations.Element("graphic");



                switch (id)
                {
                    default: break;
                    case 1:
                        if (nameAttribute.Value == "Корпус 1")
                        {
                            PlaceInfo(ListInfoCorps, nameAttribute, adressElement, telephoneElement, emailElement, graphicElement);


                        }
                        break;
                    case 2:
                        if (nameAttribute.Value == "Корпус 2")
                        {
                            PlaceInfo(ListInfoCorps, nameAttribute, adressElement, telephoneElement, emailElement, graphicElement);
                        }
                        break;
                    case 3:
                        if (nameAttribute.Value == "Корпус 3")
                        {
                            PlaceInfo(ListInfoCorps, nameAttribute, adressElement, telephoneElement, emailElement, graphicElement);
                        }
                        break;
                    case 4:
                        if (nameAttribute.Value == "Корпус 4")
                        {
                            PlaceInfo(ListInfoCorps, nameAttribute, adressElement, telephoneElement, emailElement, graphicElement);

                            
                        }
                        break;
                }                
            }
            return ListInfoCorps;
        }
        public void PlaceInfo(List<InfoCorps> ListInfoCorps, XAttribute nameAttribute, XElement adressElement, XElement telephoneElement, XElement emailElement, XElement graphicElement)
        {
            InfoCorps infoCorps = new InfoCorps();
            infoCorps.nameAttribute = nameAttribute.Value;
            infoCorps.adressElement = adressElement.Value;
            infoCorps.telephoneElement = telephoneElement.Value;
            infoCorps.emailElement = emailElement.Value;
            infoCorps.graphicElement = graphicElement.Value;

            ListInfoCorps.Add(infoCorps);
        }
    }
}
