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
    public class PersonalitiesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PersonalitiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Personalities
        public async Task<IActionResult> Index()
        {
            return View(await _context.Personalities.ToListAsync());
        }

        // GET: Personalities/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personality = await _context.Personalities
                .FirstOrDefaultAsync(m => m.PersonalityId == id);
            if (personality == null)
            {
                return NotFound();
            }

            return View(personality);
        }

        // GET: Personalities/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Personalities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PersonalityId,PersonalityName")] Personality personality)
        {
            if (ModelState.IsValid)
            {
                _context.Add(personality);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(personality);
        }

        // GET: Personalities/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personality = await _context.Personalities.FindAsync(id);
            if (personality == null)
            {
                return NotFound();
            }
            return View(personality);
        }

        // POST: Personalities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("PersonalityId,PersonalityName")] Personality personality)
        {
            if (id != personality.PersonalityId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(personality);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonalityExists(personality.PersonalityId))
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
            return View(personality);
        }

        // GET: Personalities/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personality = await _context.Personalities
                .FirstOrDefaultAsync(m => m.PersonalityId == id);
            if (personality == null)
            {
                return NotFound();
            }

            return View(personality);
        }

        // POST: Personalities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var personality = await _context.Personalities.FindAsync(id);
            _context.Personalities.Remove(personality);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonalityExists(string id)
        {
            return _context.Personalities.Any(e => e.PersonalityId == id);
        }
    }
}
