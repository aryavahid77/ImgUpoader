using ImgUpoader.Domain;
using ImgUpoader.Infrastructure;
using ImgUpoader.Persistance;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace ImgUpoader.Application;

public class ImgService
{
    const string UploadFolder = $"images/user-content";
    IImgRepository _imgRepo;
    IWebHostEnvironment _environment;

    public ImgService(IImgRepository imgRepo, IWebHostEnvironment environment)
    {
        _imgRepo = imgRepo;
        _environment = environment;
    }


    public async Task UploadAsync(IFormFile imgFile)
    {
        var folder = $"{_environment.WebRootPath}/{UploadFolder}";
        (bool success, string localFileName) = await imgFile.UploadFileAsync(folder);
        if (!success)
            throw new Exception();

        var img = new Img()
        {
            FileSize = imgFile.Length,
            OriginalURL = imgFile.FileName,
            FileExtension = imgFile.FileName.Substring(imgFile.FileName.LastIndexOf('.')),
            LocalName = localFileName
        };


        await _imgRepo.AddAsync(img);
        var dbSaveResult = await _imgRepo.SaveChangesAsync();

    }

    public async Task<Paginated<ImgDto>> ListAsync(int pageSize, int pageNo)
    {
        var paginatedImgs = await _imgRepo.ListAsync(pageSize, pageNo);
        return new Paginated<ImgDto>(
               paginatedImgs.TotalCount,
               paginatedImgs.Items.Select(x => new ImgDto()
               {
                   DownloadDate = x.DownloadDate,
                   FileExtension = x.FileExtension,
                   FileSize = x.FileSize,
                   OriginalURL = x.OriginalURL,
                   LocalName = x.LocalName,
               }).ToList());
    }
}


