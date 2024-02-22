using Microsoft.EntityFrameworkCore;
using AuthenticatedAPI.Models;

namespace AuthenticatedAPI. Data;

public class AppDataContext : DbContext
{
    public AppDataContext(DbContextOptions<AppDataContext> options)
    :base(options)
    {}
     public DbSet<ShoppingCart> ShoppingCarts { get; set; }
     public DbSet<Category> categories { get; set; }
     public DbSet<Product> products{ get; set;}
}