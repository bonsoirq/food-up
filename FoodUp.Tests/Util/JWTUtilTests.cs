using System;
using FoodUp.Web.Models;
using FoodUp.Web.Util;
using JWT.Algorithms;
using JWT.Builder;
using NUnit.Framework;

namespace FoodUp.Tests.Util
{
    public class JWTUtilTests
    {
        private User user;
        private long exp;
        private int sub;

        [SetUp]
        public void Setup()
        {
            Environment.SetEnvironmentVariable("JWT_SECRET", "secret");

            user = new User
            {
                Id = 123,
                Login = "login",
                Password = "password",
                Birthday = new DateTime()
            };

            exp = DateTimeOffset.UtcNow.AddHours(1).ToUnixTimeSeconds();
            sub = user.Id;
        }

        [Test]
        public void JWTUtilGenerateUserToken()
        {
            var token = JWTUtil.GenerateUserToken(user);
            Assert.IsTrue(token.StartsWith("eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9"));
        }

        [Test]
        public void JWTUtilDecodeUserToken()
        {
            var token = new JwtBuilder()
                .WithAlgorithm(new HMACSHA256Algorithm())
                .WithSecret(Environment.GetEnvironmentVariable("JWT_SECRET"))
                .AddClaim("exp", exp)
                .AddClaim("sub", sub)
                .Build();
            var json = JWTUtil.DecodeUserToken(token);
            Assert.IsTrue(json.Contains("\"exp\":" + exp));
            Assert.IsTrue(json.Contains("\"sub\":" + sub));
        }
    }
}
