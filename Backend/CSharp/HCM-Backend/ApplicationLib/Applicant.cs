using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLib
{
    public class Applicant
    {
        [Required]
        public string Gender { get; set; }
        public DateTime Birthday { get; set; }
        public string BirthLocation { get; set; }
        public string Nationality { get; set; }
        public string InsuranceNumber { get; set; }
        public Address Address { get; set; }
        public SocioDemographicData SocioDemographicData { get; set; }
        public Photo Photo { get; set; }
    }
}
