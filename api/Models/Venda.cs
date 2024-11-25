using System.ComponentModel.DataAnnotations;

namespace CamposDealerAPI.Models
{
    public class Venda
    {
        [Key]
        public int idVenda { get; set; }

        public int IdCliente { get; set; } // continua obrigatorio
        public Cliente? Cliente { get; set; } // torna o Clinte opcional

        public int idProduto { get; set; } // continua obrigatorio
        public Produto? Produto { get; set; } // torna o Produto opcional

        public int qtdVenda { get; set; }
        public decimal vlrUnitarioVenda { get; set; }
        public DateTime dthVenda { get; set; }

        public decimal ValorTotal => vlrUnitarioVenda * qtdVenda;
    }
}
