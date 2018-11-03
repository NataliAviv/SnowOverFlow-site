using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SnowOverFlow.Data;
using SnowOverFlow.Models;
using SnowOverFlow.Utility;

namespace SnowOverFlow.Controllers
{

    public class CountriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CountriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Countries
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Country.Include(c => c.Continent);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Countries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var country = await _context.Country
                .Include(c => c.Continent)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (country == null)
            {
                return NotFound();
            }

            return View(country);
        }

        [Authorize(Roles = SD.AdminEndUser)]
        // GET: Countries/Create
        public IActionResult Create()
        {
            ViewData["ContinentID"] = new SelectList(_context.Continent, "ID", "Name");
            return View();
        }

        // POST: Countries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = SD.AdminEndUser)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Language,Currency,ContinentID")] Country country)
        {
            if (ModelState.IsValid)
            {
                _context.Add(country);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ContinentID"] = new SelectList(_context.Continent, "ID", "Name", country.ContinentID);
            return View(country);
        }

        [Authorize(Roles = SD.AdminEndUser)]
        // GET: Countries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var country = await _context.Country.SingleOrDefaultAsync(m => m.ID == id);
            if (country == null)
            {
                return NotFound();
            }
            ViewData["ContinentID"] = new SelectList(_context.Continent, "ID", "Name", country.ContinentID);
            return View(country);
        }

        // POST: Countries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = SD.AdminEndUser)]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Language,Currency,ContinentID")] Country country)
        {
            if (id != country.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(country);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CountryExists(country.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ContinentID"] = new SelectList(_context.Continent, "ID", "Name", country.ContinentID);
            return View(country);
        }
        [Authorize(Roles = SD.AdminEndUser)]
        // GET: Countries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var country = await _context.Country
                .Include(c => c.Continent)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (country == null)
            {
                return NotFound();
            }

            return View(country);
        }
        [Authorize(Roles = SD.AdminEndUser)]
        // POST: Countries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var country = await _context.Country.SingleOrDefaultAsync(m => m.ID == id);
            _context.Country.Remove(country);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CountryExists(int id)
        {
            return _context.Country.Any(e => e.ID == id);
        }

        /*public IActionResult AveRankCountry()
        {
            var countryRank = _context.Site.Include(s => s.Country).GroupBy(a => a.Country.Name)
                                                        .Select(a => new { Name = a.Key, Rank = a.Sum(b => b.Rank) }).ToList();


            var data = new List<dynamic>();

            foreach (var country in countryRank)
            {
                var sitesNumber = _context.Country.Where(x => x.Name.Equals(country.Name)).First().Sites.Count;
                
                var rank = country.Rank / (sitesNumber);
                data.Add(new { companyName = country.Name, Rank = rank });
            }

            return View(data);
        }*/
    }
}
