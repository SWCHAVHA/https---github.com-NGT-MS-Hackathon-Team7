using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using Newtonsoft.Json;
using Npgsql;
using Penalty_Calculation1.Models;

namespace Penalty_Calculation1.Job
{
    public class CombinedJob
    {
        private NpgsqlConnection GetConnection()
        {
            return new NpgsqlConnection("Server=localhost;Port=5432;Database=Penalty_Calculation;User Id=postgres;Password=Priti@12345");
        }

        private void SaveJsonFileToDatabase(string filePath)
        {
            if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
            {
                // Handle invalid file path or file not found
                Console.WriteLine("Invalid file path or file not found.");
                return;
            }

            try
            {
                string jsonContent = File.ReadAllText(filePath);
                var data = JsonConvert.DeserializeObject<List<Transaction>>(jsonContent);

                using (var dbContext = new PenaltyCalculationContext())
                {
                    if (data != null)
                    {
                        dbContext.Transactions!.AddRange(data);
                        dbContext.SaveChanges();
                    }
                }

                // Data saved successfully
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while saving data: {ex.Message}");
            }
        }

        private DataTable GetPenaltyRecord()
        {
            DataTable table = new DataTable();

            using (NpgsqlConnection con = GetConnection())
            {
                try
                {
                    string queryForGetPenalty = @"SELECT t.transaction_id, t.security_quantity, r.penalty_rate, p.sec_price FROM public.transaction t INNER JOIN public.security_price p ON t.isin = p.isin_sec_id INNER JOIN public.security_penalty_rate r ON p.isin_sec_id = r.isin ORDER BY t.transaction_id";

                    NpgsqlDataAdapter adp = new NpgsqlDataAdapter(queryForGetPenalty, con);
                    adp.Fill(table);

                    return table;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred while retrieving penalty records: {ex.Message}");
                }

                return table;
            }
        }

        private decimal? CalculatePenaltyRecord(PenaltyModel model)
        {
            var calculatePenaltyAmount = model.FailedTransactionQty * model.SecurityPrice * model.SecurityPenaltyRate;
            return calculatePenaltyAmount;
        }

        private void UpdatePenaltyRecord(int transactionId, decimal penaltyAmount)
        {
            using (NpgsqlConnection con = GetConnection())
            {
                try
                {
                    con.Open();
                    string queryForUpdatePenalty = $"UPDATE transaction SET penalty_amount = {penaltyAmount} WHERE transaction_id = {transactionId}";
                    NpgsqlCommand cmd = new NpgsqlCommand(queryForUpdatePenalty, con);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine($"An error occurred while updating penalty record: {e.Message}");
                }
                finally
                {
                    con.Close();
                }
            }
        }

        public void ProcessJsonFileAndUpdatePenalty(string filePath)
        {
            SaveJsonFileToDatabase(filePath);

            DataTable table = GetPenaltyRecord();

            if (table != null && table.Rows.Count > 0)
            {
                foreach (DataRow row in table.Rows)
                {
                    PenaltyModel penaltyModel = new PenaltyModel();

                    penaltyModel.TransactionId = Convert.ToInt32(row["transaction_id"].ToString());
                    penaltyModel.FailedTransactionQty = Convert.ToInt32(row["security_quantity"].ToString());
                    penaltyModel.SecurityPrice = Convert.ToDecimal(row["sec_price"].ToString());
                    penaltyModel.SecurityPenaltyRate = Convert.ToDecimal(row["penalty_rate"].ToString());

                    if (penaltyModel != null)
                    {
                        var calculatePenaltyAmount = CalculatePenaltyRecord(penaltyModel);
                        UpdatePenaltyRecord((int)penaltyModel.TransactionId, (decimal)calculatePenaltyAmount);
                    }
                }
            }
        }
    }
}
