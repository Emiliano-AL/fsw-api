using System.ComponentModel.DataAnnotations;

namespace fsw_api.Models
{
    public class CityDTO
    {
        [Required]
        [MaxLength(100)]
        public string CityName { get; set; }

        [Required]
        [MaxLength(100)]
        public string Country { get; set; }
        public decimal NumVisits { get; set; }
        public decimal Population { get; set; }
        public decimal NumHotels { get; set; }
        public int Certifications { get; set; }
    }
}
