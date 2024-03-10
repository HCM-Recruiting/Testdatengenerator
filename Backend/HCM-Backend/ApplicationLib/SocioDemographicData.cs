using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLib
{
    public class SocioDemographicData
    {
        public string BirthName { get; set; }
        public string FamilyStatus { get; set; }
        public string FamilyStatusDescription { get; set; }
        public string MilitaryService { get; set; }
        public DateTime MilitaryFrom { get; set; }
        public DateTime MilitaryTo { get; set; }
        public string Religion { get; set; }
        public string ReligionDescription { get; set; }
        public DateTime WeddingDay { get; set; }
        public List<NameChange> NameChanges { get; set; }
        public int NumberOfChildren { get; set; }
        public string Hobbies { get; set; }
        public string Comments { get; set; }
    }
}
