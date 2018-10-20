using System;
using System.Threading;
using System.Threading.Tasks;

namespace MediatR.CQRS
{
    public abstract class FluentMediator<TPayload, TResponse> where TResponse : class
    {
        private readonly IMediator _mediator;
        private IAction<TPayload> _action;
        private Func<IResult<TPayload>, TResponse> _onFailure;
        private Func<TPayload, TResponse> _onSuccess;

        protected FluentMediator(IMediator mediator)
        {
            _mediator = mediator;
        }

        public FluentMediator<TPayload, TResponse> Try(IAction<TPayload> request,
            CancellationToken cancellationToken = new CancellationToken())
        {
            _action = request;
            return this;
        }

        public FluentMediator<TPayload, TResponse> OnSuccess(Func<TPayload, TResponse> onSuccess)
        {
            _onSuccess = onSuccess;
            return this;
        }

        public FluentMediator<TPayload, TResponse> OnFailure(Func<IResult<TPayload>, TResponse> onFailure)
        {
            _onFailure = onFailure;
            return this;
        }

        public async Task<TResponse> Send()
        {
            var result = await _mediator.Send(_action);

            if (result.IsFailure)
            {
                return _onFailure(result);
            }

            return _onSuccess?.Invoke(result.Payload) ??
                   throw new InvalidOperationException("Success handler is not defined");
        }
    }
}
