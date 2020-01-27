using System.Linq;
using System.Threading.Tasks;
using FoodUp.Web.Data;
using FoodUp.Web.Models;
using FoodUp.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoodUp.Web.Controllers
{
  public class ReviewsController : Controller
    {

    private readonly FoodUpContext _context;
    private readonly IUserService _userService;

    public ReviewsController(FoodUpContext context)
    {
      _context = context;
      _userService = new UserService(this, _context);
    }

    // GET: Recipes/5/Reviews
    public async Task<IActionResult> Index(int recipeId)
    {
      ViewBag.CurrentUser = await _userService.CurrentUser();
      return Json(await _context.Review.Where(x => x.Id == recipeId).ToListAsync());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(int recipeId, [Bind("Id,Rating,Comment")] Review review)
    {
      var user = ViewBag.CurrentUser = await _userService.CurrentUser();
      if (user == null)
      {
        return Unauthorized("You are not authorized to access this page");
      }
      review.ReviewerId = user.Id;
      review.RecipeId = recipeId;
      if (ModelState.IsValid)
      {
        _context.Add(review);
        await _context.SaveChangesAsync();
        return Json(review);
      }
      return UnprocessableEntity();
    }
    }
}
