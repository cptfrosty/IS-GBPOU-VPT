using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Terminal
{
    public class InformationSpecialties
    {
        public string nameDir;
        public string nameAttribute;
        public string infoElement;
    }

    public class DirButton
    {
        public string nameAttribute;
    }

    class XmlSpecialties
    {
        private XmlSpecialties _instance;

        public List<DirButton> dirButtonList = new List<DirButton>();

        public int GetCountDirButton
        {
            get => dirButtonList.Count;
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
                DirButton dirButton = new DirButton();
                dirButton.nameAttribute = dir.Attributes().ToList().Where(p => (p.Name == "nameForBtn")).FirstOrDefault().Value;
                dirButtonList.Add(dirButton);
            }
        }
    }
}
