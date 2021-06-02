using CNABParser.Data.Models;
using System.Linq;

namespace CNABParser.Data.Repositories
{
    public static class DbInitializer
    {
        public static void Initialize(CNABContext context)
        {
            context.Database.EnsureCreated();
            if (context.Tipos.Any())
            {
                //DB criado e tabela de Tipos de Transacao foi preenchida
                return;
            }
            TipoTransacao[] tipos = new TipoTransacao[]
            {
                new TipoTransacao{ Tipo = 1, Descricao = "Débito", Natureza = 1 },
                new TipoTransacao{ Tipo = 2, Descricao = "Boleto", Natureza = -1 },
                new TipoTransacao{ Tipo = 3, Descricao = "Financiamento", Natureza = -1 },
                new TipoTransacao{ Tipo = 4, Descricao = "Crédito", Natureza = 1 },
                new TipoTransacao{ Tipo = 5, Descricao = "Recebimento Empréstimo", Natureza = 1 },
                new TipoTransacao{ Tipo = 6, Descricao = "Vendas", Natureza = 1 },
                new TipoTransacao{ Tipo = 7, Descricao = "Recebimento TED", Natureza = 1 },
                new TipoTransacao{ Tipo = 8, Descricao = "Recebimento DOC", Natureza = 1 },
                new TipoTransacao{ Tipo = 9, Descricao = "Aluguel", Natureza = -1 }
            };
            foreach (TipoTransacao t in tipos)
            {
                context.Tipos.Add(t);
            }
            context.SaveChanges();
        }
    }
}