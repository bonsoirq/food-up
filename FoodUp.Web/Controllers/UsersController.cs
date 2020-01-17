using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FoodUp.Web.Data;
using FoodUp.Web.Models;
using FoodUp.Web.Util;
using FoodUp.Web.Services;

namespace FoodUp.Web.Controllers
{
  public class UsersController : CookieController
    {
    private readonly FoodUpContext _context;
    private readonly IUserService _userService;

    public UsersController(FoodUpContext context)
    {
      _context = context;
      _userService = new UserService(this, _context);
    }

    // GET: Users
    public async Task<IActionResult> Index()
    {
      return View(await _context.User.ToListAsync());
    }

    // GET: Users/Details/5
    public async Task<IActionResult> Details(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var user = await _context.User
          .FirstOrDefaultAsync(m => m.Id == id);
      if (user == null)
      {
        return NotFound();
      }

      return View(user);
    }

    // GET: Users/Create
    public IActionResult Create()
    {
      return View();
    }

    // POST: Users/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Login,Password,Birthday")] User user)
    {
      if (await _userService.UserExists(user.Login))
      {
        return UnprocessableEntity("This login has been taken");
      }
      if (ModelState.IsValid)
      {
        user.EncryptPassword();
        _context.Add(user);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(HomeController.Index));
      }
      return View(user);
    }

    // GET: Users/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var currentUser = await _userService.CurrentUser();
      if (currentUser == null || currentUser.Id != id)
      {
        return Unauthorized();
      }

      var user = await _context.User.FindAsync(id);
      if (user == null)
      {
        return NotFound();
      }
      return View(user);
    }

    // POST: Users/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Birthday")] User user)
    {
      var currentUser = await _userService.CurrentUser();
      if (currentUser == null || currentUser.Id != id)
      {
        return Unauthorized();
      }
      if (id != user.Id)
      {
        return NotFound();
      }

      if (ModelState.IsValid)
      {
        try
        {
          _context.Update(user);
          await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
          if (!await _userService.UserExists(user.Id))
          {
            return NotFound();
          }
          else
          {
            throw;
          }
        }
        return RedirectToAction(nameof(Index));
      }
      return View(user);
    }

    // GET: Users/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var user = await _userService.FindById(id);
      if (user == null)
      {
        return NotFound();
      }

      return View(user);
    }

    // POST: Users/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
      var user = await _userService.FindById(id);
      _context.User.Remove(user);
      await _context.SaveChangesAsync();
      return RedirectToAction(nameof(Index));
    }
  }
}
