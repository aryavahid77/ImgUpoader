using ImgUpoader.Domain;
using System.Text;

namespace ImgUpoader.Application
{
    public class TextFileImgImporter : IImgImporter
    {
        const string UploadFolder = $"images";
        IImgRepository _imgRepo;
        IWebHostEnvironment _environment;

        public TextFileImgImporter(IImgRepository imgRepo, IWebHostEnvironment environment)
        {
            _imgRepo = imgRepo;
            _environment = environment;
        }

        public async Task<int> Import()
        {
            var urls = ReadUrlsFromFile($"{_environment.WebRootPath}/links.txt");

            var saveUrlToImgTasks = urls.Select(x => DownloadUrlToImg(x)).ToList();

            var imgs = await Task.WhenAll(saveUrlToImgTasks);

            await _imgRepo.AddRangeAsync(imgs.Where(x => x != null).ToArray());
            var dbSaveResult = await _imgRepo.SaveChangesAsync();
            return dbSaveResult;
        }

        public List<string> ReadUrlsFromFile(string txtPath)
        {
            var list = new List<string>();
            var fileStream = new FileStream(txtPath, FileMode.Open, FileAccess.Read);
            using var streamReader = new StreamReader(fileStream, Encoding.UTF8);
            string line;
            while ((line = streamReader.ReadLine()) != null)
                if (!string.IsNullOrEmpty(line) && line.Length > 10)
                    list.Add(line);

            return list;
        }

        public async Task<Img?> DownloadUrlToImg(string url)
        {
            var folder = $"{_environment.WebRootPath}/{UploadFolder}";

            using var client = new HttpClient();
            using var result = await client.GetAsync(url);
            var bytes = result.IsSuccessStatusCode ? await result.Content.ReadAsByteArrayAsync() : null;
            if (bytes == null) return null;

            var ms = new MemoryStream(bytes);
            var localFileName = $"{Guid.NewGuid()}.png";
            await using (var fileStream = new FileStream(Path.Combine(folder, localFileName), FileMode.OpenOrCreate))
            {
                await ms.CopyToAsync(fileStream);
            }

            var img = new Img()
            {
                FileSize = bytes.Length,
                OriginalURL = url,
                FileExtension = localFileName.Substring(localFileName.LastIndexOf('.')),
                LocalName = localFileName
            };
            return img;
        }

    }
}
