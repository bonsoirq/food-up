using System.Threading.Tasks;
using FoodUp.Web.Data;
using FoodUp.Web.Models;
using FoodUp.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoodUp.Web.Controllers
{
  public class RecipesController : Controller
  {

    private readonly FoodUpContext _context;
    private readonly IUserService _userService;

    public RecipesController(FoodUpContext context)
    {
      _context = context;
      _userService = new UserService(this, _context);
    }

    // GET: Recipes
    public async Task<IActionResult> Index()
    {
      return View(await _context.Recipe.ToListAsync());
    }

    // GET: Recipes/Create
    public async Task<IActionResult> Create()
    {
      var user = ViewBag.CurrentUser = await _userService.CurrentUser();
      if (user == null)
      {
        return Unauthorized("You are not authorized to access this page");
      }
      return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Title,Content")] Recipe recipe)
    {
      var user = ViewBag.CurrentUser = await _userService.CurrentUser();
      if (user == null)
      {
        return Unauthorized("You are not authorized to access this page");
      }
      recipe.CreatorId = user.Id;
      if (ModelState.IsValid)
      {
        _context.Add(recipe);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(RecipesController.Index));
      }
      return View(recipe);
    }
  }
}
