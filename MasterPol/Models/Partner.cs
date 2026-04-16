using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MasterPol.Models
{
    [Table("partners")]
    public class Partner
    {
        [Key]
        public int Id { get; set; }

        [Column("partner_type"), MaxLength(10)]
        public string PartnerType { get; set; }

        [Required, MaxLength(200)]
        public string Name { get; set; }

        [Column("last_name"), Required, MaxLength(100)]
        public string LastName { get; set; }

        [Column("first_name"), Required, MaxLength(100)]
        public string FirstName { get; set; }

        [Column("middle_name"), MaxLength(100)]
        public string MiddleName { get; set; }

        [Required, MaxLength(100)]
        public string Email { get; set; }

        [Column("phone_country_code"), MaxLength(10)]
        public string PhoneCountryCode { get; set; }

        [Column("phone_city_code"), MaxLength(10)]
        public string PhoneCityCode { get; set; }

        [Column("phone_number"), Required, MaxLength(20)]
        public string PhoneNumber { get; set; }

        [Column("address_postal_code"), MaxLength(20)]
        public string AddressPostalCode { get; set; }

        [Column("address_region"), MaxLength(100)]
        public string AddressRegion { get; set; }

        [Column("address_city"), Required, MaxLength(100)]
        public string AddressCity { get; set; }

        [Column("address_street"), Required, MaxLength(200)]
        public string AddressStreet { get; set; }

        [Column("address_building"), Required, MaxLength(50)]
        public string AddressBuilding { get; set; }

        [Required, MaxLength(20)]
        public string Inn { get; set; } 

        public int? Rating { get; set; }
    }
}