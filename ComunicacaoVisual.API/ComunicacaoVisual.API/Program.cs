using ComunicacaoVisual.API.Models;

var builder = WebApplication.CreateBuilder(args);

// 1. Configurações de Serviços
builder.Services.AddDbContext<ControleVendasContext>();

builder.Services.AddCors(options => {
    options.AddPolicy("PermitirTudo", b => b.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

builder.Services.AddControllers();

// ADICIONE ESTAS DUAS LINHAS AQUI (Importante para o Play funcionar):
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirTudo",
        policy =>
        {
            policy.AllowAnyOrigin() // Permite que o Base44 ou seu navegador acessem a API
                  .AllowAnyMethod() // Permite GET, POST, PUT, DELETE
                  .AllowAnyHeader(); // Permite enviar textos, fotos, etc.
        });
});


var app = builder.Build();

// 2. Configurações do App (A ordem aqui importa!)

// ADICIONE ESTAS TRÊS LINHAS AQUI (Para abrir a tela de teste):
// Removemos o "if" e as chaves para o Swagger funcionar sempre
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseCors("PermitirTudo");


// O CORS deve vir ANTES da Autorização
app.UseCors("PermitirTudo");

app.UseAuthorization();

app.MapControllers();

app.Run();