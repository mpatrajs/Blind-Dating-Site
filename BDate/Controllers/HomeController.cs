using BDate.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BDate.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        public HomeController(
            ILogger<HomeController> logger, 
            UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _userManager = userManager;
        }

        [Authorize(Roles = "InActiveUser, ActiveUser, Admin")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's userId

            await _userManager.FindByIdAsync(userId);
            ApplicationUser applicationUser = await _userManager.FindByIdAsync(userId);
            if (await _userManager.IsInRoleAsync(applicationUser, "ActiveUser"))
            {
                return RedirectToAction("Index", "Profiles", new { area = "" });
            }
            else if (await _userManager.IsInRoleAsync(applicationUser, "InActiveUser"))
            {
                return RedirectToAction("Create", "Profiles", new { id = userId });
            }
            else if (await _userManager.IsInRoleAsync(applicationUser, "Admin"))
            {
                return RedirectToAction("Create", "Roles", new { area = "" });
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> IndexAsync()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _userManager.FindByIdAsync(userId);
            
            await _userManager.UpdateAsync(await user);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
