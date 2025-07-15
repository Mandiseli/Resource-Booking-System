using Microsoft.AspNetCore.Mvc;
using ResourceBookingSystem.Models;

namespace ResourceBookingSystem.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
