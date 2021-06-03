using CNABParser.Data.Models;
using CNABParser.Data.Repositories;
using CNABParser.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CNABParser.Business.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace CNABParser.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITransacaoService _transacaoService;
        private readonly IWebHostEnvironment _environment;
        private string _fileFolder;


        public HomeController(ILogger<HomeController> logger, 
            ITransacaoService transacaoService, 
            IWebHostEnvironment environment)
        {
            this._logger = logger;
            this._transacaoService = transacaoService;
            this._environment = environment;
            this._fileFolder = $"{_environment.WebRootPath}\\upload\\";
        }

        public async Task<IActionResult> Index()
        {
            return View(await _transacaoService.GetAllAsync());
        }

        [HttpPost]
        [Route("Vaga/ArquivoForm/{Vaga}")]
        [Route("Vaga/ArquivoForm/{Vaga}/{Arquivo}")]
        public async Task<IActionResult> ArquivoForm(IFormFile formFile)
        {
            if (await UploadFile(formFile))
            {
                return RedirectToAction("Index");
            }
            else
            {
                return Error();
            }
        }

        private async Task<bool> UploadFile(IFormFile file)
        {
            bool ret = true;
            string nomeArquivoOriginal = String.Empty;
            string nomeArquivoNovo = String.Empty;
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random random = new Random();
            string nome = new string(Enumerable.Repeat(chars, 8).Select(s => s[random.Next(s.Length)]).ToArray()) + DateTime.Now.ToString("yyyyMMddHHmmss");
            try
            {
                nomeArquivoOriginal = file.FileName;
                string[] temp = nomeArquivoOriginal.Split('.');
                nomeArquivoNovo = $"{nome}.{temp[1].ToLower()}";
                string destino = _fileFolder += nomeArquivoNovo;
                // Copia o arquivo para o local de destino
                using (var stream = new FileStream(destino, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }
            catch
            {
                ret = false;
            }
            return ret;
        }

        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var transacao = await _context.Transacoes
        //        .FirstOrDefaultAsync(m => m.ID == id);
        //    if (transacao == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(transacao);
        //}

        //public IActionResult Create()
        //{
        //    return View();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("ID,DataTransacao,Valor,CPF,Cartao,Proprietario,RazaoSocial")] Transacao transacao)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(transacao);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(transacao);
        //}

        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var transacao = await _context.Transacoes.FindAsync(id);
        //    if (transacao == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(transacao);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("ID,DataTransacao,Valor,CPF,Cartao,Proprietario,RazaoSocial")] Transacao transacao)
        //{
        //    if (id != transacao.ID)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(transacao);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!TransacaoExists(transacao.ID))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(transacao);
        //}

        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var transacao = await _context.Transacoes
        //        .FirstOrDefaultAsync(m => m.ID == id);
        //    if (transacao == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(transacao);
        //}

        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var transacao = await _context.Transacoes.FindAsync(id);
        //    _context.Transacoes.Remove(transacao);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool TransacaoExists(int id)
        //{
        //    return _context.Transacoes.Any(e => e.ID == id);
        //}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
