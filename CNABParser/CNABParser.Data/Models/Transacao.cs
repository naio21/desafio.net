using System;
using System.ComponentModel.DataAnnotations;

namespace CNABParser.Data.Models
{
    public class Transacao
    {
        public int ID { get; set; }
        public int TipoID { get; set; }
        public TipoTransacao Tipo { get; set; }
        public DateTime DataRegistro { get; set; }
        [Display(Name = "Data da Transação")]
        public DateTime DataTransacao { get; set; }
        [Display(Name = "Valor")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public double Valor { get; set; }
        public string CPF { get; set; }
        [Display(Name = "Cartão")]
        public string Cartao { get; set; }
        [Display(Name = "Proprietário")]
        public string Proprietario { get; set; }
        [Display(Name = "Razão Social")]
        public string RazaoSocial { get; set; }
    }
}
