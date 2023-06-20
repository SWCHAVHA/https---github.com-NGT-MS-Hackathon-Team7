using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Penalty_Calculation1.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net;

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

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Report()
        {
            return View("ReportOptions");
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
            var holidays = _dbContext.HolidayCalenders.Include("Country").ToList();
            return View(holidays);
        }


        [HttpGet]
        public IActionResult Create()
        {

            var model = new HolidayCalender();

            return View(model);
            //return View();
        }


        [HttpPost]

        public ActionResult Create(HolidayCalender model)

        {


            SaveDataToDatabase(model);

            return RedirectToAction("Holidays");

            return View(model);

        }

        private void SaveDataToDatabase(HolidayCalender model)

        {

            // Implement your logic to save the data to the database

            // Here, I assume you have a method to save the data to your database

            // Example:

            var newData = new HolidayCalender

            {

                HolidayId = model.HolidayId,

                CountryId = model.CountryId,

                HolidayDate = model.HolidayDate,

                LastUpdatedDate = model.LastUpdatedDate,

                Description = model.Description,

                Year = model.Year,

                // Set other properties accordingly

            };

            // Save newData to the database using your database context or repository

        }
/*
        [HttpGet]
        public IActionResult CreateSecurities()
        {
            var model = new SecurityPrice();

            return View(model);
            //return View();
        }

        [HttpPost]

        public ActionResult CreateSecurities(SecurityPrice model)

        {


            SaveDataToDatabase(model);

            return RedirectToAction("Securities");



            return View(model);

        }




        private void SaveDataToDatabase(SecurityPrice model)

        {

            var newData = new SecurityPrice

            {

                PriceId = model.PriceId,

                Poh = model.Poh,

                IsinSecId = model.IsinSecId,

                ValidFromDate = model.ValidFromDate,

                SecPrice = model.SecPrice,

            };

        }
*/

        [HttpGet]
        public IActionResult CreatePenaltyRates()
        {
            var model = new SecurityPenaltyRate();

            return View(model);
            //return View();
        }

        [HttpPost]

        public ActionResult CreatePenaltyRates(SecurityPenaltyRate model)

        {

            if (ModelState.IsValid)

            {

                SaveDataToDatabase(model);

                return RedirectToAction("PenaltyRates");

            }

            return View(model);

        }

        private void SaveDataToDatabase(SecurityPenaltyRate model)

        {

            var newData = new SecurityPenaltyRate

            {

                PenaltyId = model.PenaltyId,

                PenaltyRate = model.PenaltyRate,

                ValidFromDate = model.ValidFromDate,

                LastUpdatedDate = model.LastUpdatedDate,


            };

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
            if (ModelState.IsValid)
            {
                _dbContext.HolidayCalenders.Update(holiday);
                _dbContext.SaveChanges();
                return RedirectToAction("Holidays");
            }
            return View(holiday);
        }

        [HttpPost]
        public IActionResult DeleteHoliday(int id)
        {
            var holiday = _dbContext.HolidayCalenders.Find(id);
            if (holiday == null)
            {
                return NotFound();
            }

            _dbContext.HolidayCalenders.Remove(holiday);
            _dbContext.SaveChanges();
            return RedirectToAction("Holidays");
        }

        public IActionResult Securities()
        {
            var securities = _dbContext.SecurityPrices.ToList();
            return View(securities);
        }

       [HttpGet]
        public IActionResult CreateSecurities(long id)
        {
            var security = _dbContext.SecurityPrices.Find(id);
            if (security == null)
            {
                return NotFound();
            }
            return View(security);
        }

        [HttpPost]
        public IActionResult CreateSecurities(SecurityPrice security)
        {
            if (ModelState.IsValid)
            {
                _dbContext.SecurityPrices.Add(security);
                _dbContext.SaveChanges();
                return RedirectToAction("Securities");
            }



            return View(security);
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

            _dbContext.SecurityPrices.Remove(security);
            _dbContext.SaveChanges();
            return RedirectToAction("Securities");
        }

        public IActionResult PenaltyRatesAll()
        {
            var penaltyRates = _dbContext.SecurityPenaltyRates.ToList();
            return View(penaltyRates);
        }

        [HttpPost]
        public IActionResult CreatePenaltyRate(SecurityPenaltyRate penaltyRate)
        {
            if (ModelState.IsValid)
            {
                _dbContext.SecurityPenaltyRates.Add(penaltyRate);
                _dbContext.SaveChanges();
                return RedirectToAction("PenaltyRates");
            }
            return View(penaltyRate);
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
            if (ModelState.IsValid)
            {
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
            }

            return View(model); // Return the view with validation errors if the model is invalid
        }


        [HttpPost]
        public IActionResult DeletePenaltyRate(long id)
        {
            var penaltyRate = _dbContext.SecurityPenaltyRates.Find(id);
            if (penaltyRate == null)
            {
                return NotFound();
            }

            _dbContext.SecurityPenaltyRates.Remove(penaltyRate);
            _dbContext.SaveChanges();
            return RedirectToAction("PenaltyRates");
        }




    


    }
}
































