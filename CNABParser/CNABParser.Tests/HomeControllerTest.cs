using CNABParser.Business.Services;
using CNABParser.Data.Repositories;
using CNABParser.MVC.Controllers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Moq;
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
        private readonly CNABContext _dbContext;
        private readonly string _appPath;

        public HomeControllerTest()
        {
            var optionsDbContext = new DbContextOptionsBuilder<CNABContext>().UseInMemoryDatabase(databaseName: this.GetType().FullName).Options;
            _dbContext = new CNABContext(optionsDbContext);
            _repository = new TransacaoRepository(_dbContext);
            _service = new TransacaoService(_repository);
            Mock<IWebHostEnvironment> webHostMock = new Mock<IWebHostEnvironment>();
            _appPath = Directory.GetCurrentDirectory();
            webHostMock.Setup(x => x.WebRootPath).Returns(_appPath);
            _environment = webHostMock.Object;
        }

        private IFormFile formFile(string formFilePath)
        {
            FileInfo fi = new FileInfo(formFilePath);
            string fileName = fi.Name;
            string content = File.ReadAllText(formFilePath);
            MemoryStream ms = new MemoryStream();
            StreamWriter sw = new StreamWriter(ms);
            sw.Write((content));
            sw.Flush();
            ms.Position = 0;
            FormFile ret = new FormFile(ms, 0, fi.Length, fileName, fileName)
            {
                Headers = new HeaderDictionary(),
                ContentType = "text/plain"
            };
            return ret;
        }

        [Fact(DisplayName = "Upload e Parse realizados com sucesso (arquivo padrão CNAB)")]
        public async Task UploadAndParseSuccessful()
        {
            //Arrange
            IFormFile iFormFile = formFile($"{_appPath}\\upload\\CNABSuccess.txt");
            //Act
            HomeController homeController = new HomeController(_service, _environment);
            await homeController.ArquivoForm(iFormFile);
            //Assert
            Assert.True(_dbContext.Transacoes.Any());
        }

        [Fact(DisplayName = "Upload e Parse falharam (arquivo fora do padrão CNAB)")]
        public async Task UploadAndParseFailed()
        {
            //Arrange
            IFormFile iFormFile = formFile($"{_appPath}\\upload\\CNABFail.txt");
            //Act
            HomeController homeController = new HomeController(_service, _environment);
            await homeController.ArquivoForm(iFormFile);
            //Assert
            Assert.False(_dbContext.Transacoes.Any());
        }
    }
}
