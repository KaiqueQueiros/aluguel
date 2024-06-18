using System.ComponentModel.DataAnnotations.Schema;

namespace Aluguel.Models;

[Table("CarroMarca")]
public class CarroMarca
{
    
    public Carro Carro { get; set; }
    public int CarroId { get; set; }
    public Marca Marca { get; set; }
    public int MarcaId { get; set; }
}