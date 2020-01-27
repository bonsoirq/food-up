using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FoodUp.Web.Models;
using FoodUp.Web.Data;
using System.Threading.Tasks;
using FoodUp.Web.Services;
using FoodUp.Web.Controllers;

namespace FoodUp.Tests.Mocks
{
    public class MockController : Controller
    {
        private readonly FoodUpContext _context;
        private readonly IUserService _userService;

        public MockController(FoodUpContext context)
        {
            _context = context;
            _userService = new UserService(this, _context);
        }
    }
}
