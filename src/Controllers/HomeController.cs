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

        public IActionResult MainPage(string username, string HK) // Får passet username og HK fra Login der kalder MainPage action metoden
        {
            var reactions = _context.ContrastReactions.ToList();
            ViewBag.userName = username;
            ViewBag.HK = HK;
            return View(reactions);
        }




        // Prøver lige noget med EF her - ved ikke helt om det virker. Har også tilføjet en Login.cs model, og lidt kode til DBcontext (så vi kan bruge ORM på Login(ID, username, og password)).
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
                        return RedirectToAction("MainPage", new { username, HK });

                    }
                }

                ViewBag.ErrorMessage = "Forkert hospitalskode, brugernavn eller adgangskode.";
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Forkert hospitalskode, brugernavn eller adgangskode. Kan også være databasefejl.";
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
                return BadRequest($"Error: {ex.Message}");
            }

            return View("Index");
        }

        [HttpPost]
        public IActionResult CreateReaction(ContrastReactions reaction, string username, string HK)
        {
            if (ModelState.IsValid)
            {
                _context.ContrastReactions.Add(reaction);
                _context.SaveChanges();
                return RedirectToAction("MainPage", new { username, HK }); // Pass username and HK
            }

            // If model state is invalid, return to the view with the model state errors
            ViewBag.userName = username; // Set username for the current request
            ViewBag.HK = HK;             // Set HK for the current request
            return View(reaction);
        }


        [HttpPost]
        public IActionResult DeleteReaction(int id, string username, string HK)
        {
            var reaction = _context.ContrastReactions.Find(id);
            if (reaction != null)
            {
                _context.ContrastReactions.Remove(reaction);
                _context.SaveChanges();
            }
            return RedirectToAction("MainPage", new { username, HK }); // Pass username and HK
        }

    }
}
