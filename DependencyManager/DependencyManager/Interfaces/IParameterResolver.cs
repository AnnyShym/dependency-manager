using System;

namespace DependencyManager.Interfaces
{
    public interface IParameterResolver
    {

        Type TypeToResolve { get; }
        ValueType[] ValueArgs { get; }

    }
}
