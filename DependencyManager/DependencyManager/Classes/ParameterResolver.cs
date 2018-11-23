using DependencyManager.Interfaces;
using System;

namespace DependencyManager.Classes
{
    public class ParameterResolver : IParameterResolver
    {

        public Type TypeToResolve { get; }
        public ValueType[] ValueArgs { get; }

        public ParameterResolver(Type typeToResolve, params ValueType[] args)
        {

            if (typeToResolve == null)
                throw new ArgumentNullException(Properties.Resources.ArgumentNullExceptionMessage);

            TypeToResolve = typeToResolve;
            ValueArgs = args;

        }

    }
}
