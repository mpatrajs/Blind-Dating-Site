using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BDate.Data;
using BDate.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace BDate.Controllers
{
    public class ProfilesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public ProfilesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Profiles
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Profiles.Include(p => p.ApplicationUser);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Profiles/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profile = await _context.Profiles
                .Include(p => p.Personalities)
                .Include(p => p.ApplicationUser)
                .FirstOrDefaultAsync(m => m.UserId == id);

            if (profile == null)
            {
                return NotFound();
            }

            return View(profile);
        }

        // GET: Profiles/Create
        public IActionResult Create()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewData["UserId"] = new SelectList(_context.Users.Where(u => u.Id == userId), "Id", "Id");
            return View();
        }

        // POST: Profiles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,FirstName,LastName,DateOfBirth,Gender")] Profile profile)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                profile.UserId = userId;
                _context.Add(profile);
                await _context.SaveChangesAsync();

                //Changing current user`s isActive field to true (AspNetUsers table)
                var user = _userManager.FindByIdAsync(userId);
                user.Result.IsActive = true;
                await _userManager.UpdateAsync(await user);

                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", profile.UserId);
            return View(profile);
        }

        // GET: Profiles/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var profile = _context.Profiles.Include(p => p.Personalities)
            .SingleOrDefault(a => a.UserId == id);


            //var profile = await _context.Profiles.FindAsync(id);


            /*            var profile = await _context.Profiles
                        .Include(p => p.Personalities)
                        .Include(p => p.ApplicationUser)
                        .FirstOrDefaultAsync(m => m.UserId == id);*/

            var personalities = await _context.Personalities.ToListAsync();
            ViewBag.Personality = personalities;
            if (profile == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", profile.UserId);
            return View(profile);
        }

        // POST: Profiles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("UserId,FirstName,LastName,DateOfBirth,Gender")] Profile profile, List<String> checkedValues)
        {
            if (id != profile.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                   _context.Update(profile);
                    var profileOnGet = _context.Profiles.Include(p => p.Personalities).SingleOrDefault(a => a.UserId == id);
                    profileOnGet.Personalities.Clear();

                    foreach (var checkedPersonality in checkedValues)
                    {
                        var personality = _context.Personalities
                        .FirstOrDefaultAsync(m => m.PersonalityId == checkedPersonality);

                        profile.Personalities.Add(await personality);
                    }

                    _context.Update(profile);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProfileExists(profile.UserId))
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
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", profile.UserId);
            return View(profile);
        }

        // GET: Profiles/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profile = await _context.Profiles
                .Include(p => p.ApplicationUser)
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (profile == null)
            {
                return NotFound();
            }

            return View(profile);
        }

        // POST: Profiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var profile = await _context.Profiles.FindAsync(id);
            _context.Profiles.Remove(profile);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProfileExists(string id)
        {
            return _context.Profiles.Any(e => e.UserId == id);
        }
    }
}
