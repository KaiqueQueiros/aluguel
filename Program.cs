using Aluguel.Data;

using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Adiciona os serviços ao contêiner.
builder.Services.AddControllersWithViews();

// Configuração do DbContext para PostgreSQL
builder.Services.AddDbContext<AluguelContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configuração do pipeline de requisição HTTP.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // O valor padrão do HSTS é de 30 dias. Você pode alterar isso para cenários de produção.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Motorista}/{action=Index}/{id?}");

app.Run();