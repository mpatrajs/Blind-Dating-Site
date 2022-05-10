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
    public class PersonalityControllerTests : IDisposable {
        private readonly ApplicationDbContext _context;

        public PersonalityControllerTests() {
            // By suplying a new service provider for each context,
            // we have a single databse instance per test.
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            // Build context options
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "PersonalityControllerTests")
                .UseInternalServiceProvider(serviceProvider);

            //Instantiate the context
            _context = new ApplicationDbContext(builder.Options);

            // Seed the database
            _context.Personalities.AddRange(
                Enumerable.Range(1, 10)
                    .Select(t => new Personality {
                        PersonalityId = t.ToString(),
                        PersonalityName = "Personality " + t
                    })
            );

            _context.SaveChanges();
        }

        [Fact]
        public async Task Index_ReturnsPersonalities() {
            // Arrange
            var controller = new PersonalitiesController(_context);

            // Act
            var result = await controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Personality>>(viewResult.ViewData.Model);
            Assert.Equal(10, model.Count());
        }

        [Fact]
        public async Task Details_ReturnsPersonality() {
            // Arrange
            var controller = new PersonalitiesController(_context);

            // Act
            var result = await controller.Details("1");

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var task = Assert.IsAssignableFrom<Personality>(viewResult.Model);
            Assert.Equal("Personality 1", task.PersonalityName);
        }

        [Fact]
        public async Task Details_ReturnsNotFound_WhenIdIsAbsent() {
            // Arrange
            var controller = new PersonalitiesController(_context);

            // Act
            var result = await controller.Details(null);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Details_ReturnsNotFound_WhenPersonalityIsAbsent() {
            // Arrange
            var controller = new PersonalitiesController(_context);

            // Act
            var result = await controller.Details("11");

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Create_ReturnsNewlyCreatedPersonality() {
            // Arrange
            var controller = new PersonalitiesController(_context);

            // Act
            var result = await controller.Create(
                new Personality { 
                    PersonalityId = Guid.NewGuid().ToString(), 
                    PersonalityName = Guid.NewGuid().ToString()
                } );

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirectToActionResult.ControllerName);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        [Fact]
        public async Task Create_ReturnsNull_InvalidModelState() {
            // Arrange
            var controller = new PersonalitiesController(_context);
            controller.ModelState.AddModelError("PersonalityName", "Name is required");

            // Act
            var result = await controller.Create(null);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Null(viewResult.ViewData.Model);
        }

        [Fact]
        public void Create_ReturnsViewResultNullModel() {
            // Arrange
            var controller = new PersonalitiesController(_context);

            // Act
            var result = controller.Create();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Null(viewResult.ViewData.Model);
        }

        [Fact]
        public async Task Edit_ReturnsNotFound_WhenIdIsInvalid() {
            // Arrange
            var controller = new PersonalitiesController(_context);

            // Act
            var result = await controller.Edit("11", new Personality { PersonalityId = "11", PersonalityName = "Personality 11" } );

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Edit_ReturnsViewResult() {
            // Arrange
            var controller = new PersonalitiesController(_context);

            // Act
            var result = await controller.Edit("1");

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<Personality>(viewResult.ViewData.Model);
            Assert.Equal("1", model.PersonalityId);
            Assert.Equal("Personality 1", model.PersonalityName);
        }

        [Fact]
        public async Task Delete_ReturnsNotFound_WhenIdIsInvalid() {
            // Arrange
            var controller = new PersonalitiesController(_context);

            // Act
            var result = await controller.Delete("11");

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Delete_DeletesRightPersonality() {
            // Arrange
            var controller = new PersonalitiesController(_context);

            // Act
            var result = await controller.Delete("1");

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<Personality>(viewResult.ViewData.Model);
            Assert.Equal("1", model.PersonalityId);
            Assert.Equal("Personality 1", model.PersonalityName);
        }

        // Helper method which makes fresh data seed for each test
        public void Dispose() {
            _context.Dispose();
        }
    }
}
