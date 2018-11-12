using System;
using System.Threading;
using System.Threading.Tasks;

namespace MediatR.CQRS
{
    public class ExceptionHandlerBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TResponse : class, IResult
    {
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            try
            {
                return await next();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                var resultType = typeof(TResponse);

                if (resultType.IsGenericType)
                {
                    var genericResultType = resultType.GetGenericArguments()[0];
                    var failureResultType = typeof(FailureResult<>).MakeGenericType(genericResultType);
                    return Activator.CreateInstance(failureResultType, ex) as TResponse;
                }

                return new FailureResult(ex) as TResponse;
            }
        }
    }
}
