using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FoodUp.Web.Models;
using FoodUp.Web.Data;
using static FoodUp.Web.Util.ControllerExtensions;

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

        public IActionResult Index()
        {
            return View(this.CurrentUser(_context));
        }

        public IActionResult Privacy()
        {
            return View(this.CurrentUser(_context));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
