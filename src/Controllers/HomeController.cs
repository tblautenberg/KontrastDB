using KontrastDB.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using KontrastDB.Models;
using MySql.Data.MySqlClient;
using System.Linq.Expressions;

namespace BugSplatter.Controllers {

	public class HomeController : Controller 
	{

        private readonly KontrastContext _context;

        public HomeController(KontrastContext context) // Injecter vores context til HomeController constructeren
        {
            _context = context;
        }

        public IActionResult Index() {
			return View();
		}


        // Pr�ver lige noget med EF her - ved ikke helt om det virker. Har ogs� tilf�jet en Login.cs model, og lidt kode til DBcontext (s� vi kan bruge ORM p� Login(ID, username, og password)).
        [HttpPost]
        public IActionResult Login(string HK, string username, string password)
        {
            try
            {
                var user = _context.Users.FirstOrDefault(u => u.username == username);

                if (user != null)
                {

                    if (user.password == password && user.HK == HK)
                    {
                        ViewBag.userName = username;
                        return View("MainPage");
                    }
                }

                ViewBag.ErrorMessage = "Forkert hospitalskode, brugernavn eller adgangskode.";
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Forkert hospitalskode, brugernavn eller adgangskode. Kan ogs� v�re databasefejl.";
                Console.WriteLine($"Exception: {ex.Message}");
            }

            return View("Index");
        }

        public IActionResult LogOut()
        {
            return View("Index");
        }

        [HttpPost]
        public IActionResult TestCon()
        {
            try
            {
                var canConnect = _context.Database.CanConnect();

                if (canConnect)
                {
                    ViewBag.Message = "Forbindelsen til databasen er online!";
                }
                else
                {
                    ViewBag.Message = "Kunne ikke tilslutte databasen.";
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Couldent send request to the database. Are we online?";
            }

            return View("Index");
        }
    }
}
