using System;

namespace MediatR.CQRS
{
    public class SuccessResult<TPayload> : IResult<TPayload>
    {
        public Boolean IsSuccess => true;

        public Boolean IsFailure => false;

        public Exception Exception => throw new InvalidOperationException("A SuccessResult has no exception.");

        public TPayload Payload { get; }

        public SuccessResult(TPayload payload)
        {
            Payload = payload;
        }
    }
}
