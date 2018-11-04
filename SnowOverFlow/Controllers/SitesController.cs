using System;
using System.Collections.Generic;
using System.Data;
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

    public class SitesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SitesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Sites
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Site.Include(s => s.Country);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Sites/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var site = await _context.Site
                .Include(s => s.Country)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (site == null)
            {
                return NotFound();
            }

            return View(site);
        }
        [Authorize(Roles = SD.AdminEndUser)]
        // GET: Sites/Create
        public IActionResult Create()
        {
            ViewData["CountryId"] = new SelectList(_context.Country, "ID", "Name");
            return View();
        }

        // POST: Sites/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = SD.AdminEndUser)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,CountryId,Rank,SeasonStart,SeasonEnd,Latitude,Longtitude,Pistes,Difficulty,BeerPrice")] Site site)
        {
            if (ModelState.IsValid)
            {
                _context.Add(site);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CountryId"] = new SelectList(_context.Country, "ID", "Name", site.CountryId);
            return View(site);
        }

        [Authorize(Roles = SD.AdminEndUser)]
        // GET: Sites/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var site = await _context.Site.SingleOrDefaultAsync(m => m.ID == id);
            if (site == null)
            {
                return NotFound();
            }
            ViewData["CountryId"] = new SelectList(_context.Country, "ID", "Name", site.CountryId);
            return View(site);
        }
        [Authorize(Roles = SD.AdminEndUser)]
        // POST: Sites/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,CountryId,Rank,SeasonStart,SeasonEnd,Latitude,Longtitude,Pistes,Difficulty,BeerPrice")] Site site)
        {
            if (id != site.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(site);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SiteExists(site.ID))
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
            ViewData["CountryId"] = new SelectList(_context.Country, "ID", "Name", site.CountryId);
            return View(site);
        }

        [Authorize(Roles = SD.AdminEndUser)]
        // GET: Sites/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var site = await _context.Site
                .Include(s => s.Country)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (site == null)
            {
                return NotFound();
            }

            return View(site);
        }

        [Authorize(Roles = SD.AdminEndUser)]
        // POST: Sites/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var site = await _context.Site.SingleOrDefaultAsync(m => m.ID == id);
            _context.Site.Remove(site);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SiteExists(int id)
        {
            return _context.Site.Any(e => e.ID == id);
        }


        public IActionResult OrderByName()
        {
            var SitesByName = from s in _context.Site
                              orderby s.Name ascending
                              select s;

            return View(SitesByName);
        }

        public IActionResult OrderByRank()
        {
            var SitesByRank = from s in _context.Site
                              orderby s.Rank ascending
                              select s;

            return View(SitesByRank);
        }

        public IActionResult GroupByCountry()
        {
            var SitesByCountry = _context.Site.GroupBy(s => s.Country).SelectMany(c => c).ToList();

            /*var SitesByCountry = from s in _context.Site
                                 group s by s.Country into g
                                 orderby g.Key
                                 select new { name = g , Country = g.Key };*/

            return View(SitesByCountry);
        }

        public IActionResult MinimumBeerPrice()
        {
            var MinBeerPrice = from s in _context.Site
                               orderby s.BeerPrice ascending
                               select s;

            return View(MinBeerPrice);
        }




        public IActionResult PriceLess20()
        {

            /*IEnumerable<Site> result = from s in _context.Site
                                       join c in _context.Country
                                       on s.CountryId equals c.ID
                                       where s.BeerPrice <= 20
                                       select s;*/

            IEnumerable<Site> result = _context.Site.Join(_context.Country, s => s.CountryId, c => c.ID,
                                       (s , cs) => new { s , cs })
                                       .Where(tp => tp.s.BeerPrice < 20)
                                       .OrderBy(tp => tp.s.BeerPrice)
                                       .Select(tp => tp.s);

            return View(result);
        }

        /*public IActionResult CountrySitesCounter()
        {

            var result = _context.Country.Join(_context.Site, c => c.ID, s => s.CountryId,
                                          (c, s) => new { c, s })
                                          .GroupBy(tp => tp.c, tp => tp.s)
                                          .Select(g => new { g, NumSites = g.Count() })
                                          .OrderByDescending(tp2 => tp2.NumSites)
                                          .Select(tp2 => new { tp2.g.Key.Name , tp2.NumSites });

            var group = new List<Country>();
            foreach (var t in result)
            {
                group.Add(new Country()
                {
                    Name = t.Name,
                });
            }

            return View(group);
        }*/


    }
}
