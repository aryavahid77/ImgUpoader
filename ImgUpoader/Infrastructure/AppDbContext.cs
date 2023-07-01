using ImgUpoader.Domain;
using Microsoft.EntityFrameworkCore;

namespace ImgUpoader.Infrastructure;

public class AppDbContext : DbContext, IAppDbContext
{
    public DbSet<Img> Imgs { get; set; }


    public AppDbContext(DbContextOptions contextOptions) : base(contextOptions)
    {
    }
}

public interface IAppDbContext
{
    DbSet<Img> Imgs { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken=default);
}