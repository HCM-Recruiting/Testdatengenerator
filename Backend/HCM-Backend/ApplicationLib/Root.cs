using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ApplicationLib
{
    public class Root
    {
        [XmlElement("entities")]
        public List<Application> Entities { get; set; }
    }
}
