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

        public async Task<IEnumerable<Hobby>> GetAllAsync() {
            var result = await _context.Hobbies.ToListAsync();
            return result;
        }

        public async Task<Hobby> GetByIdAsync(string id) {
            var result = await _context.Hobbies.FindAsync(id);
            return result;
        }

        public async Task AddAsync(Hobby hobby) {
            _context.Add(hobby);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Hobby hobby) {
            _context.Update(hobby);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id) {
            var hobby = await _context.Hobbies.FindAsync(id);
            _context.Hobbies.Remove(hobby);
            await _context.SaveChangesAsync();
        }

        public bool HobbyExists(string id) {
            return _context.Hobbies.Any(e => e.HobbyId == id);
        }
    }
}
