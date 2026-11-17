using BlazorWeb.Common.Exceptions;
using Shared.Respons;
using System.Net;

namespace BlazorWeb.HttpHandlers;

public class ExceptionDelegatingHandler : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {            
        var response = await base.SendAsync(request, cancellationToken);

        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            await EvaluateStatusCode(response.StatusCode, response);
        }
        return response;
    }

    private static Task EvaluateStatusCode( HttpStatusCode code, HttpResponseMessage response)
    {
        return code switch
        {
            HttpStatusCode.BadRequest => ThrowExceptionHandler(response),

            HttpStatusCode.Unauthorized => throw new UnauthorizedException(),

            _ => throw new ApplicationException()
        };
    }
    private static async Task ThrowExceptionHandler(HttpResponseMessage responseMessage)
    {
        var errors = await responseMessage.Content.ReadFromJsonAsync<FailureResponse>();
        
        if (errors != null)
            throw new ValidationException(errors);
        else
            throw new ApplicationException(errors!.Detail);     
    }
}
