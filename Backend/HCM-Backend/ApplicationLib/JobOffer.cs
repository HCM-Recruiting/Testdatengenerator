using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ApplicationLib
{
    public class JobOffer
    {
        [XmlElement("identifier")]
        public string Identifier { get; set; }
        [XmlElement("id")]
        public string Id { get; set; }
    }
}
