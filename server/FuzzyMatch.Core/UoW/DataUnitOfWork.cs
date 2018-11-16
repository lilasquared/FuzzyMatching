using FuzzyMatch.Core.Configuration;

namespace FuzzyMatch.Core.UoW
{
    public class DataUnitOfWork : UnitOfWork
    {
        public DataUnitOfWork(ContextProvider provider) : base(provider) { }
    }
}