using Application.Common.Models;
using FluentValidation;
using MediatR;

namespace Application.Common.Behaviours;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }
    
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (_validators.Any())
        {
            var context = new ValidationContext<TRequest>(request);
            var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
            var failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();

            if (failures.Any())
            {
                var responseType=typeof(TResponse);
                if (responseType.IsGenericType && responseType.GetGenericTypeDefinition() == typeof(ResponseModel<>))
                {
                    var responseInstance=(dynamic)Activator.CreateInstance(responseType)!;
                    responseInstance.Success = false;
                    responseInstance.Messages = failures.Select(f => f.ErrorMessage).ToArray();
                    responseInstance.StatusCode = 400;
                    
                    return responseInstance;
                }
                
                throw new ValidationException(failures);
            }
        }
        return await next();
    }
}