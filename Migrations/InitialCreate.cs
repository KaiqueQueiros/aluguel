using Aluguel.Data;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Aluguel.Migrations;
using Microsoft.EntityFrameworkCore.Migrations;

[DbContext(typeof(AluguelContext))]
[Migration("20240325000541_InitialCreate")]

public class InitialCreate : Migration {
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Marca",
            columns: table => new
            {
                Id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                Nome = table.Column<string>(type: "text", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Marca", x => x.Id);
            });
        
        migrationBuilder.CreateTable(
            name: "Carro",
            columns: table => new
            {
                Id = table.Column<int>(type: "INTEGER", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                Modelo = table.Column<string>(type: "TEXT", nullable: false),
                Ano = table.Column<string>(type: "TEXT", nullable: false),
                MarcaId = table.Column<int>(type: "INTEGER", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Carro", x => x.Id);
                table.ForeignKey(
                    name: "FK_Carros_Marca_MarcaId",  // Corrija de Marca para MarcaId
                    column: x => x.MarcaId,  // Corrija de Marca para MarcaId
                    principalTable: "Marca",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });
        migrationBuilder.CreateTable(
            name: "CarroMarca",
            columns: table => new
            {
                CarroId = table.Column<int>(nullable: false),
                MarcaId = table.Column<int>(nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_CarroMarcas", x => new { x.CarroId, x.MarcaId });
                table.ForeignKey(
                    name: "FK_CarroMarcas_Carros_CarroId",
                    column: x => x.CarroId,
                    principalTable: "Carro",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_CarroMarcas_Marcas_MarcaId",
                    column: x => x.MarcaId,
                    principalTable: "Marca",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_CarroMarcas_MarcaId",
            table: "CarroMarca",
            column: "MarcaId");
        migrationBuilder.CreateTable(
            name: "Motorista",
            columns: table => new
            {
                Id = table.Column<int>(type: "INTEGER", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                Nome = table.Column<string>(type: "TEXT", nullable: true),
                DataNascimento = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                Endereco = table.Column<string>(type: "TEXT", nullable: true),
                CarroId = table.Column<int>(type:"INTEGER",nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Motorista", x => x.Id);
                table.ForeignKey(
                    name: "FK_MotoristaCarro",
                    column: x => x.CarroId,
                    principalTable: "Carro",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });
       
    }
    

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Motorista");
        migrationBuilder.DropTable(
            name: "Carro");
        migrationBuilder.DropTable(
            name: "Marca");
    }
    
    
    
}