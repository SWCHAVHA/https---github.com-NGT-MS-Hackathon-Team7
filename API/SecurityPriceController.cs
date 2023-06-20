using Microsoft.AspNetCore.Mvc;

using Npgsql;

using System;

namespace API.Controllers

{

    [ApiController]

    [Route("api/SecurityPrice")]

    public class SecurityPriceController : ControllerBase

    {

        private readonly string _connectionString = "Host=localhost;Port=5432;Database=Penalty_Calculation;Username=postgres;Password=Priti@12345";




        [HttpGet("{price_id}")]

        public IActionResult GetSecurityPrice(long price_id)

        {

            try

            {

                using (var connection = new NpgsqlConnection(_connectionString))

                {

                    connection.Open();




                    var query = "SELECT isin_sec_id, poh, valid_from_date, sec_price, cntl_timestamp, cntl_userid FROM security_price WHERE price_id = @PriceId";




                    using (var command = new NpgsqlCommand(query, connection))

                    {

                        command.Parameters.AddWithValue("PriceId", price_id);




                        using (var reader = command.ExecuteReader())

                        {

                            if (reader.Read())

                            {

                                var isin_sec_id = reader.GetValue(0) as string[];

                                var poh = reader.GetInt16(1);

                                var valid_from_date = reader.GetDateTime(2);

                                var sec_price = reader.GetDecimal(3);

                                var cntl_timestamp = reader.GetDateTime(4);

                                var cntl_userid = reader.GetValue(5) as string[];




                                var response = new

                                {

                                    isin_sec_id = isin_sec_id,

                                    poh = poh,

                                    valid_from_date = valid_from_date,

                                    sec_price = sec_price,

                                    cntl_timestamp = cntl_timestamp,

                                    cntl_userid = cntl_userid,

                                    price_id = price_id

                                };




                                return Ok(response);

                            }

                        }

                    }

                }




                return NotFound($"Price with ID '{price_id}' not found.");

            }

            catch (Exception ex)

            {

                return StatusCode(500, $"An error occurred while retrieving the security price: {ex.Message}");

            }

        }

    }

}