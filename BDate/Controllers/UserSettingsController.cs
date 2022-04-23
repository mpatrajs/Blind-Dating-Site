using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BDate.Data;
using BDate.Models;

namespace BDate.Controllers
{
    public class UserSettingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserSettingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: UserSettings
        public async Task<IActionResult> Index()
        {
            return View(await _context.UserSettings.ToListAsync());
        }

        // GET: UserSettings/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userSetting = await _context.UserSettings
                .FirstOrDefaultAsync(m => m.ID == id);
            if (userSetting == null)
            {
                return NotFound();
            }

            return View(userSetting);
        }

        // GET: UserSettings/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UserSettings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,SettingName,SettingOn")] UserSetting userSetting)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userSetting);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(userSetting);
        }

        // GET: UserSettings/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userSetting = await _context.UserSettings.FindAsync(id);
            if (userSetting == null)
            {
                return NotFound();
            }
            return View(userSetting);
        }

        // POST: UserSettings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("ID,SettingName,SettingOn")] UserSetting userSetting)
        {
            if (id != userSetting.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userSetting);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserSettingExists(userSetting.ID))
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
            return View(userSetting);
        }

        // GET: UserSettings/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userSetting = await _context.UserSettings
                .FirstOrDefaultAsync(m => m.ID == id);
            if (userSetting == null)
            {
                return NotFound();
            }

            return View(userSetting);
        }

        // POST: UserSettings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var userSetting = await _context.UserSettings.FindAsync(id);
            _context.UserSettings.Remove(userSetting);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserSettingExists(long id)
        {
            return _context.UserSettings.Any(e => e.ID == id);
        }
    }
}
