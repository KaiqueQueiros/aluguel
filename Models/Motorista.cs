namespace Aluguel.Models
{
    using System;

    public class Motorista
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string? Endereco { get; set; }

        // Chave estrangeira para Carro
        public int CarroId { get; set; }
        public Carro Carro { get; set; }
    }
}