using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ApplicationLib
{
    public class Applicant
    {
        [Required]
        [XmlElement("gender")]
        public string? Gender { get; set; }
        [XmlElement("birthday")]
        public string? Birthday { get; set; }
        [XmlElement("birthLocation")]
        public string? BirthLocation { get; set; }
        [XmlElement("nationality")]
        public string? Nationality { get; set; }
        [XmlElement("insuranceNumber")]
        public string? InsuranceNumber { get; set; }
        [XmlElement("address")]
        public Address? Address { get; set; }
        [XmlElement("socioDemographicData")]
        public SocioDemographicData? SocioDemographicData { get; set; }
        [XmlElement("photo")]
        public Document? Photo { get; set; }
    }
}
