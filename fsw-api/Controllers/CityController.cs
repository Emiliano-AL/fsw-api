using Microsoft.AspNetCore.Mvc;
using fsw_api.Models;
using fsw_api.Services;
using fsw_api.Helpers;
using System.Data;
using Npgsql;
using System.Text.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace fsw_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CityController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public CityController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        private string ConvertDataTableToJson(DataTable dataTable)
        {
            string jsonString = string.Empty;

            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                jsonString = JsonConvert.SerializeObject(dataTable, Formatting.Indented);
            }

            return jsonString;
        }

        [HttpGet]
        public ActionResult<IEnumerable<City>> GetCities()
        {
            string query = @"select CityId as ""CityId"", CityName as ""CityName"", Country as ""Country"", 
                NumVisits as ""NumVisits"", Population as ""Population"", NumHotels as ""NumHotels"",
                Certifications as ""Certifications"" from City";

            DataTable table = new DataTable();
            string sqlDS = _configuration.GetConnectionString("CityAppConn");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDS))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }

            string result = ConvertDataTableToJson(table);

            List<City> listCity = JsonConvert.DeserializeObject<List<City>>(result);
            return Ok(new ApiResponse<IEnumerable<City>>
            {
                StatusCode = 200,
                Message = "Cities list",
                Success = true,
                Content = listCity
            });
        }

        [HttpGet("{cityId}")]
        public ActionResult<City> GetCity(int cityId)
        {
            string query = @"select CityId as ""CityId"", CityName as ""CityName"", Country as ""Country"", 
                NumVisits as ""NumVisits"", Population as ""Population"", NumHotels as ""NumHotels"",
                Certifications as ""Certifications"" from City where CityId=@CityId";

            DataTable table = new DataTable();
            string sqlDS = _configuration.GetConnectionString("CityAppConn");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDS))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@CityId", cityId);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }

            string result = ConvertDataTableToJson(table);
            City city = JsonConvert.DeserializeObject<List<City>>(result)[0];

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
            string query = @"insert into City(CityName, Country, NumVisits, Population, NumHotels, Certifications)
                values(@CityName, @Country, @NumVisits, @Population, @NumHotels, @Certifications)";

            DataTable table = new DataTable();
            string sqlDS = _configuration.GetConnectionString("CityAppConn");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDS))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@CityName", city.CityName);
                    myCommand.Parameters.AddWithValue("@Country", city.Country);
                    myCommand.Parameters.AddWithValue("@NumVisits", city.NumVisits);
                    myCommand.Parameters.AddWithValue("@Population", city.Population);
                    myCommand.Parameters.AddWithValue("@NumHotels", city.NumHotels);
                    myCommand.Parameters.AddWithValue("@Certifications", city.Certifications);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }

            City city1 = new City();
            city1.CityName = city.CityName;
            city1.Country = city.Country;
            city1.NumVisits = city.NumVisits;
            city1.NumHotels = city.NumHotels;
            city1.Certifications = city.Certifications;

            return Ok(new ApiResponse<City>
            {
                StatusCode = 200,
                Message = "City added",
                Success = true,
                Content = city1
            });
        }

        [HttpPut("{cityId}")]
        public ActionResult<City> UpdateCity(int cityId, CityDTO cityUpdate)
        {
            string query = @"update City set CityName = @CityName, Country = @Country, NumVisits = @NumVisits, 
                Population = @Population, NumHotels = @NumHotels, Certifications = @Certifications
                where CityId=@CityId";

            DataTable table = new DataTable();
            string sqlDS = _configuration.GetConnectionString("CityAppConn");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDS))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@CityId", cityId);

                    myCommand.Parameters.AddWithValue("@CityName", cityUpdate.CityName);
                    myCommand.Parameters.AddWithValue("@Country", cityUpdate.Country);
                    myCommand.Parameters.AddWithValue("@NumVisits", cityUpdate.NumVisits);
                    myCommand.Parameters.AddWithValue("@Population", cityUpdate.Population);
                    myCommand.Parameters.AddWithValue("@NumHotels", cityUpdate.NumHotels);
                    myCommand.Parameters.AddWithValue("@Certifications", cityUpdate.Certifications);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }

            City city1 = new City();
            city1.CityName = cityUpdate.CityName;
            city1.Country = cityUpdate.Country;
            city1.NumVisits = cityUpdate.NumVisits;
            city1.NumHotels = cityUpdate.NumHotels;
            city1.Certifications = cityUpdate.Certifications;

            return Ok(new ApiResponse<City>
            {
                StatusCode = 200,
                Message = "City updated",
                Success = true,
                Content = city1
            });
        }


        [HttpDelete("{cityId}")]
        public ActionResult<City> DeleteCity(int cityId)
        {
            string query = @"delete from City where CityId=@CityId";

            DataTable table = new DataTable();
            string sqlDS = _configuration.GetConnectionString("CityAppConn");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDS))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@CityId", cityId);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }

            City city = new City();

            return Ok(new ApiResponse<City>
            {
                StatusCode = 200,
                Message = "City deleted",
                Success = true,
                Content = city
            });
        }

    }
}
