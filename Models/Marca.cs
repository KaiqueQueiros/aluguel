using System.ComponentModel.DataAnnotations.Schema;

namespace Aluguel.Models;
[Table("Marca")]
public class Marca
{
    [Column("Id")]
    public int Id { get; set; }
    [Column("Nome")]
    public string Nome { get; set; }
    public ICollection<Carro> Carros { get; set; } 
    public ICollection<CarroMarca> CarroMarcas { get; set; } 


}