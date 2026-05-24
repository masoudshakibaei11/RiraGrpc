using FluentValidation;
using Grpc.Core;
using Grpc.Core.Interceptors;

namespace Rira.Presentation.Interceptors;

public class ValidatorInterceptor : Interceptor
{
    private readonly IServiceProvider _serviceProvider;

    public ValidatorInterceptor(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(
        TRequest request,
        ServerCallContext context,
        UnaryServerMethod<TRequest, TResponse> continuation)
    {

        var validator = _serviceProvider.GetService<IValidator<TRequest>>();

        if (validator != null)
        {
            var result = await validator.ValidateAsync(request);
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }
        }

        return await continuation(request, context);
    }
}
