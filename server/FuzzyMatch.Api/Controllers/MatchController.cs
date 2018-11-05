﻿using System.Linq;
using System.Threading.Tasks;
using FuzzyMatch.Api.Handlers.Matches;
using FuzzyMatch.Api.Models;
using MediatR;
using MediatR.CQRS.Requests;
using Microsoft.AspNetCore.Mvc;

namespace FuzzyMatch.Api.Controllers
{
    [Route("/api/matches")]
    public class MatchController : Controller
    {
        private readonly IMediator _mediator;

        public MatchController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public Task<IActionResult> Create(CreateMatch match)
        {
            return _mediator.Try(match)
                .OnSuccess(payload => Ok())
                .OnFailure(result => BadRequest())
                .Send();
        }

        [HttpGet]
        public Task<IActionResult> GetAll()
        {
            return _mediator.Try(new GetAll<Match>())
                .OnSuccess(payload => Json(payload.Select(MatchSummary.FromMatch)))
                .OnFailure(result => BadRequest())
                .Send();
        }
    }
}