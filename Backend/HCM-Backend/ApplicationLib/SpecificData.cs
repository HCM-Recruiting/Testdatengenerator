using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLib
{
    //Besonderheiten
    public class SpecificData
    {
        public string RegionalDomain { get; set; }
        public bool OwnCar { get; set; }
        public bool BusinessTrip { get; set; }
        public bool ChangeHome { get; set; }
        public bool DisabilityRightsApply { get; set; }
        public bool ApplicationToReductionInEarningCapacity { get; set; }
        public string SevereDisability { get; set; }
        public int BusinessTripPercent { get; set; }
        public int ReductionInEarningCapacity { get; set; }
        public string WorkPermitStatus { get; set; }
        public string WorkPermitType { get; set; }
        public DateTime WorkPermitValidTo { get; set; }
        public string WorkPermitIdCardNr { get; set; }
        public DateTime ApplicationDateToReductionInEarningCapacity { get; set; }
        public string AdditionalFunction { get; set; }
        public string AdditionalInformation { get; set; }
        public string Expectation { get; set; }
        public string Motivation { get; set; }
        public string RegionalPrefs { get; set; }
    }
}
