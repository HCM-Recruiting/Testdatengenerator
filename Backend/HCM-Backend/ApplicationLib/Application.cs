using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ApplicationLib
{
    public class Application
    {
        [Required]
        public JobOffer JobOffer { get; set; }
        [Required]
        public string IncomingDate { get; set; }
        public bool KeepOnFile { get; set; }
        [XmlElement("retentionDate")]
        public string RetentionDate { get; set; }
        [XmlElement("privacyTermsAccepted")]
        public bool PrivacyTermsAccepted { get; set; }
        [XmlElement("privacyTermsAcceptedDate")]
        public string PrivacyTermsAcceptedDate { get; set; }
        [XmlElement("forwardTermsAccepted")]
        public bool ForwardTermsAccepted { get; set; }
        [XmlElement("forwardTermsAcceptedDate")]
        public string ForwardTermsAcceptedDate { get; set; }
        [XmlElement("earliestBeginDate")]
        public string? EarliestBeginDate { get; set; }
        [XmlElement("salaryWish")]
        public int SalaryWish { get; set; }
        [XmlElement("salaryCurrency")]
        public string SalaryCurrency { get; set; }
        [XmlElement("salaryUnit")]
        public string SalaryUnit { get; set; }
        [XmlElement("activityDimension")]
        public string ActivityDimension { get; set; }
        [XmlElement("offerKnownBy")]
        public string OfferKnownBy { get; set; }
        public Applicant Applicant { get; set; }
        public SpecificData SpecificData { get; set; }
        public List<Document> Documents { get; set; }

        public void Serialize(XmlWriter writer)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Application));
            XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
            namespaces.Add("", "");
            serializer.Serialize(writer, this, namespaces);
        }
    }
}



