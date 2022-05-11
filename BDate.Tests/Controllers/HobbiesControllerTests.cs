using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BDate.Data;
using BDate.Models;
using BDate.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Microsoft.AspNetCore.Mvc;

namespace BDate.Tests.Controllers {
    public class HobbiesControllerTests : IDisposable {
        private readonly ApplicationDbContext _context;

        public HobbiesControllerTests() {
            // By suplying a new service provider for each context,
            // we have a single databse instance per test.
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            // Build context options
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "HobbiesControllerTests")
                .UseInternalServiceProvider(serviceProvider);

            //Instantiate the context
            _context = new ApplicationDbContext(builder.Options);

            // Seed the database
            _context.Hobbies.AddRange(
                Enumerable.Range(1, 10)
                    .Select(t => new Hobby {
                        HobbyId = t.ToString(),
                        HobbyName = "Hobby " + t
                    })
            );

            _context.SaveChanges();
        }

        [Fact]
        public async Task Index_ReturnsHobbies() {
            // Arrange
            var controller = new HobbiesController(_context);

            // Act
            var result = await controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Hobby>>(viewResult.ViewData.Model);
            Assert.Equal(10, model.Count());
        }

        [Fact]
        public async Task Details_ReturnsHobby() {
            // Arrange
            var controller = new HobbiesController(_context);

            // Act
            var result = await controller.Details("1");

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var task = Assert.IsAssignableFrom<Hobby>(viewResult.Model);
            Assert.Equal("Hobby 1", task.HobbyName);
        }

        [Fact]
        public async Task Details_ReturnsNotFound_WhenIdIsAbsent() {
            // Arrange
            var controller = new HobbiesController(_context);

            // Act
            var result = await controller.Details(null);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Details_ReturnsNotFound_WhenHobbyIsAbsent() {
            // Arrange
            var controller = new HobbiesController(_context);

            // Act
            var result = await controller.Details("11");

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Create_RedirectsToIndex_WhenNewHobbyIsCreated() {
            // Arrange
            var controller = new HobbiesController(_context);

            // Act
            var result = await controller.Create(
                new Hobby {
                    HobbyId = Guid.NewGuid().ToString(),
                    HobbyName = Guid.NewGuid().ToString()
                });

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirectToActionResult.ControllerName);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        [Fact]
        public async Task Create_ReturnsNull_InvalidModelState() {
            // Arrange
            var controller = new HobbiesController(_context);
            controller.ModelState.AddModelError("HobbyName", "Name is required");

            // Act
            var result = await controller.Create(null);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Null(viewResult.ViewData.Model);
        }

        [Fact]
        public void Create_ReturnsViewResultNullModel() {
            // Arrange
            var controller = new HobbiesController(_context);

            // Act
            var result = controller.Create();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Null(viewResult.ViewData.Model);
        }

        [Fact]
        public async Task Edit_ReturnsNotFound_WhenIdIsInvalid() {
            // Arrange
            var controller = new HobbiesController(_context);

            // Act
            var result = await controller.Edit("11", new Hobby { HobbyId = "11", HobbyName = "Hobby 11" });

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Edit_ReturnsViewResult() {
            // Arrange
            var controller = new HobbiesController(_context);

            // Act
            var result = await controller.Edit("1");

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<Hobby>(viewResult.ViewData.Model);
            Assert.Equal("1", model.HobbyId);
            Assert.Equal("Hobby 1", model.HobbyName);
        }

        [Fact]
        public async Task Delete_ReturnsNotFound_WhenIdIsInvalid() {
            // Arrange
            var controller = new HobbiesController(_context);

            // Act
            var result = await controller.Delete("11");

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Delete_DeletesRightHobby() {
            // Arrange
            var controller = new HobbiesController(_context);

            // Act
            var result = await controller.Delete("1");

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<Hobby>(viewResult.ViewData.Model);
            Assert.Equal("1", model.HobbyId);
            Assert.Equal("Hobby 1", model.HobbyName);
        }

        [Fact]
        public async Task DeleteConfirmed_RedirectsToIndex_WhenNewHobbyIsDeleted() {
            // Arrange
            var controller = new HobbiesController(_context);

            // Act
            var result = await controller.DeleteConfirmed("1");

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirectToActionResult.ControllerName);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        // Helper method which makes fresh data seed for each test
        public void Dispose() {
            _context.Dispose();
        }
    }
}
