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
using Microsoft.AspNetCore.Identity;

namespace SnowOverFlow.Controllers
{
  
    public class SitesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly Like _like;

        public SitesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
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
            var sites = from s in _context.Site
                        orderby s.Name ascending
                        select s;

            return View(sites);
        }

        public IActionResult OrderByRank()
        {
            var sites = from s in _context.Site
                        orderby s.Rank ascending
                        select s;

            return View(sites);
        }

        // GET: sites/getLike
        [HttpGet]
        public async Task<IActionResult> getLike(int? siteId)
        {
            var userId = (await _userManager.GetUserAsync(HttpContext.User))?.Id;
            if (userId == null)
            {
                return Unauthorized();
            }
            if (siteId == null)
            {
                return NotFound();
            }

            var site = await _context.Site
                .Include(s => s.Country)
                .SingleOrDefaultAsync(m => m.ID == siteId);
            if (site == null)
            {
                return NotFound();
            }
            //m => m.ApplicationUser. == userId && 
            var like = await _context.Like
                .Include(s => s.ApplicationUser)
                .Include(s => s.Site)
                .SingleOrDefaultAsync(m=> m.ApplicationUser.Id == userId && m.Site.ID == site.ID );



            return Ok(userId);
        }
        [HttpGet("site/getSites")]
        public async Task<IActionResult> getSites(int?[] siteIds, int? countryId)
        {
            var sites2 = _context.Site.Where(x => siteIds.Contains(x.ID));

            return Ok(sites2);
        }


    }
}
