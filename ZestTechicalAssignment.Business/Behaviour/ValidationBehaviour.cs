using FluentValidation;
using FluentValidation.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZestTechicalAssignment.Business.Behaviour
{
    public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : class, IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(next);

            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);

                var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context)));

                var failures = validationResults
                    .Where(r => r.Errors.Count > 0)
                    .SelectMany(r => r.Errors)
                    .Where(f => f != null)
                    .GroupBy(
                    x => x.PropertyName,
                    x => x.ErrorMessage,
                    (propertyName, errorMessages) => new
                    {
                        Key = propertyName,
                        Values = errorMessages.Distinct().ToArray()
                    }).Select(x => new ValidationFailure(x.Key, string.Join(",", x.Values)))
                    .ToList();

                if (failures.Count != 0)
                    throw new FluentValidation.ValidationException(failures);
            }
            return await next();
        }
    }
}
