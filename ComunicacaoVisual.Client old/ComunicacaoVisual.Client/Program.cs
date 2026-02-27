using ComunicacaoVisual.Client;
using ComunicacaoVisual.Client.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddScoped<PedidoService>();
// HttpClient apontando para sua API
// registra o handler
builder.Services.AddScoped<AuthMessageHandler>();

builder.Services.AddScoped(sp =>
{
    var handler = sp.GetRequiredService<AuthMessageHandler>();
    handler.InnerHandler = new HttpClientHandler();

    return new HttpClient(handler)
    {
        BaseAddress = new Uri("http://localhost:5001/")
    };
});
builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();

// AuthService como AuthenticationStateProvider
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<AuthenticationStateProvider, AuthService>();

await builder.Build().RunAsync();