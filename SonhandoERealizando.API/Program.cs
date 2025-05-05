using SonhandoERealizando.Application.Services;
using SonhandoERealizando.Application.Services.Interfaces;
using SonhandoERealizando.Domain.Interfaces;
using SonhandoERealizando.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseCors("AllowAll");
    app.MapOpenApi();
}


app.MapGet("/", () => "API SonhandoERealizando está funcionando! Use /api/clientes para acessar os recursos.");

app.UseHttpsRedirection();
app.UseCors("AllowAll");

app.UseRouting();
app.UseAuthorization();
app.MapControllers();

Console.WriteLine("Servidor em execução. Acesse http://localhost:5001/api/clientes para testar a API.");

app.Run();
