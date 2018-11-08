using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SnowOverFlow.Data;
using SnowOverFlow.Models;

namespace SnowOverFlow.Controllers
{
    public class LikesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public LikesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Likes
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Like.Include(l => l.ApplicationUser).Include(l => l.Site);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Likes/Details/5
        public async Task<IActionResult> Details(string userId, int siteId)
        {
            if (userId == null)
            {
                return NotFound();
            }

            var like = await _context.Like
                .Include(l => l.ApplicationUser)
                .Include(l => l.Site)
                .SingleOrDefaultAsync(m => m.UserID == userId && m.SiteID == siteId);
            if (like == null)
            {
                return NotFound();
            }

            return View(like);
        }

        // GET: Likes/Create
        public IActionResult Create()
        {
            ViewData["UserID"] = new SelectList(_context.ApplicationUser, "Id", "Id");
            ViewData["SiteID"] = new SelectList(_context.Site, "ID", "Name");
            return View();
        }

        // POST: Likes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserID,SiteID")] Like like)
        {
            if (ModelState.IsValid)
            {
                _context.Add(like);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserID"] = new SelectList(_context.ApplicationUser, "Id", "Id", like.UserID);
            ViewData["SiteID"] = new SelectList(_context.Site, "ID", "Name", like.SiteID);
            return View(like);
        }

        [HttpGet("toggle_like")]
        public async Task<IActionResult> likethis(int siteId)
        {
            Like newLike;
            var userId = (await _userManager.GetUserAsync(HttpContext.User))?.Id;

            if (userId == null) return Unauthorized();

            var like = await _context.Like.FindAsync(userId, siteId);

            if (like == null)
            {
                like = new Like() { UserID = userId, SiteID = siteId };
                _context.Like.Add(like);
            }
            else
            {
                _context.Remove(like);
            }

            await _context.SaveChangesAsync();
            newLike = new Like();
            newLike.SiteID = siteId;
            newLike.UserID = userId;

            return Ok();
        }

        [HttpGet("getLikes")]
        public async Task<IActionResult> getLikes(int siteId)
        {
            var userId = (await _userManager.GetUserAsync(HttpContext.User))?.Id;
            var likes = _context.Like;
            var likes2 = likes.ToListAsync();
            var result = new { likes=likes2};


            return Ok(new {likes=likes2,user= userId });
        }


        // GET: Likes/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var like = await _context.Like.SingleOrDefaultAsync(m => m.UserID == id);
            if (like == null)
            {
                return NotFound();
            }
            ViewData["UserID"] = new SelectList(_context.ApplicationUser, "Id", "Id", like.UserID);
            ViewData["SiteID"] = new SelectList(_context.Site, "ID", "Name", like.SiteID);
            return View(like);
        }

        // POST: Likes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("UserID,SiteID")] Like like)
        {
            if (id != like.UserID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(like);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LikeExists(like.UserID))
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
            ViewData["UserID"] = new SelectList(_context.ApplicationUser, "Id", "Id", like.UserID);
            ViewData["SiteID"] = new SelectList(_context.Site, "ID", "Name", like.SiteID);
            return View(like);
        }

        // GET: Likes/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var like = await _context.Like
                .Include(l => l.ApplicationUser)
                .Include(l => l.Site)
                .SingleOrDefaultAsync(m => m.UserID == id);
            if (like == null)
            {
                return NotFound();
            }

            return View(like);
        }

        // POST: Likes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var like = await _context.Like.SingleOrDefaultAsync(m => m.UserID == id);
            _context.Like.Remove(like);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LikeExists(string id)
        {
            return _context.Like.Any(e => e.UserID == id);
        }
    }
}
