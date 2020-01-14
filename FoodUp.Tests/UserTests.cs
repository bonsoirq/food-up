    using FoodUp.Web.Models;
using NUnit.Framework;

namespace FoodUp.Tests
{
    public class UserTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void EncryptsPasswordAndRemovesPlainPasswordProperty()
        {
            var user = new User()
            {
                Password = "password"
            };

            user.EncryptPassword();
            Assert.IsNull(user.Password);
            Assert.IsNotNull(user.EncryptedPassword);
        }
    }
}