using System;
using MediatR.CQRS;

namespace FuzzyMatch.Core
{
    public abstract class ControllableModel : IControllable
    {
        public abstract String GetRoute();
    }
}