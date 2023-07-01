namespace ImgUpoader.Application;

public static class FormFileExt
{
    public static async Task<(bool IsSuccess, string localFileName)> UploadFileAsync(this IFormFile formFile, string folder)
    {
      
        if (!Directory.Exists(folder))
        {
            Directory.CreateDirectory(folder);
        }


        if (formFile == null || formFile.Length <= 0) return (false, "");


        var fileExt = formFile.FileName.Substring(formFile.FileName.LastIndexOf('.'));

        var localFileName = $"{Guid.NewGuid()}.{fileExt}";

        await using (var stream = new FileStream(Path.Combine(folder, localFileName), FileMode.OpenOrCreate))
        {
            await formFile.CopyToAsync(stream);
        }

        return (true, localFileName);
    }
}


