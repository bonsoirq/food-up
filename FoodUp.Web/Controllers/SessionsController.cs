using System.Linq;
using System.Threading.Tasks;
using FoodUp.Web.Data;
using FoodUp.Web.Models;
using FoodUp.Web.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodUp.Web.Controllers
{
  public class SessionsController : Controller
  {
    private readonly FoodUpContext _context;

    public SessionsController(FoodUpContext context)
    {
      _context = context;
    }
    public async Task<IActionResult> Create()
    {
      if (await this.CurrentUser(_context) != null)
      {
        return Redirect("/");
      }
      return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(string login, string password)
    {
      if (await this.CurrentUser(_context) != null)
      {
        return Redirect("/");
      }
      if (!UserExists(login))
      {
        return NotFound("Invalid login or password");
      }
      var user = FindUser(login);
      if (PasswordMatches(user, password))
      {
        Response.Cookies.Append(
        "_session",
        JWTUtil.GenerateUserToken(user),
        new CookieOptions()
        {
          HttpOnly = true
        });
        return Redirect("/");
      }
      return Unauthorized();
    }

    public IActionResult Delete()
    {
      Response.Cookies.Delete("_session");
      return Redirect("/");
    }

    private bool UserExists(string login)
    {
      return FindUser(login) != null;
    }

    private User FindUser(string login)
    {
      return _context.User.Where(x => x.Login == login).FirstOrDefault();
    }

    private bool PasswordMatches(User user, string password)
    {
      var encryptedPassword = user.EncryptedPassword;
      return BCrypt.Net.BCrypt.Verify(password, encryptedPassword);
    }
  }
}
