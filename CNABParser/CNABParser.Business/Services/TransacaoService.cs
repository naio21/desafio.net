using CNABParser.Business.Interfaces;
using CNABParser.Data.Interfaces;
using CNABParser.Data.Models;
using System;
using System.Collections.Generic;
using System.IO;
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

        public async Task ProcessaArquivoAsync(string filePath)
        {
            List<Transacao> transacoes = new List<Transacao>();
            string[] registros = File.ReadAllLines(filePath);
            foreach(string registro in registros)
            {
                transacoes.Add(parseTransacao(registro));
            }
            await _repo.AddListTransacoesAsync(transacoes);
            File.Delete(filePath);
        }

        private Transacao parseTransacao(string registro)
        {
            Transacao ret = new();
            ret.TipoID = Convert.ToInt32(registro.Substring(0, 1));
            ret.DataRegistro = DateTime.Now;
            int year = Convert.ToInt32(registro.Substring(1, 4));
            int month = Convert.ToInt32(registro.Substring(5, 2));
            int day = Convert.ToInt32(registro.Substring(7, 2));
            ret.Valor = Convert.ToDouble(registro.Substring(9, 9)) / 100;
            ret.CPF = registro.Substring(19, 11);
            ret.Cartao = registro.Substring(30, 12);
            int hour = Convert.ToInt32(registro.Substring(42, 2));
            int minute = Convert.ToInt32(registro.Substring(44, 2));
            int second = Convert.ToInt32(registro.Substring(46, 2));
            ret.DataTransacao = new DateTime(year, month, day, hour, minute, second);
            ret.Proprietario = registro.Substring(48, 14).Trim();
            ret.RazaoSocial = registro.Substring(62).Trim();
            return ret;
        }
    }
}
