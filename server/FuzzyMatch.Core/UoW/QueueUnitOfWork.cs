using FuzzyMatch.Core.Configuration;

namespace FuzzyMatch.Core.UoW
{
    public class QueueUnitOfWork : UnitOfWork
    {
        public QueueUnitOfWork(ContextProvider provider) : base(provider) { }
    }
}
