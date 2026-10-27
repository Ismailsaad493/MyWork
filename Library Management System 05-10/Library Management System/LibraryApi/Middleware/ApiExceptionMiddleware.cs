using System.ComponentModel.DataAnnotations;
using LibraryApi.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace LibraryApi.Middleware;

public sealed class ApiExceptionMiddleware(
    RequestDelegate next,
    ILogger<ApiExceptionMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (OperationCanceledException) when (context.RequestAborted.IsCancellationRequested)
        {
        }
        catch (Exception exception)
        {
            await WriteProblemAsync(context, exception);
        }
    }

    private async Task WriteProblemAsync(HttpContext context, Exception exception)
    {
        var (status, title, detail) = exception switch
        {
            EntityNotFoundException => (StatusCodes.Status404NotFound, "Resource not found", exception.Message),
            ConflictException => (StatusCodes.Status409Conflict, "Conflict", exception.Message),
            ValidationException => (StatusCodes.Status400BadRequest, "Invalid request", exception.Message),
            DbUpdateConcurrencyException => (StatusCodes.Status409Conflict, "Conflict", "The resource changed during this request. Please reload and try again."),
            DbUpdateException when IsConstraintViolation(exception) =>
                (StatusCodes.Status409Conflict, "Conflict", "The change conflicts with existing library data."),
            _ => (StatusCodes.Status500InternalServerError, "Server error", "An unexpected error occurred.")
        };

        if (status == StatusCodes.Status500InternalServerError)
        {
            logger.LogError(exception, "Unhandled API exception for {Path}", context.Request.Path);
        }
        else if (exception is DbUpdateException or DbUpdateConcurrencyException)
        {
            logger.LogWarning(exception, "A database update conflicted for {Path}", context.Request.Path);
        }

        context.Response.StatusCode = status;
        context.Response.ContentType = "application/problem+json";
        var problem = new ProblemDetails
        {
            Status = status,
            Title = title,
            Detail = detail,
            Instance = context.Request.Path
        };
        problem.Extensions["traceId"] = context.TraceIdentifier;
        await context.Response.WriteAsJsonAsync(problem, cancellationToken: context.RequestAborted);
    }

    private static bool IsConstraintViolation(Exception exception) =>
        exception.InnerException is SqlException { Number: 2601 or 2627 or 547 };
}
