namespace ImgUpoader.Domain
{
    public class Img
    {
        public long Id { get; set; }
        public string OriginalURL { get; set; }
        public string LocalName { get; set; }
        public string FileExtension { get; set; }
        public long FileSize { get; set; }
        public DateTime DownloadDate { get; set; } = DateTime.UtcNow;
    }
}