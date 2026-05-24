using FluentValidation;
using Grpc.Core;
using Grpc.Core.Interceptors;

namespace Rira.Presentation.Interceptors;

public class ExceptionInterceptor : Interceptor
{
    private readonly ILogger<ExceptionInterceptor> _logger;


    public ExceptionInterceptor(ILogger<ExceptionInterceptor> logger)
    {
        _logger = logger;
    }
    public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(
    TRequest request, ServerCallContext context, UnaryServerMethod<TRequest, TResponse> continuation)
    {
        try
        {
            return await continuation(request, context);
        }
        catch (ValidationException ex) 
        {
            _logger.LogWarning("Validation failed for {Method}: {Errors}", context.Method, ex.Message);


            _logger.LogWarning("Validation failed: {Message}", ex.Message);


            throw new RpcException(new Status(StatusCode.InvalidArgument, ex.Message));
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogError(ex, "Resource not found in {Method}", context.Method);
            throw new RpcException(new Status(StatusCode.NotFound, ex.Message));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception in {Method}", context.Method);
            throw new RpcException(new Status(StatusCode.Internal, "خطای داخلی سرور رخ داده است."));
        }
    }


}
