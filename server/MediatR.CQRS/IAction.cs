namespace MediatR.CQRS
{
    public interface IAction<out TPayload> : IRequest<IResult<TPayload>> { }
}
