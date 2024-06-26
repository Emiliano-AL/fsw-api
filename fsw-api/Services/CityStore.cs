﻿using fsw_api.Models;

namespace fsw_api.Services
{
    public class CityStore
    {
        public List<City> Cities { get; set; }

        public static CityStore Current { get; } = new CityStore();

        public CityStore()
        {
            Cities = new List<City>()
            {
                new City()
                {
                    CityId = 1,
                    CityName = "Buenos Aires",
                    Country = "Argentina",
                    NumVisits = 1567,
                    NumHotels = 2344,
                    Certifications = 4,
                    Population = 197400
                },
                new City()
                {
                    CityId = 2,
                    CityName = "Santiago",
                    Country = "Chile",
                    NumVisits = 1567,
                    NumHotels = 2345,
                    Certifications = 5,
                    Population = 297400,
                },
                new City()
                {
                    CityId = 3,
                    CityName = "Ciudad de México",
                    Country = "México",
                    NumVisits = 1267,
                    NumHotels = 23345,
                    Certifications = 5,
                    Population = 457400,
                },
                new City()
                {
                    CityId = 4,
                    CityName = "Bogotá",
                    Country = "Colombia",
                    NumVisits = 1267,
                    NumHotels = 23345,
                    Certifications = 5,
                    Population = 457400,
                },
                new City()
                {
                    CityId = 5,
                    CityName = "São Paulo",
                    Country = "Brasil",
                    NumVisits = 1267,
                    NumHotels = 23345,
                    Certifications = 5,
                    Population = 457400,
                },
                new City()
                {
                    CityId = 6,
                    CityName = "Cancún",
                    Country = "México",
                    NumVisits = 1267,
                    NumHotels = 23345,
                    Certifications = 5,
                    Population = 457400,
                },
                new City()
                {
                    CityId = 7,
                    CityName = "Río de Janeiro",
                    Country = "Brasil",
                    NumVisits = 1267,
                    NumHotels = 23345,
                    Certifications = 5,
                    Population = 457400,
                },
            };
        }
    }
}
