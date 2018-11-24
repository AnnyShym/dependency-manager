using System;

namespace DependencyManager.Interfaces
{
    public interface IResolver
    {
        Type TypeToResolve { get; }
    }
}
