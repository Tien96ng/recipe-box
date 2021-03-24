using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Recipe.Models
{
  public class RecipeContext : IdentityDbContext<ApplicationUser>
  {
    public virtual DbSet<Food> Food { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<FoodUser> FoodUsers { get; set; }

    public RecipeContext(DbContextOptions options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseLazyLoadingProxies();
    }
  }
}