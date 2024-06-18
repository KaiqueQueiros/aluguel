using Microsoft.EntityFrameworkCore;

namespace Aluguel.Data;

public class Startup
{
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }
    public void ConfigureServices(IServiceCollection services)
    {
        // Configuração do DbContext para PostgreSQL
        services.AddDbContext<AluguelContext>(options =>
        {
            options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"));
        });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        // Configuração do pipeline de middleware
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            // Configuração para produção
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        // Configurações adicionais...

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
        });
    }

}