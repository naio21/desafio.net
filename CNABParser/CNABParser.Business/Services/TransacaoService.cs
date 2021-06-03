using CNABParser.Business.Interfaces;
using CNABParser.Data.Interfaces;
using CNABParser.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CNABParser.Business.Services
{
    public class TransacaoService : ITransacaoService
    {
        private readonly ITransacaoRepository _repo;

        public TransacaoService(ITransacaoRepository repository)
        {
            this._repo = repository;
        }

        public async Task<IEnumerable<Transacao>> GetAllAsync()
        {
            return await _repo.GetAllTransacoesAsync();
        }
    }
}
