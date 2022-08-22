using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Terminal
{

    public class InformationXmlFaq
    {
        public string nameAttribute;
        public string answerElement;
        
    }


    public class XmlFaq
    {

        private XmlFaq _instance;
        public List<InformationXmlFaq> informationXmlFaqList = new List<InformationXmlFaq>();

        //public List<InformationSpecialties> GetSpecialtiesInfo
        //{
        //    get => specialtiesList;
        //}

        public int GetCountinformationinformationXmlFaqList
        {
            get => informationXmlFaqList.Count;
        }

        public XmlFaq Instance()
        {
            if (_instance == null)
            {
                _instance = new XmlFaq();
                FindFaq();
            }
            return _instance;
        }
        public void FindFaq()
        {
            XDocument xdoc = XDocument.Load(AppDomain.CurrentDomain.BaseDirectory + "Frame/Faq.xml");
            foreach (XElement dir in xdoc.Element("informations").Elements("question"))
            {              
                    InformationXmlFaq informationXmlFaq = new InformationXmlFaq();

                    informationXmlFaq.nameAttribute = dir.Attribute("name").Value;
                    informationXmlFaq.answerElement = dir.Element("answer").Value;

                    informationXmlFaqList.Add(informationXmlFaq);
            }
        }
    }

}
