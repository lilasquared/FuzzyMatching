using System;
using FuzzyMatch.Core.Configuration;
using StructureMap;

namespace FuzzyMatch.Worker
{
    public class Program
    {
        public static void Main(String[] args)
        {
            var container = new Container(new DefaultRegistry());

            container.Configure(x => x.For<MatchQueueProcessor>().Use<MatchQueueProcessor>());

            var processor = container.GetInstance<MatchQueueProcessor>();
            processor.Start().Wait();
        }
    }
}
