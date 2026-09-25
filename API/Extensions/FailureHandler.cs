using Microsoft.AspNetCore.Mvc;
using Shared.ResultService;

namespace API.Extensions;

public static class FailureHandler
{
    public static IResult HandleFailure(Result result) =>
        result switch
        {
            { IsSuccess: true } => throw new InvalidOperationException(),
            ValidationErrors validationErrors => Results.BadRequest( CreateProblemDetails(
                        "Validation Error",
                        StatusCodes.Status400BadRequest,
                        result.Error,
                        validationErrors.Errors)),
            _ =>
                Results.BadRequest( CreateProblemDetails(
                        "Bad Request",
                        StatusCodes.Status400BadRequest,
                        result.Error))
        };

    private static ProblemDetails CreateProblemDetails(string title,int status, Error error, Error[]? errors = null) =>
        new()
        {
            Title = title,
            Type = error.Code,
            Detail = error.Message,
            Status = status,
            Extensions = { { nameof(errors), errors } }
        };

    private static ProblemDetails CreateProblemDetails(string title, int status, Error errors) =>
       new()
       {
           Title = title,
           Type = errors.Code,
           Detail = errors.Message,
           Status = status,
           Extensions = { { nameof(errors), new[] { errors } } }
       };
}
