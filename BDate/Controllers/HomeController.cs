using BDate.Models;
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
        public HomeController(ILogger<HomeController> logger, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index(string Id)
        {
            // ГОВНОКОД, НО ЕСТЬ БАЗОВАЯ ЛОГИКА
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's userId
            var userName = User.FindFirstValue(ClaimTypes.Name); // will give the user's userName
            var userIsActive = _userManager.FindByIdAsync(userId).Result.IsActive;

            ViewBag.urlId = Id;
            ViewBag.userId = userId;
            ViewBag.userName = userName;
            ViewBag.IsActive = userIsActive;

            if (userIsActive == false)
            {
                // if isActive == false
                // fill Profile table
                // Change attribute and return back to Home/Index
                // ЗДЕСЬ НАДО ЗАСЕТИТЬ ID для PROFILE и Setting
                return View();
            }
            else
            {
                // else {view with users Users}
                // isActive = true
                // Give role => ActiveUser
                // return list of profiles
                return View("Privacy");
                //return RedirectToAction("Index","Users");
            }
            //return View();
        }

        //When SUMBIT button is clicked
        [HttpPost]
        public async Task<IActionResult> IndexAsync()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _userManager.FindByIdAsync(userId);
            user.Result.IsActive = true;
            
            await _userManager.UpdateAsync(await user);
            //var result = await _userManager.UpdateAsync(user);
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
