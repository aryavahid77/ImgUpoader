using ImgUpoader.Domain;
using ImgUpoader.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace ImgUpoader.Persistance
{
    public class ImgRepository: IImgRepository
    {
        IAppDbContext _db;

        public ImgRepository(IAppDbContext db)
        {
            _db = db;
        }


        public async Task<Paginated<Img>> ListAsync(int pageSize,int pageNo)
        {
            return new Paginated<Img>(
                await _db.Imgs.CountAsync(),
                await _db.Imgs.Skip(pageNo*pageNo).Take(pageSize).ToListAsync());
        }

        public async Task AddAsync(Img img)
        {
            await _db.Imgs.AddAsync(img);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _db.SaveChangesAsync();
        }

        public async Task AddRangeAsync(params Img[] imgs)
        {
            await _db.Imgs.AddRangeAsync(imgs);
        }
    }
}
