using MediatR;
using MediatR.CQRS;
using Microsoft.AspNetCore.Mvc;

namespace FuzzyMatch.Api
{
    public class FluentHttpMediator<TPayload> : FluentMediator<TPayload, IActionResult>
    {
        public FluentHttpMediator(IMediator mediator) : base(mediator) { }
    }

    public static class MediatorExtensions
    {
        public static FluentHttpMediator<TPayload> Try<TPayload>(this IMediator mediator, IAction<TPayload> action)
        {
            return new FluentHttpMediator<TPayload>(mediator).Try(action) as FluentHttpMediator<TPayload>;
        }
    }
}
