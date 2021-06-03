using CNABParser.Data.Interfaces;
using CNABParser.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CNABParser.Data.Repositories
{
    public class TransacaoRepository : ITransacaoRepository
    {
        private readonly CNABContext _context;

        public TransacaoRepository(CNABContext context)
        {
            this._context = context;
        }

        public async Task<IEnumerable<Transacao>> GetAllTransacoesAsync()
        {
            return await _context.Transacoes.ToListAsync();
        }
    }
}
