using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

// APELIDO PARA O SWAGGER: Apontamos para o nível anterior para evitar o erro
// Mudamos o apelido para um nível acima para fugir do erro de "Models"


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ComunicacaoVisual.API.DBModels.ControleVendasContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlOptions =>
        {
            sqlOptions.EnableRetryOnFailure(
                maxRetryCount: 10,
                maxRetryDelay: TimeSpan.FromSeconds(10),
                errorNumbersToAdd: null
            );
        }
    )
);



// 1. Definição da Chave (Lógica limpa sem duplicidade)
var jwtKey = builder.Configuration["Jwt:Key"]
             ?? builder.Configuration["Jwt__Key"]
             ?? "8jU4Cm8P653gAYuvU5p1_Segredo2026_Longa";

// 2. Transformação em Bytes usando UTF8
var chave = Encoding.UTF8.GetBytes(jwtKey);

// 3. Configuração da Autenticação
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(chave),

            // Mantemos estas como 'false' para evitar erros de ambiente no Docker
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = false,

            // Zera a tolerância de tempo para validação imediata
            ClockSkew = TimeSpan.Zero
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

builder.Services.AddSwaggerGen(options =>
{
    // Usamos 'dynamic' para forçar o VS a ignorar o erro de namespace na compilação
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "ComunicacaoVisual API",
        Version = "v1"
    });

    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Coloque apenas o token JWT abaixo."
    });

    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();


app.UseCors("PermitirTudo");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();