using Agenda.Application.Services;
using Agenda.Domain.Ceps;
using Agenda.Domain.Contatos;
using Agenda.Infrastructure.DataAccess;
using Agenda.Infrastructure.Repositories;
using Agenda.Infrastructure.RestResources;
using Serilog;

namespace Agenda.WebApp;

static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();

        builder.Host.UseSerilog((context, configuration) =>
            configuration.ReadFrom.Configuration(context.Configuration));

        builder.Services.AddSingleton<ISqlConnectionFactory, SqlConnectionFactory>(serviceProvider =>
        {
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();

            var connectionString = configuration.GetConnectionString("DefaultConnection") ??
                                   throw new InvalidOperationException("The connection string is null");

            return new SqlConnectionFactory(connectionString);
        });

        builder.Services.AddSingleton<ICepRepository, CepRepository>();
        builder.Services.AddSingleton<IContatoRepository, ContatoRepository>();

        builder.Services.AddSingleton<IContatoService, ContatoService>();

        builder.Services.AddHttpClient<ICepExternalService, CepExternalService>(client =>
        {
            client.BaseAddress = new Uri(builder.Configuration["CepBaseUrl"]!);
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}