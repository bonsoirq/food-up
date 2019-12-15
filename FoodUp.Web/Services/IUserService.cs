using System;
using System.Threading.Tasks;
using FoodUp.Web.Models;
namespace FoodUp.Web.Services
{
    public interface IUserService
    {
      Task<User> CurrentUser();
      Task<User> FindById(int? id);
      Task<User> FindByLogin(string login);
      Task<bool> UserExists(int id);
      Task<bool> UserExists(string login);
    }
}
