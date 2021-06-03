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
            return await _context
                .Transacoes
                .Include(x => x.Tipo)
                .ToListAsync();
        }

        public async Task AddListTransacoesAsync(IEnumerable<Transacao> transacoes)
        {
            await _context.AddRangeAsync(transacoes);
            await _context.SaveChangesAsync();
        }
    }
}
