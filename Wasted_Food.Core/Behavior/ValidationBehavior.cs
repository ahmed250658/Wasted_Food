using FluentValidation;
using MediatR;
using Microsoft.Extensions.Localization;
using Wasted_Food.Core.Shared;

namespace Wasted_Food.Core.Behavior
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;
        public readonly IEnumerable<IValidator<TRequest>> _vaildators;
        #endregion

        #region Constructor
        public ValidationBehavior(IEnumerable<IValidator<TRequest>> vaildators, IStringLocalizer<SharedResource> stringLocalizer)
        {
            _vaildators = vaildators;
            _stringLocalizer = stringLocalizer;
        }
        #endregion

        #region Handle Function
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_vaildators.Any())
            {
                var context = new ValidationContext<TRequest>(request);
                var vaildationResults = await Task.WhenAll(_vaildators.Select(v => v.ValidateAsync(context, cancellationToken)));
                var failure = vaildationResults.SelectMany(x => x.Errors).Where(f => f != null).ToList();
                if (failure.Count != 0)
                {
                    var message = failure.Select(x => _stringLocalizer[x.PropertyName] + ":" + _stringLocalizer[x.ErrorMessage]).FirstOrDefault();
                    throw new ValidationException(message);
                }
            }
            return await next();
        }
        #endregion
    }
}
