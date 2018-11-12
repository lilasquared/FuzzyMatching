using System;
using System.Collections.Generic;
using FuzzyMatch.Core.Appends;
using LiteDB;
using MediatR;
using MediatR.CQRS;
using StructureMap;

namespace FuzzyMatch.Core.Configuration
{
    public static class DataContext
    {
        public static String Data => "data";
        public static String Queue => "queue";
    }

    public delegate LiteDatabase LiteDatabaseProvider(String key);

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

            var dbs = new Dictionary<String, String>
            {
                {DataContext.Data, Environment.GetEnvironmentVariable("DB_PATH")},
                {DataContext.Queue, Environment.GetEnvironmentVariable("QUEUE_PATH")}
            };

            For<LiteDatabaseProvider>()
                .Use<LiteDatabaseProvider>(key => new LiteDatabase(dbs[key]));

            For<IQueueWriter<PerformAppend>>()
                .Use<LiteDatabaseQueue<PerformAppend>>();

            For<IQueueReader<PerformAppend>>()
                .Use<LiteDatabaseQueue<PerformAppend>>();
        }
    }
}
