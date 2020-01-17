using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FoodUp.Web.Models;
using FoodUp.Web.Data;
using System.Threading.Tasks;
using FoodUp.Web.Services;

namespace FoodUp.Web.Controllers
{
  public class HomeController : CookieController
    {
    private readonly ILogger<HomeController> _logger;
    private readonly FoodUpContext _context;
    private readonly IUserService _userService;


    public HomeController(ILogger<HomeController> logger, FoodUpContext context)
    {
      _context = context;
      _logger = logger;
      _userService = new UserService(this, _context);
    }

    public async Task<IActionResult> Index()
    {
      ViewBag.CurrentUser = await _userService.CurrentUser();
      return View();
    }

    public async Task<IActionResult> Privacy()
    {
      ViewBag.CurrentUser = await _userService.CurrentUser();
      return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
    }
}
