using ImgUpoader.Persistance;

namespace ImgUpoader.Domain
{
    public interface IImgRepository
    {
        Task<int> SaveChangesAsync();
        Task AddAsync(Img img);
        Task<Paginated<Img>> ListAsync(int pageSize, int pageNo);
    }
}
