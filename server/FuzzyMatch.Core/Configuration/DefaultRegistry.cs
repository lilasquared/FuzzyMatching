using System;
using FuzzyMatch.Core.Appends;
using FuzzyMatch.Core.UoW;
using LiteDB;
using MediatR;
using MediatR.CQRS;
using StructureMap;

namespace FuzzyMatch.Core.Configuration
{
    public delegate LiteDatabase ContextProvider();

    public class DefaultRegistry : Registry
    {
        public DefaultRegistry()
        {
            Scan(scan =>
            {
                scan.AssembliesFromApplicationBaseDirectory();
                scan.WithDefaultConventions();
                scan.LookForRegistries();

                scan.ConnectImplementationsToTypesClosing(typeof(IRequestHandler<>));
                scan.ConnectImplementationsToTypesClosing(typeof(IRequestHandler<,>));
            });

            For<IMediator>().Use<Mediator>();
            For<ServiceFactory>().Use<ServiceFactory>(ctx => ctx.GetInstance);
            For(typeof(IPipelineBehavior<,>)).Add(typeof(ExceptionHandlerBehavior<,>));

            For<DataUnitOfWork>()
                .Use<DataUnitOfWork>()
                .Ctor<ContextProvider>()
                .Is(() => new LiteDatabase(Environment.GetEnvironmentVariable("DB_PATH")));

            For<QueueUnitOfWork>()
                .Use<QueueUnitOfWork>()
                .Ctor<ContextProvider>()
                .Is(() => new LiteDatabase(Environment.GetEnvironmentVariable("QUEUE_PATH")));

            For<IQueueWriter<PerformAppend>>()
                .Use<LiteDatabaseQueue<PerformAppend>>();

            For<IQueueReader<PerformAppend>>()
                .Use<LiteDatabaseQueue<PerformAppend>>();
        }
    }
}
