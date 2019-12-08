using System;
using FoodUp.Web.Models;
using JWT.Algorithms;
using JWT.Builder;

namespace FoodUp.Web.Util
{
  public class JWTUtil
  {
    public static string GenerateUserToken(User user)
    {
        var token = new JwtBuilder()
            .WithAlgorithm(new HMACSHA256Algorithm())
            .WithSecret(Environment.GetEnvironmentVariable("JWT_SECRET"))
            .AddClaim("exp", DateTimeOffset.UtcNow.AddHours(1).ToUnixTimeSeconds())
            .AddClaim("sub", user.Id)
            .Build();
        return token;
    }

    public static string DecodeUserToken(string token)
    {
      var json = new JwtBuilder()
            .WithSecret(Environment.GetEnvironmentVariable("JWT_SECRET"))
            .MustVerifySignature()
            .Decode(token);
      return json;
    }
  }
}
