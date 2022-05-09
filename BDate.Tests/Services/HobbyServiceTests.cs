using BDate.Data;
using BDate.Models;
using BDate.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace BDate.Tests {
    public class HobbyServiceTests {
        private readonly ApplicationDbContext _context;

        public HobbyServiceTests() {
            // By suplying a new service provider for each context,
            // we have a single databse instance per test.
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            // Build context options
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "HobbyServiceTests")
                .UseInternalServiceProvider(serviceProvider);

            //Instantiate the context
            _context = new ApplicationDbContext(builder.Options);

            // Seed the database
            _context.Hobbies.AddRange(
                Enumerable.Range(1, 10)
                    .Select(t => new Hobby { 
                        HobbyId = Guid.NewGuid().ToString(), 
                        HobbyName = Guid.NewGuid().ToString()
                    })
            );

            _context.SaveChanges();
        }

        [Fact]
        public async Task GetAllAsync_ReturnsHobbies() {
            // Arrange
            var service = new HobbyService(_context);

            // Act
            var result = await service.GetAllAsync();

            // Assert
            var model = Assert.IsAssignableFrom<IEnumerable<Hobby>>(result);
            Assert.Equal(10, model.Count());
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsNotFound_GivenInvalidId() {
            // Arrange
            var service = new HobbyService(_context);

            // Act
            var result = await service.GetByIdAsync("99");

            // Assert
            Assert.Null(result);
        }
    }
}
