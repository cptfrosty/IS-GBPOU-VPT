using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;

namespace Terminal.XmlWindow
{
    public class InformationSchedule
    {
        public string nameDay { get; set; }
        public string nameGroup { get; set; }
        public string schedule { get; set; }
    }

    public class InformationDay
    {
        public string nameDay { get; set; }
    }

    public class JsonSchedule
    {
        public int countDay { get; set; }

        public JsonSchedule()
        {
            FindSchedule();
        }

        public List<InformationSchedule> informationScheduleList = new List<InformationSchedule>();

        public List<InformationDay> informationDaysList = new List<InformationDay>();

        public List<InformationSchedule> GetList()
        {
            return informationScheduleList;
        }

        public List<InformationDay> GetDays()
        {
            return informationDaysList;
        }

        public void FindSchedule()
        {
            InformationSchedule informationSchedule = new InformationSchedule();

            InformationDay informationDay = new InformationDay();

            string path = "JsonShedule.txt";

            string jsonShedule;

            using (StreamReader reader = new StreamReader(path))
            {
                jsonShedule = reader.ReadToEnd();
            }

            JObject obj = JObject.Parse(jsonShedule);
            JObject corpusOne = (JObject)obj["schedule"]["1"];

            foreach (var i in corpusOne)
            {
                informationDay.nameDay = i.Key;
                informationDaysList.Add(informationDay);

                JObject groups = (JObject)i.Value;

                foreach (var j in groups)
                {
                    if (j.Value is JObject)
                    {
                        foreach (var groupSc in j.Value)
                        {
                            JProperty groupSc1 = (JProperty)groupSc;

                            informationSchedule.nameDay = i.Key.ToString();
                            informationSchedule.nameGroup = groupSc1.Name;
                            informationSchedule.schedule = groupSc1.Value.ToString();

                            informationScheduleList.Add(informationSchedule);
                        }
                    }
                }
            }
        }
    }
}
