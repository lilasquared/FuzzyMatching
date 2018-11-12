using System;

namespace FuzzyMatch.Core
{
    public class QueueMessage : IQueueable
    {
        public Int32 Id { get; set; }
        public Object Payload { get; }
    }
}
