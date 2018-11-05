using System;
using System.Threading.Tasks;
using MediatR;
using MediatR.CQRS.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FuzzyMatch.Api.Abstracts
{
    public class BaseController<T> : Controller where T : class
    {
        private readonly IMediator _mediator;

        public BaseController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public Task<IActionResult> GetAll()
        {
            return _mediator
                .Try(new GetAll<T>())
                .OnSuccess(Json)
                .OnFailure(result => StatusCode(StatusCodes.Status500InternalServerError))
                .Send();
        }

        [HttpGet("{id}")]
        public Task<IActionResult> GetOne(Int32 id)
        {
            return _mediator
                .Try(new GetOne<T>(id))
                .OnSuccess(Json)
                .OnFailure(result => NotFound(id))
                .Send();
        }

        [HttpPost]
        public Task<IActionResult> Create([FromBody] T model)
        {
            return _mediator
                .Try(new Create<T>(model))
                .OnSuccess(Json)
                .OnFailure(result => BadRequest())
                .Send();
        }

        [HttpDelete("{id}")]
        public Task<IActionResult> Delete(Int32 id)
        {
            return _mediator
                .Try(new Delete<T>(id))
                .OnSuccess(payload => Accepted())
                .OnFailure(result => BadRequest())
                .Send();
        }
    }
}
