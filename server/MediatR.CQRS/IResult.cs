using System;

namespace MediatR.CQRS
{
    public interface IResult
    {
        Boolean IsSuccess { get; }
        Boolean IsFailure { get; }
        Exception Exception { get; }
    }

    public interface IResult<out TPayload> : IResult
    {
        TPayload Payload { get; }
    }
}
