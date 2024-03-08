using API.Enties;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace API;

public class DataContext : DbContext
{
  public DataContext(DbContextOptions options) : base(options)
  {

  }

  public DbSet<AppUser> Users { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);


  }
}
