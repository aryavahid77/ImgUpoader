using ImgUpoader.Persistance;

namespace ImgUpoader.Application
{
    public interface IImgService
    {
        Task<Paginated<ImgDto>> ListAsync(int pageSize, int pageNo);
        Task<int> UploadAsync(IFormFile imgFile);
    }
}