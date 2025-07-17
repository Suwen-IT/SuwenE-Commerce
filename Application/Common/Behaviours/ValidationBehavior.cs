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
                    var dataType = responseType.GetGenericArguments()[0];

                    var constructor=typeof(ResponseModel<>)
                        .MakeGenericType(dataType)
                        .GetConstructor(new[] { typeof(string[]), typeof(int) });

                    if (constructor != null)
                    {
                        var instance = constructor.Invoke(new object[]
                         {
                             failures.Select(f => f.ErrorMessage).ToArray(),400 
                         });

                        return (TResponse)instance!;
                    }

                    throw new InvalidOperationException($"ResponseModel<{dataType.Name}> uygun bir constructor ile oluþturulamadý.");
                }
                
                throw new ValidationException(failures);
            }
        }
        return await next();
    }
}