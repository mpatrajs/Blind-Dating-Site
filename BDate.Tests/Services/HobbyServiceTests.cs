using BDate.Data;
using BDate.Models;
using BDate.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace BDate.Tests {
    public class HobbyServiceTests {
        [Fact]
        public async Task GetAllAsync_ReturnsHobbies() {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "GetAllAsync_ReturnsHobbies")
                .Options;

            var context = new ApplicationDbContext(options);

            context.Hobbies.AddRange(
                Enumerable.Range(1, 10)
                    .Select(t => new Hobby { 
                        HobbyId = Guid.NewGuid().ToString(), 
                        HobbyName = Guid.NewGuid().ToString()
                    })
            );

            context.SaveChanges();

            var service = new HobbyService(context);

            // Act
            var result = await service.GetAllAsync();

            // Assert
            var model = Assert.IsAssignableFrom<IEnumerable<Hobby>>(result);
            Assert.Equal(10, model.Count());
        }

    }
}
