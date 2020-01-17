using System.Linq;
using System.Threading.Tasks;
using FoodUp.Web.Data;
using FoodUp.Web.Models;
using FoodUp.Web.Services;
using FoodUp.Web.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodUp.Web.Controllers
{
  public class SessionsController : CookieController
    {
    private readonly FoodUpContext _context;
    private IUserService _userService;

    public SessionsController(FoodUpContext context)
    {
      _context = context;
      _userService = new UserService(this, _context);
    }
    public async Task<IActionResult> Create()
    {
      if (await _userService.CurrentUser() != null)
      {
        return Redirect("/");
      }
      return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(string login, string password)
    {
      if (await _userService.CurrentUser() != null)
      {
        return Redirect("/");
      }
      if (!await _userService.UserExists(login))
      {
        return NotFound("Invalid login or password");
      }
      var user = await _userService.FindByLogin(login);
      if (PasswordMatches(user, password))
      {
        AppendSessionCookie(user);
        return Redirect("/");
      }
      return Unauthorized("Invalid login or password");
    }

    public IActionResult Delete()
    {
      RemoveSessionCookie();
      return Redirect("/");
    }

    private bool PasswordMatches(User user, string password)
    {
      var encryptedPassword = user.EncryptedPassword;
      return BCrypt.Net.BCrypt.Verify(password, encryptedPassword);
    }

    private void AppendSessionCookie(User user)
    {
      Response.Cookies.Append(
        "_session",
        JWTUtil.GenerateUserToken(user),
        new CookieOptions()
        {
          HttpOnly = true
        });
    }

    private void RemoveSessionCookie()
    {
      Response.Cookies.Delete("_session");
    }
  }
}
