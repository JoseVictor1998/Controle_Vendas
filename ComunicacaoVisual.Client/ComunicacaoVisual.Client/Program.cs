using ComunicacaoVisual.Client;
using ComunicacaoVisual.Client.Services; // Adicione esta linha para reconhecer a pasta Services
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Configuração da sua API no Docker
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("http://localhost:5001/api")
});

// REGISTRO OBRIGATÓRIO: Isso faz a linha vermelha do AuthService sumir
builder.Services.AddScoped<AuthService>();

await builder.Build().RunAsync();