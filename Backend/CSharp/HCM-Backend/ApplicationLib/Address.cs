using System.ComponentModel.DataAnnotations;

namespace ApplicationLib
{
    public class Address
    {
        public string Title { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string Street { get; set; }
        public string StreetNumber { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
        public Contact Contact { get; set; }
    }
}
