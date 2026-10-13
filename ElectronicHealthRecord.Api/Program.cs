using ElectronicHealthRecord.Api.ExceptionHandlers;
using ElectronicHealthRecord.Application.Interfaces;
using ElectronicHealthRecord.Application.Services;
using ElectronicHealthRecord.Infrastructure.Data;
using ElectronicHealthRecord.Infrastructure.Repositories;
using ElectronicHealthRecord.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using System.Text;

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

builder.Services.AddScoped<ITokenService, TokenService>();

var chaveSecreta = builder.Configuration["Jwt:ChaveSecreta"]!;
var key = Encoding.UTF8.GetBytes(chaveSecreta);
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["Jwt:Emissor"],
        ValidateAudience = true,
        ValidAudience = builder.Configuration["Jwt:Publico"],
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddAuthorization();

var app = builder.Build();

app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
