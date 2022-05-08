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
using BDate.Services;

namespace BDate.Controllers
{
    public class HobbiesController : Controller
    {
        private readonly IHobbyService _service;

        public HobbiesController(IHobbyService service)
        {
            _service = service;
        }

        // GET: Hobbies
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var data = _service.GetAll();
            return View(await data);
        }

        // GET: Hobbies/Details/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var data = _service.GetById(id);
            if (data == null)
            {
                return NotFound();
            }

            return View(await data);
        }

        // GET: Hobbies/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Hobbies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HobbyId,HobbyName")] Hobby hobby)
        {
            if (ModelState.IsValid)
            {
                await _service.Add(hobby);
                return RedirectToAction(nameof(Index));
            }
            return View(hobby);
        }

        // GET: Hobbies/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //var result = await _context.Hobbies.FindAsync(id);
            var data = _service.Edit(id);
            if (data == null)
            {
                return NotFound();
            }
            return View(await data);
        }

        // POST: Hobbies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("HobbyId,HobbyName")] Hobby hobby)
        {
            if (id != hobby.HobbyId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _service.Update(hobby);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_service.HobbyExists(hobby.HobbyId))
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
            return View(hobby);
        }

        // GET: Hobbies/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var data = await _service.GetById(id);
            if (data == null)
            {
                return NotFound();
            }

            return View(data);
        }

        // POST: Hobbies/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            //var hobby = await _context.Hobbies.FindAsync(id);
            //_context.Hobbies.Remove(hobby);
            //await _context.SaveChangesAsync();
            await _service.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
