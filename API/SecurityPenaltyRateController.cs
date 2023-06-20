using Microsoft.AspNetCore.Mvc;

using Npgsql;

using System;

namespace API.Controllers

{

    [ApiController]

    [Route("api/SecurityPenaltyRate")]                   

    public class SecurityPenaltyRateController : ControllerBase

    {

        private readonly string _connectionString = "Host=localhost;Port=5432;Database=Penalty_Calculation;Username=postgres;Password=priti@12345";




        [HttpGet("{penalty_id}")]

        public IActionResult GetSecurityPenaltyRate(long penalty_id)

        {

            try

            {

                using (var connection = new NpgsqlConnection(_connectionString))

                {

                    connection.Open();




                    var query = "SELECT penalty_id, valid_from_date, penalty_rate, last_updated_date, cntl_timestamp, cntl_userid, approve_penalty_required, approval FROM security_penalty_rate WHERE penalty_id = @PenaltyId";




                    using (var command = new NpgsqlCommand(query, connection))

                    {

                        command.Parameters.AddWithValue("PenaltyId", penalty_id);




                        using (var reader = command.ExecuteReader())

                        {

                            if (reader.Read())

                            {

                                var penaltyId = reader.GetInt64(0);

                                var validFromDate = reader.GetDateTime(1).Date;

                                var penaltyRate = reader.GetDecimal(2);

                                var lastUpdatedDate = reader.GetDateTime(3).Date;

                                var cntlTimestamp = reader.GetDateTime(4);

                                var cntlUserid = reader.GetValue(5) as string[];

                                var approvePenaltyRequired = reader.GetDecimal(6);

                                var approval = reader.GetString(7);




                                var response = new

                                {

                                    PenaltyId = penaltyId,

                                    ValidFromDate = validFromDate,

                                    PenaltyRate = penaltyRate,

                                    LastUpdatedDate = lastUpdatedDate,

                                    CntlTimestamp = cntlTimestamp,

                                    CntlUserid = cntlUserid,

                                    ApprovePenaltyRequired = approvePenaltyRequired,

                                    Approval = approval

                                };




                                return Ok(response);

                            }

                        }

                    }

                }



                return NotFound($"Penalty with ID '{penalty_id}' not found.");

            }

            catch (Exception ex)

            {

                return StatusCode(500, $"An error occurred while retrieving the security penalty rate: {ex.Message}");

            }

        }

    }

}



















