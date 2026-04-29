using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace PadariaWeb.Models
{
    public class Cliente
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Phone]
        public string Telefone { get; set; }

        [Required]
        public string Endereco { get; set; }

        public ICollection<Pedido> Pedidos { get; set; }
    }
}
