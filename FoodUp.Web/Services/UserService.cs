using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using FoodUp.Web.Controllers;
using FoodUp.Web.Data;
using FoodUp.Web.Models;
using FoodUp.Web.Util;
using JWT;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoodUp.Web.Services
{
  public class UserService : IUserService
  {
    private FoodUpContext _context;
    private CookieController _controller;

    private User _user;
    public UserService(CookieController controller, FoodUpContext context)
    {
      _controller = controller;
      _context = context;
    }

    public async Task<User> CurrentUser()
    {
      if (_user != null)
      {
        return _user;
      }
      if (!_controller.ContainsCookie("_session"))
      {
        return null;
      }
      try
      {
        var cookie = ExtractSessionCookie();
        int userId = ExtractUserId(cookie);
        _user = await FindById(userId);
        return _user;
      }
      catch (TokenExpiredException)
      {
        return null;
      }
      catch (SignatureVerificationException)
      {
        return null;
      }
    }

    public async Task<User> FindById(int id)
    {
      return await _context.User.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<User> FindById(int? id)
    {
      return await FindById(id ?? default(int));
    }

    public async Task<User> FindByLogin(string login)
    {
      return await _context.User.FirstOrDefaultAsync(x => x.Login == login);
    }

    public async Task<bool> UserExists(string login)
    {
      var user = await FindByLogin(login);
      return user != null;
    }

    public async Task<bool> UserExists(int id)
    {
      var user = await FindById(id);
      return user != null;
    }

    private string ExtractSessionCookie()
    {
        string sessionCookie = null;
        _controller.Request.Cookies.TryGetValue("_session", out sessionCookie);
        return sessionCookie;
    }

    private int ExtractUserId(string cookie)
    {
      var json = JWTUtil.DecodeUserToken(cookie);
      var dictionary = JsonSerializer.Deserialize<Dictionary<string, int>>(json);
      return dictionary["sub"];
    }
  }
}
