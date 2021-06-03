using CNABParser.Business.Interfaces;
using CNABParser.MVC.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CNABParser.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITransacaoService _transacaoService;
        private readonly IWebHostEnvironment _environment;
        private string _fileFolder;


        public HomeController(ITransacaoService transacaoService, 
            IWebHostEnvironment environment)
        {
            this._transacaoService = transacaoService;
            this._environment = environment;
            this._fileFolder = $"{_environment.WebRootPath}\\upload\\";
        }

        public async Task<IActionResult> Index()
        {
            return View(await _transacaoService.GetAllAsync());
        }

        [HttpPost]
        public async Task<IActionResult> ArquivoForm(IFormCollection collection)
        {
            try
            {
                IFormFile arquivoCNAB = collection.Files[0];
                if(arquivoCNAB.ContentType != "text/plain")
                {
                    return StatusCode(StatusCodes.Status415UnsupportedMediaType, "Arquivo inválido");
                }
                await UploadFile(arquivoCNAB);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        private async Task UploadFile(IFormFile file)
        {
            string nomeArquivoNovo = String.Empty;
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random random = new Random();
            string nome = new string(Enumerable.Repeat(chars, 8).Select(s => s[random.Next(s.Length)]).ToArray()) + DateTime.Now.ToString("yyyyMMddHHmmss");
            string[] temp = file.FileName.Split('.');
            nomeArquivoNovo = $"{nome}.{temp[1].ToLower()}";
            string destino = _fileFolder += nomeArquivoNovo;
            // Copia o arquivo para o local de destino
            using (var stream = new FileStream(destino, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            await _transacaoService.ProcessaArquivoAsync(destino);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
