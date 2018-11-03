using System;

namespace MediatR.CQRS
{
    public static class Result
    {
        public static IResult<Unit> Success()
        {
            return new SuccessResult<Unit>(Unit.Value);
        }

        public static IResult<TPayload> Success<TPayload>(TPayload payload)
        {
            return new SuccessResult<TPayload>(payload);
        }

        public static IResult<Unit> Failure(Exception exception)
        {
            return new FailureResult<Unit>(exception);
        }

        public static IResult<TPayload> Failure<TPayload>(Exception exception)
        {
            return new FailureResult<TPayload>(exception);
        }
    }
}
