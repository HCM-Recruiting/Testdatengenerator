using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ApplicationLib
{
    //Kontaktdaten
    public class Contact
    {
        [XmlElement("telNumber")]
        public string TelNumber { get; set; }
        [XmlElement("mobileNumber")]
        public string MobileNumber { get; set; }
        [XmlElement("email")]
        public string Email { get; set; }
        [XmlElement("website")]
        public string Website { get; set; }
    }
}
