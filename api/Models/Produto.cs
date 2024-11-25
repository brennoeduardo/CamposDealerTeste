using System.ComponentModel.DataAnnotations;

namespace CamposDealerAPI.Models
{
    public class Produto
    {
        [Key]
        public int idProduto { get; set; }

        [Required]
        public string dscProduto { get; set; }

        [Required]
        public decimal vlrUnitario { get; set; }
    }
}
