using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FuzzyMatch.Core.UoW;
using MediatR;
using MediatR.CQRS;

namespace FuzzyMatch.Core.Appends
{
    public class PerformAppendHandler : IRequestHandler<PerformAppend, IResult<Unit>>
    {
        private readonly DataUnitOfWork _uow;

        public PerformAppendHandler(DataUnitOfWork uow)
        {
            _uow = uow;
        }

        public Task<IResult<Unit>> Handle(PerformAppend request, CancellationToken cancellationToken)
        {
            var match = GetAppend(request.MatchId);

            if (match == null)
            {
                throw new InvalidOperationException();
            }

            match.Start();
            UpdateAppend(match);

            var levenshtein = new Levenshtein();
            var source = GetDataset(match.SourceId);
            var lookup = GetDataset(match.LookupId);

            var sourceData = GetData(source.FileId).ToArray();
            var lookupData = GetData(lookup.FileId).ToArray();

            try
            {
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
                    match.Progress = sourceRecordId * 100.0 / sourceData.Length;
                    UpdateAppend(match);
                }

                match.Finish();
                UpdateAppend(match);
            }
            catch (Exception ex)
            {
                match.Error(ex);
                UpdateAppend(match);
                throw;
            }

            return Task.FromResult(Result.Success());
        }

        private void UpdateAppend(Append append)
        {
            _uow.ExecuteCommand(db => db.GetCollection<Append>().Update(append));
        }

        private Append GetAppend(Int32 appendId)
        {
            return _uow.Execute(db => db.GetCollection<Append>().FindById(appendId));
        }

        private Dataset GetDataset(Int32 id)
        {
            return _uow.Execute(db =>
            {
                var dataset = db.GetCollection<Dataset>().FindById(id);

                if (dataset == null)
                {
                    throw new InvalidOperationException($"Dataset {id} does not exist.");
                }

                return dataset;
            });
        }

        private IEnumerable<String> GetData(String fileId)
        {
            return _uow.Execute(db => db.FileStorage.OpenRead(fileId).ReadLines().ToArray());
        }
    }
}
