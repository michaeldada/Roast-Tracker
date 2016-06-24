using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using RoastTrackr.Models;
using Microsoft.AspNet.Identity;
using System.Security.Claims;

namespace RoastTrackr.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public HomeController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ApplicationDbContext db)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _db = db;
        }

        private ApplicationDbContext db = new ApplicationDbContext();

        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.FindByIdAsync(User.GetUserId());
            return View(_db.Coffees.Where(x => x.User.Id == currentUser.Id));
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Roast Tracker offers coffee roasters a tool for logging their batches and tracking their inventory. They can look back upon previous roasts to analyze the resulting product and make tweaks to the roast profile. Every time a batch is logged of a certain coffee, it will be deducted from the inventory of that coffee so that roasters can keep a closer eye on their coffee reserves.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Any and all feedback is welcomed at mail.michaeldada@gmail.com";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
