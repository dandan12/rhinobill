namespace rhinobill.core.Pipelines.Abstractions
{
    public interface ICommand<TResponse> : IRequest<Result<TResponse>>;
    public interface ICommandHandler<TCommand, TResponse> : IRequestHandler<TCommand, Result<TResponse>> where TCommand : ICommand<TResponse>;
}
