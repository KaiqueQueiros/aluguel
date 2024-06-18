using Aluguel.Models;
using Microsoft.EntityFrameworkCore;

namespace Aluguel.Data
{
    public class AluguelContext : DbContext
    {
        public AluguelContext(DbContextOptions<AluguelContext> options)
            : base(options)
        {
        }

        public DbSet<Motorista> Motorista { get; set; }
        public DbSet<Carro> Carro { get; set; }
        public DbSet<Marca> Marca { get; set; }
        public DbSet<CarroMarca> CarroMarca { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Definindo a chave composta para CarroMarca
            modelBuilder.Entity<CarroMarca>()
                .HasKey(cm => new { cm.CarroId, cm.MarcaId });

            // Configurando o relacionamento entre Carro e Marca
            modelBuilder.Entity<Carro>()
                .HasOne(c => c.Marca)
                .WithMany(m => m.Carros)
                .HasForeignKey(c => c.MarcaId);  // Usando MarcaId como chave estrangeira

            // Configurando o relacionamento entre CarroMarca e Marca
            modelBuilder.Entity<CarroMarca>()
                .HasOne(cm => cm.Marca)
                .WithMany(m => m.CarroMarcas)
                .HasForeignKey(cm => cm.MarcaId);

            // Configurando o relacionamento entre Motorista e Carro
            modelBuilder.Entity<Motorista>()
                .HasOne(m => m.Carro)
                .WithMany()
                .HasForeignKey(m => m.CarroId);
    
            modelBuilder.Entity<Motorista>()
                .Property(m => m.DataNascimento)
                .HasColumnType("timestamp with time zone");
        }

    }
}