using ComunicacaoVisual.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// 1. CONEXÃO COM SQL SERVER
builder.Services.AddDbContext<ControleVendasContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// 2. CONFIGURAÇÃO DE SEGURANÇA (JWT)
// Substitua "Sua_Chave_Secreta_Muito_Longa_E_Segura_123" por uma chave forte
var chave = Encoding.ASCII.GetBytes("HIqZPFh1CXELeez3lXTi");
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(chave),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirTudo", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

// app.UseHttpsRedirection(); // Opcional para rede local se não usar SSL
app.UseCors("PermitirTudo");

// IMPORTANTE: A ordem aqui importa! Autenticação antes de Autorização.
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();