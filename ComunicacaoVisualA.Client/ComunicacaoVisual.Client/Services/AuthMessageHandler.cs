using Microsoft.JSInterop;
using System.Net;
using System.Net.Http.Headers;

namespace ComunicacaoVisual.Client.Services;

public class AuthMessageHandler : DelegatingHandler
{
    private readonly IJSRuntime _js;

    public AuthMessageHandler(IJSRuntime js) => _js = js;

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        var token = await _js.InvokeAsync<string>("localStorage.getItem", "authToken");

        if (!string.IsNullOrWhiteSpace(token))
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await base.SendAsync(request, cancellationToken);

        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            await _js.InvokeVoidAsync("localStorage.removeItem", "authToken");
            await _js.InvokeVoidAsync("localStorage.removeItem", "userName");
            await _js.InvokeVoidAsync("localStorage.removeItem", "userLevel");
        }

        return response;
    }
}