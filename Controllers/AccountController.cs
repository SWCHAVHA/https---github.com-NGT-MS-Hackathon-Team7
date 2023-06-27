
using Penalty_Calculation1;

using Penalty_Calculation1.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Penalty_Calculation1.Job;

namespace Penalty_Calculation1.Controllers

{

    public class AccountController : Controller

    {

        private readonly PenaltyCalculationContext _dbContext;




        public AccountController(PenaltyCalculationContext dbContext)

        {

            _dbContext = dbContext;

        }




        public IActionResult Register()

        {

            return View();

        }




        [HttpPost]
    

        public IActionResult Register(User user)

        {

            if (ModelState.IsValid)

            {

                // Hash the password before storing it in the database

                string hashedPassword = HashPassword(user.Password);




                user.Password = hashedPassword;

                _dbContext.Users.Add(user);

                _dbContext.SaveChanges();
                return RedirectToAction("Login");
            }
            return View(user);

        }
        public IActionResult Login()

        {
            return View();
        }

 public IActionResult HomeView()
        {
            return View();
        }

        [HttpPost]
        
        public IActionResult Login(User user)

        {

            if (ModelState.IsValid)

            {

                // Retrieve the user from the database based on the username

                var existingUser = _dbContext.Users.FirstOrDefault(u => u.Username == user.Username);




                if (existingUser != null && VerifyPassword(user.Password, existingUser.Password))

                {

                    // User authenticated successfully

                    // Store authentication state, such as in a session or cookie

                    return RedirectToAction("Index", "Home");

                }

            }

            ModelState.AddModelError(string.Empty, "Invalid username or password");

            return View(user);

        }


       private string HashPassword(string password)

        {

            return BCrypt.Net.BCrypt.HashPassword(password);

        }




        private bool VerifyPassword(string password, string hashedPassword)

        {

            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);

        }

         public IActionResult ProcessJob()
        { // Provide the file path of the JSON file you want to process and update the penalty
            string filePath = "C:/Users/SWCHAVHA/OneDrive - Capgemini/Desktop/Penalty_Calculation1/Penalty_Calculation1/JsonFile.json"; // Create an instance of CombinedJob 
            CombinedJob job = new CombinedJob(); // Call the ProcessJsonFileAndUpdatePenalty method
            job.ProcessJsonFileAndUpdatePenalty(filePath); // Optionally, you can redirect to another view or return a message
            return RedirectToAction("Login");
        }





       

    }

}


