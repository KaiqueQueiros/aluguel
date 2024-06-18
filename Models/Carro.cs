using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aluguel.Models
{
    [Table("Carro")]
    public class Carro
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Required]
        [Column("Modelo")]
        public string Modelo { get; set; }

        [Required]
        [Column("Ano")]
        public string Ano { get; set; }

     
        public int MarcaId { get; set; } // Chave estrangeira
        public virtual Marca Marca { get; set; } 
    }
}