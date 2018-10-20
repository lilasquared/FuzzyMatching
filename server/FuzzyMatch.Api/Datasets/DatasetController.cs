using System;
using System.Threading.Tasks;
using FuzzyMatch.Api.Requests;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FuzzyMatch.Api.Datasets
{
    [Route("api/datasets")]
    public class DatasetController : Controller
    {
        private readonly IMediator _mediator;

        public DatasetController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public Task<IActionResult> GetAll()
        {
            return _mediator.Try(new GetAll<Dataset>())
                .OnSuccess(Json)
                .OnFailure(result => BadRequest())
                .Send();
        }

        [HttpPost]
        public Task<IActionResult> Create([FromBody] Dataset dataset)
        {
            return _mediator.Try(new Create<Dataset>(dataset))
                .OnSuccess(Json)
                .OnFailure(result => BadRequest())
                .Send();
        }

        [HttpPost]
        [Route("{datasetId}/data")]
        public Task<IActionResult> UploadData(Int32 datasetId, IFormFile file)
        {
            return _mediator
                .Try(new UploadData
                {
                    DatasetId = datasetId,
                    FileName = file.FileName,
                    FileStream = file.OpenReadStream()
                })
                .OnSuccess(payload => Json(payload))
                .OnFailure(result => BadRequest())
                .Send();
        }
    }
}
