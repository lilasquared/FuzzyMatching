using System;

namespace MediatR.CQRS
{
    public static class Result
    {
        public static IResult<TPayload> Success<TPayload>(TPayload payload)
        {
            return new SuccessResult<TPayload>(payload);
        }

        public static IResult<TPayload> Failure<TPayload>(Exception exception)
        {
            return new FailureResult<TPayload>(exception);
        }
    }
}
