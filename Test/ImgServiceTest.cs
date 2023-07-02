using ImgUpoader.Application;
using ImgUpoader.Domain;
using ImgUpoader.Infrastructure;
using ImgUpoader.Persistance;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Moq.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    public class ImgServiceTest
    {
        ImgService importer;
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

            importer = new ImgService(userRepo.Object, host.Object);
        }

        [Test]
        public async Task Upload_File()
        {
            //Arrange
            var fileMock = new Mock<IFormFile>();
            //Setup mock file using a memory stream
            var content = "data:image/png;base64,zsdasdfasdasdasdasdasd";
            var fileName = "test.jpg";
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(content);
            writer.Flush();
            ms.Position = 0;
            fileMock.Setup(_ => _.OpenReadStream()).Returns(ms);
            fileMock.Setup(_ => _.FileName).Returns(fileName);
            fileMock.Setup(_ => _.Length).Returns(ms.Length);


            //Act
            await importer.UploadAsync(fileMock.Object);

            //Assert
            Assert.Pass();
        }
    }
}
