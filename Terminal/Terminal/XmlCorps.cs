using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Terminal
{ 
    public class InfoCorp
    {
        public string nameAttribute;
        public string adressElement;
        public string telephoneElement;
        public string emailElement;
        public string graphicElement;
        
    }
    
    class XmlCorps
    {
        WindowInf windowInf = new WindowInf();
        public void GetCountCorp()
        {     
            XDocument xdoc = XDocument.Load(AppDomain.CurrentDomain.BaseDirectory + "Frame/Corps.xml");
            foreach (XElement informations in xdoc.Element("informations").Elements("info"))
            {
                InfoCorp infoCorp = new InfoCorp();
                infoCorp.nameAttribute = informations.Attribute("name").Value;
                infoCorp.adressElement = informations.Element("adress").Value;
                infoCorp.telephoneElement = informations.Element("telephone").Value;
                infoCorp.emailElement = informations.Element("email").Value;
                infoCorp.graphicElement = informations.Element("graphic").Value;

               // windowInf.ListInfoCorp.Add(infoCorp);
            }          
        }
    }
}
