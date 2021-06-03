using CNABParser.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CNABParser.Data.Interfaces
{
    public interface ITransacaoRepository
    {
        public Task<IEnumerable<Transacao>> GetAllTransacoesAsync();
    }
}
