namespace fsw_api.Models
{
    public class CityDTO
    {
        public string CityName { get; set; }
        public string Country { get; set; }
        public decimal NumVisits { get; set; }
        public decimal Population { get; set; }
        public decimal NumHotels { get; set; }
        public int Certifications { get; set; }
    }
}
