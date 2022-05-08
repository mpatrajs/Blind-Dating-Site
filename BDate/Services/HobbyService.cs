using BDate.Data;
using BDate.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BDate.Services {
    public class HobbyService : IHobbyService {
        private readonly ApplicationDbContext _context;

        public HobbyService(ApplicationDbContext context) {
            _context = context;
        }

        public async Task Add(Hobby hobby) {
            _context.Add(hobby);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(string id) {
            var hobby = await _context.Hobbies.FindAsync(id);
            _context.Hobbies.Remove(hobby);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Hobby>> GetAll() {
            var result = await _context.Hobbies.ToListAsync();
            return result;
        }

        public async Task<Hobby> GetById(string id) {
            var result = await _context.Hobbies
                .FirstOrDefaultAsync(m => m.HobbyId == id);
            return result;
        }

        public async Task Update(Hobby hobby) {
            //var result = await _context.Hobbies.FindAsync(id);
            var result = _context.Update(hobby);
            await _context.SaveChangesAsync();
        }
        public async Task<Hobby> Edit(string id) {
            var result = await _context.Hobbies.FindAsync(id);
            return result;
        }

        public bool HobbyExists(string id) {
            return _context.Hobbies.Any(e => e.HobbyId == id);
        }
    }
}
