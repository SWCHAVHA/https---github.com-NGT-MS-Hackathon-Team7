using System;

using System.Collections.Generic;

using System.Linq;

using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using Npgsql;

using System.Collections;

using Penalty_Calculation1.Models;




namespace PenaltyWebApi.Controllers

{

   

    [ApiController]

    [Route("api/transaction")]

    public class HolidayCalendarController : ControllerBase

    {

        private readonly string _connectionString = "Host=localhost;Port=5432;Database=PenaltyCalculation1;Username=postgres;Password=1234";




        [HttpGet("{transaction_id}")]

        public IActionResult GetTransaction(int transaction_id)

        {

            try

            {

                using (var connection = new NpgsqlConnection(_connectionString))

                {

                    connection.Open();




                    var query = "SELECT * FROM transaction WHERE transaction_id = @TransactionId";




                    using (var command = new NpgsqlCommand(query, connection))

                    {

                        command.Parameters.AddWithValue("TransactionId", transaction_id);




                        using (var reader = command.ExecuteReader())

                        {

                            if (reader.Read())

                            {

                                var transaction = new Transaction

                                {

                                    TransactionId = reader.GetInt32(0),

                                    PlaceOfHoldingTechNumber = reader.GetInt16(1),

                                    Isin = reader.GetString(2),

                                    SecurityQuantity = reader.GetInt32(3),

                                    TransactionTypeCode = reader.GetString(4),

                                    InstructionTypeCode = reader.GetString(5),

                                    MatchingReference = reader.GetString(6),

                           

                                    SettlementCashAmount = reader.GetDecimal(8),

                                    CalendarId = reader.GetString(9),

                                    PartyId = reader.GetString(10),

                                    PenaltyId = reader.GetInt64(11),

                                    PriceId = reader.GetInt64(12),

                                    LoginId = reader.GetInt32(13),

                                    CounterPartyId = reader.GetString(14),

                                    PartyRoleCd = reader.GetString(15),

                                    CounterPartyRoleCd = reader.GetString(16),

                                    FailingPartyRoleCd = reader.GetString(17),

                                    PenaltyAmount = reader.GetDecimal(18),

                                    Sign = reader.GetFieldValue<BitArray>(19),

                                    CntlTimestamp = reader.GetDateTime(20),

                                    CntlUserid = reader.GetString(21),

                                    Login = null!, // You can populate these properties if needed

                                    Party = null!,

                                    Penalty = null!,

                                    Price = null!

                                };




                                return Ok(transaction);

                            }

                        }

                    }

                }




                return NotFound($"Transaction with ID '{transaction_id}' not found.");

            }

            catch (Exception ex)

            {

                return StatusCode(500, $"An error occurred while retrieving the transaction: {ex.Message}");

            }

        }

    }

}