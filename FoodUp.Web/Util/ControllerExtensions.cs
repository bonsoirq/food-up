using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using FoodUp.Web.Data;
using FoodUp.Web.Models;
using JWT;
using Microsoft.AspNetCore.Mvc;

namespace FoodUp.Web.Util
{
  public static class ControllerExtensions
  {
    public async static Task<User> CurrentUser(this Controller controller, FoodUpContext context)
    {
      if (!controller.Request.Cookies.ContainsKey("_session"))
      {
        return null;
      }
      try
      {
        string sessionCookie = null;
        controller.Request.Cookies.TryGetValue("_session", out sessionCookie);
        var json = JWTUtil.DecodeUserToken(sessionCookie);
        var dictionary = JsonSerializer.Deserialize<Dictionary<string, int>>(json);
        var user = await context.User.FindAsync(dictionary["sub"]);
        return user;
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
  }
}
