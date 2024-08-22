using FluentValidation;

namespace rhinobill.core.Pipelines.Behaviours
{
    public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> fluentValidators;

        public ValidationBehaviour(IEnumerable<IValidator<TRequest>> fluentValidators)
        {
            this.fluentValidators = fluentValidators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
           var errorResult = await BuildErrorResult(request, cancellationToken);
            if (errorResult is not null)
                throw new ResultException(errorResult);

            return await next();
        }

        private async Task<ErrorResult> BuildErrorResult(TRequest request, CancellationToken cancellationToken)
        {
            foreach (var validator in fluentValidators)
            {
                var result = await validator.ValidateAsync(request, cancellationToken);
                if (!result.IsValid && result.Errors.Any())
                {
                    var error = result.Errors.First();
                    return new ErrorResult(error.ErrorCode, error.ErrorMessage, ErrorType.BadRequest);
                }
            }

            return null;
        }
    }
}
