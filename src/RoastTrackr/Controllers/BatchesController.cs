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
    public class BatchesController : Controller
    {
        private ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public BatchesController(UserManager<ApplicationUser> userManager, ApplicationDbContext db)
        {
            _userManager = userManager;
            _db = db;    
        }
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Batches
        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.FindByIdAsync(User.GetUserId());
            return View(_db.Batches.Where(x => x.User.Id == currentUser.Id));
        }

        // GET: Batches/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Batch batch = await _db.Batches.SingleAsync(m => m.BatchId == id);
            if (batch == null)
            {
                return HttpNotFound();
            }

            return View(batch);
        }

        // GET: Batches/Create
        public async Task<IActionResult> Create()
        {
            var currentUser = await _userManager.FindByIdAsync(User.GetUserId());
            ViewData["CoffeeId"] = new SelectList((_db.Coffees.Where(x => x.User.Id == currentUser.Id)), "CoffeeId", "CoffeeName");
            return View();
        }

        // POST: Batches/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Batch batch)
        {
            var currentUser = await _userManager.FindByIdAsync(User.GetUserId());
            if (ModelState.IsValid)
            {
                
                batch.User = currentUser;
                _db.Batches.Add(batch);
                Coffee coffee = await _db.Coffees.SingleAsync(m => m.CoffeeId == batch.CoffeeId);
                coffee.Inventory -= batch.BatchWeight;
                _db.Update(coffee);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewData["CoffeeId"] = new SelectList((_db.Coffees.Where(x => x.User.Id == currentUser.Id)), "CoffeeId", "CoffeeName");
            return View(batch);
        }

        // GET: Batches/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var currentUser = await _userManager.FindByIdAsync(User.GetUserId());
            if (id == null)
            {
                return HttpNotFound();
            }

            Batch batch = await _db.Batches.SingleAsync(m => m.BatchId == id);
            if (batch == null)
            {
                return HttpNotFound();
            }
            ViewData["CoffeeId"] = new SelectList((_db.Coffees.Where(x => x.User.Id == currentUser.Id)), "CoffeeId", "CoffeeName");
            return View(batch);
        }

        // POST: Batches/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Batch batch)
        {
            if (ModelState.IsValid)
            {
                var currentUser = await _userManager.FindByIdAsync(User.GetUserId());
                batch.User = currentUser;
                _db.Update(batch);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["CoffeeId"] = new SelectList(_db.Coffees, "CoffeeId", "CoffeeName", batch.CoffeeId);
            return View(batch);
        }

        // GET: Batches/Delete/5
        [ActionName("Delete")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Batch batch = await _db.Batches.SingleAsync(m => m.BatchId == id);
            if (batch == null)
            {
                return HttpNotFound();
            }

            return View(batch);
        }

        // POST: Batches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Batch batch = await _db.Batches.SingleAsync(m => m.BatchId == id);
            _db.Batches.Remove(batch);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
