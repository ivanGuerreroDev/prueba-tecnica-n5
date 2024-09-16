using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using webapi.Data;
using webapi.Models;
using webapi.Services;
using webapi.CQRS.Commands;
using webapi.CQRS.Queries;
using webapi.Handlers;
using webapi.Repositories;
using webapi.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Confluent.Kafka;

var builder = WebApplication.CreateBuilder(args);

// Configurar el DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configurar servicios
builder.Services.AddScoped<IPermissionRepository, PermissionRepository>();
builder.Services.AddScoped<IPermissionService, PermissionService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Configurar CQRS
builder.Services.AddScoped<ICommandHandler<ModifyPermissionCommand>, ModifyPermissionHandler>();
builder.Services.AddScoped<ICommandHandler<RequestPermissionCommand>, RequestPermissionHandler>();
builder.Services.AddScoped<IQueryHandler<GetPermissionsQuery, Permission>, GetPermissionsHandler>();

// Configuración de ElasticSearch
builder.Services.AddSingleton<IElasticClient>(provider =>
{
    var settings = new ConnectionSettings(new Uri("http://localhost:9200"))
        .DefaultIndex("permissions");
    return new ElasticClient(settings);
});

// Configuración de Kafka Consumer
builder.Services.AddHostedService<KafkaConsumerService>();

// Configurar servicios adicionales
builder.Services.AddControllers();

// Configurar Swagger para documentación
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(typeof(Program).Assembly);
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

var app = builder.Build();

// Configuración del middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Aplicar migraciones pendientes o crear base de datos si no existe
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.EnsureCreated();
    dbContext.Database.Migrate();
}

app.Run();
