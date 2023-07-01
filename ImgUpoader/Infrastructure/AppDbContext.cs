using ImgUpoader.Domain;
using Microsoft.EntityFrameworkCore;

namespace ImgUpoader.Infrastructure;

public class AppDbContext : DbContext
{
    public DbSet<Img> Imgs { get; set; }


    public AppDbContext(DbContextOptions contextOptions) : base(contextOptions)
    {
    }
}
