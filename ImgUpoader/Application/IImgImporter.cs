namespace ImgUpoader.Application
{
    public interface IImgImporter
    {
        Task<int> Import();
    }
}