using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Penalty_Calculation1.Models;
using Microsoft.EntityFrameworkCore;

using System.Net;
using Microsoft.AspNetCore.Authorization;
using Penalty_Calculation1.Job;

namespace Penalty_Calculation1.Controllers
{
    public class HomeController : Controller
    {
        private readonly PenaltyCalculationContext _dbContext;




        public HomeController(PenaltyCalculationContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult HomeView()

        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }





        public IActionResult Report()
        {
            return View("ReportOptions", new List<Transaction>());
        }

        public IActionResult GenerateReport(int month, int year, string reportType)

        {
            DateOnly currentDate = DateOnly.FromDateTime(DateTime.Today);
            if (reportType == "daily")
            {

                List<Transaction> transactions = _dbContext.Transactions.Where(e => e.SettlementDate == currentDate).ToList();
                return View("ReportOptions", transactions);
            }
            else
            {
                var startDate = new DateOnly(year, month, 1);
                var endDate = startDate.AddMonths(1).AddDays(-1);

                List<Transaction> transactions = _dbContext.Transactions
                    .Where(e => e.SettlementDate >= startDate && e.SettlementDate <= endDate)
                    .ToList();

                return View("ReportOptions", transactions);
            }
        }


        public IActionResult DailyPenaltyReport()
        {
            return View();
        }


        public IActionResult MonthlyPenaltyReport()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult ReferenceData()
        {
            return View();
        }

        public IActionResult Holidays()
        {
            var holidays = _dbContext.HolidayCalenders.Where(b => b.Enable == true).Include("Country").ToList();
            return View(holidays);
        }


        [HttpGet]
        public IActionResult CreateHoliday()
        {
            return View();
        }

        [HttpPost]

        public IActionResult CreateHoliday(HolidayCalender model)

        {

            _dbContext.HolidayCalenders.Add(model);

            _dbContext.SaveChanges();

            ViewBag.Message = "data Insert Successfully";

            return RedirectToAction("Holidays");
        }


        [HttpGet]

        public IActionResult EditHoliday(int id)
        {
            var holiday = _dbContext.HolidayCalenders.Find(id);
            if (holiday == null)
            {
                return NotFound();
            }
            return View(holiday);
        }

        [HttpPost]

        public IActionResult EditHoliday(HolidayCalender holiday)
        {

            DateOnly today = DateOnly.FromDateTime(DateTime.Today);
            if (holiday.HolidayDate < today)
            {
                ModelState.AddModelError("HolidayDate", "Cannot select a past date for the holiday.");
                return View(holiday);
            }

            _dbContext.HolidayCalenders.Update(holiday);
            _dbContext.SaveChanges();
            return RedirectToAction("Holidays");
        }
        [HttpPost]

        public IActionResult DeleteHoliday(int id)
        {
            var holiday = _dbContext.HolidayCalenders.Find(id);
            if (holiday == null)
            {
                return NotFound();
            }

            holiday.Enable = false;
            _dbContext.SaveChanges();
            return RedirectToAction("Holidays");
        }


