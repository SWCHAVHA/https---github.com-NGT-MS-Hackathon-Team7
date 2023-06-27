using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using Penalty_Calculation1.Models;

using System.Data;











namespace PenaltyReportAPI.Controllers
{
    [ApiController]
    [Route("api/Report")]
    public class ReportController : ControllerBase
    {
         private readonly string _connectionString = "Host=localhost;Port=5432;Database=Penality_Calculation;Username=postgres;Password=Priti@12345";

        //private readonly string _connectionString = "";

        [HttpGet]
          [Authorize(Roles = "Admin,User")]
        [Route("DailyReport")]
        public IActionResult GetGenerateDailyPenaltyReport()
        {            
            DataTable table = new DataTable();
            using(NpgsqlConnection con = new NpgsqlConnection(_connectionString))
            {
                try 
                {
                    DateTime currentDate = DateTime.Now.Date;
                    List<ReportModel> reportModels = new List<ReportModel>();
                    string queryForGetPenality = @"select * from public.transaction Where settlement_date = '" +currentDate + "'" ;
                    NpgsqlDataAdapter adp = new NpgsqlDataAdapter(queryForGetPenality, con);                
                    adp.Fill(table);  
                    if(table != null && table.Rows.Count > 0)
                    {
                    HelperForBindingData helper = new HelperForBindingData();
                    reportModels = helper.BindReportData(table);
                    }
                    return Ok(reportModels);              
                }
                catch(Exception ex)
                {
                    return StatusCode(500, $"An error occurred while retrieving the transaction report: {ex.Message}");
                }                
            } 
                   
        }

         [HttpGet]
           
        [Route("MonthlyReport")]
        public IActionResult GetGenerateMonthlyPenaltyReport(int month, int year)
        {            
            DataTable table = new DataTable();
            
            using(NpgsqlConnection con = new NpgsqlConnection(_connectionString))
            {
                try 
                {                    
                    var startDate = new DateTime(year, month, 1);
                    var endDate = startDate.AddMonths(1).AddDays(-1);
                    List<ReportModel> reportModels = new List<ReportModel>();
                    string queryForGetPenality = @"select * from public.transaction Where settlement_date Between '" + startDate + "' and '" + endDate + "'" ;
                    NpgsqlDataAdapter adp = new NpgsqlDataAdapter(queryForGetPenality, con);                
                    adp.Fill(table);  
                    if(table != null && table.Rows.Count > 0)
                    {
                    HelperForBindingData helper = new HelperForBindingData();
                    reportModels = helper.BindReportData(table);
                    }

                    if(reportModels != null && reportModels.Count >0)
                    {
                        return Ok(reportModels); 
                    }
                    return Ok("No record found for that month.");
                                 
                }
                catch(Exception ex)
                {
                    return StatusCode(500, $"An error occurred while retrieving the transaction report: {ex.Message}");
                }                
            } 
                   
        }
        
    }

}