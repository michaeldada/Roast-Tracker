using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using RoastTrackr.Models;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Identity;
using System.Security.Claims;

namespace RoastTrackr.Controllers
{
    [Authorize]
    public class CoffeesController : Controller
    {
        private ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public CoffeesController(UserManager<ApplicationUser> userManager, ApplicationDbContext db)
        {
            _userManager = userManager;
            _db = db;    
        }

        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Coffees
        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.FindByIdAsync(User.GetUserId());
            return View(_db.Coffees.Where(x => x.User.Id == currentUser.Id));
        }

        // GET: Coffees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Coffee coffee = await _db.Coffees.SingleAsync(m => m.CoffeeId == id);
            if (coffee == null)
            {
                return HttpNotFound();
            }

            return View(coffee);
        }

        // GET: Coffees/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Coffees/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Coffee coffee)
        {
            if (ModelState.IsValid)
            {
                var currentUser = await _userManager.FindByIdAsync(User.GetUserId());
                coffee.User = currentUser;
                _db.Coffees.Add(coffee);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(coffee);
        }

        // GET: Coffees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Coffee coffee = await _db.Coffees.SingleAsync(m => m.CoffeeId == id);
            if (coffee == null)
            {
                return HttpNotFound();
            }
            return View(coffee);
        }

        // POST: Coffees/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Coffee coffee)
        {
            if (ModelState.IsValid)
            {
                var currentUser = await _userManager.FindByIdAsync(User.GetUserId());
                coffee.User = currentUser;
                _db.Update(coffee);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(coffee);
        }

        // GET: Coffees/Delete/5
        [ActionName("Delete")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Coffee coffee = await _db.Coffees.SingleAsync(m => m.CoffeeId == id);
            if (coffee == null)
            {
                return HttpNotFound();
            }

            return View(coffee);
        }

        // POST: Coffees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Coffee coffee = await _db.Coffees.SingleAsync(m => m.CoffeeId == id);
            _db.Coffees.Remove(coffee);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
