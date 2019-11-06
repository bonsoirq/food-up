using FoodUp.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodUp.Web.Data
{
    public class FoodUpContext : DbContext
    {
        public FoodUpContext(DbContextOptions<FoodUpContext> options) : base(options)
        {
        }

        public DbSet<User> User { get; set; }
    }
}
