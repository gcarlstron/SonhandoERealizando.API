using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using SonhandoERealizando.API.Handlers;
using SonhandoERealizando.Application.Services;
using SonhandoERealizando.Application.Services.Interfaces;
using SonhandoERealizando.Domain.Interfaces;
using SonhandoERealizando.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IServiceRepository, ServiceRepository>();
builder.Services.AddScoped<IServiceService, ServiceService>();
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

builder.Services.AddAuthentication("BasicAuthentication")
    .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);

builder.Services.AddAuthorization();

var app = builder.Build();

// Configuração do pipeline
if (app.Environment.IsDevelopment())
{
    app.UseCors("AllowAll");
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");

app.UseRouting(); // Deve vir antes de UseAuthorization e UseEndpoints
app.UseAuthentication();
app.UseAuthorization(); // Deve vir após UseRouting e antes de MapControllers

app.MapControllers();

app.MapGet("/", () => "API SonhandoERealizando está funcionando! Use /api/clientes para acessar os recursos.");

Console.WriteLine("Servidor em execução. Acesse http://localhost:5001/api/clientes para testar a API.");

app.Run();
