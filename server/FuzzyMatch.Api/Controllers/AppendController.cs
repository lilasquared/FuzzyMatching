using System;
using System.Linq;
using System.Threading.Tasks;
using FuzzyMatch.Api.Handlers.Matches;
using FuzzyMatch.Core;
using FuzzyMatch.Core.Appends;
using MediatR;
using MediatR.CQRS.Requests;
using Microsoft.AspNetCore.Mvc;

namespace FuzzyMatch.Api.Controllers
{
    [Route("/api/appends")]
    public class AppendController : Controller
    {
        private readonly IMediator _mediator;

        public AppendController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public Task<IActionResult> Create([FromBody] CreateMatch match)
        {
            return _mediator.Try(match)
                .OnSuccess(payload => Ok())
                .OnFailure(result => BadRequest())
                .Send();
        }

        [HttpGet]
        public Task<IActionResult> GetAll()
        {
            return _mediator.Try(new GetAll<Append>())
                .OnSuccess(payload => Json(payload.Select(AppendSummary.FromMatch)))
                .OnFailure(result => BadRequest())
                .Send();
        }

        [HttpDelete]
        [Route("{id}")]
        public Task<IActionResult> Delete(Int32 id)
        {
            return _mediator.Try(new Delete<Append>(id))
                .OnSuccess(payload => Accepted())
                .OnFailure(result => BadRequest())
                .Send();
        }

        [HttpGet]
        [Route("{id}")]
        public Task<IActionResult> Get(Int32 id)
        {
            return _mediator.Try(new GetOne<Append>(id))
                .OnSuccess(Json)
                .OnFailure(result => BadRequest())
                .Send();
        }

        [HttpPost]
        [Route("{id}/start")]
        public Task<IActionResult> Start(Int32 id)
        {
            return _mediator.Try(new StartMatch(id))
                .OnSuccess(payload => Accepted())
                .OnFailure(result => BadRequest())
                .Send();
        }
    }
}
