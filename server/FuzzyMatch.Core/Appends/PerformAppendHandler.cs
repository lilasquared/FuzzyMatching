using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FuzzyMatch.Core.Configuration;
using MediatR;
using MediatR.CQRS;

namespace FuzzyMatch.Core.Appends
{
    public class PerformAppendHandler : IRequestHandler<PerformAppend, IResult<Unit>>
    {
        private readonly LiteDatabaseProvider _provider;

        public PerformAppendHandler(LiteDatabaseProvider provider)
        {
            _provider = provider;
        }

        public Task<IResult<Unit>> Handle(PerformAppend request, CancellationToken cancellationToken)
        {
            using (var db = _provider(DataContext.Data))
            {
                var matches = db.GetCollection<Append>();
                var match = matches.FindById(request.MatchId);

                if (match == null)
                {
                    throw new InvalidOperationException();
                }

                match.Start();
                matches.Update(match);

                var levenshtein = new Levenshtein();
                var source = GetDataset(match.SourceId);
                var lookup = GetDataset(match.LookupId);

                var sourceData = db.FileStorage.OpenRead(source.FileId).ReadLines().ToList();
                var lookupData = db.FileStorage.OpenRead(lookup.FileId).ReadLines().ToList();

                var sourceRecordId = 0;
                foreach (var sourceRecord in sourceData)
                {
                    var lookupRecordId = 0;
                    foreach (var lookupRecord in lookupData)
                    {
                        var ratio = levenshtein.GetRatio(sourceRecord, lookupRecord);
                        if (ratio >= match.Threshold)
                        {
                            match.Results.Add(new AppendResult
                            {
                                SourceRecordId = sourceRecordId,
                                SourceRecord = sourceRecord,
                                LookupRecordId = lookupRecordId,
                                LookupRecord = lookupRecord,
                                Ratio = ratio
                            });
                        }

                        lookupRecordId++;
                    }

                    sourceRecordId++;
                    match.Progress = sourceRecordId * 100.0 / sourceData.Count;
                    matches.Update(match);
                }

                match.Finish();
                matches.Update(match);
            }

            return Task.FromResult(Result.Success());
        }

        private Dataset GetDataset(Int32 id)
        {
            using (var db = _provider(DataContext.Data))
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
