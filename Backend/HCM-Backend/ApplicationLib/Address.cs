using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace ApplicationLib
{
    public class Address
    {
        [XmlElement("title")]
        public string Title { get; set; }
        [Required]
        [XmlElement("firstName")]
        public string FirstName { get; set; }
        [Required]
        [XmlElement("lastName")]
        public string LastName { get; set; }
        [XmlElement("street")]
        public string Street { get; set; }
        [XmlElement("streetNumber")]
        public string StreetNumber { get; set; }
        [XmlElement("zipCode")]
        public string ZipCode { get; set; }
        [XmlElement("city")]
        public string City { get; set; }
        [XmlElement("region")]
        public string Region { get; set; }
        [XmlElement("country")]
        public string Country { get; set; }
        [XmlElement("contact")]
        public Contact Contact { get; set; }
    }
}
