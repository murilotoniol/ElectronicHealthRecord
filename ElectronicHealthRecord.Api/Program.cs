using Scalar.AspNetCore;
using ElectronicHealthRecord.Api.ExceptionHandlers;
using ElectronicHealthRecord.Application.Interfaces;
using ElectronicHealthRecord.Application.Services;
using ElectronicHealthRecord.Infrastructure.Data;
using ElectronicHealthRecord.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddControllers();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddOpenApi();
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite(connectionString));

builder.Services.AddScoped<IPacienteRepository, PacienteRepository>();
builder.Services.AddScoped<IPacienteService, PacienteService>();

builder.Services.AddScoped<IAtendimentoRepository, AtendimentoRepository>();
builder.Services.AddScoped<IAtendimentoService, AtendimentoService>();

builder.Services.AddScoped<IProfissionalRepository, ProfissionalRepository>();
builder.Services.AddScoped<IProfissionalService, ProfissionalService>();

builder.Services.AddScoped<IRegistroClinicoRepository, RegistroClinicoRepository>();
builder.Services.AddScoped<IRegistroClinicoService, RegistroClinicoService>();

builder.Services.AddScoped<IPrescricaoRepository, PrescricaoRepository>();
builder.Services.AddScoped<IPrescricaoService, PrescricaoService>();

var app = builder.Build();

app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
