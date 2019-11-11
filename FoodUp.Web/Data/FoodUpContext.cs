using FoodUp.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodUp.Web.Data
{
  public class FoodUpContext : DbContext
  {
    public FoodUpContext(DbContextOptions<FoodUpContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<User>()
          .HasAlternateKey(c => c.Login)
          .HasName("AK_Login");
    }

    public DbSet<User> User { get; set; }
  }
}
