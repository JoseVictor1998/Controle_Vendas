using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;




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



var jwtKey = builder.Configuration["Jwt:Key"];
Console.WriteLine("JWT KEY (Program.cs): " + jwtKey);
if (string.IsNullOrWhiteSpace(jwtKey))
    throw new Exception("Jwt:Key NÃO carregou. Verifique Jwt__Key no docker-compose e appsettings.json.");

var chave = Encoding.UTF8.GetBytes(jwtKey);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;

        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                var header = context.Request.Headers.Authorization.ToString();
                Console.WriteLine($"AUTH HEADER RAW: '{header}'");

                // Se o middleware já parseou, vai estar aqui
                var t = context.Token;

                // Se não parseou, tenta extrair só para LOG (não setar)
                if (string.IsNullOrWhiteSpace(t) && header.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
                    t = header.Substring("Bearer ".Length);

                t = t?.Trim();

                if (string.IsNullOrWhiteSpace(t))
                {
                    Console.WriteLine("JWT: vazio");
                }
                else
                {
                    var parts = t.Split('.');
                    Console.WriteLine($"JWT: len={t.Length} parts={parts.Length}");
                    Console.WriteLine($"JWT first10='{t.Substring(0, Math.Min(10, t.Length))}' last10='{t.Substring(Math.Max(0, t.Length - 10))}'");
                }

                return Task.CompletedTask;
            },


        };

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(chave),

            ValidateIssuer = false,
            ValidateAudience = false,

            ValidateLifetime = true,
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

    options.OperationFilter<SwaggerAuthorizeCheckOperationFilter>();

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

app.Use(async (context, next) =>
{
    Console.WriteLine($"AUTH HEADER RAW: '{context.Request.Headers.Authorization}'");
    await next();
});

app.Use(async (context, next) =>
{
    var auth = context.Request.Headers.Authorization.ToString();
    Console.WriteLine($"AUTH HEADER RAW: '{auth}'");
    await next();
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();