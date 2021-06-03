using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CNABParser.Data.Interfaces;
using CNABParser.Data.Models;

namespace CNABParser.Business.Interfaces
{
    public interface ITransacaoService
    {
        public Task<IEnumerable<Transacao>> GetAllAsync();
        public Task ProcessaArquivoAsync(string filePath);
    }
}
