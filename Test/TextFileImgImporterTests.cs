using ImgUpoader.Application;
using ImgUpoader.Domain;
using ImgUpoader.Infrastructure;
using ImgUpoader.Persistance;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MockQueryable.Moq;
using Moq;
using Moq.EntityFrameworkCore;
using NUnit.Framework;

namespace Test
{
    public class TextFileImgImporterTests
    {
        TextFileImgImporter importer;
        Mock<IAppDbContext> db;

        [SetUp]
        public void Setup()
        {
            db = new Mock<IAppDbContext>();

            db.Setup(x => x.Imgs)
                   .ReturnsDbSet(new List<Img>()
                    {
                    });


            var userRepo = new Mock<ImgRepository>(db.Object);
            var host = new Mock<IWebHostEnvironment>();
            host.Setup(x => x.WebRootPath).Returns("D:\\_temp\\ImgUpoader\\ImgUpoader\\wwwroot");

            importer = new TextFileImgImporter(userRepo.Object, host.Object);
        }

        [Test]
        public void Read_Urls_From_File()
        {
            var host = new Mock<IWebHostEnvironment>();
            host.Setup(x=>x.WebRootPath).Returns("D:\\_temp\\ImgUpoader\\ImgUpoader\\wwwroot");

            var urls =  importer.ReadUrlsFromFile($"{host.Object.WebRootPath}/links.txt");

            Assert.That(urls.Count, Is.EqualTo(8));
        }

        [Test]
        public async Task Download_Img1()
        {
            var img =await importer.DownloadUrlToImg($"https://tourism.780.ir/tourism/_next/image?url=%2Ftourism%2F_next%2Fstatic%2Fmedia%2Fair1.8d270ca0.jpg&w=3840&q=75");

            Assert.That(img.FileSize, Is.EqualTo(10026));
        }

        [Test]
        public async Task Download_Img2()
        {
            var img = await importer.DownloadUrlToImg($"https://tourism.780.ir/tourism/_next/image?url=%2Ftourism%2F_next%2Fstatic%2Fmedia%2Fflight_reserve_hotel.fe761bc9.jpg&w=640&q=75");

            Assert.That(img.FileSize, Is.EqualTo(14751));
        }

        [Test]
        public async Task Import_From_File()
        {
            var importCount =await importer.Import();

            Assert.That(importCount, Is.GreaterThan(-1));
        }

   
    }
}