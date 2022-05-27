using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Terminal
{
    public class InformationAdditional
    {
        public string nameAttribute;
        public string fullNameElement;
        public string graphicElement;
    }


    class XmlAdditional
    {

        private XmlAdditional _instance;
        public List<InformationAdditional> informationAdditionalList = new List<InformationAdditional>();

        //public List<InformationSpecialties> GetSpecialtiesInfo
        //{
        //    get => specialtiesList;
        //}

        public int GetCountinformationAdditionalList
        {
            get => informationAdditionalList.Count;
        }

        public XmlAdditional Instance()
        {
            if (_instance == null)
            {
                _instance = new XmlAdditional();
                FindAdditional();
            }
            return _instance;
        }
        public void FindAdditional()
        {
            XDocument xdoc = XDocument.Load(AppDomain.CurrentDomain.BaseDirectory + "Frame/Additional.xml");
            foreach (XElement dir in xdoc.Element("informations").Elements("Corp"))
            {
                foreach (var add in dir.Elements("nameClub"))
                {                    
                    InformationAdditional informationAdditional = new InformationAdditional();

                    informationAdditional.nameAttribute = add.Attribute("name").Value;
                    informationAdditional.fullNameElement = add.Element("fullName").Value;
                    informationAdditional.graphicElement = add.Element("graphic").Value;

                    informationAdditionalList.Add(informationAdditional);
                    
                }


            }
        }
    }
}
