using ElectronicHealthRecord.Domain.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ElectronicHealthRecord.Api.ExceptionHandlers;

public class GlobalExceptionHandler : IExceptionHandler
{
    private const int ClientClosedRequest = 499;

    private readonly IProblemDetailsService _problemDetailsService;
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(
        IProblemDetailsService problemDetailsService,
        ILogger<GlobalExceptionHandler> logger)
    {
        _problemDetailsService = problemDetailsService;
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        if (exception is OperationCanceledException && httpContext.RequestAborted.IsCancellationRequested)
        {
            _logger.LogDebug("Requisição cancelada pelo cliente: {Path}", httpContext.Request.Path);
            httpContext.Response.StatusCode = ClientClosedRequest;
            return true;
        }

        var (statusCode, title, detail) = exception switch
        {
            NotFoundException ex => (
                StatusCodes.Status404NotFound,
                "Recurso não encontrado",
                ex.Message
            ),
            ConflictException ex => (
                StatusCodes.Status409Conflict,
                "Conflito",
                ex.Message
            ),
            BusinessRuleException ex => (
                StatusCodes.Status422UnprocessableEntity,
                "Regra de negócio violada",
                ex.Message
            ),
            DbUpdateException ex when EhViolacaoDeUnicidade(ex) => (
                StatusCodes.Status409Conflict,
                "Conflito",
                "Já existe um registro com os mesmos dados únicos."
            ),
            _ => (
                StatusCodes.Status500InternalServerError,
                "Erro interno do servidor",
                "Ocorreu um erro interno inesperado. Por favor, tente novamente mais tarde."
            )
        };

        if (statusCode >= StatusCodes.Status500InternalServerError)
        {
            _logger.LogError(exception, "Erro não tratado em {Method} {Path}",
                httpContext.Request.Method, httpContext.Request.Path);
        }
        else
        {
            _logger.LogWarning("{Title} em {Method} {Path}: {Message}",
                title, httpContext.Request.Method, httpContext.Request.Path, exception.Message);
        }

        httpContext.Response.StatusCode = statusCode;

        return await _problemDetailsService.TryWriteAsync(new ProblemDetailsContext
        {
            HttpContext = httpContext,
            Exception = exception,
            ProblemDetails = new ProblemDetails
            {
                Status = statusCode,
                Title = title,
                Detail = detail,
                Instance = httpContext.Request.Path
            }
        });
    }

    private static bool EhViolacaoDeUnicidade(DbUpdateException exception)
    {
        var mensagem = exception.InnerException?.Message ?? string.Empty;

        return mensagem.Contains("UNIQUE constraint failed", StringComparison.OrdinalIgnoreCase)
            || mensagem.Contains("23505", StringComparison.Ordinal)
            || mensagem.Contains("duplicate key value", StringComparison.OrdinalIgnoreCase);
    }
}
