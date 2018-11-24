using System;

namespace DependencyManager.Interfaces
{
    public interface IParameterResolver : IResolver
    {
        ValueType[] ValueArgs { get; }
    }
}
