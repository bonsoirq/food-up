using FoodUp.Web.Data;
using FoodUp.Web.Controllers;
using FoodUp.Web.Models;
using FoodUp.Web.Services;
using NUnit.Framework;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using Microsoft.AspNetCore.Http;
using System.Collections;
using FoodUp.Tests.Mocks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using MockQueryable.Moq;

namespace FoodUp.Tests
{
    public class UserServiceTests
    {
        private Mock<MockController> controller;
        private Mock<FoodUpContext> context;
        private UserService userService;
        private Mock<DbSet<User>> userMock;

        [SetUp]
        public void Setup()
        {
            context = new Mock<FoodUpContext>(new DbContextOptions<FoodUpContext>());
            controller = new Mock<MockController>(context.Object);
            userService = new UserService(controller.Object, context.Object);

            var user = new User
            {
                Id = default(int),
                Login = "login",
                Password = "password",
                Birthday = new DateTime()
            };
            var users = new List<User>();
            users.Add(user);
            userMock = users.AsQueryable().BuildMockDbSet();
            context.SetupGet(x => x.User).Returns(userMock.Object);
        }

        [Test]
        public void UserServiceDoesNotContainSession()
        {
            var task = userService.CurrentUser();
            task.Wait();
            Assert.IsNull(task.Result);
        }

        [Test]
        public void UserServiceFindUserById()
        {
            var task = userService.FindById(default(int));
            task.Wait();
            Assert.AreEqual(default(int), task.Result.Id);
        }

        [Test]
        public void UserServiceFindUserByIdDefault()
        {
            var task = userService.FindById(null);
            task.Wait();
            Assert.AreEqual(default(int), task.Result.Id);
        }

        [Test]
        public void UserServiceFindUserByLogin()
        {
            var task = userService.FindByLogin("login");
            task.Wait();
            Assert.AreEqual("login", task.Result.Login);
        }

        [Test]
        public void UserServiceUserExists()
        {
            var task = userService.UserExists("login");
            var task2 = userService.UserExists("login2");
            task.Wait();
            task2.Wait();
            Assert.IsTrue(task.Result);
            Assert.IsFalse(task2.Result);
        }
    }
}
