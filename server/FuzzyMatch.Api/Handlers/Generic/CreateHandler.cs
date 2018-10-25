﻿using System.Threading;
using System.Threading.Tasks;
using FuzzyMatch.Api.Configuration;
using MediatR;
using MediatR.CQRS;
using MediatR.CQRS.Requests;

namespace FuzzyMatch.Api.Handlers.Generic
{
    public class CreateHandler<TModel> : IRequestHandler<Create<TModel>, IResult<TModel>>
    {
        private readonly DatabaseOptions _dbOptions;

        public CreateHandler(DatabaseOptions dbOptions)
        {
            _dbOptions = dbOptions;
        }

        public Task<IResult<TModel>> Handle(Create<TModel> request, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                using (var db = new LiteDB.LiteDatabase(_dbOptions.Path))
                {
                    db.GetCollection<TModel>().Insert(request.Model);
                    return Result.Success(request.Model);
                }
            }, cancellationToken);
        }
    }
}
