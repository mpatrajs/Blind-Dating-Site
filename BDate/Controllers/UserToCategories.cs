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
    public class UserToCategories : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserToCategories(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: UserToCategories
        public async Task<IActionResult> Index()
        {
            return View(await _context.UserToCategories.ToListAsync());
        }

        // GET: UserToCategories/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userToCategories = await _context.UserToCategories
                .FirstOrDefaultAsync(m => m.Relation_ID == id);
            if (userToCategories == null)
            {
                return NotFound();
            }

            return View(userToCategories);
        }

        // GET: UserToCategories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UserToCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Relation_ID,User_ID,Kategorijas_ID")] UserToCategories userToCategories)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userToCategories);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(userToCategories);
        }

        // GET: UserToCategories/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userToCategories = await _context.UserToCategories.FindAsync(id);
            if (userToCategories == null)
            {
                return NotFound();
            }
            return View(userToCategories);
        }

        // POST: UserToCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Relation_ID,User_ID,Kategorijas_ID")] UserToCategories userToCategories)
        {
            if (id != userToCategories.Relation_ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userToCategories);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserToCategoriesExists(userToCategories.Relation_ID))
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
            return View(userToCategories);
        }

        // GET: UserToCategories/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userToCategories = await _context.UserToCategories
                .FirstOrDefaultAsync(m => m.Relation_ID == id);
            if (userToCategories == null)
            {
                return NotFound();
            }

            return View(userToCategories);
        }

        // POST: UserToCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var userToCategories = await _context.UserToCategories.FindAsync(id);
            _context.UserToCategories.Remove(userToCategories);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserToCategoriesExists(long id)
        {
            return _context.UserToCategories.Any(e => e.Relation_ID == id);
        }
    }
}
