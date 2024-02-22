using Microsoft.EntityFrameworkCore;

namespace AuthenticatedAPI. Data;

public class AppDataContext : DbContext
{
    public AppDataContext(DbContextOptions<AppDataContext> options)
    :base(options)
    {}
}