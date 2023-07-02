using ImgUpoader.Domain;
using Microsoft.EntityFrameworkCore;

namespace ImgUpoader.Infrastructure;

public interface IAppDbContext
{
    DbSet<Img> Imgs { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken=default);
}