using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using fsw_api.Models;
using fsw_api.Services;
using fsw_api.Helpers;

namespace fsw_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CityController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<City>> GetCities()
        {
            return Ok(new ApiResponse<IEnumerable<City>>
            {
                StatusCode = 200,
                Message = "Cities list",
                Success = true,
                Content = CityStore.Current.Cities
            });
        }

        [HttpGet("{cityId}")]
        public ActionResult<City> GetCity(int cityId)
        {
            var city = CityStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if(city == null)
            {
                return NotFound(new ApiResponse<City>
                {
                    StatusCode = 404,
                    Message = "City not found",
                    Success = false,
                    Content = null
                });
            }
            
            return Ok(new ApiResponse<City>
            {
                StatusCode = 200,
                Message = "City found",
                Success = true,
                Content = city
            });
        }

        [HttpPost]
        public ActionResult<City> PostCity(CityDTO city)
        {
            var maxCityId = CityStore.Current.Cities.Max(c => c.Id);
            var newCity = new City()
            {
                Id = maxCityId + 1,
                CityName = city.CityName,
                Country = city.Country,
                Certifications = city.Certifications,
                NumHotels = city.NumHotels,
                NumVisits = city.NumVisits,
                Population = city.Population
            };

            CityStore.Current.Cities.Add(newCity);
            return Ok( new ApiResponse<City>
            {
                StatusCode = 200,
                Message = "City updated",
                Success = true,
                Content = newCity
            });
        }
    }
}
