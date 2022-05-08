using BDate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BDate.Services {
    public interface IHobbyService {
        Task<IEnumerable<Hobby>> GetAll();
        Task<Hobby> GetById(string id);
        Task<Hobby> Edit(string id);
        Task Add(Hobby hobby);
        Task Update(Hobby hobby);
        Task Delete(string id);
        bool HobbyExists(string id);
    }
}
