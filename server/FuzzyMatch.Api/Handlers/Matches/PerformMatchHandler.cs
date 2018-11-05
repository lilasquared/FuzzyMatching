using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FuzzyMatch.Api.Configuration;
using FuzzyMatch.Api.Models;
using LiteDB;
using MediatR;
using MediatR.CQRS;

namespace FuzzyMatch.Api.Handlers.Matches
{
    public class PerformMatchHandler : IRequestHandler<PerformMatch, IResult<Unit>>
    {
        private readonly DatabaseOptions _dbOptions;

        public PerformMatchHandler(DatabaseOptions dbOptions)
        {
            _dbOptions = dbOptions;
        }

        public Task<IResult<Unit>> Handle(PerformMatch request, CancellationToken cancellationToken)
        {
            var levenshtein = new Levenshtein();
            using (var db = new LiteDatabase(_dbOptions.Path))
            {
                var matches = db.GetCollection<Match>();
                var match = matches.FindById(request.Id);

                match.Status = MatchStatus.Processing;
                matches.Update(match);

                var source = GetDataset(match.SourceId);
                var lookup = GetDataset(match.LookupId);

                var sourceData = db.FileStorage.OpenRead(source.FileId).ReadLines().ToList();
                var lookupData = db.FileStorage.OpenRead(lookup.FileId).ReadLines().ToList();

                Int32 sourceRecordId = 0, lookupRecordId = 0;
                foreach (var sourceRecord in sourceData)
                {
                    foreach (var lookupRecord in lookupData)
                    {
                        var ratio = levenshtein.GetRatio(sourceRecord, lookupRecord);
                        if (ratio >= match.Threshold)
                        {
                            match.Results.Add(new MatchResult
                            {
                                SourceRecordId = sourceRecordId,
                                LookupRecordId = lookupRecordId,
                                Ratio = ratio
                            });
                        }

                        lookupRecordId++;
                    }

                    sourceRecordId++;
                }

                match.Status = MatchStatus.Completed;
                matches.Update(match);
            }

            return Task.FromResult(Result.Success());
        }

        private Dataset GetDataset(Int32 id)
        {
            using (var db = new LiteDatabase(_dbOptions.Path))
            {
                var dataset = db.GetCollection<Dataset>().FindById(id);

                if (dataset == null)
                {
                    throw new InvalidOperationException($"Dataset {id} does not exist.");
                }

                return dataset;
            }
        }
    }
}
