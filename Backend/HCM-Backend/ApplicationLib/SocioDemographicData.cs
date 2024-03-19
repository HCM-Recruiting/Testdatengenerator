using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ApplicationLib
{
    public class SocioDemographicData
    {
        [XmlElement("birthName")]
        public string BirthName { get; set; }
        [XmlElement("familyStatus")]
        public string FamilyStatus { get; set; }
        [XmlElement("religion")]
        public string Religion { get; set; }
        [XmlElement("militaryService")]
        public string MilitaryService { get; set; }
        [XmlElement("militaryFrom")]
        public string MilitaryFrom { get; set; }
        [XmlElement("militaryTo")]
        public string MilitaryTo { get; set; }
        [XmlElement("numberOfChildren")]
        public int NumberOfChildren { get; set; }
        [XmlElement("hobbies")]
        public string Hobbies { get; set; }
        [XmlElement("comments")]
        public string Comments { get; set; }
    }
}
