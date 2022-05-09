using BDate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BDate.Services {
    public interface IHobbyService {
        Task<IEnumerable<Hobby>> GetAllAsync();
        Task<Hobby> GetByIdAsync(string id);
        Task AddAsync(Hobby hobby);
        Task UpdateAsync(Hobby hobby);
        Task DeleteAsync(string id);
        bool HobbyExists(string id);
    }
}
