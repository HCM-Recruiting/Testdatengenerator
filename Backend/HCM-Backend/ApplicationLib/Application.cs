using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLib
{
    public class Application
    {
        [Required]
        public JobOffer JobOffer { get; set; }
        [Required]
        public DateTime IncomingDate { get; set; }
        public bool KeepOnFile { get; set; }
        public DateTime RetentionDate { get; set; }
        public bool PrivacyTermsAccepted { get; set; }
        public DateTime PrivacyTermsAcceptedDate { get; set; }
        public bool ForwardTermsAccepted { get; set; }
        public DateTime ForwardTermsAcceptedDate { get; set; }
        public DateTime EarliestBeginDate { get; set; }
        public int SalaryWish { get; set; }
        public string SalaryCurrency { get; set; }
        public string SalaryUnit { get; set; }
        public string ActivityDimension { get; set; }
        public string OfferKnownBy { get; set; }
        public Applicant Applicant { get; set; }
        public SpecificData SpecificData { get; set; }
        public List<Document> Documents { get; set; }
    }
}
