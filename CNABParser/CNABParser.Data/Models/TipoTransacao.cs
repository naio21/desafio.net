using System.ComponentModel.DataAnnotations;

namespace CNABParser.Data.Models
{
    public class TipoTransacao
    {
        public int ID { get; set; }
        public int Tipo { get; set; }
        [Display(Name = "Tipo")]
        public string Descricao { get; set; }
        public int Natureza { get; set; }
    }
}
