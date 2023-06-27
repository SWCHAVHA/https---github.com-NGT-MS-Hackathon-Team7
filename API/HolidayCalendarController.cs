using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [ApiController]
  
    public class HolidayCalendarController : ControllerBase
    {
        private readonly string _connectionString = "Host=localhost;Port=5432;Database=Penalty_Calculation;Username=postgres;Password=Priti@12345";

 

        [HttpGet("{year}")]
       
        public IActionResult GetHoliday(int year, string country)
        {
            try
            {
                using (var connection = new NpgsqlConnection(_connectionString))
                {
                    connection.Open();

                    var query = "SELECT holiday_id, description, country FROM holiday_calender WHERE year = @Year AND country = @Country";

                    using (var command = new NpgsqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("Year", year);
                        command.Parameters.AddWithValue("Country", country);

 

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                var holidayId = reader.GetInt32(0);
                                var description = reader.GetString(1);
                                var holidayCountry = reader.GetString(2);

 

                                var response = new
                                {
                                    holiday_id = holidayId,
                                    description = description,
                                    country = holidayCountry
                                };

 

                                return Ok(response);
                            }
                        }
                    }
                }

 

                return NotFound($"Holiday with ID '{year}' and country '{country}' not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving the holiday: {ex.Message}");
            }


        }
    }
}

