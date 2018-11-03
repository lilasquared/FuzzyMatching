using System;
using System.Threading.Tasks;
using FuzzyMatch.Api.Handlers.Datasets;
using FuzzyMatch.Api.Models;
using MediatR;
using MediatR.CQRS.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FuzzyMatch.Api.Controllers
{
    [Route("/api/datasets")]
    public class DatasetController : Controller
    {
        private readonly IMediator _mediator;

        public DatasetController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create(String name, IFormFile file)
        {
            if (!file.FileName.EndsWith(".txt"))
            {
                return BadRequest("Only .txt file extensions are supported");
            }

            return await _mediator
                .Try(new CreateDataset
                {
                    Name = name,
                    FileStream = file.OpenReadStream(),
                    FileName = file.FileName
                })
                .OnSuccess(Json)
                .OnFailure(result => BadRequest())
                .Send();
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return await _mediator
                .Try(new GetAll<Dataset>())
                .OnSuccess(Json)
                .OnFailure(result => BadRequest())
                .Send();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(Int32 id)
        {
            return await _mediator
                .Try(new Delete<Dataset>(id))
                .OnSuccess(payload => Accepted())
                .OnFailure(result => BadRequest())
                .Send();
        }
    }
}
