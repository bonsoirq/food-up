using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FoodUp.Web.Models;
using FoodUp.Web.Data;
using FoodUp.Web.Util;
using System.Threading.Tasks;

namespace FoodUp.Web.Controllers
{
  public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly FoodUpContext _context;
    

        public HomeController(ILogger<HomeController> logger, FoodUpContext context)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.CurrentUser = await this.CurrentUser(_context);
            return View();
        }

        public async Task<IActionResult> Privacy()
        {
            ViewBag.CurrentUser = await this.CurrentUser(_context);
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
