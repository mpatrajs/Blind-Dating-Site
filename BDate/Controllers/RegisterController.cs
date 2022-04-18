using Microsoft.AspNetCore.Mvc;

namespace BDate.Controllers
{
    public class RegisterController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public string Index(string name, string surname)
        {
            return name;
        }
    }
}
