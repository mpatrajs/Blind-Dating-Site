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
using Microsoft.AspNetCore.Authorization;

namespace BDate.Controllers
{
    public class ProfilesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public ProfilesController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        // GET: Profiles

        [Authorize(Roles = "ActiveUser")]
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var applicationDbContext = _context.Profiles.Where(p => p.UserId != userId)
                .Include(p => p.Personalities)
                .Include(p => p.Hobbies)
                .Include(p => p.Matches)
                .Include(p => p.Setting)
                .Include(p => p.ApplicationUser);

            // Profile Ids to whom current user sent a match
            var matchesOfCureentUser = await _context.Matches
                .Where(m => m.fromProfileId == userId)
                .Select(m => m.toProfileId)
                .ToListAsync();

            // Profile Ids which sent to current user match offer
            var profileIdOfAlreadyMatchedId = await _context.Matches
                .Where(p => p.toProfileId == userId)
                .Select(p => p.fromProfileId)
                .ToListAsync();

            //var setting = await _context.Settings.Select(s => s.)
            ViewBag.currentUserId = userId;
            ViewBag.matchesOfCureentUser = matchesOfCureentUser;
            ViewBag.profileIdOfAlreadyMatchedId = profileIdOfAlreadyMatchedId;

            return View(await applicationDbContext.ToListAsync());
        }
        [Authorize(Roles = "ActiveUser")]
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
        [Authorize(Roles = "ActiveUser")]
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
        [Authorize(Roles = "InActiveUser")]
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
        [Authorize(Roles = "InActiveUser")]
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

                //Changing users role from InActiveUser to ActiveUser
                var user = _userManager.FindByIdAsync(userId);
                await _userManager.AddToRoleAsync(await user, "ActiveUser");
                await _userManager.RemoveFromRoleAsync(await user, "InActiveUser");
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

                // Automatically sign in user after creating profile to refresh cookie (cookie stores Claims and Roles)
                ApplicationUser applicationUseruser = await _userManager.FindByIdAsync(userId);
                if (applicationUseruser != null)
                {
                    await _signInManager.SignInAsync(applicationUseruser, isPersistent: false);
                }

                return RedirectToAction("Details", "Profiles", new { id = profile.UserId });
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", profile.UserId);
            return View(profile);
        }

        // GET: Profiles/Edit/5
        [Authorize(Roles = "ActiveUser")]
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
        [Authorize(Roles = "ActiveUser")]
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
        [Authorize(Roles = "ActiveUser")]
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
        [Authorize(Roles = "ActiveUser")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _userManager.FindByIdAsync(userId);
            //Changing users role from ActiveUser to InActiveUser
            await _userManager.RemoveFromRoleAsync(await user, "ActiveUser");
            await _userManager.AddToRoleAsync(await user, "InActiveUser");
            await _userManager.UpdateAsync(await user);

            // await _userManager.RemoveFromRoleAsync();
            var profile = await _context.Profiles.FindAsync(id);
            _context.Profiles.Remove(profile);
            await _context.SaveChangesAsync();

            // Automatically sign in user after creating profile to refresh cookie (cookie stores Claims and Roles)
            ApplicationUser applicationUseruser = await _userManager.FindByIdAsync(userId);
            if (applicationUseruser != null)
            {
                await _signInManager.SignInAsync(applicationUseruser, isPersistent: false);
            }

            return RedirectToAction("Create", "Profiles");
        }

        private bool ProfileExists(string id)
        {
            return _context.Profiles.Any(e => e.UserId == id);
        }

        // GET: Match
        [Authorize(Roles = "ActiveUser")]
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
