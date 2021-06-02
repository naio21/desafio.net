using System;

namespace CNABParser.Data.Models
{
    public class Transacao
    {
        public int ID { get; set; }
        public TipoTransacao Tipo { get; set; }
        public DateTime DataTransacao { get; set; }
        public double Valor { get; set; }
        public string CPF { get; set; }
        public string Cartao { get; set; }
        public string Proprietario { get; set; }
        public string RazaoSocial { get; set; }
    }
}
