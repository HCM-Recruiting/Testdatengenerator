using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ApplicationLib
{
    public class Document
    {
        [XmlElement("useAsEmployeePhoto")]
        public bool UseAsEmployeePhoto { get; set; }
        [XmlElement("assignmentType")]
        public string AssignmentType { get; set; }
        [XmlElement("category")] 
        public string Category { get; set; }
        [XmlElement("subCategory")] 
        public string SubCategory { get; set; }
        [XmlElement("name")] 
        public string Name { get; set; }
        [XmlElement("title")] 
        public string Title { get; set; }
        [XmlElement("size")] 
        public int Size { get; set; }
        [XmlElement("mimetype")] 
        public string Mimetype { get; set; }
        [XmlElement("description")] 
        public string Description { get; set; }
        [XmlElement("documentBytes")] 
        public string DocumentBytes { get; set; }
    }
}
