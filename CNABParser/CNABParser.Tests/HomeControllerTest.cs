using CNABParser.Business.Services;
using CNABParser.Data.Models;
using CNABParser.Data.Repositories;
using CNABParser.MVC.Controllers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace CNABParser.Tests
{
    public class HomeControllerTest
    {
        private readonly TransacaoRepository _repository;
        private readonly TransacaoService _service;
        private readonly IWebHostEnvironment _environment;
        private readonly string _appPath;

        public HomeControllerTest()
        {
            var optionsDbContext = new DbContextOptionsBuilder<CNABContext>().UseInMemoryDatabase(databaseName: this.GetType().FullName).Options;
            CNABContext _dbContext = new CNABContext(optionsDbContext);
            //CNABContext _dbContext = new CNABContext(new DbContextOptions<CNABContext>());
            _repository = new TransacaoRepository(_dbContext);
            _service = new TransacaoService(_repository);
            Mock<IWebHostEnvironment> webHostMock = new Mock<IWebHostEnvironment>();
            _appPath = Directory.GetCurrentDirectory();
            webHostMock.Setup(x => x.WebRootPath).Returns(_appPath);
            _environment = webHostMock.Object;
        }

        private Mock<IFormFile> mockFormFile(string formFilePath)
        {
            Mock<IFormFile> mockFormFile = new Mock<IFormFile>();
            FileInfo fi = new FileInfo(formFilePath);
            string fileName = fi.Name;
            string content = File.ReadAllText(formFilePath);
            MemoryStream ms = new MemoryStream();
            StreamWriter sw = new StreamWriter(ms);
            sw.Write((content));
            sw.Flush();
            ms.Position = 0;
            mockFormFile.Setup(_ => _.OpenReadStream()).Returns(ms);
            mockFormFile.Setup(_ => _.FileName).Returns(fileName);
            mockFormFile.Setup(_ => _.Length).Returns(fi.Length);
            mockFormFile.Setup(_ => _.ContentDisposition).Returns($"form-data; name=\"arquivo\"; filename=\"{fileName}\"");
            mockFormFile.Setup(_ => _.ContentType).Returns($"text/plain");
            return mockFormFile;
        }

        [Fact(DisplayName = "Upload e Parse realizados com sucesso (arquivo padrão CNAB)")]
        public async Task UploadAndParseSuccessful()
        {
            //Arrange
            Mock<IFormFile> formFile = mockFormFile($"{_appPath}\\upload\\CNABSuccess.txt");
            //Act
            HomeController homeController = new HomeController(_service, _environment);
            await homeController.ArquivoForm(formFile.Object);
            //Assert
            IEnumerable<Transacao> transacoes = await _repository.GetAllTransacoesAsync();
            Assert.True(transacoes.Any());
        }
    }
}
