using System;
using System.Linq;
using System.Threading.Tasks;
using FoodUp.Web.Data;
using FoodUp.Web.Models;
using FoodUp.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoodUp.Web.Controllers
{
  public class RecipesController : CookieController
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
      ViewBag.CurrentUser = await _userService.CurrentUser();
      return View(await _context.Recipe.ToListAsync());
    }

    // GET: Recipes/Details/5
    public async Task<IActionResult> Details(int? id)
    {
      ViewBag.CurrentUser = await _userService.CurrentUser();
      if (id == null)
      {
        return NotFound();
      }


      var recipe = await _context.Recipe
          .FirstOrDefaultAsync(m => m.Id == id);
      if (recipe == null)
      {
        return NotFound();
      }
      var creator = await _userService.FindById(recipe.CreatorId);
      ViewData["Creator"] = creator.Login;

      var reviews = await _context.Review.Where(x => x.RecipeId == recipe.Id).ToListAsync();
      ViewData["AverageRating"] = reviews.Count == 0 ? 0 : Math.Round(reviews.Average(x => x.Rating), 2);
      ViewBag.Reviews = reviews;
      return View(recipe);
    }

    // GET: Recipes/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
      var user = ViewBag.CurrentUser = await _userService.CurrentUser();
      if (user == null)
      {
        return Unauthorized("You are not authorized to access this page");
      }

      if (id == null)
      {
        return NotFound();
      }

      var recipe = await _context.Recipe.FindAsync(id);
      if (recipe.CreatorId != user.Id)
      {
        return Unauthorized("You are not authorized to access this page");
      }

      if (recipe == null)
      {
        return NotFound();
      }
      return View(recipe);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Title, Ingredients, Content")] Recipe patchedRecipe)
    {
      var user = ViewBag.CurrentUser = await _userService.CurrentUser();
      if (user == null)
      {
        return Unauthorized("You are not authorized to access this page");
      }

      var recipe = await _context.Recipe.FindAsync(id);
      if (recipe.CreatorId != user.Id)
      {
        return Unauthorized("You are not authorized to access this page");
      }

      recipe.Title = patchedRecipe.Title;
      recipe.Ingredients = patchedRecipe.Ingredients;
      recipe.Content = patchedRecipe.Content;

      if (ModelState.IsValid)
      {
        try
        {
          _context.Update(recipe);
          await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
          throw;
        }
        return RedirectToAction(nameof(Index));
      }
      return View(recipe);
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
    public async Task<IActionResult> Create([Bind("Id, Title, Ingredients, Content")] Recipe recipe)
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

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
      var recipe = await _context.Recipe
        .FirstOrDefaultAsync(m => m.Id == id);
      if (recipe == null )
      {
        return NotFound();
      }
      _context.Recipe.Remove(recipe);
      await _context.SaveChangesAsync();
      return RedirectToAction(nameof(Index));
    }
    }
}