        [HttpGet]
        public IActionResult SearchHolidays()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SearchHolidays(string country, int year)
        {
            var holidays = _dbContext.HolidayCalenders.Include(c => c.Country).ToList();

            if (!string.IsNullOrEmpty(country))
            {
                holidays = holidays.Where(h => h.Country.CountryName.Trim().Equals(country, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if (year != 0)
            {
                holidays = holidays.Where(h => h.Year == year).ToList();
            }

            return View("Holidays", holidays);
        }



        public IActionResult Securities()
        {
            var securities = _dbContext.SecurityPrices.Where(b => b.Enable == true).ToList();
            return View(securities);
        }

        [HttpGet]

        public IActionResult CreateSecurity()
        {
            return View();
        }

        [HttpPost]

        public IActionResult CreateSecurity(SecurityPrice model)
        {

            _dbContext.SecurityPrices.Add(model);
            _dbContext.SaveChanges();
            ViewBag.Message = "data Insert Successfully";

            return RedirectToAction("Securities");
        }


        [HttpGet]

        public IActionResult EditSecurity(long id)
        {
            var security = _dbContext.SecurityPrices.Find(id);
            if (security == null)
            {
                return NotFound();
            }
            return View(security);
        }
        [HttpPost]

        public IActionResult EditSecurity(SecurityPrice security)
        {
            if (ModelState.IsValid)
            {
                DateOnly today = DateOnly.FromDateTime(DateTime.Today);
                if (security.ValidFromDate < today)
                {
                    ModelState.AddModelError("ValidFromDate", "Cannot select a past date for the security.");
                    return View(security);
                }

                _dbContext.SecurityPrices.Update(security);
                _dbContext.SaveChanges();
                return RedirectToAction("Securities");
            }
            return View(security);
        }

        [HttpPost]

        public IActionResult DeleteSecurity(long id)
        {
            var security = _dbContext.SecurityPrices.Find(id);
            if (security == null)
            {
                return NotFound();
            }

            security.Enable = false;
            _dbContext.SaveChanges();
            return RedirectToAction("Securities");
        }

        [HttpGet]
        public IActionResult SearchSecurities()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SearchSecurities(string priceId)
        {
            if (!string.IsNullOrEmpty(priceId) && long.TryParse(priceId, out long id))
            {
                var securities = _dbContext.SecurityPrices
                    .Where(s => s.PriceId == id)
                    .ToList();

                return View("Securities", securities);
            }

            var allSecurities = _dbContext.SecurityPrices.ToList();
            return View("Securities", allSecurities);
        }


        public IActionResult PenaltyRates()
        {
            var penaltyRates = _dbContext.SecurityPenaltyRates.Where(b => b.Enable == true).ToList();
            return View(penaltyRates);
        }






        [HttpGet]

        public IActionResult CreatePenaltyRate()
        {
            return View();
        }

        [HttpPost]


        public IActionResult CreatePenaltyRate(SecurityPenaltyRate model)
        {


            _dbContext.SecurityPenaltyRates.Add(model);

            ViewBag.Message = "data Insert Successfully";



            _dbContext.SaveChanges();
            return RedirectToAction("PenaltyRates");


        }

        [HttpGet]

        public IActionResult EditPenaltyRate(long id)
        {
            var penaltyRate = _dbContext.SecurityPenaltyRates.Find(id);
            if (penaltyRate == null)
            {
                return NotFound();
            }
            return View(penaltyRate);
        }

        [HttpPost]


        public IActionResult EditPenaltyRate(SecurityPenaltyRate model)
        {
            //if (ModelState.IsValid)
            //{
            DateOnly today = DateOnly.FromDateTime(DateTime.Today);
            if (model.ValidFromDate < today)
            {
                ModelState.AddModelError("ValidFromDate", "Cannot select a past date for the penalty rate.");
                return View(model);
            }

            using (var dbContext = new PenaltyCalculationContext())
            {
                var existingData = dbContext.SecurityPenaltyRates.Find(model.PenaltyId);

                if (existingData != null)
                {
                    existingData.ValidFromDate = model.ValidFromDate;
                    existingData.PenaltyRate = model.PenaltyRate;
                    existingData.LastUpdatedDate = model.LastUpdatedDate;

                    dbContext.SaveChanges();
                }
            }

            return RedirectToAction("PenaltyRates"); // Redirect to a success page or action
                                                     //}

            //return View(model); // Return the view with validation errors if the model is invalid
        }



        [HttpPost]

        public IActionResult DeletePenaltyRate(long id)
        {
            var penaltyRate = _dbContext.SecurityPenaltyRates.Find(id);
            if (penaltyRate == null)
            {
                return NotFound();
            }

            penaltyRate.Enable = false;
            _dbContext.SaveChanges();
            return RedirectToAction("PenaltyRates");

        }


        [HttpGet]
        public IActionResult SearchPenaltyRates()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SearchPenaltyRates(long penaltyId)
        {
            if (penaltyId != 0)
            {
                var penaltyRates = _dbContext.SecurityPenaltyRates
                    .Where(p => p.PenaltyId == penaltyId)
                    .ToList();

                return View("PenaltyRates", penaltyRates);
            }

            var allPenaltyRates = _dbContext.SecurityPenaltyRates.ToList();
            return View("PenaltyRates", allPenaltyRates);
        }

        public IActionResult Penalty()
        {
            return View("PenaltyOptions");
        }

        public IActionResult ViewAllPenalties()
        {
            List<Transaction> transactions = _dbContext.Transactions.ToList();
            return View("AllPenalties", transactions);
        }

        public IActionResult UpdatePenalties()
        {
            List<Transaction> transactions = _dbContext.Transactions.Where(b => b.Enable == true).ToList();
            return View("Transaction", transactions);
        }

        [HttpGet]
        public IActionResult EditPenalties(int id)
        {
            var transaction = _dbContext.Transactions.FirstOrDefault(t => t.TransactionId == id);

            if (transaction == null)
            {
                return NotFound();
            }

            return View(transaction);
        }

        [HttpPost]
        public IActionResult EditPenalties(Transaction transaction)
        {

            var existingTransaction = _dbContext.Transactions.FirstOrDefault(t => t.TransactionId == transaction.TransactionId);

            if (existingTransaction != null)
            {
                existingTransaction.PenaltyAmount = transaction.PenaltyAmount;

                _dbContext.SaveChanges();

                return RedirectToAction("UpdatePenalties");
            }
            return View(transaction);

        }

        [HttpPost]
        public IActionResult RemovePenalties(int id)
        {
            var existingTransaction = _dbContext.Transactions.FirstOrDefault(t => t.TransactionId == id);

            if (existingTransaction != null)
            {
                existingTransaction.Enable = false;
                _dbContext.SaveChanges();
            }

            return RedirectToAction("UpdatePenalties");
        }   



        
    }
}

















