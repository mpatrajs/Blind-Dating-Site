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
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var applicationDbContext = _context.Profiles.Where(p => p.UserId != currentUserId)
                .Include(p => p.Personalities)
                .Include(p => p.Hobbies)
                .Include(p => p.Matches)
                .Include(p => p.Setting)
                .Include(p => p.ApplicationUser);

            // Profile Ids to whom current user sent a match
            var matchesOfCureentUser = await _context.Matches
                .Where(m => m.fromProfileId == currentUserId)
                .Select(m => m.toProfileId)
                .ToListAsync();

            // Profile Ids which sent to current user match offer
            var profileIdOfAlreadyMatchedId = await _context.Matches
                .Where(p => p.toProfileId == currentUserId)
                .Select(p => p.fromProfileId)
                .ToListAsync();

            //var setting = await _context.Settings.Select(s => s.)

            ViewBag.currentUserId = currentUserId;
            ViewBag.matchesOfCureentUser = matchesOfCureentUser;
            ViewBag.profileIdOfAlreadyMatchedId = profileIdOfAlreadyMatchedId;

            return View(await applicationDbContext.ToListAsync());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> IndexAsync(String profileId)
        {
            //current userId
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            // check if current user already have fromProfileId then redirecttoaction
            // if Matches where toProfileId == userId. Select fromProfileId . to List Contains (profileId this is from url)
            var profileIdOfAlreadyMatchedId = await _context.Matches
                .Where(p => p.toProfileId == userId)
                .Select(p => p.fromProfileId)
                .ToListAsync();

            if (!profileIdOfAlreadyMatchedId.Contains(profileId))
            {
                var match = new Match
                {
                    fromProfileId = userId,
                    toProfileId = profileId,
                };

                _context.Add(match);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Profiles");
            }
            else
            {
                return RedirectToAction("Index", "Profiles");
            }
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
                .Include(p => p.Hobbies)
                .Include(p => p.ApplicationUser)
                .FirstOrDefaultAsync(p => p.UserId == id);

            if (profile == null)
            {
                return NotFound();
            }

            return View(profile);
        }

        // GET: Profiles/Create
        public async Task<IActionResult> Create()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var personalities = await _context.Personalities.ToListAsync();
            var hobbies = await _context.Hobbies.ToListAsync();

            ViewBag.Personality = personalities;
            ViewBag.Hobby = hobbies;

            ViewData["UserId"] = new SelectList(_context.Users.Where(u => u.Id == userId), "Id", "Id");

            return View();
        }

        // POST: Profiles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,FirstName,LastName,DateOfBirth,Gender")] Profile profile, List<String> checkedPersonalityValues, List<String> checkedHobbyValues)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                profile.UserId = userId;
                _context.Add(profile);
                await _context.SaveChangesAsync();

                foreach (var checkedPersonality in checkedPersonalityValues)
                {
                    var personality = _context.Personalities
                    .FirstOrDefaultAsync(m => m.PersonalityId == checkedPersonality);

                    profile.Personalities.Add(await personality);
                }

                foreach (var checkedHobby in checkedHobbyValues)
                {
                    var hobby = _context.Hobbies
                    .FirstOrDefaultAsync(m => m.HobbyId == checkedHobby);

                    profile.Hobbies.Add(await hobby);
                }

                //Changing current user`s isActive field to true (AspNetUsers table)
                var user = _userManager.FindByIdAsync(userId);
                user.Result.IsActive = true;
                await _userManager.UpdateAsync(await user);

                //Add one to one with Setting
                var setting = new Setting
                {
                    SettingId = userId,
                    isHiddenAge = false,
                    isHiddenLastName = false
                };
                _context.Add(setting);
                await _context.SaveChangesAsync();

                _context.Update(profile);
                await _context.SaveChangesAsync();

                return RedirectToAction("Details", "Profiles", new { id = profile.UserId });
                //return RedirectToAction(nameof(Index));
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

            var profile = _context.Profiles
                .Include(p => p.Personalities)
                .Include(p => p.Hobbies)
                .SingleOrDefault(a => a.UserId == id);

            var personalities = await _context.Personalities.ToListAsync();
            var hobbies = await _context.Hobbies.ToListAsync();

            ViewBag.Personality = personalities;
            ViewBag.Hobby = hobbies;

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
        public async Task<IActionResult> Edit(string id, [Bind("UserId,FirstName,LastName,DateOfBirth,Gender")] Profile profile, List<String> checkedPersonalityValues, List<String> checkedHobbyValues)
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

                    var profileOnGet = _context.Profiles
                        .Include(p => p.Personalities)
                        .Include(p => p.Hobbies)
                        .SingleOrDefault(a => a.UserId == id);

                    profileOnGet.Personalities.Clear();
                    profileOnGet.Hobbies.Clear();

                    foreach (var checkedPersonality in checkedPersonalityValues)
                    {
                        var personality = _context.Personalities
                        .FirstOrDefaultAsync(m => m.PersonalityId == checkedPersonality);

                        profile.Personalities.Add(await personality);
                    }

                    foreach (var checkedHobby in checkedHobbyValues)
                    {
                        var hobby = _context.Hobbies
                        .FirstOrDefaultAsync(m => m.HobbyId == checkedHobby);

                        profile.Hobbies.Add(await hobby);
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
                return RedirectToAction("Details", "Profiles", new { id = profile.UserId });
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

        // GET: Match
        public async Task<IActionResult> Match()
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var applicationDbContext = _context.Profiles.Where(p => p.UserId != currentUserId)
                .Include(p => p.Personalities)
                .Include(p => p.Hobbies)
                .Include(p => p.Matches)
                .Include(p => p.Setting)
                .Include(p => p.ApplicationUser);

            var profileIdOfAlreadyMatchedId = await _context.Matches
                .Where(p => p.toProfileId == currentUserId)
                .Select(p => p.fromProfileId)
                .ToListAsync();

            ViewBag.currentUserId = currentUserId;
            ViewBag.profileIdOfAlreadyMatchedId = profileIdOfAlreadyMatchedId;

            return View(await applicationDbContext.ToListAsync());
        }
    }
}
