using System;

namespace MediatR.CQRS
{
    public class FailureResult : IResult
    {
        public Boolean IsSuccess => false;

        public Boolean IsFailure => true;

        public Exception Exception { get; }

        public FailureResult(Exception ex)
        {
            Exception = ex;
        }
    }

    public class FailureResult<TPayload> : FailureResult, IResult<TPayload>
    {
        public TPayload Payload => throw new InvalidOperationException("A FailureResult has no payload.");

        public FailureResult(Exception ex) : base(ex) { }
    }
}
