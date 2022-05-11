using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BDate.Data;
using BDate.Models;
using Microsoft.AspNetCore.Authorization;

namespace BDate.Controllers
{
    public class SettingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SettingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Settings
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Settings.Include(s => s.Profile);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Settings/Details/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var setting = await _context.Settings
                .Include(s => s.Profile)
                .FirstOrDefaultAsync(m => m.SettingId == id);
            if (setting == null)
            {
                return NotFound();
            }

            return View(setting);
        }

        // GET: Settings/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewData["SettingId"] = new SelectList(_context.Profiles, "UserId", "UserId");
            return View();
        }

        // POST: Settings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("SettingId,isHiddenAge,isHiddenLastName")] Setting setting)
        {
            if (ModelState.IsValid)
            {
                _context.Add(setting);
                await _context.SaveChangesAsync();
                ViewData["SettingId"] = new SelectList(_context.Profiles, "UserId", "UserId", setting.SettingId);
                return RedirectToAction(nameof(Index));
            }
            //ViewData["SettingId"] = new SelectList(_context.Profiles, "UserId", "UserId", setting.SettingId);
            return View(setting);
        }

        // GET: Settings/Edit/5
        [Authorize(Roles = "ActiveUser")]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var setting = await _context.Settings.FindAsync(id);
            if (setting == null)
            {
                return NotFound();
            }
            ViewData["SettingId"] = new SelectList(_context.Profiles, "UserId", "UserId", setting.SettingId);
            return View(setting);
        }

        // POST: Settings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "ActiveUser")]
        public async Task<IActionResult> Edit(string id, [Bind("SettingId,isHiddenAge,isHiddenLastName")] Setting setting)
        {
            if (id != setting.SettingId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(setting);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SettingExists(setting.SettingId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                //return RedirectToAction(nameof(Index));
                return RedirectToAction("Details", "Profiles", new { id = setting.SettingId });
            }
            ViewData["SettingId"] = new SelectList(_context.Profiles, "UserId", "UserId", setting.SettingId);
            return View(setting);
        }

        // GET: Settings/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var setting = await _context.Settings
                .Include(s => s.Profile)
                .FirstOrDefaultAsync(m => m.SettingId == id);
            if (setting == null)
            {
                return NotFound();
            }

            return View(setting);
        }

        // POST: Settings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var setting = await _context.Settings.FindAsync(id);
            _context.Settings.Remove(setting);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SettingExists(string id)
        {
            return _context.Settings.Any(e => e.SettingId == id);
        }
    }
}
