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
    public class SettingsControllerTests : IDisposable {
        private readonly ApplicationDbContext _context;

        public SettingsControllerTests() {
            // By suplying a new service provider for each context,
            // we have a single databse instance per test.
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            // Build context options
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "SettingsControllerTests")
                .UseInternalServiceProvider(serviceProvider);

            //Instantiate the context
            _context = new ApplicationDbContext(builder.Options);

            // Seed the database
            _context.Settings.AddRange(
                Enumerable.Range(1, 10)
                    .Select(t => new Setting() {
                        Profile = new Profile() { UserId = t.ToString() },
                        SettingId = t.ToString(),
                        isHiddenAge = false,
                        isHiddenLastName = false
                    })
            );

            _context.SaveChanges();
        }

        [Fact]
        public async Task Index_ReturnsSettings() {
            // Arrange
            var controller = new SettingsController(_context);

            // Act
            var result = await controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Setting>>(viewResult.ViewData.Model);
            Assert.Equal(10, model.Count());
        }

        [Fact]
        public async Task Details_ReturnsSetting() {
            // Arrange
            var controller = new SettingsController(_context);

            // Act
            var result = await controller.Details("1");

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var task = Assert.IsAssignableFrom<Setting>(viewResult.Model);
            Assert.Equal("1", task.SettingId);
        }

        [Fact]
        public async Task Details_ReturnsNotFound_WhenIdIsAbsent() {
            // Arrange
            var controller = new SettingsController(_context);

            // Act
            var result = await controller.Details(null);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Details_ReturnsNotFound_WhenSettingIsAbsent() {
            // Arrange
            var controller = new SettingsController(_context);

            // Act
            var result = await controller.Details("11");

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Create_RedirectsToIndex_WhenNewSettingIsCreated() {
            // Arrange
            var controller = new SettingsController(_context);

            // Act
            var result = await controller.Create(
                new Setting() {
                    Profile = new Profile() { UserId = Guid.NewGuid().ToString() },
                    SettingId = Guid.NewGuid().ToString(),
                    isHiddenAge = false,
                    isHiddenLastName = false
                });

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirectToActionResult.ControllerName);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        [Fact]
        public async Task Create_ReturnsNull_InvalidModelState() {
            // Arrange
            var controller = new SettingsController(_context);
            controller.ModelState.AddModelError("SettingId", "Id is required");

            // Act
            var result = await controller.Create(null);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Null(viewResult.ViewData.Model);
        }

        [Fact]
        public void Create_ReturnsViewResultNullModel() {
            // Arrange
            var controller = new SettingsController(_context);

            // Act
            var result = controller.Create();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Null(viewResult.ViewData.Model);
        }

        [Fact]
        public async Task Edit_ReturnsNotFound_WhenIdIsInvalid() {
            // Arrange
            var controller = new SettingsController(_context);

            // Act
            var result = await controller.Edit("11", new Setting() {
                Profile = new Profile() { UserId = "11" },
                SettingId = "11",
                isHiddenAge = true,
                isHiddenLastName = true
            });

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Edit_ReturnsViewResult() {
            // Arrange
            var controller = new SettingsController(_context);

            // Act
            var result = await controller.Edit("1");

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<Setting>(viewResult.ViewData.Model);
            Assert.Equal("1", model.Profile.UserId);
            Assert.Equal("1", model.SettingId);
            Assert.False(model.isHiddenAge);
            Assert.False(model.isHiddenLastName);
        }

        [Fact]
        public async Task Delete_ReturnsNotFound_WhenIdIsInvalid() {
            // Arrange
            var controller = new SettingsController(_context);

            // Act
            var result = await controller.Delete("11");

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Delete_DeletesRightSetting() {
            // Arrange
            var controller = new SettingsController(_context);

            // Act
            var result = await controller.Delete("1");

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<Setting>(viewResult.ViewData.Model);
            Assert.Equal("1", model.Profile.UserId);
            Assert.Equal("1", model.SettingId);
            Assert.False(model.isHiddenAge);
            Assert.False(model.isHiddenLastName);
        }

        [Fact]
        public async Task DeleteConfirmed_RedirectsToIndex_WhenNewSettingIsDeleted() {
            // Arrange
            var controller = new SettingsController(_context);

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
